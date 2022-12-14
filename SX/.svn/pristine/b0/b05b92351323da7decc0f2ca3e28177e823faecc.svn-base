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

using FastReport;
using FastReport.Utils;
using FastReport.Web;
using FastReport.Data;

public partial class Manager_Rubber_Report_EquipRubTimeReport : System.Web.UI.Page
{
    protected PptEquipRubTimeManager equipRubTimeManager = new PptEquipRubTimeManager();
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxChejian.SelectedItem.Index = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("EquipRubTimeReport.frx"));
        //绑定数据源
        ds = equipRubTimeManager.GetEquipRubTime(txtBeginDate.Text, txtEndDate.Text, "", cbxChejian.SelectedItem.Value);
        report.RegisterData(ds.Tables[0], "EquipRubTime");
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();
    }
}