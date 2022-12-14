using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Eqm_downikindService : BaseService<Eqm_downikind>, IEqm_downikindService
    {
		#region 构造方法

        public Eqm_downikindService() : base(){ }

        public Eqm_downikindService(string connectStringKey) : base(connectStringKey){ }

        public Eqm_downikindService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法


        public DataSet GetDataByParas(Eqm_downikind queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine(@"SELECT T1.PARAM_NAME,T.Mp_ikindcode,T.Mp_ikindname FROM Eqm_downikind T
LEFT JOIN Eqm_Mpparam T1 ON T.MP_MKINDCODE = T1.PARAM_ID AND T1.Param_Type=4");
            sb.AppendLine("WHERE 1=1");

            if (!string.IsNullOrEmpty(queryParams.Mp_ikindcode))
                sb.AppendLine("AND T.Mp_ikindcode=" + queryParams.Mp_ikindcode);
            if (!string.IsNullOrEmpty(queryParams.Mp_ikindname))
                sb.AppendLine("AND T.Mp_ikindname='" + queryParams.Mp_ikindname + "'");
            if (!string.IsNullOrEmpty(queryParams.Mp_mkindcode))
                sb.AppendLine("AND T.Mp_mkindcode='" + queryParams.Mp_mkindcode + "'");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }

    }
}
