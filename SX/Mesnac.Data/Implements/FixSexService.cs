using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class FixSexService : BaseService<FixSex>, IFixSexService
    {
		#region ���췽��

        public FixSexService() : base(){ }

        public FixSexService(string connectStringKey) : base(connectStringKey){ }

        public FixSexService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
