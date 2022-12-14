using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pmt_mstopkindService : BaseService<Pmt_mstopkind>, IPmt_mstopkindService
    {
		#region 构造方法

        public Pmt_mstopkindService() : base(){ }

        public Pmt_mstopkindService(string connectStringKey) : base(connectStringKey){ }

        public Pmt_mstopkindService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
