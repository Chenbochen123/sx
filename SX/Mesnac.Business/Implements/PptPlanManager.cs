using System;
using System.Collections.Generic;
using System.Text;
using NBear.Common;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    using Mesnac.Data.Components;
    public class PptPlanManager : BaseManager<PptPlan>, IPptPlanManager
    {
        #region ����ע���빹�췽��

        private IPptPlanService service;

        public PptPlanManager()
        {
            this.service = new PptPlanService();
            base.BaseService = this.service;
        }

        public PptPlanManager(string connectStringKey)
        {
            this.service = new PptPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptPlanManager(NBear.Data.Gateway way)
        {
            this.service = new PptPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region ��ѯ�����ඨ��
        public class QueryParams : PptPlanService.QueryParams
        {
        }
        public PageResult<PptPlan> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        #endregion

        /// <summary>
        /// ����ָ����̨�ƻ���Ϣ
        /// ���˽�
        /// 2013-2-20
        /// </summary>
        /// <param name="group">��̨����</param>
        /// <param name="date">�ƻ�����</param>
        /// <returns></returns>
        public DataSet GetEquipPlan(string equipCode, string date)
        {
            return service.GetEquipPlan(equipCode, date);
        }
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
        public DataSet GetEquipPlan(string equipCode, string date, string shiftid, string recipematerialcode, string recipename)
        {
            return service.GetEquipPlan(equipCode, date, shiftid, recipematerialcode, recipename);
        }
        /// <summary>
        /// ����ָ��������豸��λʱ�����
        /// ����
        /// 2014-3-25
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workShopCode"></param>
        /// <returns></returns>
        public DataSet GetEquipmentPruductionSummary(DateTime beginTime, DateTime endTime, string workShopCode, string shiftId, string equipmentCode)
        {
            return service.GetEquipmentPruductionSummary(beginTime, endTime, workShopCode, shiftId, equipmentCode);
        }
        /// <summary>
        /// �ƻ�ִ�м��
        /// 2013-03-7
        /// ���˽�
        /// </summary>
        /// <param name="equipCode">��̨</param>
        /// <param name="date">����</param>
        /// <returns></returns>
        public DataSet GetPlanMonitor(string equipCode, string date)
        {
            return service.GetPlanMonitor(equipCode, date);
        }
        /// <summary>
        /// ���������ƻ���
        /// ���˽�
        /// 2013-2-26
        /// </summary>
        /// <param name="date">����XXXX-XX-XX</param>
        /// <param name="equipCode">��̨����</param>
        /// <param name="shiftid">���</param>
        /// <returns></returns>
        public string GetGetMaxPlanId(string date, string equipCode, string shiftid)
        {
            return this.service.GetGetMaxPlanId(date, equipCode, shiftid);
        }

        /// <summary>
        /// ����ƻ�ʱ���Ĳ��������ȼ�
        /// sunyj
        /// 2013-2-27
        /// </summary>
        /// <param name="date">�ƻ�����</param>
        /// <param name="equipCode">��̨���</param>
        /// <param name="shiftid">���</param>
        /// <param name="priLevel">��������ȼ�</param>
        public bool UpdatePriLevel(string date, string equipCode, string shiftid, int priLevel)
        {
            return this.service.UpdatePriLevel(date, equipCode, shiftid, priLevel);
        }

        /// <summary>
        /// �����´�ƻ�
        /// ���˽� 2013-3-1
        /// </summary>
        /// <param name="equipCode">��̨</param>
        /// <param name="date">����</param>
        /// <param name="shiftid">���</param>
        /// <returns></returns>
        public bool AllUpdatePlanState(string equipCode, string date, string shiftid)
        {
            return this.service.AllUpdatePlanState(equipCode, date, shiftid);
        }
        /// <summary>
        /// ��ȡҩƷ�ļƻ��Ų���Ϣ
        /// ���˽�
        /// 2013-3-2
        /// </summary>
        /// <param name="date">����</param>
        /// <param name="equipCode">��̨</param>
        /// <param name="shiftId">���</param>
        /// <returns></returns>
        public DataSet GetXLPlanCreate(string date, string equipCode, string shiftId)
        {
            return this.service.GetXLPlanCreate(date, equipCode, shiftId);
        }

        #region IPptPlanManager ��Ա

        /// <summary>
        /// ��ȡ�����ƻ������ϵ�
        /// 2013-4-1
        /// ���˽�
        /// </summary>
        /// <param name="date">�ƻ�����</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetSumDayMater(string date, string type,string store)
        {
            return this.service.GetSumDayMater(date, type,store);
        }

        /// <summary>
        /// ��ȡС�ϳ�����ѯ�еļƻ���Ϣ
        /// ���˽�
        /// 2013-4-2
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptPlan> GetSmallPlanTablePageDataBySql(PptPlanService.QueryParams queryParams)
        {
            return this.service.GetSmallPlanTablePageDataBySql(queryParams);
        }

        #endregion


        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 13:34:04
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PptPlan> GetPlanLotReportPageDataBySql(QueryParams queryParams)
        {
            return this.service.GetPlanLotReportPageDataBySql(queryParams);
        }


        public EntityArrayList<BasMaterial> GetPlanPptMaterial(PptPlanManager.QueryParams queryParams)
        {
            return this.service.GetPlanPptMaterial(queryParams);
        }


        public DataSet GetRptPlanLotMain(string planID) {
            return this.service.GetRptPlanLotMain(planID);
        }
        public DataSet GetRptPlanLotMainAvgAndSum(string planID)
        {
            return this.service.GetRptPlanLotMainAvgAndSum(planID);
        }
        public  DataSet GetRptPlanLotMaterialDetailInfo(string planID) {
            return this.service.GetRptPlanLotMaterialDetailInfo(planID);
        }
        public DataSet GetRptPlanLotRubsDetailInfo(string planID)
        {
            return this.service.GetRptPlanLotRubsDetailInfo(planID);
        }
        public DataSet GetRptPlanLotSumDetailInfo(string planID)
        {
            return this.service.GetRptPlanLotSumDetailInfo(planID);
        }
        public DataSet GetPlanTotalReport(string byGroup, DateTime beginDate, DateTime endDate, string shiftID, string classID, string equipCode, string materCode)
        {
            return this.service.GetPlanTotalReport(byGroup, beginDate, endDate, shiftID, classID, equipCode, materCode);
        }

        public PageResult<PptPlan> GetTableTotalDataBySql(QueryParams queryParams)
        {
            return this.service.GetTableTotalDataBySql(queryParams);
        }

        public DataSet CompoundQuery(QueryParams queryParams)
        {
            return this.service.CompoundQuery(queryParams);
        }
        public DataSet CompoundQueryMonth(QueryParams queryParams)
        {
            return this.service.CompoundQueryMonth(queryParams);
        }
      
    }
}
