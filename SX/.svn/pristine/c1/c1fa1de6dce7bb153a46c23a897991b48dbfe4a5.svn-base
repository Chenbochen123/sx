using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcLedgerKeyValueService : BaseService<QmcLedgerKeyValue>, IQmcLedgerKeyValueService
    {
		#region 构造方法

        public QmcLedgerKeyValueService() : base(){ }

        public QmcLedgerKeyValueService(string connectStringKey) : base(connectStringKey){ }

        public QmcLedgerKeyValueService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public string GetNextValueId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ValueId) + 1 as ValueId From QmcLedgerKeyValue ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }

}
