using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Data.Components;

public partial class Manager_RubberQuality_Report_PmtEquipParamChart : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public string[] COLORS = new string[] { "#00ff00" };
    PptLotManager pm = new PptLotManager();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!X.IsAjaxRequest)
        {
            txtBeginTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtEndTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
        }

    }
    
    protected void btnSearchFirstClick(object sender, DirectEventArgs e)
    {
        PptLotManager.QueryParams qp = new PptLotManager.QueryParams();
        qp.BeginTime = Convert.ToDateTime(this.txtBeginTime.Text.Trim()).ToString("yyyy-MM-dd");
        qp.EndTime = Convert.ToDateTime(this.txtBeginTime.Text.Trim()).ToString("yyyy-MM-dd");
        qp.EquipCode = this.hiddenEquipCode.Value.ToString();
        qp.ShiftID = this.txtBarCode.Text.Trim();
        DataSet ds = pm.GetPptLot(qp);
        DataTable dt = ds.Tables[0];
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        if (dt != null && dt.Rows.Count > 0)
        {

            dt1.Columns.Add("SecondSpan", typeof(int));
            dt1.Columns.Add("时间", typeof(string));
            dt1.Columns.Add("数值", typeof(string));
            dt1.Columns.Add("物料", typeof(string));
            dt1.Columns.Add("排胶温度", typeof(string));
            dt1.Columns.Add("混炼时间", typeof(string));
            dt1.Columns.Add("最大压力", typeof(string));
            dt1.Columns.Add("最大能量", typeof(string));
            dt1.Columns.Add("最大功率", typeof(string));
            dt2.Columns.Add("SecondSpan", typeof(int));
            dt2.Columns.Add("时间", typeof(string));
            dt2.Columns.Add("数值", typeof(string));
            dt2.Columns.Add("物料", typeof(string));
            dt2.Columns.Add("排胶温度", typeof(string));
            dt2.Columns.Add("混炼时间", typeof(string));
            dt2.Columns.Add("最大压力", typeof(string));
            dt2.Columns.Add("最大能量", typeof(string));
            dt2.Columns.Add("最大功率", typeof(string));
            dt3.Columns.Add("SecondSpan", typeof(int));
            dt3.Columns.Add("时间", typeof(string));
            dt3.Columns.Add("数值", typeof(string));
            dt3.Columns.Add("物料", typeof(string));
            dt3.Columns.Add("排胶温度", typeof(string));
            dt3.Columns.Add("混炼时间", typeof(string));
            dt3.Columns.Add("最大压力", typeof(string));
            dt3.Columns.Add("最大能量", typeof(string));
            dt3.Columns.Add("最大功率", typeof(string));

            string type = this.cbxType.Value.ToString();
            if (string.IsNullOrEmpty(type))
            {
                type = "排胶温度";
            }
            DataRow dr = dt.Rows[0];

            for (int i = 0; i < 480; i++)
            {
                
                DataRow drs1 = dt1.NewRow();
                drs1["SecondSpan"] = i;
                drs1["时间"] = "";
                drs1["数值"] = 0;
                drs1["物料"] = "";
                drs1["排胶温度"] = "";
                drs1["混炼时间"] = "";
                drs1["最大压力"] = "";
                drs1["最大能量"] = "";
                drs1["最大功率"] = "";
                DataRow drs2 = dt2.NewRow();
                drs2["SecondSpan"] = i;
                drs2["时间"] = "";
                drs2["数值"] = 0;
                drs2["物料"] = "";
                drs2["排胶温度"] = "";
                drs2["混炼时间"] = "";
                drs2["最大压力"] = "";
                drs2["最大能量"] = "";
                drs2["最大功率"] = "";
                DataRow drs3 = dt3.NewRow();
                drs3["SecondSpan"] = i;
                drs3["时间"] = "";
                drs3["数值"] = 0;
                drs3["物料"] = "";
                drs3["排胶温度"] = "";
                drs3["混炼时间"] = "";
                drs3["最大压力"] = "";
                drs3["最大能量"] = "";
                drs3["最大功率"] = "";
                dt1.Rows.Add(drs1);
                dt2.Rows.Add(drs2);
                dt3.Rows.Add(drs3);
            }
            this.Chart1.Series[0].Title = type;

            this.Chart2.Series[0].Title = type;

            this.Chart3.Series[0].Title = type;

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow drs = dt.Rows[j];

                DateTime dtshi = DateTime.Parse(drs["时间"].ToString());
                if (string.Compare(dtshi.ToString("HH:mm"), "16:00") < 0 && drs["Shift_ID"].ToString() == "1")
                {
                    continue;
                }
                if (string.Compare(dtshi.ToString("HH:mm"), "16:00") >= 0 && string.Compare(dtshi.ToString("HH:mm:ss"), "23:59:59") <= 0)
                {
                    
                    int dataint = (int.Parse(dtshi.ToString("HH:mm").Substring(0, 2)) - 16) * 60 + int.Parse(dtshi.ToString("HH:mm").Substring(3, 2));
                    dt1.Rows[dataint]["SecondSpan"] = dataint;
                    dt1.Rows[dataint]["数值"] = drs[type].ToString();
                    dt1.Rows[dataint]["时间"] = drs["时间"].ToString();
                    dt1.Rows[dataint]["物料"] = drs["物料"].ToString();
                    dt1.Rows[dataint]["排胶温度"] = drs["排胶温度"].ToString();
                    dt1.Rows[dataint]["混炼时间"] = drs["混炼时间"].ToString();
                    dt1.Rows[dataint]["最大压力"] = drs["最大压力"].ToString();
                    dt1.Rows[dataint]["最大能量"] = drs["最大能量"].ToString();
                    dt1.Rows[dataint]["最大功率"] = drs["最大功率"].ToString();
                    
                }
                else if (string.Compare(dtshi.ToString("HH:mm"), "00:00") >= 0 && string.Compare(dtshi.ToString("HH:mm:ss"), "07:59:59") <= 0)
                {
                    int dataint = int.Parse(dtshi.ToString("HH:mm").Substring(0, 2)) * 60 + int.Parse(dtshi.ToString("HH:mm").Substring(3, 2));
                    dt2.Rows[dataint]["SecondSpan"] = dataint;
                    dt2.Rows[dataint]["数值"] = drs[type].ToString();
                    dt2.Rows[dataint]["时间"] = drs["时间"].ToString();
                    dt2.Rows[dataint]["物料"] = drs["物料"].ToString();
                    dt2.Rows[dataint]["排胶温度"] = drs["排胶温度"].ToString();
                    dt2.Rows[dataint]["混炼时间"] = drs["混炼时间"].ToString();
                    dt2.Rows[dataint]["最大压力"] = drs["最大压力"].ToString();
                    dt2.Rows[dataint]["最大能量"] = drs["最大能量"].ToString();
                    dt2.Rows[dataint]["最大功率"] = drs["最大功率"].ToString();
                }
                else if (string.Compare(dtshi.ToString("HH:mm"), "08:00") >= 0 && string.Compare(dtshi.ToString("HH:mm:ss"), "15:59:59") <= 0)
                {
                    int dataint = (int.Parse(dtshi.ToString("HH:mm").Substring(0, 2)) - 8) * 60 + int.Parse(dtshi.ToString("HH:mm").Substring(3, 2));
                    dt3.Rows[dataint]["SecondSpan"] = dataint;
                    dt3.Rows[dataint]["数值"] = drs[type].ToString();
                    dt3.Rows[dataint]["物料"] = drs["物料"].ToString();
                    dt3.Rows[dataint]["排胶温度"] = drs["排胶温度"].ToString();
                    dt3.Rows[dataint]["混炼时间"] = drs["混炼时间"].ToString();
                    dt3.Rows[dataint]["最大压力"] = drs["最大压力"].ToString();
                    dt3.Rows[dataint]["最大能量"] = drs["最大能量"].ToString();
                    dt3.Rows[dataint]["最大功率"] = drs["最大功率"].ToString();
                }
            }
            Store store1 = this.Chart1.GetStore();
            store1.DataSource = dt1;
            store1.DataBind();
            Store store2 = this.Chart2.GetStore();
            store2.DataSource = dt2;
            store2.DataBind();
            Store store3 = this.Chart3.GetStore();
            store3.DataSource = dt3;
            store3.DataBind();
        }
        else
        {
            Store store1 = this.Chart1.GetStore();
            store1.Dispose();
            Store store2 = this.Chart2.GetStore();
            store2.Dispose();
            Store store3 = this.Chart3.GetStore();
            store3.Dispose();
        }
    }
}