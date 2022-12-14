using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class JCZL_partsService : BaseService<JCZL_parts>, IJCZL_partsService
    {
		#region 构造方法

        public JCZL_partsService() : base(){ }

        public JCZL_partsService(string connectStringKey) : base(connectStringKey){ }

        public JCZL_partsService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
