using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_SetTimeService : BaseService<Ppt_SetTime>, IPpt_SetTimeService
    {
		#region ���췽��

        public Ppt_SetTimeService() : base(){ }

        public Ppt_SetTimeService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_SetTimeService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
