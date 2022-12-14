using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class EQM_waiWeiWXService : BaseService<EQM_waiWeiWX>, IEQM_waiWeiWXService
    {
		#region 构造方法

        public EQM_waiWeiWXService() : base(){ }

        public EQM_waiWeiWXService(string connectStringKey) : base(connectStringKey){ }

        public EQM_waiWeiWXService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
