using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysDeptActionService : BaseService<SysDeptAction>, ISysDeptActionService
    {
		#region 构造方法

        public SysDeptActionService() : base(){ }

        public SysDeptActionService(string connectStringKey) : base(connectStringKey){ }

        public SysDeptActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
