using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class SysCodeService : BaseService<SysCode>, ISysCodeService
    {
		#region 构造方法

        public SysCodeService() : base(){ }

        public SysCodeService(string connectStringKey) : base(connectStringKey){ }

        public SysCodeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
