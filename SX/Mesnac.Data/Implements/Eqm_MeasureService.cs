using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_MeasureService : BaseService<Eqm_Measure>, IEqm_MeasureService
    {
		#region 构造方法

        public Eqm_MeasureService() : base(){ }

        public Eqm_MeasureService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_MeasureService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
