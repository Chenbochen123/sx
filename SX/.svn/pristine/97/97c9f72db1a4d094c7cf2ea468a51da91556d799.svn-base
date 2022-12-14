using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtOpenActionModelDetailService : BaseService<PmtOpenActionModelDetail>, IPmtOpenActionModelDetailService
    {
		#region 构造方法

        public PmtOpenActionModelDetailService() : base(){ }

        public PmtOpenActionModelDetailService(string connectStringKey) : base(connectStringKey){ }

        public PmtOpenActionModelDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 袁洋 @ 2013-04-03 13:58:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtOpenActionModelDetail>();
                MainModelID = null;
                OpenMixingNo = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// 页面查询条数条件
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtOpenActionModelDetail> PageParams { get; set; }
            /// <summary>
            /// 主模板编号
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string MainModelID { get; set; }
            /// <summary>
            /// 开炼机机编号
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string OpenMixingNo { get; set; }
            /// <summary>
            /// 删除标志
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @ 2013-04-03 13:03:23
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtOpenActionModelDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtOpenActionModelDetail> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT ObjID, MainModelID, OpenMixingNo, OpenActionCode, 
                                        MixTime, CoolMixSpeed, OpenMixSpeed, MixRollor, 
                                        WaterTemp, RubberTemp, CarSpeed FROM PmtOpenActionModelDetail t1
                                 LEFT JOIN PmtOpenActionModelMain main ON main.ObjID  = t1.MainModelID
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.MainModelID))
            {
                sqlstr.AppendLine("AND t1.MainModelID = '" + queryParams.MainModelID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.OpenMixingNo))
            {
                sqlstr.AppendLine("AND t1.OpenMixingNo = '" + queryParams.OpenMixingNo + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
            {
                sqlstr.AppendLine("AND t1.DeleteFlag = '" + queryParams.DeleteFlag + "'");
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
