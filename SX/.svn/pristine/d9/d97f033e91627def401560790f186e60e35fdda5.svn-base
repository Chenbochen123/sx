using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PptBarBomDataService : BaseService<PptBarBomData>, IPptBarBomDataService
    {
        #region 构造方法

        public PptBarBomDataService() : base() { }

        public PptBarBomDataService(string connectStringKey) : base(connectStringKey) { }

        public PptBarBomDataService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public class QueryParams
        {
            public QueryParams()
            {
                PageParams = new PageResult<PptBarBomData>();
                Barcode = null;
            }
            public string Barcode { get; set; }
            public PageResult<PptBarBomData> PageParams { get; set; }
        }


        public DataTable GetBarBomInfo(string barcode)
        {
            string sqlstr = @" SELECT  t1.* ,
                        t2.MaterialName AS SourceMaterName ,
                        t3.MaterialName AS TargetMaterName ,
                        t4.MaterialName AS CurrentMaterName
                FROM    dbo.PptBarBomData t1
                        LEFT JOIN dbo.BasMaterial t2 ON t1.SourceMaterCode = t2.MaterialCode
                        LEFT JOIN dbo.BasMaterial t3 ON t1.TargetMaterCode = t3.MaterialCode
                        LEFT JOIN dbo.BasMaterial t4 ON t1.CurrentMaterCode = t4.MaterialCode
                WHERE t1.CurrentBarcode='" + barcode + "'";

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        public DataTable GetBatchInfo(string barcode)
        {
            if (barcode.Length > 18)
            {
                barcode = barcode.Substring(0, 18);
            }
            string sqlstr = @" SELECT     materialinfo.MaterialName, 
                                          storeinfo.Barcode, 
                                          storeinfo.BillNo , 
                                          storeinfo.ProductNo,
                                          CONVERT(varchar(100), storeinfo.ProcDate, 23) as ProcDate,
                                          CONVERT(varchar(100), storeinfo.RecordDate, 23) as RecordDate,
                                          factoryinfo.FacName,
                                          storeinfo.LLBarcode
                               FROM       PstMaterialStoreinDetail storeinfo
                               LEFT JOIN  PstMaterialStorein materialstore on storeinfo.BillNo = materialstore.BillNo
                               LEFT JOIN  BasFactoryInfo factoryinfo on materialstore.FactoryID = factoryinfo.ObjID
                               LEFT JOIN  BasMaterial materialinfo on materialinfo.MaterialCode = storeinfo.MaterCode
                               WHERE      storeinfo.Barcode LIKE '" + barcode + "%'";

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        public DataTable GetUseNodeByCurrentBarcode(string currentBarcode)
        {
            string sqlstr = @"  SELECT top 200 * FROM PptBarBomData barbom
                                LEFT JOIN PptLotData lot ON lot.Barcode = barbom.sourceBarcode
                                WHERE barbom.CurrentBarcode = '" + currentBarcode + "' order by UsedDatetime desc";

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }
    }
}
