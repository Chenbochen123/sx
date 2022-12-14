using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PptAlarmService : BaseService<PptAlarm>, IPptAlarmService
    {
		#region 构造方法

        public PptAlarmService() : base(){ }

        public PptAlarmService(string connectStringKey) : base(connectStringKey){ }

        public PptAlarmService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
