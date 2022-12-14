using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialRubberSplitService : IBaseService<PstMaterialRubberSplit>
    {
        PageResult<PstMaterialRubberSplit> GetTablePageDataBySqlPrint(PstMaterialRubberSplitService.QueryParams queryParams);
        PageResult<PstMaterialRubberSplit> GetTablePageTotalBySqlPrint(PstMaterialRubberSplitService.QueryParams queryParams);
        PageResult<PstMaterialRubberSplit> GetTablePageOastBySqlPrint(PstMaterialRubberSplitService.QueryParams queryParams);
        DataSet GetByPrintInfo(string BarcodeSplit);
        DataSet GetSqlInfo(string sql);
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID, string OperPerson, string OperDate);
        DataSet GetByOastInfo(string StorageID, string StoragePlaceID, string MaterCode, string BeginDate, string EndDate, string barcode);
        string CancelBarcodeSplit(string storageID, string storagePlaceID, string barcodeSplit);
        DataSet GetBarcodeSplitQuery(string barcode, string storageID, string storagePlaceID);
        PageResult<PstMaterialRubberSplit> GetTableSplitReset(PstMaterialRubberSplitService.QueryParams queryParams);
        DataSet ProcUnReset(string BarCodeSplit, string OperPerson);
        PageResult<PstMaterialRubberSplit> GetTableSplitUnLock(PstMaterialRubberSplitService.QueryParams queryParams);
    }
}
