using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_RubberQuality_BasicInfo_CheckItemType : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            添加 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "" };
            删除 = new SysPageAction() { ActionID = 4, ActionName = "btnDelete" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
    }
    #endregion

    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected QmtCheckItemManager itemManager = new QmtCheckItemManager();
    protected QmtCheckItemTypeManager manager = new QmtCheckItemTypeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindDeleteFlag();
            bindList();
            this.winSave.Hidden = true;
            this.hdnHasEditAction.SetValue(this._.修改.SeqIdx == 0);
        }
    }
    #region 自定义方法
    private void bindDeleteFlag()
    {
       
    }
    private DataSet getList()
    {
        QmtCheckItemTypeParams _params = new QmtCheckItemTypeParams();
        _params.typeName = this.txtItemTypeName.Text.Trim();

        return manager.GetDataByParas(_params);
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        this.txtObjID.Text = string.Empty;
        //this.txtTypeID.Text = string.Empty;
        this.txtTypeName.Text = string.Empty;

        this.winSave.Hidden = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text.Equals("Add"))
        {
            string sql = "select MAX(Item_class)+1 from Qmt_Itemclass";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into Qmt_Itemclass values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + txtTypeName.Text + "')";
            manager.GetBySql(sql).ToDataSet();

            X.Msg.Alert("操作", "添加成功").Show();
        }
        else
        {
            string sql = "update Qmt_Itemclass set Item_classname='" + txtTypeName.Text + "' where Item_class='" + txtObjID.Text + "';";
            manager.GetBySql(sql).ToDataSet();

            X.Msg.Alert("操作", "修改成功").Show();

        }
        this.winSave.Hidden = true;
        bindList();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winSave.Hidden = true;
    }

    /// <summary>
    /// 创建标识：qusf 20140108
    /// 创建说明：删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [DirectMethod]
    public bool btnDelete_Click(string objID)
    {
        string sql = "delete Qmt_Itemclass where Item_class='" + objID + "'";
        manager.GetBySql(sql).ToDataSet();
        AppendWebLog("检验项目类型-删除", objID);
        bindList();
        return true;


    
    }
    #endregion

    #region GridPanel事件响应
    [DirectMethod]
    public void pnlList_Edit(string commandName, string ObjID)
    {
        QmtCheckItemType type = manager.GetById(Convert.ToInt32(ObjID));
        if (type != null)
        {
            this.hideMode.Text = "Edit";
            this.txtObjID.Text = type.ObjID.ToString();
            //this.txtTypeID.Text = type.ItemTypeID;
            this.txtTypeName.Text = type.ItemTypeName;


            this.winSave.Hidden = false;
        }
        else
        {
            X.Msg.Alert("提示", "此记录没有找到，请重新操作").Show();
            bindList();
        }
    }
    protected void refreshList(object sender, StoreReadDataEventArgs e)
    {
        bindList();
    }
    #endregion

}