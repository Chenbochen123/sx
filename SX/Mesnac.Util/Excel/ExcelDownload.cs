using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Mesnac.Util.Excel
{
    /// <summary>
    /// Excel下载
    /// 孙本强 @ 2013-04-03 13:12:18
    /// </summary>
    /// <remarks></remarks>
    public class ExcelDownload
    {
        /// <summary>
        /// Excel文件下载
        /// 孙本强 @ 2013-04-03 13:12:18
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="filename">The filename.</param>
        /// <remarks></remarks>
        public void ExcelFileDown(DataSet ds, string filename)
        {
            Stream ms = new MemoryStream();
            new DataToFile().ToExcel(ds, ref ms);
            FileDown((MemoryStream)ms, filename);
            ms.Close();
            ms.Dispose();
        }
        /// <summary>
        /// Excel文件下载
        /// 孙本强 @ 2013-04-03 13:12:18
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="filename">The filename.</param>
        /// <remarks></remarks>
        public void ExcelFileDown(DataTable dt, string filename)
        {
            Stream ms = new MemoryStream();
            new DataToFile().ToExcel(dt, ref ms);
            FileDown((MemoryStream)ms, filename);
            ms.Close();
            ms.Dispose();
        }
        /// <summary>
        /// 文件下载
        /// 孙本强 @ 2013-04-03 13:12:18
        /// </summary>
        /// <param name="ms">The ms.</param>
        /// <param name="filename">The filename.</param>
        /// <remarks></remarks>
        public void FileDown(MemoryStream ms, string filename)
        {
            Page page = (Page)HttpContext.Current.Handler;
            Byte[] content = ms.ToArray();
            page.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            page.Response.Cache.SetNoStore();
            page.Response.Clear();
            page.Response.ClearHeaders();
            page.Response.ClearContent();
            page.Response.Buffer = true;
            page.Response.ContentType = "application/octet-stream";
            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename + ".xls", Encoding.UTF8));
            page.Response.AppendHeader("Content-Length", content.Length.ToString());
            page.Response.BinaryWrite(content);
            page.Response.Flush();
            page.Response.End();
        }
    }
}
