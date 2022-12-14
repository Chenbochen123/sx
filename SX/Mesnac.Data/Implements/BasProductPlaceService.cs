using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class BasProductPlaceService : BaseService<BasProductPlace>, IBasProductPlaceService
    {
		#region ���췽��

        public BasProductPlaceService() : base(){ }

        public BasProductPlaceService(string connectStringKey) : base(connectStringKey){ }

        public BasProductPlaceService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        public DataSet GetProductPlace(string FactoryID)
        {
            string sql = "select * from BasProductPlace where FactoryID = '" + FactoryID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
