using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_CleanRecordService : BaseService<Eqm_CleanRecord>, IEqm_CleanRecordService
    {
		#region ���췽��

        public Eqm_CleanRecordService() : base(){ }

        public Eqm_CleanRecordService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_CleanRecordService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
