using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pmt_XLAutoCreateService : BaseService<Pmt_XLAutoCreate>, IPmt_XLAutoCreateService
    {
		#region 构造方法

        public Pmt_XLAutoCreateService() : base(){ }

        public Pmt_XLAutoCreateService(string connectStringKey) : base(connectStringKey){ }

        public Pmt_XLAutoCreateService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
