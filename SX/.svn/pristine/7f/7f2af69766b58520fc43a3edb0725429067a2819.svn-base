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
    public class QmtCheckStandMasterManager : BaseManager<QmtCheckStandMaster>, IQmtCheckStandMasterManager
    {
        #region 属性注入与构造方法

        private IQmtCheckStandMasterService service;

        public QmtCheckStandMasterManager()
        {
            this.service = new QmtCheckStandMasterService();
            base.BaseService = this.service;
        }

        public QmtCheckStandMasterManager(string connectStringKey)
        {
            this.service = new QmtCheckStandMasterService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckStandMasterManager(NBear.Data.Gateway way)
        {
            this.service = new QmtCheckStandMasterService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas(IQmtCheckStandMasterParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }


        public QmtCheckStandMaster GetById(string id)
        {
            return this.service.GetListByWhere(QmtCheckStandMaster._.StandId==id)[0];
        }

        /// <summary>
        /// 升级
        /// </summary>
        /// <param name="entity"></param>
        public void Upgrade(QmtCheckStandMaster entity)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                this.service.Update(new PropertyItem[] { QmtCheckStandMaster._.StandVisionStat }
                    , new object[] { 0 }
                    , QmtCheckStandMaster._.DeleteFlag == "0"
                    & QmtCheckStandMaster._.StandVisionStat == "1"
                    & QmtCheckStandMaster._.StandCode == entity.StandCode
                    & QmtCheckStandMaster._.MaterCode == entity.MaterCode);
                if (entity.StandId == 0)
                {
                    this.Insert(entity);
                }
                else
                {
                    this.Update(entity);
                }
                scope.Complete();
                scope.Dispose();
            }

        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteWithLogic(QmtCheckStandMaster entity)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                IQmtCheckStandGradeService dQmtCheckStandGradeService = new QmtCheckStandGradeService();
                dQmtCheckStandGradeService.Update(new PropertyItem[] { QmtCheckStandGrade._.DeleteFlag }
                    , new object[] { "1" }
                    , QmtCheckStandGrade._.DeleteFlag == "0"
                    & QmtCheckStandGrade._.StandId == entity.StandId);

                IQmtCheckStandDetailService dQmtCheckStandDetailService = new QmtCheckStandDetailService();
                dQmtCheckStandDetailService.Update(new PropertyItem[] { QmtCheckStandDetail._.DeleteFlag }
                    , new object[] { "1" }
                    , QmtCheckStandDetail._.DeleteFlag == "0"
                    & QmtCheckStandDetail._.StandId == entity.StandId);

                entity.DeleteFlag = "1";
                this.Update(entity);
                scope.Complete();
                scope.Dispose();
            }
        }

        /// <summary>
        /// 插入
        /// 修改标识：qusf 20131008
        /// 修改内容：1.因加入审核功能，版本号要大于0
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private int InsertForImport(QmtCheckStandMaster entity)
        {
            EntityArrayList<QmtCheckStandMaster> entities =
                this.GetListByWhereAndOrder(QmtCheckStandMaster._.StandCode == entity.StandCode
                & QmtCheckStandMaster._.MaterCode == entity.MaterCode
                & QmtCheckStandMaster._.DeleteFlag == "0"
                & QmtCheckStandMaster._.StandVision > 0
                , QmtCheckStandMaster._.StandVision.Desc);
            if (entities.Count > 0)
            {
                entity.StandVision = entities[0].StandVision.Value + 1;
            }
            else
            {
                entity.StandVision = 1;
            }

            return base.Insert(entity);
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="mQmtCheckStandMaster"></param>
        /// <param name="mQmtCheckStandDetailList"></param>
        /// <param name="mQmtCheckStandGradeList"></param>
        /// <param name="mQmtCheckStandEquipList"></param>
        /// <param name="mQmtCheckStandEquipGradeList"></param>
        public void AddCopy(QmtCheckStandMaster mQmtCheckStandMaster, EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList
            , EntityArrayList<QmtCheckStandGrade> mQmtCheckStandGradeList, EntityArrayList<QmtCheckStandEquip> mQmtCheckStandEquipList
            , EntityArrayList<QmtCheckStandEquipGrade> mQmtCheckStandEquipGradeList)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                mQmtCheckStandMaster.Detach();
                int standIdCopyTo = this.Insert(mQmtCheckStandMaster);

                IQmtCheckStandDetailService bQmtCheckStandDetailService = new QmtCheckStandDetailService();
                foreach (QmtCheckStandDetail mQmtCheckStandDetail in mQmtCheckStandDetailList)
                {
                    mQmtCheckStandDetail.Detach();
                    mQmtCheckStandDetail.StandId = standIdCopyTo;
                    bQmtCheckStandDetailService.Insert(mQmtCheckStandDetail);
                }

                IQmtCheckStandGradeService bQmtCheckStandGradeService = new QmtCheckStandGradeService();
                foreach (QmtCheckStandGrade mQmtCheckStandGrade in mQmtCheckStandGradeList)
                {
                    mQmtCheckStandGrade.Detach();
                    mQmtCheckStandGrade.StandId = standIdCopyTo;
                    bQmtCheckStandGradeService.Insert(mQmtCheckStandGrade);
                }

                IQmtCheckStandEquipService bQmtCheckStandEquipService = new QmtCheckStandEquipService();
                foreach (QmtCheckStandEquip mQmtCheckStandEquip in mQmtCheckStandEquipList)
                {
                    mQmtCheckStandEquip.Detach();
                    mQmtCheckStandEquip.StandId = standIdCopyTo;
                    bQmtCheckStandEquipService.Insert(mQmtCheckStandEquip);
                }

                IQmtCheckStandEquipGradeService bQmtCheckStandEquipGradeService = new QmtCheckStandEquipGradeService();
                foreach (QmtCheckStandEquipGrade mQmtCheckStandEquipGrade in mQmtCheckStandEquipGradeList)
                {
                    mQmtCheckStandEquipGrade.Detach();
                    mQmtCheckStandEquipGrade.StandId = standIdCopyTo;
                    bQmtCheckStandEquipGradeService.Insert(mQmtCheckStandEquipGrade);
                }

                scope.Complete();
                scope.Dispose();

            }

        }

        /// <summary>
        /// 拷贝并升级
        /// </summary>
        /// <param name="mQmtCheckStandMaster"></param>
        /// <param name="mQmtCheckStandDetailList"></param>
        /// <param name="mQmtCheckStandGradeList"></param>
        /// <param name="mQmtCheckStandEquipList"></param>
        /// <param name="mQmtCheckStandEquipGradeList"></param>
        public void UpgradeCopy(QmtCheckStandMaster mQmtCheckStandMaster, EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList
            , EntityArrayList<QmtCheckStandGrade> mQmtCheckStandGradeList, EntityArrayList<QmtCheckStandEquip> mQmtCheckStandEquipList
            , EntityArrayList<QmtCheckStandEquipGrade> mQmtCheckStandEquipGradeList)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                this.service.Update(new PropertyItem[] { QmtCheckStandMaster._.StandVisionStat }
                    , new object[] { 0 }
                    , QmtCheckStandMaster._.DeleteFlag == "0"
                    & QmtCheckStandMaster._.StandVisionStat == "1"
                    & QmtCheckStandMaster._.StandCode == mQmtCheckStandMaster.StandCode
                    & QmtCheckStandMaster._.MaterCode == mQmtCheckStandMaster.MaterCode);

                mQmtCheckStandMaster.Detach();
                int standIdCopyTo = this.Insert(mQmtCheckStandMaster);

                IQmtCheckStandDetailService bQmtCheckStandDetailService = new QmtCheckStandDetailService();
                foreach (QmtCheckStandDetail mQmtCheckStandDetail in mQmtCheckStandDetailList)
                {
                    mQmtCheckStandDetail.Detach();
                    mQmtCheckStandDetail.StandId = standIdCopyTo;
                    bQmtCheckStandDetailService.Insert(mQmtCheckStandDetail);
                }

                IQmtCheckStandGradeService bQmtCheckStandGradeService = new QmtCheckStandGradeService();
                foreach (QmtCheckStandGrade mQmtCheckStandGrade in mQmtCheckStandGradeList)
                {
                    mQmtCheckStandGrade.Detach();
                    mQmtCheckStandGrade.StandId = standIdCopyTo;
                    bQmtCheckStandGradeService.Insert(mQmtCheckStandGrade);
                }

                IQmtCheckStandEquipService bQmtCheckStandEquipService = new QmtCheckStandEquipService();
                foreach (QmtCheckStandEquip mQmtCheckStandEquip in mQmtCheckStandEquipList)
                {
                    mQmtCheckStandEquip.Detach();
                    mQmtCheckStandEquip.StandId = standIdCopyTo;
                    bQmtCheckStandEquipService.Insert(mQmtCheckStandEquip);
                }

                IQmtCheckStandEquipGradeService bQmtCheckStandEquipGradeService = new QmtCheckStandEquipGradeService();
                foreach (QmtCheckStandEquipGrade mQmtCheckStandEquipGrade in mQmtCheckStandEquipGradeList)
                {
                    mQmtCheckStandEquipGrade.Detach();
                    mQmtCheckStandEquipGrade.StandId = standIdCopyTo;
                    bQmtCheckStandEquipGradeService.Insert(mQmtCheckStandEquipGrade);
                }

                scope.Complete();
                scope.Dispose();
            }

        }

        /// <summary>
        /// 保存导入的标准
        /// 修改标识：qusf 20131009
        /// 修改内容：1.因增加审核功能，所以增加修改时间、提交及审核信息
        /// </summary>
        /// <param name="mQmtCheckStandMasterList"></param>
        /// <param name="mQmtCheckStandDetailList"></param>
        public void SaveImport(EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList
            , EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList)
        {
            //System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            //transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            //{
            //插入

            IQmtCheckStandDetailService bQmtCheckStandDetailService = new QmtCheckStandDetailService();

            List<QmtCheckStandDetail> detailList = new List<QmtCheckStandDetail>();

            foreach (QmtCheckStandMaster mQmtCheckStandMaster in mQmtCheckStandMasterList)
            {
                int seq = mQmtCheckStandMaster.StandId;
                mQmtCheckStandMaster.Detach();

                mQmtCheckStandMaster.LastModifyTime = DateTime.Now;
                mQmtCheckStandMaster.LastSubmitTime = DateTime.Now;
                mQmtCheckStandMaster.LastSubmitUser = mQmtCheckStandMaster.WorkerBarcode;
                mQmtCheckStandMaster.LastAuditTime = DateTime.Now;
                mQmtCheckStandMaster.LastAuditUser = mQmtCheckStandMaster.WorkerBarcode;
                mQmtCheckStandMaster.LastAuditMemo = "导入";


                int standId = this.InsertForImport(mQmtCheckStandMaster);

                QmtCheckStandDetail[] mQmtCheckStandDetails = mQmtCheckStandDetailList.Filter(QmtCheckStandDetail._.StandId == seq);
                foreach (QmtCheckStandDetail mQmtCheckStandDetail in mQmtCheckStandDetails)
                {
                    //mQmtCheckStandDetailList.Remove(mQmtCheckStandDetail);
                    mQmtCheckStandDetail.StandId = standId;
                    mQmtCheckStandDetail.Detach();
                    //bQmtCheckStandDetailService.Insert(mQmtCheckStandDetail);
                    detailList.Add(mQmtCheckStandDetail);
                }
            }

            bQmtCheckStandDetailService.BatchInsert(detailList);

            if (mQmtCheckStandMasterList.Count > 0)
            {
                string guid = mQmtCheckStandMasterList[0].GUID;
                this.service.UpdateStandVisionStatByGUID(guid);
            }

            //    scope.Complete();
            //    scope.Dispose();

            //}
        }



        /// <summary>
        /// 审核通过
        /// 修改标识：qusf 20140930
        /// 修改说明：1.已删除的记录的已启用标志也会修改为未启用
        /// </summary>
        /// <param name="entity"></param>
        public void Audit(QmtCheckStandMaster entity)
        {
            EntityArrayList<QmtCheckStandMaster> entities =
                this.GetListByWhereAndOrder(QmtCheckStandMaster._.StandCode == entity.StandCode
                & QmtCheckStandMaster._.MaterCode == entity.MaterCode
                & QmtCheckStandMaster._.DeleteFlag == "0"
                & QmtCheckStandMaster._.StandVision > 0
                , QmtCheckStandMaster._.StandVision.Desc);
            if (entities.Count > 0)
            {
                entity.StandVision = entities[0].StandVision.Value + 1;
            }
            else
            {
                entity.StandVision = 1;
            }

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                this.service.Update(new PropertyItem[] { QmtCheckStandMaster._.StandVisionStat }
                    , new object[] { 0 }
                    , QmtCheckStandMaster._.StandVisionStat == "1"
                    & QmtCheckStandMaster._.StandCode == entity.StandCode
                    & QmtCheckStandMaster._.MaterCode == entity.MaterCode);

                this.Update(entity);

                scope.Complete();
                scope.Dispose();
            }

        }

        /// <summary>
        /// 根据查询条件获取标准信息(包含等同物料)
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public DataSet GetCheckStandInfoByParas(IQmtCheckStandMasterQueryInfoParams queryParams)
        {
            return this.service.GetCheckStandInfoByParas(queryParams);
        }
    }
}
