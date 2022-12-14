using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
using System.Data;
    public class EqmSparePartStoreService : BaseService<EqmSparePartStore>, IEqmSparePartStoreService
    {
		#region 构造方法

        public EqmSparePartStoreService() : base(){ }

        public EqmSparePartStoreService(string connectStringKey) : base(connectStringKey){ }

        public EqmSparePartStoreService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string sparePartCode { get; set; }
            public string majorType { get; set; }
            public string minorType { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmSparePartStore> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<EqmSparePartStore> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmSparePartStore> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      store.ObjID, store.SparePartCode , main.MainTypeName AS MajorType,  
                                                minor.DetailTypeName AS MinorType , part.SparePartName, store.Standards,
                                                part.SparePartOtherName, part.SparePartSimpleName, part.Price, part.SAPCode,
			                                    store.CurrentStoreNum , store.MaxStoreNum , store.MinStoreNum,
			                                    store.PosStoragePlaceID , store.UseStoragePlaceID, 
                                                store.DeleteFlag, store.Remark, store.Ext_1, store.Ext_2, store.Ext_3, store.Ext_4 
                                    FROM        EqmSparePartStore       store
                                    LEFT JOIN   EqmSparePart            part        ON  part.SparePartCode = store.SparePartCode
                                    LEFT JOIN   EqmSparePartMainType    main        ON  main.ObjID = store.MajorType
                                    LEFT JOIN   EqmSparePartDetailType  minor       ON  minor.DetailTypeCode = store.MinorType
                                    WHERE       1=1  ");
            if (!string.IsNullOrEmpty(queryParams.sparePartCode))
            {
                sqlstr.AppendLine(" AND store.SparePartCode = '" + queryParams.sparePartCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.majorType))
            {
                sqlstr.AppendLine(" AND store.MajorType = '" + queryParams.majorType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.minorType))
            {
                sqlstr.AppendLine(" AND store.MinorType = '" + queryParams.minorType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND store.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public DataSet GetSparePartStoreDetail(string sparePartCode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    select a.*, b.SparePartName , c.UserName from ( select * from EqmSapSparePart where SparePartCode='" + sparePartCode + "'  union ");
            sqlstr.AppendLine(@"    select * from EqmSparePartRepairOut where SparePartCode='" + sparePartCode + "' ) a  ");
            sqlstr.AppendLine(@"    LEFT JOIN EqmSparePart  b   ON  a.SparePartCode     =   b.SparePartCode
                                    LEFT JOIN BasUser       c   ON  a.ReceiveUser       =   c.WorkBarcode   order by ReceiveDate ");

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }
    }
}
