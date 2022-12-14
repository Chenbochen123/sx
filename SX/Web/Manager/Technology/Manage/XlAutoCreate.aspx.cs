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


public partial class Manager_Technology_Manage_XlAutoCreate : Mesnac.Web.UI.Page
{
    protected Pmt_XLAutoCreateManager Manager = new Pmt_XLAutoCreateManager();
    protected Pmt_materialManager MaterManager = new Pmt_materialManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
            bindBasEquip();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {

        
        this.winSave.Hidden = true;
        bindList();
    }

    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_material> list = MaterManager.GetListByWhere(Pmt_material._.Mkind_code == "2" && Pmt_material._.Ikind_code == "01");
        foreach (Pmt_material main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Mater_name, main.Mater_code);
            cbxmater2.Items.Add(item);
            cbxmater1.Items.Add(item);
        }
    }

    private DataSet getList(string txtName)
    {


        return GetDataByParas(txtName);
      
    }

    public System.Data.DataSet GetDataByParas(string txtname)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select * from  pmt_XlAutoCreate");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(txtname))
        {
            sb.AppendLine("AND mater_code='" + txtname + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        string Name = "";
        if (!string.IsNullOrEmpty(cbxmater1.Text))
        {
            Name = cbxmater1.Text.ToString();
        }
        this.store.DataSource = getList(Name);
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        cbxmater2.Text = "";
        this.winSave.Hidden = false;
    }
    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            EntityArrayList<Pmt_XLAutoCreate> listAdd = Manager.GetListByWhere(Pmt_XLAutoCreate._.Mater_Code == cbxmater2.Text);
            if (listAdd.Count > 0)
            { X.Msg.Alert("提示", "已有该物料！").Show(); return; }
            Pmt_XLAutoCreate record = new Pmt_XLAutoCreate();
            record.Mater_Code = cbxmater2.Text;
            EntityArrayList<Pmt_material> listAdd2 = MaterManager.GetListByWhere(Pmt_material._.Mater_code == cbxmater2.Text);
            if(listAdd2.Count>0)
            { record.Mater_name = listAdd2[0].Mater_name; }
            else{X.Msg.Alert("提示", "已经不存在该物料！").Show();}
            

            if (Manager.Insert(record) >= 0)
            {
                this.AppendWebLog("物料添加", "添加物料号：" + record.Mater_Code);
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
  
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Pmt_XLAutoCreate record = Manager.GetById(ObjID);
        if(record != null)
        {
            Manager.Delete(ObjID);
            this.AppendWebLog("物料删除", "修改物料号：" + record.Mater_Code);
            bindList();
            X.Msg.Alert("提示", "删除成功！").Show();
        }
        else
        {
            X.Msg.Alert("提示", "该记录已经不存在！").Show();
            return;
        }
    }


    #endregion


}