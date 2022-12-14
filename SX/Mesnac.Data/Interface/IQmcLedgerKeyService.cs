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
        //��ȡ��һ����Ŀ���
        string GetNextKeyId();
    }
}
