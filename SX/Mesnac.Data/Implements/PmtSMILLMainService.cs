using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtSMILLMainService : BaseService<PmtSMILLMain>, IPmtSMILLMainService
    {
		#region 构造方法

        public PmtSMILLMainService() : base(){ }

        public PmtSMILLMainService(string connectStringKey) : base(connectStringKey){ }

        public PmtSMILLMainService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
