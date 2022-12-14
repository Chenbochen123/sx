
namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysRoleManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:46:31
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleManager : IBaseManager<SysRole>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 11:46:32
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysRole> GetTablePageDataBySql(SysRoleManager.QueryParams queryParams);
    }
}
