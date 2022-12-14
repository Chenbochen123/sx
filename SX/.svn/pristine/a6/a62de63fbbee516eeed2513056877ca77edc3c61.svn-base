using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
using System.Data;
    public class PpmRubConsumeService : BaseService<PpmRubConsume>, IPpmRubConsumeService
    {
        #region 构造方法

        public PpmRubConsumeService() : base() { }

        public PpmRubConsumeService(string connectStringKey) : base(connectStringKey) { }

        public PpmRubConsumeService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法



        public class QueryParams
        {
            public string equipCode { get; set; }
            public string shiftID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string materType { get; set; }
            public string materCode { get; set; }
            public string chejian { get; set; }
            public PageResult<PpmRubConsume> pageParams { get; set; }
        }


        public DataTable GetTotalPageDataBySql(string begindate,string enddate,string chejian,string equipcode,string matertype,string matercode)
        { 
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT RIGHT(CONVERT(NVARCHAR(10),Plandate,112),6) AS PlanDate,c.EquipName,CASE  Left(A.Costcode,1) WHEN '4' THEN '母炼胶' WHEN '5' THEN '终炼胶' WHEN '6' THEN '返回胶' ELSE '其他' END AS MinerTypeName,e.MaterialName AS CostMaterName,b.MaterialName
,CONVERT(decimal(18,1),SUM(consumeqty)) AS consumeqty,CONVERT(decimal(18,1),SUM(Consqty)) AS Consqty
 from PpmRubConsume A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join BasMaterial E on A.CostCode = E.MaterialCode
                                where A.DeleteFlag = '0' ");
             if (begindate!="")
                sqlstr.AppendLine(" AND A.PlanDate >= '" + begindate + "'");
            if (enddate!="")
                sqlstr.AppendLine(" AND A.PlanDate <= '" + enddate + "'");
            if (!string.IsNullOrEmpty(equipcode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + equipcode + "'");
            }
            if (!string.IsNullOrEmpty(matertype))
            {

                sqlstr.AppendLine(" AND case when Left(A.Costcode,1)  in ('4','5','6') then Left(A.Costcode,1) else '05' end = '" + matertype + "'");
            }
            if (!string.IsNullOrEmpty(matercode))
            {
                sqlstr.AppendLine(" AND A.Costcode = '" + matercode + "'");
            }
            if (!string.IsNullOrEmpty(chejian))
            {
                sqlstr.AppendLine(" AND c.WorkShopCode = '" + chejian + "'");
            }
            sqlstr.AppendLine(" GROUP BY RIGHT(CONVERT(NVARCHAR(10),Plandate,112),6),CASE  Left(A.Costcode,1) WHEN '4' THEN '母炼胶' WHEN '5' THEN '终炼胶' WHEN '6' THEN '返回胶' ELSE '其他' END,c.EquipName,e.MaterialName,b.MaterialName ");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            
            return css.ToDataSet().Tables[0];

        }

        public PageResult<PpmRubConsume> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubConsume> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select Id, PlanDate, CostCode, E.MaterialName CostMaterName, MaterCode, B.MaterialName, A.EquipCode, C.EquipName, ShiftID, ShiftClassID, ConsumeQty, BalanceQty,
	                                ConsQty,SurPlus,HandFlag,ConsRate=(SurPlus/(case ConsumeQty when 0 then 999999 else ConsumeQty end))*100,
	                                Mater_Kind=substring(MaterCode,2,2), CASE  Left(A.CostCode,1) WHEN '4' THEN '母炼胶' WHEN '5' THEN '终炼胶' WHEN '6' THEN '返回胶' ELSE '其他' END as MinorTypeName, RecordDate
                                from PpmRubConsume A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join BasMaterial E on A.CostCode = E.MaterialCode
                                where A.DeleteFlag = '0'  ");
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materType))
            {
                if (queryParams.materType == "05")
                {
                    sqlstr.AppendLine(" AND  Left(A.CostCode,1) not in ('4','5','6')");
                }
                else
                {
                    sqlstr.AppendLine(" AND Left(A.CostCode,1) = '" + queryParams.materType + "'");
                }
               
            }
            if (!string.IsNullOrEmpty(queryParams.chejian))
            {
                sqlstr.AppendLine(" AND c.WorkShopCode = '" + queryParams.chejian + "'");
            }

            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }
    }
}
