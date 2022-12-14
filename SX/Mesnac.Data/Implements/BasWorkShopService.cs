using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
using NBear.Common;
    using System.Data;
    public class BasWorkShopService : BaseService<BasWorkShop>, IBasWorkShopService
    {
		#region 构造方法

        public BasWorkShopService() : base(){ }

        public BasWorkShopService(string connectStringKey) : base(connectStringKey){ }

        public BasWorkShopService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string workshopName { get; set; }
            public string isInnerGroup { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasWorkShop> pageParams { get; set; }
        }

        public PageResult<BasWorkShop> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasWorkShop> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT shop.ObjID , shop.WorkShopName , '' as IsInnerGroup , shop.Remark , shop.DeleteFlag  
                                 FROM BasWorkShop shop
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.workshopName))
            {
                sqlstr.AppendLine(" AND WorkShopName like '%" + queryParams.workshopName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.isInnerGroup))
            {
                sqlstr.AppendLine(" AND IsInnerGroup like '%" + queryParams.isInnerGroup + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND DeleteFlag ='" + queryParams.deleteFlag + "'");
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
        public string GetNextWorkShopCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasWorkShop ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(10, '0');
        }

        public EntityArrayList<BasWorkShop> getAllMiLanWorkShop()
        {
            EntityArrayList<BasWorkShop> workshopList = new EntityArrayList<BasWorkShop>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT	a.WorkShopCode , b.WorkShopName , FactoryID , IsInnerGroup , b.Remark , b.DeleteFlag
                                    FROM	BasEquip a  left join BasWorkShop b on a.WorkShopCode = b.ObjID 
                                    WHERE	EquipType = '01'  group by a.WorkShopCode,b.WorkShopName,FactoryID , IsInnerGroup , b.Remark , b.DeleteFlag; ");
            workshopList = this.GetBySql(sqlstr.ToString()).ToArrayList<BasWorkShop>();
            return workshopList;
        }

        public DataSet getAllMiLanWorkShopNode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT	a.WorkShopCode AS NodeId , b.WorkShopName AS ShowName 
                                    FROM	BasEquip a  LEFT JOIN BasWorkShop b on a.WorkShopCode = b.ObjID 
                                    WHERE	EquipType = '01'  group by a.WorkShopCode,b.WorkShopName;   ");
           return this.GetBySql(sqlstr.ToString()).ToDataSet();
        }
    }
}
