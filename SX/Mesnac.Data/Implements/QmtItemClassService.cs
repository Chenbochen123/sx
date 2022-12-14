using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using NBear.Common;
    public class QmtItemClassService : BaseService<QmtItemClass>, IQmtItemClassService
    {
		#region 构造方法

        public QmtItemClassService() : base(){ }

        public QmtItemClassService(string connectStringKey) : base(connectStringKey){ }

        public QmtItemClassService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string itemClass { get; set; }
            public string itemClassName { get; set; }
            public PageResult<QmtItemClass> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<QmtItemClass> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmtItemClass> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT ObjID , ItemClass , ItemClassName  
                                 FROM QmtItemClass WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.itemClass))
            {
                sqlstr.AppendLine(" AND ItemClass = '" + queryParams.itemClass + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.itemClassName))
            {
                sqlstr.AppendLine(" AND itemClassName like '%" + queryParams.itemClassName + "%'");
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
        /// 获取ItemClass的下一个主键值
        /// </summary>
        /// <returns></returns>
        public int GetItemClassNextPrimaryKeyValue()
        {
            EntityArrayList<QmtItemClass> itemClassList = this.GetAllListOrder(QmtItemClass._.ObjID.Desc);
            if (itemClassList.Count == 0)
            {
                return 0;
            }
            else
            {
                return itemClassList[0].ObjID + 1;
            }
        }
    }
}
