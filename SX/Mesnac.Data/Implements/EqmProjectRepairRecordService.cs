using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using NBear.Common;
    public class EqmProjectRepairRecordService : BaseService<EqmProjectRepairRecord>, IEqmProjectRepairRecordService
    {
		#region 构造方法

        public EqmProjectRepairRecordService() : base(){ }

        public EqmProjectRepairRecordService(string connectStringKey) : base(connectStringKey){ }

        public EqmProjectRepairRecordService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法


        public class QueryParams
        {
            public string objID { get; set; }
            public string repairStartDate { get; set; }
            public string repairEndDate { get; set; }
            public string equipCode { get; set; }
            public string shiftID { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmProjectRepairRecord> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<EqmProjectRepairRecord> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmProjectRepairRecord> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT	    a.ObjID, a.MainDailyID, d.EquipName as EquipCode, b.ShiftName as ShiftID, a.RepairDate, a.RepairStartDate, 
		                                    a.RepairEndDate, a.RepairSpendTime, f.UserName as RepairUser, a.RepairType, c.PartName as RepairPart, 
		                                    a.FaultDetail, a.RepairResult, a.RecordDate, e.UserName as RecordUser 
                                FROM        EqmProjectRepairRecord a
                                LEFT JOIN   PptShift            b       ON  a.ShiftID       =   b.ObjID
                                LEFT JOIN   BasEquipPartInfo    c       ON  a.RepairPart    =   c.ObjID
                                LEFT JOIN   BasEquip            d       ON  a.EquipCode     =   d.EquipCode
                                LEFT JOIN   BasUser             e       ON  a.RecordUser    =   e.WorkBarCode
                                LEFT JOIN   BasUser             f       ON  a.RepairUser    =   f.WorkBarCode
                                WHERE   1=1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND a.ObjID = " + queryParams.objID);
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.repairStartDate))
                {
                    sqlstr.AppendLine(" AND a.RepairDate >=  '" + queryParams.repairStartDate + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.repairEndDate))
                {
                    sqlstr.AppendLine(" AND a.RepairDate >=  '" + queryParams.repairEndDate + "'");
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND a.EquipCode ='" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND a.ShiftID ='" + queryParams.shiftID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND a.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        /// <summary>
        /// 获取下一个主键值
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNextPrimaryKeyValue()
        {
            EntityArrayList<EqmProjectRepairRecord> list = this.GetAllListOrder(EqmProjectRepairRecord._.ObjID.Desc);
            if (list.Count == 0)
            {
                return 1;
            }
            else
            {
                return list[0].ObjID + 1;
            }
        }

        /// <summary>
        /// 获取下一个主键值
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNextMainDailyID(string daily)
        {
            EntityArrayList<EqmProjectRepairRecord> list = this.GetListByWhere(EqmProjectRepairRecord._.MainDailyID.Like( daily + "%"));
            if (list.Count == 0)
            {
                return daily + "01";
            }
            else
            {
                return (Convert.ToInt32(list[0].MainDailyID) + 1).ToString();
            }
        }
    }
}
