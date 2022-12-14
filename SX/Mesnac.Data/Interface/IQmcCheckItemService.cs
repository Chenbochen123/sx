using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmcCheckItemService : IBaseService<QmcCheckItem>
    {
        PageResult<QmcCheckItem> GetTablePageDataBySql(Mesnac.Data.Implements.QmcCheckItemService.QueryParams queryParams);
        //获取下一个项目编号
        string GetNextItemId();
    }
}
