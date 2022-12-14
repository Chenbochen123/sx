using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using NBear.Common;
    public class PptClassService : BaseService<PptClass>, IPptClassService
    {
		#region 构造方法

        public PptClassService() : base(){ }

        public PptClassService(string connectStringKey) : base(connectStringKey){ }

        public PptClassService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IPptClassService 成员

        /// <summary>
        /// 根据班组名称查询班组信息
        /// 孙宜建
        /// 2013-1-25
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PptClass GetClassByName(string name)
        {
            PptClass cl = new PptClass();
            WhereClip where = PptClass._.ClassName == name;
            EntityArrayList<PptClass> lis = this.GetListByWhere(where);
            if (lis.Count <= 0)
                return null;
            else
                cl = lis[0];
            return cl;
        }
        #endregion

        public class QueryParams
        {
            public string objID { get; set; }
            public string shiftClassName { get; set; }
            public string userFlag { get; set; }
            public PageResult<PptClass> pageParams { get; set; }
        }

        public PageResult<PptClass> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptClass> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    ObjID , ClassName , UseFlag
                                 FROM	    PptClass 
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.shiftClassName))
            {
                sqlstr.AppendLine(" AND ClassName like '%" + queryParams.shiftClassName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.userFlag))
            {
                sqlstr.AppendLine(" AND UseFlag ='" + queryParams.userFlag + "'");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
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
