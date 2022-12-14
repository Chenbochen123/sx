using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pst_mminjarService : BaseService<Pst_mminjar>, IPst_mminjarService
    {
		#region 构造方法

        public Pst_mminjarService() : base(){ }

        public Pst_mminjarService(string connectStringKey) : base(connectStringKey){ }

        public Pst_mminjarService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
