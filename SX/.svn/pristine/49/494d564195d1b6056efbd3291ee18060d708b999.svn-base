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
    public class EqmStopFaultService : BaseService<EqmStopFault>, IEqmStopFaultService
    {
		#region 构造方法

        public EqmStopFaultService() : base(){ }

        public EqmStopFaultService(string connectStringKey) : base(connectStringKey){ }

        public EqmStopFaultService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IEqmStopFaultService 成员

        public System.Data.DataSet GetDataByParas( EqmStopFaultParams queryParams )
        {
            StringBuilder sb =new StringBuilder();
            #region
            sb.AppendLine( "SELECT TA.ObjID, TA.TypeID, TA.FaultCode, TA.FaultName, TA.DeleteFlag, TB.TypeName, TC.ItemName AS DeleteName, TD.ItemName AS MainTypeName" );
            sb.AppendLine( "FROM EqmStopFault TA" );
            sb.AppendLine( "LEFT JOIN EqmStopType TB ON TA.TypeID=TB.TypeCode" );
            sb.AppendLine( "LEFT JOIN SysCode TC ON TA.DeleteFlag=TC.ItemCode AND TC.TypeID='YesNo'" );
            sb.AppendLine( "LEFT JOIN SysCode TD ON TB.MainTypeID=TD.ItemCode AND TD.TypeID='StopMainType'" );
            sb.AppendLine( "WHERE 1=1" );
            if ( !string.IsNullOrEmpty( queryParams.objID ) )
                sb.AppendLine( "AND TA.ObjID=" + queryParams.objID );
            if ( !string.IsNullOrEmpty( queryParams.typeID ) )
                sb.AppendLine( "AND TA.TypeID='" + queryParams.typeID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.faultCode ) )
                sb.AppendLine( "AND TA.FaultCode='" + queryParams.faultCode + "'" );
            if ( !string.IsNullOrEmpty( queryParams.faultName ) )
                sb.AppendLine( "AND TA.FaultName LIKE '%" + queryParams.faultName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.deleteFlag ) )
                sb.AppendLine( "AND TA.DeleteFlag='" + queryParams.deleteFlag + "'" );
            if ( !string.IsNullOrEmpty( queryParams.typeName ) )
                sb.AppendLine( "AND TB.ItemName LIKE '%" + queryParams.typeName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeID ) )
                sb.AppendLine( "AND TB.MainTypeID='" + queryParams.mainTypeID + "'" );
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql( sb.ToString() );
            return css.ToDataSet();
        }

        public string GetNextFaultCodeByParas( EqmStopFault eqmStopFault )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( @"SELECT '" + eqmStopFault.TypeID + "'+RIGHT('00'+CAST(CAST(ISNULL(RIGHT(MAX(" + EqmStopFault._.FaultCode.ColumnName + "),3),'0') AS INT)+1 AS VARCHAR(4)),3)" );
            sb.AppendLine( "FROM " + this.tableName );
            sb.Append( "WHERE " + EqmStopFault._.TypeID.ColumnName + "='" + eqmStopFault.TypeID + "'" );
            return this.GetBySql( sb.ToString() ).ToScalar().ToString();
        }

        #endregion

        public class QueryParams
        {
            public string faultCode { get; set; }
            public string faultName { get; set; }
            public string typeID { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmStopFault> pageParams { get; set; }
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
        public PageResult<EqmStopFault> GetEqmStopFaultBySearchKey(QueryParams queryParams)
        {
            PageResult<EqmStopFault> pageParams = queryParams.pageParams;
            EntityArrayList<EqmStopFault> materialList = new EntityArrayList<EqmStopFault>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      DISTINCT    stopFault.FaultCode , stopFault.FaultName
                                    FROM        EqmStopFault stopFault
                                    WHERE       [dbo].[FuncSysGetPY](FaultName) like '%{0}%' ");
            if (!string.IsNullOrEmpty(queryParams.faultCode))
            {
                sqlstr.AppendLine(" AND stopFault.TypeCode = '" + queryParams.faultCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.typeID))
            {
                sqlstr.AppendLine(" AND stopFault.TypeID = '" + queryParams.typeID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND stopFault.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            string sqlresult = String.Format(sqlstr.ToString(), queryParams.faultName);
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
