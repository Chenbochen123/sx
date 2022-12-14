using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using System.Data;
    using Mesnac.Data.Implements;
    public interface IQmcLedgerManager : IBaseManager<QmcLedger>
    {
        DataTable GetLedgerUnion();
        DataTable GetLedgerUnion(QmcLedgerService.QueryParams param);
        DataTable GetReport(QmcLedgerService.QueryParams param);
        string GetNextLedgerId();
    }
}
