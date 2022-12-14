using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtDealNotionService : BaseService<QmtDealNotion>, IQmtDealNotionService
    {
		#region 构造方法

        public QmtDealNotionService() : base(){ }

        public QmtDealNotionService(string connectStringKey) : base(connectStringKey){ }

        public QmtDealNotionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public System.Data.DataSet GetDataByParas(QmtDealNotionParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("SELECT TA.ObjID, TA.DealNotion, TA.Remark, TA.DeleteFlag, ''  AS DeleteName");
            sb.AppendLine("FROM QmtDealNotion TA");
            //sb.AppendLine("INNER JOIN SysCode TB ON TA.DeleteFlag=TB.ItemCode AND TB.TypeID='YesNo'");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.objID))
                sb.AppendLine("AND TA.ObjID=" + queryParams.objID);
            if (!string.IsNullOrEmpty(queryParams.dealNotion))
                sb.AppendLine("AND TA.DealNotion LIKE '%" + queryParams.dealNotion + "%'");
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
                sb.AppendLine("AND TA.DeleteFlag='" + queryParams.deleteFlag + "'");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }
    }
}
