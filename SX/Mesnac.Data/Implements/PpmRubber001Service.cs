using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubber001Service : BaseService<PpmRubber001>, IPpmRubber001Service
    {
		#region 构造方法

        public PpmRubber001Service() : base(){ }

        public PpmRubber001Service(string connectStringKey) : base(connectStringKey){ }

        public PpmRubber001Service(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string chejianCode { get; set; }
            public string planDate { get; set; }
            public string shiftID { get; set; }
            public string stockType { get; set; }
            public string equipCode { get; set; }
            public string materCode { get; set; }
            public string objID { get; set; }
            public PageResult<PpmRubber001> pageParams { get; set; }
        }

        public PageResult<PpmRubber001> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubber001> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.StorageID, B.StorageName, B.ERPCode, A.Barcode, CONVERT(varchar(10), PlanDate, 120) PlanDate, 
	                                A.ShiftID, C.ShiftName, A.EquipCode, D.EquipName, A.MaterCode, E.MaterialName, A.ShiftNum, 
	                                A.TotalWeight, A.OperPerson, F.UserName, A.SAPVersionID, E.SAPMaterialShortCode, A.StockType
                                from PpmRubber001 A
	                                left join BasStorage B on A.StorageID = B.StorageID
	                                left join PptShift C on A.ShiftID = C.ObjID
	                                left join BasEquip D on A.EquipCode = D.EquipCode
	                                left join BasMaterial E on A.MaterCode = E.MaterialCode
                                    left join BasUser F on A.OperPerson = F.HRCode
                                where RubberType = 'ZL'");
            if (queryParams.chejianCode != "all")
                sqlstr.AppendLine(" AND D.WorkShopCode = '" + queryParams.chejianCode + "'");

            sqlstr.AppendLine(" AND A.PlanDate = '" + queryParams.planDate + "'");
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            sqlstr.AppendLine(" AND A.StockType = '" + queryParams.stockType + "'");

            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }

            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND A.ObjID = '" + queryParams.objID + "'");
            }

            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
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

        public DataSet GetCondition(string ObjID)
        {
            string sql = @"select B.WorkShopCode, CONVERT(varchar(10), A.PlanDate, 120) PlanDate, A.ShiftID, A.StockType
                            from PpmRubber001 A
	                            left join BasEquip B on A.EquipCode = B.EquipCode
                            where A.ObjID = '" + ObjID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
