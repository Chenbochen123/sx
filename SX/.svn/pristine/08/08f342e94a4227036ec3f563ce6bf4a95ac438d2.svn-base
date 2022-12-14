using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IPptWeighDataManager : IBaseManager<PptWeighData>
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

        PageResult<PptWeighData> GetOverErrorAllowPageDataBySql(PptWeighDataManager.QueryParams queryParams);
        PageResult<PptWeighData> GetWeighRatePageDataBySql(PptWeighDataManager.QueryParams queryParams);

        DataTable GetWeighMaterialByBarcode(string barcode);
    }
}
