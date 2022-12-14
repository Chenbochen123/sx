using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcPropertyValueService : BaseService<QmcPropertyValue>, IQmcPropertyValueService
    {
		#region 构造方法

        public QmcPropertyValueService() : base(){ }

        public QmcPropertyValueService(string connectStringKey) : base(connectStringKey){ }

        public QmcPropertyValueService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public string GetNextValueId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ValueId) + 1 as ValueId From QmcPropertyValue ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
