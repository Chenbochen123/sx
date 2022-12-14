using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberChkDetailService : BaseService<PpmRubberChkDetail>, IPpmRubberChkDetailService
    {
		#region 构造方法

        public PpmRubberChkDetailService() : base(){ }

        public PpmRubberChkDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberChkDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string barcode { get; set; }
            public string chkResultFlag { get; set; }
            public string NoPassFlag { get; set; }
            public PageResult<PpmRubberChkDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberChkDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberChkDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, Barcode, OrderID, ProductNo, MaterCode, B.MaterialName MaterialName, ProcDate, SendNum, PieceWeight, SendWeight, RecordDate, ChkDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, convert(bit, SendChkFlag) SendChkFlag, StoreInNum, StoreInWeight
                                from PpmRubberChkDetail A
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
                sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
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

        public DataSet GetByBillNo(string BillNo, string IsStoreIn)
        {
            string sql = @"select BillNo, ProductNo, Barcode, OrderID, MaterCode, B.MaterialName MaterialName, ProcDate, SendChkDate, SendNum, 
                            PieceWeight, SendWeight, ChkDate, RecordDate, convert(bit, ChkResultFlag) ChkResultFlag, PassWeight, 
                            convert(bit, SendChkFlag) SendChkFlag, A.DeleteFlag, A.Remark, A.PassNum, A.PassNum-A.StoreInNum LastNum, 
                            A.PassWeight-A.StoreInNum*A.PieceWeight LastWeight, StoreInNum, StoreInWeight, 0 NewNum
                            from PpmRubberChkDetail A
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
                            from PpmRubberChkDetail A
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
                            from PpmRubberChkDetail A
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
            int rows = this.GetBySql("select Barcode from PpmRubberChkDetail where DeleteFlag = '0' and BillNo = '" + BillNo + "'").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select '" + BillNo.Replace("SJ", "BC") + "' + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(Barcode, 4))) + 1), 4) from PpmRubberChkDetail where DeleteFlag = '0' and BillNo = '" + BillNo + "'").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select '" + BillNo.Replace("SJ", "BC") + "' +  '0001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateSendChkFlag(string StrBillNo, string SendPerson)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = "update PpmRubberChkDetail set ChkDate=GETDATE(), SendChkFlag = '1', SendPerson='" + SendPerson + "', ChkResultFlag = '1', PassNum = SendNum, PassWeight = SendWeight where DeleteFlag = '0' and BillNo in (" + StrBillNo + ")";
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
                sql = "update PpmRubberChkDetail set ChkDate=null, SendChkFlag = '0', SendPerson='" + SendPerson + "', ChkResultFlag = '0', PassNum = 0, PassWeight = 0 where DeleteFlag = '0' and BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public PpmRubberChkDetail GetEntity(string BillNo, string Barcode, string OrderID)
        {
            PpmRubberChkDetail chkDetail = this.GetBySql("select * from PpmRubberChkDetail where BillNo = '" + BillNo + "' and Barcode = '" + Barcode + "' and orderID = '" + OrderID + "'").ToFirst<PpmRubberChkDetail>();
            return chkDetail;
        }
    }
}
