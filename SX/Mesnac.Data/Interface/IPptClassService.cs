using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IPptClassService : IBaseService<PptClass>
    {
        /// <summary>
        /// 根据班组名称查询班组信息
        /// 孙宜建
        /// 2013-1-25
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PptClass GetClassByName(string name);

        /// <summary>
        /// 分页方法获取班组数据
        /// yuany
        /// 2013年1月29日
        /// </summary>
        Components.PageResult<PptClass> GetTablePageDataBySql(Implements.PptClassService.QueryParams queryParams);
    }
}
