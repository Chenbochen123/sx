using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    using Mesnac.Data.Components;
    public class BasStoragePlacePropService : BaseService<BasStoragePlaceProp>, IBasStoragePlacePropService
    {
		#region 构造方法

        public BasStoragePlacePropService() : base(){ }

        public BasStoragePlacePropService(string connectStringKey) : base(connectStringKey){ }

        public BasStoragePlacePropService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string StoragePlaceID { get; set; }
            public string MaterialCode { get; set; }
            public string MaterialName { get; set; }
            public int? StorageCapacity { get; set; }
            public int? StorageNumber { get; set; }
            public int? SpecialPlace { get; set; }
            public string PoisitionType { get; set; }
            public string StoragePlaceName { get; set; }
            public PageResult<BasStoragePlaceProp> pageParams { get; set; }
        }

        public PageResult<BasStoragePlaceProp> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasStoragePlaceProp> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"
                                  select b.ObjID, c.StoragePlaceID,c.StoragePlaceName, a.MaterialCode,a.MaterialName,b.StorageCapacity,b.StorageNumber, (case when b.StorageCapacity=0 then 0 when b.StorageNumber=0 then 0 else CONVERT(decimal(6,2),(b.StorageNumber+0.0)/(b.StorageCapacity+0.0)) end) Per, b.SpecialPlace
                                   from  BasMaterial a
                                  left join BasStoragePlaceProp b on a.MaterialCode=b.MaterialCode
                                  left join BasStoragePlace c on b.StoragePlaceID=c.StoragePlaceID
						        where b.StoragePlaceID <> ''
                                 ");
            if (!string.IsNullOrEmpty(queryParams.StoragePlaceID))
            {
                sqlstr.AppendLine(" AND c.StoragePlaceID ='" + queryParams.StoragePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.StoragePlaceName))
            {
                sqlstr.AppendLine(" AND c.StoragePlaceName LIKE '%" + queryParams.StoragePlaceName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.MaterialCode))
            {
                sqlstr.AppendLine(" AND a.MaterialCode = '" + queryParams.MaterialCode+ "'");
            }
            if (!string.IsNullOrEmpty(queryParams.MaterialName))
            {
                sqlstr.AppendLine(" AND a.MaterialCodeLIKE '%" + queryParams.MaterialName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.StorageCapacity.ToString()))
            {
                sqlstr.AppendLine(" AND b.StorageCapacity= '" + queryParams.StorageCapacity + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.StorageNumber.ToString()))
            {
                sqlstr.AppendLine(" AND b.StorageNumber= " + queryParams.StorageNumber + "");
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

        public DataSet GetStoragePlaceState(string storagePlace)
        {
            var s = "";
            if (string.IsNullOrWhiteSpace(storagePlace))
            {
                s = "<>''";
            }
            else
                s = "='" + storagePlace + "'";
            var sql = @"select b.ObjID, c.StoragePlaceID,c.StoragePlaceName, a.MaterialCode,a.MaterialName,b.StorageCapacity,b.StorageNumber,b.SpecialPlace,b.PositionType
                           from  BasMaterial a
                          left join BasStoragePlaceProp b on a.MaterialCode=b.MaterialCode
                          left join BasStoragePlace c on b.StoragePlaceID=c.StoragePlaceID
						    where b.StoragePlaceID <> ''
                          and c.StoragePlaceID" + s + "order by c.StoragePlaceName";
            NBear.Data.CustomSqlSection a = this.GetBySql(sql);
            return a.ToDataSet();
        }
        public DataSet GetStoragePlaceGroup()
        {
            var sql = @"select distinct(PositionType) from   BasStoragePlaceProp
                where PositionType<>'' ";
            NBear.Data.CustomSqlSection a = this.GetBySql(sql);
            return a.ToDataSet();
        }

    }
}
