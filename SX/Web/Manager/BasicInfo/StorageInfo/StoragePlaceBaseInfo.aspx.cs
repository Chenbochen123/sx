using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Text;

public partial class Manager_BasicInfo_StoragePlaceBaseInfo_StoragePlaceBaseInfo : Mesnac.Web.UI.Page
{
    private BasStoragePlaceManager manager = new BasStoragePlaceManager();
    private BasStorageManager storageManager = new BasStorageManager();
    private BasStoragePlacePropManager storagePlacePropManager = new BasStoragePlacePropManager();
    private BasStoragePlaceSubManager storagePlaceSubManager = new BasStoragePlaceSubManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeStorage();
        }
    }

    #region 查询显示左侧库房列表树
    [DirectMethod]
    public string treePanelStorageLoad(string pageid)
    {
        WhereClip whereStorage = new WhereClip();
        whereStorage.And(BasStorage._.DeleteFlag == "0");
        whereStorage.And(BasStorage._.StorageHigherLevel == pageid);
        EntityArrayList<BasStorage> storageList = storageManager.GetListByWhere(whereStorage);
        NodeCollection nodes = new Ext.Net.NodeCollection();
        if (storageList.Count > 0)
        {
            foreach (BasStorage storage in storageList)
            {
                if (storageManager.GetListByWhere(BasStorage._.StorageHigherLevel == storage.StorageID && BasStorage._.DeleteFlag == "0").Count > 0)
                {
                    Node node = new Node();
                    node.NodeID = storage.StorageID;
                    node.Text = storage.StorageName;
                    node.Icon = Icon.Building;
                    node.Leaf = false;
                    nodes.Add(node);
                }
                else
                {
                    Node node = new Node();
                    node.NodeID = storage.StorageID;
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
        EntityArrayList<BasStorage> lst = storageManager.GetListByWhere(where);
        foreach (BasStorage storage in lst)
        {

            Node node = new Node();
            node.NodeID = storage.StorageID;
            node.Text = storage.StorageName;
            node.Icon = Icon.Building;
            treePanel.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    [DirectMethod]
    public string SetSelectStorageID(string storageID)
    {
        hiddenNodeID.Text = storageID;
        string storageStr = string.Empty;
        DataSet ds = storageManager.GetStorageStr(storageID);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            storageStr += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
        }
        if (!string.IsNullOrEmpty(storageStr))
        {
            hiddenSelectStorageID.Text = storageStr.Substring(0, storageStr.Length - 1);
        }
        else
        {
            hiddenSelectStorageID.Text = string.Empty;
        }

        pageToolBar.DoRefresh();
        return "OK";
    }

    #region 分页相关方法
    private PageResult<BasStoragePlace> GetPageResultData(PageResult<BasStoragePlace> pageParams)
    {
        BasStoragePlaceManager.QueryParams queryParams = new BasStoragePlaceManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageID = hiddenSelectStorageID.Text;
        queryParams.storagePlaceName = txtStoragePlaceName.Text;
        if (cbxStorageType.SelectedItem.Value != "all")
            queryParams.storageType = cbxStorageType.SelectedItem.Value;
        //if (cbxDefaultFlag.SelectedItem.Value != "all")
        //    queryParams.defaultFlag = cbxDefaultFlag.SelectedItem.Value;
        queryParams.deleteFlag = "0";
        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasStoragePlace> GetTablePageDataBySql(BasStoragePlaceManager.QueryParams queryParams)
    {
        PageResult<BasStoragePlace> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT A.ObjID, A.StorageID,case ShiYanFlag when '1' then '是' else '否' end as ShiYanFlag, B.StorageName, a.StoragePlaceID, StoragePlaceName, AutoGenFlag, CONVERT(BIT, DefaultFlag) DefaultFlag, CONVERT(BIT, A.LockFlag) LockFlag,
A.CancelFlag, A.Remark,c.MaterialName,c.StorageCapacity, case(specialplace) when '1' then '是' else '否' end as SpecialPlace,
case(isfull) when '1' then '是' else '否' end as isfull
                                FROM BasStoragePlace A
                                LEFT JOIN BasStorage B ON A.StorageID = B.StorageID
left join BasStoragePlaceProp c on A.StoragePlaceid = c.StoragePlaceid WHERE 1 = 1");
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND B.StorageID in (" + queryParams.storageID + ")");
        }
        if (!string.IsNullOrEmpty(queryParams.storagePlaceName))
        {
            sqlstr.AppendLine(" AND StoragePlaceName LIKE '%" + queryParams.storagePlaceName + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.defaultFlag))
        {
            sqlstr.AppendLine(" AND DefaultFlag = '" + queryParams.defaultFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.storageType))
        {
            sqlstr.AppendLine(" AND B.StorageType = '" + queryParams.storageType + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.lockFlag))
        {
            sqlstr.AppendLine(" AND A.LockFlag = '" + queryParams.lockFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.cancelFlag))
        {
            sqlstr.AppendLine(" AND A.CancelFlag = '" + queryParams.cancelFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
        }
        if (!string.IsNullOrEmpty(TextField1.Text))
        {
            sqlstr.AppendLine(" AND c.materialname LIKE '%" + TextField1.Text + "%'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasStoragePlace> pageParams = new PageResult<BasStoragePlace>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID";

        PageResult<BasStoragePlace> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    [DirectMethod]
    public object GridPanelBindData1(string action, Dictionary<string, object> extraParams)
    {
        txtRemark1.Text = "a";
        if (TextField2.Text.Length == 0) return null;

        String sql = "select * from basmaterial where materialcode in (" + TextField2.Text.Substring(0, TextField2.Text.Length-1) + ")";
        //X.Js.Alert(sql);
        //return null;
        txtRemark1.Text = "b";
        
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        DataTable data = ds.Tables[0];

        int total = data.Rows.Count;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtStorageName2.Text = string.Empty;
        txtStoragePlaceName2.Text = string.Empty;
        txtRemark2.Text = string.Empty;
        var storageId = hiddenNodeID.Text;

        DataSet ds = storageManager.GetDataSetByFieldsAndWhere("StorageName", "WHERE StorageID = '" + storageId + "'");
        if (ds.Tables[0].Rows.Count > 0)
            txtStorageName2.Text = storageManager.GetDataSetByFieldsAndWhere("StorageName", "WHERE StorageID = '" + storageId + "'").Tables[0].Rows[0][0].ToString();
        else
            txtStorageName2.Text = string.Empty;

        this.winAdd.Show();
    }

    public DataSet GetStoragePlaceID(string StorageID)
    {
        DataSet ds = manager.GetBySql("select ObjID from BasStoragePlace where StorageID = '" + StorageID + "'").ToDataSet();
        if (ds.Tables[0].Rows.Count > 0)
            return manager.GetBySql("select StorageID + RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StoragePlaceID, 3)) + 1), 3) from BasStoragePlace where ObjID = (select max(ObjID) from BasStoragePlace where StorageID = '" + StorageID + "')").ToDataSet();
        else
            return manager.GetBySql("SELECT '" + StorageID + "' + '001'").ToDataSet();
    }
    public void btnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            EntityArrayList<BasStorage> storageList =storageManager.GetListByWhere(BasStorage._.StorageName == txtStorageName2.Text);
            var storageId = storageList[0].StorageID;
            EntityArrayList<BasStoragePlace> storagePlaceList = manager.GetListByWhere(BasStoragePlace._.StorageID == storageId && BasStoragePlace._.StoragePlaceName == txtStoragePlaceName2.Text && BasStoragePlace._.DeleteFlag == "0");
            if (storagePlaceList.Count > 0)
            {
                X.Msg.Alert("提示", "此库位名称已被使用！").Show();
                return;
            }
            BasStoragePlace store = new BasStoragePlace();
            DataSet ds = GetStoragePlaceID(storageId);
            //int objID=((int)manager.GetMaxValueByProperty(BasStoragePlace._.ObjID) + 1);
            //store.ObjID = objID;
            store.StorageID = storageId;
            store.StoragePlaceID = ds.Tables[0].Rows[0][0].ToString();
            store.StoragePlaceName = txtStoragePlaceName2.Text;
            store.DeleteFlag = "0";
            store.LockFlag = "0";
            store.CancelFlag = "0";
            store.Remark = txtRemark2.Text;

            manager.Insert(store);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "库位基础信息添加",
                Message = "保存成功！",
                Buttons = MessageBox.Button.OK,
                Width = 300,
                Closable = true,
                AnimEl = this.btnAddSave.ClientID,
            });
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("库位基础信息添加", "保存失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        BasStoragePlace store = manager.GetById(Convert.ToInt32(objID));
        txtObjID1.Text = store.ObjID.ToString();
        hiddenStorageID.Text = store.StorageID;

        if (store.ShiYanFlag == "1") chkSY.Checked = true;
        else
            chkSY.Checked = false;



        DataSet ds = storageManager.GetDataSetByFieldsAndWhere("StorageName", "WHERE StorageID = '" + store.StorageID + "'");
        if (ds.Tables[0].Rows.Count > 0)
            txtStorageName1.Text = storageManager.GetDataSetByFieldsAndWhere("StorageName", "WHERE StorageID = '" + store.StorageID + "'").Tables[0].Rows[0][0].ToString();
        else
            txtStorageName1.Text = string.Empty;
        txtStoragePlaceName1.Text = store.StoragePlaceName;
        hiddenStoragePlaceName.Text = store.StoragePlaceName;
        txtRemark1.Text = store.Remark;
        ds = storagePlacePropManager.GetDataSetByFieldsAndWhere("ObjID", "WHERE StoragePlaceID = '" + store.StoragePlaceID + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {BasStoragePlaceProp bp = storagePlacePropManager.GetById(ds.Tables[0].Rows[0][0].ToString());

        TextArea1.Text = bp.MaterialName;
        TextField2.Text = bp.MaterialCode;
        txtStoragePlacaSubAmount.Text = bp.StorageCapacity.ToString();
        if (bp.SpecialPlace == "1") chkModifySpecialPlaceFlag.Checked = true;
            else
        chkModifySpecialPlaceFlag.Checked = false;
        }


        //PagingToolbar1.DoRefresh();
        this.winModify.Show();
    }
    [Ext.Net.DirectMethod()]
    public void AddMaterial(string MaterialID, string MaterialName)
    {
        String s = MaterialID + ",";
        String sn= MaterialName + "\n";
        if (TextField2.Text.IndexOf(s) >= 0)
        { TextField2.Text = TextField2.Text.Replace(s, "");
        if (TextArea1.Text.LastIndexOf(sn) == 0)
        { TextArea1.Text = "\n"+TextArea1.Text;
        TextArea1.Text = TextArea1.Text.Replace("\n" + sn , "");
        }
        else
        {
            TextArea1.Text = TextArea1.Text.Replace("\n" + sn , "\n");
        }
        }
        else
        { TextField2.Text = TextField2.Text + s;
      
        TextArea1.Text = TextArea1.Text + sn;
        }
    }


    [Ext.Net.DirectMethod()]
    public void ClearMaterial(object sender, EventArgs e)
    {
        TextField2.Text = "";
        TextArea1.Text = "";
    
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            EntityArrayList<BasStoragePlace> storageList = manager.GetListByWhere(BasStoragePlace._.StorageID == hiddenStorageID.Text && BasStoragePlace._.StoragePlaceName == txtStoragePlaceName1.Text && BasStoragePlace._.DeleteFlag == "0");
            if (storageList.Count > 0 && txtStoragePlaceName1.Text != hiddenStoragePlaceName.Text)
            {
                X.Msg.Alert("提示", "此库位名称已被使用！").Show();
                return;
            }

            BasStoragePlace store = manager.GetById(txtObjID1.Text);
            var storagePlace = store.StoragePlaceID;
            int storageCapacity = 0;
            if (string.IsNullOrWhiteSpace(txtStoragePlacaSubAmount.Text))
            {
                storageCapacity = 0;
            }
            else
            {
                storageCapacity = Convert.ToInt32(txtStoragePlacaSubAmount.Text);
            }
            
            store.StorageID = hiddenStorageID.Text;
            store.StoragePlaceName = txtStoragePlaceName1.Text;
            //store.DefaultFlag = chkDefaultFlag1.Checked ? "1" : "0";
            store.ShiYanFlag = chkSY.Checked ? "1" : "0";
            store.Remark = txtRemark1.Text;
            var check=chkModifySpecialPlaceFlag.Checked ? "1" : "0";
            EntityArrayList<BasStoragePlaceProp> storagePlacePropList = storagePlacePropManager.GetListByWhere(BasStoragePlaceProp._.StoragePlaceID == storagePlace);
            //通过库位ID找到对应的BasStoragePlaceProp表只进行更新，若没有找到需要增加一条新纪录
            if (storagePlacePropList.Count > 0)
            {
                storagePlacePropManager.Update(new PropertyItem[] { BasStoragePlaceProp._.StorageCapacity, BasStoragePlaceProp._.StoragePlaceID, BasStoragePlaceProp._.MaterialCode, BasStoragePlaceProp._.MaterialName, BasStoragePlaceProp._.SpecialPlace },
                    new object[] { storageCapacity, storagePlace, TextField2.Text, TextArea1.Text, check }, BasStoragePlaceProp._.StoragePlaceID == storagePlace);
            }
            else
            {
                BasStoragePlaceProp storagePlaceProp = new BasStoragePlaceProp();
                storagePlaceProp.StorageCapacity = storageCapacity;
                storagePlaceProp.StoragePlaceID = storagePlace;
                storagePlaceProp.MaterialCode = TextField2.Text;
                storagePlaceProp.MaterialName = TextArea1.Text;
                storagePlaceProp.SpecialPlace = chkModifySpecialPlaceFlag.Checked ? "1":"0";
                storagePlaceProp.StorageNumber = 0;
                storagePlacePropManager.Insert(storagePlaceProp);
            }
            //所有
            EntityArrayList<BasStoragePlaceSub> storagePlaceSubList = storagePlaceSubManager.GetListByWhere(BasStoragePlaceSub._.StoragePlaceID == storagePlace);
            //未作废
            EntityArrayList<BasStoragePlaceSub> storagePlaceSubList2 = storagePlaceSubManager.GetListByWhere(BasStoragePlaceSub._.StoragePlaceID == storagePlace && BasStoragePlaceSub._.CancelFlag == 0);
            //已作废
            EntityArrayList<BasStoragePlaceSub> storagePlaceSubList3 = storagePlaceSubManager.GetListByWhere(BasStoragePlaceSub._.StoragePlaceID == storagePlace && BasStoragePlaceSub._.CancelFlag == 1);
            
            //如果数据库中未作废的子库位的数量小于设置的大小进行添加新的子库位
            //开始比较所有，如果仍然小于则先进行解除作废，然后添加新库位。
            if (storagePlaceSubList2.Count < storageCapacity)
            {
                if (storagePlaceSubList.Count < storageCapacity)
                {
                    int needCreateAmount = storageCapacity - storagePlaceSubList.Count;
                    //如果仍然小于则先进行解除作废
                    for (int b = 1; b <=storagePlaceSubList3.Count;b++ )
                    {
                        storagePlaceSubList3[b - 1].CancelFlag = "0";
                        storagePlaceSubManager.Update(storagePlaceSubList3[b-1]);
                    }

                    var count = storagePlaceSubList.Count;
                    int a = 1;
                    if (count > 0)
                    a = count+1;
                    //添加新库位
                    for (; a <= count+needCreateAmount; a++)
                    {
                        BasStoragePlaceSub storagePlaceSub = new BasStoragePlaceSub();
                        storagePlaceSub.StoragePlaceID = storagePlace;
                        
                        
                        storagePlaceSub.StoragePlaceSubID = storagePlace + a.ToString().PadLeft(3, '0');
                        storagePlaceSub.StoragePlaceSubName = a.ToString().PadLeft(3, '0');
                        
                        storagePlaceSub.CancelFlag = "0";
                        storagePlaceSubManager.Update(storagePlaceSub);
                    }
                }
                else if (storagePlaceSubList.Count > storageCapacity)
                {
                    int needCancelAmount = storagePlaceSubList.Count - storageCapacity;
                    for (int c =1 ; c <= needCancelAmount; c++)
                    {
                        BasStoragePlaceSub storagePlaceSub = storagePlaceSubList[c];
                        storagePlaceSub.CancelFlag = "0";
                        storagePlaceSubManager.Update(storagePlaceSubList3[c]);
                    }
                }
            }
            //如果未作废的数量多于需求的则只需解除部分作废的即可,并且作废从后边的库位开始
            else if (storagePlaceSubList2.Count > storageCapacity)
            {
                //int needCancelAmount = storagePlaceSubList2.Count - storageCapacity;
                for (int a = storagePlaceSubList.Count; a > storageCapacity; a--)
                {
                    BasStoragePlaceSub storagePlaceSub=storagePlaceSubList[a-1];
                    storagePlaceSub.CancelFlag = "1";
                    storagePlaceSubManager.Update(storagePlaceSub);
                }
            }
            //取消默认库位，设置自动生成的库位为默认库位
            //if (chkDefaultFlag1.Checked)
            //    manager.SetDefaultStoragePlace(txtObjID1.Text, hiddenStorageID.Text);
            //else
            //    manager.SetAutoGenDefault(txtObjID1.Text, hiddenStorageID.Text);            
            manager.Update(store);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            X.MessageBox.Alert("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            BasStoragePlace store = manager.GetById(objID);
            store.DeleteFlag = "1";

            manager.Update(store);
            pageToolBar.DoRefresh();
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
            BasStoragePlace store = manager.GetById(objID);

            store.CancelFlag = "1";
            //作废的同时关闭启用 by dingby 2016-08-10 
            store.LockFlag = "0";
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
            BasStoragePlace store = manager.GetById(objID);
            store.CancelFlag = "0";

            manager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "操作失败：" + e;
        }
        return "已取消作废";
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchUsing_Click()
    {
        string IDS = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            IDS += "'" + row.RecordID + "', ";
            EntityArrayList<BasStoragePlace> storageList = manager.GetListByWhere(BasStoragePlace._.ObjID == row.RecordID && BasStoragePlace._.LockFlag == "1");
            if (storageList.Count > 0)
            {
                return "此库位已被启用!";
            }
            
        }

        bool result = manager.UpdateLocked(IDS.Remove(IDS.Length - 2, 1));
        if (result == true)
        {
            pageToolBar.DoRefresh();
            return "启用成功！";
        }
        else
        {
            return "启用失败！";
        }
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string storageID = hiddenStorageID.Text;
        string storagePlaceName = field.Text;
        EntityArrayList<BasStoragePlace> storagePlaceList = manager.GetListByWhere(BasStoragePlace._.StorageID == storageID && BasStoragePlace._.StoragePlaceName == storagePlaceName && BasStoragePlace._.DeleteFlag == "0");
        if (storagePlaceList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (storagePlaceList[0].StoragePlaceName.ToString() == hiddenStoragePlaceName.Text)
            {
                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此库房名称已被使用！";
            }
        }
        //TextField field = (TextField)sender;

        //if (field.Text != "")
        //{
        //    e.Success = true;
        //}
        //else
        //{
        //    e.Success = false;
        //    e.ErrorMessage = "此属性必须填写！";
        //}
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

    protected void CheckFieldAmount(object sender, RemoteValidationEventArgs e)
    {
        int? amount;
        string msg = "";
        try
        {
            if (string.IsNullOrWhiteSpace(txtStoragePlacaSubAmount.Text))
            {
                amount = null;
            }
            else
            {
                amount = Convert.ToInt32(txtStoragePlacaSubAmount.Text);
            }
            if (amount <= 0 && amount != null)
            {
                msg = "子库位数量必须为整数";
                X.Msg.Alert("错误", msg);
                X.Msg.Show();
                e.Success = false;
                return;
            }
            e.Success = true;            
        }
        catch
        {
            msg = "子库位数量必须为整数";
            //X.Msg.Alert("错误", msg);
            //X.Msg.Show();
            throw new Exception(msg);
        }

       
    }
    #endregion
}