using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckAssessLotService : BaseService<QmtCheckAssessLot>, IQmtCheckAssessLotService
    {
		#region 构造方法

        public QmtCheckAssessLotService() : base(){ }

        public QmtCheckAssessLotService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckAssessLotService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
