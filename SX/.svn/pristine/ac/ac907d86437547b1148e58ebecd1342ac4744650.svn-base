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
using System.Text;
using Mesnac.Business.Interface;

public partial class Manager_Equipment_EquipRepairProtectPlan_EquipRepairProtectPlan : Mesnac.Web.UI.Page
{
    protected IEqmRepairProtectPlanManager manager = new EqmRepairProtectPlanManager();//业务对象
    protected IBasUserManager userManager = new BasUserManager();
    protected IBasEquipManager equipManager = new BasEquipManager();
    protected ISysCodeManager sysCodeManager = new SysCodeManager();
    
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

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
            导出 = new SysPageAction() { ActionID = 5, ActionName = "btnExport" };
            审核 = new SysPageAction() { ActionID = 6, ActionName = "btn_finish" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
        if (!X.IsAjaxRequest)
        {
            EntityArrayList<SysCode> planStopTimeList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanStopTime");
            EntityArrayList<SysCode> planTypeList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanType");

            foreach (SysCode item in planStopTimeList)
            {
                add_plan_stop_time.Items.Add(new Ext.Net.ListItem(item.ItemName, item.ItemCode));
                modify_plan_stop_time.Items.Add(new Ext.Net.ListItem(item.ItemName, item.ItemCode));
            }
            txt_start_repair_date.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
            txt_end_repair_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxIsDelete.Value = "0";

            foreach (SysCode item in planTypeList)
            {
                add_plan_name.Items.Add(new Ext.Net.ListItem(item.ItemName, item.ItemCode));
                modify_plan_name.Items.Add(new Ext.Net.ListItem(item.ItemName, item.ItemCode));
                txt_plan_name.Items.Add(new Ext.Net.ListItem(item.ItemName, item.ItemCode));
            }
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<EqmRepairProtectPlan> GetPageResultData(PageResult<EqmRepairProtectPlan> pageParams)
    {
        EqmRepairProtectPlanManager.QueryParams queryParams = new EqmRepairProtectPlanManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.responseUser = hidden_txt_response_user.Value.ToString();
        queryParams.finishUser = hidden_txt_finish_user.Value.ToString();
        queryParams.confirmUser = hidden_txt_confirm_user.Value.ToString();
        queryParams.equipCode = hidden_select_equip_code.Value.ToString();
        queryParams.planName = txt_plan_name.Value.ToString();
        if (cbxIsDelete.SelectedItem.Value != "all")
            queryParams.deleteFlag = cbxIsDelete.SelectedItem.Value;
        try
        {
            queryParams.startRepairDate =
                Convert.ToDateTime(txt_start_repair_date.Value).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_start_repair_date.Value.ToString();
        }
        catch
        {
        }
        try
        {
            queryParams.endRepairDate =
                Convert.ToDateTime(txt_end_repair_date.Value).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_end_repair_date.Value.ToString();
        }
        catch
        {
        }
        try
        {
            queryParams.startFinishDate = 
                Convert.ToDateTime(txt_start_finish_date.Value).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_start_finish_date.Value.ToString();
        }
        catch
        {
        }
        try
        {
            queryParams.endFinishDate = 
                Convert.ToDateTime(txt_end_finish_date.Value).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_end_finish_date.Value.ToString();
        }
        catch
        {
        }
        try
        {
            queryParams.startConfirmDate = 
                Convert.ToDateTime(txt_start_confirm_date.Value).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_start_confirm_date.Value.ToString();
        }
        catch
        {
        }
        try
        {
            queryParams.endConfirmDate = 
                Convert.ToDateTime(txt_end_confirm_date.Value).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_end_confirm_date.Value.ToString();
        }
        catch
        {
        }
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmRepairProtectPlan> pageParams = new PageResult<EqmRepairProtectPlan>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<EqmRepairProtectPlan> lst = GetPageResultData(pageParams);
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
        PageResult<EqmRepairProtectPlan> pageParams = new PageResult<EqmRepairProtectPlan>();
        pageParams.PageSize = -100;
        PageResult<EqmRepairProtectPlan> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "设备维修保养计划");
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
        btnAddSave.Disable(true);
        add_repair_date.Value = "";
        add_repair_time.Value = "1";
        add_response_user.Value = "";
        hidden_add_response_user.Value = "";
        add_repair_protect_plan_content.Value = "";
        add_equip_code.Value = "";
        
        add_need_stop_time.Value = "1";
        add_plan_stop_time.Value = "";
        add_plan_name.Value = "";
        add_plan_month.Value = DateTime.Now.ToString("yyyy-MM");
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击审批按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_finish_Click(object sender, DirectEventArgs e)
    { 
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择要审核的维修保养计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string objID = planRow["ObjID"];
            EqmRepairProtectPlan plan = manager.GetById(objID);
            if (plan.ConfirmDate != null)
            {
                this.msg.Alert("提示", "此计划已被审核,无需再进行审核!").Show();
                return;
            }
            finish_obj_id.Value = objID;
            finish_plan_name.Value = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanType" && SysCode._.ItemCode == plan.PlanName)[0].ItemName;
            finish_plan_month.Value = Convert.ToDateTime(plan.PlanMonth).ToString("yyyy-MM");
            finish_equip_code.Value = equipManager.GetListByWhere(BasEquip._.EquipCode == plan.EquipCode)[0].EquipName;
            finish_repair_date.Value = Convert.ToDateTime(plan.RepairDate).ToString("yyyy-MM-dd");
            finish_repair_time.Value = plan.RepairTime;
            finish_need_stop_time.Value = plan.NeedStopTime;
            finish_plan_stop_time.Value = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanStopTime" && SysCode._.ItemCode == plan.PlanStopTime)[0].ItemName; 
            finish_response_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == plan.ResponseUser)[0].UserName;
            finish_repair_protect_plan_content.Value = plan.RepairProtectPlanContent;
        }
        modify_finish_date.Value = "";
        modify_finish_user.Value = "";
        modify_finish_condition.Value = "";
        hidden_modify_finish_user.Value = "";
        modify_verification.Value = "";
        btnFinishSave.Disable(true);
        this.winFinish.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        EqmRepairProtectPlan plan = manager.GetById(obj_id);
        if (plan.ConfirmDate != null)
        {
            msg.Alert("提示", "已确认计划不能再做修改!").Show();
            return;
        }
        modify_obj_id.Value = plan.ObjID;
        modify_repair_time.Value = plan.RepairTime;
        modify_repair_date.Value = plan.RepairDate;
        try
        {
            modify_response_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == plan.ResponseUser)[0].UserName;
            hidden_modify_response_user.Value = plan.ResponseUser;
        }
        catch (Exception)
        {
        }
        hidden_equip_code.Value = plan.EquipCode;
        modify_equip_code.Value = equipManager.GetListByWhere(BasEquip._.EquipCode == plan.EquipCode)[0].EquipName;
        modify_repair_protect_plan_content.Value = plan.RepairProtectPlanContent;
        modify_need_stop_time.Value = plan.NeedStopTime;
        modify_plan_stop_time.Value = plan.PlanStopTime;
        modify_plan_name.Value = plan.PlanName;
        modify_plan_month.Value = plan.PlanMonth;
        this.winModify.Show();

    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            BasUser currentUser = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0];
            EqmRepairProtectPlan plan = manager.GetById(obj_id);
            if (plan.DeleteFlag == "1")
            {
                return "此问题已经关闭!";
            }
            if(String.IsNullOrEmpty(plan.FinishUser))
            {
                return "未审核的计划不能关闭";
            }
            plan.DeleteFlag = "1";
            this.AppendWebLog("维修保养计划信息关闭", "问题记录序号：" + plan.ObjID);
            manager.Update(plan);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "关闭失败：" + e;
        }
        return "关闭成功";
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
        this.winFinish.Close();
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
            EqmRepairProtectPlan plan = new EqmRepairProtectPlan();
            plan.RepairDate = Convert.ToDateTime(add_repair_date.Value);
            plan.RepairTime = Convert.ToInt32(add_repair_time.Value == null ? "1" : add_repair_time.Value.ToString());
            plan.ResponseUser = hidden_add_response_user.Value.ToString();
            plan.EquipCode = hidden_equip_code.Value.ToString();
            plan.RepairProtectPlanContent = add_repair_protect_plan_content.Value.ToString();
            plan.NeedStopTime = Convert.ToInt32(add_need_stop_time.Value == null ? "1" : add_need_stop_time.Value.ToString());
            plan.PlanStopTime = add_plan_stop_time.Value.ToString();
            plan.PlanName = add_plan_name.Value.ToString();
            plan.PlanMonth = Convert.ToDateTime(add_plan_month.Value).ToString("yyyy-MM");
            plan.DeleteFlag = "0";

            if (!Convert.ToDateTime(plan.RepairDate).ToString("yyyy-MM").Equals(plan.PlanMonth))
            {
                msg.Alert("提示", "计划月份与计划检修日期不符合!").Show();
                return;
            }
            manager.Insert(plan);
            this.AppendWebLog("维修保养计划信息添加", "序号：" + plan.ObjID);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Notify("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击审批的保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnFinishSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            EqmRepairProtectPlan plan = manager.GetById(finish_obj_id.Value);
            plan.FinishCondition = modify_finish_condition.Value.ToString();
            plan.FinishDate = Convert.ToDateTime(modify_finish_date.Value);
            plan.FinishUser = hidden_modify_finish_user.Value.ToString();
            plan.Verification = modify_verification.Value.ToString();
            plan.ConfirmUser = this.Session["UserID"].ToString();
            plan.ConfirmDate = DateTime.Now;
            manager.Insert(plan);
            this.AppendWebLog("维修保养计划信息审核", "序号：" + plan.ObjID);
            pageToolBar.DoRefresh();
            this.winFinish.Close();
            msg.Notify("操作", "保存成功").Show();
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
            EqmRepairProtectPlan plan = new EqmRepairProtectPlan();
            plan.ObjID = Convert.ToInt32(modify_obj_id.Value);
            plan.Attach();
            plan.RepairDate = Convert.ToDateTime(modify_repair_date.Value);
            plan.RepairTime = Convert.ToInt32(modify_repair_time.Value == null ? "1" : modify_repair_time.ToString());
            plan.ResponseUser = hidden_modify_response_user.Value.ToString();
            plan.EquipCode = hidden_equip_code.Value.ToString();
            plan.RepairProtectPlanContent = modify_repair_protect_plan_content.Value.ToString();
            plan.NeedStopTime = Convert.ToInt32(modify_need_stop_time.Value == null ? "1" : modify_need_stop_time.ToString());
            plan.PlanStopTime = modify_plan_stop_time.Value.ToString();
            plan.PlanName = modify_plan_name.Value.ToString();
            plan.PlanMonth = Convert.ToDateTime(modify_plan_month.Value).ToString("yyyy-MM");
            plan.DeleteFlag = "0";
            if (!Convert.ToDateTime(plan.RepairDate).ToString("yyyy-MM").Equals(plan.PlanMonth))
            {
                msg.Alert("提示", "计划月份与计划检修日期不符合!").Show();
                return;
            }
            manager.Update(plan);
            this.AppendWebLog("维修保养计划信息修改", "序号：" + plan.ObjID);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 点击问题记录详细信息
    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_detail(string obj_id)
    {
        EqmRepairProtectPlan record = manager.GetById(obj_id);
        try
        {
            detail_plan_name.Value = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanType" && SysCode._.ItemCode == record.PlanName)[0].ItemName;
            detail_plan_month.Value = Convert.ToDateTime(record.PlanMonth).ToString("yyyy-MM");
            detail_repair_date.Value = Convert.ToDateTime(record.RepairDate).ToString("yyyy-MM-dd");
            detail_repair_time.Value = record.RepairTime;
            detail_response_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.ResponseUser)[0].UserName;
        }
        catch (Exception)
        {
        }

        try
        {
            detail_finish_date.Value = ((DateTime)record.FinishDate).ToString("yyyy-MM-dd");
            detail_finish_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.FinishUser)[0].UserName;
        }
        catch (Exception)
        {
        }

        try
        {
            detail_confirm_date.Value = ((DateTime)record.ConfirmDate).ToString("yyyy-MM-dd"); 
            detail_confirm_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.ConfirmUser)[0].UserName;
        }
        catch (Exception)
        {
        }
        try
        {
            detail_equip_code.Value = equipManager.GetListByWhere(BasEquip._.EquipCode == record.EquipCode)[0].EquipName;
            detail_need_stop_time.Value = record.NeedStopTime;
            detail_plan_stop_time.Value = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanStopTime" && SysCode._.ItemCode == record.PlanStopTime)[0].ItemName; 
            detail_finish_condition.Value = record.FinishCondition;
            detail_repair_protect_plan_content.Value = record.RepairProtectPlanContent;
            detail_verification.Value = record.Verification;
        }
        catch (Exception)
        {

        }
        detail_remark.Value = record.Remark;
        this.winDetail.Show();

    }
    #endregion

    #region 选择设备关联出维修人员
    [Ext.Net.DirectMethod()]
    public string get_repair_user_by_equip(string equipCode, string webform)
    {
        string repairUser = "";
        string UserName = "";
        
        EntityArrayList<BasEquip> equipList = equipManager.GetListByWhere(BasEquip._.EquipCode == equipCode);
        if (equipList.Count > 0)
        {
            repairUser = equipList[0].RepairUser;
            try
            {
                UserName = userManager.GetListByWhere(BasUser._.WorkBarcode == repairUser)[0].UserName;
            }
            catch (Exception)
            {
                UserName = "";
            }
        }
        if ("ADD".Equals(webform))
        {
            add_response_user.Value = UserName;
            hidden_add_response_user.Value = repairUser;
        }
        else
        {
            modify_response_user.Value = UserName;
            hidden_modify_response_user.Value = repairUser;
        }
        return "";
    }
    #endregion

    #region 获取需要停车时间总和
    protected void GetNeedStopTime(object sender, EventArgs e)
    {
        //if (add_plan_name.Value != null &&
        //    add_plan_month.Value != null &&
        //    add_equip_code.Value != null )
        //{
        //    string count = manager.GetNeedStopTimeCount(hidden_equip_code.Value.ToString(), add_plan_name.Value.ToString(), Convert.ToDateTime(add_plan_month.Value).ToString("yyyy-MM"));
        //    add_need_stop_time.Value = String.IsNullOrEmpty(count) ? "0" : count;
        //}
    }
    #endregion
}