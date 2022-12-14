using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmcLedgerKeyService : IBaseService<QmcLedgerKey>
    {
        PageResult<QmcLedgerKey> GetTablePageDataBySql(Mesnac.Data.Implements.QmcLedgerKeyService.QueryParams queryParams);
        //获取下一个项目编号
        string GetNextKeyId();
    }
}
