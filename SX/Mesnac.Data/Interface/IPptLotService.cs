using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPptLotService : IBaseService<PptLot>
    {
        PageResult<PptLot> GetTablePageDataBySql(PptLotService.QueryParams queryParams);

        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLot> GetBarCodeScannPageDataBySql(PptLotService.QueryParams queryParams);
        /// <summary>
        /// 车报表使用根据barcode获得lotinfo
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        DataSet GetLotInfoByBarcode(string barcode);
        DataSet GetPptLot(PptLotService.QueryParams queryParams);
    }
}
