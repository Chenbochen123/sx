using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_balanceCheckService : BaseService<Ppt_balanceCheck>, IPpt_balanceCheckService
    {
		#region 构造方法

        public Ppt_balanceCheckService() : base(){ }

        public Ppt_balanceCheckService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_balanceCheckService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
