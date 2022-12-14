using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using NBear.Common;
    public class EqmSparePartService : BaseService<EqmSparePart>, IEqmSparePartService
    {
		#region 构造方法

        public EqmSparePartService() : base(){ }

        public EqmSparePartService(string connectStringKey) : base(connectStringKey){ }

        public EqmSparePartService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string sparePartCode { get; set; }
            public string sparePartName { get; set; }
            public string sparePartMainType { get; set; }
            public string sparePartDetailType { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmSparePart> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<EqmSparePart> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmSparePart> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT  part.ObjID, part.SparePartCode, main.MainTypeName AS SparePartMainType, 
                                            minor.DetailTypeName AS SparePartDetailType , 
                                            SparePartName, SparePartOtherName, SparePartSimpleName,unit1.UnitName AS UnitCode, part.SparePartStandards , 
                                            unit2.UnitName AS MinorUnitCode, Price, SAPCode, DefineDate, 
                                            part.DeleteFlag, part.Remark, part.Ext_1, part.Ext_2, part.Ext_3, part.Ext_4 
                                    FROM    EqmSparePart part
                                    LEFT JOIN   EqmSparePartMainType main       ON  main.ObjID = part.SparePartMainType
                                    LEFT JOIN   EqmSparePartDetailType minor    ON  minor.DetailTypeCode = part.SparePartDetailType
                                    LEFT JOIN   BasUnit unit1                   ON  unit1.ObjID = part.UnitCode
                                    LEFT JOIN   BasUnit unit2                   ON  unit2.ObjID = part.MinorUnitCode
                                    WHERE   1=1 ");
            if (!string.IsNullOrEmpty(queryParams.sparePartCode))
            {
                sqlstr.AppendLine(" AND part.SparePartCode = '" + queryParams.sparePartCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.sparePartName))
            {
                sqlstr.AppendLine(" AND part.SparePartName like '%" + queryParams.sparePartName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.sparePartMainType))
            {
                sqlstr.AppendLine(" AND part.SparePartMainType = '" + queryParams.sparePartMainType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.sparePartDetailType))
            {
                sqlstr.AppendLine(" AND part.SparePartDetailType = '" + queryParams.sparePartDetailType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND part.DeleteFlag ='" + queryParams.deleteFlag + "'");
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
        /// 取流水号方法
        /// </summary>
        /// <param name="MajorTypeID"></param>
        /// <param name="MinorTypeID"></param>
        /// <returns></returns>
        public string GetNextSparePartCode(string MajorTypeID, string MinorTypeID)
        {
            string startStr = MajorTypeID + MinorTypeID;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT  Convert(bigint , Max(SparePartCode)) + 1 
                                    AS      SparePartCode 
                                    FROM	dbo.EqmSparePart ");
            sqlstr.AppendLine(@"    WHERE   SparePartCode Like '" + startStr + "%' ");
           
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = startStr + "00001";
            }
            return temp;
        }

        /// <summary>
        /// Gets the distinct recipe eqmsparepart name and code.
        /// 袁洋 2014年4月8日15:00:53
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchKey">The search key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PageResult<EqmSparePart> GetSparePartBySearchKey(QueryParams queryParams)
        {
            PageResult<EqmSparePart> pageParams = queryParams.pageParams;
            EntityArrayList<EqmSparePart> materialList = new EntityArrayList<EqmSparePart>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      DISTINCT    part.SparePartCode , part.SparePartName
                                    FROM        EqmSparePart part
                                    WHERE       1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.sparePartCode))
            {
                sqlstr.AppendLine(" AND part.SparePartCode = '" + queryParams.sparePartCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.sparePartName))
            {
                sqlstr.AppendLine(" AND [dbo].[FuncSysGetPY](SparePartName) like '%" + queryParams.sparePartName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.sparePartMainType))
            {
                sqlstr.AppendLine(" AND part.SparePartMainType = '" + queryParams.sparePartMainType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.sparePartDetailType))
            {
                sqlstr.AppendLine(" AND part.SparePartDetailType = '" + queryParams.sparePartDetailType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND part.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            string sqlresult = String.Format(sqlstr.ToString(), queryParams.sparePartName);
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
