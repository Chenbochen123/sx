using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_RubberQuality_BasicInfo_CheckStandType : Mesnac.Web.UI.Page
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
    //protected QmtCheckStandManager standManager = new QmtCheckStandManager();
    protected QmtCheckStandTypeManager manager = new QmtCheckStandTypeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindDeleteFlag();
            bindWorkShop();
            bindCheckType();
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
        QmtCheckStandTypeParams _params = new QmtCheckStandTypeParams();
        _params.standTypeName = this.txtStandTypeName.Text.Trim();

        return manager.GetDataByParas(_params);
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    /// <summary>
    /// 创建标识：qusf 20131105
    /// 创建说明：绑定车间
    /// </summary>
    private void bindWorkShop()
    {
     
    }
    /// <summary>
    /// 创建标识：qusf 20131105
    /// 创建说明：绑定使用用途
    /// </summary>
    private void bindCheckType()
    {
       
    }

    #endregion

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    /// <summary>
    /// 修改标识：qusf 20131105
    /// 修改说明：1.增加车间、使用用途的处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        this.txtObjID.Text = string.Empty;
        this.txtTypeName.Text = string.Empty;

    

        this.winSave.Hidden = false;
    }
    /// <summary>
    /// 修改标识：qusf 20131105
    /// 修改说明：1.增加车间、使用用途的处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text.Equals("Add"))
        {
            string sql = "select MAX(Stand_code)+1 from Qmt_QuaStand";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into Qmt_QuaStand values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + txtTypeName.Text + "')";
            manager.GetBySql(sql).ToDataSet();

            X.Msg.Alert("操作", "添加成功").Show();
        }
        else
        {
            string sql = "update Qmt_QuaStand set Stand_name='" + txtTypeName.Text + "' where Stand_code='" + txtObjID.Text + "'";
            manager.GetBySql(sql).ToDataSet();

            X.Msg.Alert("操作", "修改成功").Show();
        }
        bindList();
        this.winSave.Hidden = true;
     }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winSave.Hidden = true;
    }

    /// <summary>
    /// 创建标识：qusf 20140107
    /// 创建说明：删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [DirectMethod]
    public bool btnDelete_Click(string objID)
    {
        QmtCheckStandType mQmtCheckStandType = manager.GetById(Convert.ToInt32(objID));
        if (mQmtCheckStandType == null)
        {
            X.Msg.Alert("提示", "此记录没有找到，请重新操作").Show();
            bindList();
            return false;
        }
        StringBuilder sbDelLog = new StringBuilder();
        sbDelLog.AppendFormat("删除成功");
        sbDelLog.AppendFormat(",ObjID={0},CheckTypeCode={1},mQmtCheckStandType={2}"
            , new object[] { mQmtCheckStandType.ObjID.ToString(), mQmtCheckStandType.CheckTypeCode, mQmtCheckStandType.CheckTypeName });
        sbDelLog.AppendFormat(",StandTypeName={0},WorkShopId={1},DeleteFlag={2}"
            , new object[] { mQmtCheckStandType.StandTypeName, mQmtCheckStandType.WorkShopId.ToString(), mQmtCheckStandType.DeleteFlag });

        manager.Delete(mQmtCheckStandType);

        AppendWebLog("检验标准类型-删除", sbDelLog.ToString());
        bindList();

        return true;
        
    }
    #endregion

    #region GridPanel事件响应
    /// <summary>
    /// 修改标识：qusf 20131105
    /// 修改说明：1.增加车间、使用用途的处理
    /// </summary>
    /// <param name="commandName"></param>
    /// <param name="ObjID"></param>
    [DirectMethod]
    public void pnlList_Edit(string commandName, string ObjID)
    {
        QmtCheckStandType type = manager.GetById(Convert.ToInt32(ObjID));
        if (type != null)
        {
            this.hideMode.Text = "Edit";
            this.txtObjID.Text = type.ObjID.ToString();
            this.txtTypeName.Text = type.StandTypeName;
         

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