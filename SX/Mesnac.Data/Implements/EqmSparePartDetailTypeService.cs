using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class EqmSparePartDetailTypeService : BaseService<EqmSparePartDetailType>, IEqmSparePartDetailTypeService
    {
		#region 构造方法

        public EqmSparePartDetailTypeService() : base(){ }

        public EqmSparePartDetailTypeService(string connectStringKey) : base(connectStringKey){ }

        public EqmSparePartDetailTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IEqmSparePartDetailTypeService 成员

        public System.Data.DataSet GetDataByParas( EqmSparePartDetailTypeParams queryParams )
        {
            StringBuilder sb =new StringBuilder();
            #region
            sb.AppendLine( "SELECT TA.ObjID, TA.MainTypeID, TA.DetailTypeCode, TA.DetailTypeName, TA.AutoIn, TA.Remark, TA.DeleteFlag, TB.MainTypeName, TC.ItemName AS AutoInName, TD.ItemName AS DeleteName" );
            sb.AppendLine( "FROM EqmSparePartDetailType TA" );
            sb.AppendLine( "LEFT JOIN EqmSparePartMainType TB ON TA.MainTypeID=TB.ObjID" );
            sb.AppendLine( "LEFT JOIN SysCode TC ON TA.AutoIn=TC.ItemCode AND TC.TypeID='YesNo'" );
            sb.AppendLine( "LEFT JOIN SysCode TD ON TA.DeleteFlag=TD.ItemCode AND TD.TypeID='YesNo'" );
            sb.AppendLine( "WHERE 1=1" );

            if ( !string.IsNullOrEmpty( queryParams.objID ) )
                sb.AppendLine( "AND TA.ObjID=" + queryParams.objID );
            if ( !string.IsNullOrEmpty( queryParams.detailTypeCode ) )
                sb.AppendLine( "AND TA.DetailTypeCode='" + queryParams.detailTypeCode + "'" );
            if ( !string.IsNullOrEmpty( queryParams.detailTypeName ) )
                sb.AppendLine( "AND TA.DetailTypeName LIKE '%" + queryParams.detailTypeName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeID ) )
                sb.AppendLine( "AND TA.MainTypeID=" + queryParams.mainTypeID );
            if ( !string.IsNullOrEmpty( queryParams.autoIn ) )
                sb.AppendLine( "AND TA.AutoIn='" + queryParams.autoIn + "'" );
            if ( !string.IsNullOrEmpty( queryParams.deleteFlag ) )
                sb.AppendLine( "AND TA.DeleteFlag='" + queryParams.deleteFlag + "'" );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeName ) )
                sb.AppendLine( "AND TB.MainTypeName LIKE '%" + queryParams.mainTypeName + "%'" );
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql( sb.ToString() );
            return css.ToDataSet();
        }

        #endregion
    }
}
