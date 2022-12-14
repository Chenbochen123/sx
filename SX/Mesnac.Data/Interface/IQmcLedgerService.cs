using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
using Mesnac.Data.Implements;
    public interface IQmcLedgerService : IBaseService<QmcLedger>
    {
        DataTable GetLedgerUnion();
        DataTable GetLedgerUnion(QmcLedgerService.QueryParams param);
        DataTable GetReport(QmcLedgerService.QueryParams param);
        //��ȡ��һ��̨�˱��
        string GetNextLedgerId();
    }
}
