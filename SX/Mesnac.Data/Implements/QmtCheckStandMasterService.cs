using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;

    public class QmtCheckStandMasterService : BaseService<QmtCheckStandMaster>, IQmtCheckStandMasterService
    {
		#region 构造方法

        public QmtCheckStandMasterService() : base(){ }

        public QmtCheckStandMasterService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckStandMasterService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetDataByParas(IQmtCheckStandMasterParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("SELECT TA.*");
            sb.AppendLine(" , CASE TA.StandVisionStat WHEN '0' THEN '已停用' WHEN '1' THEN '已启用' WHEN '2' THEN '已作废' WHEN '3' THEN '未提交' WHEN '4' THEN '已提交' WHEN '5' THEN '已退回' ELSE '' END AS StandVisionStatExp");
            sb.AppendLine(" , CASE WHEN LastModifyTime > ISNULL(LastSubmitTime, '2000-01-01') THEN CASE WHEN LastModifyTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastModifyTime ELSE LastAuditTime END ELSE CASE WHEN LastSubmitTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastSubmitTime ELSE LastAuditTime END END LastOperateTime");
            //sb.AppendLine(" , CASE TA.StandVisionStat WHEN '0' THEN 0 WHEN '1' THEN 1 WHEN '2' THEN 2 WHEN '-1' THEN -99' WHEN '-2' THEN '-2' ELSE '99' END AS StandVisionStatOrder");
            sb.AppendLine(" , TB.StandTypeName, TC.MaterialName AS MaterName");
            sb.AppendLine(" , TD.UserName LastModifyUserName");
            sb.AppendLine(" , TE.UserName LastSubmitUserName");
            sb.AppendLine(" , TF.UserName LastAuditUserName");
            sb.AppendLine(" FROM QmtCheckStandMaster TA");
            sb.AppendLine(" LEFT JOIN QmtCheckStandType TB ON TA.StandCode = TB.ObjID");
            sb.AppendLine(" LEFT JOIN BasMaterial TC ON TA.MaterCode = TC.MaterialCode");
            sb.AppendLine(" LEFT JOIN BasUser TD ON TA.WorkerBarcode = TD.WorkBarcode AND TD.DeleteFlag = '0'");
            sb.AppendLine(" LEFT JOIN BasUser TE ON TA.LastSubmitUser = TE.WorkBarcode AND TE.DeleteFlag = '0'");
            sb.AppendLine(" LEFT JOIN BasUser TF ON TA.LastAuditUser = TF.WorkBarcode AND TF.DeleteFlag = '0'");
            sb.AppendLine(" WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.StandId))
                sb.AppendLine(" AND TA.StandId = " + queryParams.StandId);
            if (!string.IsNullOrEmpty(queryParams.StandCode))
                sb.AppendLine(" AND TA.StandCode = '" + queryParams.StandCode + "'");
            if (!string.IsNullOrEmpty(queryParams.MaterCode))
                sb.AppendLine(" AND TA.MaterCode = '" + queryParams.MaterCode + "'");


//            if (!string.IsNullOrEmpty(queryParams.MaterCode))
//                sb.AppendLine(@" AND TA.MaterCode in (
//select materialcode from basmaterial
//where materialgroup in
//(select materialgroup from basmaterial where materialcode = '" + queryParams.MaterCode + "'))");


            if (!string.IsNullOrEmpty(queryParams.WorkerBarcode))
                sb.AppendLine(" AND TA.ItemName = '" + queryParams.WorkerBarcode + "'");
            if (!string.IsNullOrEmpty(queryParams.StandVision))
                sb.AppendLine(" AND TA.StandVision = '" + queryParams.StandVision + "'");
            if (!string.IsNullOrEmpty(queryParams.StandVisionStat))
                sb.AppendLine(" AND TA.StandVisionStat = '" + queryParams.StandVisionStat + "'");
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine(" AND TA.DeleteFlag = '" + queryParams.DeleteFlag + "'");
            if (!(queryParams.PmtType == "-1" || String.IsNullOrEmpty(queryParams.PmtType)))
                sb.AppendLine(" AND TA.PmtType = '" + queryParams.PmtType + "'");
            sb.AppendLine(") TA");
            sb.AppendLine("ORDER BY TA.MaterName, TA.LastOperateTime DESC, TA.StandVision DESC");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }

        /// <summary>
        /// 修改标识：qusf 20140930
        /// 修改说明：1.已删除的记录的已启用标志也会修改为未启用
        /// </summary>
        /// <param name="guid"></param>
        public void UpdateStandVisionStatByGUID(string guid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE dbo.QmtCheckStandMaster");
            sb.AppendLine("SET StandVisionStat = '0'");
            sb.AppendLine("FROM dbo.QmtCheckStandMaster t1");
            sb.AppendLine("WHERE t1.StandVisionStat = '1'");
            sb.AppendLine("AND ISNULL(t1.GUID, '') <> '" + guid + "'");
            sb.AppendLine("AND EXISTS (");
            sb.AppendLine("	SELECT *");
            sb.AppendLine("	FROM dbo.QmtCheckStandMaster t2");
            sb.AppendLine("	WHERE t1.StandCode = t2.StandCode");
            sb.AppendLine("	AND t1.MaterCode = t2.MaterCode");
            sb.AppendLine("	AND t2.GUID = '" + guid + "'");
            sb.AppendLine(")");

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            css.ExecuteNonQuery();
        }

        public DataSet GetCheckStandInfoByParas(IQmtCheckStandMasterQueryInfoParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("    SELECT A.MaterialName MaterName, A.MaterialGroup, C.MaterialName GroupName, B.*");
            sb.AppendLine("     , CASE B.StandVisionStat WHEN '0' THEN '已停用' WHEN '1' THEN '已启用' WHEN '2' THEN '已作废' WHEN '3' THEN '未提交' WHEN '4' THEN '已提交' WHEN '5' THEN '已退回' ELSE '' END AS StandVisionStatExp");
            sb.AppendLine("     , CASE WHEN LastModifyTime > ISNULL(LastSubmitTime, '2000-01-01') THEN CASE WHEN LastModifyTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastModifyTime ELSE LastAuditTime END ELSE CASE WHEN LastSubmitTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastSubmitTime ELSE LastAuditTime END END LastOperateTime");
            sb.AppendLine("     , G.StandTypeName");
            sb.AppendLine("     , D.UserName LastModifyUserName");
            sb.AppendLine("     , E.UserName LastSubmitUserName");
            sb.AppendLine("     , F.UserName LastAuditUserName");
            sb.AppendLine("    FROM (");
            sb.AppendLine("        SELECT MaterialCode, MaterialName, CASE WHEN MaterialCode = MaterialGroup THEN '' ELSE ISNULL(MaterialGroup, '') END MaterialGroupT");
            sb.AppendLine("        , CASE WHEN ISNULL(MaterialGroup, '') = '' THEN MaterialCode ELSE MaterialGroup END MaterialGroup");
            sb.AppendLine("        FROM BasMaterial");
            sb.AppendLine("        WHERE 1 = 1");
            sb.AppendFormat("        AND MaterialCode = '{0}'", queryParams.MaterCode);
            sb.AppendLine();
            sb.AppendLine("    ) A ");
            sb.AppendLine("    INNER JOIN QmtCheckStandMaster B ON A.MaterialGroup = B.MaterCode AND B.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN BasMaterial C ON A.MaterialGroupT = C.MaterialCode AND C.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN BasUser D ON B.WorkerBarcode = D.WorkBarcode AND D.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN BasUser E ON B.LastSubmitUser = E.WorkBarcode AND E.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN BasUser F ON B.LastAuditUser = F.WorkBarcode AND F.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN QmtCheckStandType G ON B.StandCode = G.ObjID");
            sb.AppendLine("    WHERE 1 = 1");

            if (queryParams.StandCode != null && queryParams.StandCode != "")
            {
                sb.AppendFormat("    AND B.StandCode = {0}", queryParams.StandCode);
                sb.AppendLine();
            }

            if (queryParams.StandVisionStat != null && queryParams.StandVisionStat != "")
            {
                sb.AppendFormat("    AND B.StandVisionStat = {0}", queryParams.StandVisionStat);
                sb.AppendLine();
            }
            sb.AppendLine(") A");

            sb.AppendLine("    ORDER BY A.MaterName, A.LastOperateTime DESC, A.StandVision DESC, A.StandVisionStat");


            return this.GetBySql(sb.ToString()).ToDataSet();
        }
          public DataSet GetCheckStandInfoByParas_new(IQmtCheckStandMasterQueryInfoParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM (");
            sb.AppendLine("    SELECT A.MaterialName MaterName, A.MaterialGroup,isnull(C.MaterialName+sc.ItemName,'')  GroupName, B.*");
            sb.AppendLine("     , CASE B.StandVisionStat WHEN '0' THEN '已停用' WHEN '1' THEN '已启用' WHEN '2' THEN '已作废' WHEN '3' THEN '未提交' WHEN '4' THEN '已提交' WHEN '5' THEN '已退回' ELSE '' END AS StandVisionStatExp");
            sb.AppendLine("     , CASE WHEN LastModifyTime > ISNULL(LastSubmitTime, '2000-01-01') THEN CASE WHEN LastModifyTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastModifyTime ELSE LastAuditTime END ELSE CASE WHEN LastSubmitTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastSubmitTime ELSE LastAuditTime END END LastOperateTime");
            sb.AppendLine("     , G.StandTypeName");
            sb.AppendLine("     , D.UserName LastModifyUserName");
            sb.AppendLine("     , E.UserName LastSubmitUserName");
            sb.AppendLine("     , F.UserName LastAuditUserName");
            sb.AppendLine("    FROM (");
            sb.AppendLine(@" select distinct recipeMaterialCode,RecipeMaterialName+Sc2.ItemName  MaterialName,A.RecipeType,ISNULL( B.MaterCode, left(dbo.FuncGetGroupName(GroupName),13)) MaterialGroup,
ISNULL( B.PmtType, SUBSTRING( dbo.FuncGetGroupName(GroupName),14,LEN(dbo.FuncGetGroupName(GroupName)) -13))  gb
,case isnull(B.MaterCode,'1') when  '1'  then left(dbo.FuncGetGroupName(GroupName),13)  else '' end a3,
case isnull(B.PmtType,'10') when '10' then SUBSTRING( dbo.FuncGetGroupName(GroupName),14,LEN(dbo.FuncGetGroupName(GroupName))) else '' end a4
 from pmtrecipe A
left join QmtCheckStandMaster B ON A.recipeMaterialCode = B.MaterCode and A.RecipeType=B.PmtType
left join QmtCheckDT C ON A.recipeMaterialCode =C.MaterialID and A.RecipeType=C.RecipeType
 left join SysCode SC2 on  A.RecipeType=SC2.ItemCode  and SC2.TypeID='PmtType'");
     
            sb.AppendLine("        WHERE 1 = 1");
            //sb.AppendFormat("        AND recipeMaterialCode = '{0}'", queryParams.MaterCode);
            sb.AppendLine(@" AND recipeMaterialCode in (
            select materialcode from basmaterial
            where materialgroup in
            (select materialgroup from basmaterial where materialcode = '" + queryParams.MaterCode + "'))");

            if (!(queryParams.PmtType == "-1" || String.IsNullOrEmpty(queryParams.PmtType)))
                sb.AppendLine(" AND A.RecipeType = '" + queryParams.PmtType + "'");
            sb.AppendLine();
            sb.AppendLine("    ) A ");
            sb.AppendLine("    INNER JOIN QmtCheckStandMaster B ON A.MaterialGroup = B.MaterCode and A.gb=B.PmtType AND B.DeleteFlag = '0'");
            sb.AppendLine("    LEFT JOIN BasMaterial C ON A.a3 = C.MaterialCode AND C.DeleteFlag = '0' ");
            sb.AppendLine("     LEFT JOIN BasUser D ON B.WorkerBarcode = D.WorkBarcode AND D.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN BasUser E ON B.LastSubmitUser = E.WorkBarcode AND E.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN BasUser F ON B.LastAuditUser = F.WorkBarcode AND F.DeleteFlag = '0'");
            sb.AppendLine("     LEFT JOIN QmtCheckStandType G ON B.StandCode = G.ObjID");
            sb.AppendLine("   left join SysCode SC on  A.a4=SC.ItemCode  and SC.TypeID='PmtType'");
            sb.AppendLine("    WHERE 1 = 1");

            if (queryParams.StandCode != null && queryParams.StandCode != "")
            {
                sb.AppendFormat("    AND B.StandCode = {0}", queryParams.StandCode);
                sb.AppendLine();
            }

            if (queryParams.StandVisionStat != null && queryParams.StandVisionStat != "")
            {
                sb.AppendFormat("    AND B.StandVisionStat = {0}", queryParams.StandVisionStat);
                sb.AppendLine();
            }
          
            sb.AppendLine(") A");

            sb.AppendLine("    ORDER BY A.MaterName, A.LastOperateTime DESC, A.StandVision DESC, A.StandVisionStat");


            return this.GetBySql(sb.ToString()).ToDataSet();
        }
    

    }

    public class QmtCheckStandMasterParams : IQmtCheckStandMasterParams
    {
        public string StandId
        {
            get;
            set;
        }
        public string StandCode
        {
            get;
            set;
        }
        public string MaterCode
        {
            get;
            set;
        }
        public string DefineDate
        {
            get;
            set;
        }
        public string WorkerBarcode
        {
            get;
            set;
        }
        public string StandVision
        {
            get;
            set;
        }
        public string StandVisionStat
        {
            get;
            set;
        }
        public string StandDate
        {
            get;
            set;
        }
        public string Choiceness
        {
            get;
            set;
        }
        public string RegDateTime
        {
            get;
            set;
        }
        public string DeleteFlag
        {
            get;
            set;
        }
        public string GUID
        {
            get;
            set;
        }
        public PageResult<QmtCheckStandMaster> PageResult
        {
            get;
            set;
        }

        public string PmtType
        {
            get;
            set;
        }
    }

    public class QmtCheckStandMasterQueryInfoParams : IQmtCheckStandMasterQueryInfoParams
    {
        public string MaterCode { get; set; }
        public string StandVisionStat { get; set; }
        public string StandCode { get; set; }
        public string PmtType { get; set; }
    }

}
