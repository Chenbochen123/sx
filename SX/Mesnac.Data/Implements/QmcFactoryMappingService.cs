using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class QmcFactoryMappingService : BaseService<QmcFactoryMapping>, IQmcFactoryMappingService
    {
		#region 构造方法

        public QmcFactoryMappingService() : base(){ }

        public QmcFactoryMappingService(string connectStringKey) : base(connectStringKey){ }

        public QmcFactoryMappingService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string seriesId { get; set; }
            public string supplierName { get; set; }
            public string supplierERPCode { get; set; }
            public string manufacturerName { get; set; }
            public string manufacturerERPCode { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcFactoryMapping> pageParams { get; set; }
        }

        public PageResult<QmcFactoryMapping> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcFactoryMapping> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT * FROM QmcFactoryMapping where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.supplierName))
            {
                sqlstr.AppendFormat(" AND SupplierName LIKE '%{0}%'", queryParams.supplierName);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.supplierERPCode))
            {
                sqlstr.AppendFormat(" AND SupplierERPCode LIKE '%{0}%'", queryParams.supplierERPCode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.manufacturerName))
            {
                sqlstr.AppendFormat(" AND ManufacturerName LIKE '%{0}%'", queryParams.manufacturerName);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.manufacturerERPCode))
            {
                sqlstr.AppendFormat(" AND ManufacturerERPCode LIKE '%{0}%'", queryParams.manufacturerERPCode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.seriesId))
            {
                sqlstr.AppendLine(" AND SeriesId = '" + queryParams.seriesId + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND DeleteFlag = '" + queryParams.deleteFlag + "'");
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

        public string GetNextMappingId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(MappingId) + 1 as MappingId From QmcFactoryMapping ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
