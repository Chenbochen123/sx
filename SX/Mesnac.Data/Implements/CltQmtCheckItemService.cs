using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class CltQmtCheckItemService : BaseService<CltQmtCheckItem>, ICltQmtCheckItemService
    {
		#region ���췽��

        public CltQmtCheckItemService() : base(){ }

        public CltQmtCheckItemService(string connectStringKey) : base(connectStringKey){ }

        public CltQmtCheckItemService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
