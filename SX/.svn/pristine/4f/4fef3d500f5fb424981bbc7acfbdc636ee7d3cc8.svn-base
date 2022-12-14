using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Data;

using Ext.Net;

using Mesnac.Web.UI;
using Mesnac.Entity;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;

using NBear.Common;

public partial class Manager_RubberQuality_BasicInfo_CheckDataImportLog : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExcel" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "CheckDataImportLog.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);

        }
    }

    [DirectMethod]
    public bool SearchQrigMasterLog()
    {
        WhereClip whereClip = new WhereClip();
        if (TextFieldNorthFileName.RawText != null && TextFieldNorthFileName.RawText.Trim() != "")
        {
            whereClip.And(QmtQrigImportLogMaster._.FileName.Like("%" + TextFieldNorthFileName.RawText.Trim() + "%"));
        }
        if (DateFieldNorthImportDate.RawText != null && DateFieldNorthImportDate.RawText.Trim() != "")
        {
            whereClip.And(QmtQrigImportLogMaster._.RecordTime >= DateFieldNorthImportDate.SelectedDate);
            whereClip.And(QmtQrigImportLogMaster._.RecordTime < DateFieldNorthImportDate.SelectedDate.AddDays(1));
        }
        if (ComboBoxNorthImportFlag.Value != null && ComboBoxNorthImportFlag.Value.ToString() != "")
        {
            whereClip.And(QmtQrigImportLogMaster._.Flag == ComboBoxNorthImportFlag.Value.ToString());
        }

        IQmtQrigImportLogMasterManager bQmtQrigImportLogMasterManager = new QmtQrigImportLogMasterManager();
        EntityArrayList<QmtQrigImportLogMaster> mQmtQrigImportLogMasterList = bQmtQrigImportLogMasterManager.GetListByWhereAndOrder(
            whereClip
            , QmtQrigImportLogMaster._.ObjId.Desc);

        StoreCenter.DataSource = mQmtQrigImportLogMasterList;
        StoreCenter.DataBind();

        X.Msg.Alert("提示", "查询完毕").Show();

        return true;
    }

    /// <summary>
    /// 获取导入的明细数据
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    [DirectMethod]
    public bool GetQrigDetailInfo(string guid)
    {
        if (guid == "")
        {
            StoreCenterDetail.RemoveAll();
            return false;
        }
        IQmtQrigImportLogMasterManager bQmtQrigImportLogMasterManager = new QmtQrigImportLogMasterManager();
        DataSet ds = bQmtQrigImportLogMasterManager.GetQrigDetailInfo(guid);

        StoreCenterDetail.DataSource = ds;
        StoreCenterDetail.DataBind();

        return true;
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        if (CheckboxSelectionModelCenter.SelectedRows.Count() == 0)
        {
            X.Msg.Alert("提示", "请选中要导出的记录").Show();
            return;
        }

        string objId = CheckboxSelectionModelCenter.SelectedRows[0].RecordID;
        
        IQmtQrigImportLogMasterManager bQmtQrigImportLogMasterManager = new QmtQrigImportLogMasterManager();
        QmtQrigImportLogMaster mQmtQrigImportLogMaster = bQmtQrigImportLogMasterManager.GetById(new object[] { objId });
        if (mQmtQrigImportLogMaster == null)
        {
            X.Msg.Alert("提示", "未找到要导出的记录").Show();
            return;
        }
        byte[] bytes = mQmtQrigImportLogMaster.FileContent;

        System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
        string fileName = mQmtQrigImportLogMaster.FileName;
        fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);

        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, fileName);

    }
}