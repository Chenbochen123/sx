using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Business.Implements;
    using Mesnac.Data.Components;
    public interface IQmcFactoryMappingManager : IBaseManager<QmcFactoryMapping>
    {
        PageResult<QmcFactoryMapping> GetTablePageDataBySql(QmcFactoryMappingManager.QueryParams queryParams);
        //获取下一个关系编号
        string GetNextMappingId();
    }
}
