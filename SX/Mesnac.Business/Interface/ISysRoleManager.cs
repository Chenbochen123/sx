
namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysRoleManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:31
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleManager : IBaseManager<SysRole>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:46:32
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysRole> GetTablePageDataBySql(SysRoleManager.QueryParams queryParams);
    }
}
