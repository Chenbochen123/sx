using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PptOpenMixDataService : BaseService<PptOpenMixData>, IPptOpenMixDataService
    {
		#region 构造方法

        public PptOpenMixDataService() : base(){ }

        public PptOpenMixDataService(string connectStringKey) : base(connectStringKey){ }

        public PptOpenMixDataService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetOpenMixByBarCode(string barCode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT		m.Barcode, m.OpenMixId, a.ShowName as OpenActionCode, m.MixTime, m.CoolMixSpeed, 
			                                    m.OpenMixSpeed, m.MixRollor, m.WaterTemp, m.RubberTemp, 
			                                    m.CarSpeed, m.Status, m.SetMixTime
                                    FROM	    PptOpenMixData m
                                    LEFT JOIN   PmtOpenAction a  ON m.OpenActionCode = a.ActionCode  ");
            sqlstr.AppendLine(@"    WHERE      1=1");
            sqlstr.AppendLine(@"    AND        m.Barcode='" + barCode + "'  ");
            sqlstr.AppendLine(@"    Order by   m.OpenMixId  ");

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }
    }
}
