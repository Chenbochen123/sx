using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class TblMixService : BaseService<TblMix>, ITblMixService
    {
		#region 构造方法

        public TblMixService() : base(){ }

        public TblMixService(string connectStringKey) : base(connectStringKey){ }

        public TblMixService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
