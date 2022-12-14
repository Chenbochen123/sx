using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcLedgerKeyValueService : IBaseService<QmcLedgerKeyValue>
    {
        //获取下一个值编号
        string GetNextValueId();
    }
}
