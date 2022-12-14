using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcCheckDataService : IBaseService<QmcCheckData>
    {
        DataSet GetDataSetByParams(IQmcCheckDataQueryParams paras);

        DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras);

        DataSet GetQmcSampleLedgerInfoQueryByParams(IQmcSampleLedgeQueryParams paras);

        DataSet GetAllRecorderInfo();

        DataSet GetSpecInfoByMaterCode(string materCode);

        NBear.Common.EntityArrayList<QmcCheckItemDetail> GetCheckItemDetailByParams(IQmcCheckDataQueryItemDetailParams paras);
    }

    public interface IQmcCheckDataQueryParams
    {
        string MinorTypeID { get; set; }
        string MaterCode { get; set; }
        string BeginCheckDate { get; set; }
        string EndCheckDate { get; set; }
        string SupplyFacId { get; set; }
        string ProductFacId { get; set; }
        string CheckResult { get; set; }
        string Barcode { get; set; }
        string BillNo { get; set; }
        string RecordStat { get; set; }
        string RecorderId { get; set; }
    }

    public interface IQmcSampleLedgeQueryParams
    {
        string BeginRecordDate { get; set; }
        string EndRecordDate { get; set; }
        string SampleName { get; set; }
        string SampleCode { get; set; }
        string SupplierId { get; set; }
        string ManufacturerId { get; set; }
        string FactoryCode { get; set; }
        string Barcode { get; set; }

    }

    public interface IQmcCheckDataQueryItemDetailParams
    {
        string StandardId { get; set; }
        string MaterCode { get; set; }
        string Frequency { get; set; }
    }
}
