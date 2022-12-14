using System;
using System.Collections.Generic;
using System.Text;
using NBear.Common;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
    using Mesnac.Data.Components;
    public interface IPptPlanService : IBaseService<PptPlan>
    {

        /// <summary>
        /// ����ָ����̨�ƻ���Ϣ
        /// ���˽�
        /// 2013-2-20
        /// </summary>
        /// <param name="group">��̨����</param>
        /// <param name="date">�ƻ�����</param>
        /// <returns></returns>
        DataSet GetEquipPlan(string equipCode, string date);
        /// <summary>
        /// ��������ѯ����ָ����̨�ƻ���Ϣ
        /// Ԭ��
        /// 2013-3-21
        /// </summary>
        /// <param name="equipCode">��̨����</param>
        /// <param name="date">�ƻ�����</param>
        /// <param name="shiftid">��κ�</param>
        /// <param name="recipematerialcode">�䷽���ϴ���</param>
        /// <param name="recipename">�䷽����</param>
        /// <returns></returns>
        DataSet GetEquipPlan(string equipCode, string date, string shiftid, string recipematerialcode, string recipename);
        /// <summary>
        /// �ƻ�ִ�м��
        /// 2013-03-7
        /// ���˽�
        /// </summary>
        /// <param name="equipCode">��̨</param>
        /// <param name="date">����</param>
        /// <returns></returns>
        DataSet GetPlanMonitor(string equipCode, string date);
        /// <summary>
        /// ���������ƻ���
        /// ���˽�
        /// 2013-2-26
        /// </summary>
        /// <param name="date">����XXXX-XX-XX</param>
        /// <param name="equipCode">��̨����</param>
        /// <param name="shiftid">���</param>
        /// <returns></returns>
        string GetGetMaxPlanId(string date, string equipCode, string shiftid);
        /// <summary>
        /// ����ָ��������豸��λʱ�����
        /// ����
        /// 2014-3-25
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workShopCode"></param>
        /// <returns></returns>
        DataSet GetEquipmentPruductionSummary(DateTime beginTime, DateTime endTime, string workShopCode, string shiftId, string equipmentCode);
        /// <summary>
        /// ����ƻ�ʱ���Ĳ��������ȼ�
        /// sunyj
        /// 2013-2-27
        /// </summary>
        /// <param name="date">�ƻ�����</param>
        /// <param name="equipCode">��̨���</param>
        /// <param name="shiftid">���</param>
        /// <param name="priLevel">��������ȼ�</param>
        bool UpdatePriLevel(string date, string equipCode, string shiftid, int priLevel);

        /// <summary>
        /// �����´�ƻ�
        /// ���˽� 2013-3-1
        /// </summary>
        /// <param name="equipCode">��̨</param>
        /// <param name="date">����</param>
        /// <param name="shiftid">���</param>
        /// <returns></returns>
        bool AllUpdatePlanState(string equipCode, string date, string shiftid);


        /// <summary>
        /// ��ȡҩƷ�ļƻ��Ų���Ϣ
        /// ���˽�
        /// 2013-3-2
        /// </summary>
        /// <param name="date">����</param>
        /// <param name="equipCode">��̨</param>
        /// <param name="shiftId">���</param>
        /// <returns></returns>
        DataSet GetXLPlanCreate(string date, string equipCode, string shiftId);
        /// <summary>
        /// �ƻ�ִ�з���
        /// ���˽�
        /// 2013-3-28
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptPlan> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanService.QueryParams queryParams);
        /// <summary>
        /// ��ȡ�����ƻ������ϵ�
        /// 2013-4-1
        /// ���˽�
        /// </summary>
        /// <param name="date">�ƻ�����</param>
        /// <param name="type"></param>
        /// <returns></returns>
        DataTable GetSumDayMater(string date, string type,string store);

        /// <summary>
        /// ��ȡС�ϳ�����ѯ�еļƻ���Ϣ
        /// ���˽�
        /// 2013-4-2
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptPlan> GetSmallPlanTablePageDataBySql(Implements.PptPlanService.QueryParams queryParams);


        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 13:33:12
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<PptPlan> GetPlanLotReportPageDataBySql(Mesnac.Data.Implements.PptPlanService.QueryParams queryParams);

        EntityArrayList<BasMaterial> GetPlanPptMaterial(Implements.PptPlanService.QueryParams queryParams);
        /// <summary>
        /// ���������planIDֵ��ȡ����Ϣ
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        DataSet GetRptPlanLotMain(string planID);
        DataSet GetRptPlanLotMainAvgAndSum(string planID);
        DataSet GetRptPlanLotMaterialDetailInfo(string planID);
        DataSet GetRptPlanLotRubsDetailInfo(string planID);
        DataSet GetRptPlanLotSumDetailInfo(string planID);
        DataSet GetPlanTotalReport(string byGroup, DateTime beginDate, DateTime endDate, string shiftID, string classID, string equipCode, string materCode);
        PageResult<PptPlan> GetTableTotalDataBySql(Implements.PptPlanService.QueryParams queryParams);
        DataSet CompoundQuery(Implements.PptPlanService.QueryParams queryParams);
        DataSet CompoundQueryMonth(Implements.PptPlanService.QueryParams queryParams);

    }
}
