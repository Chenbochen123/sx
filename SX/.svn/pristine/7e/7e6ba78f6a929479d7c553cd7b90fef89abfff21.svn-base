using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubInfoService : IBaseService<BasRubInfo>
    {
        //胶料信息分页方法
        PageResult<BasRubInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubInfoService.QueryParams queryParams);
        //获取胶料下一个编号
        string GetNextRubInfoCode();
    }
}
