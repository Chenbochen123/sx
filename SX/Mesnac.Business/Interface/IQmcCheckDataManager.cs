using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;

    using NBear.Common;

    public interface IQmcCheckDataManager : IBaseManager<QmcCheckData>
    {
        void Insert(QmcCheckData mQmcCheckData
            , EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList
            , EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList);

        void Update(QmcCheckData mQmcCheckData
            , EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList
            , EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList);

        DataSet GetDataSetByParams(IQmcCheckDataQueryParams paras);

        DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras);

        DataSet GetQmcSampleLedgerInfoQueryByParams(IQmcSampleLedgeQueryParams paras);

        DataSet GetAllRecorderInfo();

        DataSet GetSpecInfoByMaterCode(string materCode);

        EntityArrayList<QmcCheckItemDetail> GetCheckItemDetailByParams(IQmcCheckDataQueryItemDetailParams paras);
    }
}
