using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using System.Data;
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    public interface IQmcSampleLedgerService : IBaseService<QmcSampleLedger>
    {
        DataTable GetLedgerUnion();
        DataTable GetLedgerUnion(QmcSampleLedgerService.QueryParams param);
        //获取下一个台账编号
        string GetNextLedgerId();
        //获取自动流水号
        string GetAutoFlowSampleCode();
    }
}
