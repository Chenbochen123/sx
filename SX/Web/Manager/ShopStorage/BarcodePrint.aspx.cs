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

public partial class Manager_ShopStorage_BarcodePrint : System.Web.UI.Page
{
    protected PstShopStorageDetailManager shopStorageDetailManager = new PstShopStorageDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("ShopBarcodePrint.frx"));
        //绑定数据源
        string sql = string.Empty;
        string str = "";
        bool batchPrint = false;
        try
        {
            str = Request.QueryString["Str"].ToString();
            batchPrint = false;
        }
        catch
        {
            str = Request.QueryString["Strs"].ToString();
            batchPrint = true;
        }
        if (!batchPrint)
        {
            string[] sArray = str.Split(',');
            string storageID = sArray[0].ToString();
            string storagePlaceID = sArray[1].ToString();
            string barcode = sArray[2].ToString();
            string orderID = sArray[3].ToString();
            string num = sArray[4].ToString();
            string houseNo = sArray[5].ToString();
            string mark = sArray[6].ToString();
            string productNo = sArray[7].ToString();
            DataSet ds = shopStorageDetailManager.GetByPrintInfo(barcode, storageID, storagePlaceID, orderID);

            string data1 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ShiftName"].ToString()) ? " " : ds.Tables[0].Rows[0]["ShiftName"].ToString();
            string data2 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaterialName"].ToString()) ? " " : ds.Tables[0].Rows[0]["MaterialName"].ToString();
            string data3 = barcode;
            string data4 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Weight"].ToString()) ? " " : ds.Tables[0].Rows[0]["Weight"].ToString();
            string data5 = CommonUtil.inSQL(returnStr(houseNo));
            string data6 = CommonUtil.inSQL(returnStr(mark));
            string data7 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateDay"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateDay"].ToString();
            string data8 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateMinute"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateMinute"].ToString();
            string data9 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateDay1"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateDay1"].ToString();
            string data10 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateMinute1"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateMinute1"].ToString();
            string data11 = DateTime.Now.Year.ToString() + "-" + CommonUtil.inSQL(returnStr(productNo));
            for (int i = 0; i < Convert.ToInt32(num); i++)
            {
                sql += "select '" + data1 + "' ShiftName, '" + data2 + "' MaterialName, '" + data3 + "' Barcode, '" + data4 + "' Weight, '" + data7 + "' DateDay, '" + data8 + "' DateMinute, '" + data9 + "' DateDay1, '" + data10 + "' DateMinute1, '" + data5 + "' HouseNo, '" + data6 + "' Mark, '" + i + "' Num, '" + data11 + "' ProductNo union all ";
            }
        }
        else
        {
            string[] sArray = str.Split(';');
            string[] sbarcodeArray = sArray[0].Split(',');
            string[] sstorageArray = sArray[1].Split(',');
            string[] sstorageplaceArray = sArray[2].Split(',');
            string[] sorderidArray = sArray[3].Split(',');
            string houseNo = sArray[4].ToString();
            string mark = sArray[5].ToString();
            string num = sArray[6].ToString();
            string productNo = sArray[7].ToString();
            DataSet ds;

            for (int i = 0; i < sbarcodeArray.Length; i++)
            {
                ds = shopStorageDetailManager.GetByPrintInfo(sbarcodeArray[i].ToString(), sstorageArray[i].ToString(), sstorageplaceArray[i].ToString(), sorderidArray[i].ToString());

                string data1 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ShiftName"].ToString()) ? " " : ds.Tables[0].Rows[0]["ShiftName"].ToString();
                string data2 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaterialName"].ToString()) ? " " : ds.Tables[0].Rows[0]["MaterialName"].ToString();
                string data3 = sbarcodeArray[i].ToString();
                string data4 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Weight"].ToString()) ? " " : ds.Tables[0].Rows[0]["Weight"].ToString();
                string data5 = CommonUtil.inSQL(returnStr(houseNo));
                string data6 = CommonUtil.inSQL(returnStr(mark));
                string data7 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateDay"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateDay"].ToString();
                string data8 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateMinute"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateMinute"].ToString();
                string data9 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateDay1"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateDay1"].ToString();
                string data10 = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DateMinute1"].ToString()) ? " " : ds.Tables[0].Rows[0]["DateMinute1"].ToString();
                string data11 = DateTime.Now.Year.ToString() + "-" + CommonUtil.inSQL(returnStr(productNo));
                for (int j = 0; j < Convert.ToInt32(num); j++)
                {
                    sql += "select '" + data1 + "' ShiftName, '" + data2 + "' MaterialName, '" + data3 + "' Barcode, '" + data4 + "' Weight, '" + data7 + "' DateDay, '" + data8 + "' DateMinute, '" + data9 + "' DateDay1, '" + data10 + "' DateMinute1, '" + data5 + "' HouseNo, '" + data6 + "' Mark, '" + j + "' Num, '" + data11 + "' ProductNo union all ";
                }
            }
        }

        DataSet dsSql = shopStorageDetailManager.GetSqlInfo(sql.Substring(0, sql.Length - 10));

        report.RegisterData(dsSql.Tables[0], "ShopBarcodePrint");
    }

    protected string returnStr(string str)
    {
        string strResult = str.Replace("%2B", "+").Replace("%22", "\"").Replace("%27", "'").Replace("%2F", "\\").Replace("%20", " ").Replace("%3F", "?").Replace("%23", "#").Replace("%26", "&").Replace("%3D", "=");
        return strResult;
    }
}