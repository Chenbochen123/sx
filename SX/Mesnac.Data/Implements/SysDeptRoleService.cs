using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysDeptRoleService : BaseService<SysDeptRole>, ISysDeptRoleService
    {
		#region ���췽��

        public SysDeptRoleService() : base(){ }

        public SysDeptRoleService(string connectStringKey) : base(connectStringKey){ }

        public SysDeptRoleService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
