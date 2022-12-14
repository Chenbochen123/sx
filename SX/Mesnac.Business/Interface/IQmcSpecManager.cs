using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IQmcSpecManager : IBaseManager<QmcSpec>
    {
        PageResult<QmcSpec> GetTablePageDataBySql(QmcSpecManager.QueryParams queryParams);
        //获取下一个规格编号
        string GetNextSpecId();
    }
}
