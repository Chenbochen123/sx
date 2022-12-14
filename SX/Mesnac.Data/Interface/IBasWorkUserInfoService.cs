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
        //��ȡ��һ�����
        string GetNextObjID();
        DataSet UserQueryByCode(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams);
    }
}
