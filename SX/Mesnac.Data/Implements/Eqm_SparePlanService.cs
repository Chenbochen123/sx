using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_SparePlanService : BaseService<Eqm_SparePlan>, IEqm_SparePlanService
    {
		#region 构造方法

        public Eqm_SparePlanService() : base(){ }

        public Eqm_SparePlanService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_SparePlanService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
