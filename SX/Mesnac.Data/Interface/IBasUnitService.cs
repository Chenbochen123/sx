using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasUnitService : IBaseService<BasUnit>
    {
        //��λ�ķ�ҳ����
        PageResult<BasUnit> GetTablePageDataBySql(Mesnac.Data.Implements.BasUnitService.QueryParams queryParams);

        //��ȡ��λ����һ������ֵ
        int GetUnitNextPrimaryKeyValue();
    }
}
