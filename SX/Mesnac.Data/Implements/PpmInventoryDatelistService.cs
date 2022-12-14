using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmInventoryDatelistService : BaseService<PpmInventoryDatelist>, IPpmInventoryDatelistService
    {
		#region 构造方法

        public PpmInventoryDatelistService() : base(){ }

        public PpmInventoryDatelistService(string connectStringKey) : base(connectStringKey){ }

        public PpmInventoryDatelistService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
