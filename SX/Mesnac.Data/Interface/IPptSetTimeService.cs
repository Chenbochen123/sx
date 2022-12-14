using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
    public interface IPptSetTimeService : IBaseService<PptSetTime>
    {

        /// <summary>
        /// ���ݹ���ID��ѯ���ʱ���
        /// ���˽�
        /// 2013-1-29
        /// </summary>
        /// <param name="procedureID">����ID</param>
        /// <returns></returns>
        DataSet GetDataSetByProcedureID(string procedureID);
        /// <summary>
        /// ���ݹ���ID��ȡ�Ĺ���������
        /// ���˽�
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">����ID</param>
        /// <returns></returns>
        int GetShiftNumByProcedureID(int proid);

        /// <summary>
        /// �޸Ĺ���İ��ʱ���
        /// ���˽�
        /// 2013-2-10
        /// </summary>
        /// <param name="setTime"></param>
        bool UpdateSetTime(PptSetTime setTime);
    }
}
