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

public partial class Manager_ShopStorage_JarClearRe : Mesnac.Web.UI.Page
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

            查询 = new SysPageAction() { ActionID = 2, ActionName = "btnSearch" };
          
        }

        public SysPageAction 查询 { get; private set; } //必须为 public
      
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
        if (!X.IsAjaxRequest && !IsPostBack)
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
   

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
    
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptPlanMgr> pageParams = new PageResult<PptPlanMgr>();
     
     
        String sql = "select * from Pstmminjar where ClearFlag = '1' and ClearTime >= '"+txt_start_plan_date.Text+
            "' and ClearTime <='" + txt_end_plan_date.Text + " 23:59:59'";
        sql = @"select *,( RealWeight-isnull(UsedWeigh,'0')) as RWeight from Pstmminjar  left join basequip on Pstmminjar.equipcode = basequip.equipcode 
     left join basmaterial on Pstmminjar.matercode = basmaterial.materialcode where ClearFlag = '1' ";
        sql = sql + " and  ClearTime >= '"+txt_start_plan_date.Text+ "'";
        sql = sql + " and  ClearTime <= '" + DateTime.Parse(txt_end_plan_date.Text).AddDays(1).ToString("yyyy-MM-dd") + "'";
        if (!String.IsNullOrEmpty(cbo_equip_code.SelectedItem.Value))
        { sql = sql + " and  basequip.equipcode = '" + cbo_equip_code.SelectedItem.Value + "'"; }

        if (!String.IsNullOrEmpty(hidden_material_code.Text))
        { sql = sql + " and  basmaterial.materialcode  = '" + hidden_material_code.Text + "'"; }
        DataSet ds = manager.GetBySql(sql).ToDataSet();











        DataTable data = ds.Tables[0];

        int total = data.Rows.Count;
        //txt_start_plan_date.Text = sql;
        return new { data, total };
    }
    #endregion

    #region 打印
   

    #endregion

    #region 增删改查按钮激发的事件
   

    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
     
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

 
    #endregion




    #region 点击进行批量删除
    [Ext.Net.DirectMethod]
    public void BtnAllDelete_Click(object sender, DirectEventArgs e)
    {
    
    }
    #endregion

    #region 下达计划相关操作
    public void BtnCreatePlan_Click(object sender, DirectEventArgs e)
    {
        
    }

    public void BtnCreatePlanSave_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] planDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        if (planDic.Length == 0)
        {
            this.msg.Alert("提示", "请选择需要恢复的条码!").Show();
            return;
        }
        #region 下达计划信息筛选校验
        foreach (Dictionary<string, string> planRow in planDic)
        {
            string JarID = planRow["JarID"];
            string EquipCode = planRow["EquipCode"];
            string FeedJarNo = planRow["FeedJarNo"];
            String sql = "update Pstmminjar set ClearFlag = null,InTime = getdate(),BDsavetime = getdate() where JarID='" + JarID + "' ";
    
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            sql = "select * from PstJarClear where EquipCode = '" + EquipCode + "' and Jarid = '" + FeedJarNo + "' and flag ='2'";
            ds = manager.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count == 0)
            {
                sql = "insert into PstJarClear values ('" + EquipCode + "','" + FeedJarNo + "',getdate(),'2')";
                ds = manager.GetBySql(sql).ToDataSet();
            }

        }
        #endregion

    

        pageToolBar.DoRefresh();
        this.msg.Alert("提示", "恢复完成").Show();


    }

     #endregion

  
}