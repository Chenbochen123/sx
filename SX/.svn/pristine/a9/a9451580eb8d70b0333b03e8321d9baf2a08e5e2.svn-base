using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckItemTypeService : BaseService<QmtCheckItemType>, IQmtCheckItemTypeService
    {
		#region 构造方法

        public QmtCheckItemTypeService() : base(){ }

        public QmtCheckItemTypeService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckItemTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public System.Data.DataSet GetDataByParas(QmtCheckItemTypeParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.ObjID, TA.ItemTypeID, TA.ItemTypeName, TA.DeleteFlag, ''  AS DeleteName");
            sb.AppendLine("FROM QmtCheckItemType TA");
            //sb.AppendLine("INNER JOIN SysCode TB ON TA.DeleteFlag=TB.ItemCode AND TB.TypeID='YesNo'");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.objID))
                sb.AppendLine("AND TA.ObjID=" + queryParams.objID);
            if (!string.IsNullOrEmpty(queryParams.typeID))
                sb.AppendLine("AND TA.ItemTypeID='" + queryParams.typeID + "'");
            if (!string.IsNullOrEmpty(queryParams.typeName))
                sb.AppendLine("AND TA.ItemTypeName LIKE '%" + queryParams.typeName + "%'");
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.deleteFlag + "'");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }

        public string GetNextTypeIDByParas(QmtCheckItemType qmtCheckItemType)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"SELECT CHAR(CASE WHEN ASCII(ISNULL(MAX(RIGHT(" + QmtCheckItemType._.ItemTypeID.ColumnName + ", 1)), 0))=ASCII(9) THEN ASCII('A') ELSE ASCII(ISNULL(MAX(RIGHT(" + QmtCheckItemType._.ItemTypeID.ColumnName + ", 1)), 0))+1 END) AS TypeID");
            sb.AppendLine("FROM " + this.tableName);
            return this.GetBySql(sb.ToString()).ToScalar().ToString();
        }
    }
}
