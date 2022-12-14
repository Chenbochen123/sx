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

public partial class Manager_BasicInfo_StorageFlowBaseInfo_StorageFlowBaseInfo : Mesnac.Web.UI.Page
{
    private BasStorageFlowManager manager = new BasStorageFlowManager();//业务对象

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
        }
    }

    #region 分页相关方法
    private PageResult<BasStorageFlow> GetPageResultData(PageResult<BasStorageFlow> pageParams)
    {
        BasStorageFlowManager.QueryParams queryParams = new BasStorageFlowManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.sourceStorage = txtSourceStorage.Text;
        queryParams.targetStorage = txtTargetStorage.Text;
        if (cbxForbiddenFlag.SelectedItem.Value != "all")
            queryParams.forbiddenFlag = cbxForbiddenFlag.SelectedItem.Value;
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasStorageFlow> pageParams = new PageResult<BasStorageFlow>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasStorageFlow> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtSourceStorage2.Text = string.Empty;
        txtTargetStorage2.Text = string.Empty;
        txtLimitTimes2.Text = string.Empty;
        txtRemark2.Text = string.Empty;

        this.winAdd.Show();
    }

    public void btnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            BasStorageFlow store = new BasStorageFlow();
            store.FlowID = Guid.NewGuid().ToString();
            store.SourceStorage = txtSourceStorage2.Text;
            store.TargetStorage = txtTargetStorage2.Text;
            store.LimitTimes = Convert.ToInt32(txtLimitTimes2.Text);
            store.ForbiddenFlag = chkForbiddenFlag2.Checked ? "1" : "0";
            store.Remark = txtRemark2.Text;

            manager.Insert(store);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "库房流程基础信息添加",
                Message = "保存成功！",
                Buttons = MessageBox.Button.OK,
                Width = 300,
                Closable = true,
                AnimEl = this.btnAddSave.ClientID,
            });
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("库房流程基础信息添加", "保存失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        BasStorageFlow store = manager.GetById(Convert.ToInt32(objID));
        txtObjID1.Text = store.ObjID.ToString();
        //txtStorageName1.Text = store.StorageID; //有错误
        txtSourceStorage1.Text = store.SourceStorage;
        txtTargetStorage1.Text = store.TargetStorage;
        txtLimitTimes1.Text = store.LimitTimes.ToString();
        if (store.ForbiddenFlag == "1")
            chkForbiddenFlag1.Checked = true;
        else
            chkForbiddenFlag1.Checked = false;
        txtRemark1.Text = store.Remark;

        this.winModify.Show();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            BasStorageFlow store = manager.GetById(txtObjID1.Text);
            //store.StorageID = txtStorageName1.Text;
            store.SourceStorage = txtSourceStorage1.Text;
            store.TargetStorage = txtTargetStorage1.Text;
            store.LimitTimes = Convert.ToInt32(txtLimitTimes1.Text);
            store.ForbiddenFlag = chkForbiddenFlag1.Checked ? "1" : "0";
            store.Remark = txtRemark1.Text;

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
            BasStorageFlow store = manager.GetById(objID);
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

        if (field.Text != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须填写！";
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