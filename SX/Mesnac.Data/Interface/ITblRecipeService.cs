using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface ITblRecipeService : IBaseService<TblRecipe>
    {
        //配方信息分页方法
        PageResult<TblRecipe> GetTablePageDataBySql(Mesnac.Data.Implements.TblRecipeService.QueryParams queryParams);
        //获取胶料下一个编号
        string GetNextRubInfoCode();
    }
}
