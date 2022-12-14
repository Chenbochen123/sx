using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
using System.Data;
    public interface ISysRubPowerUserService : IBaseService<SysRubPowerUser>
    {
        //������Ϣ��ҳ����
        PageResult<SysRubPowerUser> GetTablePageDataBySql(Mesnac.Data.Implements.SysRubPowerUserService.QueryParams queryParams);
    }
}
