using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PmtOpenActionModelDetailManager : BaseManager<PmtOpenActionModelDetail>, IPmtOpenActionModelDetailManager
    {
		#region ����ע���빹�췽��
		
        private IPmtOpenActionModelDetailService service;

        public PmtOpenActionModelDetailManager()
        {
            this.service = new PmtOpenActionModelDetailService();
            base.BaseService = this.service;
        }

		public PmtOpenActionModelDetailManager(string connectStringKey)
        {
			this.service = new PmtOpenActionModelDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtOpenActionModelDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PmtOpenActionModelDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// Ԭ�� @2014��9��29��11:12:26
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtOpenActionModelDetailService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// Ԭ�� @2014��9��29��11:12:32
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtOpenActionModelDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        /// <summary>
        /// ���湤���䷽
        /// �ﱾǿ @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="pmtRecipe">The PMT recipe.</param>
        /// <param name="pmtRecipeWeight">The PMT recipe weight.</param>
        /// <param name="pmtRecipeMixing">The PMT recipe mixing.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SaveOpenActionModelDetail(string MainModelID, EntityArrayList<PmtOpenActionModelDetail> pmtOpenActionModelDetailList ,string openMixingNo)
        {
            string Result = "";
            #region ��������
                //����
                foreach (var item in pmtOpenActionModelDetailList)
                {
                   EntityArrayList<PmtOpenActionModelDetail>  detailList = this.service.GetListByWhere(
                       PmtOpenActionModelDetail._.MainModelID == item.MainModelID && 
                       PmtOpenActionModelDetail._.OpenMixingNo == openMixingNo  &&
                       PmtOpenActionModelDetail._.MixingStep == item.MixingStep);
                   if (detailList.Count > 0)
                   {
                       PmtOpenActionModelDetail detail = detailList[0];
                       detail.OpenActionCode = item.OpenActionCode;
                       detail.MixingStep = item.MixingStep; ;
                       detail.MixTime = item.MixTime;
                       detail.CoolMixSpeed = item.CoolMixSpeed;
                       detail.OpenMixSpeed = item.OpenMixSpeed;
                       detail.MixRollor = item.MixRollor;
                       detail.WaterTemp = item.WaterTemp;
                       detail.RubberTemp = item.RubberTemp;
                       detail.CarSpeed = item.CarSpeed;
                       this.service.Update(detail);
                   }
                   else
                   {
                        PmtOpenActionModelDetail detail = new PmtOpenActionModelDetail();
                        detail.MainModelID = MainModelID;
                        detail.OpenMixingNo = openMixingNo;
                        detail.OpenActionCode = item.OpenActionCode;
                        detail.MixingStep = item.MixingStep;
                        detail.MixTime = item.MixTime;
                        detail.CoolMixSpeed = item.CoolMixSpeed;
                        detail.OpenMixSpeed = item.OpenMixSpeed;
                        detail.MixRollor = item.MixRollor;
                        detail.WaterTemp = item.WaterTemp;
                        detail.RubberTemp = item.RubberTemp;
                        detail.CarSpeed = item.CarSpeed;
                        this.service.Insert(detail);
                   }
                }
            #endregion
            return Result;
        }   
    }
}
