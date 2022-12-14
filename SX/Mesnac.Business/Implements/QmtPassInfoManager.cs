using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;

    using NBear.Common;

    public class QmtPassInfoManager : BaseManager<NBear.Common.Entity>, IQmtPassInfoManager
    {
        #region 属性注入与构造方法

        private IQmtPassInfoService service;

        public QmtPassInfoManager()
        {
            this.service = new QmtPassInfoService();
            base.BaseService = this.service;
        }

        public QmtPassInfoManager(string connectStringKey)
        {
            this.service = new QmtPassInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtPassInfoManager(NBear.Data.Gateway way)
        {
            this.service = new QmtPassInfoService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataInfoByQueryParams(IQmtPassInfoQueryParams paras)
        {
            return this.service.GetDataInfoByQueryParams(paras);
        }

        public void Pass(string barcode, string passUserId, string passUserName, string passMemo)
        {
            this.service.Pass(barcode, passUserId, passUserName, passMemo);
        }
    }
}
