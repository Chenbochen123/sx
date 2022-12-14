using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class EqmStopTypeService : BaseService<EqmStopType>, IEqmStopTypeService
    {
		#region 构造方法

        public EqmStopTypeService() : base(){ }

        public EqmStopTypeService(string connectStringKey) : base(connectStringKey){ }

        public EqmStopTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string typeCode { get; set; }
            public string typeName { get; set; }
            public string mainTypeID { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmStopType> pageParams { get; set; }
        }

        public DataSet GetDataByParas( EqmStopTypeParams queryParams )
        {
            StringBuilder sb =new StringBuilder();
            #region
            sb.AppendLine( "SELECT TA.ObjID, TA.MainTypeID, TA.TypeCode, TA.TypeName, TA.DeleteFlag,TB.ItemName AS MainTypeName,TC.ItemName AS DeleteName" );
            sb.AppendLine( "FROM EqmStopType TA" );
            sb.AppendLine( "LEFT JOIN SysCode TB ON TA.MainTypeID=TB.ItemCode AND TB.TypeID='StopMainType'" );
            sb.AppendLine( "LEFT JOIN SysCode TC ON TA.DeleteFlag=TC.ItemCode AND TC.TypeID='YesNo'" );
            sb.AppendLine( "WHERE 1=1" );

            if ( !string.IsNullOrEmpty( queryParams.objID ) )
                sb.AppendLine( "AND TA.ObjID=" + queryParams.objID );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeID ) )
                sb.AppendLine( "AND TA.MainTypeID='" + queryParams.mainTypeID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.typeCode ) )
                sb.AppendLine( "AND TA.TypeCode='" + queryParams.typeCode + "'" );
            if ( !string.IsNullOrEmpty( queryParams.typeName ) )
                sb.AppendLine( "AND TA.TypeName LIKE '%" + queryParams.typeName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.deleteFlag ) )
                sb.AppendLine( "AND TA.DeleteFlag='" + queryParams.deleteFlag + "'" );
            if ( !string.IsNullOrEmpty( queryParams.stopMainType ) )
                sb.AppendLine( "AND TB.ItemName LIKE '%" + queryParams.stopMainType + "%'" );
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql( sb.ToString() );
            return css.ToDataSet();
        }

        /// <summary>
        /// 获取StopType的下一个类型代码
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNextTypeCodeByParas( EqmStopType eqmStopType )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( @"SELECT '" + eqmStopType.MainTypeID + "'+RIGHT('0'+CAST(CAST(ISNULL(RIGHT(MAX(" + EqmStopType._.TypeCode.ColumnName + "),2),'0') AS INT)+1 AS VARCHAR(3)),2)" );
            sb.AppendLine( "FROM " + this.tableName );
            sb.Append( "WHERE " + EqmStopType._.MainTypeID.ColumnName + "='" + eqmStopType.MainTypeID + "'" );
            return this.GetBySql( sb.ToString() ).ToScalar().ToString();
        }

        /// <summary>
        /// 根据关键词检索设备停机类型
        /// 袁洋 2014年4月8日15:00:53
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchKey">The search key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PageResult<EqmStopType> GetEqmStopTypeBySearchKey(QueryParams queryParams)
        {
            PageResult<EqmStopType> pageParams = queryParams.pageParams;
            EntityArrayList<EqmStopType> materialList = new EntityArrayList<EqmStopType>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      DISTINCT    stopType.TypeCode , stopType.TypeName
                                    FROM        EqmStopType stopType
                                    WHERE       [dbo].[FuncSysGetPY](TypeName) like '%{0}%' ");
            if (!string.IsNullOrEmpty(queryParams.typeCode))
            {
                sqlstr.AppendLine(" AND stopType.TypeCode = '" + queryParams.typeCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.mainTypeID))
            {
                sqlstr.AppendLine(" AND stopType.MainTypeID = '" + queryParams.mainTypeID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND stopType.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            string sqlresult = String.Format(sqlstr.ToString(), queryParams.typeName);
            pageParams.QueryStr = sqlresult;
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlresult);
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

    }
}
