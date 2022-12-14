using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Business.Interface;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Components;
    using Mesnac.Entity;

    using NBear.Common;

    public class QmtQrigMasterManager : BaseManager<QmtQrigMaster>, IQmtQrigMasterManager
    {
		#region 属性注入与构造方法
		
        private IQmtQrigMasterService service;

        public QmtQrigMasterManager()
        {
            this.service = new QmtQrigMasterService();
            base.BaseService = this.service;
        }

		public QmtQrigMasterManager(string connectStringKey)
        {
			this.service = new QmtQrigMasterService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtQrigMasterManager(NBear.Data.Gateway way)
        {
			this.service = new QmtQrigMasterService(way);
            base.BaseService = this.service;
        }

        #endregion

        public PageResult<QmtQrigMaster> GetDataByQueryParams(IQmtQrigMasterQueryParams queryParams)
        {
            return this.service.GetDataByQueryParams(queryParams);
        }

        public PageResult<QmtQrigMaster> GetDetailDataByQueryParams(IQmtQrigMasterQueryParams queryParams)
        {
            return this.service.GetDetailDataByQueryParams(queryParams);
        }

        /// <summary>
        /// 修改标识：qusf 20131023
        /// 修改内容：1.增加对车次的更新处理
        /// </summary>
        /// <param name="mQmtQrigMasterList"></param>
        /// <param name="mQmtQrigDetailList"></param>
        public void SaveImport(EntityArrayList<QmtQrigMaster> mQmtQrigMasterList, EntityArrayList<QmtQrigDetail> mQmtQrigDetailList)
        {
            if (mQmtQrigMasterList.Count == 0)
            {
                return;
            }

           // string guid = mQmtQrigMasterList[0].GUID;

            System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            {
                //插入

                IQmtQrigDetailService bQmtQrigDetailService = new QmtQrigDetailService();

                List<QmtQrigMaster> masterList = new List<QmtQrigMaster>();
                List<QmtQrigDetail> detailList = new List<QmtQrigDetail>();

                foreach (QmtQrigMaster mQmtQrigMaster in mQmtQrigMasterList)
                {
                    mQmtQrigMaster.Detach();
                    masterList.Add(mQmtQrigMaster);
                }
                this.BatchInsert(masterList);

                //mQmtQrigMasterList = this.GetListByWhereAndOrder(QmtQrigMaster._.GUID == guid
                //    , QmtQrigMaster._.SeqNo.Asc);

                //foreach (QmtQrigMaster mQmtQrigMaster in mQmtQrigMasterList)
                //{
                //    string importRelativeId = mQmtQrigMaster.ImportRelativeId;

                //    QmtQrigDetail[] mQmtQrigDetails = mQmtQrigDetailList.Filter(QmtQrigDetail._.ImportRelativeId == importRelativeId);

                //    foreach (QmtQrigDetail mQmtQrigDetail in mQmtQrigDetails)
                //    {
                //        //mQmtQrigDetailList.Remove(mQmtQrigDetail);
                //        mQmtQrigDetail.SeqNo = mQmtQrigMaster.SeqNo;
                //        mQmtQrigDetail.Detach();
                //        //bQmtQrigDetailService.Insert(mQmtQrigDetail);
                //        detailList.Add(mQmtQrigDetail);
                //    }
                //}

                bQmtQrigDetailService.BatchInsert(detailList);

                //对车次的更新处理
                //this.service.UpdateSerialIdByLLSerialId(guid);

                //IQmtQrigImportLogMasterService bQmtQrigImportLogMasterService = new QmtQrigImportLogMasterService();
                //EntityArrayList<QmtQrigImportLogMaster> mQmtQrigImportLogMasterList = bQmtQrigImportLogMasterService.GetListByWhere(
                //    QmtQrigImportLogMaster._.GUID == guid);

                //if (mQmtQrigImportLogMasterList.Count > 0)
                //{
                //    QmtQrigImportLogMaster mQmtQrigImportLogMaster = mQmtQrigImportLogMasterList[0];
                //    mQmtQrigImportLogMaster.Flag = "1";
                    
                //    bQmtQrigImportLogMasterService.Update(mQmtQrigImportLogMaster);
                //}

                scope.Complete();
                scope.Dispose();

            }

        }

        /// <summary>
        /// 修改标识：qusf 20131023
        /// 修改内容：1.增加对车次的更新处理
        /// </summary>
        /// <param name="mQmtQrigMasterList"></param>
        /// <param name="mQmtQrigDetailList"></param>
        public void SaveImport_ReCheck(EntityArrayList<QmtQrigMaster> mQmtQrigMasterList, EntityArrayList<QmtQrigDetail> mQmtQrigDetailList)
        {
            if (mQmtQrigMasterList.Count == 0)
            {
                return;
            }

//string guid = mQmtQrigMasterList[0].GUID;

            System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            {
                //插入

                IQmtQrigDetailService bQmtQrigDetailService = new QmtQrigDetailService();

                List<QmtQrigMaster> masterList = new List<QmtQrigMaster>();
                List<QmtQrigDetail> detailList = new List<QmtQrigDetail>();

                foreach (QmtQrigMaster mQmtQrigMaster in mQmtQrigMasterList)
                {
                    mQmtQrigMaster.Detach();
                    masterList.Add(mQmtQrigMaster);

                    //    EntityArrayList<QmtQrigMaster> tempQmtQrigMasterList = this.service.GetListByWhereAndOrder(QmtQrigMaster._.PlanDate == mQmtQrigMaster.PlanDate
                    //        & QmtQrigMaster._.EquipCode == mQmtQrigMaster.EquipCode
                    //        & QmtQrigMaster._.ShiftId == mQmtQrigMaster.ShiftId
                    //        & QmtQrigMaster._.MaterCode == mQmtQrigMaster.MaterCode
                    //        & QmtQrigMaster._.SerialId == mQmtQrigMaster.SerialId
                    //        & QmtQrigMaster._.ImportRelativeId == mQmtQrigMaster.ImportRelativeId
                    //        , QmtQrigMaster._.CheckNum.Desc);

                    //    if (tempQmtQrigMasterList.Count == 0)
                    //    {
                    //        mQmtQrigMaster.CheckNum = 2;
                    //    }
                    //    else
                    //    {
                    //        mQmtQrigMaster.CheckNum = tempQmtQrigMasterList[0].CheckNum + 1;
                    //    }
                    //    this.Insert(mQmtQrigMaster);
                    //}
                    ////this.BatchInsert(masterList);

                    //mQmtQrigMasterList = this.GetListByWhereAndOrder(QmtQrigMaster._.GUID == guid
                    //    , QmtQrigMaster._.SeqNo.Asc);

                    //foreach (QmtQrigMaster mQmtQrigMaster in mQmtQrigMasterList)
                    //{
                    //    string importRelativeId = mQmtQrigMaster.ImportRelativeId;

                    //    QmtQrigDetail[] mQmtQrigDetails = mQmtQrigDetailList.Filter(QmtQrigDetail._.ImportRelativeId == importRelativeId);

                    //    foreach (QmtQrigDetail mQmtQrigDetail in mQmtQrigDetails)
                    //    {
                    //        //mQmtQrigDetailList.Remove(mQmtQrigDetail);
                    //        mQmtQrigDetail.SeqNo = mQmtQrigMaster.SeqNo;
                    //        mQmtQrigDetail.Detach();
                    //        //bQmtQrigDetailService.Insert(mQmtQrigDetail);
                    //        detailList.Add(mQmtQrigDetail);
                    //    }
                    //}

                    bQmtQrigDetailService.BatchInsert(detailList);

                    //对车次的更新处理
                    //this.service.UpdateSerialIdByLLSerialId(guid);

                    //IQmtQrigImportLogMasterService bQmtQrigImportLogMasterService = new QmtQrigImportLogMasterService();
                    //EntityArrayList<QmtQrigImportLogMaster> mQmtQrigImportLogMasterList = bQmtQrigImportLogMasterService.GetListByWhere(
                    //    QmtQrigImportLogMaster._.GUID == guid);

                    //if (mQmtQrigImportLogMasterList.Count > 0)
                    //{
                    //    QmtQrigImportLogMaster mQmtQrigImportLogMaster = mQmtQrigImportLogMasterList[0];
                    //    mQmtQrigImportLogMaster.Flag = "1";

                    //    bQmtQrigImportLogMasterService.Update(mQmtQrigImportLogMaster);
                    //}

                    scope.Complete();
                    scope.Dispose();

                }

            }
        }

        /// <summary>
        /// 逻辑删除
        /// 创建标识：qusf 20131112
        /// </summary>
        /// <param name="entity"></param>
        public void LogicDelete(QmtQrigMaster origin)
        {
            // 启用事务
            System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            {
                // 更新相关的质检数据 

                // 逻辑删除当前数据
                origin.DeleteFlag = "1";
                this.service.Update(origin);

                // 更新相关质检数据的NeedReJudgeGrade
                //if (origin.StandCode.Value == 1 || origin.StandCode.Value == 2)
                //{
                //    this.service.Update(new PropertyItem[] { QmtQrigMaster._.NeedReJudgeGrade }, new object[] { "1" }
                //    , QmtQrigMaster._.PlanDate == origin.PlanDate
                //    & QmtQrigMaster._.ShiftId == origin.ShiftId
                //    & QmtQrigMaster._.MaterCode == origin.MaterCode
                //    & QmtQrigMaster._.EquipCode == origin.EquipCode
                //    & QmtQrigMaster._.ZJSID == origin.ZJSID
                //    & QmtQrigMaster._.StandCode.In(new object[] { 1, 2 })
                //    & QmtQrigMaster._.DeleteFlag == "0");
                //}
                //else
                //{
                //    this.service.Update(new PropertyItem[] { QmtQrigMaster._.NeedReJudgeGrade }, new object[] { "1" }
                //    , QmtQrigMaster._.PlanDate == origin.PlanDate
                //    & QmtQrigMaster._.ShiftId == origin.ShiftId
                //    & QmtQrigMaster._.MaterCode == origin.MaterCode
                //    & QmtQrigMaster._.EquipCode == origin.EquipCode
                //    & QmtQrigMaster._.ZJSID == origin.ZJSID
                //    & QmtQrigMaster._.StandCode == origin.StandCode.Value
                //    & QmtQrigMaster._.DeleteFlag == "0");
                //}

                scope.Complete();
                scope.Dispose();
            }

        }

        /// <summary>
        /// 逻辑更新
        /// 创建标识：qusf 20131112
        /// </summary>
        /// <param name="entity"></param>
        public void LogicUpdate(QmtQrigMaster entity)
        {
            // 查找原数据记录
            QmtQrigMaster origin = this.service.GetById(entity.SeqNo);

            // 查找判级记录
            IQmtCheckMasterService dQmtCheckMasterService = new QmtCheckMasterService();

            EntityArrayList<QmtCheckMaster> mQmtCheckMasterList = null;

            //if (origin.StandCode.Value == 1 || origin.StandCode.Value == 2)
            //{
            //    mQmtCheckMasterList = dQmtCheckMasterService.GetListByWhereAndOrder(
            //        QmtCheckMaster._.PlanDate == origin.PlanDate
            //        & QmtCheckMaster._.MaterCode == origin.MaterCode
            //        & QmtCheckMaster._.EquipCode == origin.EquipCode
            //        & QmtCheckMaster._.ShiftId == origin.ShiftId.Value
            //        & QmtCheckMaster._.ZJSID == origin.ZJSID
            //        & QmtCheckMaster._.StandCode.In(new object[] { 1, 2 })
            //        , QmtCheckMaster._.CheckCode.Asc);

            //}
            //else
            //{
            //    mQmtCheckMasterList = dQmtCheckMasterService.GetListByWhereAndOrder(
            //        QmtCheckMaster._.PlanDate == origin.PlanDate
            //        & QmtCheckMaster._.MaterCode == origin.MaterCode
            //        & QmtCheckMaster._.EquipCode == origin.EquipCode
            //        & QmtCheckMaster._.ShiftId == origin.ShiftId.Value
            //        & QmtCheckMaster._.ZJSID == origin.ZJSID
            //        & QmtCheckMaster._.StandCode == origin.StandCode.Value
            //        , QmtCheckMaster._.CheckCode.Asc);

            //}

            System.Collections.Specialized.StringCollection checkCodeCollection = new System.Collections.Specialized.StringCollection();
            foreach (QmtCheckMaster mQmtCheckMaster in mQmtCheckMasterList)
            {
                checkCodeCollection.Add(mQmtCheckMaster.CheckCode);
            }
            string[] checkCodes = new string[checkCodeCollection.Count];
            checkCodeCollection.CopyTo(checkCodes, 0);

            // 启用事务
            System.Transactions.TransactionOptions transOptions = new System.Transactions.TransactionOptions();
            transOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transOptions))
            {
                // 删除相关的判级数据

                if (checkCodes.Length > 0)
                {
                    // 删除考核明细
                    IQmtCheckAssessDetailService dQmtCheckAssessDetailService = new QmtCheckAssessDetailService();
                    dQmtCheckAssessDetailService.DeleteByWhere(QmtCheckAssessDetail._.CheckCode.In(checkCodes));

                    // 删除考核车次
                    IQmtCheckAssessLotService dQmtCheckAssessLotService = new QmtCheckAssessLotService();
                    dQmtCheckAssessLotService.DeleteByWhere(QmtCheckAssessLot._.CheckCode.In(checkCodes));

                    // 删除考核记录
                    IQmtCheckAssessMasterService dQmtCheckAssessMasterService = new QmtCheckAssessMasterService();
                    dQmtCheckAssessMasterService.DeleteByWhere(QmtCheckAssessMaster._.CheckCode.In(checkCodes));

                    // 删除判级明细
                    IQmtCheckDetailService dQmtCheckDetailService = new QmtCheckDetailService();
                    dQmtCheckDetailService.DeleteByWhere(QmtCheckDetail._.CheckCode.In(checkCodes));

                    // 删除判级车次
                    IQmtCheckLotService dQmtCheckLotService = new QmtCheckLotService();
                    dQmtCheckLotService.DeleteByWhere(QmtCheckLot._.CheckCode.In(checkCodes));

                    // 删除判级记录
                    dQmtCheckMasterService.DeleteByWhere(QmtCheckMaster._.CheckCode.In(checkCodes));

                }
                // 更新相关的质检数据 

                // 更新当前数据
                //entity.NeedReJudgeGrade = "1";
                //this.service.Update(entity);

                //// 更新相关质检数据的NeedReJudgeGrade
                //if (origin.StandCode.Value == 1 || origin.StandCode.Value == 2)
                //{
                //    this.service.Update(new PropertyItem[] { QmtQrigMaster._.NeedReJudgeGrade }, new object[] { "1" }
                //    , QmtQrigMaster._.PlanDate == origin.PlanDate
                //    & QmtQrigMaster._.ShiftId == origin.ShiftId
                //    & QmtQrigMaster._.MaterCode == origin.MaterCode
                //    & QmtQrigMaster._.EquipCode == origin.EquipCode
                //    & QmtQrigMaster._.ZJSID == origin.ZJSID
                //    & QmtQrigMaster._.StandCode.In(new object[] { 1, 2 })
                //    & QmtQrigMaster._.DeleteFlag == "0");
                //}
                //else
                //{
                //    this.service.Update(new PropertyItem[] { QmtQrigMaster._.NeedReJudgeGrade }, new object[] { "1" }
                //    , QmtQrigMaster._.PlanDate == origin.PlanDate
                //    & QmtQrigMaster._.ShiftId == origin.ShiftId
                //    & QmtQrigMaster._.MaterCode == origin.MaterCode
                //    & QmtQrigMaster._.EquipCode == origin.EquipCode
                //    & QmtQrigMaster._.ZJSID == origin.ZJSID
                //    & QmtQrigMaster._.StandCode == origin.StandCode.Value
                //    & QmtQrigMaster._.DeleteFlag == "0");
                //}

                scope.Complete();
                scope.Dispose();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paras"></param>
        public DataSet StaticsQrigProductionAmount(IQmtQrigMasterStaticProdAmountParams paras)
        {
            return this.service.StaticsQrigProductionAmount(paras);
        }
    }
}
