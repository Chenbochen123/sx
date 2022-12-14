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
using System.Text.RegularExpressions;
using Mesnac.Data.Components;

public partial class Manager_RawMaterialQuality_FactoryMapping : BasePage
{

    #region 属性注入
    protected IQmcFactoryMappingManager mappingManager = new QmcFactoryMappingManager();
    protected IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    protected IBasMaterialMinorTypeManager seriesManager = new BasMaterialMinorTypeManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新增对应关系 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新增对应关系 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitSeries();//初始化原材料下拉菜单
        }
    }

    /// <summary>
    /// 初始化原材料下拉菜单
    /// </summary>
    protected void InitSeries()
    {
        EntityArrayList<BasMaterialMinorType> lst = new EntityArrayList<BasMaterialMinorType>();
        lst = seriesManager.GetListByWhere(BasMaterialMinorType._.MajorID == 1 && BasMaterialMinorType._.DeleteFlag == "0");
        if (lst.Count > 0)
        {
            cbxSeriesName.Text = lst[0].MinorTypeName;
            cbxSeriesName.Value = lst[0].MinorTypeID;
            foreach (BasMaterialMinorType type in lst)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = type.MinorTypeName;
                item.Value = type.MinorTypeID;
                cbxSeriesName.Items.Add(item);
                cbxAddSeriesName.Items.Add(item);
                cbxAddSeriesName.SelectedItem.Index = 0;
                cbxModifySeriesName.Items.Add(item);
                cbxModifySeriesName.SelectedItem.Index = 0;
            }
        }
    }
    #endregion

    #region 分页相关方法
    /// <summary>
    /// 根据筛选条件获取分页数据
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<QmcFactoryMapping> GetPageResultData(PageResult<QmcFactoryMapping> pageParams)
    {
        QmcFactoryMappingManager.QueryParams queryParams = new QmcFactoryMappingManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.supplierName = txtSupplierName.Text.TrimEnd().TrimStart();
        queryParams.supplierERPCode = txtSupplierERPCode.Text.TrimEnd().TrimStart();
        queryParams.manufacturerName = txtManufacturerName.Text.TrimEnd().TrimStart();
        queryParams.manufacturerERPCode = txtManufacturerERPCode.Text.TrimEnd().TrimStart();
        queryParams.seriesId = cbxSeriesName.SelectedItem.Value;
        queryParams.deleteFlag = "0";
        return mappingManager.GetTablePageDataBySql(queryParams);
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
        PageResult<QmcFactoryMapping> pageParams = new PageResult<QmcFactoryMapping>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "MappingId ASC";

        PageResult<QmcFactoryMapping> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
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
        txtAddSeriesId.Value = "";
        txtAddSupplierId.Value = "";
        txtAddManufacturerId.Value = "";
        txtAddMappingId.Value = "";
        txtAddRemark.Value = "";
        trfAddManufacturerName.Value = "";
        trfAddSupplierName.Value = "";
        cbxAddSeriesName.SelectedItem.Index = 0;
        btnAddMappingSave.Disable();
        this.windowAddMapping.Show();
    }
    
    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<QmcFactoryMapping> pageParams = new PageResult<QmcFactoryMapping>();
        pageParams.PageSize = -100;//全部数据
        PageResult<QmcFactoryMapping> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlMapping.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()) &&(cb.Visible == true))
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "厂商对应关系报表");
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
    public string commandcolumn_direct_delete(string mappingId)
    {
        try
        {
            QmcFactoryMapping mapping = mappingManager.GetById(Convert.ToInt32(mappingId));
            mapping.DeleteFlag = "1";
            mappingManager.Update(mapping);
            this.AppendWebLog("厂商对应关系删除", "关系编号：" + mappingId);
            pageToolBar.DoRefresh();
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
    public void commandcolumn_direct_edit(string mappingId)
    {
        //初始化修改窗口
        QmcFactoryMapping mapping = mappingManager.GetById(Convert.ToInt32(mappingId));
        txtModifyMappingId.Value = mapping.MappingId;
        txtModifySeriesId.Value = mapping.SeriesId;
        txtModifySupplierId.Value = mapping.SupplierId;
        txtModifyManufacturerId.Value = mapping.ManufacturerId;
        txtModifyRemark.Value = mapping.Remark;
        trfModifySupplierName.Value = mapping.SupplierName;
        trfModifyManufacturerName.Value = mapping.ManufacturerName;
        cbxModifySeriesName.Value = mapping.SeriesId;
        cbxModifySeriesName.Text = mapping.SeriesName;
        
        this.windowModifyMapping.Show();
    }

    /// <summary>
    /// 点击添加项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddMappingSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcFactoryMapping> lst = mappingManager.GetListByWhere(((QmcFactoryMapping._.SeriesId == cbxAddSeriesName.SelectedItem.Value.ToString()) && (QmcFactoryMapping._.SupplierId == Convert.ToInt32(txtAddSupplierId.Text))) && (QmcFactoryMapping._.ManufacturerId == Convert.ToInt32(txtAddManufacturerId.Text)) && (QmcFactoryMapping._.DeleteFlag == "0"));
            if (lst.Count > 0)
            {
                X.Msg.Alert("提示", "有完全重复的对应关系！").Show();
                return;
            }
            QmcFactoryMapping mapping = new QmcFactoryMapping();
            BasFactoryInfo supplier = factoryManager.GetById(Convert.ToInt32(txtAddSupplierId.Value));
            BasFactoryInfo manufacturer = factoryManager.GetById(Convert.ToInt32(txtAddManufacturerId.Value));
            mapping.MappingId = Convert.ToInt32(mappingManager.GetNextMappingId());
            if (cbxAddSeriesName.SelectedItem.Value.Length == 2)
            {
                mapping.SeriesId = cbxAddSeriesName.Value.ToString();
                mapping.SeriesName = cbxAddSeriesName.SelectedItem.Text;
            }
            mapping.ManufacturerId = Convert.ToInt32((txtAddManufacturerId.Text));
            mapping.SupplierId = Convert.ToInt32((txtAddSupplierId.Text));
            mapping.SupplierName = (string)(trfAddSupplierName.Text);
            mapping.ManufacturerName = (string)(trfAddManufacturerName.Text);
            mapping.SupplierERPCode = supplier.ERPCode;
            mapping.ManufacturerERPCode = manufacturer.ERPCode;
           
            mapping.DeleteFlag = "0";
            mapping.Remark = (string)(txtAddRemark.Text);
            mappingManager.Insert(mapping);
            this.AppendWebLog("厂商对应关系添加", "关系编号：" + mapping.MappingId);
            pageToolBar.DoRefresh();
            this.windowAddMapping.Close();
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
    /// 点击修改关系中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyMappingSave_Click(object sender, EventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<QmcFactoryMapping> lst = mappingManager.GetListByWhere(((QmcFactoryMapping._.SeriesId == cbxModifySeriesName.SelectedItem.Value.ToString()) && (QmcFactoryMapping._.SupplierId == txtModifySupplierId.Text.TrimStart().TrimEnd())) && (QmcFactoryMapping._.ManufacturerId == txtModifyManufacturerId.Text.TrimStart().TrimEnd()) && (QmcFactoryMapping._.DeleteFlag == "0"));
            if (lst.Count > 0)
            {
                X.Msg.Alert("提示", "有完全重复的对应关系！").Show();
                return;
            }
            QmcFactoryMapping mapping = new QmcFactoryMapping();
            BasFactoryInfo supplier = factoryManager.GetById(Convert.ToInt32(txtModifySupplierId.Value));
            BasFactoryInfo manufacturer = factoryManager.GetById(Convert.ToInt32(txtModifyManufacturerId.Value));
            mapping.MappingId = Convert.ToInt32(txtModifyMappingId.Text);
            mapping.Attach();
            if (cbxModifySeriesName.SelectedItem.Value.Length == 2)
            {
                mapping.SeriesId = cbxModifySeriesName.SelectedItem.Value.ToString();
                mapping.SeriesName = cbxModifySeriesName.SelectedItem.Text;
            }
            mapping.SupplierId =  Convert.ToInt32(txtModifySupplierId.Text);
            mapping.SupplierName = (string)trfModifySupplierName.Text;
            mapping.SupplierERPCode = supplier.ERPCode;
            mapping.ManufacturerId = Convert.ToInt32(txtModifyManufacturerId.Text);
            mapping.ManufacturerName = (string)trfModifyManufacturerName.Text;
            mapping.ManufacturerERPCode = manufacturer.ERPCode;
            mapping.Remark = (string)txtModifyRemark.Text;
            mapping.DeleteFlag = "0";
            mappingManager.Update(mapping);
            this.AppendWebLog("厂商对应关系修改", "关系编号：" + mapping.MappingId);
            pageToolBar.DoRefresh();
            this.windowModifyMapping.Close();
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
        this.windowModifyMapping.Close();
        this.windowAddMapping.Close();
    }
    #endregion
}