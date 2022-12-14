using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using System.Data;
    public class BasEquipPartRelationService : BaseService<BasEquipPartRelation>, IBasEquipPartRelationService
    {
		#region 构造方法

        public BasEquipPartRelationService() : base(){ }

        public BasEquipPartRelationService(string connectStringKey) : base(connectStringKey){ }

        public BasEquipPartRelationService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string partCode { get; set; }
            public string equipCode { get; set; }
            public string equipType { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasEquipPartRelation> pageParams { get; set; }
        }

        public PageResult<BasEquipPartRelation> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasEquipPartRelation> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    relation.ObjID, part.PartName AS PartCode,equip.EquipName AS EquipCode,
                                            equiptype.EquipTypeName AS EquipType, relation.EquipAddress AS EquipAddress,
                                            relation.LastModifyDate, relation.Remark, relation.DeleteFlag AS DeleteFlag
                                 FROM	    BasEquipPartRelation relation  
                                 LEFT JOIN  BasEquipPartInfo    part       ON  relation.PartCode = part.ObjID
                                 LEFT JOIN  BasEquip            equip      ON  relation.EquipCode = equip.EquipCode
                                 LEFT JOIN  BasEquipType        equiptype  ON  relation.EquipType = equiptype.ObjID
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND relation.ObjID like '%" + queryParams.objID + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.partCode))
            {
                sqlstr.AppendLine(" AND relation.PartCode like '%" + queryParams.partCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND relation.EquipCode like '%" + queryParams.equipCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipType))
            {
                sqlstr.AppendLine(" AND relation.EquipType like '%" + queryParams.equipType + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND relation.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND relation.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public string GetNextEquipPartRelationCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" SELECT MAX(ObjID) + 1 AS ObjID FROM BasEquipPartRelation ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp ="1";
            }
            return temp.PadLeft(10, '0');
        }

        public DataSet GetEquipPartsByEquipCode(string equipCode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    relation.PartCode , part.PartName
                                 FROM	    BasEquipPartRelation relation  
                                 LEFT JOIN  BasEquipPartInfo    part       ON  relation.PartCode = part.ObjID
                                 WHERE      relation.DeleteFlag = '0' ");
            if (!string.IsNullOrEmpty(equipCode))
            {
                sqlstr.AppendLine(" AND relation.EquipCode like '%" + equipCode + "%'");
            }
            return this.GetBySql(sqlstr.ToString()).ToDataSet();
        }
    }
}
