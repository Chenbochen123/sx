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
using Newtonsoft.Json;
public partial class Manager_ProducingPlan_PlanEntering_AddPlan : System.Web.UI.Page
{


    protected PptPlanMgrManager manager = new PptPlanMgrManager();//业务对象
    protected BasMaterialManager materialManager = new BasMaterialManager();//业务对象
    protected BasEquipManager equipManager = new BasEquipManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected PmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
    protected PptPlanManager planManager = new PptPlanManager();
    protected PptShiftTimeManager pptShiftTimeManager = new PptShiftTimeManager();
    protected IBasWorkShopManager basWorkShopManager = new BasWorkShopManager();
    private const string constSelectAllText = "---请选择---";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
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
            this.txt_start_plan_date.Text = DateTime.Now.ToString();
            LoadGridData();
        }
    }

    protected void EquipCodeRefresh(object sender, StoreReadDataEventArgs e)
    {
        List<object> data = new List<object>();
        EntityArrayList<BasEquip> equipList = equipManager.GetListByWhereAndOrder(BasEquip._.WorkShopCode == txtWorkShopCode.Text.Replace(constSelectAllText, "0") & BasEquip._.DeleteFlag == 0 & BasEquip._.EquipType < "03", BasEquip._.EquipName.Asc);
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
        queryParams.equipCode = this.cbo_equip_code.Text.Replace(constSelectAllText, "");
        queryParams.startPlanDate = txt_start_plan_date.Text;
        queryParams.deleteFlag = "0";
        return manager.GetTablePageAddPlanInfoBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
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

    [DirectMethod]
    public void LoadGridData()
    {
        this.pageToolBar.DoRefresh();
        //string equipcode = this.hidden_select_equip_code.Text;
        //string planDate =Convert.ToDateTime(this.txt_start_plan_date.Text).ToString("yyyy-MM-dd");
        //string materCode = this.hidden_material_code.Text;
        //this.store.DataSource = manager.GetListAddPlanInfoByWhere(planDate, equipcode, materCode).Tables[0];
        //this.store.DataBind();
    }


    /// <summary>
    /// 点击删除触发的事件
    /// sunyj   2013年5月13日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            PptPlanMgr planMgr = manager.GetById(objID);
            manager.Delete(planMgr);
            LoadGridData();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }
    /// <summary>
    /// 点击提交的事件
    /// sunyj   2013年5月11日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_edit(string objID)
    {
        try
        {
            PptPlanMgr planMgr = manager.GetById(objID);
            if (planMgr.PlanDate < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                msg.Alert("提示", "不能下达过期的计划！");
                msg.Show();
                return null;
            }
            if (String.IsNullOrEmpty(planMgr.EquipCode) || String.IsNullOrEmpty(planMgr.MaterialCode) || String.IsNullOrEmpty(planMgr.RecipeName))
            {
                msg.Alert("提示","计划信息不全！请填写完整！");
                msg.Show();
                return null;
            }
            if (planMgr.ActualOnePlan == 0 && planMgr.ActualTwoPlan == 0 && planMgr.ActualThreePlan == 0)
            {
                msg.Alert("提示", "请核实计划数量！");
                msg.Show();
                return null;
            }

            planMgr.AddFlag = "1";
            manager.Update(planMgr);
            //this.AppendWebLog("计划信息删除", "计划编号：" + objID);
            LoadGridData();
        }
        catch (Exception e)
        {
            return "提交失败：" + e;
        }
        return "提交成功";
    }
     /// <summary>
    /// 点击添加按钮激发的事件
    /// sunyj   2013年5月11日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        PptPlanMgr planMgr = new PptPlanMgr();
        planMgr.PlanDate = DateTime.Parse(this.txt_start_plan_date.Text);
        if (string.IsNullOrEmpty(cbo_equip_code.Text) || cbo_equip_code.Text == constSelectAllText)
        {
                msg.Alert("提示","请选择机台！");
                msg.Show();
                return;
        }
        else
        {
            planMgr.EquipCode = this.cbo_equip_code.Text;
        }
        planMgr.AuditFlag = "0";
        planMgr.CreatePlanFlag = "0";
        planMgr.DeleteFlag = "0";
        planMgr.AddFlag = "0";
        manager.Insert(planMgr);
        LoadGridData();
        //msg.Notify("操作", "保存成功").Show();
    }
    private static object lockObj = new object();
    /// <summary>
    /// grid编辑模式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Edit(object sender, DirectEventArgs e)
    {
        lock (lockObj)
        {
            List<string> fields = new List<string> { "EquipName", "MaterialName", "RecipeName" };
            int i = fields.IndexOf(e.ExtraParams["field"]);
            JsonObject data = JSON.Deserialize<JsonObject>(e.ExtraParams["record"]);
            ModelProxy record = null;
            record = this.store.GetAt(int.Parse(e.ExtraParams["index"]));
            string recipeMaterialName = "";
            string recipeName = "";
          
            if (i >= 0)
            {
                switch (fields[i])
                {
                    case "MaterialName":
                        try
                        {
                            record.Set("RecipeName", "");
                            data["RecipeName"] = "";
                            recipeMaterialName = data["MaterialName"].ToString();
                            EntityArrayList<BasMaterial> ms = materialManager.GetListByWhere(BasMaterial._.MaterialName == recipeMaterialName);
                            if (ms.Count > 0)
                            {
                                data["ERPCode"] = ms[0].ERPCode;
                                record.Set("ERPCode", ms[0].ERPCode);
                            }
                            else
                            {
                                data["ERPCode"] ="";
                                record.Set("ERPCode","");
                            }
                           
                        }
                        catch (Exception)
                        {
                            record.Set("RecipeName", "");
                            record.Set("MaterialCode", "");
                            data["RecipeName"] = "";
                            data["MaterialCode"] = "";
                            data["ERPCode"] = "";
                        }
                        break;
                    case "RecipeName":
                        string materCode = "";
                        string equipId = data["EquipCode"].ToString();
                        try
                        {
                            materCode=data["MaterialCode"].ToString();
                        }
                        catch (Exception)
                        {
                            msg.Alert("提示","请先选择物料名称！");
                            msg.Show();
                        }
                        try
                        {
                            recipeName=data["RecipeName"].ToString();
                        }
                        catch (Exception)
                        {
                            msg.Alert("提示", "请先选择配方编号！");
                            msg.Show();
                        }   
                        break;
                    default:
                        break;
                }
            }
            try
            {
                PptPlanMgr ppMgr = manager.GetById(data["ObjID"].ToString());
                #region 异常处理计划量和备注
                try
                {
                    ppMgr.RecipeName = data["RecipeName"].ToString();
                }
                catch (Exception)
                {
                    ppMgr.RecipeName = "";
                }
               
                ppMgr.EquipCode = data["EquipCode"].ToString();
                EntityArrayList<PmtRecipe> recipeLst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialName == data["MaterialName"] &  PmtRecipe._.RecipeEquipCode == ppMgr.EquipCode & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
                if (recipeLst.Count > 0)
                {
                    ppMgr.MaterialCode = recipeLst[0].RecipeMaterialCode; 
                }
                try
                {
                    ppMgr.ERPCode = data["ERPCode"].ToString();
                }
                catch (Exception)
                {

                    ppMgr.ERPCode = "";
                }
                
                try
                {
                    ppMgr.ActualOneRemark = data["ActualOneRemark"].ToString();
                }
                catch (Exception)
                {
                    ppMgr.ActualOneRemark = "";
                }
                try
                {
                    ppMgr.ActualTwoRemark = data["ActualTwoRemark"].ToString();
                }
                catch (Exception)
                {
                    ppMgr.ActualTwoRemark = "";
                }
                try
                {
                    ppMgr.ActualThreeRemark = data["ActualThreeRemark"].ToString();
                }
                catch (Exception)
                {
                    ppMgr.ActualThreeRemark = "";
                }
                try
                {
                    ppMgr.ActualOnePlan = Convert.ToInt32(data["ActualOnePlan"].ToString());
                }
                catch (Exception)
                {
                    ppMgr.ActualOnePlan = 0;
                }
                try
                {
                    ppMgr.ActualTwoPlan = Convert.ToInt32(data["ActualTwoPlan"].ToString());
                }
                catch (Exception)
                {
                    ppMgr.ActualTwoPlan = 0;
                }
                try
                {
                    ppMgr.ActualThreePlan = Convert.ToInt32(data["ActualThreePlan"].ToString());
                }
                catch (Exception)
                {
                    ppMgr.ActualThreePlan = 0;
                }
                #endregion
                ppMgr.PlanTotalCount = ppMgr.ActualOnePlan + ppMgr.ActualTwoPlan + ppMgr.ActualThreePlan;
                record.Set("PlanTotalCount", ppMgr.PlanTotalCount);
                data["PlanTotalCount"] = ppMgr.PlanTotalCount;
                manager.Update(ppMgr);
            }
            catch (Exception)
            {

            }
            LoadGridData();
        }
    }
    /// <summary>
    /// 更新配方编号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RecipeNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string name = e.Parameters["query"];
        string equipCode = "";
        string materialCode = "";
        if (!string.IsNullOrEmpty(name))
        {
            string[] code = name.Split('_');
            if (code.Length == 2)
            {
                equipCode = code[1];
                materialCode = code[0];
            }
        }
        try
        {
            EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialCode == materialCode & PmtRecipe._.RecipeEquipCode == equipCode & PmtRecipe._.AuditFlag == 1);//2014年4月21日 因为添加计划配方不全，所以做修改 去除 & PmtRecipe._.RecipeState == 1
            RecipeNameStore.DataSource = lst;
            RecipeNameStore.DataBind();
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// 更新GridPanel中的物料名称列表框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RecipeMaterialNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        try
        {
            string equipCode = "";
            equipCode = e.Parameters["query"];
            string query = string.Empty;
            EntityArrayList<PmtRecipe> lst = new EntityArrayList<PmtRecipe>();
            lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, query);
            this.RecipeMaterialNameStore.DataSource = lst;
            this.RecipeMaterialNameStore.DataBind();
        }
        catch (Exception)
        {
        }
    }
    /// <summary>
    /// 进行批量提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnSummitAuditPlan_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要提交的计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            planMgr.AddFlag = "1";
            if (planMgr.PlanDate < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                continue;
            }
            if (String.IsNullOrEmpty(planMgr.EquipCode) || String.IsNullOrEmpty(planMgr.MaterialCode) || String.IsNullOrEmpty(planMgr.RecipeName))
            {
                continue;
            }
            if (planMgr.ActualOnePlan == 0 && planMgr.ActualTwoPlan == 0 && planMgr.ActualThreePlan == 0)
            {
                continue;
            }
            manager.Update(planMgr);
        }
        LoadGridData();
        //this.msg.Notify("提示", "提交成功!").Show();
    }

    /// <summary>
    /// 进行删除操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [Ext.Net.DirectMethod]
    public void BtnDeletePlan_Click(string Values)
    {
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(Values);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要删除的计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            manager.Delete(planMgr);
        }
        LoadGridData();
        //this.msg.Notify("提示", "提交成功!").Show();
    }
    /// <summary>
    /// 进行批量修改计划日期
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnUpdatePlanDate_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要修改计划日期的计划!").Show();
            return;
        }
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string planID = planRow["ObjID"];
            PptPlanMgr planMgr = manager.GetById(planID);
            planMgr.PlanDate = Convert.ToDateTime(this.txt_start_plan_date.Text);
            manager.Update(planMgr);
        }
        LoadGridData();
        //this.msg.Notify("提示", "提交成功!").Show();
    }
    
}