using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysUserAllActionService : BaseService<SysUserAllAction>, ISysUserAllActionService
    {
		#region 构造方法

        public SysUserAllActionService() : base(){ }

        public SysUserAllActionService(string connectStringKey) : base(connectStringKey){ }

        public SysUserAllActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
