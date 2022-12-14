using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using NBear.Data;
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// SysPageActionService 实现类
    /// 孙本强 @ 2013-04-03 12:53:35
    /// </summary>
    /// <remarks></remarks>
    public class SysPageActionService : BaseService<SysPageAction>, ISysPageActionService
    {
        #region 构造方法

        /// <summary>
        /// 类 SysPageActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:35
        /// </summary>
        /// <remarks></remarks>
        public SysPageActionService() : base() { }

        /// <summary>
        /// 类 SysPageActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:35
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysPageActionService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 SysPageActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:35
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysPageActionService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法


        #region ISysPageActionService 成员函数
        /// <summary>
        /// 获取当前页面用户的操作信息
        /// 孙本强 @ 2013-04-03 12:51:58
        /// 孙本强 @ 2013-04-03 12:53:35
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysPageAction> GetUserPageActionList(string url, string userid)
        {
            List<string> Result = new List<string>();
            string sqlstr = String.Empty;
            sqlstr = @"SELECT t2.* FROM SysUserAllAction  t1
                        INNER JOIN SysPageAction t2 ON t1.ActionID = t2.ObjID
                        INNER JOIN dbo.SysPageMenu t3 ON t2.PageMenuID=t3.ObjID
                        WHERE t1.UserCode=@UserID AND UPPER(t3.PageUrl)=@PageUrl";
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr);
            css.AddInputParameter("@UserID", this.TypeToDbType(userid.GetType()), userid);
            css.AddInputParameter("@PageUrl", this.TypeToDbType(url.GetType()), url.ToUpper());
            return css.ToArrayList<SysPageAction>();
        }

        /// <summary>
        /// 获取所有的页面操作信息
        /// 孙本强 @ 2013-04-03 12:51:58
        /// 孙本强 @ 2013-04-03 12:53:36
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetAllPageMenuAction()
        {
            DataTable Result = new DataTable();
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcGetAllPageMenuAction");
            DataSet ds = sps.ToDataSet();
            return ds;
        }

        public DataSet GetAllPageMenuAction(string powerName)
        {
            DataTable Result = new DataTable();
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcGetAllPageMenuActionByPowerName");
            sps.AddInputParameter("powerName", DbType.String, powerName);
            DataSet ds = sps.ToDataSet();
            return ds;
        }
        #endregion
    }
}
