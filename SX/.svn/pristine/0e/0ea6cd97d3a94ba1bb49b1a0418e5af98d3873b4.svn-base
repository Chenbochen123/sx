using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using Mesnac.Util;

public partial class Manager_RawMaterialQuality_SampleLebelPrint : Mesnac.Web.UI.Page
{
    #region 属性注入 
    protected IQmcSampleLedgerManager sampleLegerManager = new QmcSampleLedgerManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        //获取台账ID参数
        string str = "";
        str = Request.QueryString["Strs"].ToString();
        string[] sampleIdArray = str.Split(',');
        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("SampleLabelPrint.frx"));
        //绑定数据源
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("SampleName");
        DataColumn dc2 = new DataColumn("SampleCode");
        DataColumn dc3 = new DataColumn("Frequency");
        DataColumn dc4 = new DataColumn("ReceiveDate");
        DataColumn dc5 = new DataColumn("Barcode");
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        dt.Columns.Add(dc3);
        dt.Columns.Add(dc4);
        dt.Columns.Add(dc5);
        for (int i = 0; i < sampleIdArray.Length; i++)
        {
            DataRow dr = dt.NewRow();
            QmcSampleLedger ledger = sampleLegerManager.GetById(sampleIdArray[i]);
            DataSet ds2 = sampleLegerManager.GetBySql(@"select COUNT(*) from QmcSampleLedger
where materialcode = '" + ledger.MaterialCode + "' and SampleSource ='" + ledger.SampleSource + "'   and BillNo<='" + ledger.BillNo + "'").ToDataSet();
            string data1 = string.IsNullOrEmpty(ledger.SampleName) ? " " : ledger.SampleName;
            string data2 = string.IsNullOrEmpty(ledger.SampleCode) ? " " : ledger.SampleCode;
            string data3 = string.IsNullOrEmpty(ledger.Frequency) ? " " : ledger.Frequency;
            string receiveDate = ledger.ReceiveDate.ToString();
            string data5 = ledger.Barcode.ToString();
            if (!string.IsNullOrEmpty(receiveDate))
            {
                receiveDate = receiveDate.Replace(" 0:00:00", "");
            }
            string data4 = string.IsNullOrEmpty(receiveDate) ? " " : receiveDate;
            dr["SampleName"] = data1;
            dr["SampleCode"] = data2;
            //dr["Frequency"] = data3;
            dr["Frequency"] = ledger.SampleSource+ds2.Tables[0].Rows[0][0].ToString();
            dr["ReceiveDate"] = data4;
            dr["Barcode"] = data5;
            dt.Rows.Add(dr);
        }
        report.RegisterData(dt, "SampleLabelPrint");
    }
    #endregion

    #region 页面方法
    /// <summary>
    /// 格式化请求字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected string returnStr(string str)
    {
        string strResult = str.Replace("%2B", "+").Replace("%22", "\"").Replace("%27", "'").Replace("%2F", "\\").Replace("%20", " ").Replace("%3F", "?").Replace("%23", "#").Replace("%26", "&").Replace("%3D", "=");
        return strResult;
    }
    #endregion
}