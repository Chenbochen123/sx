using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class PstStorgaeDetailService : BaseService<PstStorgaeDetail>, IPstStorgaeDetailService
    {
		#region ���췽��

        public PstStorgaeDetailService() : base(){ }

        public PstStorgaeDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstStorgaeDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
