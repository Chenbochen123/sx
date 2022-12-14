using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_downcodeService : BaseService<Eqm_downcode>, IEqm_downcodeService
    {
		#region 构造方法

        public Eqm_downcodeService() : base(){ }

        public Eqm_downcodeService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_downcodeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
