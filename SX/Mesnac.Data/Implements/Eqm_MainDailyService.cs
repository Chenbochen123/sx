using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_MainDailyService : BaseService<Eqm_MainDaily>, IEqm_MainDailyService
    {
		#region ���췽��

        public Eqm_MainDailyService() : base(){ }

        public Eqm_MainDailyService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_MainDailyService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
