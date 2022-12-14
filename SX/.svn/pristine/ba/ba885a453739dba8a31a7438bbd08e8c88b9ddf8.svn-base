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
    public class QmtCheckStandDetailManager : BaseManager<QmtCheckStandDetail>, IQmtCheckStandDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckStandDetailService service;

        public QmtCheckStandDetailManager()
        {
            this.service = new QmtCheckStandDetailService();
            base.BaseService = this.service;
        }

		public QmtCheckStandDetailManager(string connectStringKey)
        {
			this.service = new QmtCheckStandDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckStandDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckStandDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas(IQmtCheckStandDetailParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        public new int Insert(QmtCheckStandDetail entity)
        {
            EntityArrayList<QmtCheckStandDetail> entityList = this.service.GetListByWhereAndOrder(QmtCheckStandDetail._.StandId == entity.StandId
                && QmtCheckStandDetail._.ItemCd == entity.ItemCd
                , QmtCheckStandDetail._.WeightId.Desc);
            if (entityList.Count == 0)
            {
                entity.WeightId = 1;
            }
            else
            {
                entity.WeightId = entityList[0].WeightId + 1;
            }

            return base.Insert(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteWithLogic(QmtCheckStandDetail entity)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                EntityArrayList<QmtCheckStandDetail> entityList = this.service.GetListByWhere(
                    QmtCheckStandDetail._.DeleteFlag == "0"
                    & QmtCheckStandDetail._.StandId == entity.StandId
                    & QmtCheckStandDetail._.ItemCd == entity.ItemCd
                    & QmtCheckStandDetail._.WeightId != entity.WeightId);

                if (entityList.Count == 0)
                {
                    IQmtCheckStandGradeService dQmtCheckStandGradeService = new QmtCheckStandGradeService();
                    dQmtCheckStandGradeService.Update(new PropertyItem[] { QmtCheckStandGrade._.DeleteFlag }
                        , new object[] { "1" }
                        , QmtCheckStandGrade._.DeleteFlag == "0"
                        & QmtCheckStandGrade._.StandId == entity.StandId 
                        & QmtCheckStandGrade._.ItemCd == entity.ItemCd);

                }

                entity.DeleteFlag = "1";
                this.Update(entity);

                scope.Complete();
                scope.Dispose();
            }
        }
    }
}
