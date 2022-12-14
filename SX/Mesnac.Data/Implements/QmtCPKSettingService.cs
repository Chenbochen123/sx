using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCPKSettingService : BaseService<QmtCPKSetting>, IQmtCPKSettingService
    {
		#region 构造方法

        public QmtCPKSettingService() : base(){ }

        public QmtCPKSettingService(string connectStringKey) : base(connectStringKey){ }

        public QmtCPKSettingService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
