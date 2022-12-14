using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPptLotDataManager : IBaseManager<PptLotData>
    {
        PageResult<PptLotData> GetBarcodeTablePageDataBySql(PptLotDataManager.QueryParams queryParams);
        PageResult<PptLotData> GetTablePageDataBySql(PptLotDataManager.QueryParams queryParams);
        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLotData> GetBarCodeScannPageDataBySql(PptLotDataManager.QueryParams queryParams, string sqlwhere);
        DataSet GetLotInfoByBarcode(string barcode);


        PageResult<PptLotData> GetAnalysisTechnology(PptLotDataManager.QueryParams queryParams);

        /// <summary>
        /// 获取主机手产量统计
        /// 孙宜建
        /// 2013-05-27
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLotData> GetTablePageHostStatisticsBySql(PptLotDataManager.QueryParams queryParams);

        /// <summary>
        /// 获取班组产量统计
        /// 孙宜建
        /// 2013-05-28
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLotData> GetTablePageClassStatisticsBySql(PptLotDataManager.QueryParams queryParams);
    }
}
