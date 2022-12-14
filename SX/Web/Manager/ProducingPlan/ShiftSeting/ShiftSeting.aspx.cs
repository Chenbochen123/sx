using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Entity;
using Mesnac.Business.Implements;
using System.Text;
using System.Data;

public partial class Manager_ProducingPlan_ShiftSeting_ShiftSeting : Mesnac.Web.UI.Page
{

    PptShiftTimeManager pptShiftimeManager =null;
    PptProcedureManager pptProcedureManager = null;
    PptShiftTimeRuleManager pptShiftTimeRuleManager = null;
    PptSetTimeManager pptSetTimeManager =null;
    PptClassManager pptClassManager =null;
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    public static string facid { get; set; }
    public static string ShiftDT { get; set; }
    public static string ProcedureID { get; set; }
    public static string ShiftID { get; set; }

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch"};
            工序 = new SysPageAction() { ActionID = 2, ActionName = "btnPro" };
            时间 = new SysPageAction() { ActionID = 3, ActionName = "btnTime" };
            规律 = new SysPageAction() { ActionID = 4, ActionName = "btnRule" };
            设定 = new SysPageAction() { ActionID = 5, ActionName = "btnSeting" };
            修改 = new SysPageAction() { ActionID = 6, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 工序 { get; private set; } //必须为 public
        public SysPageAction 时间 { get; private set; } //必须为 public
        public SysPageAction 规律 { get; private set; } //必须为 public
        public SysPageAction 设定 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
    }
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        pptShiftimeManager = new PptShiftTimeManager();
        pptProcedureManager = new PptProcedureManager();
        pptShiftTimeRuleManager = new PptShiftTimeRuleManager();
        pptSetTimeManager = new PptSetTimeManager();
        pptClassManager = new PptClassManager();
        if (!X.IsAjaxRequest)
        {
            //加载工序
            BindProcedure();
            LoadClass();
            LoadGridData(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), "0");
            this.cbo_dept.SelectedItem.Index = 0;
            this.txtStratShiftDate.Text = DateTime.Now.ToString();
            this.txtEndShiftDate.Text = DateTime.Now.ToString();
        }

    }
    /// <summary>
    /// 绑定工序方法
    /// 孙宜建
    /// 2013-1-18
    /// </summary>
    private void BindProcedure()
    {
        EntityArrayList<PptProcedure> pptProcedureLists = pptProcedureManager.GetAllList();
        foreach (PptProcedure procedure in pptProcedureLists)
        {
            this.cbo_dept.Items.Add(new Ext.Net.ListItem(procedure.ProcedureName, procedure.ObjID.ToString()));
            this.cbo_Ppt_DeptType.Items.Add(new Ext.Net.ListItem(procedure.ProcedureName, procedure.ObjID.ToString()));
        }
        Store store = this.gpSetProcedure.GetStore();
        store.DataSource = pptProcedureManager.GetAllDataSet().Tables[0];
        store.DataBind();
    }
    /// <summary>
    /// 初始化加载班次信息
    /// </summary>
    [Ext.Net.DirectMethod()]
    protected void LoadClass()
    {
        DataTable classes = pptClassManager.GetDataSetByWhere("Where UseFlag=1").Tables[0];
        foreach (DataRow cl in classes.Rows)
        {
            string ClassName=cl["ClassName"].ToString();
            string ObjID=Convert.ToInt32(cl["ObjID"]).ToString();
            this.modify_class.Items.Add(new Ext.Net.ListItem(ClassName, ObjID));
        }
    }
    /// <summary>
    /// 相应Grid的更新事件
    /// 孙宜建
    /// 2013-1-17
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MyData_Refresh(object sender, StoreReadDataEventArgs e)
    {
        
         string start = this.txtStratShiftDate.Text;
         string end = this.txtEndShiftDate.Text;
         string dept = "0";
         if (cbo_dept.SelectedItem.Value != null)
         {
             dept = this.cbo_dept.SelectedItem.Value.ToString();
         }
         if (Convert.ToDateTime(start).Year == 0001)
         {
             start = DateTime.Now.ToString("yyyy-MM-dd");
         }
         if (Convert.ToDateTime(end).Year == 0001)
         {
             end = DateTime.Now.ToString("yyyy-MM-dd");
         }
         LoadGridData(start,end,dept);
    }
    /// <summary>
    /// 查询起始日期和结束日期查询对应工序的班次信息
    /// 孙宜建
    /// 2013-1-28
    /// </summary>
    /// <param name="start">开始日期</param>
    /// <param name="end">结束日期</param>
    /// <param name="dept">工序ID 全部查询 设置工序ID=0</param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public void LoadGridData(string start,string end,string dept)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        Store store = this.gpShiftQuery.GetStore();
        store.DataSource = pptShiftimeManager.GetShiftTimeByTime(start,end,dept).Tables[0];
        store.DataBind();
    }
    /// <summary>
    /// 班次查询功能
    /// 孙宜建
    /// 2013-1-20
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSeacherShift_Click(object sender, DirectEventArgs e)
    {
        if (txtStratShiftDate.Text == "")
        {
            return;
        }
        if (txtEndShiftDate.Text == "")
        {
            return;
        }
        if (cbo_dept.SelectedItem == null)
        {
            cbo_dept.Focus();
            return;

        }
        string start = this.txtStratShiftDate.Text;
        string end = this.txtEndShiftDate.Text;
        string dept = "0";
        if (cbo_dept.SelectedItem.Value == null)
        {
            return;
        }
        dept = this.cbo_dept.SelectedItem.Value.ToString();
        LoadGridData(start, end, dept);
    }

    #region 班次设定
    /// <summary>
    /// 班次设定取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAddShiftCancel_Click(object sender, DirectEventArgs e)
    {
        this.AddShiftWindow.Close();
    }
    /// <summary>
    /// 设置班次设定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAddRule_Click(object sender, DirectEventArgs e)
    {
        string dt="";
        int num=0;
        int proid=0;
        proid=Convert.ToInt32(cbo_Ppt_DeptType.SelectedItem.Value);
        dt = Convert.ToDateTime(txt_Shift_start.Text).ToString("yyyy-MM-dd");
        num =Convert.ToInt32(txt_WeekNum.Text);
        pptShiftimeManager.AddPptShiftTime(dt, num, proid);
        this.AddShiftWindow.Close();
 
    }
    #endregion

    #region 工序设置 暂时只设置查询功能
    /// <summary>
    /// 取消设置工序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnDeptCancel_Click(object sender, DirectEventArgs e)
    {
        this.AddDeptWin.Close();
    }
    /// <summary>
    /// 增加工序取消  暂时没启用
    /// </summary>
    /// <param name="isPhantom"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    [DirectMethod]
    public object ValidateSave(bool isPhantom, JsonObject values)
    {
        if (!isPhantom)
        {
            return new { valid = true };
        }

        if (!values.ContainsKey("Dept_Code") || Convert.ToInt32(values["Dept_Code"]) < 1000)
        {
            return new { valid = false, msg = "Salary must be >=1000 for new employee" };
        }

        return new { valid = true };
    }
    #endregion

    #region 班次设定窗体弹出事件
    /// <summary>
    /// 设置班次设定窗体中工序改变时相应事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [Ext.Net.DirectMethod()]
    public void InitShiftClassWin(object sender, DirectEventArgs e)
    {
        InitSetShifClass();
    }
    /// <summary>
    /// 设置班次设定窗体中工序改变时相应事件
    /// </summary>
    private void InitSetShifClass()
    {
        int proid = 0;
        if (cbo_Ppt_DeptType.SelectedItem != null)
        {
            proid = Convert.ToInt32(cbo_Ppt_DeptType.SelectedItem.Value);
        }
        //X.Msg.Notify("", proid).Show();
        WhereClip where = PptShiftTimeRule._.ProcedureID == proid;

        string sql = "select * from Ppt_ShiftimeRule where deptType = '" + proid + "'";
        this.txt_WeekDay.Text = pptShiftTimeRuleManager.GetBySql(sql).ToDataSet().Tables[0].Rows.Count.ToString();

        int shiftnum = pptSetTimeManager.GetShiftNumByProcedureID(proid);
        this.txt_Shift_number.Text = shiftnum.ToString();

        int classnum = pptShiftTimeRuleManager.GetShiftClassNumByProcedureID(proid);
        this.txt_Class_number.Text = (classnum).ToString();

        return;
        //EntityArrayList<PptShiftTime> list = pptShiftimeManager.GetTopNListWhereOrder(1, where, PptShiftTime._.ShiftDT.Desc);
        //if (list.Count <= 0)
        //{
        //    this.txt_Shift_start.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //}
        //else
        //{
        //    this.txt_Shift_start.Text = Convert.ToDateTime(list[0].ShiftDT).AddDays(1).ToString("yyyy-MM-dd");
        //}



    }
    /// <summary>
    /// 点击设定菜单 前台调用方法
    /// </summary>
    [Ext.Net.DirectMethod()]
    public void InitShift()
    {
        InitSetShifClass();
        AddShiftWindow.Show();
    }
    #endregion

    #region 点击修改弹出
    /// <summary>
    /// 点击修改激发的事件
    /// 孙宜建   
    /// 2013年1月31日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string ShiftDT, string facid, string ProcedureID, string ShiftID)
    {
        Manager_ProducingPlan_ShiftSeting_ShiftSeting.facid = facid;
        Manager_ProducingPlan_ShiftSeting_ShiftSeting.ProcedureID = ProcedureID;
        Manager_ProducingPlan_ShiftSeting_ShiftSeting.ShiftDT = ShiftDT;
        Manager_ProducingPlan_ShiftSeting_ShiftSeting.ShiftID = ShiftID;
        //X.Msg.Notify(Manager_ProducingPlan_ShiftSeting_ShiftSeting.facid, facid).Show();

        ShiftDT = ShiftDT.Substring(1, 10);
        string sql = "select * from PptShiftTime where FacID = '" + facid + "' and ShiftDT = '" + ShiftDT + "'and ProcedureID = '" + ProcedureID + "'and ShiftID = '" + ShiftID + "'";
        DataSet ds = pptShiftimeManager.GetBySql(sql).ToDataSet();
        modify_pptShiftTimeId.Value = ds.Tables[0].Rows[0][0].ToString();
        modify_shiftDT.Value = ds.Tables[0].Rows[0]["ShiftDT"].ToString();
        modify_dfStartWook.Value = ds.Tables[0].Rows[0]["ShiftStart"].ToString();
        modify_dfStopWook.Value = ds.Tables[0].Rows[0]["ShiftEnd"].ToString();
        modify_tfStart.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["ShiftStart"].ToString()).ToString("HH:mm:ss");
        modify_tfStop.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["ShiftEnd"].ToString()).ToString("HH:mm:ss");
        modify_class.Value = ds.Tables[0].Rows[0]["ShiftClassID"].ToString();
        //modify_class.Text = ds.Tables[0].Rows[0]["ShiftClassID"].ToString();
        this.winModify.Show();
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// 孙宜建
    /// 2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            string start = Convert.ToDateTime(modify_dfStartWook.Text).ToString("yyyy-MM-dd") + " " + modify_tfStart.Text;
            string stop = Convert.ToDateTime(modify_dfStopWook.Text).ToString("yyyy-MM-dd") + " " + modify_tfStop.Text;

            string facid = Manager_ProducingPlan_ShiftSeting_ShiftSeting.facid;
            string ProcedureID = Manager_ProducingPlan_ShiftSeting_ShiftSeting.ProcedureID;
            string ShiftDT = Manager_ProducingPlan_ShiftSeting_ShiftSeting.ShiftDT;
            string ShiftID = Manager_ProducingPlan_ShiftSeting_ShiftSeting.ShiftID;
            ShiftDT = ShiftDT.Substring(1, 10);


            string sql = @"update Ppt_Shiftime set Shift_start='" + start + @"',Shift_end='" + stop + @"',Shift_class='" + Convert.ToInt32(modify_class.Value) + @"'
                          where Fac_id = '" + facid + "' and Shift_dt = '" + ShiftDT + "' and Dept_code = '" + ProcedureID + "' and Shift_id = '" + ShiftID + "'";
            pptShiftimeManager.GetBySql(sql).ToDataSet();


            this.winModify.Close();
            pageToolBar.DoRefresh();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// 孙宜建   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winModify.Close();
    }
    #endregion
}