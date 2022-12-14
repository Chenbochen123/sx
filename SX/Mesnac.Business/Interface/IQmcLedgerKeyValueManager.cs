using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    public interface IQmcLedgerKeyValueManager : IBaseManager<QmcLedgerKeyValue>
    {
        string GetNextValueId();
    }
}
