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
        //�䷽��Ϣ��ҳ����
        PageResult<TblRecipe> GetTablePageDataBySql(Mesnac.Data.Implements.TblRecipeService.QueryParams queryParams);
        //��ȡ������һ�����
        string GetNextRubInfoCode();
    }
}
