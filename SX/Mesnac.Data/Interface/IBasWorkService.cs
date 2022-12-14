using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasWorkService : IBaseService<BasWork>
    {
        PageResult<BasWork> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkService.QueryParams queryParams);
        //��ȡ��λ��һ�����
        string GetNextWorkPositionCode();
    }
}
