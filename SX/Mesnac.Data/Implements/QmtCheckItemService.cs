using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckItemService : BaseService<QmtCheckItem>, IQmtCheckItemService
    {
		#region 构造方法

        public QmtCheckItemService() : base(){ }

        public QmtCheckItemService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckItemService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public System.Data.DataSet GetDataByParas(QmtCheckItemParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.ObjID,TA.TypeID,TA.ItemCode,TA.ItemName,TA.ItemEnvir,TA.Remark,TA.DeleteFlag,TB.ItemTypeName,TC.ItemName AS DeleteName,TD.Display_id");
            sb.AppendLine("FROM QmtCheckItem TA");
            sb.AppendLine("LEFT JOIN QmtCheckItemType TB ON TA.TypeID=TB.ItemTypeID");
            sb.AppendLine("LEFT JOIN SysCode TC ON TA.DeleteFlag=TC.ItemCode AND TC.TypeID='YesNo'");
            sb.AppendLine("left join qmt_checkitemcd TD ON TA.ItemCode = TD.Item_cd");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.objID))
                sb.AppendLine("AND TA.ObjID=" + queryParams.objID);
            if (!string.IsNullOrEmpty(queryParams.typeID))
                sb.AppendLine("AND TA.TypeID='" + queryParams.typeID + "'");
            if (!string.IsNullOrEmpty(queryParams.itemCode))
                sb.AppendLine("AND TA.ItemCode='" + queryParams.itemCode + "'");
            if (!string.IsNullOrEmpty(queryParams.itemName))
                sb.AppendLine("AND TA.ItemName LIKE '%" + queryParams.itemName + "%'");
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.deleteFlag + "'");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }

        public string GetNextItemCodeByParas(QmtCheckItem qmtCheckItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT '" + qmtCheckItem.TypeID + "'+RIGHT('0'+CAST(CAST(ISNULL(RIGHT(MAX(" + QmtCheckItem._.ItemCode.ColumnName + "),2),'0') AS INT)+1 AS VARCHAR(3)),2)");
            sb.AppendLine("FROM " + this.tableName);
            sb.Append("WHERE " + QmtCheckItem._.TypeID.ColumnName + "='" + qmtCheckItem.TypeID + "'");
            return this.GetBySql(sb.ToString()).ToScalar().ToString();
        }
    }
}
