using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Util.Module.ErrorLogModule;
using Mesnac.Util;
using Ext.Net;
using System.Data;
using Mesnac.Entity;
using System.IO;

/// <summary>
/// Manager_System_SysLog_WebErrorLogQuery 实现类
/// 孙本强 @ 2013-04-03 13:08:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_SysLog_WebErrorLogQuery : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:08:21
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
        SortedList<FileTxtLogs.MyDateTime , string> list = FileTxtLogs.GetFileList();
        foreach (var temp in list)
        {
            if (temp.Value.IndexOf("AppErrorLog") != -1)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(temp.Value, temp.Value);
                FileLogList.Items.Add(item);
            }
        }
        FileLogList.Select(0);
    }

    #region 分页相关方法

    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:08:21
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams); 
        int pageIdx = prms.Page;
        List<FileTxtLogsTable> lst = new List<FileTxtLogsTable>();
        string selectValue = FileLogList.SelectedItem.Value == null ? FileLogList.Items.First().Value:FileLogList.SelectedItem.Value.ToString();
        lst = FileTxtLogs.GetFileTxtLogs(FileTxtLogs.LogPath + selectValue);
        lst.Sort();

        List<FileTxtLogsTable> lists = new List<FileTxtLogsTable>();
        if (lst.Count / 10 == 0 || (lst.Count % 10 == 0 && lst.Count / 10 == 1))//当不够十条，或者只为十条
        {
            for (int i = (10 * (pageIdx - 1) + 1); i < lst.Count; i++)
            {
                lists.Add(lst[i - 1]);
            }
        }
        else
        {
            for (int i = (10 * (pageIdx - 1) + 1); i < (10 * pageIdx + 1); i++)
            {
                lists.Add(lst[i - 1]);
            }
        }
        DataTable data = new DataTable();
        data.Columns.Add("LogDateTime");
        data.Columns.Add("LogTxt");
        data.Columns.Add("LogUserIp");
        data.Columns.Add("LogErrorUrl");

        foreach (FileTxtLogsTable logs in lists)
        {
            DataRow dr = data.NewRow();
            dr["LogDateTime"] = logs.LogDateTime;
            dr["LogTxt"] = logs.LogTxt;
            dr["LogUserIp"] = logs.LogUserIp;
            dr["LogErrorUrl"] = logs.LogErrorUrl;
            data.Rows.Add(dr);
            

        }
        int total = lst.Count;
        return new { data, total };
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        string selectValue = FileLogList.SelectedItem.Value == null ? FileLogList.Items.First().Value : FileLogList.SelectedItem.Value.ToString();
        string path = FileTxtLogs.LogPath + selectValue;
        string fileName = selectValue;//客户端保存的文件名
        string filePath = Server.MapPath("../../../App_Data/Log/" + selectValue);//路径

        FileInfo fileInfo = new FileInfo(filePath);
        //若该文件存在则弹出对话框让你选择保存地址（本地） 
        if (fileInfo.Exists)
        {
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
    #endregion


}