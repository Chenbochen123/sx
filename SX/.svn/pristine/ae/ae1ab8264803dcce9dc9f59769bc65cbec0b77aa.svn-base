using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmInventoryService : BaseService<PpmInventory>, IPpmInventoryService
    {
		#region 构造方法

        public PpmInventoryService() : base(){ }

        public PpmInventoryService(string connectStringKey) : base(connectStringKey){ }

        public PpmInventoryService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string chejianCode { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public DateTime inventoryEndDate { get; set; }
            public string isHaveSulf { get; set; }
            public string profitLoss { get; set; }

            public PageResult<PpmInventory> pageParams { get; set; }
        }

        public PageResult<PpmInventory> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmInventory> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, B.LLBarCode, B.MaterCode, C.MaterialName, 
	                                A.RealWeight, B.RealWeight OldWeight, A.ProfitLossFlag, 
	                                CONVERT(varchar(19), A.OperTime, 120) OperTime, D.UserName, 
	                                CONVERT(varchar(19), E.EndDate, 120) EndDate, PanCunType
                                from PpmInventory A
	                                left join PpmRubberStorage B on A.Barcode = B.Barcode
	                                left join BasMaterial C on B.MaterCode = C.MaterialCode
	                                left join BasUser D on A.OperPerson = D.WorkBarcode
	                                left join PpmInventoryDatelist E on A.InventoryNum = E.InventoryNum
                                where 1 = 1");

            sqlstr.AppendLine(" AND E.StorageID = '" + queryParams.chejianCode + "'");

            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND E.EndDate >= '" + queryParams.beginDate.ToString() + "'");

            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND E.EndDate <= '" + queryParams.endDate.ToString() + "'");

            sqlstr.AppendLine(" AND E.EndDate = '" + queryParams.inventoryEndDate + "'");
            sqlstr.AppendLine(" AND E.PanCunType = '" + queryParams.isHaveSulf + "'");

            if (queryParams.profitLoss != "all")
            sqlstr.AppendLine(" AND A.ProfitLossFlag = '" + queryParams.profitLoss + "'");

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

        public DataSet GetInverntoryEndDate(DateTime BeginDate, DateTime EndDate, string CheJian)
        {
            string sql = "select EndDate EndDateText, EndDate EndDateValue from PpmInventoryDatelist where EndDate >= '" + BeginDate + "' and EndDate <= '" + EndDate.AddDays(1) + "' and StorageID = '" + CheJian + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
