﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_ProducingPlan_PlanEntering_PlanExec : Mesnac.Web.UI.Page
{

    private BasEquipManager baseEquipManager = new BasEquipManager();// new BasEquipManager();
    private PptPlanManager pptPlanManager = new PptPlanManager();// new PptPlanManager();
    private PmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
    private PptShiftTimeManager pptShiftTimeManager = new PptShiftTimeManager();
    private SYS_USERManager userManager = new SYS_USERManager();
    Ext.Net.MessageBox mss = new MessageBox();//提示信息
    private string shiftFlag = "";

    private static string _equipCode = "";
    private static string _shiftID = "";

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {

            查询 = new SysPageAction() { ActionID = 1, ActionName = "txtStratPlanDate,query1,query2,query3" };
            添加 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd1,btnAdd2,btnAdd3,MenuItem10,MenuItem4,MenuAdd,btnCopy1,btnCopy2,btnCopy3" };
            删除 = new SysPageAction() { ActionID = 3, ActionName = "MenuItem8,MenuItem2,MenuDel" };
            下达 = new SysPageAction() { ActionID = 4, ActionName = "MenuItem1,MenuItem7,MenuItem13,btnPlan1,btnPlan2,btnPlan3" };
            移动 = new SysPageAction() { ActionID = 5, ActionName = "MenuUp,MenuDn,MenuItem5,MenuItem6,MenuItem11,MenuItem12" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; }
        public SysPageAction 删除 { get; private set; }
        public SysPageAction 下达 { get; private set; }
        public SysPageAction 移动 { get; private set; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //baseEquipManager = new BasEquipManager();
        //pptPlanManager = new PptPlanManager();
        //pmtRecipeManager = new PmtRecipeManager();
        //pptShiftTimeManager = new PptShiftTimeManager();
       // this.Zhong.Hidden = true;
        if (!X.IsAjaxRequest)
        {
            this.Session["equipCode"] = null;
            txtStratPlanDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            InitTreeDept();
        }
    }

    #region 树
    /// <summary>
    /// 初始化机台列表树
    /// </summary>
    private void InitTreeDept()
    {
        treeEquip.GetRootNode().RemoveAll();
        treeEquip.GetRootNode().AppendChild(getTreeNodeByDelLevel());
    }

    /// <summary>
    /// 获取机台树的算法
    /// </summary>
    /// <param name="dep_num"></param>
    /// <returns></returns>
    private Node getTreeNodeByDelLevel()
    {
        Node node = new Node();
        node.NodeID = "0";
        node.Text = "机台分组";
        node.Expanded = true;
        Dictionary<string, string> depChildFristList = new Dictionary<string, string>();
        var query = baseEquipManager.GetListByWhereAndOrder(BasEquip._.EquipType < "03" && BasEquip._.DeleteFlag == 0, BasEquip._.EquipGroup.Asc).GroupBy(pet => pet.EquipGroup.Trim()).Where(pet => !string.IsNullOrEmpty(pet.Key));
        foreach (var info in query)
        {
            Node childNode = new Node();
            childNode.NodeID = info.Key;
            childNode.Text = info.Key;
            childNode.Expanded = false;
            var child = baseEquipManager.GetListByWhereAndOrder(BasEquip._.EquipGroup == info.Key & BasEquip._.EquipType < "03" && BasEquip._.DeleteFlag == 0, BasEquip._.EquipCode.Asc);
            foreach (var item in child)
            {
                Node nodeLeaf = new Node();
                nodeLeaf.Text = item.EquipName;
                nodeLeaf.Qtip = item.EquipCode;
                nodeLeaf.Leaf = true;
                childNode.Children.Add(nodeLeaf);
            }
            node.Children.Add(childNode);
        }
        return node;
    }
    #endregion

    /// <summary>
    /// 相应点击机台树事件
    /// </summary>
    /// <param name="equipCode"></param>
    [DirectMethod]
    public void LoadGridData(string equipCode)
    {
        //判断当前机台当前时间是否设置班组信息

        #region 初始化班组信息
        this.Session["equipCode"] = equipCode;
        string date = this.txtStratPlanDate.Text;
        hidden_parent_num.Value = date;
        if (equipCode.Length > 2)
        {
            string proID = equipCode.Substring(1, 1);
            string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();
            if (Convert.ToInt32(count) <= 0)
            {

                mss.Alert("提示", "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息");
                mss.Show();
                return;
            }
            DataTable dtZao = pptShiftTimeManager.GetClassNameByPIDAndDate("1", proID, date);
            DataTable dtZHong = pptShiftTimeManager.GetClassNameByPIDAndDate("2", proID, date);
            DataTable dtYe = pptShiftTimeManager.GetClassNameByPIDAndDate("3", proID, date);
            string zaoClass = dtZao.Rows[0]["ClassName"].ToString();
            string zaoShift = dtZao.Rows[0]["ShiftName"].ToString();

            //string zhongClass = dtZHong.Rows[0]["ClassName"].ToString();
            //string zhongShift = dtZHong.Rows[0]["ShiftName"].ToString();
            string yeClass = dtYe.Rows[0]["ClassName"].ToString();
            string yeShift = dtYe.Rows[0]["ShiftName"].ToString();

            zaoPanel.Html = "<table width='100%' height='100%'><tr align='center'><td align='center'>" + zaoShift + "--" + zaoClass + "</td></tr></table> ";
           // ZhongPanel.Html = "<table width='100%' height='100%'><tr align='center'><td align='center'>" + zhongShift + "--" + zhongClass + "</td></tr></table> ";
            YePanel.Html = "<table width='100%' height='100%'><tr align='center'><td align='center'>" + yeShift + "--" + yeClass + "</td></tr></table> ";
        }
        #endregion

        #region 绑定对应的早中晚 计划信息

        //X.Msg.Notify("", equipCode+";"+ date).Show();
        DataSet ds = pptPlanManager.GetEquipPlan(equipCode, date);
        this.ZaoPlanStore.DataSource = ds.Tables[0];
        this.ZaoPlanStore.DataBind();


        this.ZhongPlanStore.DataSource = ds.Tables[1];
        this.ZhongPlanStore.DataBind();

        this.YePlanStore.DataSource = ds.Tables[2];
        this.YePlanStore.DataBind();

        //获取对应机台的物料配方 删除标识未添加

        //this.RecipeMaterialNameStore.Reload();
        this.txtzhouqizao.Text = ds.Tables[4].Rows[0]["PlanNum"].ToString();
        this.txtcheshuzao.Text = ds.Tables[3].Rows[0]["PlanNum"].ToString();
        this.txtzhouqiwan.Text = ds.Tables[6].Rows[0]["PlanNum"].ToString();
        this.txtchechuwan.Text = ds.Tables[5].Rows[0]["PlanNum"].ToString();
        this.Panel3.Title = baseEquipManager.GetListByWhere(BasEquip._.EquipCode == equipCode)[0].EquipName;
        #endregion

        #region 绑定添加下拉框的数值
        add_recipe_material_code_zao.Value = "";
        add_recipe_material_code_zhong.Value = "";
        add_recipe_material_code_ye.Value = "";
        add_recipe_name_zao.Value = "";
        add_recipe_name_zhong.Value = "";
        add_recipe_name_ye.Value = "";
        this.RecipeMaterialNameStore_zao.Reload();
        this.RecipeMaterialNameStore_zhong.Reload();
        this.RecipeMaterialNameStore_ye.Reload();
        this.AddRecipeMaterialNameStore_zao.Reload();
        this.AddRecipeMaterialNameStore_zhong.Reload();
        this.AddRecipeMaterialNameStore_ye.Reload();
        #endregion
    }

    /// <summary>
    /// 增加新的计划
    /// </summary>
    [DirectMethod]
    public bool AddPlan(string shiftid)
    {
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        string date = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd");
        if (equipCode == null || equipCode == "")
        {
            X.Msg.Alert("提示", "请先选择左侧机台!").Show();
            return false; ;
        }
        string proID = equipCode.Substring(1, 1);
        string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();
        if (Convert.ToInt32(count) <= 0)
        {

            mss.Alert("提示", "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息");
            mss.Show();
            return false;
        }

        if (Convert.ToDateTime(date) < DateTime.Now.AddDays(-1))
        {
            mss.Alert("提示", "不能设置当前日期之前的计划信息！");
            mss.Show();
            return false;
        }
        string planNo = pptPlanManager.GetGetMaxPlanId(date, equipCode, shiftid);

        string classID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftid, proID, date).Rows[0]["ShiftClassID"].ToString();

        PptPlan pptPlan = new PptPlan();
        WhereClip where = PptPlan._.PlanDate == date & PptPlan._.RecipeEquipCode == equipCode & PptPlan._.ShiftID == shiftid & PptPlan._.ClassID == classID;
        OrderByClip order = PptPlan._.PriLevel.Desc;
        EntityArrayList<PptPlan> lst = pptPlanManager.GetListByWhereAndOrder(where, order);
        if (lst.Count > 0)
        {
            //pptPlan.SerialNum = lst.Count + 1;
            pptPlan.PriLevel = lst[0].PriLevel + 1;
        }
        else
        {
            pptPlan.PriLevel = 1;
            //pptPlan.SerialNum = 1;
        }
        order = PptPlan._.SerialNum.Desc;
        lst = pptPlanManager.GetListByWhereAndOrder(where, order);

        if (lst.Count > 0)
        {
            pptPlan.SerialNum = lst[0].SerialNum + 1;
        }
        else
        {
            pptPlan.SerialNum = 1;
        }
        //默认新增数据的物料名称和配方编号

        pptPlan.PlanDate = Convert.ToDateTime(date);
        pptPlan.PlanSource = "N";
        pptPlan.UrgencyState = 2;//计划紧急状态
        pptPlan.SmallCreate = 0;
        pptPlan.RecipeEquipCode = equipCode;
        pptPlan.ClassID = Convert.ToInt32(classID);//获取当前班次id;
        pptPlan.PlanID = planNo;
        pptPlan.ShiftID = Convert.ToInt32(shiftid);
        pptPlan.PlanNum = 1;
        pptPlan.PlanState = "1";
        pptPlan.OperDatetime = DateTime.Now;
        pptPlan.OperCode = this.UserID;//获取当前登录用户工号
        pptPlan.DeleteFlag = "0";
        pptPlanManager.Insert(pptPlan);
        this.AppendWebLog("生产计划录入-添加计划", "物料名称：" + pptPlan.RecipeMaterialName +
            ",配方编号:" + pptPlan.RecipeName +
            ",生产车数:" + pptPlan.PlanNum +
            ",操作人:" + this.UserID);
        LoadGridData(equipCode);
        return true;
    }


    /// <summary>
    /// 向上插入的方法
    /// </summary>
    /// <param name="priLevel">优先级</param>
    /// <param name="shiftid">班次号</param>
    /// <returns></returns>
    [DirectMethod]
    public string InsertPlan(int priLevel, string shiftid)
    {
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        string date = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd");

        string proID = equipCode.Substring(1, 1);
        string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();

        #region 空值验证
        if (equipCode == null || equipCode == "")
        {
            return "请先选择左侧机台!";
        }
        if (Convert.ToInt32(count) <= 0)
        {
            return "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息!";
        }

        if (Convert.ToDateTime(date) < DateTime.Now.AddDays(-1))
        {
            return "不能设置当前日期之前的计划信息!";
        }

        string validateStr = ValidateAddContent(shiftid);
        if (validateStr != "")
        {
            return validateStr;
        }
        #endregion
        string planNo = pptPlanManager.GetGetMaxPlanId(date, equipCode, shiftid);

        string classID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftid, proID, date).Rows[0]["ShiftClassID"].ToString();

        PptPlan pptPlan = new PptPlan();
        WhereClip where = PptPlan._.PlanDate == date & PptPlan._.RecipeEquipCode == equipCode & PptPlan._.ShiftID == shiftid & PptPlan._.ClassID == classID;
        OrderByClip order = PptPlan._.PriLevel.Desc;
        EntityArrayList<PptPlan> lst = pptPlanManager.GetListByWhereAndOrder(where, order);
        if (lst.Count > 0)
        {
            //pptPlan.SerialNum = lst.Count+ 1;
            pptPlan.PriLevel = priLevel;//插入的顺序
        }
        else
        {
            pptPlan.PriLevel = 1;
            //pptPlan.SerialNum = 1;
        }
        order = PptPlan._.SerialNum.Desc;
        lst = pptPlanManager.GetListByWhereAndOrder(where, order);
        if (lst.Count > 0)
        {
            pptPlan.SerialNum = lst[0].SerialNum + 1;
        }
        else
        {
            pptPlan.SerialNum = 1;
        }

        pptPlan.PlanDate = Convert.ToDateTime(date);
        pptPlan.PlanSource = "N";
        pptPlan.UrgencyState = 2;//计划紧急状态
        pptPlan.SmallCreate = 0;
        pptPlan.RecipeEquipCode = equipCode;
        pptPlan.ClassID = Convert.ToInt32(classID);//获取当前班次id;
        pptPlan.PlanID = planNo;
        pptPlan.ShiftID = Convert.ToInt32(shiftid);
        pptPlan.PlanNum = 1;
        pptPlan.PlanState = "1";
        pptPlan.OperDatetime = DateTime.Now;
        pptPlan.OperCode = this.UserID;//获取当前登录用户工号
        pptPlan.DeleteFlag = "0";
        //赋值操作
        switch (shiftid)
        {
            case "1":
                pptPlan.RecipeMaterialCode = add_recipe_material_code_zao.SelectedItem.Value;
                pptPlan.RecipeName = add_recipe_name_zao.SelectedItem.Value;
                pptPlan.PlanNum = Convert.ToInt32(add_plan_num_zao.RawValue);
                break;
            case "2":
                pptPlan.RecipeMaterialCode = add_recipe_material_code_zhong.SelectedItem.Value;
                pptPlan.RecipeName = add_recipe_name_zhong.SelectedItem.Value;
                pptPlan.PlanNum = Convert.ToInt32(add_plan_num_zhong.RawValue);
                break;
            case "3":
                pptPlan.RecipeMaterialCode = add_recipe_material_code_ye.SelectedItem.Value;
                pptPlan.RecipeName = add_recipe_name_ye.SelectedItem.Value;
                pptPlan.PlanNum = Convert.ToInt32(add_plan_num_ye.RawValue);
                break;
            default:
                break;
        }
        //
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
        //
        if (pptPlanManager.UpdatePriLevel(date, equipCode, shiftid, priLevel))
        {
            string username="";
            EntityArrayList<SYS_USER> list = userManager.GetListByWhere(SYS_USER._.USER_ID == this.UserID);
                if(list.Count>0)
                {username=list[0].Real_name;}
                else{username="";}
            //pptPlanManager.Insert(pptPlan);
            // insert into PptPlan(PlanID,PlanDate,RecipeEquipCode,RecipeMaterialCode,RecipeMaterialName,RecipeVersionID,RecipeUserVersion,ShiftID,ClassID,
//SerialNum,PriLevel,RecipeName,RecipeType,Runtype,PrintType,PlanSource,TotalWeight,PlanNum,PlanWeight,UrgencyState,PlanState,OperDatetime,PlanEndTime,RealStartTime,RealEndtime,OperCode,ShowFlag,UserID,FanLianNum,AvgTime,shelfnum,SmallCreate,CreatePlanFlag)
            string sql = @" insert into Ppt_Plan ([Plan_id],[Plan_date],[Equip_code],[Mater_code],[Mater_Name],[Edt_code],[User_Edtcode],[Shift_id],[Shift_class],[Serial_num],[Pri_level],[recipe_code],[Recipe_Type],[Run_Type],[Print_Type],[Plan_Source],[Total_weight],[Plan_num],[Plan_weight],[Real_num],[Real_weight],[Urgency_state],[Plan_state]
           ,[Oper_datetime],[Plan_EndTime],[Real_StartTime],[Real_Endtime],[Oper_code],[revise_sgn],[SAP_BillNo],[Message],[ShowFlag],[worker_code],[mem_note],[remark],[FanLianNum],[avgtime],[CJName],[CJweight],[shelfnum],[smallcreate],[createPlanFlag],[DeleteFlag],[xlbagweight]) values('" + planNo + "','" + pptPlan.PlanDate + "','" + equipCode + "','" + pptPlan.RecipeMaterialCode + "','" + pptPlan.RecipeMaterialName + "','" + pptPlan.RecipeVersionID + "','','" + pptPlan.ShiftID + "','" + pptPlan.ClassID + "','" + pptPlan.SerialNum + "','" + pptPlan.PriLevel + "','" + pptPlan.RecipeName + "','" + pptPlan.RecipeType + "','','','','" + pptPlan.TotalWeight + "','" + pptPlan.PlanNum + "','" + pptPlan.PlanWeight + "','" + pptPlan.RealNum + "',cast('0' as numeric(12,3)),'" + pptPlan.UrgencyState + "','" + pptPlan.PlanState + "',CONVERT(varchar(19),GETDATE(),120),'','','','" + pptPlan.OperCode + "','','','','','" + username + "','','','','','',cast('0' as numeric(8,3)),'','','','',cast('0' as numeric(12,3)))";
            pptPlanManager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("生产计划录入-插入计划", "物料名称：" + pptPlan.RecipeMaterialName +
                ",配方编号:" + pptPlan.RecipeName +
                ",生产车数:" + pptPlan.PlanNum +
                ",操作人:" + this.UserID);
        }
        LoadGridData(equipCode);
        //添加后清除值操作
        switch (shiftid)
        {
            case "1":
                add_recipe_material_code_zao.Value = "";
                add_recipe_name_zao.Value = "";
                add_plan_num_zao.RawValue = "";
                break;
            case "2":
                add_recipe_material_code_zhong.Value = "";
                add_recipe_name_zhong.Value = "";
                add_plan_num_zhong.RawValue = "";
                break;
            case "3":
                add_recipe_material_code_ye.Value = "";
                add_recipe_name_ye.Value = "";
                add_plan_num_ye.RawValue = "";
                break;
            default:
                break;
        }
        return "插入成功";
    }
    /// <summary>
    /// 根据计划ID删除计划信息
    /// </summary>
    /// <param name="planID"></param>
    /// <returns></returns>
    [DirectMethod]
    public string DelePlan(string planID)
    {
        PptPlan pp = new PptPlan();
        pp.PlanID = planID;
        WhereClip where = PptPlan._.PlanID == planID;
        try
        {
           // PptPlan ppt = pptPlanManager.GetById(planID);
            EntityArrayList<PptPlan> list = pptPlanManager.GetListByWhere(PptPlan._.PlanID == planID);
            if (list.Count==0)
                return "计划不存在!";
            else
            {
                
                if (Convert.ToInt32(list[0].PlanState) == 3)
                {
                    return "接受后的计划不能删除!";
                }
                if (Convert.ToInt32(list[0].PlanState) == 4)
                {
                    return "运行后的计划不能删除!";
                }
                if (Convert.ToInt32(list[0].PlanState) == 5)
                {
                    return "完成后的计划不能删除!";
                }
            }
            pptPlanManager.DeleteByWhere(where);
            this.AppendWebLog("生产计划录入-删除计划", "物料名称：" + list[0].RecipeMaterialName +
                ",配方编号:" + list[0].RecipeName +
                ",生产车数:" + list[0].PlanNum +
                ",操作人:" + this.UserID);
            return "";
        }
        catch (Exception)
        {
            return "删除发生异常，请联系管理员!";
        }
    }

    /// <summary>
    /// 下达计划
    /// </summary>
    /// <param name="planID"></param>
    /// <returns></returns>
    [DirectMethod]
    public string UpPlanState(string planID)
    {
        
        try
        {
            if (Convert.ToDateTime(txtStratPlanDate.Text) < DateTime.Now.AddDays(-1))
            {
                return "不能设置当前日期之前的计划信息!";
            }
            else
            {
                //PptPlan pptPlan = pptPlanManager.GetById(planID, this.txtStratPlanDate.Text);
                PptPlan pptPlan = pptPlanManager.GetListByWhere(PptPlan._.PlanID==planID )[0];
                if (Convert.ToInt32(pptPlan.PlanState) == 2)
                {
                    return "已下达的计划不能下达!";
                }
                if (Convert.ToInt32(pptPlan.PlanState) == 3)
                {
                    return "已接受的计划不能下达!";
                }
                if (Convert.ToInt32(pptPlan.PlanState) == 4)
                {
                    return "已运行的计划不能下达!";
                }
                if (Convert.ToInt32(pptPlan.PlanState) == 5)
                {
                    return "已完成的计划不能下达!";
                }
                
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("@PlanID", planID);

                //DataSet dss = pptPlanManager.GetDataSetByStoreProcedure("ProcPlanNeedXCCheck", dict);
                //if (dss.Tables[0].Rows[0][0].ToString().IndexOf("ok")<0)
                //    return dss.Tables[0].Rows[0][0].ToString();



                pptPlan.PlanState = "2";//下达状态
                string sql = "update Ppt_plan set Plan_state=2 where Plan_id='"+planID+"'";
                pptPlanManager.GetBySql(sql).ToDataSet();
               // pptPlanManager.Update(pptPlan);
                this.AppendWebLog("生产计划录入-下达计划", "物料名称：" + pptPlan.RecipeMaterialName +
                ",配方编号:" + pptPlan.RecipeName +
                ",生产车数:" + pptPlan.PlanNum +
                ",操作人:" + this.UserID);
                LoadGridData(pptPlan.RecipeEquipCode);
                return "此计划下达成功!";
            }
        }
        catch (Exception)
        {
            return "下达计划异常，请联系管理员!";
        }

    }


    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="i">上移为0下移为1</param>
    /// <param name="planID">计划号</param>
    /// <param name="priLevel">当前计划的优先级</param>
    /// <param name="shiftid">班次</param>
    /// <returns></returns>
    [DirectMethod]
    public bool MovePlan(int i, string planID, int priLevel, string shiftid)
    {
        try
        {

            PptPlan pptPlan = pptPlanManager.GetListByWhere(PptPlan._.PlanID == planID && PptPlan._.PlanDate == this.txtStratPlanDate.Text)[0];
            int temp = 0;
            temp = Convert.ToInt32(pptPlan.PriLevel);
            WhereClip where = new WhereClip();
            OrderByClip order = new OrderByClip();
            if (i == 0)
            {
                where = PptPlan._.PlanDate == this.txtStratPlanDate.Text & PptPlan._.RecipeEquipCode == pptPlan.RecipeEquipCode & PptPlan._.ShiftID == shiftid & PptPlan._.PriLevel < temp;
                order = PptPlan._.PriLevel.Desc;
            }
            else
            {
                where = PptPlan._.PlanDate == this.txtStratPlanDate.Text & PptPlan._.RecipeEquipCode == pptPlan.RecipeEquipCode & PptPlan._.ShiftID == shiftid & PptPlan._.PriLevel > temp;
                order = PptPlan._.PriLevel.Asc;
            }
            EntityArrayList<PptPlan> lst = pptPlanManager.GetListByWhereAndOrder(where, order);
            if (lst.Count > 0)
            {
                PptPlan sounPlan = pptPlanManager.GetListByWhereAndOrder(where, order)[0];


                //pptPlan.PriLevel = sounPlan.PriLevel;
                //sounPlan.PriLevel = temp;

                //pptPlanManager.Update(pptPlan);
                //pptPlanManager.Update(sounPlan);
                String sql = "update Ppt_Plan set Pri_Level = '" + sounPlan.PriLevel + "' where Plan_ID = '" + pptPlan.PlanID + "'";
                pptPlanManager.GetBySql(sql).ToDataSet();
                sql = "update Ppt_Plan set Pri_Level = '" + pptPlan.PriLevel + "' where Plan_ID = '" + sounPlan.PlanID + "'";
                pptPlanManager.GetBySql(sql).ToDataSet();
                LoadGridData(pptPlan.RecipeEquipCode);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e2)
        {
            //return e2.Message;
            return false;
        }
    }

    private string Unescape(string ss)
    {
        return System.Text.RegularExpressions.Regex.Unescape(ss);
    }
    /// <summary>
    /// 更新GridPanel中的物料名称列表框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RecipeMaterialNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        Ext.Net.Store store = (Ext.Net.Store)sender;
        string submitDirectEventConfig = this.Page.Request["submitDirectEventConfig"]; //{"config":{"extraParams":{"query":"r","page":1,"start":0,"limit":25}}}
        string query = string.Empty;
        if (!string.IsNullOrWhiteSpace(submitDirectEventConfig))
        {
            try
            {
                submitDirectEventConfig = Unescape(submitDirectEventConfig);
                JavaScriptObject config = (JavaScriptObject)JavaScriptConvert.DeserializeObject(submitDirectEventConfig);
                if (submitDirectEventConfig.IndexOf("query") != -1)
                {
                    query = (((Newtonsoft.Json.JavaScriptObject)(((Newtonsoft.Json.JavaScriptObject)(config["config"]))["extraParams"]))["query"]).ToString();
                }
            }
            catch
            {
            }
        }
        string comboxID = store.ID;
        EntityArrayList<PmtRecipe> lst = new EntityArrayList<PmtRecipe>();
        //X.Msg.Notify("", equipCode+";"+ query).Show();
        if (comboxID.IndexOf("zao") != -1)
        {
            lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, query);
            this.RecipeMaterialNameStore_zao.DataSource = lst;
            this.RecipeMaterialNameStore_zao.DataBind();
        }
        else if (comboxID.IndexOf("zhong") != -1)
        {
            lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, query);

            this.RecipeMaterialNameStore_zhong.DataSource = lst;
            this.RecipeMaterialNameStore_zhong.DataBind();
        }
        else if (comboxID.IndexOf("ye") != -1)
        {
            lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, query);
            this.RecipeMaterialNameStore_ye.DataSource = lst;
            this.RecipeMaterialNameStore_ye.DataBind();
        }
    }

    /// <summary>
    /// 更新GridPanel中的物料名称列表框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AddRecipeMaterialNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        Ext.Net.Store store = (Ext.Net.Store)sender;
        string comboxID = store.ID;
        EntityArrayList<PmtRecipe> lst = new EntityArrayList<PmtRecipe>();
        if (comboxID.IndexOf("zao") != -1)
        {
            lst = GetDistinctRecipeMaterialNameAndCode(20, equipCode, add_recipe_material_code_zao.Text);
            this.AddRecipeMaterialNameStore_zao.DataSource = lst;
            this.AddRecipeMaterialNameStore_zao.DataBind();
        }
        else if (comboxID.IndexOf("zhong") != -1)
        {
            lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, add_recipe_material_code_zhong.Text);

            this.AddRecipeMaterialNameStore_zhong.DataSource = lst;
            this.AddRecipeMaterialNameStore_zhong.DataBind();
        }
        else if (comboxID.IndexOf("ye") != -1)
        {
            lst = pmtRecipeManager.GetDistinctRecipeMaterialNameAndCode(20, equipCode, add_recipe_material_code_ye.Text);
            this.AddRecipeMaterialNameStore_ye.DataSource = lst;
            this.AddRecipeMaterialNameStore_ye.DataBind();
        }
    }

    public EntityArrayList<PmtRecipe> GetDistinctRecipeMaterialNameAndCode(int top, string equipCode, string searchKey)
    {
        EntityArrayList<PmtRecipe> recipeList = new EntityArrayList<PmtRecipe>();
        string sqlstr = "";
        sqlstr = @"    SELECT DISTINCT  RecipeMaterialCode , RecipeMaterialName FROM PmtRecipe 
                                    WHERE RecipeEquipCode = '{0}' AND AuditFlag = '1' AND RecipeState = '1' AND [dbo].[FuncSysGetPY](RecipeMaterialName) like '%{1}%' and Plan_Use=1 ";
        sqlstr = String.Format(sqlstr, equipCode, searchKey);
//        String sql = @"union all select distinct BasMaterial.MaterialCode,MaterialName from BasMaterialDL 
//left join BasMaterial on BasMaterialDL.MaterialCode =BasMaterial.MaterialCode
//where BasMaterialDL.deleteflag = '0' and BasMaterialDL.EquipCode='" + equipCode + "' and MaterialName like '%" + searchKey + "%'";
//        sqlstr = sqlstr + sql;
        DataSet ds = pmtRecipeManager.GetBySql(sqlstr.ToString()).ToDataSet();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            PmtRecipe recipe = new PmtRecipe();
            recipe.RecipeMaterialCode = row["RecipeMaterialCode"].ToString();
            recipe.RecipeMaterialName = row["RecipeMaterialName"].ToString();
            recipeList.Add(recipe);
        }
        return recipeList;
    }

    protected void RecipeNameStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        string name = e.Parameters["query"];
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialName == name & PmtRecipe._.RecipeEquipCode == equipCode & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
        DataTable dt = pmtRecipeManager.GetRecipeNameByRecipeMaterialName(name).Tables[0];
        RecipeNameStore.DataSource = lst;
        RecipeNameStore.DataBind();
    }
    /// 点击添加按钮激发的事件
    /// 孙宜建   2013年2月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// 孙宜建   2013年2月16日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// grid编辑模式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Edit(object sender, DirectEventArgs e)
    {
        List<string> fields = new List<string> { "RecipeMaterialName", "RecipeName" };
        int startIndex = fields.IndexOf(e.ExtraParams["field"]);
        string flag = e.ExtraParams["flag"].ToString();
        JsonObject data = JSON.Deserialize<JsonObject>(e.ExtraParams["record"]);
        ModelProxy record = null;
        if (flag == "1")
        {
            record = this.ZaoPlanStore.GetAt(int.Parse(e.ExtraParams["index"]));
        }
        else if (flag == "2")
        {
            record = this.ZhongPlanStore.GetAt(int.Parse(e.ExtraParams["index"]));
        }
        else if (flag == "3")
        {
            record = this.YePlanStore.GetAt(int.Parse(e.ExtraParams["index"]));
        }
        else
        {
            return;
        }
        string recipeMaterialName = "";
        string recipeName = "";
        recipeMaterialName = data["RecipeMaterialName"].ToString();
        for (int i = startIndex + 1; i < 2; i++)
        {

            switch (fields[i])
            {
                case "RecipeName":
                    if (recipeMaterialName == "")
                    {
                        record.Set(fields[i], "");
                    }
                    EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialName == recipeMaterialName & PmtRecipe._.RecipeEquipCode == data["RecipeEquipCode"].ToString() & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);

                    DataTable dt = pmtRecipeManager.GetRecipeNameByRecipeMaterialName(recipeMaterialName).Tables[0];
                    if (lst.Count > 0)
                    {
                        record.Set(fields[i], lst[0].RecipeName);
                        data["RecipeName"] = lst[0].RecipeName;
                        recipeName = lst[0].RecipeName;
                    }
                    else
                    {
                        //record.Set(fields[i], "");
                    }
                    break;
                default:
                    break;
            }
        }
        string planID = data["PlanID"].ToString();
        string planState = "";
        EntityArrayList<PptPlan> pptPlanList = pptPlanManager.GetListByWhere(PptPlan._.PlanID == planID);
        if (pptPlanList.Count > 0)
        {
            planState = pptPlanList[0].PlanState;
        }
        if (Convert.ToInt32(planState) == 3)
        {
            mss.Alert("错误", "已接受后的计划不能修改");
            mss.Show();
            LoadGridData(data["RecipeEquipCode"].ToString());
            return;
        }
        if (Convert.ToInt32(planState) == 4)
        {
            mss.Alert("错误", "已运行后的计划不能修改");
            mss.Show();
            LoadGridData(data["RecipeEquipCode"].ToString());
            return;
        }
        if (Convert.ToInt32(planState) == 5)
        {
            mss.Alert("错误", "已完成后的计划不能修改");
            mss.Show();
            LoadGridData(data["RecipeEquipCode"].ToString());
            return;
        }
        string proID = data["RecipeEquipCode"].ToString().Substring(1, 1);
        string shiftID = data["ShiftID"].ToString();
        string classID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftID, proID, txtStratPlanDate.Text).Rows[0]["ShiftClassID"].ToString();


        PptPlan pptPlan = pptPlanManager.GetById(data["PlanID"], txtStratPlanDate.Text);
        pptPlan.ClassID = Convert.ToInt32(classID);
        pptPlan.RecipeMaterialName = data["RecipeMaterialName"].ToString();
        pptPlan.RecipeName = data["RecipeName"].ToString();
        pptPlan.PlanNum = Convert.ToInt32(data["PlanNum"].ToString());
        pptPlan.PriLevel = Convert.ToInt32(data["PriLevel"].ToString());
        pptPlan.OperDatetime = DateTime.Now;
        EntityArrayList<PmtRecipe> recipeLst = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeMaterialName == pptPlan.RecipeMaterialName & PmtRecipe._.RecipeName == pptPlan.RecipeName & PmtRecipe._.RecipeEquipCode == data["RecipeEquipCode"] & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
        if (recipeLst.Count > 0)
        {
            pptPlan.RecipeMaterialCode = recipeLst[0].RecipeMaterialCode;
            pptPlan.RecipeVersionID = recipeLst[0].RecipeVersionID;
            pptPlan.TotalWeight = recipeLst[0].LotTotalWeight;
            pptPlan.PlanWeight = pptPlan.PlanNum * pptPlan.TotalWeight;
            pptPlan.RecipeType = recipeLst[0].RecipeType;
            pptPlan.RecipeUserVersion = recipeLst[0].RecipeUserVersion;
        }
        else
        {
            //int t = pptPlan.RecipeName.IndexOf("正常");
            //string edt = pptPlan.RecipeName.Substring(0, t);
            //String sql = "select BasMaterialDL.* , materialname from BasMaterialDL left join BasMaterial on BasMaterialDL.MaterialCode =BasMaterial.MaterialCode where equipcode = '" + data["RecipeEquipCode"].ToString() + "' and BasMaterialDL.materialcode = '" + pptPlan.RecipeMaterialCode + "' and BasMaterialDL.deleteflag = '0' and edtcode  = '" + edt + "'";
            //DataSet ds = pmtRecipeManager.GetBySql(sql).ToDataSet();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = ds.Tables[0].Rows[0];
            //    //pptPlan.RecipeMaterialName = dr["materialname"].ToString();
            //    //pptPlan.RecipeVersionID = int.Parse(edt);
            //    pptPlan.TotalWeight = decimal.Parse(dr["SetWeight"].ToString());
            //    pptPlan.PlanWeight = pptPlan.PlanNum * pptPlan.TotalWeight;

            //}

            //else
            //{
                X.Msg.Alert("提示", "请选择有效的物料名称!").Show();
                LoadGridData(data["RecipeEquipCode"].ToString());
                return;
          //  }
        }

    //    X.Msg.Notify(data["PlanID"].ToString(), data["RecipeMaterialName"].ToString() + ";" + data["RecipeName"].ToString() + ";" + data["PlanNum"].ToString()).Show();
        //X.Msg.Notify(pptPlan.PlanID, pptPlan.RecipeMaterialName+";"+ pptPlan.RecipeName+";"+pptPlan.PlanNum+";"+ pptPlan.RecipeMaterialCode).Show();
        //return;
        //pptPlanManager.Update(pptPlan);

        string sqlstr = "update ppt_plan set Mater_code='"+ pptPlan.RecipeMaterialCode + "',Mater_Name='"+ pptPlan.RecipeMaterialName + "',recipe_code='"+ pptPlan.RecipeName + "',Plan_num='"+ pptPlan.PlanNum + "' where Plan_id='"+ planID.Trim() + "'";

        string sqlstr1 = "update ppt_plan set Plan_Weight='" + pptPlan.PlanNum * pptPlan.TotalWeight + "' where Plan_id='" + planID.Trim() + "'";
        pptPlanManager.GetBySql(sqlstr).ToDataSet();
        pptPlanManager.GetBySql(sqlstr1).ToDataSet();
        this.AppendWebLog("生产计划录入-修改计划", "物料名称：" + pptPlan.RecipeMaterialName +
               ",配方编号:" + pptPlan.RecipeName +
               ",生产车数:" + pptPlan.PlanNum +
               ",操作人:" + this.UserID);
        LoadGridData(data["RecipeEquipCode"].ToString());
        this.RecipeMaterialNameStore.Reload();
    }

    [DirectMethod]
    public bool DateChage()
    {
        if (this.Session["equipCode"] == null)
        {
            return false;
        }
        else
        {
            LoadGridData(this.Session["equipCode"].ToString());
            return true;
        }

    }

    public void AllUpdatePlanState(object sender, DirectEventArgs e)
    {
        string shiftid = e.ExtraParams["flage"].ToString();
        string date = this.txtStratPlanDate.Text;
        if (this.Session["equipCode"] == null)
        {
            mss.Alert("提示", "请先选择机台！");
            mss.Show();
            return;
        }       
        if (Convert.ToDateTime(date) < DateTime.Now.AddDays(-1))
        {
            mss.Alert("提示", "不能设置当前日期之前的计划信息！");
            mss.Show();
            return;
        }
        string equipCode = this.Session["equipCode"].ToString();
        
        //string sql = "select itemcode from SysUserCtrl where typeid ='XCCtrl'";
        //DataSet ds = pptPlanManager.GetBySql(sql).ToDataSet();
        //if(ds.Tables[0].Rows.Count>0)
        //{
        //    if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //    {

        //        sql = "select * from pptplan where RecipeEquipCode = '" + equipCode + "' and plandate = '" + date + "' and shiftid ='" + shiftid + "' AND DeleteFlag=0 AND PlanState=1";
        //        ds = pptPlanManager.GetBySql(sql).ToDataSet();
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {


        //            Dictionary<string, object> dict = new Dictionary<string, object>();
        //            dict.Add("@PlanID", dr["PlanID"].ToString());

        //            DataSet dss = pptPlanManager.GetDataSetByStoreProcedure("ProcPlanNeedXCCheck", dict);
        //            if (dss.Tables[0].Rows[0][0].ToString() != "ok")
        //            {
        //                mss.Alert("提示", dr["RecipeMaterialName"].ToString() + dss.Tables[0].Rows[0][0].ToString());
        //                mss.Show();
        //                return;
        //            }
        //        }
        //    }

        //}
  

        try
        {
            DataSet ds = pptPlanManager.GetEquipPlan(equipCode, date);
            if (ds.Tables[Convert.ToInt32(shiftid) - 1].Rows.Count > 0)
            {

                //pptPlanManager.AllUpdatePlanState(equipCode, date, shiftid);

                string sql = @"UPDATE dbo.PptPlan SET PlanState=2 
                               WHERE PlanDate = '"+date+"' AND ShiftID='" + shiftid + "' AND RecipeEquipCode='" + equipCode + "' AND PlanState=1";
                DataSet dh= pptPlanManager.GetBySql(sql).ToDataSet();

                LoadGridData(equipCode);
            }
            else
            {
                mss.Alert("提示", "该机台没有计划信息！请先添加计划");
                mss.Show();
                return;
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// yuany 添加计划触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void AddRecipePlan(object sender, DirectEventArgs e)
    {


        string shiftid = e.ExtraParams["shiftFlag"].ToString();
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        string date = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd");
        if (equipCode == null || equipCode == "")
        {
            X.Msg.Alert("提示", "请先选择左侧机台!").Show();
            return;
        }
        #region 空值验证
        string validateStr = ValidateAddContent(shiftid);
        if (validateStr != "")
        {
            X.Msg.Alert("提示", validateStr).Show();
            return;
        }
        #endregion
        string proID = equipCode.Substring(1, 1);
        string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();
        if (Convert.ToInt32(count) <= 0)
        {

            mss.Alert("提示", "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息");
            mss.Show();
            return;
        }

        if (Convert.ToDateTime(date) < DateTime.Now.AddDays(-1))
        {
            mss.Alert("提示", "不能设置当前日期之前的计划信息！");
            mss.Show();
            return;
        }
        string planNo = pptPlanManager.GetGetMaxPlanId(date, equipCode, shiftid);

        string classID = pptShiftTimeManager.GetClassNameByPIDAndDate(shiftid, proID, date).Rows[0]["ShiftClassID"].ToString();

        PptPlan pptPlan = new PptPlan();
        WhereClip where = PptPlan._.PlanDate == date & PptPlan._.RecipeEquipCode == equipCode & PptPlan._.ShiftID == shiftid & PptPlan._.ClassID == classID;
        OrderByClip order = PptPlan._.PriLevel.Desc;
        EntityArrayList<PptPlan> lst = pptPlanManager.GetListByWhereAndOrder(where, order);
        if (lst.Count > 0)
        {
            //pptPlan.SerialNum = lst.Count + 1;
            pptPlan.PriLevel = lst[0].PriLevel + 1;
        }
        else
        {
            pptPlan.PriLevel = 1;
            //pptPlan.SerialNum = 1;
        }
        order = PptPlan._.SerialNum.Desc;
        lst = pptPlanManager.GetListByWhereAndOrder(where, order);

        if (lst.Count > 0)
        {
            pptPlan.SerialNum = lst[0].SerialNum + 1;
        }
        else
        {
            pptPlan.SerialNum = 1;
        }

        DateTime dt = Convert.ToDateTime(date);
        pptPlan.PlanDate = Convert.ToDateTime(date);
        pptPlan.PlanSource = "N";
        pptPlan.UrgencyState = 2;//计划紧急状态
        pptPlan.SmallCreate = 0;
        pptPlan.RecipeEquipCode = equipCode;
        pptPlan.ClassID = Convert.ToInt32(classID);//获取当前班次id;
        pptPlan.PlanID = planNo;
        pptPlan.ShiftID = Convert.ToInt32(shiftid);
        pptPlan.PlanNum = 1;
        pptPlan.PlanState = "1";
        pptPlan.OperDatetime = DateTime.Now;
        pptPlan.OperCode = this.UserID;//获取当前登录用户工号
        pptPlan.DeleteFlag = "0";

        //赋值操作
        switch (shiftid)
        {
            case "1":
                pptPlan.RecipeMaterialCode = add_recipe_material_code_zao.SelectedItem.Value;
                pptPlan.RecipeName = add_recipe_name_zao.SelectedItem.Value;
                pptPlan.PlanNum = Convert.ToInt32(add_plan_num_zao.RawValue);
                break;
            case "2":
                pptPlan.RecipeMaterialCode = add_recipe_material_code_zhong.SelectedItem.Value;
                pptPlan.RecipeName = add_recipe_name_zhong.SelectedItem.Value;
                pptPlan.PlanNum = Convert.ToInt32(add_plan_num_zhong.RawValue);
                break;
            case "3":
                pptPlan.RecipeMaterialCode = add_recipe_material_code_ye.SelectedItem.Value;
                pptPlan.RecipeName = add_recipe_name_ye.SelectedItem.Value;
                pptPlan.PlanNum = Convert.ToInt32(add_plan_num_ye.RawValue);
                break;
            default:
                break;
        }
        //
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
        //else
        //{
        //    int t = pptPlan.RecipeName.IndexOf("-");
        //    string edt = pptPlan.RecipeName.Substring(0, t);
        //    String sql = "select BasMaterialDL.* , materialname from BasMaterialDL left join BasMaterial on BasMaterialDL.MaterialCode =BasMaterial.MaterialCode where equipcode = '" + equipCode + "' and BasMaterialDL.materialcode = '" + pptPlan.RecipeMaterialCode + "' and BasMaterialDL.deleteflag = '0' and edtcode  = '" + edt + "'";
        //    DataSet ds = pmtRecipeManager.GetBySql(sql).ToDataSet();

        //    DataRow dr = ds.Tables[0].Rows[0];
        //    pptPlan.RecipeMaterialName = dr["materialname"].ToString();
        //    pptPlan.RecipeVersionID = int.Parse(edt);
        //    pptPlan.TotalWeight = decimal.Parse(dr["SetWeight"].ToString());
        //    pptPlan.PlanWeight = pptPlan.PlanNum * pptPlan.TotalWeight;

        //}

      
        //
        //pptPlanManager.Insert(pptPlan);

        //X.Msg.Notify("", planNo).Show();
        string sql = " select UserName from BasUser where Workbarcode ='" + pptPlan.OperCode + "' ";
        DataSet dhr = pptPlanManager.GetBySql(sql).ToDataSet();
        string username = "";
        if (dhr.Tables[0].Rows.Count > 0) username = dhr.Tables[0].Rows[0][0].ToString();
        string sqlstr = @" insert into Ppt_Plan ([Plan_id],[Plan_date],[Equip_code],[Mater_code],[Mater_Name],[Edt_code],[User_Edtcode],[Shift_id],[Shift_class],[Serial_num],[Pri_level],[recipe_code],[Recipe_Type],[Run_Type],[Print_Type],[Plan_Source],[Total_weight],[Plan_num],[Plan_weight],[Real_num],[Real_weight],[Urgency_state],[Plan_state]
           ,[Oper_datetime],[Plan_EndTime],[Real_StartTime],[Real_Endtime],[Oper_code],[revise_sgn],[SAP_BillNo],[Message],[ShowFlag],[worker_code],[mem_note],[remark],[FanLianNum],[avgtime],[CJName],[CJweight],[shelfnum],[smallcreate],[createPlanFlag],[DeleteFlag],[xlbagweight])  values('" + planNo + "','" + dt.ToString("yyyy-MM-dd") + "','" + equipCode + "','" + pptPlan.RecipeMaterialCode + "','" + pptPlan.RecipeMaterialName + "','" + pptPlan.RecipeVersionID + "','','" + pptPlan.ShiftID + "','" + pptPlan.ClassID + "','" + pptPlan.SerialNum + "','" + pptPlan.PriLevel + "','" + pptPlan.RecipeName + "','" + pptPlan.RecipeType + "','','','','" + pptPlan.TotalWeight + "','" + pptPlan.PlanNum + "','" + pptPlan.PlanWeight + "','" + pptPlan.RealNum + "',cast('0' as numeric(12,3)),'" + pptPlan.UrgencyState + "','" + pptPlan.PlanState + "',CONVERT(varchar(19),GETDATE(),120),'','','','" + pptPlan.OperCode + "','','','','','" + username + "','','','','','',cast('0' as numeric(8,3)),'','','','',cast('0' as numeric(12,3)))";
        //TT.Text = sqlstr; 
        //return;
        pptPlanManager.GetBySql(sqlstr).ToDataSet();

        LoadGridData(equipCode);
        //添加后清除值操作
        switch (shiftid)
        {
            case "1":
                add_recipe_material_code_zao.Value = "";
                add_recipe_name_zao.Value = "";
                add_plan_num_zao.RawValue = "";
                break;
            case "2":
                add_recipe_material_code_zhong.Value = "";
                add_recipe_name_zhong.Value = "";
                add_plan_num_zhong.RawValue = "";
                break;
            case "3":
                add_recipe_material_code_ye.Value = "";
                add_recipe_name_ye.Value = "";
                add_plan_num_ye.RawValue = "";
                break;
            default:
                break;
        }
        //X.Msg.Alert("提示", "添加成功!").Show();
    }

    /// <summary>
    /// 查找配方计划
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void FindRecipePlan(object sender, DirectEventArgs e)
    {
        string shiftid = e.ExtraParams["shiftFlag"].ToString();
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        string date = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd");
        if (equipCode == null || equipCode == "")
        {
            X.Msg.Alert("提示", "请先选择左侧机台!").Show();
            return;
        }
        string proID = equipCode.Substring(1, 1);
        string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();
        if (Convert.ToInt32(count) <= 0)
        {

            mss.Alert("提示", "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息");
            mss.Show();
            return;
        }

        string recipeMaterialCode = "";
        string recipeName = "";
        DataSet ds = new DataSet();
        switch (shiftid)
        {
            case "1":
                recipeMaterialCode = (add_recipe_material_code_zao.Value == null || add_recipe_material_code_zao.Value == "") ? "" : add_recipe_material_code_zao.SelectedItem.Value.ToString();
                recipeName = (add_recipe_name_zao.Value == null || add_recipe_name_zao.Value == "") ? "" : add_recipe_name_zao.SelectedItem.Value.ToString();
                ds = pptPlanManager.GetEquipPlan(equipCode, date, "1", recipeMaterialCode, recipeName);
                this.ZaoPlanStore.DataSource = ds.Tables[0];
                this.ZaoPlanStore.DataBind();
                txtcheshuzao.Text = ds.Tables[1].Rows[0]["PlanNum"].ToString();
                txtzhouqizao.Text = ds.Tables[2].Rows[0]["PlanNum"].ToString();
                break;
            case "2":
                recipeMaterialCode = (add_recipe_material_code_zhong.Value == null || add_recipe_material_code_zhong.Value == "") ? "" : add_recipe_material_code_zhong.SelectedItem.Value.ToString();
                recipeName = (add_recipe_name_zhong.Value == null || add_recipe_name_zhong.Value == "") ? "" : add_recipe_name_zhong.SelectedItem.Value.ToString();
                ds = pptPlanManager.GetEquipPlan(equipCode, date, "2", recipeMaterialCode, recipeName);
                this.ZhongPlanStore.DataSource = ds.Tables[0];
                this.ZhongPlanStore.DataBind();
                break;
            case "3":
                recipeMaterialCode = (add_recipe_material_code_ye.Value == null || add_recipe_material_code_ye.Value == "") ? "" : add_recipe_material_code_ye.SelectedItem.Value.ToString();
                recipeName = (add_recipe_name_ye.Value == null || add_recipe_name_ye.Value == "") ? "" : add_recipe_name_ye.SelectedItem.Value.ToString();
                ds = pptPlanManager.GetEquipPlan(equipCode, date, "3", recipeMaterialCode, recipeName);
                this.YePlanStore.DataSource = ds.Tables[0];
                this.YePlanStore.DataBind();
                txtchechuwan.Text = ds.Tables[1].Rows[0]["PlanNum"].ToString();
                txtzhouqiwan.Text = ds.Tables[2].Rows[0]["PlanNum"].ToString();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 添加当选择物料后获得配方编号
    /// yuany
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void FillAddRecipeComboBox(object sender, DirectEventArgs e)
    {
        string recipeMaterialCode = "";
        string shiftid = e.ExtraParams["shiftFlag"].ToString();
        switch (shiftid)
        {
            case "1": recipeMaterialCode = (add_recipe_material_code_zao.Value == null || add_recipe_material_code_zao.Value == "") ? "" : add_recipe_material_code_zao.SelectedItem.Value.ToString();
                break;
            case "2": recipeMaterialCode = (add_recipe_material_code_zhong.Value == null || add_recipe_material_code_zhong.Value == "") ? "" : add_recipe_material_code_zhong.SelectedItem.Value.ToString();
                break;
            case "3": recipeMaterialCode = (add_recipe_material_code_ye.Value == null || add_recipe_material_code_ye.Value == "") ? "" : add_recipe_material_code_ye.SelectedItem.Value.ToString();
                break;
            default:
                break;
        }
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;

        //X.Msg.Notify("", recipeMaterialCode+";"+equipCode).Show();
        EntityArrayList<PmtRecipe> lst = pmtRecipeManager.GetListByWhereAndOrder(PmtRecipe._.RecipeMaterialCode == recipeMaterialCode & PmtRecipe._.RecipeEquipCode == equipCode & PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1,PmtRecipe._.RecipeType.Asc);


        //if (lst.Count == 0)
        //{
        //    String sql = "select * from BasMaterialDL where equipcode= '" + equipCode + "' and materialcode = '" + recipeMaterialCode + "' and deleteflag = '0'";
        //    DataSet ds = pmtRecipeManager.GetBySql(sql).ToDataSet();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        PmtRecipe recipe = new PmtRecipe();
        //        recipe.RecipeMaterialCode = recipeMaterialCode;

        //        recipe.RecipeName = dr["EdtCode"].ToString() + "-" + dr["SetWeight"].ToString() + "kg";
        //        recipe.RecipeMaterialName = recipe.RecipeName;
        //        lst.Add(recipe);
        //    }

        //}
        //X.Msg.Notify("", lst.Count).Show();
        AddRecipeNameStore.DataSource = lst;
        AddRecipeNameStore.DataBind();

        if (lst.Count >= 1)
        {
            switch (shiftid)
            {
                case "1": add_recipe_name_zao.Value = lst[0].RecipeName;
                    break;
                case "2": add_recipe_name_zhong.Value = lst[0].RecipeName;
                    break;
                case "3": add_recipe_name_ye.Value = lst[0].RecipeName;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 校验添加和插入空值的方法
    /// </summary>
    /// <param name="shiftid"></param>
    /// <returns></returns>
    private string ValidateAddContent(string shiftid)
    {
        string recipeMaterialCode = "";
        string recipeName = "";
        object planNum = "";
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;
        switch (shiftid)
        {
            case "1":
                recipeMaterialCode = add_recipe_material_code_zao.SelectedItem.Value;
                recipeName = add_recipe_name_zao.SelectedItem.Value;
                planNum = add_plan_num_zao.RawValue;
                break;
            case "2":
                recipeMaterialCode = add_recipe_material_code_zhong.SelectedItem.Value;
                recipeName = add_recipe_name_zhong.SelectedItem.Value;
                planNum = add_plan_num_zhong.RawValue;
                break;
            case "3":
                recipeMaterialCode = add_recipe_material_code_ye.SelectedItem.Value;
                recipeName = add_recipe_name_ye.SelectedItem.Value;
                planNum = add_plan_num_ye.RawValue;
                break;
            default:
                break;
        }

        if (recipeMaterialCode == null || recipeMaterialCode == "")
        {
            return "物料名称不能为空!";
        }
        if (recipeName == null || recipeName == "")
        {
            return "配方编号不能为空!";
        }
        if (planNum == null || planNum == "")
        {
            return "生产车数不能为空!";
        }

        EntityArrayList<PmtRecipe> recipeLst = pmtRecipeManager.GetListByWhere(
            PmtRecipe._.RecipeMaterialCode == recipeMaterialCode &
            PmtRecipe._.RecipeName == recipeName &
            PmtRecipe._.RecipeEquipCode == equipCode &
            PmtRecipe._.AuditFlag == 1 & PmtRecipe._.RecipeState == 1);
        if (recipeLst.Count == 0)
        {
            return "请选择有效的物料名称!";
            //String sql = "select * from BasMaterialDL where equipcode = '" + equipCode + "' and materialcode = '" + recipeMaterialCode + "' and deleteflag = '0' ";
            //DataSet ds = pmtRecipeManager.GetBySql(sql).ToDataSet();
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    return "请选择有效的物料名称!";
            //}
        }

        return "";
    }

    /// <summary>
    /// 复制计划
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CopyRecipePlan(object sender, DirectEventArgs e)
    {
        string shiftid = e.ExtraParams["shiftFlag"].ToString();
        string equipCode = "";
        equipCode = this.Session["equipCode"] as string;

        _equipCode = equipCode;
        _shiftID = shiftid;

        string date = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd");
        if (equipCode == null || equipCode == "")
        {
            X.Msg.Alert("提示", "请先选择左侧机台!").Show();
            return;
        }

        string proID = equipCode.Substring(1, 1);
        string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();
        if (Convert.ToInt32(count) <= 0)
        {

            mss.Alert("提示", "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息");
            mss.Show();
            return;
        }

        DateField1.Value = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd");
        DateField2.Value = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        //cbxStorageType2.Select(0);
        cbxStorageType2.Value = _shiftID;
        winCopy.Show();        
    }

    public void btnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            string proID = _equipCode.Substring(1, 1);
            string date = Convert.ToDateTime(this.DateField2.Text).ToString("yyyy-MM-dd");
            string count = pptShiftTimeManager.GetClassNameByPIDAndDate("0", proID, date).Rows[0]["Num"].ToString();
            if (Convert.ToInt32(count) <= 0)
            {

                mss.Alert("提示", "还没有设置" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "班次信息！请先设置班次信息");
                mss.Show();
                return;
            }

            IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("@FromDt", DateField1.RawText);
            dict.Add("@ToDt", DateField2.RawText);
            dict.Add("@FromShift", _shiftID);
            dict.Add("@ToShift", cbxStorageType2.Value.ToString());
            dict.Add("@FromEquip", _equipCode);
            dict.Add("@ToEquip", _equipCode);
            dict.Add("@OpCode", this.UserID);
            dict.Add("@ToShiftClass", "");

            bBasWorkShopManager.GetDataSetByStoreProcedure("Proc_PlanCopy", dict);

            X.Msg.Alert("提示", "计划复制成功").Show();
            this.winCopy.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("提示", "计划复制失败：" + ex).Show();
        }
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winCopy.Close();
    }
}