using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    public interface IQmtItemClassService : IBaseService<QmtItemClass>
    {
        //单位的分页方法
        PageResult<QmtItemClass> GetTablePageDataBySql(Mesnac.Data.Implements.QmtItemClassService.QueryParams queryParams);

        //获取单位的下一个主键值
        int GetItemClassNextPrimaryKeyValue();
    }
}
