using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using NBear.Common;
    /// <summary>
    /// 混炼类型
    /// 孙本强 @ 2013-04-03 13:02:55
    /// </summary>
    /// <remarks></remarks>
    public class PmtMixTypeService : BaseService<PmtMixType>, IPmtMixTypeService
    {
		#region 构造方法

        /// <summary>
        /// 类 PmtMixTypeService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:55
        /// </summary>
        /// <remarks></remarks>
        public PmtMixTypeService() : base(){ }

        /// <summary>
        /// 类 PmtMixTypeService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:55
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtMixTypeService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 PmtMixTypeService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:55
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtMixTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 13:02:55
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// Gets or sets the obj ID.
            /// 孙本强 @ 2013-04-03 13:02:55
            /// </summary>
            /// <value>The obj ID.</value>
            /// <remarks></remarks>
            public string objID { get; set; }
            /// <summary>
            /// Gets or sets the name of the mix.
            /// 孙本强 @ 2013-04-03 13:02:55
            /// </summary>
            /// <value>The name of the mix.</value>
            /// <remarks></remarks>
            public string mixName { get; set; }
            /// <summary>
            /// Gets or sets the remark.
            /// 孙本强 @ 2013-04-03 13:02:55
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string remark { get; set; }
            /// <summary>
            /// Gets or sets the delete flag.
            /// 孙本强 @ 2013-04-03 13:02:55
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string deleteFlag { get; set; }
            /// <summary>
            /// Gets or sets the page params.
            /// 孙本强 @ 2013-04-03 13:02:55
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtMixType> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// 孙本强 @ 2013-04-03 13:02:56
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtMixType> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtMixType> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT ObjID , MixName ,MixCubage , Remark , DeleteFlag 
                                 FROM PmtMixType WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.mixName))
            {
                sqlstr.AppendLine(" AND UnitName like '%" + queryParams.mixName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND DeleteFlag ='" + queryParams.deleteFlag + "'");
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


        /// <summary>
        /// 获取Unit的下一个主键值
        /// 孙本强 @ 2013-04-03 13:02:56
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPmtMixTypeNextPrimaryKeyValue()
        {
            EntityArrayList<PmtMixType> unitList = this.GetAllListOrder(PmtMixType._.ObjID.Desc);
            if (unitList.Count == 0)
            {
                return 1;
            }
            else
            {
                return unitList[0].ObjID + 1;
            }
        }
    }
}
