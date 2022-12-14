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

    public class QmtCheckStandEquipGradeManager : BaseManager<QmtCheckStandEquipGrade>, IQmtCheckStandEquipGradeManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckStandEquipGradeService service;

        public QmtCheckStandEquipGradeManager()
        {
            this.service = new QmtCheckStandEquipGradeService();
            base.BaseService = this.service;
        }

		public QmtCheckStandEquipGradeManager(string connectStringKey)
        {
			this.service = new QmtCheckStandEquipGradeService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckStandEquipGradeManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckStandEquipGradeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas(IQmtCheckStandEquipGradeParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteWithLogic(QmtCheckStandEquipGrade entity)
        {
            entity.DeleteFlag = "1";
            this.Update(entity);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new int Insert(QmtCheckStandEquipGrade entity)
        {
            EntityArrayList<QmtCheckStandEquipGrade> entityList = this.service.GetListByWhereAndOrder(QmtCheckStandEquipGrade._.StandId == entity.StandId
                && QmtCheckStandEquipGrade._.ItemCd == entity.ItemCd
                , QmtCheckStandEquipGrade._.WeightId.Desc);
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
