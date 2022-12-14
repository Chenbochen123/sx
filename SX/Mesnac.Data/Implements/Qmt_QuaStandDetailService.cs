using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Qmt_QuaStandDetailService : BaseService<Qmt_QuaStandDetail>, IQmt_QuaStandDetailService
    {
		#region 构造方法

        public Qmt_QuaStandDetailService() : base(){ }

        public Qmt_QuaStandDetailService(string connectStringKey) : base(connectStringKey){ }

        public Qmt_QuaStandDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
