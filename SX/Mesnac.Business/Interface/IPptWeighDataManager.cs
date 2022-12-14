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
        /// ���ݼƻ�ID��ѯ��С�ϳ�����Ϣ
        /// ���˽�
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        DataTable GetSmallMaterWeighListByPlanID(string planID);

        /// <summary>
        /// ���ݼƻ�ID��ѯ��С�ϳ�����׼��Ϣ
        /// ���˽�
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
