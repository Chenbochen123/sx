using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using System.Data;
    public interface IPptWeighDataService : IBaseService<PptWeighData>
    {
        /// <summary>
        /// 根据计划ID查询出小料称量信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        DataTable GetSmallMaterWeighListByPlanID(string planID);

        /// <summary>
        /// 根据计划ID查询出小料称量标准信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        DataTable GetSmallMaterWeighStandardByPlanID(string planID);

        PageResult<PptWeighData> GetOverErrorAllowPageDataBySql(PptWeighDataService.QueryParams queryParams);

        PageResult<PptWeighData> GetWeighRatePageDataBySql(PptWeighDataService.QueryParams queryParams);

        DataTable GetWeighMaterialByBarcode(string barcode);
    }
}
