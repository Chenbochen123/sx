using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
using Mesnac.Business.Interface;

public partial class Manager_BasicInfo_WorkInfo_WorkUserInfo : Mesnac.Web.UI.Page
{
    protected BasEquipManager equipmanager = new BasEquipManager();//业务对象
    protected PptShiftManager shiftmanager = new PptShiftManager();//业务对象
    protected PptClassManager classmanager = new PptClassManager();//业务对象
    protected BasWorkManager manager = new BasWorkManager();//业务对象
    protected BasUserManager usermanager = new BasUserManager();//业务对象
    protected BasWorkUserInfoManager workUserInfomanager = new BasWorkUserInfoManager();//业务对象

    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasWork> entityList;
    private const string constSelectAllText = "---请选择---";

    
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            删除 = new SysPageAction() { ActionID = 2, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
            查询 = new SysPageAction() { ActionID = 4, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 5, ActionName = "btn_history_search" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            导出 = new SysPageAction() { ActionID = 7, ActionName = "btnExport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 添加岗位人员
    protected void BtnAddUser_Click_test(object sender, DirectEventArgs e)
    {
        string strcomboBoxWorkID = "";
        string strpanel = "";
        int index = this.GetIndex();
        Ext.Net.Panel panel = this.BuildPanel(index);
        TextField textFieldWorkBarcode = this.BuildTextFieldWorkBarcode(index);
        ComboBox comboBoxWorkID = this.BuildComboBoxWorkID(index);
        TextField textFieldAttendance = this.BuildTextFieldAttendance(index);
        TextField textFieldRemark = this.BuildTextFieldRemark(index);
        panel.Render("App.Panel_2", RenderMode.AddTo);
        textFieldWorkBarcode.Render(panel, RenderMode.AddTo);
        comboBoxWorkID.Render(panel, RenderMode.AddTo);
        textFieldAttendance.Render(panel, RenderMode.AddTo);
        textFieldRemark.Render(panel, RenderMode.AddTo);
        
        //填充岗位选项
        strcomboBoxWorkID = comboBoxWorkID.ID.ToString();
        strpanel = panel.ID.ToString();

        if (!string.IsNullOrWhiteSpace(strcomboBoxWorkID)&&!string.IsNullOrWhiteSpace(strcomboBoxWorkID))
        {
            EntityArrayList<BasWork> lstWork = manager.GetListByWhere(BasWork._.DeleteFlag == "0");
            foreach (BasWork m in lstWork)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkName;
         //       ComboBox comboBox = (ComboBox)(X.GetCtl(strcomboBoxWorkID));
                X.AddScript("GetControl('" + strcomboBoxWorkID + "')");
                //comboBox.Items.Add(item);

            }
        }
    }
    int index = 2;
    protected void BtnAddUser_Click(object sender, DirectEventArgs e)
    {
        index = this.GetIndex();
        string ID_add_Panel = "add_Panel" + index.ToString();

        foreach (Ext.Net.Panel addPanel in Panel_2.Items)
        {
            if (addPanel.ID.Contains(ID_add_Panel))
            {
                addPanel.Hidden = false;
                addPanel.Enable(true);
            }
        }
    }

    public Ext.Net.Panel BuildPanel(int index)
    {
        return this.X().Panel()
            .ID("add_Panel" + index.ToString())
            .Layout("ColumnLayout")
            .Padding(10);
    }


    public Ext.Net.TextField BuildTextFieldWorkBarcode(int index)
    {
        return this.X().TextField()
            .ID("add_WorkBarcode" + index.ToString())
            .FieldLabel("员工编号")
            .AllowBlank(false)
            .LabelAlign(LabelAlign.Right)
            .IndicatorText("*")
            .IndicatorCls("red-text")
            .ColumnWidth(.25);
    }
    public Ext.Net.ComboBox BuildComboBoxWorkID(int index)
    {
        return this.X().ComboBox()
            .ID("add_WorkID" + index.ToString())
            .Editable(false)
            .FieldLabel("岗位")
            .AllowBlank(false)
            .IndicatorText("*")
            .IndicatorCls("red-text")
            .LabelAlign(LabelAlign.Right)
            .ColumnWidth(.25)
            ;
    }
    public Ext.Net.TextField BuildTextFieldAttendance(int index)
    {
        return this.X().TextField()
            .ID("add_Attendance" + index.ToString())
            .FieldLabel("出勤")
            .AllowBlank(false)
            .LabelAlign(LabelAlign.Right)
            .IndicatorText("*")
            .IndicatorCls("red-text")
            .ColumnWidth(.25);
    }

    public Ext.Net.TextField BuildTextFieldRemark(int index)
    {
        return this.X().TextField()
            .ID("add_Remark" + index.ToString())
            .FieldLabel("备注")
            .AllowBlank(false)
            .LabelAlign(LabelAlign.Right)
            .ColumnWidth(.25);
    }

    private int GetIndex()
    {
        int index = Convert.ToInt32(this.HiddenIndex.Text);
        this.HiddenIndex.Text = (index + 1).ToString();
        return index;
    }
    #endregion 



    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 岗位名称
            txtStratDate.Text = DateTime.Now.ToString("yy-MM-dd");
            txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yy-MM-dd");
            Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
            EntityArrayList<BasEquip> lstEquip = equipmanager.GetListByWhereAndOrder(BasEquip._.EquipType == "01", BasEquip._.EquipName.Asc);
            EntityArrayList<PptShift> lstShift = shiftmanager.GetListByWhere(PptShift._.UseFlag == "1");
            EntityArrayList<PptClass> lstClass = classmanager.GetListByWhere(PptClass._.UseFlag == "1");
            EntityArrayList<BasWork> lstWork = manager.GetListByWhereAndOrder(BasWork._.DeleteFlag == "0", BasWork._.WorkName.Asc);
            cmoShiftID.Items.Clear();
            cmoShiftID.Items.Add(allitem);
            modify_ShiftID.Items.Clear();
            modify_ShiftID.Items.Add(allitem);
            add_ShiftID.Items.Clear();
            cmoWorkID.Items.Clear();
            cmoWorkID.Items.Add(allitem);
            modify_WorkID.Items.Clear();
            modify_WorkID.Items.Add(allitem);
            add_WorkID1.Items.Clear();
            cmoWorkBarcode.Text = "";
            modify_WorkBarcode.Text = "";
            add_WorkBarcode1.Text = "";
            modify_ClassID.Items.Clear();
            modify_ClassID.Items.Add(allitem);
            add_ClassID.Items.Clear();
            foreach (PptShift m in lstShift)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.ShiftName;
                cmoShiftID.Items.Add(item);
                modify_ShiftID.Items.Add(item);
                add_ShiftID.Items.Add(item);
            }
            if (cmoShiftID.Items.Count > 0)
            {
                cmoShiftID.Text = (cmoShiftID.Items[0].Value);
            }
            if (modify_ShiftID.Items.Count > 0)
            {
                modify_ShiftID.Text = (modify_ShiftID.Items[0].Value);
            }
            if (add_ShiftID.Items.Count > 0)
            {
                add_ShiftID.Text = (add_ShiftID.Items[0].Value);
            }
            foreach (BasWork m in lstWork)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkName;
                cmoWorkID.Items.Add(item);
                modify_WorkID.Items.Add(item);
                add_WorkID1.Items.Add(item);
                add_WorkID2.Items.Add(item);
                add_WorkID3.Items.Add(item);
                add_WorkID4.Items.Add(item);
                add_WorkID5.Items.Add(item);
                add_WorkID6.Items.Add(item);
                add_WorkID7.Items.Add(item);
                add_WorkID8.Items.Add(item);
                add_WorkID9.Items.Add(item);
                add_WorkID10.Items.Add(item);
            }
            if (cmoWorkID.Items.Count > 0)
            {
                cmoWorkID.Text = (cmoWorkID.Items[0].Value);
            }
            if (modify_WorkID.Items.Count > 0)
            {
                modify_WorkID.Text = (modify_WorkID.Items[0].Value);
            }
            
            foreach (PptClass m in lstClass)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.ClassName;
                cmoWorkID.Items.Add(item);
                modify_ClassID.Items.Add(item);
                add_ClassID.Items.Add(item);
            }
            if (modify_ClassID.Items.Count > 0)
            {
                modify_ClassID.Text = (modify_ClassID.Items[0].Value);
            }
            if (add_ClassID.Items.Count > 0)
            {
                add_ClassID.Text = (add_ClassID.Items[0].Value);
            }
            #endregion
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasWorkUserInfo> GetPageResultData(PageResult<BasWorkUserInfo> pageParams)
    {
        BasWorkUserInfoManager.QueryParams queryParams = new BasWorkUserInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.startpdtDate = txtStratDate.Text.TrimEnd().TrimStart();
        queryParams.endpdtDate = txtEndDate.Text.TrimEnd().TrimStart();
        queryParams.recordWorkBarcode = cmoEquipCode.Text.TrimEnd().TrimStart();
        queryParams.shiftID = cmoShiftID.Text.Replace(constSelectAllText, "");
        queryParams.workID = cmoWorkID.Text.Replace(constSelectAllText, "");
        //queryParams.workID = cmoWorkID.SelectedItem.Value;
        queryParams.workBarcode = cmoWorkBarcode.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return workUserInfomanager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasWorkUserInfo> pageParams = new PageResult<BasWorkUserInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "PdtDate ASC";
        PageResult<BasWorkUserInfo> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {

        PageResult<BasWorkUserInfo> pageParams = new PageResult<BasWorkUserInfo>();
        pageParams.PageSize = -100;
        PageResult<BasWorkUserInfo> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "岗位人员信息");
    }
    #endregion

    #region 增删改查按钮激发的事件

    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        add_Date.Text = DateTime.Now.ToString("yy-MM-dd");
        add_EquipCode.Text = "";
        if (add_ShiftID.Items.Count > 0)
        {
            add_ShiftID.Text = (add_ShiftID.Items[0].Value);
        }
        if (add_ClassID.Items.Count > 0)
        {
            add_ClassID.Text = (add_ClassID.Items[0].Value);
        }
        if (add_WorkID1.Items.Count > 0)
        {
            add_WorkID1.Text = (add_WorkID1.Items[0].Value);
        }

        add_WorkBarcode1.Text = "";
        add_Attendance1.Text = "";
        add_Remark1.Text = "";

        add_WorkBarcode2.Text = "";
        add_Attendance2.Text = "";
        add_Remark2.Text = "";

        add_WorkBarcode3.Text = "";
        add_Attendance3.Text = "";
        add_Remark3.Text = "";

        add_WorkBarcode4.Text = "";
        add_Attendance4.Text = "";
        add_Remark4.Text = "";

        add_WorkBarcode5.Text = "";
        add_Attendance5.Text = "";
        add_Remark5.Text = "";

        add_WorkBarcode6.Text = "";
        add_Attendance6.Text = "";
        add_Remark6.Text = "";

        add_WorkBarcode7.Text = "";
        add_Attendance7.Text = "";
        add_Remark7.Text = "";


        add_WorkBarcode8.Text = "";
        add_Attendance8.Text = "";
        add_Remark8.Text = "";


        add_WorkBarcode9.Text = "";
        add_Attendance9.Text = "";
        add_Remark9.Text = "";

        add_WorkBarcode10.Text = "";
        add_Attendance10.Text = "";
        add_Remark10.Text = "";
        foreach (Ext.Net.Panel addPanel in Panel_2.Items)
        {
            if (addPanel.ID.Contains("add_Panel"))
            {
                if (!addPanel.ID.Contains("add_Panel1"))
                {
                    addPanel.Hidden = true;
                    addPanel.Enable(false);
                }
            }
        }
        this.HiddenIndex.Text = "2";
        this.winAdd.Show();

    }

    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        BasWorkUserInfo work = workUserInfomanager.GetById(obj_id);
        modify_Objid.Text = work.ObjID.ToString();
        modify_Date.Value = work.PdtDate;
        modify_EquipCode.Text = work.RecordWorkBarcode;
        modify_ShiftID.Text = work.ShiftID.ToString();
        modify_ClassID.Text = work.ClassID.ToString();
        modify_WorkBarcode.Text = work.WorkBarcode.ToString();
        modify_WorkID.Text = work.WorkID.ToString();
        modify_Attendance.Text = work.Attendance.ToString();
        modify_Remark.Text = work.Remark;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            BasWorkUserInfo work = workUserInfomanager.GetById(Convert.ToInt32(obj_id));
            work.DeleteFlag = "0";
            workUserInfomanager.Update(work);
            this.AppendWebLog("岗位人员信息恢复", "编号：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";

    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            BasWorkUserInfo work = workUserInfomanager.GetById(Convert.ToInt32(obj_id));
            work.DeleteFlag = "1";
            workUserInfomanager.Update(work);
            this.AppendWebLog("岗位人员信息删除", "编号：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }


    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(add_EquipCode.Text.TrimEnd().TrimStart()))
            {
                X.Msg.Alert("提示", "请输入主机手编号！").Show();
                return;
            }
            int a= 0;
            if (!int.TryParse(add_EquipCode.Text.Trim(), out a))
            {
                X.Msg.Alert("提示", "请输入合法的主机手编号！").Show();
                return;
            }
            foreach (Ext.Net.Panel addPanel in Panel_2.Items)
            {
                if (addPanel.ID.Contains("add_Panel"))
                {
                    BasWorkUserInfo workUserInfo = new BasWorkUserInfo();
                    workUserInfo.ObjID = Convert.ToInt32(workUserInfomanager.GetNextObjID());
                    workUserInfo.PdtDate = DateTime.Parse(add_Date.Text);
                    workUserInfo.RecordWorkBarcode = add_EquipCode.Text.TrimEnd().TrimStart();
                    workUserInfo.ShiftID = Convert.ToInt32(add_ShiftID.Text.Replace(constSelectAllText, ""));
                    workUserInfo.ClassID = Convert.ToInt32(add_ClassID.Text.Replace(constSelectAllText, ""));
                    workUserInfo.WorkBarcode = "";
                    workUserInfo.WorkID = 0;
                    workUserInfo.Attendance = 0;
                    workUserInfo.Remark = "";
                    foreach (BaseControl control in addPanel.Items)
                    {
                        
                        if (control.ID.Contains("add_WorkBarcode"))
                        {
                            if (!string.IsNullOrWhiteSpace(((TextField)control).Text.TrimEnd().TrimStart()))
                            {
                                workUserInfo.WorkBarcode = ((TextField)control).Text.TrimEnd().TrimStart();
                            }
                        }
                        if (control.ID.Contains("add_WorkID"))
                        {
                            if (!string.IsNullOrWhiteSpace(((ComboBox)control).Text.Replace(constSelectAllText, "")))
                            {
                                workUserInfo.WorkID = Convert.ToInt32(((ComboBox)control).Text.Replace(constSelectAllText, ""));

                            }
                        }
                        if (control.ID.Contains("add_Attendance"))
                        {
                            if (!string.IsNullOrWhiteSpace(((TextField)control).Text.TrimEnd().TrimStart()))
                            {
                                workUserInfo.Attendance = Decimal.Parse(((TextField)control).Text.TrimEnd().TrimStart());
                            }
                        }
                        if (control.ID.Contains("add_Remark"))
                        {
                            workUserInfo.Remark = ((TextField)control).Text.TrimEnd().TrimStart();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(workUserInfo.WorkBarcode.ToString()) && !string.IsNullOrWhiteSpace(workUserInfo.WorkID.ToString()) && !string.IsNullOrWhiteSpace(workUserInfo.Attendance.ToString()))
                    {
                        workUserInfomanager.Insert(workUserInfo);
                    }
                }
            }
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
            
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(modify_EquipCode.Text.TrimStart().TrimEnd()))
            {
                X.Msg.Alert("提示", "请输入主机手员工编号！").Show();
                return;
            }
            if (string.IsNullOrWhiteSpace(modify_ShiftID.Text.Replace(constSelectAllText, "")))
            {
                X.Msg.Alert("提示", "请选择班次！").Show();
                return;
            }
            if (string.IsNullOrWhiteSpace(modify_WorkID.Text.Replace(constSelectAllText, "")))
            {
                X.Msg.Alert("提示", "请选择岗位！").Show();
                return;
            }

            BasWorkUserInfo workUserInfo = new BasWorkUserInfo();
            workUserInfo.ObjID = Convert.ToInt32(modify_Objid.Text);
            workUserInfo.Attach();
            workUserInfo.PdtDate = DateTime.Parse(modify_Date.Text);
            workUserInfo.RecordWorkBarcode = modify_EquipCode.Text.TrimStart().TrimEnd();
            workUserInfo.ShiftID = Convert.ToInt32(modify_ShiftID.Text.Replace(constSelectAllText, ""));
            workUserInfo.ClassID = Convert.ToInt32(modify_ClassID.Text.Replace(constSelectAllText, ""));
            workUserInfo.WorkBarcode = modify_WorkBarcode.Text.TrimEnd().TrimStart();
            workUserInfo.WorkID = Convert.ToInt32(modify_WorkID.Text.Replace(constSelectAllText, ""));
            workUserInfo.Attendance = Decimal.Parse(modify_Attendance.Text);
            workUserInfo.Remark = (string)(modify_Remark.Text);
            workUserInfomanager.Update(workUserInfo);
            this.AppendWebLog("岗位人员信息修改", "编号：" + modify_Objid.Text);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 校验方法
    protected void CheckWorkName(object sender, RemoteValidationEventArgs e)
    {
        //TextField field = (TextField)sender;
        //string workname = field.Text;
        //EntityArrayList<BasWork> workList = manager.GetListByWhere(BasWork._.WorkName == workname);
        //if (workList.Count == 0)
        //{
        //    e.Success = true;
        //}
        //else
        //{
        //    if (workList[0].WorkName.ToString() == hidden_work_name.Value.ToString())
        //    {

        //        e.Success = true;
        //    }
        //    else
        //    {
        //        e.Success = false;
        //        e.ErrorMessage = "此岗位名称已被使用！";
        //    }
        //}
    }
    #endregion

    [DirectMethod]
    public void GetCombox(object ctrl)
    {
        
    }
}