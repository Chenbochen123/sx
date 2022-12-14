using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class EqmSparePartMainTypeService : BaseService<EqmSparePartMainType>, IEqmSparePartMainTypeService
    {
		#region 构造方法

        public EqmSparePartMainTypeService() : base(){ }

        public EqmSparePartMainTypeService(string connectStringKey) : base(connectStringKey){ }

        public EqmSparePartMainTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IEqmSparePartMainTypeService 成员

        public System.Data.DataSet GetDataByParas( EqmSparePartMainTypeParams queryParams )
        {
            StringBuilder sb =new StringBuilder();
            #region
            sb.AppendLine( "SELECT TA.ObjID, TA.MainTypeName, TA.DeleteFlag,TB.ItemName AS DeleteName" );
            sb.AppendLine( "FROM EqmSparePartMainType TA" );
            sb.AppendLine( "LEFT JOIN SysCode TB ON TA.DeleteFlag=TB.ItemCode AND TB.TypeID='YesNo'" );
            sb.AppendLine( "WHERE 1=1" );

            if ( !string.IsNullOrEmpty( queryParams.objID ) )
                sb.AppendLine( "AND TA.ObjID=" + queryParams.objID );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeName ) )
                sb.AppendLine( "AND TA.MainTypeName LIKE '%" + queryParams.mainTypeName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.deleteFlag ) )
                sb.AppendLine( "AND TA.DeleteFlag='" + queryParams.deleteFlag + "'" );
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql( sb.ToString() );
            return css.ToDataSet();
        }

        #endregion
    }
}
