using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using NBear.Common;
using System.Collections.Generic;
using System.Text;
using Mesnac.Entity;


public partial class Manager_ProducingPlan_PlanEntering_PlanUse : Mesnac.Web.UI.Page
{
    protected PmtRecipeManager RecipeManager = new PmtRecipeManager();
    protected Pmt_equipManager EquipManager = new Pmt_equipManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {

        
        this.winSave.Hidden = true;
        bindBasEquip();
        bindBasMater();
        this.cbxequip.SelectedItem.Value = "01001";
        bindList();
    }
    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equip> list = EquipManager.GetListByWhere(where);
        foreach (Pmt_equip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Equip_name, main.Equip_code);
            cbxequip.Items.Add(item);
        }
    }
    private void bindBasMater()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<PmtRecipe> list = RecipeManager.GetListByWhere(where);
        foreach (PmtRecipe main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.RecipeMaterialName, main.RecipeMaterialCode);
            cbxpeifang.Items.Add(item);
        }
    }


    private DataSet getList()
    {


        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select t.RecipeEquipCode,t1.Equip_name,RecipeVersionID,RecipeMaterialName,case RecipeType when 0 then '正常' when 1 then '试制' when 2 then '返炼' when 3 then '试验' when 4 then '欧盟' when 5 then '掺胶' else '' end Type,
case RecipeState when 0 then '用完' when 1 then '正用' when 2 then '作废' else '' end State,case Plan_Use when 0 then '否' when 1 then '是' else '' end Plan_Use
 from PmtRecipe t
left join Pmt_equip t1 on t1.Equip_code=t.RecipeEquipCode");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(cbxequip.SelectedItem.Value))
        {
            sb.AppendLine("AND RecipeEquipCode='" + cbxequip.SelectedItem.Value + "'");
        }
        if (!string.IsNullOrEmpty(cbxpeifang.SelectedItem.Value))
        {
            sb.AppendLine("AND RecipeMaterialCode='" + cbxpeifang.SelectedItem.Value + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = RecipeManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"update PmtRecipe set Plan_Use = ");
        if (cbxshiyong.Text == "否")
        { sb.AppendLine("0"); }
        else if (cbxshiyong.Text == "是")
        { sb.AppendLine("1"); }
        sb.AppendLine(" WHERE 1=1");
        sb.AppendLine(" AND RecipeEquipCode='" + hideMode.Text + "'");
        sb.AppendLine(" AND RecipeVersionID='" + hideMode1.Text + "'");
        sb.AppendLine(" AND RecipeMaterialName='" + hideMode2.Text + "'");
        #endregion

       // NBear.Data.CustomSqlSection css = RecipeManager.GetBySql(sb.ToString());
        RecipeManager.GetBySql(sb.ToString()).ToDataSet();
        this.winSave.Hidden = true;
        bindList();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string RecipeEquipCode, string RecipeVersionID, string RecipeMaterialName)
    {
        EntityArrayList<PmtRecipe> list = RecipeManager.GetListByWhere(PmtRecipe._.RecipeEquipCode == RecipeEquipCode && PmtRecipe._.RecipeVersionID == RecipeVersionID && PmtRecipe._.RecipeMaterialName == RecipeMaterialName);
        if(list.Count>0)
        {
            PmtRecipe record = list[0];

            if (record != null)
            {
                if(record.Plan_Use==0)
                {
                    cbxshiyong.Text = "否";
                }
                else if (record.Plan_Use == 1)
                {
                    cbxshiyong.Text = "是";
                }
                else { }
                this.hideMode.Text = RecipeEquipCode;
                this.hideMode1.Text = RecipeVersionID;
                this.hideMode2.Text = RecipeMaterialName;

                this.winSave.Hidden = false;
            }
            else
            {
                bindList();
                X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            }
        }
    }

    #endregion

}