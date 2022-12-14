using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using System.Data;
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    public interface IQmcSampleLedgerManager : IBaseManager<QmcSampleLedger>
    {
        DataTable GetLedgerUnion();
        DataTable GetLedgerUnion(QmcSampleLedgerService.QueryParams param);
        string GetNextLedgerId();
        string GetAutoFlowSampleCode();
    }
}
