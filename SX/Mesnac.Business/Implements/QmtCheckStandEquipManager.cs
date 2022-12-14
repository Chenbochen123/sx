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

    public class QmtCheckStandEquipManager : BaseManager<QmtCheckStandEquip>, IQmtCheckStandEquipManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckStandEquipService service;

        public QmtCheckStandEquipManager()
        {
            this.service = new QmtCheckStandEquipService();
            base.BaseService = this.service;
        }

		public QmtCheckStandEquipManager(string connectStringKey)
        {
			this.service = new QmtCheckStandEquipService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckStandEquipManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckStandEquipService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas(IQmtCheckStandEquipParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteWithLogic(QmtCheckStandEquip entity)
        {
            entity.DeleteFlag = "1";
            this.Update(entity);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new int Insert(QmtCheckStandEquip entity)
        {
            EntityArrayList<QmtCheckStandEquip> entityList = this.service.GetListByWhereAndOrder(QmtCheckStandEquip._.StandId == entity.StandId
                && QmtCheckStandEquip._.ItemCd == entity.ItemCd
                , QmtCheckStandEquip._.WeightId.Desc);
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
