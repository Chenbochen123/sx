using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    using Mesnac.Data.Components;
    public class PpmRubberStorageDealService : BaseService<PpmRubberStorageDeal>, IPpmRubberStorageDealService
    {
        #region 构造方法

        public PpmRubberStorageDealService() : base() { }

        public PpmRubberStorageDealService(string connectStringKey) : base(connectStringKey) { }

        public PpmRubberStorageDealService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法
        public class QueryParams
        {
            public PageResult<PpmRubberStorageDeal> pageParams { get; set; }
            public string matercode { get; set; }
        }
        public DataTable SubmitOutDateDeal(string BarCode, string StorageID, string StoragePlaceID, string DealWay, string DealDate, string DealRemark, string DealPerson)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMOutDateDeal2");
            sps.AddInputParameter("BarCode", this.TypeToDbType(BarCode.GetType()), BarCode);
            sps.AddInputParameter("StorageID", this.TypeToDbType(StorageID.GetType()), StorageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(StoragePlaceID.GetType()), StoragePlaceID);
            sps.AddInputParameter("DealWay", this.TypeToDbType(DealWay.GetType()), DealWay);
            sps.AddInputParameter("DealDate", this.TypeToDbType(DealDate.GetType()), DealDate);
            sps.AddInputParameter("DealRemark", this.TypeToDbType(DealRemark.GetType()), DealRemark);
            sps.AddInputParameter("DealPerson", this.TypeToDbType(DealPerson.GetType()), DealPerson);
            return sps.ToDataSet().Tables[0];
        }
        public DataTable GetDateQueryByCode(string BarCode, string StorageID, string StoragePlaceID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMOutDateQueryByCode");
            sps.AddInputParameter("BarCode", this.TypeToDbType(BarCode.GetType()), BarCode);
            sps.AddInputParameter("StorageID", this.TypeToDbType(StorageID.GetType()), StorageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(StoragePlaceID.GetType()), StoragePlaceID);
            return sps.ToDataSet().Tables[0];
        }
        public PageResult<PpmRubberStorageDeal> ProcPPMOutDateQueryInvalid(QueryParams queryParams, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode,int type,string dealperson)
        {
            PageResult<PpmRubberStorageDeal> pageParams = queryParams.pageParams;
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMOutDateQueryInValid2");
            sps.AddInputParameter("WorkShop", this.TypeToDbType(workShop.GetType()), workShop);
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("BarCode", this.TypeToDbType(barCode.GetType()), barCode);
            sps.AddInputParameter("ShlefBarCode", this.TypeToDbType(shlefBarCode.GetType()), shlefBarCode);
            sps.AddInputParameter("Type", this.TypeToDbType(type.GetType()), type);
            sps.AddInputParameter("dealperson", this.TypeToDbType(dealperson.GetType()), dealperson);
            string sqlstr = sps.ToDataSet().Tables[0].Rows[0][0].ToString();
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
        public string SubmitRubberInValid(int dealid, string OperPerson)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("SubmitRubberInValid");
            sps.AddInputParameter("DealId", this.TypeToDbType(dealid.GetType()), dealid);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(OperPerson.GetType()), OperPerson);
            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }
        public string SubmitOutDateRubberInValid(int dealid, string OperPerson, string dealdate, string dealremark)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("SubmitOutDateRubberInValid");
            sps.AddInputParameter("DealId", this.TypeToDbType(dealid.GetType()), dealid);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(OperPerson.GetType()), OperPerson);
            sps.AddInputParameter("dealdate", this.TypeToDbType(dealdate.GetType()), dealdate);
            sps.AddInputParameter("dealremark", this.TypeToDbType(dealremark.GetType()), OperPerson);
            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }
        public string SubmitRubberOutDateInValid(int dealid, string OperPerson,string dealway, string dealdate, string dealremark)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("SubmitRubberOutDateInValid");
            sps.AddInputParameter("DealId", this.TypeToDbType(dealid.GetType()), dealid);
            sps.AddInputParameter("DealWay", this.TypeToDbType(dealway.GetType()), dealway);
            sps.AddInputParameter("DealDate", this.TypeToDbType(dealdate.GetType()), dealdate);
            sps.AddInputParameter("DealRemark", this.TypeToDbType(dealremark.GetType()), dealremark);
            sps.AddInputParameter("DealPerson", this.TypeToDbType(OperPerson.GetType()), OperPerson);
            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }
        public PageResult<PpmRubberStorageDeal> ProcPPMValidDateQuery(QueryParams queryParams, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type,string dealperson)
        {
            PageResult<PpmRubberStorageDeal> pageParams = queryParams.pageParams;
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMValidDateQuery2");
            sps.AddInputParameter("WorkShop", this.TypeToDbType(workShop.GetType()), workShop);
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("BarCode", this.TypeToDbType(barCode.GetType()), barCode);
            sps.AddInputParameter("ShlefBarCode", this.TypeToDbType(shlefBarCode.GetType()), shlefBarCode);
            sps.AddInputParameter("Type", this.TypeToDbType(type.GetType()), type);
            sps.AddInputParameter("dealperson", this.TypeToDbType(dealperson.GetType()), dealperson);
            sps.AddInputParameter("MaterCode", this.TypeToDbType(queryParams.matercode.GetType()), queryParams.matercode);
            string sqlstr = sps.ToDataSet().Tables[0].Rows[0][0].ToString();
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
