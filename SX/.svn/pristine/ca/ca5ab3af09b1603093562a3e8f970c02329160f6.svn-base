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
        #region 属性注入与构造方法

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
        /// 新增检测数据(并插入检测属性值和检测项目值)
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
        /// 更新检测数据(删除并重新插入检测属性值和检测项目值)
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
        /// 根据参数获取检测数据
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetDataSetByParams(IQmcCheckDataQueryParams paras)
        {
            return this.service.GetDataSetByParams(paras);
        }

        /// <summary>
        /// 根据参数获取质检报告数据
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras)
        {
            return this.service.GetReportDataSetByParams(paras);
        }

        /// <summary>
        /// 根据参数获取样品台帐数据
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetQmcSampleLedgerInfoQueryByParams(IQmcSampleLedgeQueryParams paras)
        {
            return this.service.GetQmcSampleLedgerInfoQueryByParams(paras);
        }

        /// <summary>
        /// 获取检测数据中所有录入人信息
        /// </summary>
        /// <remarks>
        /// 返回列表字段: WorkBarcode(人员编号) UserName(人员名称) 
        /// </remarks>
        /// <returns></returns>
        public DataSet GetAllRecorderInfo()
        {
            return this.service.GetAllRecorderInfo();
        }

        /// <summary>
        /// 获取某个原材料的规格信息
        /// </summary>
        /// <param name="materCode"></param>
        /// <returns></returns>
        public DataSet GetSpecInfoByMaterCode(string materCode)
        {
            return this.service.GetSpecInfoByMaterCode(materCode);
        }

        /// <summary>
        /// 根据执行标准、原材料、检验频次，获取有效的最新的检测指标信息
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public EntityArrayList<QmcCheckItemDetail> GetCheckItemDetailByParams(IQmcCheckDataQueryItemDetailParams paras)
        {
            return this.service.GetCheckItemDetailByParams(paras);
        }
    }
}
