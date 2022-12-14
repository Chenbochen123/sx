using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Ppt_pmdownrecordService : BaseService<Ppt_pmdownrecord>, IPpt_pmdownrecordService
    {
		#region 构造方法

        public Ppt_pmdownrecordService() : base(){ }

        public Ppt_pmdownrecordService(string connectStringKey) : base(connectStringKey){ }

        public Ppt_pmdownrecordService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
