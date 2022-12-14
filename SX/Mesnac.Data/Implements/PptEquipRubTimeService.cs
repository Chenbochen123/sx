using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PptEquipRubTimeService : BaseService<PptEquipRubTime>, IPptEquipRubTimeService
    {
		#region 构造方法

        public PptEquipRubTimeService() : base(){ }

        public PptEquipRubTimeService(string connectStringKey) : base(connectStringKey){ }

        public PptEquipRubTimeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetEquipRubTime(string beginTime, string endTime, string materCode, string workShopCode)
        {
            string sql = @"select PlanDate, B.EquipName, A.MaterCode, C.MaterialName, SUM(SerialCount) TotalCount, SUM(DoneAllRTime) TotalTime
                            from PptEquipRubTime A
	                            left join BasEquip B on A.EquipCode = B.EquipCode
	                            left join BasMaterial C on A.MaterCode = C.MaterialCode
                            where PlanDate >= '" + beginTime + "' and PlanDate <= '" + endTime + "'";
            if (!string.IsNullOrEmpty(materCode))
                sql += " and MaterCode = '" + materCode + "'";
            if (workShopCode != "all")
                sql += " and B.WorkShopCode = '" + workShopCode + "'";
            sql += " group by PlanDate, B.EquipName, A.MaterCode, C.MaterialName";
            
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
