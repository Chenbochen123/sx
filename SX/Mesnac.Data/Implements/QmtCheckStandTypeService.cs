using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckStandTypeService : BaseService<QmtCheckStandType>, IQmtCheckStandTypeService
    {
		#region 构造方法

        public QmtCheckStandTypeService() : base(){ }

        public QmtCheckStandTypeService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckStandTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        /// <summary>
        /// 修改标识：qusf 20131105
        /// 修改说明：1.增加对车间、使用用途的处理
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public System.Data.DataSet GetDataByParas(QmtCheckStandTypeParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.ObjID, TA.StandTypeName, TA.DeleteFlag, '' AS DeleteName");
            sb.AppendLine(", TA.WorkShopId, TA.CheckTypeCode, TA.CheckTypeName");
            sb.AppendLine(", TC.WorkShopName");
            sb.AppendLine("FROM QmtCheckStandType TA");
            //sb.AppendLine("INNER JOIN SysCode TB ON TA.DeleteFlag=TB.ItemCode AND TB.TypeID='YesNo'");
            sb.AppendLine("LEFT JOIN BasWorkShop TC ON TA.WorkShopId = TC.ObjID");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.objID))
                sb.AppendLine("AND TA.ObjID=" + queryParams.objID);
            if (!string.IsNullOrEmpty(queryParams.standTypeName))
                sb.AppendLine("AND TA.StandTypeName LIKE '%" + queryParams.standTypeName + "%'");
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.deleteFlag + "'");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }
    }
}
