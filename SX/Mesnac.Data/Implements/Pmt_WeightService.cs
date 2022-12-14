using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pmt_WeightService : BaseService<Pmt_Weight>, IPmt_WeightService
    {
		#region 构造方法

        public Pmt_WeightService() : base(){ }

        public Pmt_WeightService(string connectStringKey) : base(connectStringKey){ }

        public Pmt_WeightService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
