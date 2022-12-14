using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using System.Data;
    public interface IPptWeighService : IBaseService<PptWeigh>
    {
        /// <summary>
        /// ���ݼƻ�ID��ѯ��С�ϳ�����Ϣ
        /// ���˽�
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        DataTable GetSmallMaterWeighListByPlanID(string planID);
    }
}