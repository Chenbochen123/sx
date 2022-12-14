using System.Data;
using System.IO;
using System;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.OleDb;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Util.Excel
{
    /// <summary>
    /// 数据转化为Excel
    /// 孙本强 @ 2013-04-03 13:12:10
    /// </summary>
    /// <remarks></remarks>
    public partial class DataToFile
    {
        #region 写入Excel文件
        /// <summary>
        /// 生成文件
        /// 孙本强 @ 2013-04-03 13:12:10
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private HSSFWorkbook CreateHSSFWorkbook()
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            return hssfworkbook;
        }
        /// <summary>
        /// 生成表
        /// 孙本强 @ 2013-04-03 13:12:10
        /// </summary>
        /// <param name="hssfworkbook">The hssfworkbook.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private ISheet CreateWorksheet(HSSFWorkbook hssfworkbook, string sheetName)
        {
            ISheet sheet = hssfworkbook.CreateSheet(sheetName);
            return sheet;
        }
        /// <summary>
        /// 字段头
        /// 孙本强 @ 2013-04-03 13:12:10
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="sheet">The sheet.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected virtual int WriteTitle(DataTable dt, ISheet sheet)
        {
            int Result = 0;
            int rowIndex = 0;

            //IRow row0 = sheet.CreateRow(rowIndex);
            //row0.CreateCell(0).SetCellValue("数据生成时间");
            //row0.CreateCell(1).SetCellValue(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //Result++;
            //rowIndex++;

            IRow row1 = sheet.CreateRow(rowIndex);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row1.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            Result++;
            return Result;
        }
        /// <summary>
        /// 每行数据
        /// 孙本强 @ 2013-04-03 13:12:10
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="sheet">The sheet.</param>
        /// <param name="irows">The irows.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected virtual int WriteRow(DataRow dr, ISheet sheet, int irows)
        {
            int Result = 0;
            IRow row = sheet.CreateRow(irows);
            for (int icell = 0; icell < dr.Table.Columns.Count; icell++)
            {
                if (dr[icell] == null)
                {
                    row.CreateCell(icell).SetCellValue("");
                }
                if (dr[icell] is DateTime)
                {
                    row.CreateCell(icell).SetCellValue(((DateTime)dr[icell]).ToString("yyyy-MM-dd HH:mm:ss").Replace(" 00:00:00", ""));
                    continue;
                }
                if (dr[icell] is double)
                {
                    row.CreateCell(icell).SetCellValue((double)dr[icell]);
                    continue;
                }
                if (dr[icell] is decimal)
                {
                    string s = dr[icell].ToString();
                    row.CreateCell(icell).SetCellValue(double.Parse(s));
                    continue;
                }
                if (dr[icell] is int)
                {
                    row.CreateCell(icell).SetCellValue((int)dr[icell]);
                    continue;
                }
                row.CreateCell(icell).SetCellValue(dr[icell].ToString());
            }
            Result++;
            return Result;
        }
        /// <summary>
        /// 转化为Excel文件
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="hssfworkbook">The hssfworkbook.</param>
        /// <param name="dt">The dt.</param>
        /// <remarks></remarks>
        private void ToExcel(HSSFWorkbook hssfworkbook, DataTable dt)
        {
            const int onesheetrowscount = 60000;
            int isheetrows = 0;
            int isheetcount = 0;
            ISheet sheet = null;
            for (int idr = 0; idr < dt.Rows.Count; idr++)
            {
                if (isheetrows == 0)
                {
                    string sheetName = dt.TableName;
                    if (string.IsNullOrEmpty(sheetName))
                    {
                        sheetName = "Sheet";
                    }
                    if (isheetcount > 0)
                    {
                        sheetName += isheetcount.ToString();
                    }
                    sheet = CreateWorksheet(hssfworkbook, sheetName);
                    isheetrows += WriteTitle(dt, sheet);
                }
                isheetrows += WriteRow(dt.Rows[idr], sheet, isheetrows);
                if (idr == onesheetrowscount)
                {
                    isheetrows = 0;
                    isheetcount++;
                }
            }
        }
        /// <summary>
        /// 转化为Excel文件
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="excelStream">The excel stream.</param>
        /// <remarks></remarks>
        public void ToExcel(DataTable dt, ref Stream excelStream)
        {
            HSSFWorkbook hssfworkbook = CreateHSSFWorkbook();
            ToExcel(hssfworkbook, dt);
            hssfworkbook.Write(excelStream);
        }
        /// <summary>
        /// 转化为Excel文件
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="excelStream">The excel stream.</param>
        /// <remarks></remarks>
        public void ToExcel(DataSet ds, ref Stream excelStream)
        {
            HSSFWorkbook hssfworkbook = CreateHSSFWorkbook();
            foreach (DataTable dt in ds.Tables)
            {
                ToExcel(hssfworkbook, dt);
            }
            hssfworkbook.Write(excelStream);
        }
        #endregion

        #region 读取Excel文件 NPOI
        /// <summary>
        /// 读取表头
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private DataTable ReadColumn(System.Collections.IEnumerator rows)
        {
            DataTable Result = new DataTable();
            IRow dc_row = (HSSFRow)rows.Current;
            for (int idc = 0; idc < dc_row.LastCellNum; idc++)
            {
                string dc_name = string.Empty;
                try
                {
                    ICell cell = dc_row.GetCell(idc);
                    if (cell == null)
                    {
                        dc_name = "EmptyCell_" + idc.ToString();
                    }
                    else
                    {
                        dc_name = cell.ToString();
                    }
                }
                catch
                {
                    dc_name = "EmptyCell_" + idc.ToString();
                }
                Result.Columns.Add(dc_name);
            }
            return Result;
        }
        /// <summary>
        /// 读行信息
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="dt">The dt.</param>
        /// <remarks></remarks>
        private void ReadRow(System.Collections.IEnumerator rows, ref DataTable dt)
        {
            IRow row = (HSSFRow)rows.Current;
            DataRow dr = dt.NewRow();
            bool hasData = false;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                try
                {
                    ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = string.Empty;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                        if (!string.IsNullOrEmpty(cell.ToString()))
                        {
                            hasData = true;
                        }
                    }
                }
                catch
                {
                    dr[i] = string.Empty;
                }
            }
            if (hasData)
            {
                dt.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 读表信息
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="rows">The rows.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private DataTable ReadDataTable(ISheet sheet, System.Collections.IEnumerator rows)
        {
            DataTable Result = ReadColumn(rows);
            while (rows.MoveNext())
            {
                ReadRow(rows, ref Result);
            }
            Result.TableName = sheet.SheetName.Trim();
            return Result;
        }
        /// <summary>
        /// 读多表信息
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private DataSet ReadDataSet(string fileName, string TableName)
        {
            DataSet Result = new DataSet();
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                for (int isheet = 0; isheet < hssfworkbook.NumberOfSheets; isheet++)
                {
                    ISheet sheet = hssfworkbook.GetSheetAt(isheet);
                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                    if (!rows.MoveNext())
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(TableName))
                    {
                        if (TableName.Trim().ToUpper() != sheet.SheetName.Trim().ToUpper())
                        {
                            continue;
                        }
                    }
                    Result.Tables.Add(ReadDataTable(sheet, rows));
                }
            }
            return Result;
        }
        /// <summary>
        /// 读Excel文件
        /// 孙本强 @ 2013-04-03 13:12:11
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet FromExcel(string FileName)
        {
            return ReadDataSet(FileName, string.Empty);
        }
        /// <summary>
        /// 读Excel文件中的某个表
        /// 孙本强 @ 2013-04-03 13:12:12
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable FromExcel(string FileName, string TableName)
        {
            DataTable Result = new DataTable();
            DataSet ds = ReadDataSet(FileName, TableName);
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                Result = ds.Tables[0];
            }
            return Result;
        }
        #endregion
        #region 读取Excel文件 ODBC
        //需要安装   AccessDatabaseEngine.exe  而且系统需要运行在x86下
        /// <summary>
        /// 读Excel文件中的某个表
        /// 孙本强 @ 2013-04-03 13:12:12
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private DataSet _ReadDataSet(string fileName, string TableName)
        {
            DataSet Result = new DataSet();
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                           "Extended Properties=Excel 8.0;" +
                           "data source=\"" + fileName + "\"";
            OleDbConnection myConn = new OleDbConnection(strCon);
            if ((!string.IsNullOrEmpty(TableName.Trim())) && (!TableName.Trim().EndsWith("$")))
            {
                TableName = TableName.Trim() + "$";
            }
            try
            {
                myConn.Open();
                DataTable dt = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow dr in dt.Rows)
                {
                    string tablename = dr["TABLE_NAME"].ToString();
                    if ((!string.IsNullOrEmpty(TableName.Trim())) && (TableName.Trim() != tablename))
                    {
                        continue;
                    }
                    if (tablename.EndsWith("$"))
                    {
                        string sqlstr = " SELECT * FROM [" + tablename + "]";
                        DataTable mydatatable = new DataTable();
                        mydatatable.Clear();
                        mydatatable.Reset();
                        DbCommand mycommand = myConn.CreateCommand();
                        mycommand.CommandText = sqlstr;
                        DbDataReader myRd = mycommand.ExecuteReader();
                        mydatatable.Load(myRd, LoadOption.Upsert);
                        Result.Tables.Add(mydatatable);
                    }
                }
            }
            finally
            {
                myConn.Close();
            }
            return Result;
        }
        /// <summary>
        /// 读Excel文件
        /// 孙本强 @ 2013-04-03 13:12:12
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet _FromExcel(string FileName)
        {
            return _ReadDataSet(FileName, string.Empty);
        }
        /// <summary>
        /// 读Excel文件中的某个表
        /// 孙本强 @ 2013-04-03 13:12:12
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable _FromExcel(string FileName, string TableName)
        {
            DataTable Result = new DataTable();
            DataSet ds = _ReadDataSet(FileName, TableName);
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                Result = ds.Tables[0];
            }
            return Result;
        }

        ///
        /// Excel文档流转换成DataTable
        /// 第一行必须为标题行
        ///
        /// Excel文档流
        /// 表名称
        ///
        public static DataTable RenderFromExcel(Stream excelFileStream, string sheetName)
        {
            return RenderFromExcel(excelFileStream, sheetName, 0);
        }
        ///
        /// Excel文档流转换成DataTable
        ///
        /// Excel文档流
        /// 表名称
        /// 标题行索引号，如第一行为0
        ///
        public static DataTable RenderFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex)
        {
            DataTable table = null;
            using (excelFileStream)
            {
                IWorkbook workbook = new HSSFWorkbook(excelFileStream);
                ISheet sheet = workbook.GetSheet(sheetName);
                table = RenderFromExcel(sheet, headerRowIndex);
            }
            return table;
        }
        ///
        /// Excel文档流转换成DataTable
        /// 默认转换Excel的第一个表
        /// 第一行必须为标题行
        ///
        /// Excel文档流
        ///
        public static DataTable RenderFromExcel(Stream excelFileStream)
        {
            return RenderFromExcel(excelFileStream, 0, 0);
        }
        ///
        /// Excel文档流转换成DataTable
        /// 第一行必须为标题行
        ///
        /// Excel文档流
        /// 表索引号，如第一个表为0
        ///
        public static DataTable RenderFromExcel(Stream excelFileStream, int sheetIndex)
        {
            return RenderFromExcel(excelFileStream, sheetIndex, 0);
        }
        ///
        /// Excel文档流转换成DataTable
        ///
        /// Excel文档流
        /// 表索引号，如第一个表为0
        /// 标题行索引号，如第一行为0
        ///
        public static DataTable RenderFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex)
        {
            DataTable table = null;
            using (excelFileStream)
            {
                IWorkbook workbook = new HSSFWorkbook(excelFileStream);
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                table = RenderFromExcel(sheet, headerRowIndex);
            }
            return table;
        }
        ///
        /// Excel表格转换成DataTable
        ///
        /// 表格
        /// 标题行索引号，如第一行为0
        ///
        private static DataTable RenderFromExcel(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1
            //handling header.
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + 1 + headerRowIndex); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = GetCellValue(row.GetCell(j));
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        ///
        /// 根据Excel列类型获取列的值
        ///
        /// Excel列
        ///
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.BLANK:
                    return string.Empty;
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue.ToString();
                case CellType.ERROR:
                    return cell.ErrorCellValue.ToString();
                case CellType.NUMERIC:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return DateTime.FromOADate(cell.NumericCellValue).ToString();
                    else
                        return cell.ToString();
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.STRING:
                    return cell.StringCellValue;
                case CellType.FORMULA:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.STRING:
                            string strFORMULA = cell.StringCellValue;
                            if (strFORMULA != null && strFORMULA.Length > 0)
                                return strFORMULA.ToString();
                            else
                                return null;
                        case CellType.NUMERIC:
                            return Convert.ToString(cell.NumericCellValue);
                        case CellType.BOOLEAN:
                            return Convert.ToString(cell.BooleanCellValue);
                        case CellType.ERROR:
                            return cell.ErrorCellValue.ToString();
                        default: ; break;
                    }
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        #endregion
    }
}
