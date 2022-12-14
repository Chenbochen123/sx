using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_RubberQuality_BasicInfo_DealNotion : Mesnac.Web.UI.Page
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
    protected QmtDealNotionManager manager = new QmtDealNotionManager();

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
        QmtDealNotionParams _params = new QmtDealNotionParams();
        _params.dealNotion = this.txtDealNotion.Text.Trim();

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
        this.txtDeal.Text = string.Empty;
    

        this.winSave.Hidden = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text.Equals("Add"))
        {
            string sql = "select MAX(Deal_code)+1 from Qmt_dealnotion";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into Qmt_dealnotion  values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + txtDeal.Text + "','','')";
            manager.GetBySql(sql).ToDataSet();
        
            X.Msg.Alert("提示", "插入成功").Show();
            bindList();

         
        }
        else
        {
            string sql = "update Qmt_dealnotion set  Deal_notion='" + txtDeal.Text + "' where Deal_code='" + txtObjID.Text + "'";
            manager.GetBySql(sql).ToDataSet();

            X.Msg.Alert("提示", "更新成功").Show();
            bindList();

           
        }
        this.winSave.Hidden = true;
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
        QmtDealNotion mQmtDealNotion = manager.GetById(Convert.ToInt32(objID));
        if (mQmtDealNotion == null)
        {
            X.Msg.Alert("提示", "此记录没有找到，请重新操作").Show();
            bindList();
            return false;
        }

        System.Text.StringBuilder sbDelLog = new System.Text.StringBuilder();
        sbDelLog.AppendFormat("删除成功");
        sbDelLog.AppendFormat(",ObjID={0},DealNotion={1},Remark={2},DeleteFlag={3}"
            , new object[] { mQmtDealNotion.ObjID.ToString(), mQmtDealNotion.DealNotion
                , mQmtDealNotion.Remark, mQmtDealNotion.DeleteFlag });

        manager.Delete(mQmtDealNotion);

        AppendWebLog("检验处理方式-删除", sbDelLog.ToString());
        bindList();

        return true;

    }
    #endregion

    #region GridPanel事件响应
    [DirectMethod]
    public void pnlList_Edit(string commandName, string ObjID)
    {
        QmtDealNotion deal = manager.GetById(Convert.ToInt32(ObjID));
        if (deal != null)
        {
            this.hideMode.Text = "Edit";
            this.txtObjID.Text = deal.ObjID.ToString();
            this.txtDeal.Text = deal.DealNotion;
         

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