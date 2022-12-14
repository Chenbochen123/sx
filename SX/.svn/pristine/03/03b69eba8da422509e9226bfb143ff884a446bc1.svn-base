using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Data.Interface;
    using Mesnac.Entity;

    public interface IQmtQrigDetailManager : IBaseManager<QmtQrigDetail>
    {
        DataSet GetDataByParas(IQmtQrigDetailParams queryParams);

        void LogicDelete(QmtQrigMaster entityMaster, QmtQrigDetail entityDetail);

        void LogicUpdate(QmtQrigMaster entityMaster, QmtQrigDetail entityDetail);
    }
}
