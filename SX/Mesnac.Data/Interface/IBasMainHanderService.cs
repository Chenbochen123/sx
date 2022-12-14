using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IBasMainHanderService : IBaseService<BasMainHander>
    {
        //单位的分页方法
        PageResult<BasMainHander> GetTablePageDataBySql(Mesnac.Data.Implements.BasMainHanderService.QueryParams queryParams);
        DataSet IshaveUserInfo(string MainHanderCode, string UserCode, string ObjID);

        DataSet GetMixMainHanderInfo();
    }
}
