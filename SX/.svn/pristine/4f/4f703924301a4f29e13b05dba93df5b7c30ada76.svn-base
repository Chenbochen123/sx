using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;

    using NBear.Common;

    public interface IQmtQrigMasterManager : IBaseManager<QmtQrigMaster>
    {
        PageResult<QmtQrigMaster> GetDataByQueryParams(IQmtQrigMasterQueryParams queryParams);

        PageResult<QmtQrigMaster> GetDetailDataByQueryParams(IQmtQrigMasterQueryParams queryParams);

        void SaveImport(EntityArrayList<QmtQrigMaster> mQmtQrigMasterList, EntityArrayList<QmtQrigDetail> mQmtQrigDetailList);

        void SaveImport_ReCheck(EntityArrayList<QmtQrigMaster> mQmtQrigMasterList, EntityArrayList<QmtQrigDetail> mQmtQrigDetailList);

        void LogicDelete(QmtQrigMaster entity);

        void LogicUpdate(QmtQrigMaster entity);

        DataSet StaticsQrigProductionAmount(IQmtQrigMasterStaticProdAmountParams paras);
    }
}
