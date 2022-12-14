using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberBackReasonService : BaseService<PpmRubberBackReason>, IPpmRubberBackReasonService
    {
		#region 构造方法

        public PpmRubberBackReasonService() : base(){ }

        public PpmRubberBackReasonService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberBackReasonService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
