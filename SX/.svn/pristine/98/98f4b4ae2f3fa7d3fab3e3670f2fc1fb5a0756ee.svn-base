using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtItemClassManager : IBaseManager<QmtItemClass>
    {
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<QmtItemClass> GetTablePageDataBySql(Mesnac.Data.Implements.QmtItemClassService.QueryParams queryParams);
        /// <summary>
        /// 获取下一个主键值
        /// </summary>
        /// <returns></returns>
        int GetItemClassNextPrimaryKeyValue();
    }
}
