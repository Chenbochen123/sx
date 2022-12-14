using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Sys_ClearAlarmUserService : BaseService<Sys_ClearAlarmUser>, ISys_ClearAlarmUserService
    {
		#region 构造方法

        public Sys_ClearAlarmUserService() : base(){ }

        public Sys_ClearAlarmUserService(string connectStringKey) : base(connectStringKey){ }

        public Sys_ClearAlarmUserService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
