using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcLedgerDetailService : IBaseService<QmcLedgerDetail>
    {
        //��ȡ��һ����ϸ���
        string GetNextDetailId();
    }
}
