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


public partial class Manager_Equipment_SparePart_SparePartList : Mesnac.Web.UI.Page
{
    protected Eqm_bjtpcdManager Eqm_bjtpcdManager = new Eqm_bjtpcdManager();
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
        bindList();
    }


    private DataSet getList(string txtBJ_Name)
    {


        return GetDataByParas(txtBJ_Name);
       // return GetDataByParas(cboPlan.Value.ToString);
    }

    public System.Data.DataSet GetDataByParas(string txtBJ_Name)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select * from Eqm_Bjtpcd");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(txtBJ_Name))
        {
            sb.AppendLine("AND BJ_tpname='" + txtBJ_Name + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = Eqm_bjtpcdManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        string Name = "";
        if (!string.IsNullOrEmpty(txtBJ_Name.Text))
        {
            Name = txtBJ_Name.Text.ToString();
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
        txtbjcode.Text = "";
        txtbjname.Text = "";
        this.winSave.Hidden = false;
    }
    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            EntityArrayList<Eqm_bjtpcd> listAdd = Eqm_bjtpcdManager.GetListByWhere(Eqm_bjtpcd._.BJ_tpname == txtbjname.Text);
            if (listAdd.Count > 0)
            { X.Msg.Alert("提示", "已有该备件！").Show(); return; }
            Eqm_bjtpcd record = new Eqm_bjtpcd();
            record.BJ_tpcode = txtbjcode.Text;
            record.BJ_tpname = txtbjname.Text;

            if (Eqm_bjtpcdManager.Insert(record) >= 0)
            {
                this.AppendWebLog("备件添加", "添加备件号：" + record.BJ_tpcode);
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            Eqm_bjtpcd record = Eqm_bjtpcdManager.GetById(hideObjID.Text);

            if (record != null)
            {
            //    record.BJ_tpcode = txtbjcode.Text;
                if (txtbjcode.Text != record.BJ_tpcode)
                { X.Msg.Alert("提示", "只允许修改备件名称，不能修改备件编码！").Show(); return; }
                record.BJ_tpname = txtbjname.Text;

                if (Eqm_bjtpcdManager.Update(record) >= 0)
                {
                    this.AppendWebLog("备件修改", "修改备件号：" + record.BJ_tpcode);
                    winSave.Hidden = true;
                    bindList();
                    X.Msg.Alert("提示", "修改完成！").Show();
                }
                else
                {
                    X.Msg.Alert("提示", "修改失败！").Show();
                }
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
    public void pnlList_Edit(string ObjID)
    {
        EntityArrayList<Eqm_bjtpcd> list = Eqm_bjtpcdManager.GetListByWhere(Eqm_bjtpcd._.BJ_tpcode == ObjID);
        if(list.Count>0)
        {
            Eqm_bjtpcd record = list[0];

            if (record != null)
            {
                txtbjcode.Text = record.BJ_tpcode.ToString();
                txtbjname.Text = record.BJ_tpname.ToString();
                hideObjID.Text = ObjID;
                this.hideMode.Text = "Edit";

                this.winSave.Hidden = false;
            }
            else
            {
                bindList();
                X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            }
        }
    }
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_bjtpcd record = Eqm_bjtpcdManager.GetById(ObjID);
        if(record != null)
        {
            Eqm_bjtpcdManager.Delete(ObjID);
            this.AppendWebLog("备件删除", "修改备件号：" + record.BJ_tpcode);
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