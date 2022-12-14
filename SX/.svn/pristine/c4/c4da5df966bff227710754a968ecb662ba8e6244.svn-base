using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Ext.Net;

using NBear.Common;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;

public partial class Manager_RawMaterialQuality_QueryRawMaterialChkBillDetail : System.Web.UI.Page
{
    private string _SendChkFlag;
    private string _StockInFlag;
    private string _DetailSendChkFlag;
    private string _ChkResultFlag;

    protected void Page_Load(object sender, EventArgs e)
    {
        _SendChkFlag = Request.QueryString["SendChkFlag"];
        _StockInFlag = Request.QueryString["StockInFlag"];
        _DetailSendChkFlag = Request.QueryString["DetailSendChkFlag"];
        _ChkResultFlag = Request.QueryString["ChkResultFlag"];

        if (!X.IsAjaxRequest)
        {

            #region 加载CSS样式
            System.Web.UI.HtmlControls.HtmlGenericControl cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            #region 加载JS文件
            //System.Web.UI.HtmlControls.HtmlGenericControl scriptLink = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            //scriptLink.Attributes.Add("type", "text/javascript");
            //scriptLink.Attributes.Add("src", "QueryRawMaterialChkBillDetail.js?" + DateTime.Now.Ticks.ToString());
            //this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            DateFieldNorthBeginChkDate.SetValue(DateTime.Today);
            DateFieldNorthBeginChkDate.SetRawValue(DateTime.Today.ToString("yyyy-MM-dd"));

            DateFieldNorthEndChkDate.SetValue(DateTime.Today);
            DateFieldNorthEndChkDate.SetRawValue(DateTime.Today.ToString("yyyy-MM-dd"));

            QueryMaterialChkBillDetail();
        }
    }

    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        if (DateFieldNorthBeginChkDate.RawText != "" && DateFieldNorthEndChkDate.RawText != "")
        {
            if (DateFieldNorthBeginChkDate.SelectedDate > DateFieldNorthEndChkDate.SelectedDate)
            {
                X.Msg.Alert("提示", "开始日期不能大于结束日期").Show();
                return;
            }
        }

        QueryMaterialChkBillDetail();
    }

    private void QueryMaterialChkBillDetail()
    {
        IPstMaterialCheckDetailQueryParams paras = new PstMaterialCheckDetailQueryParams();
        if (DateFieldNorthBeginChkDate.RawText != "")
        {
            paras.BeginSendChkDate = DateFieldNorthBeginChkDate.RawText;
        }
        if (DateFieldNorthEndChkDate.RawText != "")
        {
            paras.EndSendChkDate = DateFieldNorthEndChkDate.RawText;
        }
        if (TextFieldNorthBillNo.Text.Trim() != "")
        {
            paras.BillNo = TextFieldNorthBillNo.Text.Trim();
        }
        if (TextFieldNorthNoticeNo.Text.Trim() != "")
        {
            paras.NoticeNo = TextFieldNorthNoticeNo.Text.Trim();
        }
        if (_SendChkFlag != null && _SendChkFlag != "")
        {
            paras.SendChkFlag = _SendChkFlag;
        }
        if (_SendChkFlag != null && _SendChkFlag != "")
        {
            paras.StockInFlag = _SendChkFlag;
        }
        if (_DetailSendChkFlag != null && _DetailSendChkFlag != "")
        {
            paras.DetailSendChkFlag = _DetailSendChkFlag;
        }
        if (_ChkResultFlag != null && _ChkResultFlag != "")
        {
            paras.ChkResultFlag = _ChkResultFlag;
        }

        IPstMaterialChkDetailManager bPstMaterialChkDetailManager = new PstMaterialChkDetailManager();
        DataSet ds = bPstMaterialChkDetailManager.GetPstMaterialCheckDetailQueryInfoByParams(paras);

        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
    }
}