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
    public class QmtCheckStandGradeManager : BaseManager<QmtCheckStandGrade>, IQmtCheckStandGradeManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckStandGradeService service;

        public QmtCheckStandGradeManager()
        {
            this.service = new QmtCheckStandGradeService();
            base.BaseService = this.service;
        }

		public QmtCheckStandGradeManager(string connectStringKey)
        {
			this.service = new QmtCheckStandGradeService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckStandGradeManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckStandGradeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas(IQmtCheckStandGradeParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteWithLogic(QmtCheckStandGrade entity)
        {
            entity.DeleteFlag = "1";
            this.Update(entity);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new int Insert(QmtCheckStandGrade entity)
        {
            EntityArrayList<QmtCheckStandGrade> entityList = this.service.GetListByWhereAndOrder(QmtCheckStandGrade._.StandId == entity.StandId
                && QmtCheckStandGrade._.ItemCd == entity.ItemCd
                , QmtCheckStandGrade._.WeightId.Desc);
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
    }
}
