using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_ShiftimeService : BaseService<Ppt_Shiftime>, IPpt_ShiftimeService
    {
		#region 构造方法

        public Ppt_ShiftimeService() : base(){ }

        public Ppt_ShiftimeService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_ShiftimeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
