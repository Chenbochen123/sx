using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;

    using NBear.Common;

    public class QmcCheckDataManager : BaseManager<QmcCheckData>, IQmcCheckDataManager
    {
        #region ����ע���빹�췽��

        private IQmcCheckDataService service;

        public QmcCheckDataManager()
        {
            this.service = new QmcCheckDataService();
            base.BaseService = this.service;
        }

        public QmcCheckDataManager(string connectStringKey)
        {
            this.service = new QmcCheckDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcCheckDataManager(NBear.Data.Gateway way)
        {
            this.service = new QmcCheckDataService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// �����������(������������ֵ�ͼ����Ŀֵ)
        /// </summary>
        /// <param name="mQmcCheckData"></param>
        /// <param name="mQmcCheckDataPropertyList"></param>
        /// <param name="mQmcCheckDataDetailList"></param>
        public void Insert(QmcCheckData mQmcCheckData
            , EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList
            , EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList)
        {
            System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            {
                this.service.Insert(mQmcCheckData);
                int checkId = mQmcCheckData.CheckId;

                IQmcCheckDataPropertyService dQmcCheckDataPropertyService = new QmcCheckDataPropertyService();
                foreach (QmcCheckDataProperty mQmcCheckDataProperty in mQmcCheckDataPropertyList)
                {
                    mQmcCheckDataProperty.CheckId = checkId;
                    dQmcCheckDataPropertyService.Insert(mQmcCheckDataProperty);
                }

                IQmcCheckDataDetailService dQmcCheckDataDetailService = new QmcCheckDataDetailService();
                foreach (QmcCheckDataDetail mQmcCheckDataDetail in mQmcCheckDataDetailList)
                {
                    mQmcCheckDataDetail.CheckId = checkId;
                    dQmcCheckDataDetailService.Insert(mQmcCheckDataDetail);
                }
                scope.Complete();
                scope.Dispose();

            }

        }

        /// <summary>
        /// ���¼������(ɾ�������²���������ֵ�ͼ����Ŀֵ)
        /// </summary>
        /// <param name="mQmcCheckData"></param>
        /// <param name="mQmcCheckDataPropertyList"></param>
        /// <param name="mQmcCheckDataDetailList"></param>
        public void Update(QmcCheckData mQmcCheckData
            , EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList
            , EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList)
        {
            System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            {
                this.service.Update(mQmcCheckData);
                int checkId = mQmcCheckData.CheckId;

                IQmcCheckDataPropertyService dQmcCheckDataPropertyService = new QmcCheckDataPropertyService();
                dQmcCheckDataPropertyService.DeleteByWhere(QmcCheckDataProperty._.CheckId == checkId);
                foreach (QmcCheckDataProperty mQmcCheckDataProperty in mQmcCheckDataPropertyList)
                {
                    mQmcCheckDataProperty.CheckId = checkId;
                    dQmcCheckDataPropertyService.Insert(mQmcCheckDataProperty);
                }

                IQmcCheckDataDetailService dQmcCheckDataDetailService = new QmcCheckDataDetailService();
                dQmcCheckDataDetailService.DeleteByWhere(QmcCheckDataDetail._.CheckId == checkId);
                foreach (QmcCheckDataDetail mQmcCheckDataDetail in mQmcCheckDataDetailList)
                {
                    mQmcCheckDataDetail.CheckId = checkId;
                    dQmcCheckDataDetailService.Insert(mQmcCheckDataDetail);
                }
                scope.Complete();
                scope.Dispose();

            }
        }

        /// <summary>
        /// ���ݲ�����ȡ�������
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetDataSetByParams(IQmcCheckDataQueryParams paras)
        {
            return this.service.GetDataSetByParams(paras);
        }

        /// <summary>
        /// ���ݲ�����ȡ�ʼ챨������
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras)
        {
            return this.service.GetReportDataSetByParams(paras);
        }

        /// <summary>
        /// ���ݲ�����ȡ��Ʒ̨������
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetQmcSampleLedgerInfoQueryByParams(IQmcSampleLedgeQueryParams paras)
        {
            return this.service.GetQmcSampleLedgerInfoQueryByParams(paras);
        }

        /// <summary>
        /// ��ȡ�������������¼������Ϣ
        /// </summary>
        /// <remarks>
        /// �����б��ֶ�: WorkBarcode(��Ա���) UserName(��Ա����) 
        /// </remarks>
        /// <returns></returns>
        public DataSet GetAllRecorderInfo()
        {
            return this.service.GetAllRecorderInfo();
        }

        /// <summary>
        /// ��ȡĳ��ԭ���ϵĹ����Ϣ
        /// </summary>
        /// <param name="materCode"></param>
        /// <returns></returns>
        public DataSet GetSpecInfoByMaterCode(string materCode)
        {
            return this.service.GetSpecInfoByMaterCode(materCode);
        }

        /// <summary>
        /// ����ִ�б�׼��ԭ���ϡ�����Ƶ�Σ���ȡ��Ч�����µļ��ָ����Ϣ
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public EntityArrayList<QmcCheckItemDetail> GetCheckItemDetailByParams(IQmcCheckDataQueryItemDetailParams paras)
        {
            return this.service.GetCheckItemDetailByParams(paras);
        }
    }
}
