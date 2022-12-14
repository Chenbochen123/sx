using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtCoolMILLMainService : BaseService<PmtCoolMILLMain>, IPmtCoolMILLMainService
    {
		#region 构造方法

        public PmtCoolMILLMainService() : base(){ }

        public PmtCoolMILLMainService(string connectStringKey) : base(connectStringKey){ }

        public PmtCoolMILLMainService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
