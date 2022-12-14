using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_BasicInfo_StorageInfo_StorageBaseInfo : Mesnac.Web.UI.Page
{
    private BasStorageManager manager = new BasStorageManager();
    private BasStoragePlaceManager placeManager = new BasStoragePlaceManager();

    private void BindCbxUsingSet(ComboBox cb)
    {
        for (int i = 1; i <= 28; i++)
        {
            cb.Items.Add(new Ext.Net.ListItem(i.ToString(), i.ToString()));
        }
        cb.Items.Insert(0, new Ext.Net.ListItem("", ""));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeStorage();
            //BindCbxUsingSet(chkDurationBeginDate1);
            //BindCbxUsingSet(chkDurationEndDate1);
            //BindCbxUsingSet(chkDurationBeginDate2);
            //BindCbxUsingSet(chkDurationEndDate2);
        }
    }

    #region 查询显示左侧库房列表树
    [DirectMethod]
    public string treePanelStorageLoad(string pageid)
    {
        WhereClip whereStorage = new WhereClip();
        whereStorage.And(BasStorage._.DeleteFlag == "0");
        whereStorage.And(BasStorage._.StorageHigherLevel == pageid);
        //EntityArrayList<BasStorage> storageList = manager.GetListByWhere(whereStorage);
        
        EntityArrayList<BasStorage> storageList = manager.GetAllList();
        NodeCollection nodes = new Ext.Net.NodeCollection();
        if (storageList.Count > 0)
        {
            foreach (BasStorage storage in storageList)
            {
                if (manager.GetListByWhere(BasStorage._.StorageHigherLevel == storage.StorageID && BasStorage._.DeleteFlag == "0").Count > 0)
                {
                    Node node = new Node();
                    node.NodeID = storage.StorageID.ToString();
                    node.Text = storage.StorageName;
                    node.Icon = Icon.Building;
                    node.Leaf = false;
                    nodes.Add(node);
                }
                else
                {
                    Node node = new Node();
                    node.NodeID = storage.StorageID.ToString();
                    node.Text = storage.StorageName;
                    node.Icon = Icon.BuildingGo;
                    node.Leaf = true;
                    nodes.Add(node);
                }
            }
        }
        return nodes.ToJson();
    }
    /// <summary>
    /// 初始化库房列表树
    /// </summary>
    private void InitTreeStorage()
    {
        treeStorage.GetRootNode().RemoveAll();
        WhereClip where = new WhereClip();
        where.And(BasStorage._.DeleteFlag == "0");
        where.And(BasStorage._.StorageLevel == "0");
        //EntityArrayList<BasStorage> lst = manager.GetListByWhere(where);
        EntityArrayList<BasStorage> lst = manager.GetAllList();
        foreach (BasStorage storage in lst)
        {

            Node node = new Node();
            node.NodeID = storage.StorageID.ToString();
            node.Text = storage.StorageName;
            node.Icon = Icon.Building;
            node.Leaf = true;
            treePanel.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasStorage> GetPageResultData(PageResult<BasStorage> pageParams)
    {
        BasStorageManager.QueryParams queryParams = new BasStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        if (ckxStorageType.SelectedItem.Value != "all")
            queryParams.storageType = ckxStorageType.SelectedItem.Value;
        queryParams.storageName = txtStorageName.Text;
        //queryParams.usedFlag = chkUsedFlag.Checked ? "1" : "0";
        if (cbxUsedFlag.SelectedItem.Value != "all")
            queryParams.usedFlag = cbxUsedFlag.SelectedItem.Value;
        queryParams.objID = hiddenHigherLevel.Text == "" ? "" : hiddenHigherLevel.Text;
        //queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //X.Msg.Notify(hiddenHigherLevel.Text, "").Show();

        //DataSet ds = new DataSet();
        //DataTable dt;
        //dt = manager.GetBySql("select * from BasStorage").ToDataSet().Tables[0];

        //ds.Tables.Add(dt.Copy());
        //return dt.Rows[0][0].ToString();

        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasStorage> pageParams = new PageResult<BasStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "CreateDate";
        //this.store.DataSource=null;

        PageResult<BasStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cbxStorageType2.SelectedItem.Value = "1";
        txtStorageName2.Text = string.Empty;
        //txtStorageHigherLevel2.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        hiddenObjID.Text = string.Empty;
        txtERPCode2.Text = string.Empty;
        txtDept2.Text = string.Empty;
        txtRemark2.Text = string.Empty;

        this.winAdd.Show();
    }

    public void btnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            string sql = "select MAX(store_num)+1 from JCZL_store";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            string objid = ds.Tables[0].Rows[0][0].ToString();
            //sql = "insert into JCZL_store values('" + objid + "','" + txtStorageName2.Text + "','" + txtRemark2.Text + "','Y','" + txtDepCode2.Text + "')";
            sql = "insert into JCZL_store values('" + objid + "','" + txtStorageName2.Text + "','" + txtRemark2.Text + "','" + cbxStorageType2.Text + "','" + txtDept2.Text + "','" + txtERPCode2.Text + "')";
            manager.GetBySql(sql).ToDataSet();

            X.Msg.Alert("提示","添加成功").Show();
            InitTreeStorage();
            pageToolBar.DoRefresh();
            this.winAdd.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("提示", "库房信息添加失败：" + ex).Show();
        }


    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        //X.Msg.Notify("", objID).Show();

        string sql = "select * from BasStorage where  ObjID='"+ objID + "'";
        BasStorage store = manager.GetBySql(sql).ToArray<BasStorage>()[0];



        //cbxStorageType3.SelectedItem.Value = store.StorageType;
        txtObjID1.Text = objID;
        txtStorageName1.Text = store.StorageName;
        hiddenStorageName.Text = store.StorageName;
        hiddenStorageID.Text = store.StorageHigherLevel;
        cbxStorageType3.Text = store.StorageType;
        txtDept1.Text = store.WorkShopCode;
        txtRemark1.Text = store.Remark;
        txtERPCode1.Text = store.ERPCode;
        this.winModify.Show();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.StorageName == txtStorageName1.Text && BasStorage._.DeleteFlag == "0");
            if (storageList.Count > 0 && txtStorageName1.Text != hiddenStorageName.Text)
            {
                X.Msg.Alert("提示", "此库房名称已被使用！").Show();
                return;
            }

            string sql = "update JCZL_store set store_name='" + txtStorageName1.Text + "',Remark='" + txtRemark1.Text + "',store_kind ='" + cbxStorageType3.Text + "',dep_num='" + txtDept1.Text + "',SAP_Code ='" + txtERPCode1.Text + "' where store_num='" + txtObjID1.Text + "'";
            manager.GetBySql(sql).ToDataSet();

            pageToolBar.DoRefresh();
            InitTreeStorage();
            this.winModify.Close();
            X.MessageBox.Alert("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchUsing_Click()
    {
        //StringBuilder sb = new StringBuilder();

        //foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        //{
        //    sb.AppendFormat("您选择的ID是: {0}&nbsp;&nbsp;&nbsp;&nbsp;行号: {1}<br/>", row.RecordID, row.RowIndex);
        //}

        //X.MessageBox.Alert("已选择项", sb.ToString()).Show();


        //if (this.rowSelectMuti.SelectedRows.Count <= 0)
        //{
        //    X.MessageBox.Alert("提示", "您没有选择任何项，请选择！").Show();
        //    return;
        //}

        string IDS = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            IDS += "'" + row.RecordID + "', ";
            EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.ObjID == row.RecordID && BasStorage._.UsedFlag == "1");
            if (storageList.Count > 0)
            {
                return "此库房已被启用!";
            }
        }        

        bool result = manager.UpdateUsing(IDS.Remove(IDS.Length - 2, 1));
        bool result1 = placeManager.UpdateUsingByStorageID(IDS.Remove(IDS.Length - 2, 1));

        if (result == true && result1 == true)
        {
            pageToolBar.DoRefresh();
            return "启用成功！";
        }
        else
        {
            return "启用失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID, string storageID)
    {
        try
        {
 
            string sql = "delete JCZL_store where store_num='"+ objID + "'";
            manager.GetBySql(sql).ToDataSet();
            pageToolBar.DoRefresh();
            InitTreeStorage();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_cancel(string objID)
    {
        try
        {
            BasStorage store = manager.GetById(objID);
            EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.StorageHigherLevel == store.StorageID && BasStorage._.DeleteFlag == "0");
            if (storageList.Count > 0)
            {
                return "作废失败：该库房存在下级库房，不能作废";
            }

            store.CancelFlag = "1";

            manager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "操作失败：" + e;
        }
        return "已作废";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_returncancel(string objID)
    {
        try
        {
            BasStorage store = manager.GetById(objID);
            store.CancelFlag = "0";

            manager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "操作失败：" + e;
        }
        return "已取消作废";
        //return "<font color='red'>已取消<br />作废</font>";
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    public void chkNatureMonth1_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkNatureMonth1.Checked)
        //{
        //    chkDurationBeginDate1.Disabled = true;
        //    chkDurationEndDate1.Disabled = true;
        //}
        //else
        //{
        //    chkDurationBeginDate1.Disabled = false;
        //    chkDurationEndDate1.Disabled = false;
        //}
    }

    public void chkNatureMonth2_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkNatureMonth2.Checked)
        //{
        //    chkDurationBeginDate2.Disabled = true;
        //    chkDurationEndDate2.Disabled = true;
        //}
        //else
        //{
        //    chkDurationBeginDate2.Disabled = false;
        //    chkDurationEndDate2.Disabled = false;
        //}
    }

    public void chkDurationBeginDate1_Change(object sender, EventArgs e)
    {
        //chkDurationEndDate1.Text = Convert.ToString(Convert.ToInt32(chkDurationBeginDate1.SelectedItem.Value) - 1);
        //if (chkDurationEndDate1.Text == "-1")
        //    chkDurationEndDate1.Text = "";
        //if (chkDurationEndDate1.Text == "0")
        //    chkDurationEndDate1.Text = "月底";
    }

    public void chkDurationBeginDate2_Change(object sender, EventArgs e)
    {
        //chkDurationEndDate2.Text = Convert.ToString(Convert.ToInt32(chkDurationBeginDate2.SelectedItem.Value) - 1);
        //if (chkDurationEndDate2.Text == "-1")
        //    chkDurationEndDate2.Text = "";
        //if (chkDurationEndDate2.Text == "0")
        //    chkDurationEndDate2.Text = "月底";
    }
    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string storageName = field.Text;
        EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.StorageName == storageName && BasStorage._.DeleteFlag == "0");
        if (storageList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (storageList[0].StorageName.ToString() == hiddenStorageName.Text)
            {
                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此库房名称已被使用！";
            }
        }
    }

    protected void CheckCombo(object sender, RemoteValidationEventArgs e)
    {
        ComboBox combo = (ComboBox)sender;

        if (combo.SelectedItem.Value != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须选择！";
        }
    }
    #endregion
}