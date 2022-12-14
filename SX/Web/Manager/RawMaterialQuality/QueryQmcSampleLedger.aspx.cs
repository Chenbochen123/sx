using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Ext.Net;

using NBear.Common;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;


public partial class Manager_RawMaterialQuality_QueryQmcSampleLedger : System.Web.UI.Page
{
    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
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
            System.Web.UI.HtmlControls.HtmlGenericControl scriptLink = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "QueryQmcSampleLedger.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            DateFieldNorthBeginDate.SetValue(DateTime.Today);
            DateFieldNorthBeginDate.SetRawValue(DateTime.Today.Date.ToString("yyyy-MM-dd"));

            DateFieldNorthEndDate.SetValue(DateTime.Today);
            DateFieldNorthEndDate.SetRawValue(DateTime.Today.Date.AddDays(1).ToString("yyyy-MM-dd"));

            QueryQmcSampleLedgerDetail();
        }

    }
    #endregion

    #region 页面方法
    private void QueryQmcSampleLedgerDetail()
    {
        IQmcSampleLedgeQueryParams paras = new QmcSampleLedgeQueryParams();
        if (DateFieldNorthBeginDate.RawText != "")
        {
            paras.BeginRecordDate = DateFieldNorthBeginDate.RawText;
        }
        if (DateFieldNorthEndDate.RawText != "")
        {
            paras.EndRecordDate = DateTime.Parse(DateFieldNorthEndDate.RawText).AddDays(1).ToString("yyyy-MM-dd");
        }
        if (TextFieldNorthSampleName.Text.Trim() != "")
        {
            paras.SampleName = TextFieldNorthSampleName.Text.Trim();
        }
        if (TextFieldNorthSampleCode.Text.Trim() != "")
        {
            paras.SampleCode = TextFieldNorthSampleCode.Text.Trim();
        }
        if (HiddenNorthSupplierId.Value != null && HiddenNorthSupplierId.Value.ToString() != "")
        {
            paras.SupplierId = HiddenNorthSupplierId.Value.ToString();
        }
        if (HiddenNorthManufacturerId.Value != null && HiddenNorthManufacturerId.Value.ToString() != "")
        {
            paras.ManufacturerId = HiddenNorthManufacturerId.Value.ToString();
        }
        if (TextFieldNorthFactoryCode.Text.Trim() != "")
        {
            paras.FactoryCode = TextFieldNorthFactoryCode.Text.Trim();
        }
        if (TextFieldNorthBarcode.Text.Trim() != "")
        {
            paras.Barcode = TextFieldNorthBarcode.Text.Trim();
        }

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        DataSet ds = GetQmcSampleLedgerInfoQueryByParams(paras);

        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
    }
    #endregion
        public DataSet GetQmcSampleLedgerInfoQueryByParams(IQmcSampleLedgeQueryParams paras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select A.*, B.FacName SupplierName, C.FacName ManufacturerName");
            sb.AppendLine(", D.UserName ExtractorName, E.UserName ReceiverName, F.UserName FetcherName");
            sb.AppendLine(", G.UserName HandlerName");
            sb.AppendLine("From QmcSampleLedger A");
            sb.AppendLine("Left Join BasFactoryInfo B On A.SupplierId = B.ObjID");
            sb.AppendLine("Left Join BasFactoryInfo C On A.ManufacturerId = C.ObjID");
            sb.AppendLine("Left Join BasUser D On A.ExtractorId = D.HRCode");
            sb.AppendLine("Left Join BasUser E On A.ReceiverId = E.HRCode");
            sb.AppendLine("Left Join BasUser F On A.FetcherId = F.HRCode");
            sb.AppendLine("Left Join BasUser G On A.HandlerId = G.HRCode");
            sb.AppendLine("WHERE A.DeleteFlag = '0'");
            if (paras.BeginRecordDate != null && paras.BeginRecordDate != "")
            {
                sb.AppendFormat("AND A.RecordDate >= '{0}'", paras.BeginRecordDate);
                sb.AppendLine();
            }
            if (paras.EndRecordDate != null && paras.EndRecordDate != "")
            {
                sb.AppendFormat("AND A.RecordDate < '{0}'", paras.EndRecordDate);
                sb.AppendLine();
            }
            if (paras.SampleName != null && paras.SampleName != "")
            {
                sb.AppendFormat("AND A.SampleName Like '%{0}%'", paras.SampleName);
                sb.AppendLine();
            }
            if (paras.SampleCode != null && paras.SampleCode != "")
            {
                sb.AppendFormat("AND A.SampleCode = '{0}'", paras.SampleCode);
                sb.AppendLine();
            }
            if (paras.SupplierId != null && paras.SupplierId != "")
            {
                sb.AppendFormat("AND A.SupplierId = '{0}'", paras.SupplierId);
                sb.AppendLine();
            }
            if (paras.ManufacturerId != null && paras.ManufacturerId != "")
            {
                sb.AppendFormat("AND A.ManufacturerId = '{0}'", paras.ManufacturerId);
                sb.AppendLine();
            }
            if (paras.FactoryCode != null && paras.FactoryCode != "")
            {
                sb.AppendFormat("AND A.FactoryCode = '{0}'", paras.FactoryCode);
                sb.AppendLine();
            }
            if (paras.Barcode != null && paras.Barcode != "")
            {
                sb.AppendFormat("AND A.Barcode LIKE '%{0}%'", paras.Barcode);
                sb.AppendLine();
            }
            sb.AppendFormat(@"AND A.LedgerId in (select min (LedgerId) from QmcSampleLedger t1
left join BasFactoryInfo tf on t1.FactoryCode=tf.erpcode
left join QmcFactoryNonCheck t2
on t1.MaterialCode=t2.MaterialCode and tf.objid = t2.FactoryCode
left join QmcFactoryNonCheck t3
on t1.MaterialCode=t3.MaterialCode and  t3.FactoryCode = ''
where t1.deleteflag='0' and LedgerId not in (select LedgerId from QmcCheckData)
and RecordDate > '2015-7-2'
group by  isnull(ISNULL(t2.ObjID,t3.ObjID),left(t1.MaterialCode,4)+right(t1.MaterialCode,5)) )", paras.Barcode);
            sb.AppendLine();
            sb.AppendLine("ORDER BY A.RecordDate DESC");

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
            return bQmcCheckDataManager.GetBySql(sb.ToString()).ToDataSet();
        }
    #region 事件处理
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        if (DateFieldNorthBeginDate.RawText != "" && DateFieldNorthEndDate.RawText != "")
        {
            if (DateFieldNorthBeginDate.SelectedDate > DateFieldNorthEndDate.SelectedDate)
            {
                X.Msg.Alert("提示", "开始日期不能大于结束日期").Show();
                return;
            }
        }
        QueryQmcSampleLedgerDetail();
    }
    #endregion
}