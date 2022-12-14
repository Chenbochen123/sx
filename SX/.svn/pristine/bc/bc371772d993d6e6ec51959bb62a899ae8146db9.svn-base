using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using System.Data;
    public interface IBasWorkUserInfoService : IBaseService<BasWorkUserInfo>
    {
        PageResult<BasWorkUserInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams);
        //获取下一个编号
        string GetNextObjID();
        DataSet UserQueryByCode(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams);
    }
}
