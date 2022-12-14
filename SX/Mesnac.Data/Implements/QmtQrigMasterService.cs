using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    public class QmtQrigMasterService : BaseService<QmtQrigMaster>, IQmtQrigMasterService
    {
        #region 构造方法

        public QmtQrigMasterService() : base() { }

        public QmtQrigMasterService(string connectStringKey) : base(connectStringKey) { }

        public QmtQrigMasterService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        /// <summary>
        /// 根据条件查询质检数据
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<QmtQrigMaster> GetDataByQueryParams(IQmtQrigMasterQueryParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region

            sb.AppendLine("SELECT TA.*");
            sb.AppendLine(",TA.CheckDate + ' ' + TA.CheckTime FullCheckTime");
            sb.AppendLine(",TB.EquipName,TC.ShiftName,TD.ClassName,TE.MaterialName MaterName");
            sb.AppendLine(",TF.EquipName CheckEquipName");
            //sb.AppendLine(",TG.UserName CheckUserName");
            sb.AppendLine(",TH.ClassName CheckClassName");
            sb.AppendLine(",TI.StandTypeName");
            sb.AppendLine(",TJ.ShiftName CheckShiftName");
            sb.AppendLine("FROM dbo.QmtQrigMaster TA");
            sb.AppendLine("LEFT JOIN dbo.BasEquip TB ON TA.EquipCode = TB.EquipCode");
            sb.AppendLine("LEFT JOIN dbo.PptShift TC ON TA.ShiftId = TC.ObjID");
            sb.AppendLine("LEFT JOIN dbo.PptClass TD ON TA.ShiftClass = TD.ObjID");
            sb.AppendLine("LEFT JOIN dbo.BasMaterial TE ON TA.MaterCode = TE.MaterialCode");
            sb.AppendLine("LEFT JOIN dbo.BasEquip TF ON TA.CheckEquipCode = TF.EquipCode");
            //sb.AppendLine("LEFT JOIN dbo.BasUser TG ON TA.WorkerBarcode = TG.WorkBarcode");
            sb.AppendLine("LEFT JOIN dbo.PptClass TH ON TA.CheckPlan_Class = TH.ObjID");
            sb.AppendLine("LEFT JOIN dbo.QmtCheckStandType TI ON TA.StandCode = TI.ObjID");
            sb.AppendLine("LEFT JOIN dbo.PptShift TJ ON TA.ShiftCheckId = TJ.ObjID");

            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.SPlanDate))
                sb.AppendLine("AND TA.PlanDate>='" + queryParams.SPlanDate + "'");
            if (!string.IsNullOrEmpty(queryParams.EPlanDate))
                sb.AppendLine("AND TA.PlanDate<='" + queryParams.EPlanDate + "'");
            if (!string.IsNullOrEmpty(queryParams.SCheckDate))
                sb.AppendLine("AND TA.CheckPlan_Date>='" + queryParams.SCheckDate + "'");
            if (!string.IsNullOrEmpty(queryParams.ECheckDate))
                sb.AppendLine("AND TA.CheckPlan_Date<'" + queryParams.ECheckDate + "'");
            if (!string.IsNullOrEmpty(queryParams.EquipCode))
                sb.AppendLine("AND TA.EquipCode='" + queryParams.EquipCode + "'");
            if (!string.IsNullOrEmpty(queryParams.ShiftId))
                sb.AppendLine("AND TA.ShiftId = '" + queryParams.ShiftId + "'");
            if (!string.IsNullOrEmpty(queryParams.ShiftClass))
                sb.AppendLine("AND TA.ShiftClass='" + queryParams.ShiftClass + "'");
            if (!string.IsNullOrEmpty(queryParams.CheckPlanClass))
                sb.AppendLine("AND TA.CheckPlan_Class='" + queryParams.CheckPlanClass + "'");
            if (!string.IsNullOrEmpty(queryParams.WorkerBarcode))
                sb.AppendLine("AND TA.WorkerBarcode='" + queryParams.WorkerBarcode + "'");
            if (!string.IsNullOrEmpty(queryParams.CheckEquipCode))
                sb.AppendLine("AND TA.CheckEquipCode='" + queryParams.CheckEquipCode + "'");
            if (!string.IsNullOrEmpty(queryParams.MaterCode))
                sb.AppendLine("AND TA.MaterCode='" + queryParams.MaterCode + "'");
            if (!string.IsNullOrEmpty(queryParams.TestType))
                sb.AppendLine("AND TA.TestType LIKE '%" + queryParams.TestType + "%'");
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

            if (!string.IsNullOrEmpty(queryParams.ZJSID))
            {
                sb.AppendLine("AND TA.ZJSID='" + queryParams.ZJSID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.CheckItemTypeId))
            {
                sb.AppendLine("AND EXISTS (");
                sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.TypeID = " + queryParams.CheckItemTypeId);
                sb.AppendLine(")");
            }
            if (!string.IsNullOrEmpty(queryParams.StandCode))
            {
                if (queryParams.StandCode == "1" || queryParams.StandCode == "2")
                {
                    sb.AppendLine("AND TA.StandCode IN (1, 2)");
                }
                else
                {
                    sb.AppendLine("AND TA.StandCode = " + queryParams.StandCode);
                }
            }

            PageResult<QmtQrigMaster> pageParams = queryParams.PageParams;

            if (pageParams != null && pageParams.PageSize > 0)
            {
                pageParams.QueryStr = sb.ToString();
                if (pageParams.Orderfld == "" || pageParams.Orderfld.ToLower() == "seqno")
                {
                    pageParams.Orderfld = "PlanDate,ShiftId,EquipCode,ZJSID,MaterCode,SerialId,LLSerialID,CheckDate,CheckTime";
                }
                return this.GetPageDataBySql(pageParams);
            }
            else
            {
                sb.AppendLine("ORDER BY TA.PlanDate,TA.ShiftId,TA.EquipCode,TA.ZJSID,TA.MaterCode,TA.SerialId,TA.LLSerialID,TA.CheckDate,TA.CheckTime");

                NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
                if (pageParams == null)
                {
                    pageParams = new PageResult<QmtQrigMaster>();
                }
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }

            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<QmtQrigMaster> GetDetailDataByQueryParams(IQmtQrigMasterQueryParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region

            sb.AppendLine("Select TA.PlanDate [生产日期], TB.EquipName [生产机台], TC.ShiftName [生产班次], TD.ClassName [生产班组]");
            sb.AppendLine(", TA.ZJSID [主机手], TE.MaterialName [胶料名称], TA.SerialId [车次], TA.LLSerialID [车次]");
            sb.AppendLine(", TA.TestType [检验类型], TA.CheckNum [检验次数], TA.CheckPlan_Date [检验日期], TA.CheckDate + ' ' + TA.CheckTime [检验时间]");
            sb.AppendLine(", TH.ClassName [检验班组], TJ.ShiftName [检验班次], TI.StandTypeName [检验标准分类], TF.EquipName [检验机台]");
            sb.AppendLine(", TL.ItemName [检验项], TK.ItemCheck [检验值], TM.PermMin [最小值], TM.PermMax [最大值]");
            sb.AppendLine("FROM dbo.QmtQrigMaster TA");
            sb.AppendLine("LEFT JOIN dbo.BasEquip TB ON TA.EquipCode = TB.EquipCode");
            sb.AppendLine("LEFT JOIN dbo.PptShift TC ON TA.ShiftId = TC.ObjID");
            sb.AppendLine("LEFT JOIN dbo.PptClass TD ON TA.ShiftClass = TD.ObjID");
            sb.AppendLine("LEFT JOIN dbo.BasMaterial TE ON TA.MaterCode = TE.MaterialCode");
            sb.AppendLine("LEFT JOIN dbo.BasEquip TF ON TA.CheckEquipCode = TF.EquipCode");
            //sb.AppendLine("LEFT JOIN dbo.BasUser TG ON TA.WorkerBarcode = TG.WorkBarcode");
            sb.AppendLine("LEFT JOIN dbo.PptClass TH ON TA.CheckPlan_Class = TH.ObjID");
            sb.AppendLine("LEFT JOIN dbo.QmtCheckStandType TI ON TA.StandCode = TI.ObjID");
            sb.AppendLine("LEFT JOIN dbo.PptShift TJ ON TA.ShiftCheckId = TJ.ObjID");
            sb.AppendLine("Join dbo.QmtQrigDetail TK On TA.SeqNo = TK.SeqNo");
            sb.AppendLine("Left Join dbo.QmtCheckItem TL On TK.ItemCd = TL.ItemCode");
            sb.AppendLine("Left Join dbo.QmtCheckStandDetail TM On TK.StandId = TM.StandId And TK.ItemCd = TM.ItemCd");

            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.SPlanDate))
                sb.AppendLine("AND TA.PlanDate>='" + queryParams.SPlanDate + "'");
            if (!string.IsNullOrEmpty(queryParams.EPlanDate))
                sb.AppendLine("AND TA.PlanDate<='" + queryParams.EPlanDate + "'");
            if (!string.IsNullOrEmpty(queryParams.SCheckDate))
                sb.AppendLine("AND TA.CheckPlan_Date>='" + queryParams.SCheckDate + "'");
            if (!string.IsNullOrEmpty(queryParams.ECheckDate))
                sb.AppendLine("AND TA.CheckPlan_Date<'" + queryParams.ECheckDate + "'");
            if (!string.IsNullOrEmpty(queryParams.EquipCode))
                sb.AppendLine("AND TA.EquipCode='" + queryParams.EquipCode + "'");
            if (!string.IsNullOrEmpty(queryParams.ShiftId))
                sb.AppendLine("AND TA.ShiftId = '" + queryParams.ShiftId + "'");
            if (!string.IsNullOrEmpty(queryParams.ShiftClass))
                sb.AppendLine("AND TA.ShiftClass='" + queryParams.ShiftClass + "'");
            if (!string.IsNullOrEmpty(queryParams.CheckPlanClass))
                sb.AppendLine("AND TA.CheckPlan_Class='" + queryParams.CheckPlanClass + "'");
            if (!string.IsNullOrEmpty(queryParams.WorkerBarcode))
                sb.AppendLine("AND TA.WorkerBarcode='" + queryParams.WorkerBarcode + "'");
            if (!string.IsNullOrEmpty(queryParams.CheckEquipCode))
                sb.AppendLine("AND TA.CheckEquipCode='" + queryParams.CheckEquipCode + "'");
            if (!string.IsNullOrEmpty(queryParams.MaterCode))
                sb.AppendLine("AND TA.MaterCode='" + queryParams.MaterCode + "'");
            if (!string.IsNullOrEmpty(queryParams.TestType))
                sb.AppendLine("AND TA.TestType LIKE '%" + queryParams.TestType + "%'");
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

            if (!string.IsNullOrEmpty(queryParams.ZJSID))
            {
                sb.AppendLine("AND TA.ZJSID='" + queryParams.ZJSID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.CheckItemTypeId))
            {
                sb.AppendLine("AND EXISTS (");
                sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.TypeID = " + queryParams.CheckItemTypeId);
                sb.AppendLine(")");
            }
            if (!string.IsNullOrEmpty(queryParams.StandCode))
            {
                if (queryParams.StandCode == "1" || queryParams.StandCode == "2")
                {
                    sb.AppendLine("AND TA.StandCode IN (1, 2)");
                }
                else
                {
                    sb.AppendLine("AND TA.StandCode = " + queryParams.StandCode);
                }
            }

            PageResult<QmtQrigMaster> pageParams = queryParams.PageParams;

            if (pageParams != null && pageParams.PageSize > 0)
            {
                pageParams.QueryStr = sb.ToString();
                if (pageParams.Orderfld == "" || pageParams.Orderfld.ToLower() == "seqno")
                {
                    pageParams.Orderfld = "PlanDate,ShiftId,EquipCode,ZJSID,MaterCode,SerialId,LLSerialID,CheckDate,CheckTime";
                }
                return this.GetPageDataBySql(pageParams);
            }
            else
            {
                sb.AppendLine("ORDER BY TA.PlanDate,TA.ShiftId,TA.EquipCode,TA.ZJSID,TA.MaterCode,TA.SerialId,TA.LLSerialID,TA.CheckDate,TA.CheckTime");

                NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
                if (pageParams == null)
                {
                    pageParams = new PageResult<QmtQrigMaster>();
                }
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }

            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int UpdateSerialIdByLLSerialId(string guid)
        {
            StringBuilder sb = new StringBuilder();
            #region

            sb.AppendLine("UPDATE QmtQrigMaster");
            sb.AppendLine("SET SerialId = B.Serial_BatchId");
            sb.AppendLine("FROM QmtQrigMaster A");
            sb.AppendLine("INNER JOIN PptLot B ON A.PlanDate = B.Plan_Date AND A.EquipCode = B.Equip_Code");
            sb.AppendLine("AND A.ShiftId = B.Shift_Id AND A.MaterCode = B.Mater_Code AND A.ZJSID = B.ZJSID ");
            sb.AppendLine("AND A.LLSerialID = B.LLSerialID");
            sb.AppendFormat("WHERE A.GUID = '{0}'", guid);

            #endregion

            return this.defaultGateway.FromCustomSql(sb.ToString()).ExecuteNonQuery();
        }

        public DataSet StaticsQrigProductionAmount(IQmtQrigMasterStaticProdAmountParams paras)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("@CheckSDate", paras.CheckSDate);
            values.Add("@CheckEDate", paras.CheckEDate);
            values.Add("@CheckPlanClass", paras.CheckPlanClass);
            values.Add("@WorkShopId", paras.WorkShopId);
            DataSet ds = this.GetDataSetByStoreProcedure("ProcQmtStatQrigProd", values);

            return ds;
        }
    }

    /// <summary>
    /// 修改标识：qusf 20131111
    /// 修改说明：1.增加属性CheckItemTypeId
    ///           2.将ShiftCheckId改为CheckPlanClass
    /// </summary>
    public class QmtQrigMasterQueryParams : IQmtQrigMasterQueryParams
    {
        public string SPlanDate { get; set; }
        public string EPlanDate { get; set; }
        public string SCheckDate { get; set; }
        public string ECheckDate { get; set; }
        public string EquipCode { get; set; }
        public string ShiftId { get; set; }
        public string ShiftClass { get; set; }
        public string CheckPlanClass { get; set; }
        public string WorkerBarcode { get; set; }
        public string CheckEquipCode { get; set; }
        public string MaterCode { get; set; }
        public string StandCode { get; set; }
        public string TestType { get; set; }
        public string DeleteFlag { get; set; }

        public string ZJSID { get; set; }
        public string CheckItemTypeId { get; set; }

        public PageResult<QmtQrigMaster> PageParams { get; set; }

    }

    public class QmtQrigMasterStaticProdAmountParams : IQmtQrigMasterStaticProdAmountParams
    {
        public string CheckSDate { get; set; }
        public string CheckEDate { get; set; }
        public string CheckPlanClass { get; set; }
        public string WorkShopId { get; set; }
    }

}
