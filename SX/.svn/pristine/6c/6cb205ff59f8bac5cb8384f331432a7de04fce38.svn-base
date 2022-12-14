using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPptLotDataService : IBaseService<PptLotData>
    {
        PageResult<PptLotData> GetBarcodeTablePageDataBySql(PptLotDataService.QueryParams queryParams);
        PageResult<PptLotData> GetTablePageDataBySql(PptLotDataService.QueryParams queryParams);

        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLotData> GetBarCodeScannPageDataBySql(PptLotDataService.QueryParams queryParams,string sqlwhere);
        /// <summary>
        /// 车报表使用根据barcode获得lotinfo
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        DataSet GetLotInfoByBarcode(string barcode);

        PageResult<PptLotData> GetAnalysisTechnology(PptLotDataService.QueryParams queryParams);
        /// <summary>
        /// 获取主机手产量统计
        /// 孙宜建
        /// 2013-05-27
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLotData> GetTablePageHostStatisticsBySql(PptLotDataService.QueryParams queryParams);
        /// <summary>
        /// 获取班组产量统计
        /// 孙宜建
        /// 2013-05-28
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLotData> GetTablePageClassStatisticsBySql(PptLotDataService.QueryParams queryParams);
    }
}
