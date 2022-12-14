using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_MpParamService : BaseService<Eqm_MpParam>, IEqm_MpParamService
    {
		#region 构造方法

        public Eqm_MpParamService() : base(){ }

        public Eqm_MpParamService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_MpParamService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法



    }
}
