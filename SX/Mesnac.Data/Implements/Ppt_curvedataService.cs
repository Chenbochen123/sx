using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_curvedataService : BaseService<Ppt_curvedata>, IPpt_curvedataService
    {
		#region 构造方法

        public Ppt_curvedataService() : base(){ }

        public Ppt_curvedataService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_curvedataService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
