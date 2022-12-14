using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcSpecMappingService : BaseService<QmcSpecMapping>, IQmcSpecMappingService
    {
		#region 构造方法

        public QmcSpecMappingService() : base(){ }

        public QmcSpecMappingService(string connectStringKey) : base(connectStringKey){ }

        public QmcSpecMappingService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public string GetNextMappingId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(MappingId) + 1 as MappingId From QmcSpecMapping ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
