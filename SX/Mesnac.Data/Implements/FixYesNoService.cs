using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class FixYesNoService : BaseService<FixYesNo>, IFixYesNoService
    {
		#region 构造方法

        public FixYesNoService() : base(){ }

        public FixYesNoService(string connectStringKey) : base(connectStringKey){ }

        public FixYesNoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
