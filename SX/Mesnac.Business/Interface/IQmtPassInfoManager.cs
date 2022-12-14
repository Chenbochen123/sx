using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Data.Interface;
    using Mesnac.Entity;

    public interface IQmtPassInfoManager : IBaseManager<NBear.Common.Entity>
    {
        DataSet GetDataInfoByQueryParams(IQmtPassInfoQueryParams paras);

        void Pass(string barcode, string passUserId, string passUserName, string passMemo);
    }
}
