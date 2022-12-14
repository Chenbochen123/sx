using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_WeighService : BaseService<Ppt_Weigh>, IPpt_WeighService
    {
		#region 构造方法

        public Ppt_WeighService() : base(){ }

        public Ppt_WeighService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_WeighService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
