using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PstMaterialRubberSplitManager : BaseManager<PstMaterialRubberSplit>, IPstMaterialRubberSplitManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialRubberSplitService service;

        public PstMaterialRubberSplitManager()
        {
            this.service = new PstMaterialRubberSplitService();
            base.BaseService = this.service;
        }

		public PstMaterialRubberSplitManager(string connectStringKey)
        {
			this.service = new PstMaterialRubberSplitService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialRubberSplitManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialRubberSplitService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialRubberSplitService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialRubberSplit> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySqlPrint(queryParams);
        }

        public PageResult<PstMaterialRubberSplit> GetTablePageTotalBySqlPrint(QueryParams queryParams)
        {
            return this.service.GetTablePageTotalBySqlPrint(queryParams);
        }

        public PageResult<PstMaterialRubberSplit> GetTablePageOastBySqlPrint(QueryParams queryParams)
        {
            return this.service.GetTablePageOastBySqlPrint(queryParams);
        }

        public DataSet GetByPrintInfo(string BarcodeSplit)
        {
            return this.service.GetByPrintInfo(BarcodeSplit);
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.service.GetSqlInfo(sql);
        }

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID, string OperPerson, string OperDate)
        {
            return this.service.GetByInfo(Barcode, StorageID, StoragePlaceID, OperPerson, OperDate);
        }

        public DataSet GetByOastInfo(string StorageID, string StoragePlaceID, string MaterCode, string BeginDate, string EndDate, string barcode)
        {
            return this.service.GetByOastInfo(StorageID, StoragePlaceID, MaterCode, BeginDate, EndDate,barcode);
        }

        public string CancelBarcodeSplit(string storageID, string storagePlaceID, string barcodeSplit)
        {
            return this.service.CancelBarcodeSplit(storageID, storagePlaceID, barcodeSplit);
        }

        public DataSet GetBarcodeSplitQuery(string barcode, string storageID, string storagePlaceID)
        {
            return this.service.GetBarcodeSplitQuery(barcode, storageID, storagePlaceID);
        }
        public PageResult<PstMaterialRubberSplit> GetTableSplitReset(QueryParams queryParams)
        {
            return this.service.GetTableSplitReset(queryParams);
        }
        public DataSet ProcUnReset(string BarCodeSplit, string OperPerson)
        {
            return this.service.ProcUnReset(BarCodeSplit, OperPerson);
        }
        public PageResult<PstMaterialRubberSplit> GetTableSplitUnLock(PstMaterialRubberSplitManager.QueryParams queryParams)
        {
            return this.service.GetTableSplitUnLock(queryParams);
        }
    }
}
