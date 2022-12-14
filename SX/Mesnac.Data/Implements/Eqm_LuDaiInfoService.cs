using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_LuDaiInfoService : BaseService<Eqm_LuDaiInfo>, IEqm_LuDaiInfoService
    {
		#region 构造方法

        public Eqm_LuDaiInfoService() : base(){ }

        public Eqm_LuDaiInfoService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_LuDaiInfoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
