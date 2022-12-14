using System;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_RubberQuality_BasicInfo_CheckItem : Mesnac.Web.UI.Page
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
    protected QmtCheckItemTypeManager typeManager = new QmtCheckItemTypeManager();
    //protected QmtCheckStandDetailManager detailManager = new QmtCheckStandDetailManager();
    protected QmtCheckItemManager manager = new QmtCheckItemManager();

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
    #region
    private void bindDeleteFlag()
    {

    }
    private DataSet getList()
    {
        QmtCheckItemParams _params = new QmtCheckItemParams();
        if (tpType.SelectedNodes != null)
        {
            if (tpType.SelectedNodes[0].Path.TrimStart('/').Split('/').Length == 2)
            {
                _params.typeID = this.tpType.SelectedNodes[0].NodeID;
            }
        }
        _params.itemName = this.txtItemName.Text.Trim();

        return manager.GetDataByParas(_params);
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    private void loadtpType()
    {
        this.tpType.GetRootNode().RemoveAll();
        Ext.Net.Node node;
        EntityArrayList<QmtCheckItemType> list = typeManager.GetListByWhereAndOrder(QmtCheckItemType._.DeleteFlag == "0", QmtCheckItemType._.ItemTypeID.Asc);
        foreach (QmtCheckItemType type in list)
        {
            node = new Ext.Net.Node()
            {
                NodeID = type.ItemTypeID,
                Text = type.ItemTypeName,
                Leaf = true
            };
            this.tpType.GetRootNode().AppendChild(node);
        }
        this.tpType.GetRootNode().Expand(true);
    }
    #endregion
    #region 检验项目类型树事件响应
    protected void tpType_ReadData(object sender, NodeLoadEventArgs e)
    {
        loadtpType();
    }
    protected void tpType_Refresh(object sender, EventArgs e)
    {
        loadtpType();
    }
    protected void tp_SelectedChange(object sender, EventArgs e)
    {
        bindList();
    }
    #endregion
    #region 按钮事件响应
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (tpType.SelectedNodes == null)
        {
            X.Msg.Alert("提示", "请先在检验项目类型树形列表中选择一个类型").Show();
            return;
        }
        else if (tpType.SelectedNodes[0].Path.TrimStart('/').Split('/').Length < 2)
        {
            X.Msg.Alert("提示", "请先在检验项目类型树形列表中选择一个类型").Show();
            return;
        }

        this.txtObjID.Text = string.Empty;
        //this.txtCode.Text = string.Empty;
        this.txtName.Text = string.Empty;
        this.hideTypeID.Text = tpType.SelectedNodes[0].NodeID;
        this.txtTypeName.Text = tpType.SelectedNodes[0].Text;
        this.txtxuhao.Text = string.Empty;

        this.txtxuhao.Hidden = false;

        this.winSave.Hidden = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.txtObjID.Text.Equals(string.Empty))
        {
            string sql = "select Item_class from Qmt_Itemclass where Item_classname='" + txtTypeName.Value + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            string type = ds.Tables[0].Rows[0][0].ToString();

            sql = "select MAX(Item_cd)+1 from Qmt_Checkitemcd WHERE Item_class = '" + ds.Tables[0].Rows[0][0].ToString() + "'";
            ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into Qmt_Checkitemcd(Item_cd,Item_name,Item_class,Display_id) values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + txtName.Text + "','" + type + "','"+txtxuhao.Text.Trim()+"')";
            manager.GetBySql(sql).ToDataSet();
            bindList();

            X.Msg.Notify("操作", "插入成功").Show();
        }
        else
        {
            string sql = "update Qmt_Checkitemcd set  Item_name='" + txtName.Text + "' where Item_cd='" + txtObjID.Text + "'";
            manager.GetBySql(sql).ToDataSet();
            bindList();
            X.Msg.Notify("操作", "修改成功").Show();
        }
        winSave.Hidden = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
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
        QmtCheckItem mQmtCheckItem = manager.GetById(Convert.ToInt32(objID));
        if (mQmtCheckItem == null)
        {
            X.Msg.Alert("提示", "此记录没有找到，请重新操作").Show();
            bindList();
            return false;
        }

        System.Text.StringBuilder sbDelLog = new System.Text.StringBuilder();
        sbDelLog.AppendFormat("删除成功");
        sbDelLog.AppendFormat(",ObjID={0},ItemCode={1},ItemName={2},TypeID={3}"
            , new object[] { mQmtCheckItem.ObjID.ToString(), mQmtCheckItem.ItemCode
                , mQmtCheckItem.ItemName, mQmtCheckItem.TypeID });
        sbDelLog.AppendFormat(",ItemEnvir={0},Remark={1},DeleteFlag={2}"
            , new object[] { mQmtCheckItem.ItemEnvir, mQmtCheckItem.Remark
                , mQmtCheckItem.DeleteFlag });

        manager.Delete(mQmtCheckItem);

        AppendWebLog("检验项目-删除", sbDelLog.ToString());
        bindList();

        return true;

    }
    #endregion
    #region GridPanel事件响应
    [DirectMethod]
    public void pnlList_Edit(string commandName, string ObjID)
    {
        QmtCheckItem item = manager.GetById(Convert.ToInt32(ObjID));
        if (item != null)
        {
            this.txtObjID.Text = item.ObjID.ToString();
            //this.txtCode.Text = item.ItemCode;
            this.txtName.Text = item.ItemName;
          
            this.hideTypeID.Value = item.TypeID;
            this.txtTypeName.Text = typeManager.GetListByWhere(QmtCheckItemType._.ItemTypeID == item.TypeID)[0].ItemTypeName;

            this.txtxuhao.Hidden = true;
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