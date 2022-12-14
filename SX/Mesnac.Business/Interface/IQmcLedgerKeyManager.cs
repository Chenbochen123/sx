using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Business.Implements;
    using Mesnac.Data.Components;
    public interface IQmcLedgerKeyManager : IBaseManager<QmcLedgerKey>
    {
        PageResult<QmcLedgerKey> GetTablePageDataBySql(QmcLedgerKeyManager.QueryParams queryParams);
        string GetNextKeyId();
    }
}
