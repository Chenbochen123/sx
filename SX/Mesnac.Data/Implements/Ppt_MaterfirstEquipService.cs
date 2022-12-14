using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_MaterfirstEquipService : BaseService<Ppt_MaterfirstEquip>, IPpt_MaterfirstEquipService
    {
		#region 构造方法

        public Ppt_MaterfirstEquipService() : base(){ }

        public Ppt_MaterfirstEquipService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_MaterfirstEquipService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
