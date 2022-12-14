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
    public class EqmFaultReasonService : BaseService<EqmFaultReason>, IEqmFaultReasonService
    {
		#region 构造方法

        public EqmFaultReasonService() : base(){ }

        public EqmFaultReasonService(string connectStringKey) : base(connectStringKey){ }

        public EqmFaultReasonService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IEqmFaultReasonService 成员

        public System.Data.DataSet GetDataByParas( EqmFaultReasonParams queryParams )
        {
            StringBuilder sb =new StringBuilder();
            #region
            sb.AppendLine( "SELECT TA.ObjID, TA.FaultID, TA.ReasonName, TA.DealDesc, TA.DeleteFlag, TB.FaultName, TC.ItemName AS DeleteName, TD.TypeName, TE.ItemName AS MainTypeName" );
            sb.AppendLine( "FROM EqmFaultReason TA" );
            sb.AppendLine( "LEFT JOIN EqmStopFault TB ON TA.FaultID=TB.FaultCode" );
            sb.AppendLine( "LEFT JOIN SysCode TC ON TA.DeleteFlag=TC.ItemCode AND TC.TypeID='YesNo'" );
            sb.AppendLine( "LEFT JOIN EqmStopType TD ON TB.TypeID=TD.TypeCode" );
            sb.AppendLine( "LEFT JOIN SysCode TE ON TD.MainTypeID=TE.ItemCode AND TE.TypeID='StopMainType'" );
            sb.AppendLine( "WHERE 1=1" );
            if ( !string.IsNullOrEmpty( queryParams.objID ) )
                sb.AppendLine( "AND TA.ObjID=" + queryParams.objID );
            if ( !string.IsNullOrEmpty( queryParams.faultID ) )
                sb.AppendLine( "AND TA.FaultID='" + queryParams.faultID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.reasonName ) )
                sb.AppendLine( "AND TA.ReasonName LIKE '%" + queryParams.reasonName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.dealDesc ) )
                sb.AppendLine( "AND TA.DealDesc LIKE '%" + queryParams.dealDesc + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.deleteFlag ) )
                sb.AppendLine( "AND TA.DeleteFlag='" + queryParams.deleteFlag + "'" );
            if ( !string.IsNullOrEmpty( queryParams.typeID ) )
                sb.AppendLine( "AND TB.TypeID ='" + queryParams.typeID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.faultName ) )
                sb.AppendLine( "AND TB.FaultName LIKE '%" + queryParams.faultName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeID ) )
                sb.AppendLine( "AND TD.MainTypeID ='" + queryParams.mainTypeID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.typeName ) )
                sb.AppendLine( "AND TD.TypeName LIKE '%" + queryParams.typeName + "%'" );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeName ) )
                sb.AppendLine( "AND TE.ItemName LIKE '%" + queryParams.mainTypeName + "%'" );
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql( sb.ToString() );
            return css.ToDataSet();
        }

        #endregion

        public class QueryParams
        {
            public string objID { get; set; }
            public string faultID { get; set; }
            public string reasonName { get; set; }
            public string dealDesc { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmFaultReason> pageParams { get; set; }
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
        public PageResult<EqmFaultReason> GetEqmFaultReasonBySearchKey(QueryParams queryParams)
        {
            PageResult<EqmFaultReason> pageParams = queryParams.pageParams;
            EntityArrayList<EqmFaultReason> materialList = new EntityArrayList<EqmFaultReason>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      DISTINCT    faultReason.ObjID , faultReason.FaultID , faultReason.ReasonName , faultReason.DealDesc
                                    FROM        EqmFaultReason faultReason
                                    WHERE       [dbo].[FuncSysGetPY](ReasonName) like '%{0}%' ");
            if (!string.IsNullOrEmpty(queryParams.faultID))
            {
                sqlstr.AppendLine(" AND faultReason.FaultID = '" + queryParams.faultID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.dealDesc))
            {
                sqlstr.AppendLine(" AND faultReason.DealDesc = '" + queryParams.dealDesc + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND faultReason.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            string sqlresult = String.Format(sqlstr.ToString(), queryParams.reasonName);
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
