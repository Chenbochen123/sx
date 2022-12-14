using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pst_MinstockService : BaseService<Pst_Minstock>, IPst_MinstockService
    {
		#region 构造方法

        public Pst_MinstockService() : base(){ }

        public Pst_MinstockService(string connectStringKey) : base(connectStringKey){ }

        public Pst_MinstockService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
