using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using NBear;
using NBear.Common;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;
using Mesnac.Data.Components;

public partial class Manager_RawMaterialQuality_LedgerItem : BasePage
{
    #region 属性注入
    protected IQmcLedgerKeyManager ledgerKeyManager = new QmcLedgerKeyManager();
    protected IQmcLedgerKeyValueManager ledgerKeyValueManager = new QmcLedgerKeyValueManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新增条目 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            新增条目值选项 = new SysPageAction() { ActionID = 3, ActionName = "btnAddValue" };
            导出 = new SysPageAction() { ActionID = 4, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新增条目 { get; private set; } //必须为 public
        public SysPageAction 新增条目值选项 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.btnAddValue.Disable(true);
        }

    }
    #endregion 

    #region 分页相关方法
    /// <summary>
    /// 根据筛选条件获取分页数据
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<QmcLedgerKey> GetPageResultData(PageResult<QmcLedgerKey> pageParams)
    {
        QmcLedgerKeyManager.QueryParams queryParams = new QmcLedgerKeyManager.QueryParams();
        queryParams.pageParams = pageParams;
        if (cbxValueType.SelectedItem.Value != "all")
            queryParams.valueType = cbxValueType.SelectedItem.Value;
        if (cbxCanDropDown.SelectedItem.Value != "all")
            queryParams.hasSelection = cbxCanDropDown.SelectedItem.Value;
        queryParams.deleteFlag = "0";
        return ledgerKeyManager.GetTablePageDataBySql(queryParams);
    }

    /// <summary>
    /// GridPanel数据绑定
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    { 
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<QmcLedgerKey> pageParams = new PageResult<QmcLedgerKey>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "KeyId ASC";

        PageResult<QmcLedgerKey> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    /// <summary>
    /// 行选择事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        //加载所选项目的可选值
        string keyId = e.Parameters["KeyId"];
        this.txtHiddenKeyId.Value = keyId;
        SetBtnAddValueStatus();
        this.storeValue.DataSource = ledgerKeyValueManager.GetListByWhere((QmcLedgerKeyValue._.KeyId == keyId)&&(QmcLedgerKeyValue._.DeleteFlag == "0"));
        this.storeValue.DataBind();
    }

    /// <summary>
    /// 设置按钮的状态
    /// </summary>
    protected void SetBtnAddValueStatus()
    {
        QmcLedgerKey key = ledgerKeyManager.GetById(Convert.ToInt32(txtHiddenKeyId.Value));
        if (key.HasSelection != "1")
        {
            this.btnAddValue.Disable(true);
            this.txtAddValueKeyName.Value = "";
            this.txtAddValueKeyValueType.Value = "";
        }
        else
        {
            this.btnAddValue.Enable(true);
            this.txtAddValueKeyName.Value = key.KeyName;
            this.txtAddValueKeyValueType.Value = key.ValueType;
        }
    }
    #endregion

    #region 增删改查按钮激发的事件

    /// <summary>
    /// 点击添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //初始化新增窗口
        txtAddKeyId.Value = "";
        txtAddKeyName.Value = "";
        txtHiddenKeyName.Value = "";
        txtHiddenKeyCode.Value = "";
        txtAddKeyCode.Value = "";
        txtAddKeyRemark.Value = "";
        cbxAddCanDropDown.Value = "0";
        cbxAddValueType.Value = "文字";
        btnAddKeySave.Disable(true);
        this.windowAddKey.Show();
    }

    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<QmcLedgerKey> pageParams = new PageResult<QmcLedgerKey>();
        pageParams.PageSize = -100;
        PageResult<QmcLedgerKey> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlItem.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()) && (cb.Visible == true))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        if (lst.DataSet.Tables[0].Rows.Count > 0)
        {
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "台账项目报表");
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击添加值按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_adddetail_Click(object sender, EventArgs e)
    {
        //初始化新增值窗口
        txtAddValueId.Value = "";
        txtAddValueContent.Value = "";
        txtAddValueRemark.Value = "";
        txtHiddenKeyValue.Value = "";
        btnAddValueSave.Disable(true);
        this.windowAddValue.Show();
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string keyId)
    {
        //初始化修改窗口
        QmcLedgerKey key = ledgerKeyManager.GetById(Convert.ToInt32(keyId));
        txtModifyKeyId.Value = key.KeyId;
        txtModifyKeyName.Value = key.KeyName;
        txtHiddenKeyName.Value = key.KeyName;
        txtHiddenKeyCode.Value = key.KeyCode;
        txtModifyKeyCode.Value = key.KeyCode;
        txtModifyKeyRemark.Value = key.Remark;
        cbxModifyValueType.Value = key.ValueType;
        cbxModifyCanDropDown.Value = key.HasSelection;
        this.windowModifyKey.Show();
    }

    /// <summary>
    /// 点击修改值激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string valueId)
    {
        //初始化修改值窗口
        QmcLedgerKeyValue value = ledgerKeyValueManager.GetById(Convert.ToInt32(valueId));
        QmcLedgerKey key = ledgerKeyManager.GetById(Convert.ToInt32(value.KeyId));
        txtModifyValueId.Value = value.ValueId;
        txtModifyValueContent.Value = value.KeyValue;
        txtHiddenKeyValue.Value = value.KeyValue;
        txtModifyValueRemark.Value = value.Remark;
        txtModifyValueKeyName.Value = key.KeyName;
        txtModifyValueKeyValueType.Value = key.ValueType;
        this.windowModifyValue.Show();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string keyId)
    {
        try
        {
            QmcLedgerKey key = ledgerKeyManager.GetById(Convert.ToInt32(keyId));
            key.DeleteFlag = "1";
            ledgerKeyManager.Update(key);
            this.AppendWebLog("电子台账项目删除", "项目编号：" + keyId);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击删除值触发的事件
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumndetail_direct_delete(string valueId)
    {
        try
        {
            QmcLedgerKeyValue value = ledgerKeyValueManager.GetById(Convert.ToInt32(valueId));
            value.DeleteFlag = "1";
            ledgerKeyValueManager.Update(value);
            this.AppendWebLog("电子台账项目值删除", "值编号：" + valueId);
            pageToolBar2.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.windowModifyKey.Close();
        this.windowModifyValue.Close();
        this.windowAddKey.Close();
        this.windowAddValue.Close();
    }

    /// <summary>
    /// 点击添加台账项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddKeySave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcLedgerKey> keyList = ledgerKeyManager.GetListByWhere(((QmcLedgerKey._.KeyName == txtAddKeyName.Text.TrimStart().TrimEnd()) || (QmcLedgerKey._.KeyCode == txtAddKeyCode.Text.TrimStart().TrimEnd())) && (QmcLedgerKey._.DeleteFlag == "0") && ((QmcLedgerKey._.KeyCode != null) && (QmcLedgerKey._.KeyCode != "")));
            if (keyList.Count > 0)
            {
                X.Msg.Alert("提示", "此项目名称或英文名称已被使用！").Show();
                return;
            }
            QmcLedgerKey key = new QmcLedgerKey();
            key.KeyId = Convert.ToInt32(ledgerKeyManager.GetNextKeyId());
            key.KeyName = (string)(txtAddKeyName.Text);
            key.KeyCode = (string)(txtAddKeyCode.Text);
            key.Remark = (string)(txtAddKeyRemark.Text);
            key.ValueType = (string)(cbxAddValueType.Value);
            key.HasSelection = (string)(cbxAddCanDropDown.Value);
            key.DeleteFlag = "0";
            ledgerKeyManager.Insert(key);
            this.AppendWebLog("电子台账项目添加", "台账编号：" + key.KeyId);
            pageToolBar.DoRefresh();
            this.windowAddKey.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改台账项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyKeySave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            EntityArrayList<QmcLedgerKey> keyList = ledgerKeyManager.GetListByWhere(((QmcLedgerKey._.KeyName == txtModifyKeyName.Text.TrimStart().TrimEnd()) || (QmcLedgerKey._.KeyCode == txtModifyKeyCode.Text.TrimStart().TrimEnd())) && (QmcLedgerKey._.DeleteFlag == "0") && ((QmcLedgerKey._.KeyCode != null) && (QmcLedgerKey._.KeyCode != "")));
            if (keyList.Count > 0)
            {
                if (keyList[0].KeyName != txtHiddenKeyName.Text)
                {
                    X.Msg.Alert("提示", "此项目名称已被使用！").Show();
                    return;
                }
                if (keyList[0].KeyCode != txtHiddenKeyCode.Text)
                {
                    X.Msg.Alert("提示", "此英文名称已被使用！").Show();
                    return;
                }
            }
            QmcLedgerKey key = new QmcLedgerKey();
            key.KeyId = Convert.ToInt32(txtModifyKeyId.Text);
            key.Attach();
            key.KeyName = (string)(txtModifyKeyName.Text);
            key.KeyCode = (string)(txtModifyKeyCode.Text);
            key.Remark = (string)(txtModifyKeyRemark.Text);
            key.ValueType  = (string)(cbxModifyValueType.Value);
            key.HasSelection = (string)(cbxModifyCanDropDown.Value);
            ledgerKeyManager.Update(key);
            this.AppendWebLog("电子台账项目修改", "项目编号：" + txtModifyKeyId.Text);
            pageToolBar.DoRefresh();
            this.windowModifyKey.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 选择更改赋值类型时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void cbxModifyValueType_change(object sender, EventArgs e)
    {
        if (cbxModifyValueType.Value.ToString() == "日期")
        {
            this.cbxModifyCanDropDown.Value = "0";
            this.cbxModifyCanDropDown.Disable(true);
        }
        else
        {
            this.cbxModifyCanDropDown.Enable(true);
        }
    }

    /// <summary>
    /// 选择新增赋值类型时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void cbxAddValueType_change(object sender, EventArgs e)
    {
        if (cbxAddValueType.Value.ToString() == "日期")
        {
            this.cbxAddCanDropDown.Value = "0";
            this.cbxAddCanDropDown.Disable(true);
        }
        else
        {
            this.cbxAddCanDropDown.Enable(true);
        }
    }

    /// <summary>
    /// 点击添加值中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddValueSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcLedgerKeyValue> valueList = ledgerKeyValueManager.GetListByWhere((QmcLedgerKeyValue._.KeyId == txtHiddenKeyId.Text) && (QmcLedgerKeyValue._.KeyValue == txtAddValueContent.Text.TrimStart().TrimEnd()) && (QmcLedgerKeyValue._.DeleteFlag == "0"));
            if (valueList.Count > 0)
            {
                X.Msg.Alert("提示", "此值内容有重复！").Show();
                return;
            }
            QmcLedgerKeyValue value = new QmcLedgerKeyValue();
            value.ValueId = Convert.ToInt32(ledgerKeyValueManager.GetNextValueId());
            value.KeyId = Convert.ToInt32(txtHiddenKeyId.Value);
            value.KeyValue = (string)(txtAddValueContent.Text);
            value.Remark = (string)(txtAddValueRemark.Text);
            value.DeleteFlag = "0";
            ledgerKeyValueManager.Insert(value);
            this.AppendWebLog("电子台账项目值添加", "值编号：" + value.ValueId);
            pageToolBar2.DoRefresh();
            this.windowAddValue.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改值中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnModifyValueSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcLedgerKeyValue> valueList = ledgerKeyValueManager.GetListByWhere((QmcLedgerKeyValue._.KeyId == txtHiddenKeyId.Text) && (QmcLedgerKeyValue._.KeyValue == txtModifyValueContent.Text.TrimStart().TrimEnd()) && (QmcLedgerKeyValue._.DeleteFlag == "0"));
            if (valueList.Count > 0)
            {
                if (valueList[0].KeyValue != txtHiddenKeyValue.Text)
                {
                    X.Msg.Alert("提示", "此值内容有重复！").Show();
                    return;
                }
            }
            QmcLedgerKeyValue value = new QmcLedgerKeyValue();
            value.Attach();
            value.ValueId = Convert.ToInt32(txtModifyValueId.Text);
            value.KeyId = Convert.ToInt32(txtHiddenKeyId.Value);
            value.KeyValue = (string)(txtModifyValueContent.Text);
            value.Remark = (string)(txtModifyValueRemark.Text);
            value.DeleteFlag = "0";
            ledgerKeyValueManager.Update(value);
            this.AppendWebLog("电子台账项目值修改", "值编号：" + value.ValueId);
            pageToolBar2.DoRefresh();
            this.windowModifyValue.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }
    #endregion
}