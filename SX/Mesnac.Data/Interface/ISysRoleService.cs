
namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysRoleService �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:50:49
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleService : IBaseService<SysRole>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:50:49
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysRole> GetTablePageDataBySql(SysRoleService.QueryParams queryParams);
    }
}
