using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtRubWeightSettingService : BaseService<PmtRubWeightSetting>, IPmtRubWeightSettingService
    {
		#region 构造方法

        public PmtRubWeightSettingService() : base(){ }

        public PmtRubWeightSettingService(string connectStringKey) : base(connectStringKey){ }

        public PmtRubWeightSettingService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string equipCode { get; set; }
            public string workshopID { get; set; }
            public string state { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PmtRubWeightSetting> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PmtRubWeightSetting> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtRubWeightSetting> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT rws.ObjID , equip.EquipName as EquipName , equip.EquipCode as EquipCode ,  lvl.ItemName as State , rws.EquipElectricCurrent , 
                                        lvl2.ItemName as WeightSettingCtrl , rws.LockType, rws.DeleteFlag , rws.Remark  , equip.workshopCode
                                 FROM PmtRubWeightSetting rws
                                 LEFT JOIN  BasEquip    equip   on  equip.EquipCode = rws.EquipCode
                                 LEFT JOIN  SysCode     lvl     on  lvl.ItemCode    = rws.State                 AND lvl.TypeID = 'EquipState'
                                 LEFT JOIN  SysCode     lvl2    on  lvl2.ItemCode   = rws.WeightSettingCtrl     AND lvl2.TypeID = 'WeightCtrl'
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND rws.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.workshopID))
            {
                sqlstr.AppendLine(" AND equip.workshopCode = '" + queryParams.workshopID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.state))
            {
                sqlstr.AppendLine(" AND  rws.State = '" + queryParams.state + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND rws.EquipCode like '%" + queryParams.equipCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND  rws.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND  rws.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

    }
}
