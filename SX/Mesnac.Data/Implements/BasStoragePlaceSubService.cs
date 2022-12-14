using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class BasStoragePlaceSubService : BaseService<BasStoragePlaceSub>, IBasStoragePlaceSubService
    {
		#region 构造方法

        public BasStoragePlaceSubService() : base(){ }

        public BasStoragePlaceSubService(string connectStringKey) : base(connectStringKey){ }

        public BasStoragePlaceSubService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
