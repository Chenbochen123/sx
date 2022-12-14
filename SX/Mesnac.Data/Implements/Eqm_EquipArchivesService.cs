using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_EquipArchivesService : BaseService<Eqm_EquipArchives>, IEqm_EquipArchivesService
    {
		#region 构造方法

        public Eqm_EquipArchivesService() : base(){ }

        public Eqm_EquipArchivesService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_EquipArchivesService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
