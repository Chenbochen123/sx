using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pmt_materialService : BaseService<Pmt_material>, IPmt_materialService
    {
		#region 构造方法

        public Pmt_materialService() : base(){ }

        public Pmt_materialService(string connectStringKey) : base(connectStringKey){ }

        public Pmt_materialService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
