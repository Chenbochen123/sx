using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasEquipPartInfoService : BaseService<BasEquipPartInfo>, IBasEquipPartInfoService
    {
		#region 构造方法

        public BasEquipPartInfoService() : base(){ }

        public BasEquipPartInfoService(string connectStringKey) : base(connectStringKey){ }

        public BasEquipPartInfoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string partName { get; set; }
            public string equipType { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasEquipPartInfo> pageParams { get; set; }
        }

        public PageResult<BasEquipPartInfo> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasEquipPartInfo> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    partinfo.ObjID, partinfo.PartName, equiptype.EquipTypeName AS EquipType, 
                                            partinfo.Remark, partinfo.DeleteFlag
                                 FROM	    BasEquipPartInfo partinfo  
                                 LEFT JOIN  BasEquipType equiptype  on partinfo.EquipType = equiptype.ObjID
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND partinfo.ObjID like '%" + queryParams.objID + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.partName))
            {
                sqlstr.AppendLine(" AND partinfo.PartName like '%" + queryParams.partName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipType))
            {
                sqlstr.AppendLine(" AND partinfo.EquipType like '%" + queryParams.equipType + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND partinfo.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND partinfo.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public string GetNextEquipPartInfoCode(string equipTypeCode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" SELECT MAX(ObjID) + 1 AS ObjID FROM BasEquipPartInfo WHERE ObjID LIKE '" + equipTypeCode + "%'");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = equipTypeCode + "0001";
            }
            return temp.PadLeft(6, '0');
        }
    }
}
