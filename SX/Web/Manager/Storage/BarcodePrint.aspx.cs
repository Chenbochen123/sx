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

public partial class Manager_Storage_BarcodePrint : System.Web.UI.Page
{
    protected PstMaterialStoreinManager manager = new PstMaterialStoreinManager();
    protected PstMaterialStoreinDetailManager detailManager = new PstMaterialStoreinDetailManager();

    protected PstMaterialAdjustManager adjustManager = new PstMaterialAdjustManager();
    protected PstMaterialAdjustDetailManager adjustDetailManager = new PstMaterialAdjustDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("BarcodePrint.frx"));
        //绑定数据源
        string str = Request.QueryString["Str"].ToString();
        string[] sArray = str.Split(',');
        string billNo = sArray[0].ToString();
        string storageID = sArray[1].ToString();
        string storagePlaceID = sArray[2].ToString();
        string barcode = sArray[3].ToString();
        string orderID = sArray[4].ToString();
        string shelfWeight = sArray[5].ToString();
        string num = sArray[6].ToString();
        string facBarcode = sArray[7].ToString();
        string inBarcode = sArray[8].ToString();
        string batch = sArray[9].ToString();
        string productPlace = sArray[10].ToString();
        string chkDate = sArray[11].ToString();
        int startNum = Convert.ToInt32(sArray[12].ToString());
        DataSet ds = adjustManager.GetDetailInfo(billNo, storageID, storagePlaceID, barcode, orderID);//adjustManager.GetInfoByBarcode(strBarcode.Substring(0, strBarcode.Length - 2), this.UserID);

        string sql = string.Empty;
        //decimal shelfPieceWeight = Convert.ToDecimal(shelfWeight);
        //decimal lastWeight = Convert.ToDecimal(ds.Tables[0].Rows[0]["AdjustWeight"].ToString()) - shelfPieceWeight * Convert.ToInt32(num);

        //string data1 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UserName"].ToString()) ? " " : ds.Tables[0].Rows[0]["UserName"].ToString();
        //string data2 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaterialName"].ToString()) ? " " : ds.Tables[0].Rows[0]["MaterialName"].ToString();
        //string data3 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ClassName"].ToString()) ? " " : ds.Tables[0].Rows[0]["ClassName"].ToString();
        //string data4 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date1"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date1"].ToString();
        //string data5 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date2"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date2"].ToString();
        //string data6 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date3"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date3"].ToString();
        //string data7 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date4"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date4"].ToString();
        //string data8 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date5"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date5"].ToString();
        //string data9 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date6"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date6"].ToString();
        //string data10 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date7"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date7"].ToString();
        //string data11 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["date8"].ToString()) ? "0" : ds.Tables[0].Rows[0]["date8"].ToString();
        //for (int i = 1; i <= Convert.ToInt32(num); i++)
        //{
        //    sql += "select '"+barcode+"' + RIGHT('0000' + CONVERT(VARCHAR, '" + i + "'), 4) Barcode, '" + data1 + "' UserName, '" + data2 + "' MaterialName, '" + i + "' ShelfBarcode, '" + data3 + "' ClassName, '" + data4 + "' date1, '" + data5 + "' date2, '" + data6 + "' date3, '" + data7 + "' date4, '" + data8 + "' date5, '" + data9 + "' date6, '" + data10 + "' date7, '" + data11 + "' date8 union all ";
        //}

        string data1 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Barcode"].ToString()) ? " " : ds.Tables[0].Rows[0]["Barcode"].ToString().Substring(0, 9);
        string data2 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaterialSimpleName"].ToString()) ? " " : ds.Tables[0].Rows[0]["MaterialSimpleName"].ToString();
        string data3 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaterialOtherName"].ToString()) ? " " : ds.Tables[0].Rows[0]["MaterialOtherName"].ToString();
        string data4 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ProductPlace"].ToString()) ? productPlace : ds.Tables[0].Rows[0]["ProductPlace"].ToString();
        string data5 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ProcDate"].ToString()) ? " " : ds.Tables[0].Rows[0]["ProcDate"].ToString().Substring(0, 10);
        string data6 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ValidDate"].ToString()) ? " " : ds.Tables[0].Rows[0]["ValidDate"].ToString().Substring(0, 10);
        string data7 = facBarcode;
        string data8 = inBarcode;
        string data9 = batch;
        string data10 = chkDate;//string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ChkDate"].ToString()) ? " " : ds.Tables[0].Rows[0]["ChkDate"].ToString().Substring(0, 10);
        string data11 = "合格";//string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ChkResultFlag"].ToString()) ? "未检验" : ds.Tables[0].Rows[0]["ChkResultFlag"].ToString() == "1" ? "合格" : "不合格";
        for (int i = 0; i < Convert.ToInt32(num); i++)
        {
            int liushui = i + startNum;
            sql += "select 'QSE" + DateTime.Now.ToString("MM.dd") + "-' + RIGHT('0000' + CONVERT(VARCHAR, '" + liushui + "'), 4) liushuiID, '" + barcode + "' + RIGHT('0000' + CONVERT(VARCHAR, '" + liushui + "'), 4) Barcode, '" + data1 + "' MaterCode, '" + data2 + "' MaterialSimpleName, '" + i + "' ShelfBarcode, '" + data3 + "' MaterialOtherName, '" + data4 + "' ProductPlace, '" + data5 + "' ProcDate, '" + data6 + "' ValidDate, '" + data7 + "' FacBarcode, '" + data8 + "' InBarcode, '" + data9 + "' Batch, '" + data10 + "' ChkDate, '" + data11 + "' ChkResultFalg union all ";
        }

        DataSet dsSql = adjustManager.GetSqlInfo(sql.Substring(0, sql.Length - 10));

        report.RegisterData(dsSql.Tables[0], "BarcodePrint");
    }
}