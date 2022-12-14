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
using System.Text.RegularExpressions;

public partial class Manager_RawMaterialQuality_SpecMapping : BasePage
{
    #region 属性注入
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IQmcSpecMappingManager mappingManager = new QmcSpecMappingManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected static EntityArrayList<QmcSpecMapping> copyList = new EntityArrayList<QmcSpecMapping>();
    protected static String copySeriesId = "";//存放复制的规格对应的物料的所属系列
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            复制 = new SysPageAction() { ActionID = 2, ActionName = "btnCopy" };
            粘贴 = new SysPageAction() { ActionID = 3, ActionName = "btnPaste" };
            导出 = new SysPageAction() { ActionID = 4, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
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
            this.btnSaveMapping.Disable(true);//初始化指标保存按钮
            this.ComboBox2.Disable(true);
            InitTreeSeries();//初始化原材料系列树
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
            nodeChild.NodeID = minorType.MinorTypeID.ToString();
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
    private PageResult<BasMaterial> GetPageResultData(PageResult<BasMaterial> pageParams)
    {
        BasMaterialManager.QueryParams queryParams = new BasMaterialManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialCode = txtMaterialCode.Text.TrimEnd().TrimStart();
        queryParams.majorTypeID = "1";
        queryParams.minorTypeID = txtHiddenMaterialMinorTypeId.Value.ToString();
        queryParams.materialName = txtMaterialName.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";
        return materialManager.GetTablePageDataBySql(queryParams);
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
        if (!Regex.IsMatch(txtMaterialCode.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txtMaterialCode.Text = "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterial> pageParams = new PageResult<BasMaterial>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterial> lst = GetPageResultData(pageParams);
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
        //加载所选原材料的规格
        string materialCode = e.Parameters["MaterialCode"];
        string seriesId = (string)txtHiddenMaterialMinorTypeId.Value;
        this.txtHiddenMaterialCode.Value = materialCode;
        BasMaterial material = materialManager.GetListByWhere(BasMaterial._.MaterialCode == materialCode)[0];
        this.txtHiddenMaterialName.Value = material.MaterialName;
        if (FillDetailTable() > 0)
        {
            this.btnSaveMapping.Enable(true);
            this.ComboBox2.Enable(true);
        }
        else
        {
            this.btnSaveMapping.Disable(true);
            this.ComboBox2.Disable(true);
        }
    }

    /// <summary>
    /// 重置对应关系列表
    /// </summary>
    [DirectMethod]
    public void ResetMappingPanel()
    {
        this.btnSaveMapping.Disable(true);//重置对应关系保存按钮
        this.ComboBox2.Disable(true);
        this.pnlMapping.ClearContent();
    }

    /// <summary>
    /// 填充规格映射列表
    /// </summary>
    protected int FillDetailTable()
    {
        this.mappingSelectionModel.SelectedRows.Clear();//很重要
        //获取规格
        EntityArrayList<QmcSpec> specList = specManager.GetListByWhereAndOrder((QmcSpec._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString()) && (QmcSpec._.DeleteFlag == "0"), QmcSpec._.SpecId.Asc);
        //获取规格映射
        //DeleteFlag为1的也获取
        EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhereAndOrder(QmcSpecMapping._.MaterialCode == txtHiddenMaterialCode.Value.ToString(), QmcSpecMapping._.MappingId.Asc);
        List<MappingViewUnit> unitList = new List<MappingViewUnit>();//映射显示单元
        foreach (QmcSpec spec in specList)
        {
            QmcSpecMapping tempMapping = null;
            foreach (QmcSpecMapping mapping in mappingList)
            {
                //若有映射则加载其映射
                if (mapping.SpecId == spec.SpecId)
                {
                    tempMapping = mapping;
                }
            }
            //若无映射则创建DeleteFlag为1的占位映射
            if (tempMapping == null)
            {
                tempMapping = new QmcSpecMapping();
                tempMapping.MappingId = Convert.ToInt32(mappingManager.GetNextMappingId());
                tempMapping.SpecId = spec.SpecId;
                tempMapping.SpecName = spec.SpecName;
                tempMapping.MaterialCode = txtHiddenMaterialCode.Text.ToString();
                tempMapping.DeleteFlag = "1";
                tempMapping.Remark = "";
                mappingManager.Insert(tempMapping);
                mappingList.Add(tempMapping);
            }
            //填充映射显示单元
            MappingViewUnit unit = new MappingViewUnit();
            unit.MappingId = tempMapping.MappingId.ToString();
            unit.MaterialCode = tempMapping.MaterialCode.ToString();
            unit.MaterialName = (string)txtHiddenMaterialName.Value;
            unit.SpecId = tempMapping.SpecId.ToString();
            unit.SpecName = tempMapping.SpecName;
            unit.Remark = tempMapping.Remark;
            unitList.Add(unit);
            //选中已存在的规格
            if (tempMapping.DeleteFlag == "0")
            {
                this.mappingSelectionModel.SelectedRows.Add(new SelectedRow(unit.MappingId));
            }
        }
        this.storeMapping.DataSource = unitList;
        this.storeMapping.DataBind();
        this.mappingSelectionModel.UpdateSelection();
        return unitList.Count;
    }
    #endregion

    #region 点击增删改按钮激发的事件
    /// <summary>
    /// 点击复制按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Copy_Click(object sender, EventArgs e)
    {
        string seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();//获取当前的原材料系列Id
        string materialCode = txtHiddenMaterialCode.Value.ToString();//获取当前的原材料
        EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == materialCode);
        if (mappingList.Count > 0)
        {
            copyList.Clear();
            copySeriesId = seriesId;
            foreach (QmcSpecMapping mapping in mappingList)
            {
                copyList.Add(mapping);
            }
            msg.Alert("操作", "复制了" + copyList.Count + "条规格！");
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
            //不能粘贴给自身
            if (copySeriesId != txtHiddenMaterialMinorTypeId.Value.ToString())
            {
                msg.Alert("操作", "不能粘贴到不同系列的原材料！");
                msg.Show();
                return;
            }
            if (copyList[0].MaterialCode.ToString() == txtHiddenMaterialCode.Value.ToString())
            {
                msg.Alert("操作", "不能粘贴到源物料！");
                msg.Show();
            }
            else
            {
                EntityArrayList<QmcSpecMapping> pasteList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == txtHiddenMaterialCode.Value.ToString());//存放要粘贴的数据
                foreach (QmcSpecMapping mapping in copyList)
                {
                    foreach (QmcSpecMapping pastedMapping in pasteList)
                    {
                        //建立要粘贴的数据
                        if (mapping.SpecId == pastedMapping.SpecId)
                        {
                            pastedMapping.Remark = mapping.Remark;
                            pastedMapping.DeleteFlag = mapping.DeleteFlag;
                        }
                    }
                }
                //写入粘贴的数据
                try
                {
                    int pasteCount = 0;//粘贴成功计数
                    foreach (QmcSpecMapping pastedMapping in pasteList)
                    {
                        mappingManager.Update(pastedMapping);
                        pasteCount++;
                    }
                    this.AppendWebLog("规格映射粘贴", "规格数量：" + pasteCount);
                    pageToolBar2.DoRefresh();
                    this.mappingSelectionModel.UpdateSelection();
                    msg.Alert("操作", "粘贴了" + pasteCount + "条规格！");
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
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        //获取规格
        EntityArrayList<QmcSpec> specList = specManager.GetListByWhereAndOrder((QmcSpec._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString()) && (QmcSpec._.DeleteFlag == "0"), QmcSpec._.SpecId.Asc);
        //获取规格映射
        //DeleteFlag为1的也获取
        EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhereAndOrder(QmcSpecMapping._.MaterialCode == txtHiddenMaterialCode.Value.ToString(), QmcSpecMapping._.MappingId.Asc);
        List<MappingViewUnit> unitList = new List<MappingViewUnit>();//映射显示单元
        foreach (QmcSpec spec in specList)
        {
            QmcSpecMapping tempMapping = null;
            foreach (QmcSpecMapping mapping in mappingList)
            {
                //若有规格映射则加载其映射
                if (spec.SpecId == mapping.MappingId)
                {
                    tempMapping = mapping;
                }
            }
            //若无映射则建立DeleteFlag为1的占位映射
            if (tempMapping == null)
            {
                tempMapping = new QmcSpecMapping();
                tempMapping.MappingId = Convert.ToInt32(mappingManager.GetNextMappingId());
                tempMapping.SpecId = spec.SpecId;
                tempMapping.SpecName = spec.SpecName;
                tempMapping.MaterialCode = txtHiddenMaterialCode.Text.ToString();
                tempMapping.DeleteFlag = "1";
                tempMapping.Remark = "";
                mappingManager.Insert(tempMapping);
                mappingList.Add(tempMapping);
            }
            //填充映射显示单元
            MappingViewUnit unit = new MappingViewUnit();
            unit.MappingId = tempMapping.MappingId.ToString();
            unit.MaterialCode = tempMapping.MaterialCode.ToString();
            unit.MaterialName = (string)txtHiddenMaterialName.Value;
            unit.SpecId = tempMapping.SpecId.ToString();
            unit.SpecName = tempMapping.SpecName;
            unit.Remark = tempMapping.Remark;
            if (tempMapping.DeleteFlag == "0")
            {
                unitList.Add(unit);
            }
        }
        //建立导出模板
        if (unitList.Count > 0)
        {
            DataTable dt = new DataTable("viewUnit");
            DataColumn dc1 = new DataColumn("原材料名称", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("规格名称", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("备注", Type.GetType("System.String"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            //填充导出模板
            foreach (MappingViewUnit unit in unitList)
            {
                DataRow dr = dt.NewRow();
                dr["原材料名称"] = unit.MaterialName;
                dr["规格名称"] = unit.SpecName;
                dr["备注"] = unit.Remark;
                dt.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "规格映射报表-" + txtHiddenMaterialName.Text);//生成Excel文件下载
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string mappingId)
    {
        //初始化修改窗口
        QmcSpecMapping mapping = mappingManager.GetById(mappingId);
        QmcSpec spec = specManager.GetById(mapping.SpecId);
        txtModifyMappingId.Value = mapping.MappingId;
        txtModifySpecId.Value = spec.SpecId;
        txtModifyMaterialName.Value = txtHiddenMaterialName.Value;
        txtModifySpecName.Value = spec.SpecName;
        txtModifyRemark.Value = mapping.Remark;
        if (!string.IsNullOrEmpty(mapping.DeleteFlag))
        {
            if (mapping.DeleteFlag == "0")
            {
                cbxModifyActivated.Checked = true;
            }
            else if (mapping.DeleteFlag == "1")
            {
                cbxModifyActivated.Checked = false;
            }
            else
            {
                cbxModifyActivated.Checked = false;
            }
        }
        else
        {
            cbxModifyActivated.Checked = false;
        }
        this.windowModifyMapping.Show();
    }

    /// <summary>
    /// 点击修改映射中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyMappingSave_Click(object sender, EventArgs e)
    {
        try
        {
            QmcSpecMapping mapping = mappingManager.GetById(txtModifyMappingId.Text);
            mapping.Attach();
            if (cbxModifyActivated.Checked)
            {
                mapping.DeleteFlag = "0";
            }
            else
            {
                mapping.DeleteFlag = "1";
            }
            mapping.Remark = txtModifyRemark.Text;
            mappingManager.Update(mapping);
            this.AppendWebLog("规格映射修改", "规格编号：" + txtModifyMappingId.Text);
            pageToolBar2.DoRefresh();
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
    }

    /// <summary>
    /// 点击保存规格按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_savemapping_Click(object sender, EventArgs e)
    {
        try
        {
            EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == txtHiddenMaterialCode.Value.ToString());
            int count = 0;
            foreach (QmcSpecMapping mappingOrigin in mappingList)
            {
                mappingOrigin.DeleteFlag = "1";
                foreach (SelectedRow row in mappingSelectionModel.SelectedRows)
                {
                    if (mappingOrigin.MappingId.ToString() == row.RecordID)
                    {
                        mappingOrigin.DeleteFlag = "0";
                        count++;
                    }
                }
                mappingManager.Update(mappingOrigin);
            }
            this.AppendWebLog("规格映射保存", "条目数：" + count);
            pageToolBar2.DoRefresh();
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

    #region 规格映射显示单元
    [Serializable]
    private class MappingViewUnit
    {
        #region 私有字段
        private string mappingId = String.Empty;
        private string materialCode = String.Empty;
        private string materialName = String.Empty;
        private string specId = String.Empty;
        private string specName = String.Empty;
        private string remark = String.Empty;
        #endregion

        #region 公有方法
        public string MappingId
        {
            set { this.mappingId = value; }
            get { return this.mappingId; }
        }

        public string MaterialCode
        {
            set { this.materialCode = value; }
            get { return this.materialCode; }
        }

        public string MaterialName
        {
            set { this.materialName = value; }
            get { return this.materialName; }
        }

        public string SpecId
        {
            set { this.specId = value; }
            get { return this.specId; }
        }

        public string SpecName
        {
            set { this.specName = value; }
            get { return this.specName; }
        }

        public string Remark
        {
            set { this.remark = value; }
            get { return this.remark; }
        }
        #endregion
    }
    #endregion
}