using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_bjsparecdService : BaseService<Eqm_bjsparecd>, IEqm_bjsparecdService
    {
		#region 构造方法

        public Eqm_bjsparecdService() : base(){ }

        public Eqm_bjsparecdService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_bjsparecdService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
