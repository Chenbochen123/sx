using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterialInOastService : BaseService<PstMaterialInOast>, IPstMaterialInOastService
    {
		#region 构造方法

        public PstMaterialInOastService() : base(){ }

        public PstMaterialInOastService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialInOastService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
