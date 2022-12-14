using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialRubberSplitManager : IBaseManager<PstMaterialRubberSplit>
    {
        PageResult<PstMaterialRubberSplit> GetTablePageDataBySqlPrint(PstMaterialRubberSplitManager.QueryParams queryParams);
        PageResult<PstMaterialRubberSplit> GetTablePageTotalBySqlPrint(PstMaterialRubberSplitManager.QueryParams queryParams);
        PageResult<PstMaterialRubberSplit> GetTablePageOastBySqlPrint(PstMaterialRubberSplitManager.QueryParams queryParams);
        DataSet GetByPrintInfo(string BarcodeSplit);
        DataSet GetSqlInfo(string sql);
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID, string OperPerson, string OperDate);
        DataSet GetByOastInfo(string StorageID, string StoragePlaceID, string MaterCode, string BeginDate, string EndDate, string barcode);
        string CancelBarcodeSplit(string storageID, string storagePlaceID, string barcodeSplit);
        DataSet GetBarcodeSplitQuery(string barcode, string storageID, string storagePlaceID);
        PageResult<PstMaterialRubberSplit> GetTableSplitReset(PstMaterialRubberSplitManager.QueryParams queryParams);
        DataSet ProcUnReset(string BarCodeSplit, string OperPerson);
        PageResult<PstMaterialRubberSplit> GetTableSplitUnLock(PstMaterialRubberSplitManager.QueryParams queryParams);
    }
}
