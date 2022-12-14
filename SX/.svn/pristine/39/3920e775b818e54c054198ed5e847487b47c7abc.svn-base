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
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using Mesnac.Business.Interface;
using System.Text;
public partial class Manager_Equipment_ProjectRepairRecord_ProjectRepairRecord : Mesnac.Web.UI.Page
{
    protected EqmProjectRepairRecordManager manager = new EqmProjectRepairRecordManager();//业务对象
    protected BasEquipPartRelationManager partManager = new BasEquipPartRelationManager();//业务对象
    protected BasEquipManager equipManager = new BasEquipManager();//业务对象
    protected BasEquipPartInfoManager partInfoManager = new BasEquipPartInfoManager();//业务对象
    protected BasUserManager userManager = new BasUserManager();//业务对象
    protected PptShiftManager shiftManager = new PptShiftManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
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
            EntityArrayList<PptShift> shiftList = shiftManager.GetAllList();
            foreach (PptShift shift in shiftList)
            {
                txt_shift_id.AddItem(shift.ShiftName, shift.ObjID);
            }
            ModifyShiftIdStore.DataSource = shiftList;
            ModifyShiftIdStore.DataBind();
            AddShiftIdStore.DataSource = shiftList;
            AddShiftIdStore.DataBind();
        }
    }


    /// <summary>
    /// 更新新增时关联机台的部件store
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddRepairPartStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        if (hidden_equip_code.Text != "")
        {
            AddRepairPartStore.DataSource = partManager.GetEquipPartsByEquipCode(hidden_equip_code.Text);
            AddRepairPartStore.DataBind();
        }
    }

    /// <summary>
    /// 更新修改时关联机台的部件store
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ModifyRepairPartStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        if (hidden_equip_code.Text != "")
        {
            ModifyRepairPartStore.DataSource = partManager.GetEquipPartsByEquipCode(hidden_equip_code.Text);
            ModifyRepairPartStore.DataBind();
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<EqmProjectRepairRecord> GetPageResultData(PageResult<EqmProjectRepairRecord> pageParams)
    {
        EqmProjectRepairRecordManager.QueryParams queryParams = new EqmProjectRepairRecordManager.QueryParams();
        queryParams.pageParams = pageParams;
        try
        {
            queryParams.repairStartDate = Convert.ToDateTime(txt_repair_start_date.Text).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_repair_start_date.Value.ToString();
        }
        catch
        {
        }
        try
        {
            queryParams.repairEndDate = Convert.ToDateTime(txt_repair_end_date.Text).ToString("yyyy-MM-dd").Equals("0001-01-01") ? "" : txt_repair_end_date.Value.ToString();
        }
        catch
        {
        }
        queryParams.equipCode = hidden_select_equip_code.Text;
        queryParams.shiftID = txt_shift_id.Text;
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmProjectRepairRecord> pageParams = new PageResult<EqmProjectRepairRecord>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<EqmProjectRepairRecord> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
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
        add_repair_start_date.Text = "";
        add_repair_start_time.Text = "";
        add_repair_end_date.Text = "";
        add_repair_end_time.Text = "";
        add_equip_code.Text = "";
        hidden_equip_code.Text = "";
        add_repair_part.Text = "";
        add_shift_id.Text = "";
        add_repair_spend_time.Text = "0";
        add_repair_user.Text = "";
        hidden_repair_user.Text = "";
        add_repair_type.Text = "";
        add_fault_detail.Text = "";
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        EqmProjectRepairRecord repair = manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = objID;
        modify_main_daliy_id.Text = repair.MainDailyID;
        modify_repair_start_date.Text = repair.RepairStartDate.Substring(0, 10);
        modify_repair_start_time.Text = repair.RepairStartDate.Substring(11, 8);
        modify_repair_end_date.Text = repair.RepairEndDate.Substring(0, 10);
        modify_repair_end_time.Text = repair.RepairEndDate.Substring(11, 8);
        modify_equip_code.Text = equipManager.GetListByWhere(BasEquip._.EquipCode == repair.EquipCode)[0].EquipName;
        hidden_equip_code.Text = repair.EquipCode;
        if (hidden_equip_code.Text != "")
        {
            ModifyRepairPartStore.DataSource = partManager.GetEquipPartsByEquipCode(hidden_equip_code.Text);
            ModifyRepairPartStore.DataBind();
            ModifyRepairPartStore.Reload();
        }
        modify_repair_part.Select(repair.RepairPart);
        modify_shift_id.Select(repair.ShiftID);
        modify_repair_spend_time.Text = repair.RepairSpendTime.ToString();
        modify_repair_user.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == repair.RepairUser)[0].UserName;
        hidden_repair_user.Text = repair.RepairUser;
        modify_repair_type.Text = repair.RepairType;
        modify_fault_detail.Text = repair.FaultDetail;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            EqmProjectRepairRecord repair = manager.GetById(objID);
            repair.DeleteFlag = "1";
            manager.Update(repair);
            this.AppendWebLog("项目维护记录删除", "维修编号：" + objID);
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
            string valid = ValidateData(add_repair_start_date.Text, add_repair_start_time.Text, add_repair_end_date.Text, add_repair_end_time.Text,
                hidden_equip_code.Text, add_repair_part.SelectedItem.Value, add_shift_id.SelectedItem.Value, add_repair_spend_time.Text, hidden_repair_user.Text,
                add_repair_type.SelectedItem.Value , add_fault_detail.Text);
            if (!"校验成功!".Equals(valid))
            {
                X.Msg.Alert("提示", valid).Show();
                return;
            }
            EqmProjectRepairRecord repair = new EqmProjectRepairRecord();
            repair.ObjID = manager.GetNextPrimaryKeyValue();
            repair.MainDailyID = manager.GetNextMainDailyID(Convert.ToDateTime(add_repair_start_date.Text).ToString("yyyyMMdd"));
            repair.EquipCode = hidden_equip_code.Text;
            repair.ShiftID = add_shift_id.SelectedItem.Value;
            repair.RepairDate = Convert.ToDateTime(add_repair_start_date.Text).ToString("yyyy-MM-dd");
            repair.RepairStartDate = Convert.ToDateTime(add_repair_start_date.Text).ToString("yyyy-MM-dd") + " " + add_repair_start_time.Text;
            repair.RepairEndDate = Convert.ToDateTime(add_repair_end_date.Text).ToString("yyyy-MM-dd") + " " + add_repair_end_time.Text;
            repair.RepairSpendTime = Convert.ToDecimal(add_repair_spend_time.Text);
            repair.RepairPart = add_repair_part.SelectedItem.Value;
            repair.RepairType = add_repair_type.SelectedItem.Value;
            repair.FaultDetail = add_fault_detail.Text;
            repair.RepairUser = hidden_repair_user.Text;
            repair.RecordDate = DateTime.Now;
            repair.RecordUser = this.UserID;
            repair.DeleteFlag = "0";
            manager.Insert(repair);
            this.AppendWebLog("维修记录增加", "维修记录编号：" + repair.ObjID);
            pageToolBar.DoRefresh();
            area_fault_detail.Text = "";
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
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            string valid = ValidateData(modify_repair_start_date.Text, modify_repair_start_time.Text, modify_repair_end_date.Text, modify_repair_end_time.Text,
                hidden_equip_code.Text, modify_repair_part.SelectedItem.Value, modify_shift_id.SelectedItem.Value, modify_repair_spend_time.Text, hidden_repair_user.Text,
                modify_repair_type.SelectedItem.Value , modify_fault_detail.Text);
            if (!"校验成功!".Equals(valid))
            {
                X.Msg.Alert("提示", valid).Show();
                return;
            }
            EqmProjectRepairRecord repair = manager.GetById(modify_obj_id.Text);
            repair.EquipCode = hidden_equip_code.Text;
            repair.ShiftID = modify_shift_id.SelectedItem.Value;
            repair.RepairDate = Convert.ToDateTime(modify_repair_start_date.Text).ToString("yyyy-MM-dd");
            repair.RepairStartDate = Convert.ToDateTime(modify_repair_start_date.Text).ToString("yyyy-MM-dd") + " " + modify_repair_start_time.Text;
            repair.RepairEndDate = Convert.ToDateTime(modify_repair_end_date.Text).ToString("yyyy-MM-dd") + " " + modify_repair_end_time.Text;
            repair.RepairSpendTime = Convert.ToDecimal(modify_repair_spend_time.Text);
            repair.RepairPart = modify_repair_part.SelectedItem.Value;
            repair.RepairType = modify_repair_type.SelectedItem.Value;
            repair.FaultDetail = modify_fault_detail.Text;
            repair.RepairUser = hidden_repair_user.Text;
            repair.RecordDate = DateTime.Now;
            repair.RecordUser = this.UserID;
            repair.DeleteFlag = "0";
            manager.Update(repair);
            this.AppendWebLog("维修记录信息修改", "维修记录编号：" + modify_obj_id.Text);
            pageToolBar.DoRefresh();
            area_fault_detail.Text = "";
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

    #region 校验方法
    private string ValidateData(string startDate, string startTime, string endDate, string endTime, string equipCode, string repairPart,
        string shiftId, string repairSpendTime, string repairUser, string repairType, string faultDetail)
    {
        if (Convert.ToDateTime(startDate).ToString("yyyy-MM-dd").Equals("0001-01-01"))
        {
            return "开始日期不能为空!";
        }
        if (Convert.ToDateTime(endDate).ToString("yyyy-MM-dd").Equals("0001-01-01"))
        {
            return "开始日期不能为空!";
        }
        if (string.Empty.Equals(startTime))
        {
            return "开始日期不能为空!";
        }
        if (string.Empty.Equals(endTime))
        {
            return "开始日期不能为空!";
        }
        DateTime startDt = Convert.ToDateTime(Convert.ToDateTime(startDate).ToString("yyyy-MM-dd") + " " + startTime);
        DateTime endDt = Convert.ToDateTime(Convert.ToDateTime(endDate).ToString("yyyy-MM-dd") + " " + endTime);
        if (startDt >= endDt)
        {
            return "开始日期不能大于结束日期!";
        }
        if (string.Empty.Equals(equipCode))
        {
            return "维修机台不能为空!";
        }
        if (string.Empty.Equals(repairPart))
        {
            return "维修部件不能为空!";
        }
        if (string.Empty.Equals(shiftId))
        {
            return "生产班次不能为空!";
        }
        if (string.Empty.Equals(repairSpendTime))
        {
            return "维修工时不能为空!";
        }
        if (string.Empty.Equals(repairUser))
        {
            return "维修人不能为空!";
        }
        if (string.Empty.Equals(repairType))
        {
            return "维修类型不能为空!";
        }
        if (faultDetail.Length > 480)
        {
            return "故障明细信息长度不能大于480字";
        }
        return "校验成功!";
    }
    #endregion

    protected void Row_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] dics = JSON.Deserialize<Dictionary<string, string>[]>(json);
        this.area_fault_detail.Text = dics[0]["FaultDetail"].ToString();
    }
}