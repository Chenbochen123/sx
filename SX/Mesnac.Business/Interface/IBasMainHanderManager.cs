using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IBasMainHanderManager : IBaseManager<BasMainHander>
    {  
        /// <summary>
        /// ∑÷“≥∑Ω∑®
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<BasMainHander> GetTablePageDataBySql(Mesnac.Data.Implements.BasMainHanderService.QueryParams queryParams);
        DataSet IshaveUserInfo(string MainHanderCode, string UserCode, string ObjID);

        DataSet GetMixMainHanderInfo();
    }
}
