using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class PstStorgaeService : BaseService<PstStorgae>, IPstStorgaeService
    {
		#region 构造方法

        public PstStorgaeService() : base(){ }

        public PstStorgaeService(string connectStringKey) : base(connectStringKey){ }

        public PstStorgaeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
