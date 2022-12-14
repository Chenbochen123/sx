using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcCheckDataService : BaseService<QmcCheckData>, IQmcCheckDataService
    {
        #region 构造方法

        public QmcCheckDataService() : base() { }

        public QmcCheckDataService(string connectStringKey) : base(connectStringKey) { }

        public QmcCheckDataService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        /// <summary>
        /// 根据参数获取检测数据信息
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetDataSetByParams(IQmcCheckDataQueryParams paras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.*");
            sb.AppendLine(", CASE A.CheckResult WHEN '1' THEN '合格' WHEN '0' THEN '不合格' ELSE '' END CheckResultDes");
            sb.AppendLine(", CASE ISNULL(A.RecordStat, 0) WHEN '0' THEN '未提交' WHEN '1' THEN '已提交' ELSE '' END RecordStatDes");
            sb.AppendLine(", CASE ISNULL(A.ApproveFlag, 0) WHEN '0' THEN '未审核' WHEN '1' THEN '已审核' ELSE '' END ApproveStatDes");
            sb.AppendLine(", B.MaterialName MaterName");
            sb.AppendLine(", C.FacName SupplyFacName");
            sb.AppendLine(", D.FacName ProductFacName");
            sb.AppendLine(", E.UserName RecorderName");
            sb.AppendLine(", F.UserName LastModifierName");
            sb.AppendLine(", G.SpecName");
            sb.AppendLine(", H.StandardName");
            sb.AppendLine(", CASE A.FXFlag WHEN '1' THEN '放行'  ELSE '' END FXFlag2");

            sb.AppendLine("FROM QmcCheckData A");
            sb.AppendLine("LEFT JOIN BasMaterial B ON A.MaterCode = B.MaterialCode");
            sb.AppendLine("LEFT JOIN BasFactoryInfo C ON A.SupplyFac = C.ObjID");
            sb.AppendLine("LEFT JOIN BasFactoryInfo D ON A.ProductFac = D.ObjID");
            sb.AppendLine("LEFT JOIN BasUser E ON A.RecorderId = E.WorkBarcode");
            sb.AppendLine("LEFT JOIN BasUser F ON A.LastModifierId = F.WorkBarcode");
            sb.AppendLine("Left Join QmcSpec G On A.SpecId = G.SpecId");
            sb.AppendLine("Left Join QmcStandard H On A.StandardId = H.StandardId");
            sb.AppendLine("WHERE A.DeleteFlag = '0'");
            if (paras.BillNo != null && paras.BillNo != "")
            {
                sb.AppendFormat("AND A.BillNo LIKE '%{0}%'", paras.BillNo);
                sb.AppendLine();
            }
            if (paras.Barcode != null && paras.Barcode != "")
            {
                sb.AppendFormat("AND A.Barcode LIKE '%{0}%'", paras.Barcode);
                sb.AppendLine();
            }
            if (paras.MaterCode != null && paras.MaterCode != "")
            {
                sb.AppendFormat("AND A.MaterCode = '{0}'", paras.MaterCode);
                sb.AppendLine();
            }
            else if (paras.MinorTypeID != null && paras.MinorTypeID != "")
            {
                sb.AppendFormat("AND B.MajorTypeID = 1 AND B.MinorTypeID = '{0}'", paras.MinorTypeID);
                sb.AppendLine();
            }
            if (paras.BeginCheckDate != null && paras.BeginCheckDate != "")
            {
                sb.AppendFormat("AND A.CheckDate >= '{0}'", paras.BeginCheckDate);
                sb.AppendLine();
            }
            if (paras.EndCheckDate != null && paras.EndCheckDate != "")
            {
                sb.AppendFormat("AND A.CheckDate <= '{0}'", paras.EndCheckDate);
                sb.AppendLine();
            }
            if (paras.SupplyFacId != null && paras.SupplyFacId != "")
            {
                sb.AppendFormat("AND A.SupplyFac = {0}", paras.SupplyFacId);
                sb.AppendLine();
            }
            if (paras.ProductFacId != null && paras.ProductFacId != "")
            {
                sb.AppendFormat("AND A.ProductFac = {0}", paras.ProductFacId);
                sb.AppendLine();
            }
            if (paras.CheckResult != null && paras.CheckResult != "")
            {
                sb.AppendFormat("AND A.CheckResult = '{0}'", paras.CheckResult);
                sb.AppendLine();
            }
            if (paras.RecordStat != null && paras.RecordStat != "")
            {
                sb.AppendFormat("AND ISNULL(A.RecordStat, 0) = {0}", paras.RecordStat);
                sb.AppendLine();
            }
            if (paras.RecorderId != null && paras.RecorderId != "")
            {
                sb.AppendFormat("AND ISNULL(A.RecorderId, '') = {0}", paras.RecorderId);
                sb.AppendLine();
            }

            sb.AppendLine("ORDER BY A.RecordTime DESC");
            return this.GetBySql(sb.ToString()).ToDataSet();
        }

        /// <summary>
        /// 根据参数获取质检报告信息
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.*");
            sb.AppendLine(", CASE A.CheckResult WHEN '1' THEN '合格' WHEN '0' THEN '不合格' ELSE '' END CheckResultDes");
            sb.AppendLine(", CASE ISNULL(A.RecordStat, 0) WHEN '0' THEN '未提交' WHEN '1' THEN '已提交' ELSE '' END RecordStatDes");
            sb.AppendLine(", B.MaterialName MaterName");
            sb.AppendLine(", C.FacName SupplyFacName");
            sb.AppendLine(", D.FacName ProductFacName");
            sb.AppendLine(", E.UserName RecorderName");
            sb.AppendLine(", F.UserName LastModifierName");
            sb.AppendLine(", G.SpecName");
            sb.AppendLine(", H.StandardName");
            sb.AppendLine(", I.ExtractorId, I.ReceiverId, I.FetcherId, I.HandlerId, I.ReceiveDate, I.SendDate, I.ReturnDate, I.HandleDate");
            sb.AppendLine(", J.UserName ExtractorName");
            sb.AppendLine(", K.UserName ReceiverName");
            sb.AppendLine(", L.UserName FetcherName");
            sb.AppendLine(", M.UserName HandlerName");
            sb.AppendLine("FROM QmcCheckData A");
            sb.AppendLine("LEFT JOIN BasMaterial B ON A.MaterCode = B.MaterialCode");
            sb.AppendLine("LEFT JOIN BasFactoryInfo C ON A.SupplyFac = C.ObjID");
            sb.AppendLine("LEFT JOIN BasFactoryInfo D ON A.ProductFac = D.ObjID");
            sb.AppendLine("LEFT JOIN BasUser E ON A.RecorderId = E.WorkBarcode");
            sb.AppendLine("LEFT JOIN BasUser F ON A.LastModifierId = F.WorkBarcode");
            sb.AppendLine("LEFT JOIN QmcSpec G On A.SpecId = G.SpecId");
            sb.AppendLine("LEFT JOIN QmcStandard H On A.StandardId = H.StandardId");
            sb.AppendLine("LEFT JOIN QmcSampleLedger I On A.LedgerId = I.LedgerId");
            sb.AppendLine("LEFT JOIN BasUser J ON I.ExtractorId = J.HRCode");
            sb.AppendLine("LEFT JOIN BasUser K ON I.ReceiverId = K.HRCode");
            sb.AppendLine("LEFT JOIN BasUser L ON I.FetcherId = L.HRCode");
            sb.AppendLine("LEFT JOIN BasUser M ON I.HandlerId = M.HRCode");
            sb.AppendLine("WHERE A.DeleteFlag = '0' AND A.ApproveFlag = '1'");
            if (paras.BillNo != null && paras.BillNo != "")
            {
                sb.AppendFormat("AND A.BillNo LIKE '%{0}%'", paras.BillNo);
                sb.AppendLine();
            }
            if (paras.Barcode != null && paras.Barcode != "")
            {
                sb.AppendFormat("AND A.Barcode LIKE '%{0}%'", paras.Barcode);
                sb.AppendLine();
            }
            if (paras.MaterCode != null && paras.MaterCode != "")
            {
                sb.AppendFormat("AND A.MaterCode = '{0}'", paras.MaterCode);
                sb.AppendLine();
            }
            else if (paras.MinorTypeID != null && paras.MinorTypeID != "")
            {
                sb.AppendFormat("AND B.MajorTypeID = 1 AND B.MinorTypeID = '{0}'", paras.MinorTypeID);
                sb.AppendLine();
            }
            if (paras.BeginCheckDate != null && paras.BeginCheckDate != "")
            {
                sb.AppendFormat("AND A.CheckDate >= '{0}'", paras.BeginCheckDate);
                sb.AppendLine();
            }
            if (paras.EndCheckDate != null && paras.EndCheckDate != "")
            {
                sb.AppendFormat("AND A.CheckDate <= '{0}'", paras.EndCheckDate);
                sb.AppendLine();
            }
            if (paras.SupplyFacId != null && paras.SupplyFacId != "")
            {
                sb.AppendFormat("AND A.SupplyFac = {0}", paras.SupplyFacId);
                sb.AppendLine();
            }
            if (paras.ProductFacId != null && paras.ProductFacId != "")
            {
                sb.AppendFormat("AND A.ProductFac = {0}", paras.ProductFacId);
                sb.AppendLine();
            }
            if (paras.CheckResult != null && paras.CheckResult != "")
            {
                sb.AppendFormat("AND A.CheckResult = '{0}'", paras.CheckResult);
                sb.AppendLine();
            }
            if (paras.RecordStat != null && paras.RecordStat != "")
            {
                sb.AppendFormat("AND ISNULL(A.RecordStat, 0) = {0}", paras.RecordStat);
                sb.AppendLine();
            }
            if (paras.RecorderId != null && paras.RecorderId != "")
            {
                sb.AppendFormat("AND ISNULL(A.RecorderId, '') = {0}", paras.RecorderId);
                sb.AppendLine();
            }
            //针对同一送检原材料，只可以根据最新的一条检测记录生成报告
            sb.AppendLine("AND A.CheckId in (select max(CheckId) from QmcCheckData where DeleteFlag = '0' AND ApproveFlag = '1' group by BillNo,OrderId)");
            sb.AppendLine("ORDER BY A.RecordTime DESC");
            return this.GetBySql(sb.ToString()).ToDataSet();
        }

        public DataSet GetQmcSampleLedgerInfoQueryByParams(IQmcSampleLedgeQueryParams paras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select A.*, B.FacName SupplierName, C.FacName ManufacturerName");
            sb.AppendLine(", D.UserName ExtractorName, E.UserName ReceiverName, F.UserName FetcherName");
            sb.AppendLine(", G.UserName HandlerName");
            sb.AppendLine("From QmcSampleLedger A");
            sb.AppendLine("Left Join BasFactoryInfo B On A.SupplierId = B.ObjID");
            sb.AppendLine("Left Join BasFactoryInfo C On A.ManufacturerId = C.ObjID");
            sb.AppendLine("Left Join BasUser D On A.ExtractorId = D.HRCode");
            sb.AppendLine("Left Join BasUser E On A.ReceiverId = E.HRCode");
            sb.AppendLine("Left Join BasUser F On A.FetcherId = F.HRCode");
            sb.AppendLine("Left Join BasUser G On A.HandlerId = G.HRCode");
            sb.AppendLine("WHERE A.DeleteFlag = '0'");
            if (paras.BeginRecordDate != null && paras.BeginRecordDate != "")
            {
                sb.AppendFormat("AND A.RecordDate >= '{0}'", paras.BeginRecordDate);
                sb.AppendLine();
            }
            if (paras.EndRecordDate != null && paras.EndRecordDate != "")
            {
                sb.AppendFormat("AND A.RecordDate < '{0}'", paras.EndRecordDate);
                sb.AppendLine();
            }
            if (paras.SampleName != null && paras.SampleName != "")
            {
                sb.AppendFormat("AND A.SampleName Like '%{0}%'", paras.SampleName);
                sb.AppendLine();
            }
            if (paras.SampleCode != null && paras.SampleCode != "")
            {
                sb.AppendFormat("AND A.SampleCode = '{0}'", paras.SampleCode);
                sb.AppendLine();
            }
            if (paras.SupplierId != null && paras.SupplierId != "")
            {
                sb.AppendFormat("AND A.SupplierId = '{0}'", paras.SupplierId);
                sb.AppendLine();
            }
            if (paras.ManufacturerId != null && paras.ManufacturerId != "")
            {
                sb.AppendFormat("AND A.ManufacturerId = '{0}'", paras.ManufacturerId);
                sb.AppendLine();
            }
            if (paras.FactoryCode != null && paras.FactoryCode != "")
            {
                sb.AppendFormat("AND A.FactoryCode = '{0}'", paras.FactoryCode);
                sb.AppendLine();
            }
            if (paras.Barcode != null && paras.Barcode != "")
            {
                sb.AppendFormat("AND A.Barcode LIKE '%{0}%'", paras.Barcode);
                sb.AppendLine();
            }
            sb.AppendFormat(@"AND A.LedgerId in (select min (LedgerId) from QmcSampleLedger t1
left join QmcFactoryNonCheck t2
on t1.MaterialCode=t2.MaterialCode and t1.FactoryCode = t2.FactoryCode
left join QmcFactoryNonCheck t3
on t1.MaterialCode=t3.MaterialCode and  t3.FactoryCode = ''
where LedgerId not in (select LedgerId from QmcCheckData)
and RecordDate > '2015-7-2'
group by  isnull(ISNULL(t2.ObjID,t3.ObjID),left(t1.MaterialCode,4)+right(t1.MaterialCode,5)) )", paras.Barcode);
            sb.AppendLine();
            sb.AppendLine("ORDER BY A.RecordDate DESC");
            return this.GetBySql(sb.ToString()).ToDataSet();
        }

        /// <summary>
        /// 获取检测数据中所有录入人信息
        /// </summary>
        /// <remarks>
        /// 返回列表字段: WorkBarcode(人员编号) UserName(人员名称) 
        /// </remarks>
        /// <returns></returns>
        public DataSet GetAllRecorderInfo()
        {
            DataSet ds =
             this.defaultGateway.From<QmcCheckData>().Join<BasUser>(QmcCheckData._.RecorderId == BasUser._.WorkBarcode)
              .Select(new NBear.Common.ExpressionClip[] { BasUser._.WorkBarcode.Distinct, BasUser._.UserName })
              .Where(QmcCheckData._.DeleteFlag == "0" & BasUser._.DeleteFlag == "0")
              .OrderBy(BasUser._.UserName.Asc)
              .ToDataSet();

            return ds;
        }

        /// <summary>
        /// 获取某个原材料的规格信息
        /// </summary>
        /// <param name="materCode"></param>
        /// <returns></returns>
        public DataSet GetSpecInfoByMaterCode(string materCode)
        {
            DataSet ds =
                this.defaultGateway.From<QmcSpecMapping>().Join<QmcSpec>(QmcSpec._.SpecId == QmcSpecMapping._.SpecId)
                .Select(new NBear.Common.ExpressionClip[] { QmcSpec._.SpecId.Distinct, QmcSpec._.SpecName })
                .Where(QmcSpecMapping._.DeleteFlag == "0" & QmcSpec._.DeleteFlag == "0" & QmcSpecMapping._.MaterialCode == materCode)
                .OrderBy(QmcSpec._.SpecName.Asc)
                .ToDataSet();

            return ds;
        }

        /// <summary>
        /// 根据执行标准、原材料、检验频次，获取有效的最新的检测指标信息
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public NBear.Common.EntityArrayList<QmcCheckItemDetail> GetCheckItemDetailByParams(IQmcCheckDataQueryItemDetailParams paras)
        {
            return this.defaultGateway.From<QmcCheckItemDetail>().Join<QmcCheckItem>(QmcCheckItemDetail._.ItemId == QmcCheckItem._.ItemId)
                .Where(QmcCheckItem._.StandardId == paras.StandardId & QmcCheckItemDetail._.MaterialCode == paras.MaterCode
                    & QmcCheckItem._.DeleteFlag == "0" & QmcCheckItemDetail._.DeleteFlag == "0" 
                    & QmcCheckItemDetail._.ActivateFlag == "1" & QmcCheckItemDetail._.LatestFlag == "1"
                    )
                .OrderBy(QmcCheckItem._.ItemId.Asc)
                .ToArrayList<QmcCheckItemDetail>();
        }
    }

    public class QmcCheckDataQueryParams : IQmcCheckDataQueryParams
    {
        public string MinorTypeID { get; set; }
        public string MaterCode { get; set; }
        public string BeginCheckDate { get; set; }
        public string EndCheckDate { get; set; }
        public string SupplyFacId { get; set; }
        public string ProductFacId { get; set; }
        public string CheckResult { get; set; }
        public string Barcode { get; set; }
        public string BillNo { get; set; }
        public string RecordStat { get; set; }
        public string RecorderId { get; set; }
    }

    public class QmcSampleLedgeQueryParams : IQmcSampleLedgeQueryParams
    {
        public string BeginRecordDate { get; set; }
        public string EndRecordDate { get; set; }
        public string SampleName { get; set; }
        public string SampleCode { get; set; }
        public string SupplierId { get; set; }
        public string ManufacturerId { get; set; }
        public string FactoryCode { get; set; }
        public string Barcode { get; set; }
    }

    public class QmcCheckDataQueryItemDetailParams : IQmcCheckDataQueryItemDetailParams
    {
        public string StandardId { get; set; }
        public string MaterCode { get; set; }
        public string Frequency { get; set; }
    }
}
