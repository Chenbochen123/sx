using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PptcurvedataService : BaseService<Pptcurvedata>, IPptcurvedataService
    {
		#region 构造方法

        public PptcurvedataService() : base(){ }

        public PptcurvedataService(string connectStringKey) : base(connectStringKey){ }

        public PptcurvedataService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
