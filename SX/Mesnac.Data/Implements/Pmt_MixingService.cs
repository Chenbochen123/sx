using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pmt_MixingService : BaseService<Pmt_Mixing>, IPmt_MixingService
    {
		#region 构造方法

        public Pmt_MixingService() : base(){ }

        public Pmt_MixingService(string connectStringKey) : base(connectStringKey){ }

        public Pmt_MixingService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
