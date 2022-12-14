using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    using NBear.Data;
    public class PptWeighDataService : BaseService<PptWeighData>, IPptWeighDataService
    {
        #region 构造方法

        public PptWeighDataService() : base() { }

        public PptWeighDataService(string connectStringKey) : base(connectStringKey) { }

        public PptWeighDataService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        #region 查询条件
        public class QueryParams
        {
            public PageResult<PptWeighData> PageParams { get; set; }
            public string EquipCode { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string MaterCode { get; set; }
            public string WeightType { get; set; }
        }
        #endregion
        #region IPptWeighService 成员
        /// <summary>
        /// 根据计划ID查询出小料称量信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataTable GetSmallMaterWeighListByPlanID(string planID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
                            declare @sql varchar(8000)
                          

                            set @sql='select CAST(ROW_NUMBER() OVER(ORDER BY Barcode) AS NVARCHAR(max)) 物料 , BarCode AS 车条码 '
                            select @sql = @sql + ' ,max(case When  MaterName =  ''' + MaterName + ''' and WeightID = '+CONVERT(Varchar,WeightID)+' then RealWeight else 0 end) [' + MaterName + ']'+ CHAR(13) + CHAR(10)
                            from (select DISTINCT MaterName, WeightID FROM dbo.PptWeighData WHERE PlanID='{0}') as a
                            set @sql = @sql +' ,(select sum(realweight) from PptWeighData where Barcode = b.Barcode  ) 总重 , SerialID AS 车次号  from PptWeighData b WHERE  PlanID=''{0}''  group by Barcode , SerialID '
                            PRINT @sql
                            exec(@sql)


");
            string sql = string.Format(sb.ToString(), planID);
            return this.GetBySql(sql).ToDataSet().Tables[0];

        }


        /// <summary>
        /// 根据计划ID查询出小料称量标准信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataTable GetSmallMaterWeighStandardByPlanID(string planID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
                            declare @sql varchar(max)
                            set @sql = 'select top 1 ''标准'' 物料 , '''' AS 车条码  '
                            select @sql = @sql + ' , max(case When  MaterName =  ''' + MaterName + ''' and WeightID = '+CONVERT(Varchar,WeightID)+' then SetWeight else 0 end) [' + MaterName + ']'
                            from (select DISTINCT MaterName, WeightID  FROM dbo.PptWeighData WHERE PlanID='{0}') as a
                            set @sql = @sql +' ,Sum(SetWeight) 总重 , '''' AS 车次号  , '''' AS 物料条码 from PptWeighData WHERE  PlanID=''{0}''  group by planID , Barcode , SerialID '
                            SET @sql =@sql+' Union '
                            set @sql =@sql+ ' select top 1 ''误差(±)'' 物料 , '''' AS 车条码  '
                            select @sql = @sql + ' , max(case When  MaterName =  ''' + MaterName + ''' and WeightID = '+CONVERT(Varchar,WeightID)+' then ErrorAllow else 0 end) [' + MaterName + ']'
                            from (select DISTINCT MaterName, WeightID  FROM dbo.PptWeighData WHERE PlanID='{0}') as a
                            set @sql = @sql +' ,sum(ErrorAllow) 总重 , '''' AS 车次号 , '''' AS 物料条码 from PptWeighData WHERE  PlanID=''{0}''  group by planID , Barcode , SerialID '
                            exec(@sql)");
            string sql = string.Format(sb.ToString(), planID);
            return this.GetBySql(sql).ToDataSet().Tables[0];
        }

        public PageResult<PptWeighData> GetOverErrorAllowPageDataBySql(QueryParams queryParams)
        {
            PageResult<PptWeighData> pageParams = queryParams.PageParams;

            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.PlanDate,t3.EquipName,
                                    t2.RecipeMaterialName,t1.MaterName,
                                    t1.SetWeight,t1.RealWeight,
                                    t1.ErrorAllow,t1.ErrorOut 
                                    FROM dbo.PptWeighData t1
                                    INNER JOIN dbo.PptPlan t2 ON t1.PlanID=t2.PlanID
                                    INNER JOIN dbo.BasEquip t3 ON t1.EquipCode=t3.EquipCode ");
            sqlstr.AppendLine(@" WHERE 1=1  AND ABS(t1.ErrorAllow) < ABS(t1.ErrorOut) ");//
            //if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
            //{
            //    sqlstr.AppendLine(@"AND t1.WeighTime>='" + queryParams.BeginTime + "'");
            //}
            //if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
            //{
            //    sqlstr.AppendLine(@"AND t1.WeighTime<='" + queryParams.EndTime + "'");
            //}
            sqlstr.AppendLine(@"AND t1.Barcode between '" + queryParams.BeginTime.Replace("-", "").Substring(2, 6) + "' and '" + queryParams.EndTime.Replace("-", "").Substring(2, 6) + "'");
            if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
            {
                sqlstr.AppendLine(@"AND t1.EquipCode='" + queryParams.EquipCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.MaterCode))
            {
                sqlstr.AppendLine(@"AND t1.MaterCode='" + queryParams.MaterCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.WeightType) && !"---请选择---".Equals(queryParams.WeightType))
            {
                sqlstr.AppendLine(@"AND t1.WeighType='" + queryParams.WeightType + "'");
            }
            sqlstr.AppendLine(@"ORDER BY t1.Barcode,t1.WeighTime");
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }

        public PageResult<PptWeighData> GetWeighRatePageDataBySql(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPptGetWeighRate");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StartDate"), this.TypeToDbType(queryParams.BeginTime.GetType()), queryParams.BeginTime);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EndDate"), this.TypeToDbType(queryParams.EndTime.GetType()), queryParams.EndTime);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.EquipCode.GetType()), queryParams.EquipCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("MaterCode"), this.TypeToDbType(queryParams.MaterCode.GetType()), queryParams.MaterCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("WeighType"), this.TypeToDbType(queryParams.WeightType.GetType()), "---请选择---".Equals(queryParams.WeightType) ? "" : queryParams.WeightType);
            queryParams.PageParams.DataSet = sps.ToDataSet();
            return queryParams.PageParams;
        }

        public DataTable GetWeighMaterialByBarcode(string barcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(@"    SELECT  SourceBarcode Barcode,TargetBarcode MaterBarcode,TargetMaterCode MaterCode ,B.MaterialName MaterName
                                    FROM    PptBarBomData A
                                    LEFT JOIN BasMaterial B ON A.TargetMaterCode = B.MaterialCode 
                                    WHERE   SourceBarcode = '" + barcode + "' ");
            strSql.AppendLine(@"    group by SourceBarcode ,TargetBarcode ,TargetMaterCode,B.MaterialName ");
            return this.GetBySql(strSql.ToString()).ToDataSet().Tables[0];
        }
        #endregion
    }
}
