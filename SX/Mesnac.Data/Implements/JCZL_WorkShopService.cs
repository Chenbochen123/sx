using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class JCZL_WorkShopService : BaseService<JCZL_WorkShop>, IJCZL_WorkShopService
    {
		#region 构造方法

        public JCZL_WorkShopService() : base(){ }

        public JCZL_WorkShopService(string connectStringKey) : base(connectStringKey){ }

        public JCZL_WorkShopService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
