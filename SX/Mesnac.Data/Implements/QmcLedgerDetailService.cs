using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcLedgerDetailService : BaseService<QmcLedgerDetail>, IQmcLedgerDetailService
    {
		#region 构造方法

        public QmcLedgerDetailService() : base(){ }

        public QmcLedgerDetailService(string connectStringKey) : base(connectStringKey){ }

        public QmcLedgerDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public string GetNextDetailId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(DetailId) + 1 as DetailId From QmcLedgerDetail");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
