using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_EquipProblemListService : BaseService<Eqm_EquipProblemList>, IEqm_EquipProblemListService
    {
		#region 构造方法

        public Eqm_EquipProblemListService() : base(){ }

        public Eqm_EquipProblemListService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_EquipProblemListService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
