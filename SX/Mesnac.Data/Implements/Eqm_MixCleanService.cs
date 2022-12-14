using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_MixCleanService : BaseService<Eqm_MixClean>, IEqm_MixCleanService
    {
		#region 构造方法

        public Eqm_MixCleanService() : base(){ }

        public Eqm_MixCleanService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_MixCleanService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
