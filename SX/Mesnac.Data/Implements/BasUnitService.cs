using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using NBear.Common;
    public class BasUnitService : BaseService<BasUnit>, IBasUnitService
    {
		#region ���췽��

        public BasUnitService() : base(){ }

        public BasUnitService(string connectStringKey) : base(connectStringKey){ }

        public BasUnitService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        public class QueryParams
        {
            public string objID { get; set; }
            public string unitName { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasUnit> pageParams { get; set; }
        }

        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<BasUnit> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasUnit> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT ObjID , UnitName , Remark , DeleteFlag  
                                 FROM BasUnit WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.unitName))
            {
                sqlstr.AppendLine(" AND UnitName like '%" + queryParams.unitName + "%'");
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
        /// ��ȡUnit����һ������ֵ
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetUnitNextPrimaryKeyValue()
        { 
            EntityArrayList<BasUnit> unitList = this.GetAllListOrder(BasUnit._.ObjID.Desc);
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
