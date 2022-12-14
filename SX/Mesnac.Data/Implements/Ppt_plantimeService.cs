using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_plantimeService : BaseService<Ppt_plantime>, IPpt_plantimeService
    {
		#region 构造方法

        public Ppt_plantimeService() : base(){ }

        public Ppt_plantimeService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_plantimeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
