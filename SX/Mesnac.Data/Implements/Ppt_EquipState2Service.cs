using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_EquipState2Service : BaseService<Ppt_EquipState2>, IPpt_EquipState2Service
    {
		#region 构造方法

        public Ppt_EquipState2Service() : base(){ }

        public Ppt_EquipState2Service(string connectStringKey) : base(connectStringKey){ }

        public Ppt_EquipState2Service(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
