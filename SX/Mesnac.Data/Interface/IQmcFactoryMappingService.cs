using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    public interface IQmcFactoryMappingService : IBaseService<QmcFactoryMapping>
    {
        PageResult<QmcFactoryMapping> GetTablePageDataBySql(Mesnac.Data.Implements.QmcFactoryMappingService.QueryParams queryParams);
        //��ȡ��һ����ϵ���
        string GetNextMappingId();
    }
}
