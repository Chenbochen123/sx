using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_CleanStandService : BaseService<Eqm_CleanStand>, IEqm_CleanStandService
    {
		#region ���췽��

        public Eqm_CleanStandService() : base(){ }

        public Eqm_CleanStandService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_CleanStandService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
