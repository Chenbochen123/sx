using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPptLotManager : IBaseManager<PptLot>
    {
        PageResult<PptLot> GetTablePageDataBySql(PptLotManager.QueryParams queryParams);
        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptLot> GetBarCodeScannPageDataBySql(PptLotManager.QueryParams queryParams);
        DataSet GetLotInfoByBarcode(string barcode);
        DataSet GetPptLot(PptLotManager.QueryParams queryParams);
    }
}
