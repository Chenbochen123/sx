using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialStoreoutService : BaseService<PstMaterialStoreout>, IPstMaterialStoreoutService
    {
		#region 构造方法

        public PstMaterialStoreoutService() : base(){ }

        public PstMaterialStoreoutService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialStoreoutService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string lockedFlag { get; set; }
            public string hrCode { get; set; }
            public PageResult<PstMaterialStoreout> pageParams { get; set; }
        }

        public PageResult<PstMaterialStoreout> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialStoreout> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.RecordDate, A.OutputDate, A.DeptCode, C.DepName, A.LockedFlag, B.UserName, A.Remark 
                                from PstMaterialStoreout A
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasDept C on A.DeptCode = C.DepCode
                                where 1 = 1 and A.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND B.HRCode = '" + queryParams.hrCode + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OutputDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OutputDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.lockedFlag))
            {
                sqlstr.AppendLine(" AND A.LockedFlag = '" + queryParams.lockedFlag + "'");
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
        /// 生成出库单编号 规则：SJ+年月日+三位随机号   暂定
        /// </summary>
        /// <returns>出库单号</returns>
        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PstMaterialStoreout where DeleteFlag = '0' and SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'CK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PstMaterialStoreOut where DeleteFlag = '0' and SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'CK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateLockedFlag(string StrBillNo, string ChkPerson)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PstMaterialStoreout set LockedFlag = '1', ChkPerson = '" + ChkPerson + "', ChkResultFlag = '1', ChkDate = GETDATE() where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public bool CancelLocked(string StrBillNo, string ChkPerson)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PstMaterialStoreout set LockedFlag = '0', ChkPerson = '" + ChkPerson + "', ChkResultFlag = '0', ChkDate = null where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public DataSet GetStorageInfo(string BillNo)
        {
            DataSet ds = this.GetBySql(@"select A.BillNo, A.StorageID, B.Barcode, B.MaterCode from PstMaterialStoreout A
                                        left join PstMaterialStoreoutDetail B on A.BillNo = B.BillNo
                                        where A.BillNo = '" + BillNo + "'").ToDataSet();

            return ds;
        }
    }
}
