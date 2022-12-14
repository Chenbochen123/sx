using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Svg;
using System.Drawing.Imaging;
using System.Text;

/// <summary>
/// Manager_ReportCenter_SvgToImage 实现类
/// 孙本强 @ 2013-04-03 13:10:26
/// </summary>
/// <remarks></remarks>
public partial class Manager_ReportCenter_SvgToImage : Mesnac.Web.UI.Page
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:10:26
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        string tType = Request.Form["type"];
        string tSvg = Request.Form["svg"];
        string tFileName = Request.Form["filename"];
        string tTmp = new Random().Next().ToString();
        if (string.IsNullOrWhiteSpace(tFileName))
        {
            tFileName = "chart_" + DateTime.Now.ToString("yyyyMMddHHmmss");// +tTmp.ToString();
        }
        MemoryStream tData = new MemoryStream(Encoding.UTF8.GetBytes(tSvg));
        MemoryStream tStream = new MemoryStream();
        string tExt = "";
        string tTypeString = "";
        switch (tType)
        {
            case "image/png":
                tTypeString = "-m image/png";
                tExt = "png";
                break;
            case "image/jpeg":
                tTypeString = "-m image/jpeg";
                tExt = "jpg";
                break;
            case "application/pdf":
                tTypeString = "-m application/pdf";
                tExt = "pdf";
                break;
            case "image/svg+xml":
                tTypeString = "-m image/svg+xml";
                tExt = "svg";
                break;
        }
        if (tTypeString != "")
        {
            string tWidth = Request.Form["width"].ToString();
            Svg.SvgDocument tSvgObj = SvgDocument.Open(tData);
            switch (tExt)
            {
                case "jpg":
                    tSvgObj.Draw().Save(tStream, ImageFormat.Jpeg);
                    break;
                case "png":
                    tSvgObj.Draw().Save(tStream, ImageFormat.Png);
                    break;
                case "svg":
                    tStream = tData;
                    break;
            }
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = tType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + tFileName + "." + tExt + "");
            Response.BinaryWrite(tStream.ToArray());
            Response.End();
        }
    }
}