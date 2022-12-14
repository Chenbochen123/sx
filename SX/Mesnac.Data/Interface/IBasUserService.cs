using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasUserService : IBaseService<BasUser>
    {
        PageResult<BasUser> GetTablePageDataBySql(Mesnac.Data.Implements.BasUserService.QueryParams queryParams);
        //��ȡ��Ա����һ������ֵ
        string GetNextWorkBarcode();
    }
}
