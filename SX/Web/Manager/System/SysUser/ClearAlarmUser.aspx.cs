﻿using System;
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


public partial class Manager_System_SysUser_ClearAlarmUser : Mesnac.Web.UI.Page
{
    protected SYS_USERManager userManager = new SYS_USERManager();
    protected Sys_ClearAlarmUserManager Manager = new Sys_ClearAlarmUserManager();
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
        bindUser();
        bindList();
    }


    private DataSet getList()
    {
        return GetDataByParas();
    }
    private void bindUser()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<SYS_USER> list = userManager.GetListByWhere(where);
        foreach (SYS_USER main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.USER_NAME, main.USER_ID);
            cbxName.Items.Add(item);
        }
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select * from sys_clearalarmuser");
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
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
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.winSave.Hidden = false;
        
    }
    //添加确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
            EntityArrayList<Sys_ClearAlarmUser> listAdd = Manager.GetListByWhere(Sys_ClearAlarmUser._.User_name == cbxName.Text);
            if (listAdd.Count > 0)
            { X.Msg.Alert("提示", "已有该用户！").Show(); return; }
            Sys_ClearAlarmUser record = new Sys_ClearAlarmUser();
            record.User_name = cbxName.SelectedItem.Text;
            EntityArrayList<SYS_USER> list = userManager.GetListByWhere(SYS_USER._.USER_ID == cbxName.SelectedItem.Value);
            if (list.Count > 0)
            {
                record.User_Code = list[0].USER_ID;
            }

            if (Manager.Insert(record) >= 0)
            {
                this.AppendWebLog("用户添加", "添加用户：" + record.User_name);
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
   
    [DirectMethod]
    public void pnlList_Delete(string user_Code)
    {
        Sys_ClearAlarmUser record = Manager.GetById(user_Code);
        if(record != null)
        {
            Manager.Delete(user_Code);
            this.AppendWebLog("用户删除", "修改用户号：" + record.User_Code);
            bindList();
            X.Msg.Alert("提示", "删除成功！").Show();
        }
        else
        {
            X.Msg.Alert("提示", "该用户已经不存在！").Show();
            return;
        }
    }

    #endregion


}