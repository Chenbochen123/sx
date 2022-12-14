using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using System.Data;
    public interface IBasWorkUserInfoManager : IBaseManager<BasWorkUserInfo>
    {
        PageResult<BasWorkUserInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams);
        string GetNextObjID();
        DataSet UserQueryByCode(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams);

    }
}
