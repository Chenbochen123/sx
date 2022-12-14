using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialStoreinService : BaseService<PstMaterialStorein>, IPstMaterialStoreinService
    {
		#region 构造方法

        public PstMaterialStoreinService() : base(){ }

        public PstMaterialStoreinService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialStoreinService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string billType { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string filedFlag { get; set; }
            public string hrCode { get; set; }
            public string deleteFlag { get; set; }
            public string barcode { get; set; }
            public string userCode { get; set; }
            public PageResult<PstMaterialStorein> pageParams { get; set; }
        }

        public PageResult<PstMaterialStorein> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialStorein> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, BillType, C.FacName FactoryName, B.UserName, InputDate, convert(bit, LockedFlag) LockedFlag, convert(bit, FiledFlag) FiledFlag, ChkPerson, A.Remark 
                                from PstMaterialStorein A
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasFactoryInfo C on A.FactoryID = C.ObjID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.billType))
            {
                sqlstr.AppendLine(" AND A.BillType = '" + queryParams.billType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InputDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InputDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.filedFlag))
            {
                sqlstr.AppendLine(" AND A.FiledFlag = '" + queryParams.filedFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND B.HRCode = '" + queryParams.hrCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
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

        public PageResult<PstMaterialStorein> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            PageResult<PstMaterialStorein> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, D.UserName, C.Barcode, C.OrderID, E.MaterialName, A.InputDate, B.StorageName, G.StoragePlaceName, C.InputNum, C.InputWeight, C.PieceWeight, C.ProcDate, A.StorageID, C.StoragePlaceID 
                                    from PstMaterialStorein A
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join PstMaterialStoreinDetail C on A.BillNo = C.BillNo
                                    left join BasUser D on D.WorkBarcode = '000001'
                                    left join BasMaterial E on C.MaterCode = E.MaterialCode
                                    left join BasUser F on A.MakerPerson = F.WorkBarcode
                                    left join BasStoragePlace G on C.StoragePlaceID = G.StoragePlaceID
                                where A.ChkResultFlag = '1'");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InputDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InputDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND C.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND D.HRCode = '" + queryParams.hrCode + "'");
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

        /// <summary>
        /// 生成入库单编号 规则：SJ+年月日+三位随机号   暂定
        /// </summary>
        /// <returns>送检单号</returns>
        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PstMaterialStoreIn where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'RK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PstMaterialStoreIn where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'RK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                //首先修改审批合格的标志位，然后将数据插入到库存明细数据表和主表中
                sql = @"update PstMaterialStorein set ChkResultFlag = '1', LockedFlag='1', ChkPerson = '" + UserID + "', ChkDate = getdate() where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public bool CancelChkResult(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PstMaterialStorein set ChkResultFlag = '0', LockedFlag='0', ChkPerson = '" + UserID + "', ChkDate = null where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID)
        {
            string sql = @"select A.BillNo, D.UserName, C.Barcode, C.OrderID, C.MaterCode, E.MaterialSimpleName, E.MaterialOtherName, E.MaterialName, I.ProductPlace, A.InputDate, B.StorageName, 
                                G.StoragePlaceName, C.InputNum, C.InputWeight, C.PieceWeight, C.ProcDate, A.StorageID, C.StoragePlaceID, A.ChkDate, A.ChkResultFlag,
                                case when E.ValidDate != null and E.ValidDate != 0 then DATEADD(DAY, E.ValidDate, C.ProcDate) else '2099-12-31 23:59:59' end ValidDate,
                                    DATEPART(MONTH, C.ProcDate) date1, DATEPART(DAY, C.ProcDate) date2, DATEPART(HOUR, C.ProcDate) date3, DATEPART(MI, C.ProcDate) date4,
                                    DATEPART(MONTH, case when E.ValidDate != null and E.ValidDate != 0 then DATEADD(DAY, E.ValidDate, C.ProcDate) else '2099-12-31 23:59:59' end) date5,
                                    DATEPART(DAY, case when E.ValidDate != null and E.ValidDate != 0 then DATEADD(DAY, E.ValidDate, C.ProcDate) else '2099-12-31 23:59:59' end) date6,
                                    DATEPART(HOUR, case when E.ValidDate != null and E.ValidDate != 0 then DATEADD(DAY, E.ValidDate, C.ProcDate) else '2099-12-31 23:59:59' end) date7,
                                    DATEPART(MI, case when E.ValidDate != null and E.ValidDate != 0 then DATEADD(DAY, E.ValidDate, C.ProcDate) else '2099-12-31 23:59:59' end) date8
                            from PstMaterialStorein A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join PstMaterialStoreinDetail C on A.BillNo = C.BillNo
                                left join BasUser D on D.WorkBarcode = '000001'
                                left join BasMaterial E on C.MaterCode = E.MaterialCode
                                left join BasUser F on A.MakerPerson = F.WorkBarcode
                                left join BasStoragePlace G on C.StoragePlaceID = G.StoragePlaceID
                                left join PstStorage I on C.BillNo = I.Barcode and C.StoragePlaceID = I.StoragePlaceID
                            where A.ChkResultFlag = '1'";
            if (!string.IsNullOrEmpty(billNo))
                sql += " and A.BillNo = '" + billNo + "'";
            if (!string.IsNullOrEmpty(storageID))
                sql += " and A.StorageID = '" + storageID + "'";
            if (!string.IsNullOrEmpty(storagePlaceID))
                sql += " and C.StoragePlaceID = '" + storagePlaceID + "'";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and C.Barcode = '" + barcode + "'";
            if (!string.IsNullOrEmpty(orderID))
                sql += " and C.OrderID = '" + orderID + "'";

            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
