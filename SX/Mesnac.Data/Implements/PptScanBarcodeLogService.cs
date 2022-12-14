using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PptScanBarcodeLogService : BaseService<PptScanBarcodeLog>, IPptScanBarcodeLogService
    {
		#region 构造方法

        public PptScanBarcodeLogService() : base(){ }

        public PptScanBarcodeLogService(string connectStringKey) : base(connectStringKey){ }

        public PptScanBarcodeLogService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string dtBegin { get; set; }
            public string dtEnd   { get; set; }
            public string equipCode { get; set; }
            public string recipeCode { get; set; }
            public string scanMaterCode { get; set; }
            public string scanBarCode { get; set; }
            public string userCode { get; set; }
            public string infoType { get; set; }
            public string scanUsedBarMsg { get; set; }
            public string MateName { get; set; }
            public string ProName { get; set; }
            public PageResult<PptScanBarcodeLog> pageParams { get; set; }
        }

        public PageResult<PptScanBarcodeLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptScanBarcodeLog> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	convert(varchar, p.ProcDate, 20) ProcDate,convert(varchar,p.RecordDate, 20) RecordDate,p.FactoryID, f.facname,convert(varchar, Dt, 20)  Dt, equip.EquipName AS EquipCode, mater1.MaterialName AS RecipeCode, ScanBarcode, 
                                            mater2.MaterialName AS ScanMaterCode, Msg, u.UserName AS Usercode,
                                            l.ShiftId , l.ClassId,case when l.ScanLogSignal = '1' then '手工录入' else '自动扫描' end ScanLogSignal,scanusedbarmsg
                                 FROM	    PptScanBarcodeLog l
                                 LEFT JOIN  BasEquip equip          ON  equip.EquipCode = l.EquipCode   
                                 LEFT JOIN  BasMaterial mater1      ON  mater1.MaterialCode = l.RecipeCode
                                 LEFT JOIN  BasMaterial mater2      ON  mater2.MaterialCode = l.ScanMaterCode   
                                 LEFT JOIN  BasUser u            ON  u.HRCode = l.Usercode
                                 left join PstShopStorage p  on p.Barcode=LEFT(l.scanBarcode,21)
and p.RecordDate in (select min (RecordDate) from PstShopStorage ps where ps.Barcode=LEFT(l.scanBarcode,21))
                                 left join BasFactoryInfo f on f.ObjID=p.FactoryID
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND l.EquipCode    = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.recipeCode))
            {
                sqlstr.AppendLine(" AND l.RecipeCode    = '" + queryParams.recipeCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.scanBarCode))
            {
                sqlstr.AppendLine(" AND l.scanBarCode like '%" + queryParams.scanBarCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.scanMaterCode))
            {
                sqlstr.AppendLine(" AND l.ScanMaterCode   = '" + queryParams.scanMaterCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.userCode))
            {
                sqlstr.AppendLine(" AND l.Usercode    = '" + queryParams.userCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.infoType))
            {
                sqlstr.AppendLine(" AND isnull(l.scanflag,'1')    = '" + queryParams.infoType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.scanUsedBarMsg))
            {
                sqlstr.AppendLine(" AND isnull(l.ScanLogSignal,'0')    = '" + queryParams.scanUsedBarMsg + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.MateName))
            {
                sqlstr.AppendLine(" AND mater2.MaterialName  like  '%" + queryParams.MateName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.ProName))
            {
                sqlstr.AppendLine(" AND  mater1.MaterialName  like  '%" + queryParams.ProName + "%'");
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.dtBegin))
                {
                    sqlstr.AppendLine("AND l.Dt  >='" + Convert.ToDateTime(queryParams.dtBegin).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.dtEnd))
                {
                    sqlstr.AppendLine("AND l.Dt  <='" + Convert.ToDateTime(queryParams.dtEnd).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
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
