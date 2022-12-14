using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SYS_USERService : BaseService<SYS_USER>, ISYS_USERService
    {
		#region 构造方法

        public SYS_USERService() : base(){ }

        public SYS_USERService(string connectStringKey) : base(connectStringKey){ }

        public SYS_USERService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
