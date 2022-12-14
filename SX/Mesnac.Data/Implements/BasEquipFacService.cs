using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class BasEquipFacService : BaseService<BasEquipFac>, IBasEquipFacService
    {
		#region 构造方法

        public BasEquipFacService() : base(){ }

        public BasEquipFacService(string connectStringKey) : base(connectStringKey){ }

        public BasEquipFacService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
