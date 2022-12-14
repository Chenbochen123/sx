using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysTaskRemindService : BaseService<SysTaskRemind>, ISysTaskRemindService
    {
		#region ���췽��

        public SysTaskRemindService() : base(){ }

        public SysTaskRemindService(string connectStringKey) : base(connectStringKey){ }

        public SysTaskRemindService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
