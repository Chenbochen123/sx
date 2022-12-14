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
        //��ȡ��һ��̨�˱��
        string GetNextLedgerId();
        //��ȡ�Զ���ˮ��
        string GetAutoFlowSampleCode();
    }
}
