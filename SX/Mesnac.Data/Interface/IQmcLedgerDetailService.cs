using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcLedgerDetailService : IBaseService<QmcLedgerDetail>
    {
        //获取下一个明细编号
        string GetNextDetailId();
    }
}
