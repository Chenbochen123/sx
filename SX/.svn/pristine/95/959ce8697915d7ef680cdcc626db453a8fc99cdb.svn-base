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

public partial class Manager_RawMaterialQuality_Property : BasePage
{
    #region 属性注入
    protected IQmcPropertyManager propertyManager = new QmcPropertyManager();
    protected IQmcPropertyValueManager propertyValueManager = new QmcPropertyValueManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected static EntityArrayList<QmcProperty> copyList = new EntityArrayList<QmcProperty>();//保存复制的属性
    protected static EntityArrayList<QmcPropertyValue> valueCopyList = new EntityArrayList<QmcPropertyValue>();//保存复制的属性值
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
            复制 = new SysPageAction() { ActionID = 4, ActionName = "btnCopy" };
            粘贴 = new SysPageAction() { ActionID = 5, ActionName = "btnPaste" };
            导出 = new SysPageAction() { ActionID = 6, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新增条目 { get; private set; } //必须为 public
        public SysPageAction 新增条目值选项 { get; private set; } //必须为 public
        public SysPageAction 复制 { get; private set; } //必须为 public
        public SysPageAction 粘贴 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    /// <summary>
    /// 页面初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.btnAddValue.Disable(true);
            InitTreeSeries();
        }
    }

    /// <summary>
    /// 初始化原材料系列树
    /// </summary>
    private void InitTreeSeries()
    {
        EntityArrayList<BasMaterialMinorType> seriesList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.DeleteFlag == "0"));
        foreach (BasMaterialMinorType minorType in seriesList)
            {
                Node nodeChild = new Node();
                nodeChild.NodeID = minorType.ObjID.ToString();
                nodeChild.Text = minorType.MinorTypeName;
                nodeChild.Leaf = true;
                nodeChild.Icon = Icon.Box;
                treeSeries.GetRootNode().AppendChild(nodeChild);
            }
    }
    #endregion

    #region 分页相关方法
    /// <summary>
    /// 根据筛选条件获取分页数据
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<QmcProperty> GetPageResultData(PageResult<QmcProperty> pageParams)
    {
        QmcPropertyManager.QueryParams queryParams = new QmcPropertyManager.QueryParams();
        queryParams.pageParams = pageParams;
        if (cbxValueType.SelectedItem.Value != "all")
            queryParams.valueType = cbxValueType.SelectedItem.Value;
        if (cbxCanDropDown.SelectedItem.Value != "all")
            queryParams.hasSelection = cbxCanDropDown.SelectedItem.Value;
        queryParams.seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();
        queryParams.deleteFlag = "0";
        return propertyManager.GetTablePageDataBySql(queryParams);
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
        PageResult<QmcProperty> pageParams = new PageResult<QmcProperty>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "PropertyId ASC";

        PageResult<QmcProperty> lst = GetPageResultData(pageParams);
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
        //加载所选属性的可选值
        string propertyId = e.Parameters["PropertyId"];
        this.txtHiddenPropertyId.Value = propertyId;
        SetBtnAddValueStatus();
        this.storeValue.DataSource = propertyValueManager.GetListByWhere((QmcPropertyValue._.PropertyId == propertyId) && (QmcPropertyValue._.DeleteFlag == "0"));
        this.storeValue.DataBind();
    }

    /// <summary>
    /// 设置按钮的状态
    /// </summary>
    protected void SetBtnAddValueStatus()
    {
        QmcProperty property = propertyManager.GetById(Convert.ToInt32(txtHiddenPropertyId.Value));
        if (property.HasSelection != "1")
        {
            this.btnAddValue.Disable(true);
            this.txtAddValuePropertyName.Value = "";
            this.txtAddValuePropertyValueType.Value = "";
        }
        else
        {
            this.btnAddValue.Enable(true);
            this.txtAddValuePropertyName.Value = property.PropertyName;
            this.txtAddValuePropertyValueType.Value = property.ValueType;
        }
    }
    #endregion

    #region 点击增删改按钮激发的事件

    /// <summary>
    /// 点击添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //初始化添加窗口
        BasMaterialMinorType type = minorTypeManager.GetById(Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value));
        txtAddPropertySeriesName.Value = type.MinorTypeName;
        txtAddPropertyId.Value = "";
        txtAddPropertyName.Value = "";
        txtHiddenPropertyName.Value = "";
        txtHiddenPropertyCode.Value = "";
        txtAddPropertyCode.Value = "";
        txtAddPropertyRemark.Value = "";
        cbxAddCanDropDown.Value = "0";
        cbxAddValueType.Value = "文字";
        btnAddPropertySave.Disable(true);
        this.windowAddProperty.Show();
    }

    /// <summary>
    /// 点击复制按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        string seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();
        EntityArrayList<QmcProperty> propertyList = propertyManager.GetListByWhere((QmcProperty._.SeriesId == seriesId) && (QmcProperty._.DeleteFlag == "0"));
        if (propertyList.Count > 0)
        {
            copyList.Clear();
            foreach (QmcProperty property in propertyList)
            {
                copyList.Add(property);
            }
            msg.Alert("操作", "复制了" + copyList.Count + "条记录！");
            msg.Show();
        }
        else
        {
            msg.Alert("操作", "没有可以复制的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击粘贴按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Paste_Click(object sender, EventArgs e)
    {
        if (copyList.Count > 0)
        {
            if (copyList[0].SeriesId.ToString() == txtHiddenMaterialMinorTypeId.Value.ToString())
            {
                msg.Alert("操作", "不能粘贴到源系列！");
                msg.Show();
            }
            else
            {
                valueCopyList.Clear();
                EntityArrayList<QmcProperty> pasteList = new EntityArrayList<QmcProperty>();
                EntityArrayList<QmcProperty> originList = propertyManager.GetListByWhere(QmcProperty._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString());
                int idIndex = 0;
                int valueIndex = 0;
                //略过重复的属性
                foreach (QmcProperty property in copyList)
                {
                    bool hasRepeat = false;
                    foreach (QmcProperty originProperty in originList)
                    {
                        if (property.PropertyName == originProperty.PropertyName && originProperty.DeleteFlag == "0")
                        {
                            hasRepeat = true;
                        }
                    }
                    if (hasRepeat)
                    {
                        continue;
                    }
                    //复制对应属性的可选项
                    EntityArrayList<QmcPropertyValue> valueList = new EntityArrayList<QmcPropertyValue>();
                    if (property.HasSelection == "1")
                    {
                        valueList = propertyValueManager.GetListByWhere((QmcPropertyValue._.PropertyId == property.PropertyId) && (QmcPropertyValue._.DeleteFlag == "0"));
                        if (valueList.Count > 0)
                        {
                            foreach (QmcPropertyValue value in valueList)
                            {
                                QmcPropertyValue pastedValue = new QmcPropertyValue();
                                pastedValue.ValueId = Convert.ToInt32(propertyValueManager.GetNextValueId()) + valueIndex;
                                pastedValue.PropertyId = Convert.ToInt32(propertyManager.GetNextPropertyId()) + idIndex;
                                pastedValue.PropertyValue = value.PropertyValue;
                                pastedValue.Remark = value.Remark;
                                pastedValue.DeleteFlag = value.DeleteFlag;
                                valueCopyList.Add(pastedValue);
                                valueIndex++;
                            }
                        }
                    }
                    QmcProperty pastedProperty = new QmcProperty();
                    pastedProperty.PropertyId = Convert.ToInt32(propertyManager.GetNextPropertyId()) + idIndex;
                    pastedProperty.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value);
                    pastedProperty.PropertyName = property.PropertyName;
                    pastedProperty.PropertyCode = property.PropertyCode;
                    pastedProperty.ValueType = property.ValueType;
                    pastedProperty.HasSelection = property.HasSelection;
                    pastedProperty.Remark = property.Remark;
                    pastedProperty.DeleteFlag = property.DeleteFlag;
                    pasteList.Add(pastedProperty);
                    idIndex++;                 
                }
                try
                {
                    int pasteCount = 0;
                    foreach (QmcProperty property in pasteList)
                    {
                        propertyManager.Insert(property);
                        pasteCount++;
                    }
                    //粘贴对应属性的可选项
                    foreach (QmcPropertyValue value in valueCopyList)
                    {
                        QmcPropertyValue pastedValue = new QmcPropertyValue();
                        pastedValue.ValueId = value.ValueId;
                        pastedValue.PropertyId = value.PropertyId;
                        pastedValue.PropertyValue = value.PropertyValue;
                        pastedValue.Remark = value.Remark;
                        pastedValue.DeleteFlag = value.DeleteFlag;
                        propertyValueManager.Insert(value);
                    }
                    this.AppendWebLog("属性粘贴", "粘贴数量：" + pasteCount);
                    pageToolBar.DoRefresh();
                    msg.Alert("操作", "粘贴了" + pasteCount + "条属性！");
                    msg.Show();
                }
                catch (Exception ex)
                {
                    msg.Alert("操作", "粘贴失败：" + ex);
                    msg.Show();
                }
            }
        }
        else
        {
            msg.Alert("操作", "没有可以粘贴的内容！");
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
        //初始化添加值窗口
        txtAddValueId.Value = "";
        txtAddValueContent.Value = "";
        txtAddValueRemark.Value = "";
        txtHiddenPropertyValue.Value = "";
        btnAddValueSave.Disable(true);
        this.windowAddValue.Show();
    }

    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<QmcProperty> pageParams = new PageResult<QmcProperty>();
        pageParams.PageSize = -100;
        PageResult<QmcProperty> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlProperty.ColumnModel.Columns)
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "属性项目报表-" + minorTypeManager.GetById(txtHiddenMaterialMinorTypeId.Text).MinorTypeName);
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string propertyId)
    {
        try
        {
            QmcProperty property = propertyManager.GetById(Convert.ToInt32(propertyId));
            property.DeleteFlag = "1";
            propertyManager.Update(property);
            this.AppendWebLog("属性删除", "属性编号：" + propertyId);
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
            QmcPropertyValue value = propertyValueManager.GetById(Convert.ToInt32(valueId));
            value.DeleteFlag = "1";
            propertyValueManager.Update(value);
            this.AppendWebLog("属性值删除", "值编号：" + valueId);
            pageToolBar2.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string propertyId)
    {
        //初始化修改窗口
        QmcProperty property = propertyManager.GetById(Convert.ToInt32(propertyId));
        BasMaterialMinorType type = minorTypeManager.GetById(Convert.ToInt32(txtHiddenMaterialMinorTypeId.Value));
        txtModifyPropertyId.Value = property.PropertyId;
        txtModifyPropertySeriesName.Value = type.MinorTypeName;
        txtModifyPropertyName.Value = property.PropertyName;
        txtModifyPropertyCode.Value = property.PropertyCode;
        txtModifyPropertyRemark.Value = property.Remark;
        txtHiddenPropertyName.Value = property.PropertyName;
        txtHiddenPropertyCode.Value = property.PropertyCode;
        cbxModifyValueType.Value = property.ValueType;
        cbxModifyCanDropDown.Value = property.HasSelection;
        this.windowModifyProperty.Show();
    }

    /// <summary>
    /// 点击修改值激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string valueId)
    {
        //初始化修改值窗口
        QmcPropertyValue value = propertyValueManager.GetById(Convert.ToInt32(valueId));
        QmcProperty key = propertyManager.GetById(Convert.ToInt32(value.PropertyId));
        txtModifyValueId.Value = value.ValueId;
        txtModifyValueContent.Value = value.PropertyValue;
        txtHiddenPropertyValue.Value = value.PropertyValue;
        txtModifyValueRemark.Value = value.Remark;
        txtModifyValuePropertyName.Value = key.PropertyName;
        txtModifyValuePropertyValueType.Value = key.ValueType;
        this.windowModifyValue.Show();
    }

    /// <summary>
    /// 点击添加属性中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddPropertySave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcProperty> propertyList = propertyManager.GetListByWhere(((QmcProperty._.PropertyName == txtAddPropertyName.Text.TrimStart().TrimEnd()) || (QmcProperty._.PropertyCode == txtAddPropertyCode.Text.TrimStart().TrimEnd())) && (QmcProperty._.DeleteFlag == "0") && ((QmcProperty._.PropertyCode != null) && (QmcProperty._.PropertyCode != "")));
            if (propertyList.Count > 0)
            {
                X.Msg.Alert("提示", "此属性名称或英文名称已被使用！").Show();
                return;
            }
            QmcProperty property = new QmcProperty();
            property.PropertyId = Convert.ToInt32(propertyManager.GetNextPropertyId());
            property.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Text);
            property.PropertyName = (string)(txtAddPropertyName.Text);
            property.PropertyCode = (string)(txtAddPropertyCode.Text);
            property.Remark = (string)(txtAddPropertyRemark.Text);
            property.ValueType = (string)(cbxAddValueType.Value);
            property.HasSelection = (string)(cbxAddCanDropDown.Value);
            property.DeleteFlag = "0";
            propertyManager.Insert(property);
            this.AppendWebLog("属性添加", "属性编号：" + property.PropertyId);
            pageToolBar.DoRefresh();
            this.windowAddProperty.Close();
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
    /// 点击修改属性中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyPropertySave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            EntityArrayList<QmcProperty> propertyList = propertyManager.GetListByWhere(((QmcProperty._.PropertyName == txtModifyPropertyName.Text.TrimStart().TrimEnd()) || (QmcProperty._.PropertyCode == txtModifyPropertyCode.Text.TrimStart().TrimEnd())) && (QmcProperty._.DeleteFlag == "0") && ((QmcProperty._.PropertyCode != null) && (QmcProperty._.PropertyCode != "")));
            if (propertyList.Count > 0)
            {
                if (propertyList[0].PropertyName != txtHiddenPropertyName.Text)
                {
                    X.Msg.Alert("提示", "此属性名称已被使用！").Show();
                    return;
                }
                if (propertyList[0].PropertyCode != txtHiddenPropertyCode.Text)
                {
                    X.Msg.Alert("提示", "此英文名称已被使用！").Show();
                    return;
                }
            }
            QmcProperty property = new QmcProperty();
            property.PropertyId = Convert.ToInt32(txtModifyPropertyId.Text);
            property.Attach();
            property.SeriesId = Convert.ToInt32(txtHiddenMaterialMinorTypeId.Text);
            property.PropertyName = (string)(txtModifyPropertyName.Text);
            property.PropertyCode = (string)(txtModifyPropertyCode.Text);
            property.Remark = (string)(txtModifyPropertyRemark.Text);
            property.ValueType = (string)(cbxModifyValueType.Value);
            property.HasSelection = (string)(cbxModifyCanDropDown.Value);
            propertyManager.Update(property);
            this.AppendWebLog("属性修改", "属性编号：" + txtModifyPropertyId.Text);
            pageToolBar.DoRefresh();
            this.windowModifyProperty.Close();
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
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.windowModifyProperty.Close();
        this.windowModifyValue.Close();
        this.windowAddProperty.Close();
        this.windowAddValue.Close();
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
            EntityArrayList<QmcPropertyValue> valueList = propertyValueManager.GetListByWhere((QmcPropertyValue._.PropertyId == txtHiddenPropertyId.Text) && (QmcPropertyValue._.PropertyValue == txtAddValueContent.Text.TrimStart().TrimEnd()) && (QmcPropertyValue._.DeleteFlag == "0"));
            if (valueList.Count > 0)
            {
                X.Msg.Alert("提示", "此值内容有重复！").Show();
                return;
            }
            QmcPropertyValue value = new QmcPropertyValue();
            value.ValueId = Convert.ToInt32(propertyValueManager.GetNextValueId());
            value.PropertyId = Convert.ToInt32(txtHiddenPropertyId.Value);
            value.PropertyValue = (string)(txtAddValueContent.Text);
            value.Remark = (string)(txtAddValueRemark.Text);
            value.DeleteFlag = "0";
            propertyValueManager.Insert(value);
            this.AppendWebLog("属性值添加", "值编号：" + value.ValueId);
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
            EntityArrayList<QmcPropertyValue> valueList = propertyValueManager.GetListByWhere((QmcPropertyValue._.PropertyId == txtHiddenPropertyId.Text) && (QmcPropertyValue._.PropertyValue == txtModifyValueContent.Text.TrimStart().TrimEnd()) && (QmcPropertyValue._.DeleteFlag == "0"));
            if (valueList.Count > 0)
            {
                if (valueList[0].PropertyValue != txtHiddenPropertyValue.Text)
                {
                    X.Msg.Alert("提示", "此值内容有重复！").Show();
                    return;
                }
            }
            QmcPropertyValue value = new QmcPropertyValue();
            value.Attach();
            value.ValueId = Convert.ToInt32(txtModifyValueId.Text);
            value.PropertyId = Convert.ToInt32(txtHiddenPropertyId.Value);
            value.PropertyValue = (string)(txtModifyValueContent.Text);
            value.Remark = (string)(txtModifyValueRemark.Text);
            value.DeleteFlag = "0";
            propertyValueManager.Update(value);
            this.AppendWebLog("属性值修改", "值编号：" + value.ValueId);
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