﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Entity;
using NBear.Common;
using System.Data;
using Mesnac.Business.Implements;

public partial class Manager_ProducingPlan_PlanExecMonitoring_PlanMonitoring : Mesnac.Web.UI.Page
{
    BasEquipManager baseEquipManager = null;// new BasEquipManager();
    PptPlanManager pptPlanManager = null;// new PptPlanManager();
    PmtRecipeManager pmtRecipeManager = null;
    PptShiftTimeManager pptShiftTimeManager = null;
    Ext.Net.MessageBox mss = new MessageBox();//提示信息
    protected void Page_Load(object sender, EventArgs e)
    {
        baseEquipManager = new BasEquipManager();
        pptPlanManager = new PptPlanManager();
        pmtRecipeManager = new PmtRecipeManager();
        pptShiftTimeManager = new PptShiftTimeManager();
       
        if (!X.IsAjaxRequest)
        {
            this.TaskManager1.StartAll();
            this.Session["MonitoringequipCode"] = null;
            txtStratPlanDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            InitTreeDept();
        }
    }
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {

            查询 = new SysPageAction() { ActionID = 1, ActionName = "txtStratPlanDate" };
            修改 = new SysPageAction() { ActionID = 2, ActionName = "MenuDel,MenuItem2,MenuItem8" };
            执行查询 = new SysPageAction() { ActionID = 3, ActionName = "MenuItem9,MenuItem3,MenuEdit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; }
        public SysPageAction 执行查询 { get; private set; }
    }
    #endregion


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
        var query = baseEquipManager.GetListByWhereAndOrder(BasEquip._.EquipType < "03"&&BasEquip._.DeleteFlag==0, BasEquip._.EquipGroup.Asc).GroupBy(pet => pet.EquipGroup).Where(pet=>!string.IsNullOrEmpty(pet.Key));
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
        this.Session["MonitoringequipCode"] = equipCode;
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

          //  string zhongClass = dtZHong.Rows[0]["ClassName"].ToString();
           // string zhongShift = dtZHong.Rows[0]["ShiftName"].ToString();
            string yeClass = dtYe.Rows[0]["ClassName"].ToString();
            string yeShift = dtYe.Rows[0]["ShiftName"].ToString();

            zaoPanel.Html = "<table width='100%' height='100%'><tr align='center'><td align='center'>" + zaoShift + "--" + zaoClass + "</td></tr></table> ";
           // ZhongPanel.Html = "<table width='100%' height='100%'><tr align='center'><td align='center'>" + zhongShift + "--" + zhongClass + "</td></tr></table> ";
            YePanel.Html = "<table width='100%' height='100%'><tr align='center'><td align='center'>" + yeShift + "--" + yeClass + "</td></tr></table> ";
        }
        #endregion

        #region 绑定对应的早中晚 计划信息
        DataSet ds = GetPlanMonitor(equipCode, date);
        this.ZaoPlanStore.DataSource = ds.Tables[0];
        this.ZaoPlanStore.DataBind();


        this.ZhongPlanStore.DataSource = ds.Tables[1];
        this.ZhongPlanStore.DataBind();

        this.YePlanStore.DataSource = ds.Tables[2];
        this.YePlanStore.DataBind();

        //获取对应机台的物料配方 删除标识未添加
        this.Panel3.Title = baseEquipManager.GetListByWhere(BasEquip._.EquipCode == equipCode)[0].EquipName;
        //this.Panel3.Title = ds.Tables[0].Rows[0][0].ToString();
        #endregion
    }
    public DataSet GetPlanMonitor(string equipCode, string date)
    {
        DataSet ds = new DataSet();
        //早班信息
        string sql = @"SELECT  Mem_note,RealStartTime,PlanID,RecipeMaterialName+Itemname RecipeMaterialName,PlanDate,RecipeEquipCode,CONVERT(VARCHAR(5),RealNum)+'/'+CONVERT(VARCHAR(5),PlanNum) RealPlanNum,CAST(ROUND(RealNum*1.0/PlanNum,4) AS numeric(5,2)) AS Num,CONVERT(VARCHAR(5),CAST(ROUND((RealNum*1.0/PlanNum)*100,3) AS numeric(5,0)))+'%' AS Per,RealEndtime 
 FROM dbo.PptPlan left join SysCode on TypeID = 'PmtType' and RecipeType = itemcode WHERE PlanDate = '" + date + "' AND ShiftID=1 AND PlanNum!=0 AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 ORDER BY PriLevel ;";

        //中班信息
        sql += @"SELECT Mem_note,RealStartTime,PlanID,RecipeMaterialName +Itemname  RecipeMaterialName,PlanDate,RecipeEquipCode,CONVERT(VARCHAR(5),RealNum)+'/'+CONVERT(VARCHAR(5),PlanNum) RealPlanNum,CAST(ROUND(RealNum*1.0/PlanNum,4) AS numeric(5,2)) AS Num,CONVERT(VARCHAR(5),CAST(ROUND((RealNum*1.0/PlanNum)*100,3) AS numeric(5,0)))+'%' AS Per,RealEndtime 
 FROM dbo.PptPlan left join SysCode on TypeID = 'PmtType' and RecipeType = itemcode  WHERE PlanDate = '" + date + "' AND ShiftID=2  AND PlanNum!=0 AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 ORDER BY PriLevel ;";
        //夜班信息
        sql += @"SELECT Mem_note,RealStartTime,PlanID,RecipeMaterialName +Itemname  RecipeMaterialName,PlanDate,RecipeEquipCode,CONVERT(VARCHAR(5),RealNum)+'/'+CONVERT(VARCHAR(5),PlanNum) RealPlanNum,CAST(ROUND(RealNum*1.0/PlanNum,4) AS numeric(5,2)) AS Num,CONVERT(VARCHAR(5),CAST(ROUND((RealNum*1.0/PlanNum)*100,3) AS numeric(5,0)))+'%' AS Per,RealEndtime 
 FROM dbo.PptPlan left join SysCode on TypeID = 'PmtType' and RecipeType = itemcode  WHERE PlanDate = '" + date + "' AND ShiftID=3 AND PlanNum!=0 AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 ORDER BY PriLevel ;";
        ds = pptPlanManager.GetBySql(sql).ToDataSet();

        return ds;
    }
    /// <summary>
    /// 增加新的计划
    /// </summary>
    [DirectMethod]
    public bool AddPlan(string shiftid)
    {
        string equipCode = "";
        equipCode = this.Session["MonitoringequipCode"] as string;
        string date = this.txtStratPlanDate.Text;

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
        LoadGridData(equipCode);
        return true;
    }



    [DirectMethod]
    public bool InsertPlan(int priLevel, string shiftid)
    {
        string equipCode = "";
        equipCode = this.Session["MonitoringequipCode"] as string;
        string date = this.txtStratPlanDate.Text;

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

        if (pptPlanManager.UpdatePriLevel(date, equipCode, shiftid, priLevel))
        {
            pptPlanManager.Insert(pptPlan);
        }
        LoadGridData(equipCode);
        return true;
    }

    [DirectMethod]
    public bool RealPlanInfo(string planID,string flag)
    {
        if (flag == "0")
        {
            this.txtPlanNum.Disabled = false;
            this.txtPlanWeight.Disabled = false;
            this.txtRealNum.Disabled = false;
            this.txtRealWeight.Disabled = false;
            this.btnModifySave.Disabled = false;
        }
        else if (flag == "1")
        {
            this.txtPlanNum.Disabled = true;
            this.txtPlanWeight.Disabled = true;
            this.txtRealNum.Disabled = true;
            this.txtRealWeight.Disabled = true;
            this.btnModifySave.Disabled = true;
        }
        try
        {
            PptPlan ppt = pptPlanManager.GetListByWhere(PptPlan._.PlanID==planID)[0];
            if (ppt == null)
                return false;
            else
            {
                this.txtPlanID.Text = planID;
                this.txtRecipeMaterialName.Text = ppt.RecipeMaterialName;
                this.txtRecipeName.Text = ppt.RecipeName;
                txtPlanNum.Text = ppt.PlanNum.ToString();
                txtPlanWeight.Text = ppt.PlanWeight.ToString();
                txtRealNum.Text = ppt.RealNum.ToString();
                txtRealWeight.Text = ppt.RealWeight.ToString();
                TextNote.Text = ppt.Mem_note.ToString();
                txtRealStartTime.Text = Convert.ToDateTime(ppt.RealStartTime).ToString("yyyy-MM-dd");
                txtRealEndtime.Text = Convert.ToDateTime(ppt.RealEndtime).ToString("yyyy-MM-dd");
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    [DirectMethod]
    public bool PlanNumChage()
    {
        string plannum = "1";
        string realnum = "0";
        PptPlan ppt = pptPlanManager.GetListByWhere(PptPlan._.PlanID == this.txtPlanID.Text)[0];
        if (ppt == null)
            return false;
        else
        {
            if (txtPlanNum.Text != "")
            {
                plannum=txtPlanNum.Text;
            }
            if (txtRealNum.Text != "")
            {
                realnum=txtRealNum.Text;
            }
            //this.txtPlanWeight.Text = (ppt.TotalWeight * Convert.ToInt32(plannum)).ToString();
            //this.txtRealWeight.Text = (ppt.TotalWeight * Convert.ToInt32(realnum)).ToString();
            return true;
        }
 
    }
    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// 孙宜建   2013年2月16日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        PptPlan ppt = pptPlanManager.GetListByWhere(PptPlan._.PlanID == this.txtPlanID.Text)[0];
        if (ppt == null)
            return;
        else
        {
            string plannum = "1";
            string realnum = "0";
            if (txtPlanNum.Text != "")
            {
                plannum = txtPlanNum.Text;
            }
            if (txtRealNum.Text != "")
            {
                realnum = txtRealNum.Text;
            }
            ppt.PlanNum = Convert.ToInt32(plannum);
            ppt.PlanWeight = Convert.ToDecimal(this.txtPlanWeight.Text);
            ppt.RealNum = Convert.ToInt32(realnum);
            ppt.RealWeight = Convert.ToDecimal(txtRealWeight.Text);
            ppt.Mem_note = TextNote.Text;
            //pptPlanManager.Update(ppt);
            String sql = "update ppt_plan  set Plan_Num = " + Convert.ToInt32(plannum) + ",Plan_Weight ='" + ppt.PlanWeight + "',Real_Num  ="
                + ppt.RealNum + " ,Real_Weight = '" + ppt.RealWeight + "', Mem_note='" + TextNote.Text + "' where plan_id ='" + this.txtPlanID.Text + "'";
           
            DataSet ds = pptPlanManager.GetBySql(sql).ToDataSet();
            LoadGridData(ppt.RecipeEquipCode);
            this.winModify.Close();
        }
    }
    /// <summary>
    /// 点击修改信息中取消按钮激发的事件
    /// 孙宜建   2013年2月16日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        this.winModify.Close();
    }
    [DirectMethod]
    public bool DateChage()
    {
        if (this.Session["MonitoringequipCode"] == null)
        {
            return false;
        }
        else
        {
            LoadGridData(this.Session["MonitoringequipCode"].ToString());
            return true;
        }

    }

    /// <summary>
    /// 时间刷新事件 10秒
    /// 孙宜建
    /// 2013-3-27
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RefreshStore(object sender, DirectEventArgs e)
    {
        if (this.Session["MonitoringequipCode"] == null)
        {
            return;
        }
        LoadGridData(this.Session["MonitoringequipCode"].ToString());
    }
}