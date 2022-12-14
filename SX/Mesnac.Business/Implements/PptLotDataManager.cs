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
    public class PptLotDataManager : BaseManager<PptLotData>, IPptLotDataManager
    {
        #region 属性注入与构造方法

        private IPptLotDataService service;

        public PptLotDataManager()
        {
            this.service = new PptLotDataService();
            base.BaseService = this.service;
        }

        public PptLotDataManager(string connectStringKey)
        {
            this.service = new PptLotDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptLotDataManager(NBear.Data.Gateway way)
        {
            this.service = new PptLotDataService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PptLotDataService.QueryParams
        {
        }
        public PageResult<PptLotData> GetBarcodeTablePageDataBySql(PptLotDataManager.QueryParams queryParams)
        {
            return this.service.GetBarcodeTablePageDataBySql(queryParams);
        }
        public PageResult<PptLotData> GetTablePageDataBySql(PptLotDataManager.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public PageResult<PptLotData> GetAnalysisTechnology(PptLotDataManager.QueryParams queryParams)
        {
            return this.service.GetAnalysisTechnology(queryParams);
        }

        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLotData> GetBarCodeScannPageDataBySql(PptLotDataManager.QueryParams queryParams, string sqlwhere)
        {
            return this.service.GetBarCodeScannPageDataBySql(queryParams, sqlwhere);
        }
        /// <summary>
        /// 获取主机手产量统计
        /// 孙宜建
        /// 2013-05-27
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLotData> GetTablePageHostStatisticsBySql(PptLotDataManager.QueryParams queryParams)
        {
            return this.service.GetTablePageHostStatisticsBySql(queryParams);
        }

        /// <summary>
        /// 获取班组产量统计
        /// 孙宜建
        /// 2013-05-28
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLotData> GetTablePageClassStatisticsBySql(PptLotDataManager.QueryParams queryParams)
        {
            return this.service.GetTablePageClassStatisticsBySql(queryParams);
        }
        public DataSet GetLotInfoByBarcode(string barcode)
        {
            return this.service.GetLotInfoByBarcode(barcode);
        }
    }
}
