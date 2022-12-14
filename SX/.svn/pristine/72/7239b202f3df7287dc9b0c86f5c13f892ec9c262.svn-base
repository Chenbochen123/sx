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

public partial class Manager_ProducingPlan_PlanEntering_PlanExecMgr : Mesnac.Web.UI.Page
{
    protected PptPlanMgrManager manager = new PptPlanMgrManager();//业务对象
    protected BasMaterialManager materialManager = new BasMaterialManager();//业务对象
    protected BasEquipManager equipManager = new BasEquipManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected PmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
    protected PptPlanManager planManager = new PptPlanManager();
    protected PptShiftTimeManager pptShiftTimeManager = new PptShiftTimeManager();
    protected IBasWorkShopManager basWorkShopManager = new BasWorkShopManager();
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_reload" };
            审核 = new SysPageAction() { ActionID = 3, ActionName = "btn_audit" };
            反审核 = new SysPageAction() { ActionID = 4, ActionName = "btn_unAudit" };
            复制计划 = new SysPageAction() { ActionID = 5, ActionName = "btn_copyPlan" };
            导出 = new SysPageAction() { ActionID = 6, ActionName = "btnExport" };
            下达计划 = new SysPageAction() { ActionID = 7, ActionName = "btn_createPlan" };
            修改 = new SysPageAction() { ActionID = 8, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 9, ActionName = "Delete" };
            导入计划 = new SysPageAction() { ActionID = 10, ActionName = "btnImport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 反审核 { get; private set; } //必须为 public
        public SysPageAction 复制计划 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 下达计划 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 导入计划 { get; private set; } //必须为 public
    }
    #endregion
    private const string constSelectAllText = "---请选择---";
    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest&&!IsPostBack)
        {
            txt_start_plan_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_end_plan_date.Text = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");

            #region 设备类型
            Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
            EntityArrayList<BasWorkShop> lstBasWorkShop = basWorkShopManager.GetListByWhere(BasWorkShop._.DeleteFlag == "0");
            txtWorkShopCode.Items.Clear();
            txtWorkShopCode.Items.Add(allitem);
            foreach (BasWorkShop m in lstBasWorkShop)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkShopName;
                txtWorkShopCode.Items.Add(item);
            }
            if (txtWorkShopCode.Items.Count > 0)
            {
                txtWorkShopCode.Text = (txtWorkShopCode.Items[0].Value);
            }
            #endregion
        }
    }
    #endregion
    protected void EquipCodeRefresh(object sender, StoreReadDataEventArgs e)
    {
        List<object> data = new List<object>();
        EntityArrayList<BasEquip> equipList = equipManager.GetListByWhereAndOrder(BasEquip._.WorkShopCode == txtWorkShopCode.Text.Replace(constSelectAllText, "0") & BasEquip._.DeleteFlag == 0& BasEquip._.EquipType<"03",BasEquip._.EquipName.Asc);
        cbo_equip_code.Items.Clear();
        data.Add(new { Id = constSelectAllText, Name = constSelectAllText });
        foreach (BasEquip equip in equipList)
        {
            string id = equip.EquipCode;
            string name = equip.EquipName;

            data.Add(new { Id = id, Name = name });
        }
        this.equipCodeStore.DataSource = data;
        this.equipCodeStore.DataBind();
    }


    #region 分页相关方法
    private PageResult<PptPlanMgr> GetPageResultData(PageResult<PptPlanMgr> pageParams)
    {
        PptPlanMgrManager.QueryParams queryParams = new PptPlanMgrManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.WorkShopCode = txtWorkShopCode.Text.Replace(constSelectAllText, "");
        queryParams.materialCode = hidden_material_code.Text.TrimEnd().TrimStart();
        queryParams.equipCode = cbo_equip_code.Text.Replace(constSelectAllText, "");
        queryParams.startPlanDate = txt_start_plan_date.Text;
        queryParams.endPlanDate = txt_end_plan_date.Text;
        queryParams.deleteFlag = "0";
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptPlanMgr> pageParams = new PageResult<PptPlanMgr>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjId";

        PageResult<PptPlanMgr> lst = GetPageResultData(pageParams);
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
        
        PageResult<PptPlanMgr> pageParams = new PageResult<PptPlanMgr>();
        pageParams.PageSize = -100;
        PageResult<PptPlanMgr> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "计划信息");
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

        winAdd.Icon = Icon.MonitorAdd;
        winAdd.Title = "添加计划信息";
        add_plan_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
        add_equip_code.Text = "";
        hidden_equip_code.Text = "";
        add_material_code.Text = "";
        add_recipe_name.Text = "";
        add_erp_code.Text = "";
        add_plan_total_count.Text = "0";
        add_recommend_total_count.Text = "0";
        add_actual_one_plan.Text = "0";
        add_actual_one_remark.Text = "";
        add_actual_two_plan.Text = "0";
        add_actual_two_remark.Text = "";
        add_actual_three_plan.Text = "0";
        add_actual_three_remark.Text = "";
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
        PptPlanMgr planMgr = manager.GetById(Convert.ToInt32(objID));
        if (planMgr.CreatePlanFlag == "1")
        {
            this.msg.Alert("提示", "已下达计划禁止修改!").Show();
            return;
        }
        if (planMgr.AuditFlag == "1")
        {
            this.msg.Alert("提示", "已审核计划禁止修改!").Show();
            return;
        }
        modify_obj_id.Text = objID;
        modify_plan_date.Text = planMgr.PlanDate.ToString();
        modify_equip_code.Text = equipManager.GetListByWhere(BasEquip._.EquipCode == planMgr.EquipCode)[0].EquipName;
        hidden_equip_code.Value = planMgr.EquipCode;

        modify_material_code.AddItem(materialManager.GetListByWhere(BasMaterial._.MaterialCode == planMgr.MaterialCode)[0].MaterialName, planMgr.MaterialCode);
        modify_material_code.Select(0);

        modify_recipe_name.Select(planMgr.RecipeName);
        modify_erp_code.Text = planMgr.ERPCode;
        modify_plan_total_count.Text = planMgr.PlanTotalCount.ToString();
        modify_recommend_total_count.Text = planMgr.RecommendTotalCount.ToString();
        modify_actual_one_plan.Text = planMgr.ActualOnePlan.ToString();
        modify_actual_one_remark.Text = planMgr.ActualOneRemark.ToString();
        modify_actual_two_plan.Text = planMgr.ActualTwoPlan.ToString();
        modify_actual_two_remark.Text = planMgr.ActualTwoRemark.ToString();
        modify_actual_three_plan.Text = planMgr.ActualThreePlan.ToString();
        modify_actual_three_remark.Text = planMgr.ActualThreeRemark.ToString();
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
            PptPlanMgr planMgr = manager.GetById(objID);
            if (planMgr.AuditFlag == "1")
            {
                return "已审核计划禁止删除!";
            }
            if (planMgr.CreatePlanFlag == "1")
            {
                return "已下达计划禁止删除!";
            }
            planMgr.DeleteFlag = "1";
            manager.Update(planMgr);
            this.AppendWebLog("计划信息删除", "计划编号：" + objID);
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
        this.winCreate.Close();
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
            #region 添加校验
            if (add_plan_date.Text == "")
            {
                this.msg.Alert("提示", "计划日期不能为空!").Show();
                return;
            }
            if (hidden_equip_code.Text == "")
            {
                this.msg.Alert("提示", "机台名称不能为空!").Show();
                return;
            }
            Regex reg = new Regex(@"^\d{13}$");
            if (!reg.IsMatch(add_material_code.Text))
            {
                this.msg.Alert("提示", "请选择有效的物料名称!").Show();
                return;
            }
            if (add_recipe_name.Text == "")
            {
                this.msg.Alert("提示", "配方名称不能为空!").Show();
                return;
            }
            EntityArrayList<PptPlanMgr> planMgrList 
                = manager.GetListByWhere(PptPlanMgr._.PlanDate == add_plan_date.Text
                                    && PptPlanMgr._.EquipCode == hidden_equip_code.Text
                                    && PptPlanMgr._.MaterialCode == add_material_code.Text
                                    && PptPlanMgr._.AuditFlag == "0"
                                    && PptPlanMgr._.DeleteFlag == "0");
            if (planMgrList.Count > 0)
            {
                X.Msg.Alert("提示", "已存在『同日期、同机台、同物料』未审核的计划内容!").Show();
                return;
            }

            //执行完毕的计划不能新建、复制
            EntityArrayList<PptPlan> planList
                = planManager.GetListByWhere(PptPlan._.PlanDate == add_plan_date.Text
                && PptPlan._.RecipeEquipCode == hidden_equip_code.Text
                && PptPlan._.RecipeMaterialCode == add_material_code.Text
                && PptPlan._.PlanState == "5"//完成计划
                && PptPlan._.CreatePlanFlag == "1");
            if (planList.Count > 0)
            {
                this.msg.Alert("提示", "下达的『同日期、同机台、同物料』计划已经完成!").Show();
                return;
            }

            #endregion
            PptPlanMgr planMgr = new PptPlanMgr();
            planMgr.PlanDate = DateTime.Parse(add_plan_date.Text);
            planMgr.EquipCode = hidden_equip_code.Text;
            planMgr.MaterialCode = add_material_code.Text;
            planMgr.RecipeName = add_recipe_name.Text;
            planMgr.ERPCode = add_erp_code.Text;
            planMgr.RecommendTotalCount = Convert.ToInt32(add_recommend_total_count.Text);
            planMgr.ActualOnePlan = Convert.ToInt32(add_actual_one_plan.Text);
            planMgr.ActualOneRemark = add_actual_one_remark.Text;
            planMgr.ActualTwoPlan = Convert.ToInt32(add_actual_two_plan.Text);
            planMgr.ActualTwoRemark = add_actual_two_remark.Text;
            planMgr.ActualThreePlan = Convert.ToInt32(add_actual_three_plan.Text);
            planMgr.ActualThreeRemark = add_actual_three_remark.Text;
            planMgr.PlanTotalCount = planMgr.ActualOnePlan + planMgr.ActualTwoPlan + planMgr.ActualThreePlan;
            planMgr.AuditFlag = "0";
            planMgr.CreatePlanFlag = "0";
            planMgr.DeleteFlag = "0";
            manager.Insert(planMgr);
            this.AppendWebLog("计划信息增加", "计划内容："
                + planMgr.PlanDate + "|" + planMgr.EquipCode + "|" + planMgr.MaterialCode + "|" + "|"
                + planMgr.ActualOnePlan + "|" + "|" + planMgr.ActualTwoPlan + "|" + planMgr.ActualThreePlan);
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
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            #region 修改校验
            if (modify_plan_date.Text == "")
            {
                this.msg.Alert("提示", "计划日期不能为空!").Show();
                return;
            }
            if (hidden_equip_code.Text == "")
            {
                this.msg.Alert("提示", "机台名称不能为空!").Show();
                return;
            }
            Regex reg = new Regex(@"^\d{13}$");
            if (!reg.IsMatch(modify_material_code.Text))
            {
                this.msg.Alert("提示", "请选择有效的物料名称!").Show();
                return;
            }
            if (modify_recipe_name.Text == "")
            {
                this.msg.Alert("提示", "配方名称不能为空!").Show();
                return;
            }

            //执行完毕的计划不能修改
            EntityArrayList<PptPlan> planList
                = planManager.GetListByWhere(PptPlan._.PlanDate == modify_plan_date.Text
                && PptPlan._.RecipeEquipCode == hidden_equip_code.Text
                && PptPlan._.RecipeMaterialCode == modify_material_code.Text
                && PptPlan._.PlanState == "5"//完成计划
                && PptPlan._.CreatePlanFlag == "1");
            if (planList.Count > 0)
            {
                this.msg.Alert("提示", "下达的『同日期、同机台、同物料』计划已经完成!").Show();
                return;
            }

            #endregion
            PptPlanMgr planMgr = manager.GetById(modify_obj_id.Text);
            planMgr.PlanDate = DateTime.Parse(modify_plan_date.Text);
            planMgr.EquipCode = hidden_equip_code.Text;
            planMgr.MaterialCode = modify_material_code.Text;
            planMgr.RecipeName = modify_recipe_name.Text;
            planMgr.ERPCode = modify_erp_code.Text;
            planMgr.RecommendTotalCount = Convert.ToInt32(modify_recommend_total_count.Text);
            planMgr.ActualOnePlan = Convert.ToInt32(modify_actual_one_plan.Text);
            planMgr.ActualOneRemark = modify_actual_one_remark.Text;
            planMgr.ActualTwoPlan = Convert.ToInt32(modify_actual_two_plan.Text);
            planMgr.ActualTwoRemark = modify_actual_two_remark.Text;
            planMgr.ActualThreePlan = Convert.ToInt32(modify_actual_three_plan.Text);
            planMgr.ActualThreeRemark = modify_actual_three_remark.Text;
            planMgr.PlanTotalCount = planMgr.ActualOnePlan + planMgr.ActualTwoPlan + planMgr.ActualThreePlan;
            manager.Update(planMgr);
            this.AppendWebLog("计划信息修改", "计划内容："
                + planMgr.PlanDate + "|" + planMgr.EquipCode + "|" + planMgr.MaterialCode + "|" + "|"
                + planMgr.ActualOnePlan + "|" + "|" + planMgr.ActualTwoPlan + "|" + planMgr.ActualThreePlan);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 下拉框store加载数据方法
    /// <summary>
    /// 更新GridPanel中的物料名称列表框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddRecipeMaterialNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string equipCode = hidden_equip_code.Value.ToString();
        EntityArrayList<PmtRecipe> lst = new EntityArrayList<PmtRecipe>();
        lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, add_material_code.Text);
        this.AddRecipeMaterialNameStore.DataSource = lst;
        this.AddRecipeMaterialNameStore.DataBind();
    }

    /// <summary>
    /// 更新GridPanel中的物料名称列表框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CreateRecipeMaterialNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string equipCode = hidden_equip_code.Value.ToString();
        EntityArrayList<PmtRecipe> lst = new EntityArrayList<PmtRecipe>();
        lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, create_material_code.Text);
        this.CreateRecipeMaterialNameStore.DataSource = lst;
        this.CreateRecipeMaterialNameStore.DataBind();
    }


    /// <summary>
    /// 更新GridPanel中的物料名称列表框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ModifyRecipeMaterialNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string equipCode = hidden_equip_code.Value.ToString();
        EntityArrayList<PmtRecipe> lst = new EntityArrayList<PmtRecipe>();
        lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, modify_material_code.Text);
        this.ModifyRecipeMaterialNameStore.DataSource = lst;
        this.ModifyRecipeMaterialNameStore.DataBind();
    }

    /// <summary>
    /// 添加当选择物料后获得配方名称
    /// yuany
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void FillAddRecipeComboBox(object sender, DirectEventArgs e)
    {
        string recipeMaterialCode = (add_material_code.Value == null || add_material_code.Value.ToString() == "") ? "" : add_material_code.SelectedItem.Value.ToString();
        string equipCode = hidden_equip_code.Value.ToString();
        EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == recipeMaterialCode & PmtRecipe._.RecipeEquipCode == equipCode & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
        AddRecipeNameStore.DataSource = lst;
        AddRecipeNameStore.DataBind();
        if (lst.Count == 1)
        {
            add_recipe_name.Value = lst[0].RecipeName;
        }
        //ERPCODE
        EntityArrayList<BasMaterial> materialList = materialManager.GetListByWhere(BasMaterial._.MaterialCode == recipeMaterialCode);
        add_erp_code.Text = materialList.Count > 0 ? materialList[0].ERPCode : "";
    }

    /// <summary>
    /// 修改当选择物料后获得配方名称
    /// yuany
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void FillModifyRecipeComboBox(object sender, DirectEventArgs e)
    {
        string recipeMaterialCode = (modify_material_code.Value == null || modify_material_code.Value.ToString() == "") ? "" : modify_material_code.SelectedItem.Value.ToString();
        string equipCode = hidden_equip_code.Value.ToString();
        EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == recipeMaterialCode & PmtRecipe._.RecipeEquipCode == equipCode & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
        ModifyRecipeNameStore.DataSource = lst;
        ModifyRecipeNameStore.DataBind();
        if (lst.Count == 1)
        {
            modify_recipe_name.Value = lst[0].RecipeName;
        }
        //ERPCODE
        EntityArrayList<BasMaterial> materialList = materialManager.GetListByWhere(BasMaterial._.MaterialCode == recipeMaterialCode);
        modify_erp_code.Text = materialList.Count > 0 ? materialList[0].ERPCode : "";
    }

    /// <summary>
    /// 修改当选择物料后获得配方名称
    /// yuany
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void FillCreateRecipeComboBox(object sender, DirectEventArgs e)
    {
        string recipeMaterialCode = (create_material_code.Value == null || create_material_code.Value.ToString() == "") ? "" : create_material_code.SelectedItem.Value.ToString();
        string equipCode = hidden_equip_code.Value.ToString();
        EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == recipeMaterialCode & PmtRecipe._.RecipeEquipCode == equipCode & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
        CreateRecipeNameStore.DataSource = lst;
        CreateRecipeNameStore.DataBind();
        if (lst.Count == 1)
        {
            create_recipe_name.Value = lst[0].RecipeName;
        }
    }
    #endregion

    #region 审核相关操作
    /// <summary>
    /// 审核计划
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAuditPlan_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要审核的计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            if (planRow["AuditFlag"] == "已审核")
            {
                this.msg.Alert("提示","已审核计划无需再次审核!").Show();
                return;
            }
            if (planRow["CreatePlanFlag"] == "已下达")
            {
                this.msg.Alert("提示", "此计划已下达，不能审核!").Show();
                return;
            }
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            planMgr.AuditFlag = "1";
            planMgr.AuditDate = DateTime.Now;
            planMgr.Auditor = HttpContext.Current.Session["UserID"] != null ? HttpContext.Current.Session["UserID"].ToString() : null;
            manager.Update(planMgr);
        }
        pageToolBar.DoRefresh();
        this.msg.Notify("提示", "审核成功!").Show();
    }

    /// <summary>
    /// 反审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnUnAuditPlan_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);

        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要反审核的计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            if (planRow["AuditFlag"] == "未审核")
            {
                this.msg.Alert("提示", "未审核计划无需反审核!").Show();
                return;
            }
            if (planRow["CreatePlanFlag"] == "已下达")
            {
                this.msg.Alert("提示", "已下达计划不能进行反审核!").Show();
                return;
            }
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            planMgr.AuditFlag = "0";
            planMgr.CreatePlanFlag = "0";
            planMgr.AuditDate = null;
            planMgr.Auditor = null;
            manager.Update(planMgr);
        }
        pageToolBar.DoRefresh();
        this.msg.Notify("提示", "反审核成功!").Show();
    }

    #endregion

    #region 复制计划相关操作
    [Ext.Net.DirectMethod]
    public string BtnCopyPlan_Click(string Values)
    {
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(Values);
        if (planDic.Length < 1)
        {
            return "请选择需要复制的计划!";
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            PptPlanMgr planMgr = manager.GetById(planRow["ObjID"]);
            PptPlanMgr newPlan = new PptPlanMgr();
            newPlan.PlanDate = DateTime.Now.Date;
            newPlan.EquipCode = planMgr.EquipCode;
            newPlan.MaterialCode = planMgr.MaterialCode;
            newPlan.RecipeName = planMgr.RecipeName;
            newPlan.ERPCode = planMgr.ERPCode;
            newPlan.PlanTotalCount = planMgr.PlanTotalCount;
            newPlan.RecommendTotalCount = planMgr.RecommendTotalCount;
            newPlan.ActualOnePlan = planMgr.ActualOnePlan;
            newPlan.ActualOneRemark = planMgr.ActualOneRemark;
            newPlan.ActualTwoPlan = planMgr.ActualTwoPlan;
            newPlan.ActualTwoRemark = planMgr.ActualTwoRemark;
            newPlan.ActualThreePlan = planMgr.ActualThreePlan;
            newPlan.ActualThreeRemark = planMgr.ActualThreeRemark;
            newPlan.AuditFlag = "0";
            newPlan.CreatePlanFlag = "0";
            newPlan.DeleteFlag = "0";
            newPlan.AddFlag = "0";
            manager.Insert(newPlan);
        }
        return "";
    }
    #endregion

    #region 点击进行批量删除
    [Ext.Net.DirectMethod]
    public void BtnAllDelete_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length < 1)
        {
            msg.Alert("提示","请选择需要删除的计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            PptPlanMgr planMgr = manager.GetById(planRow["ObjID"]);
            if (planMgr.AuditFlag == "1")
            {
                 msg.Alert("提示","已审核计划禁止删除!").Show();
                 return;
            }
            if (planMgr.CreatePlanFlag == "1")
            {
                 msg.Alert("提示","已下达计划禁止删除!").Show();
                 return;
            }
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            PptPlanMgr planMgr = manager.GetById(planRow["ObjID"]);
            planMgr.DeleteFlag = "1";
            manager.Update(planMgr);
            this.AppendWebLog("计划信息删除", "计划编号：" + planRow["ObjID"]);
        }
        pageToolBar.DoRefresh();
        msg.Notify("提示", "批量删除成功").Show();

    }
    #endregion

    #region 下达计划相关操作
    public void BtnCreatePlan_Click(object sender, DirectEventArgs e)
    {
        create_plan_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        create_equip_code.Text = "";
        hidden_equip_code.Text = "";
        create_material_code.Text = "";
        create_recipe_name.Text = "";
        this.winCreate.Show();
    }

    public void BtnCreatePlanSave_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要下达的计划!").Show();
            return;
        }
        #region 下达计划信息筛选校验
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            string noiceStr = "计划时间：" + ((DateTime)planMgr.PlanDate).ToString("yyyy-MM-dd") + "<br>"
               + "计划机台：" + planRow["EquipCode"] + "<br>"
               + "物料名称：" + planRow["MaterialCode"] + "<br>"
               + "配方编号：" + planRow["RecipeName"];
            if (planRow["AuditFlag"] == "未审核")
            {
                this.msg.Alert("提示", noiceStr + "<br>此计划未审核!").Show();
                return;
            }
            if (planRow["CreatePlanFlag"] == "已下达")
            {
                this.msg.Alert("提示", noiceStr + "<br>此计划不能再次下达!").Show();
                return;
            }
            if (planMgr.PlanDate <= DateTime.Now.AddDays(-1))
            {
                this.msg.Alert("提示", noiceStr + "<br>此计划时间已过期!").Show();
                return;
            }
           
        }
        #endregion

        #region 每条计划的下达操作
        String NeedXC = ""; int nxc = 0;
         string sql = "select itemcode from SysUserCtrl where typeid ='XCCtrl'";
         DataSet ds = pptShiftTimeManager.GetBySql(sql).ToDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0][0].ToString() == "1")
            { nxc = 1; }
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            string equipCode = planMgr.EquipCode;
            string planDate = ((DateTime)planMgr.PlanDate).ToString("yyyy-MM-dd");
            string materialcode = planMgr.MaterialCode;
            string recipeName = planMgr.RecipeName;
            int planZao = planMgr.ActualOnePlan != null ? Convert.ToInt32(planMgr.ActualOnePlan) : 0;
            int planZhong = planMgr.ActualTwoPlan != null ? Convert.ToInt32(planMgr.ActualTwoPlan) : 0;
            int planYe = planMgr.ActualThreePlan != null ? Convert.ToInt32(planMgr.ActualThreePlan) : 0;
            string proID = equipCode.Substring(1, 1);
            string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, planDate).Rows[0]["Num"].ToString();
            if (Convert.ToInt32(count) <= 0)
            {
                this.msg.Alert("提示", "还没有设置" + planDate + "班次信息！请先设置班次信息!").Show();
                return;
            }
            //1班
            string actualOnePlanId = CreatePlanFunction(planZao, equipCode, planDate, materialcode, recipeName, "1");
            //2班
            string actualTwoPlanId = CreatePlanFunction(planZhong, equipCode, planDate, materialcode, recipeName, "2");
            //3班
            string actualThreePlanId = CreatePlanFunction(planYe, equipCode, planDate, materialcode, recipeName, "3");
            if (nxc == 1)
            {
                String Planid = actualOnePlanId;
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("@PlanID", Planid);
                String materialname = "";
                DataSet dss = planManager.GetDataSetByStoreProcedure("ProcPlanNeedXCCheck", dict);
                if (dss.Tables[0].Rows[0][0].ToString().IndexOf("ok") < 0)
                {
                     sql = "  update pptplan set PlanState = '1' where planid ='" + Planid + "' ";
                     ds = planManager.GetBySql(sql).ToDataSet();
                
                     sql = "  select equipname from basequip where equipcode ='" + equipCode + "' ";
                     ds = planManager.GetBySql(sql).ToDataSet();
                    String ename = ds.Tables[0].Rows[0][0].ToString();
                    sql = "  select shiftname from PptShift where objid ='1' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    String sname = ds.Tables[0].Rows[0][0].ToString();

                    sql = "  select materialname from Basmaterial where materialcode ='" + materialcode + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    materialname = ds.Tables[0].Rows[0][0].ToString();

                    NeedXC = ename + materialname + sname + dss.Tables[0].Rows[0][0].ToString();
                 
                }

                Planid = actualTwoPlanId;
                dict = new Dictionary<string, object>();
                dict.Add("@PlanID", Planid);

                 dss = planManager.GetDataSetByStoreProcedure("ProcPlanNeedXCCheck", dict);
                 if (dss.Tables[0].Rows[0][0].ToString().IndexOf("ok") < 0)
                {
                    sql = "  update pptplan set PlanState = '1' where planid ='" + Planid + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();

                    sql = "  select equipname from basequip where equipcode ='" + equipCode + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    String ename = ds.Tables[0].Rows[0][0].ToString();
                    sql = "  select shiftname from PptShift where objid ='2' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    String sname = ds.Tables[0].Rows[0][0].ToString();
                    sql = "  select materialname from Basmaterial where materialcode ='" + materialcode + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    materialname = ds.Tables[0].Rows[0][0].ToString();
                    NeedXC = ename + materialname + sname + dss.Tables[0].Rows[0][0].ToString();

                }

                Planid = actualThreePlanId;
              dict = new Dictionary<string, object>();
                dict.Add("@PlanID", Planid);

                 dss = planManager.GetDataSetByStoreProcedure("ProcPlanNeedXCCheck", dict);
                 if (dss.Tables[0].Rows[0][0].ToString().IndexOf("ok") < 0)
                {
                    sql = "  update pptplan set PlanState = '1' where planid ='" + Planid + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();

                    sql = "  select equipname from basequip where equipid ='" + equipCode + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    String ename = ds.Tables[0].Rows[0][0].ToString();
                    sql = "  select shiftname from PptShift where objid ='3' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    String sname = ds.Tables[0].Rows[0][0].ToString();
                    sql = "  select materialname from Basmaterial where materialcode ='" + materialcode + "' ";
                    ds = planManager.GetBySql(sql).ToDataSet();
                    materialname = ds.Tables[0].Rows[0][0].ToString();
                    NeedXC = ename + materialname + sname + dss.Tables[0].Rows[0][0].ToString();

                }

            }
            planMgr.CreatePlanFlag = "1";
            planMgr.ActualOnePlanID = actualOnePlanId;
            planMgr.ActualTwoPlanID = actualTwoPlanId;
            planMgr.ActualThreePlanID = actualThreePlanId;
            manager.Update(planMgr);

        }
        #endregion

        pageToolBar.DoRefresh();
        if (NeedXC == "")
        {
            this.msg.Notify("提示", "计划下达成功!").Show();
        }
        else
        { X.Js.Alert( "计划已下达除了" + NeedXC); }

      
    }

    /// <summary>
    /// 下达计划的方法
    /// </summary>
    /// <param name="planNum">计划数</param>
    /// <param name="equipCode">机台号</param>
    /// <param name="planDate">计划日期</param>
    /// <param name="materialcode">物料号</param>
    /// <param name="recipeName">配方名称</param>
    /// <param name="proID">工序号</param>
    /// <param name="shiftId">班次</param>
    ///   <param name="NeedXC">需要洗车</param>
    private string CreatePlanFunction(int planNum, string equipCode, string planDate, string materialcode, string recipeName, string shiftId)
    {
        string proID = equipCode.Substring(1, 1);
        string planNo = planManager.GetGetMaxPlanId(planDate, equipCode, shiftId);
        string ClassID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftId, proID, planDate).Rows[0]["ShiftClassID"].ToString();
        EntityArrayList<PptPlan> PlanList = planManager.GetListByWhere(PptPlan._.PlanDate == planDate
              && PptPlan._.RecipeEquipCode == equipCode
              && PptPlan._.RecipeMaterialCode == materialcode
              && PptPlan._.RecipeName == recipeName
              && PptPlan._.ShiftID == shiftId
              && PptPlan._.ClassID == ClassID
              && PptPlan._.PlanState != "5"
              && PptPlan._.CreatePlanFlag == "1");//未完成的生成计划,无(count = 0)则插入
        if ( PlanList.Count == 0)
        {
            //插入操作
            if (planNum == 0)
            {
                return "";
            }
            PptPlan pptPlan = new PptPlan();
            WhereClip where = PptPlan._.PlanDate == planDate 
                & PptPlan._.RecipeEquipCode == equipCode 
                & PptPlan._.ShiftID == shiftId 
                & PptPlan._.ClassID == ClassID;
            OrderByClip order = PptPlan._.PriLevel.Desc;
            EntityArrayList<PptPlan> lst = planManager.GetListByWhereAndOrder(where, order);
            if (lst.Count > 0)
            {
                pptPlan.PriLevel = lst.Count + 1;//插入的顺序
            }
            else
            {
                pptPlan.PriLevel = 1;
            }
            order = PptPlan._.SerialNum.Desc;
            lst = planManager.GetListByWhereAndOrder(where, order);
            if (lst.Count > 0)
            {
                pptPlan.SerialNum = lst[0].SerialNum + 1;
            }
            else
            {
                pptPlan.SerialNum = 1;
            }
            pptPlan.PlanDate = Convert.ToDateTime(planDate);
            pptPlan.PlanSource = "N";
            pptPlan.UrgencyState = 2;//计划紧急状态
            pptPlan.SmallCreate = 0;
            pptPlan.RecipeEquipCode = equipCode;
            pptPlan.ClassID = Convert.ToInt32(ClassID);//获取当前班次id;
            pptPlan.PlanID = planNo;
            pptPlan.ShiftID = Convert.ToInt32(shiftId);
            pptPlan.PlanState = "1";
            pptPlan.OperDatetime = DateTime.Now;
            pptPlan.OperCode = this.UserID;//获取当前登录用户工号
            pptPlan.DeleteFlag = "0";
            pptPlan.CreatePlanFlag = "1";
            pptPlan.RecipeMaterialCode = materialcode;
            pptPlan.RecipeName = recipeName;
            pptPlan.PlanNum = Convert.ToInt32(planNum);
            pptPlan.PlanState = "2";//下达计划状态为2
            EntityArrayList<PmtRecipe> recipeLst = pmtRecipeManager.GetListByWhere(
            PmtRecipe._.RecipeMaterialCode == pptPlan.RecipeMaterialCode &
            PmtRecipe._.RecipeName == pptPlan.RecipeName &
            PmtRecipe._.RecipeEquipCode == equipCode &
            PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
            if (recipeLst.Count > 0)
            {
                pptPlan.RecipeMaterialName = recipeLst[0].RecipeMaterialName;
                pptPlan.RecipeVersionID = recipeLst[0].RecipeVersionID;
                pptPlan.TotalWeight = recipeLst[0].LotTotalWeight;
                pptPlan.PlanWeight = pptPlan.PlanNum * pptPlan.TotalWeight;
                pptPlan.RecipeType = recipeLst[0].RecipeType;
                pptPlan.RecipeUserVersion = recipeLst[0].RecipeUserVersion;
            }
            planManager.Insert(pptPlan);
          
            this.AppendWebLog("生产计划录入-插入下达计划", "物料名称：" + pptPlan.RecipeMaterialName +
                ",配方编号:" + pptPlan.RecipeName +
                ",生产车数:" + pptPlan.PlanNum +
                ",操作人:" + this.UserID);
           
            return pptPlan.PlanID;
        }
        else
        {
            PptPlan plan = PlanList[0];
            plan.PlanNum = plan.PlanNum + planNum;
            planManager.Update(plan);
            this.AppendWebLog("生产计划录入-更改下达计划", "物料名称：" + plan.RecipeMaterialName +
               ",配方编号:" + plan.RecipeName +
               ",生产车数:" + plan.PlanNum +
               ",操作人:" + this.UserID);
            return plan.PlanID;
        }
    }
    #endregion

    #region 校验方法
    //校验正在执行计划不能小于完成数
    private bool ValidatePlanFunction(int planNum, string equipCode, string planDate, string materialcode, string recipeName, string shiftId)
    {
        string proID = hidden_equip_code.Text.Substring(1, 1);
        string planNo = planManager.GetGetMaxPlanId(planDate, equipCode, shiftId);
        string zaoClassID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftId, proID, planDate).Rows[0]["ShiftClassID"].ToString();
        EntityArrayList<PptPlan> PlanList = planManager.GetListByWhere(PptPlan._.PlanDate == create_plan_date.Text
              && PptPlan._.RecipeEquipCode == hidden_equip_code.Text
              && PptPlan._.RecipeMaterialCode == create_material_code.SelectedItem.Value
              && PptPlan._.RecipeName == create_recipe_name.SelectedItem.Value
              && PptPlan._.ShiftID == shiftId
              && PptPlan._.ClassID == zaoClassID
              && PptPlan._.PlanState == "4"
              && PptPlan._.CreatePlanFlag == "1");
        if (PlanList.Count == 1)
        {
            PptPlan plan = PlanList[0];
            return plan.RealNum <= planNum + plan.PlanNum;
        }
        return true;
    }
    //校验插入计划不能小于1
    private bool ValidateInsertPlanFunction(int planNum, string equipCode, string planDate, string materialcode, string recipeName, string shiftId)
    {
        string proID = hidden_equip_code.Text.Substring(1, 1);
        string planNo = planManager.GetGetMaxPlanId(planDate, equipCode, shiftId);
        string zaoClassID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftId, proID, planDate).Rows[0]["ShiftClassID"].ToString();
        EntityArrayList<PptPlan> PlanList = planManager.GetListByWhere(PptPlan._.PlanDate == create_plan_date.Text
              && PptPlan._.RecipeEquipCode == hidden_equip_code.Text
              && PptPlan._.RecipeMaterialCode == create_material_code.SelectedItem.Value
              && PptPlan._.RecipeName == create_recipe_name.SelectedItem.Value
              && PptPlan._.ShiftID == shiftId
              && PptPlan._.ClassID == zaoClassID
              && PptPlan._.CreatePlanFlag == "1");
        if (PlanList.Count == 0)
        {
            return planNum > 0;
        }
        return true;

    }
    #endregion
}