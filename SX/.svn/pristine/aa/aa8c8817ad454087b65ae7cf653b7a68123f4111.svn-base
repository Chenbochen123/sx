using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcCheckDataPropertyService : BaseService<QmcCheckDataProperty>, IQmcCheckDataPropertyService
    {
        #region 构造方法

        public QmcCheckDataPropertyService() : base() { }

        public QmcCheckDataPropertyService(string connectStringKey) : base(connectStringKey) { }

        public QmcCheckDataPropertyService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        /// <summary>
        /// 根据检测记录ID获取检测记录检测属性信息
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public DataSet GetDataSetByCheckId(string checkId)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.*");
            sb.AppendLine(", B.PropertyCode, B.PropertyName, B.ValueType, B.HasSelection");
            sb.AppendLine(", CASE WHEN C.ValueId IS NULL THEN A.PropertyValue ELSE C.PropertyValue END ShowValue");
            sb.AppendLine("FROM QmcCheckDataProperty A");
            sb.AppendLine("LEFT JOIN QmcProperty B ON A.ItemPropertyId = B.PropertyId");
            sb.AppendLine("LEFT JOIN QmcPropertyValue C ON B.PropertyId = C.PropertyId AND A.PropertyValue = CONVERT(NVARCHAR(10), C.ValueId)");
            sb.AppendFormat("WHERE A.CheckId = {0}", checkId);
            sb.AppendLine("ORDER BY A.ItemPropertyId");
            sb.AppendLine();

            return this.GetBySql(sb.ToString()).ToDataSet();
        }
    }
}
