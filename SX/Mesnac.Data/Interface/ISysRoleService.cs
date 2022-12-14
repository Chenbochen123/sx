
namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysRoleService 接口定义
    /// 孙本强 @ 2013-04-03 12:50:49
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleService : IBaseService<SysRole>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:50:49
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysRole> GetTablePageDataBySql(SysRoleService.QueryParams queryParams);
    }
}
