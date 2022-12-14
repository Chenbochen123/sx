using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_ClassUserService : BaseService<Ppt_ClassUser>, IPpt_ClassUserService
    {
		#region 构造方法

        public Ppt_ClassUserService() : base(){ }

        public Ppt_ClassUserService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_ClassUserService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
