using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmtQrigImportLogMasterService : IBaseService<QmtQrigImportLogMaster>
    {
        System.Data.DataSet GetQrigDetailInfo(string guid);
    }
}
