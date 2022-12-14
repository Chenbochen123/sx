using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcCheckItemDetailService : BaseService<QmcCheckItemDetail>, IQmcCheckItemDetailService
    {
		#region 构造方法

        public QmcCheckItemDetailService() : base(){ }

        public QmcCheckItemDetailService(string connectStringKey) : base(connectStringKey){ }

        public QmcCheckItemDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public string GetNextDetailId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ItemDetailId) + 1 as ItemDetailId From QmcCheckItemDetail ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
