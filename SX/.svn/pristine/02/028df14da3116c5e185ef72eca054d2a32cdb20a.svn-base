using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstmmshopoutService : BaseService<Pstmmshopout>, IPstmmshopoutService
    {
		#region 构造方法

        public PstmmshopoutService() : base(){ }

        public PstmmshopoutService(string connectStringKey) : base(connectStringKey){ }

        public PstmmshopoutService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string workShopCode { get; set; }
            public string equipCode { get; set; }
            public string shiftID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string materType { get; set; }
            public string materCode { get; set; }
            public PageResult<Pstmmshopout> pageParams { get; set; }
        }

        public PageResult<Pstmmshopout> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<Pstmmshopout> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select 'M'+convert(varchar, H.WorkShopCode)+'车间' WorkShopCode, ShopOutid, PlanDate, CostCode, E.MaterialName CostMaterName, MaterCode, B.MaterialName, A.EquipCode, C.EquipName, F.ShiftName+'/'+G.ClassName Shift, ConsumeQty, BalanceQty,
                                    ConsQty, ConsQty - ConsumeQty DiffWeight,SurPlus,HandFlag,ConsRate=(SurPlus/(case ConsumeQty when 0 then 999999 else ConsumeQty end))*100,
                                    Mater_Kind=substring(MaterCode,2,2), D.MinorTypeName, RealNum, RecordDate
                                from Pstmmshopout A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join BasMaterialMinorType D on D.MajorID = '1' and substring(MaterCode,2,2) = D.MinorTypeID
                                left join BasMaterial E on A.CostCode = E.MaterialCode
                                left join PptShift F on A.ShiftID = F.ObjID
                                left join PptClass G on A.ShiftClassID = G.ObjID
                                left join BasEquip H on A.EquipCode = H.EquipCode
                                where A.DeleteFlag = '0'");
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
            {
                sqlstr.AppendLine(" AND H.WorkShopCode = '" + queryParams.workShopCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materType))
            {
                sqlstr.AppendLine(" AND substring(MaterCode,2,2) = '" + queryParams.materType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
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

        public DataSet GetShopConsumeTotal(string beginDate, string endDate, string materType,string classid,string equipcode)
        {
            string sql = @"SELECT b.EquipGroup, MaterialName,a.MaterCode, SUM(BalanceQty) TotalWeight
                            FROM Pstmmshopout a   
                            LEFT JOIN BasEquip b ON  a.EquipCode=b.EquipCode  
                            LEFT JOIN BasDept c ON b.SubFac=c.DepCode          
                            LEFT JOIN BasMaterial d ON a.MaterCode=d.MaterialCode
                            WHERE 1 = 1 "; 
                            //WHERE RecordDate >= '2013-05-01' AND RecordDate <= '2013-06-13' 
                            //AND EXISTS(select 1 from BasMaterial where MaterCode=a.MaterCode and MajorTypeID in ('01','02','03')) 
                            //GROUP BY EquipGroup, MaterialName,a.MaterCode";
            if (!string.IsNullOrEmpty(beginDate))
                sql += " AND PlanDate >= '" + beginDate + "'";
            if (!string.IsNullOrEmpty(endDate))
                sql += " AND PlanDate <= '" + endDate + "'";
            if (!string.IsNullOrEmpty(classid)&&classid!="all")
            {
                sql += " AND a.ShiftID='"+classid+"'";
            }
            if (!string.IsNullOrEmpty(equipcode))
            {
                sql += " AND a.EquipCode = '"+equipcode+"'";
            }
            if (!string.IsNullOrEmpty(materType))
            {
                if (materType == "1")
                    sql += " AND EXISTS(select 1 from BasMaterial where MaterialCode=a.MaterCode and MinorTypeID in ('01','02','03'))";
                if (materType == "2")
                    sql += " AND EXISTS(select 1 from BasMaterial where MaterialCode=a.MaterCode and MinorTypeID in ('04','05'))";
                if (materType == "3")
                    sql += " AND EXISTS(select 1 from BasMaterial where MaterialCode=a.MaterCode and MinorTypeID in ('06'))";
                if (materType == "4")
                    sql += " AND EXISTS(select 1 from BasMaterial where MaterialCode=a.MaterCode and MinorTypeID in ('07'))";
                if (materType == "5")
                    sql += " AND EXISTS(select 1 from BasMaterial where MaterialCode=a.MaterCode and MinorTypeID in ('08'))";
            }

            sql += " GROUP BY EquipGroup, MaterialName,a.MaterCode";

            DataSet ds = this.GetBySql(sql).ToDataSet();
            return ds;
        }
    }
}
