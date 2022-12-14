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


public partial class Manager_Equipment_SparePart_Lube : Mesnac.Web.UI.Page
{
    protected Eqm_lubeManager lubeManager = new Eqm_lubeManager();
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


    private DataSet getList(string txtJLName)
    {


        return GetDataByParas(txtJLName);
       // return GetDataByParas(cboPlan.Value.ToString);
    }

    public System.Data.DataSet GetDataByParas(string txtOil_name)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select * from Eqm_lube");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(txtOil_name))
        {
            sb.AppendLine("AND Oil_name='" + txtOil_name + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = lubeManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        string Name = "";
        if (!string.IsNullOrEmpty(txtOil_name.Text))
        {
            Name = txtOil_name.Text.ToString();
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
        txtOilname.Text = "";
        this.winSave.Hidden = false;
    }
    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            EntityArrayList<Eqm_lube> listAdd = lubeManager.GetListByWhere(Eqm_lube._.Oil_name == txtOilname.Text);
            if (listAdd.Count > 0)
            { X.Msg.Alert("提示", "已有该油品！").Show(); return; }
            Eqm_lube record = new Eqm_lube();
            record.Oil_name = txtOilname.Text;
            record.Oil_code = GetMaxPlanID();

            if (lubeManager.Insert(record) >= 0)
            {
                this.AppendWebLog("油品添加", "添加油品号：" + record.Oil_code);
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
            Eqm_lube record = lubeManager.GetById(hideObjID.Text);

            if (record != null)
            {
                record.Oil_name = txtOilname.Text;

                if (lubeManager.Update(record) >= 0)
                {
                    this.AppendWebLog("油品修改", "修改油品号：" + record.Oil_code);
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
        EntityArrayList<Eqm_lube> list = lubeManager.GetListByWhere(Eqm_lube._.Oil_code == ObjID);
        if(list.Count>0)
        {
            Eqm_lube record = list[0];

            if (record != null)
            {
                txtOilname.Text = record.Oil_name.ToString();
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
        Eqm_lube record = lubeManager.GetById(ObjID);
        if(record != null)
        {
            lubeManager.Delete(ObjID);
            this.AppendWebLog("油品删除", "修改油品号：" + record.Oil_code);
            bindList();
            X.Msg.Alert("提示", "删除成功！").Show();
        }
        else
        {
            X.Msg.Alert("提示", "该记录已经不存在！").Show();
            return;
        }
    }


    //自动生成油品主键号
    protected string GetMaxPlanID()
    {
        string planID = "";
        EntityArrayList<Eqm_lube> list = lubeManager.GetAllListOrder(Eqm_lube._.Oil_code.Desc);
        if (list.Count > 0)
        {
            planID = list[0].Oil_code;
            if (Convert.ToInt32(list[0].Oil_code) < 9)
            {
                planID = "00" + (Convert.ToInt32(list[0].Oil_code) + 1).ToString();
            }
            else if (Convert.ToInt32(list[0].Oil_code) < 99)
            {
                planID = "0" + (Convert.ToInt32(list[0].Oil_code) + 1).ToString();
            }
            else
            {
                planID = (Convert.ToInt32(list[0].Oil_code) + 1).ToString();
            }
        }
        else 
        {
            planID = "001";
        }
        return planID;
    }

    #endregion


}