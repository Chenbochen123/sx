using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IQmcPropertyManager : IBaseManager<QmcProperty>
    {
        PageResult<QmcProperty> GetTablePageDataBySql(QmcPropertyManager.QueryParams queryParams);
        string GetNextPropertyId();
    }
}
