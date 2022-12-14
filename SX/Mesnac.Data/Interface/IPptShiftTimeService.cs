using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
    public interface IPptShiftTimeService : IBaseService<PptShiftTime>
    {
        /// <summary>
        /// ������ʼ���ںͽ������ڲ�ѯ��Ӧ����İ����Ϣ
        /// ���˽�
        /// 2013-1-28
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        DataSet GetShiftTimeByTime(string start, string end, string dept);

        /// <summary>
        /// ���ô洢���������Ű���Ϣ
        /// ���˽�
        /// 2013-1-30
        /// </summary>
        /// <param name="dt">����</param>
        /// <param name="num">��������</param>
        /// <param name="proid">����ID</param>
        void AddPptShiftTime(string dt, int num, int proid);

        /// <summary>
        /// ��ȡ����İ����Ϣ
        /// ���˽�
        /// 2013-2-25
        /// </summary>
        /// <param name="shiftID">����</param>
        /// <param name="proID">����</param>
        /// <param name="date">����</param>
        /// <returns></returns>
        DataTable GetClassNameByPIDAndDate(string shiftID, string proID, string date);

        /// <summary>
        /// ��ȡ���쵱ǰʱ���Ӧ�İ�κͰ���
        /// ��Ӫ 2013-05-31
        /// </summary>
        /// <param name="procedureID">����</param>
        /// <param name="shiftClassID">ָ������</param>
        /// <returns></returns>
        DataSet GetShiftDS(string procedureID, string shiftClassID);
    }
}
