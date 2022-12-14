using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_bjStockService : BaseService<Eqm_bjStock>, IEqm_bjStockService
    {
		#region 构造方法

        public Eqm_bjStockService() : base(){ }

        public Eqm_bjStockService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_bjStockService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
