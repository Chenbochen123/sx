using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PptLotManager : BaseManager<PptLot>, IPptLotManager
    {
		#region 属性注入与构造方法
		
        private IPptLotService service;

        public PptLotManager()
        {
            this.service = new PptLotService();
            base.BaseService = this.service;
        }

		public PptLotManager(string connectStringKey)
        {
			this.service = new PptLotService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptLotManager(NBear.Data.Gateway way)
        {
			this.service = new PptLotService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PptLotService.QueryParams
        {
        }
        public PageResult<PptLot> GetTablePageDataBySql(PptLotManager.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLot> GetBarCodeScannPageDataBySql(PptLotManager.QueryParams queryParams)
        {
            return this.service.GetBarCodeScannPageDataBySql(queryParams);
        }
        

        public DataSet GetLotInfoByBarcode(string barcode)
        {
            return this.service.GetLotInfoByBarcode(barcode);
        }
        public DataSet GetPptLot(PptLotManager.QueryParams queryParams)
        {
            return this.service.GetPptLot(queryParams);
        }
    }
}
