using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class TblRecipeService : BaseService<TblRecipe>, ITblRecipeService
    {
		#region 构造方法

        public TblRecipeService() : base(){ }

        public TblRecipeService(string connectStringKey) : base(connectStringKey){ }

        public TblRecipeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string Mater_name { get; set; }
            public string Equip_Name { get; set; }
            public string Mater_code{ get; set; }
            public string Equip_code { get; set; }
            public string Edt_code { get; set; }
            public string Modify_Flag { get; set; }
            public string Modify_Flag1 { get; set; }
            public string Recipe_type { get; set; }
            public string Routing_type { get; set; }
            public PageResult<TblRecipe> pageParams { get; set; }
        }

        public PageResult<TblRecipe> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<TblRecipe> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" select  tr.Mater_Code,tm.Mater_Name ,be.EquipName ,case when tr.Modify_Flag=1  then '新配方' when  tr.Modify_Flag=5 then '作废配方' end as Modify_Flag,

   tr.Edt_code,tr.Recipe_type, SysCode.ItemName as Recipe_typeName
from TblRecipe tr
            join TblMater tm on tr.Mater_code=tm.Mater_Code join BasEquip be on tr.Equip_code=be.EquipCode
            left join SysCode on SysCode.TypeID='PmtTypeNew' and Recipe_type=SysCode.ItemCode
             WHERE 1=1  ");
            if (!string.IsNullOrEmpty(queryParams.Mater_code))
            {
                sqlstr.AppendLine(" AND tr.Mater_code = "+ queryParams.Mater_code);
            }
            if (!string.IsNullOrEmpty(queryParams.Equip_code))
            {
                sqlstr.AppendLine(" AND tr.Equip_code like '%"+ queryParams.Equip_code + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.Edt_code))
            {
                sqlstr.AppendLine(" AND tr.Edt_code ="+ queryParams.Edt_code);
            }
            if (!string.IsNullOrEmpty(queryParams.Mater_name))
            {
                sqlstr.AppendLine(" AND tm.Mater_name like '%"+ queryParams.Mater_name+"%'");
            }
            if (!string.IsNullOrEmpty(queryParams.Equip_Name))
            {
                sqlstr.AppendLine(" AND be.EquipName like '%"+ queryParams.Equip_Name + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.Modify_Flag) && !string.IsNullOrEmpty(queryParams.Modify_Flag1))
            {
                sqlstr.AppendLine(" AND tr.Modify_Flag like '%" + queryParams.Modify_Flag + "%' or  tr.Modify_Flag like '%" + queryParams.Modify_Flag1 + "%' ");
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

        public string GetNextRubInfoCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(RubCode) + 1 as RubCode From BasRubInfo ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(4, '0');
        }
    }
}
