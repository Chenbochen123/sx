using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialChkDetailService : BaseService<PstMaterialChkDetail>, IPstMaterialChkDetailService
    {
		#region 构造方法

        public PstMaterialChkDetailService() : base(){ }

        public PstMaterialChkDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialChkDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string barcode { get; set; }
            public string chkResultFlag { get; set; }
            public string NoPassFlag { get; set; }
            public string SendChkFlag { get; set; }
            public string materCode { get; set; }
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public PageResult<PstMaterialChkDetail> pageParams { get; set; }
        }

        public PageResult<PstMaterialChkDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialChkDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.ObjID, BillNo, Barcode, OrderID, ProductNo, MaterCode, B.MaterialName MaterialName, ProcDate, SendNum, PieceWeight, SendWeight, RecordDate, ChkDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, convert(bit, SendChkFlag) SendChkFlag, StoreInNum, StoreInWeight
                                from PstMaterialChkDetail A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                where A.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
            {
                if (queryParams.chkResultFlag == "0")
                {
                    sqlstr.AppendLine(" AND (A.ChkResultFlag IS NULL OR A.ChkResultFlag = '' OR A.ChkResultFlag '0')");
                }
                else
                {
                    sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
                }
            }
            if (!string.IsNullOrEmpty(queryParams.SendChkFlag))
            {
                sqlstr.AppendLine(" AND A.SendChkFlag = '" + queryParams.SendChkFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.NoPassFlag))
            {
                sqlstr.AppendLine(" AND A.SendNum - A.PassNum > 0");
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

        public PageResult<PstMaterialChkDetail> GetCheckSequence(QueryParams queryParams)
        {
            PageResult<PstMaterialChkDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.ObjID,A.BillNo,A.Barcode,A.OrderId,A.MaterCode,B.NoticeNo, C.MaterialName from PstMaterialChkDetail A
                                left join PstMaterialChk B on A.BillNo = B.BillNo
                                left join BasMaterial C on A.MaterCode = C.MaterialCode
                                where A.DeleteFlag = '0'
                                and B.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendFormat("AND A.BillNo LIKE '%{0}%'", queryParams.billNo);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendFormat("AND A.Barcode LIKE '%{0}%'", queryParams.barcode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendFormat("AND A.MaterCode = '{0}'", queryParams.materCode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.beginDate) && !string.IsNullOrEmpty(queryParams.endDate))
            {
                if (!(queryParams.beginDate.Contains("0001")) && !(queryParams.endDate.Contains("0001")))
                {
                    sqlstr.AppendLine("AND A.RecordDate BETWEEN '" + queryParams.beginDate + "' AND '" + queryParams.endDate + "'");
                }
            }
            if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
            {
                if (queryParams.chkResultFlag == "null")
                {
                    sqlstr.AppendLine(" AND A.ChkResultFlag IS NULL");
                }
                else
                {
                    sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
                }
            }
            if (!string.IsNullOrEmpty(queryParams.SendChkFlag))
            {
                sqlstr.AppendLine(" AND A.SendChkFlag = '" + queryParams.SendChkFlag + "'");
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

        public DataSet GetByBillNo(string BillNo, string IsStoreIn)
        {
            string sql = @"select BillNo, ProductNo, Barcode, OrderID, MaterCode, B.MaterialName MaterialName, ProcDate, SendChkDate, SendNum, 
                            PieceWeight, SendWeight, ChkDate, RecordDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, 
                            convert(bit, SendChkFlag) SendChkFlag, A.DeleteFlag, A.Remark, A.PassNum, A.PassNum-A.StoreInNum LastNum, 
                            A.PassWeight-A.StoreInNum*A.PieceWeight LastWeight, StoreInNum, StoreInWeight, 0 NewNum, A.LLBarcode
                            from PstMaterialChkDetail A
                            left join BasMaterial B on A.MaterCode = B.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "'";
            if (!string.IsNullOrEmpty(IsStoreIn))
                sql += " and A.StoreInNum = '0'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            string sql = @"select BillNo, ProductNo, Barcode, OrderID, MaterCode, B.MaterialName MaterialName, ProcDate, SendChkDate, SendNum, 
                            PieceWeight, SendWeight, ChkDate, RecordDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, 
                            convert(bit, SendChkFlag) SendChkFlag, A.DeleteFlag, A.Remark, A.PassNum, A.PassNum-A.StoreInNum LastNum, 
                            A.PassWeight-A.StoreInNum*A.PieceWeight LastWeight, StoreInNum, StoreInWeight, 0 NewNum
                            from PstMaterialChkDetail A
                            left join BasMaterial B on A.MaterCode = B.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetNoPassByBillNo(string BillNo)
        {
            string sql = @"select BillNo, ProductNo, Barcode, MaterCode, B.MaterialName MaterialName, ProcDate, SendNum, 
                            PieceWeight, SendWeight, ChkDate, RecordDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, 
                            convert(bit, SendChkFlag) SendChkFlag, A.DeleteFlag, A.Remark, A.PassNum, A.PassNum-A.StoreInNum LastNum, 
                            A.PassWeight-A.StoreInNum*A.PieceWeight LastWeight, StoreInNum, StoreInWeight, 0 NewNum, SendNum - PassNum NoPassNum, SendWeight - PassWeight NoPassWeight
                            from PstMaterialChkDetail A
                            left join BasMaterial B on A.MaterCode = B.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and ChkResultFlag = '1' and SendNum - PassNum >0";
            return this.GetBySql(sql).ToDataSet();
        }

        /// <summary>
        /// 生成条码编号 规则：送检单号+四位随机号   暂定
        /// </summary>
        /// <returns>条码号</returns>
        public string GetBarcode(string BillNo)
        {
            int rows = this.GetBySql("select Barcode from PstMaterialChkDetail where DeleteFlag = '0' and BillNo = '" + BillNo + "'").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select '" + BillNo.Replace("SJ","BC") + "' + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(Barcode, 4))) + 1), 4) from PstMaterialChkDetail where DeleteFlag = '0' and BillNo = '" + BillNo + "'").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select '" + BillNo.Replace("SJ", "BC") + "' +  '0001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateSendChkFlag(string StrBillNo, string SendPerson)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                 //ChkResultFlag = '1',
                sql = "update PstMaterialChkDetail set ChkDate=GETDATE(), SendChkFlag = '1', SendPerson='" + SendPerson + "', PassNum = SendNum, PassWeight = SendWeight where DeleteFlag = '0' and BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public bool CancelSendChk(string StrBillNo, string SendPerson)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = "update PstMaterialChkDetail set ChkDate=null, SendChkFlag = '0', SendPerson='" + SendPerson + "', ChkResultFlag = '0', PassNum = 0, PassWeight = 0 where DeleteFlag = '0' and BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public PstMaterialChkDetail GetEntity(string BillNo, string Barcode, string OrderID)
        {
            PstMaterialChkDetail chkDetail = this.GetBySql("select * from PstMaterialChkDetail where BillNo = '" + BillNo + "' and Barcode = '" + Barcode + "' and orderID = '" + OrderID + "'").ToFirst<PstMaterialChkDetail>();
            return chkDetail;
        }

        public DataSet GetAddLedgerCheckDetail(string BillNo, string Barcode, string OrderID)
        {
            //2014-08-04 暂时改为带样品台账的备注
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.BillNo, A.NoticeNo, A.FactoryID, A.Remark");
            sb.AppendLine(", B.ObjId, B.Barcode, B.OrderID, B.MaterCode, B.ProcDate, B.SendNum, B.SendWeight, B.LLBarcode");
            sb.AppendLine(", C.FacName, D.MaterialName");
            sb.AppendLine("FROM PstMaterialChk A");
            sb.AppendLine("INNER JOIN PstMaterialChkDetail B ON A.BillNo = B.BillNo");
            sb.AppendLine("LEFT JOIN BasFactoryInfo C ON A.FactoryID = C.ObjID");
            sb.AppendLine("LEFT JOIN BasMaterial D ON B.MaterCode = D.MaterialCode");
            sb.AppendLine("WHERE 1 = 1");
            if (!string.IsNullOrEmpty(BillNo))
            {
                sb.AppendLine(" AND B.BillNo = '" + BillNo + "'");
            }
            if (!string.IsNullOrEmpty(Barcode))
            {
                sb.AppendLine(" AND B.Barcode = '" + Barcode + "'");
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                sb.AppendLine(" AND B.OrderId = '" + OrderID + "'");
            }

            return this.GetBySql(sb.ToString()).ToDataSet();
        }

        public DataSet GetPstMaterialCheckDetailQueryInfoByParams(IPstMaterialCheckDetailQueryParams paras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT A.BillNo, A.NoticeNo, A.FactoryID, A.MakerPerson, A.InStockDate, A.FiledFlag, A.SendChkFlag");
            sb.AppendLine(", A.StockInFlag, A.DeleteFlag, A.Remark, A.IsFirstRecive");
            sb.AppendLine(", B.Barcode, B.OrderID, B.SeqNo, B.ProductNo, B.MaterCode, B.ProcDate, B.SendNum, B.PieceWeight");
            sb.AppendLine(", B.SendWeight, B.RecordDate, B.ChkDate, B.ChkResultFlag, B.PassNum, B.PassWeight, B.ChkPerson");
            sb.AppendLine(", B.DeleteFlag DetailDeleteFlag, B.Remark DetailRemark, B.IsFirstRecive DetailIsFirstRecive");
            sb.AppendLine(", CASE A.SendChkFlag WHEN '0' THEN '未审核' WHEN '1' THEN '已审核' ELSE '' END SendChkFlagDes");
            sb.AppendLine(", CASE A.StockInFlag WHEN '0' THEN '未入库' WHEN '1' THEN '已入库' ELSE '' END StockInFlagDes");
            sb.AppendLine(", CASE B.SendChkFlag WHEN '0' THEN '未送检' WHEN '1' THEN '已送检' ELSE '' END DetailSendChkFlagDes");
            sb.AppendLine(", C.FacName, D.MaterialName MaterName");
            sb.AppendLine("FROM PstMaterialChk A");
            sb.AppendLine("INNER JOIN PstMaterialChkDetail B ON A.BillNo = B.BillNo");
            sb.AppendLine("LEFT JOIN BasFactoryInfo C ON A.FactoryID = C.ObjID");
            sb.AppendLine("LEFT JOIN BasMaterial D ON B.MaterCode = D.MaterialCode");
            sb.AppendLine("WHERE 1 = 1");
            sb.AppendLine("AND A.DeleteFlag = '0'");
            sb.AppendLine("AND B.DeleteFlag = '0'");
            if (paras.BillNo != null && paras.BillNo != "")
            {
                sb.AppendFormat("AND A.BillNo LIKE '%{0}%'", paras.BillNo);
                sb.AppendLine();
            }
            if (paras.NoticeNo != null && paras.NoticeNo != "")
            {
                sb.AppendFormat("AND A.NoticeNo LIKE '%{0}%'", paras.NoticeNo);
                sb.AppendLine();
            }
            if (paras.FactoryId != null && paras.FactoryId != "")
            {
                sb.AppendFormat("AND A.FactoryID = '{0}'", paras.FactoryId);
                sb.AppendLine();
            }
            if (paras.BeginSendChkDate != null && paras.BeginSendChkDate != "")
            {
                sb.AppendFormat("AND B.SendChkDate >= '{0}'", paras.BeginSendChkDate);
                sb.AppendLine();
            }
            if (paras.EndSendChkDate != null && paras.EndSendChkDate != "")
            {
                sb.AppendFormat("AND B.SendChkDate < '{0}'", paras.EndSendChkDate);
                sb.AppendLine();
            }
            if (paras.SendChkFlag != null && paras.SendChkFlag != "")
            {
                sb.AppendFormat("AND A.SendChkFlag = '{0}'", paras.SendChkFlag);
                sb.AppendLine();
            }
            if (paras.StockInFlag != null && paras.StockInFlag != "")
            {
                sb.AppendFormat("AND A.StockInFlag = '{0}'", paras.StockInFlag);
                sb.AppendLine();
            }
            if (paras.DetailSendChkFlag != null && paras.DetailSendChkFlag != "")
            {
                sb.AppendFormat("AND B.SendChkFlag = '{0}'", paras.DetailSendChkFlag);
                sb.AppendLine();
            }
            if (paras.ChkResultFlag != null && paras.ChkResultFlag != "")
            {
                sb.AppendFormat("AND B.ChkResultFlag = '{0}'", paras.ChkResultFlag);
                sb.AppendLine();
            }

            sb.AppendLine("ORDER BY B.SendChkDate DESC, A.BillNo DESC");

            return this.GetBySql(sb.ToString()).ToDataSet();

        }

        /// <summary>
        /// 生成玲珑条码编号 规则：B+四位年份+四位流水号
        /// </summary>
        /// <returns>条码号</returns>
        public string GetLLBarcode(string Barcode)
        {
            //int rows = this.GetBySql("select distinct LLBarcode from PstMaterialChkDetail where DeleteFlag = '0' and Barcode = '" + Barcode + "'").ToDataSet().Tables[0].Rows.Count;
            //if (rows > 0)
            //    return this.GetBySql("select distinct LLBarcode from PstMaterialChkDetail where DeleteFlag = '0' and Barcode = '" + Barcode + "'").ToDataSet().Tables[0].Rows[0][0].ToString();
            //else
            //    return this.GetBySql("select ISNULL('B' + CONVERT(varchar, DATEPART(YEAR, GETDATE())) + RIGHT('0000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(LLBarcode, 4))) + 1), 4), 'B' + CONVERT(varchar, DATEPART(YEAR, GETDATE())) + '0001') from PstMaterialChkDetail where LLBarcode like 'B' + CONVERT(varchar, DATEPART(YEAR, GETDATE())) + '%'").ToDataSet().Tables[0].Rows[0][0].ToString();
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcGetLLBarcode");
            sps.AddInputParameter("Barcode", this.TypeToDbType(Barcode.GetType()), Barcode);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public string GetChkResult(string Barcode)
        {
            string sql = "select * from PstMaterialChkDetail where Barcode = '" + Barcode + "' and ChkResultFlag = '1'";
            int count = this.GetBySql(sql).ToDataSet().Tables[0].Rows.Count;

            if (count > 0)
                return "1";
            else
                return "0";
        }
    }

    public class PstMaterialCheckDetailQueryParams : IPstMaterialCheckDetailQueryParams
    {
        public string BillNo { get; set; }
        public string NoticeNo { get; set; }
        public string FactoryId { get; set; }
        public string BeginSendChkDate { get; set; }
        public string EndSendChkDate { get; set; }
        public string SendChkFlag { get; set; }
        public string StockInFlag { get; set; }
        public string DetailSendChkFlag { get; set; }
        public string ChkResultFlag { get; set; }

    }

}
