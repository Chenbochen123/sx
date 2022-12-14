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

public partial class Manager_RawMaterialQuality_CheckItemDetail : BasePage
{
    #region 属性注入
    protected IQmcCheckItemManager itemManager = new QmcCheckItemManager();
    protected IQmcCheckItemDetailManager detailManager = new QmcCheckItemDetailManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected static EntityArrayList<QmcCheckItemDetail> copyList = new EntityArrayList<QmcCheckItemDetail>();
    protected static String copySeriesId = "";//存放复制的检验标准对应的物料的所属系列
    protected static String copyStandardId = "";//存放复制的检验标准对应的执行标准
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
            导入 = new SysPageAction() { ActionID = 4, ActionName = "btnImport" };
            导出 = new SysPageAction() { ActionID = 5, ActionName = "btnExport" };
            保存指标 = new SysPageAction() { ActionID = 6, ActionName = "btnSaveDetail" };
            新增指标 = new SysPageAction() { ActionID = 7, ActionName = "btnAddDetail" };

        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 复制 { get; private set; } //必须为 public
        public SysPageAction 粘贴 { get; private set; } //必须为 public
        public SysPageAction 导入 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 保存指标 { get; private set; } //必须为 public
        public SysPageAction 新增指标 { get; private set; } //必须为 public
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
            InitTreeSeries();//初始化原材料系列树
            InitStandard();
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
    /// <summary>
    /// 初始化执行标准
    /// </summary>
    private void InitStandard()
    {
        EntityArrayList<QmcStandard> standardList = standardManager.GetListByWhereAndOrder(QmcStandard._.DeleteFlag == "0", QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> activeList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "1")), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> readyList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "0")), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> historyList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "2")), QmcStandard._.ActivateFlag.Asc);
        if (standardList.Count == 0)
        {
            btnSaveDetail.Disable();
            btnCopy.Disable();
            btnExport.Disable();
            btnPaste.Disable();
            btnSearch.Disable();
            msg.Alert("提示", "当前没有执行标准，请新建执行标准！");
            msg.Show();
        }
        else
        {
            foreach (QmcStandard standard in standardList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = standard.StandardName;
                item.Value = standard.StandardId.ToString();
                if (standard.ActivateFlag == "2")
                {
                    item.Text = item.Text + "(过期)";
                }
                else if (standard.ActivateFlag == "0")
                {
                    item.Text = item.Text + "(未生效)";
                }
                else if (standard.ActivateFlag == "1")
                {
                    item.Text = item.Text + "(当前)";
                    cbxStandard.Text = item.Text;
                    cbxStandard.Value = item.Value;
                }
                cbxStandard.Items.Add(item);
            }
        }
        if (activeList.Count == 0)
        {
            if (readyList.Count > 0)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = readyList[0].StandardName + "(未生效)";
                item.Value = readyList[0].StandardId.ToString();
                cbxStandard.Text = item.Text;
                cbxStandard.Value = item.Value;
            }
            else if (historyList.Count > 0)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = historyList[0].StandardName + "(过期)";
                item.Value = historyList[0].StandardId.ToString();
                cbxStandard.Text = item.Text;
                cbxStandard.Value = item.Value;
            }
            else
            {
                btnSaveDetail.Disable();
                btnCopy.Disable();
                btnExport.Disable();
                btnPaste.Disable();
                btnSearch.Disable();
                msg.Alert("提示", "当前没有执行标准，请新建执行标准！");
                msg.Show();
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
    private PageResult<BasMaterial> GetPageResultData(PageResult<BasMaterial> pageParams)
    {
        string seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();
        BasMaterialMinorType mType = minorTypeManager.GetById(seriesId);
        BasMaterialManager.QueryParams queryParams = new BasMaterialManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialCode = txtMaterialCode.Text.TrimEnd().TrimStart();
        queryParams.majorTypeID = "1";
        queryParams.minorTypeID = mType.MinorTypeID;
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
    #endregion

    #region 页面方法

    /// <summary>
    /// 行选择事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        //加载所选原材料的检测指标
        string materialCode = e.Parameters["MaterialCode"];
        string seriesId = (string)txtHiddenMaterialMinorTypeId.Value;
        this.txtHiddenMaterialCode.Value = materialCode;
        if (materialCode != "-1")
        {
            BasMaterial material = materialManager.GetListByWhere(BasMaterial._.MaterialCode == materialCode)[0];
            this.txtHiddenMaterialName.Value = material.MaterialName;
        }
        FillDetailTable();
    }

    /// <summary>
    /// 重置指标列表
    /// </summary>
    [DirectMethod]
    public void ResetDetailPanel()
    {
        this.pnlDetail.ClearContent();
    }

    /// <summary>
    /// 填充具体检测项列表
    /// </summary>
    protected int FillDetailTable()
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return 0;
        }
        this.detailSelectionModel.SelectedRows.Clear();//很重要
        //获取检测项目
        //msg.Alert("操作", txtHiddenMaterialMinorTypeId.Value.ToString() + "a" + standardId);
        //msg.Show();
        //return 0;
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhereAndOrder((QmcCheckItem._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString()) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"), QmcCheckItem._.ItemCode.Asc);
      
        //Latest的才获取
        EntityArrayList<QmcCheckItemDetail> detailList = detailManager.GetListByWhereAndOrder((QmcCheckItemDetail._.MaterialCode == txtHiddenMaterialCode.Value.ToString()) && (QmcCheckItemDetail._.LatestFlag == "1") && (QmcCheckItemDetail._.DeleteFlag == "0"), QmcCheckItemDetail._.ItemDetailId.Asc);
        List<DetailViewUnit> unitList = new List<DetailViewUnit>();//指标显示单元
        foreach (QmcCheckItem item in itemList)
        {
            #region 遍历指标
            QmcCheckItemDetail tempDetail = null;
            if (detailList.Count > 0)
            {
                foreach (QmcCheckItemDetail detail in detailList)
                {
                    //若有指标则加载其指标
                    if (item.ItemId == detail.ItemId)
                    {
                        tempDetail = detail;
                        //处理decimal多余的0
                        //已无必要
                        //switch (item.ValueType)
                        //{
                        //    case "文字":
                        //        break;
                        //    case "数字":
                        //        string primeMinCache = detail.PrimeMinValue.ToString();
                        //        string primeMaxCache = detail.PrimeMaxValue.ToString();
                        //        string goodMinCache = detail.GoodMinValue.ToString();
                        //        string goodMaxCache = detail.GoodMaxValue.ToString();
                        //        if (primeMinCache != "")
                        //        {
                        //            decimal primeMinTemp = Convert.ToDecimal(primeMinCache);
                        //            if(primeMinTemp.ToString("F3").EndsWith(".000"))
                        //            {
                        //                primeMinCache = primeMinTemp.ToString("F0");
                        //            }
                        //            else if(primeMinTemp.ToString("F3").EndsWith("00"))
                        //            {
                        //                primeMinCache = primeMinTemp.ToString("F1");
                        //            }
                        //            else if (primeMinTemp.ToString("F3").EndsWith("0"))
                        //            {
                        //                primeMinCache = primeMinTemp.ToString("F2");
                        //            }
                        //        }
                        //        if (primeMaxCache != "")
                        //        {
                        //            decimal primeMaxTemp = Convert.ToDecimal(primeMaxCache);
                        //            if (primeMaxTemp.ToString("F3").EndsWith(".000"))
                        //            {
                        //                primeMaxCache = primeMaxTemp.ToString("F0");
                        //            }
                        //            else if (primeMaxTemp.ToString("F3").EndsWith("00"))
                        //            {
                        //                primeMaxCache = primeMaxTemp.ToString("F1");
                        //            }
                        //            else if (primeMaxTemp.ToString("F3").EndsWith("0"))
                        //            {
                        //                primeMaxCache = primeMaxTemp.ToString("F2");
                        //            }
                        //        }
                        //        if (goodMinCache != "")
                        //        {
                        //            decimal goodMinTemp = Convert.ToDecimal(goodMinCache);
                        //            if (goodMinTemp.ToString("F3").EndsWith(".000"))
                        //            {
                        //                goodMinCache = goodMinTemp.ToString("F0");
                        //            }
                        //            else if (goodMinTemp.ToString("F3").EndsWith("00"))
                        //            {
                        //                goodMinCache = goodMinTemp.ToString("F1");
                        //            }
                        //            else if (goodMinTemp.ToString("F3").EndsWith("0"))
                        //            {
                        //                goodMinCache = goodMinTemp.ToString("F2");
                        //            }
                        //        }
                        //        if (goodMaxCache != "")
                        //        {
                        //            decimal goodMaxTemp = Convert.ToDecimal(goodMaxCache);
                        //            if (goodMaxTemp.ToString("F3").EndsWith(".000"))
                        //            {
                        //                goodMaxCache = goodMaxTemp.ToString("F0");
                        //            }
                        //            else if (goodMaxTemp.ToString("F3").EndsWith("00"))
                        //            {
                        //                goodMaxCache = goodMaxTemp.ToString("F1");
                        //            }
                        //            else if (goodMaxTemp.ToString("F3").EndsWith("0"))
                        //            {
                        //                goodMaxCache = goodMaxTemp.ToString("F2");
                        //            }
                        //        }
                        //        detail.PrimeTextValue = primeMinCache + detail.PrimeOperator + primeMaxCache;
                        //        detail.GoodTextValue = goodMinCache + detail.GoodOperator + goodMaxCache;
                        //        break;
                        //    default:
                        //        break;
                        //}
                    }
                }
                if (tempDetail != null)
                {
                    //填充指标显示单元
                    DetailViewUnit unitC = new DetailViewUnit();
                    unitC.ItemId = tempDetail.ItemId.ToString();
                    unitC.DetailId = tempDetail.ItemDetailId.ToString();
                    unitC.MaterialName = (string)txtHiddenMaterialName.Value;
                    unitC.ItemName = item.ItemName;
                    //unitC.GoodTextValue = tempDetail.GoodTextValue;
                    //unitC.PrimeTextValue = tempDetail.PrimeTextValue;
                    unitC.GoodTextValue = tempDetail.GoodDisplayValue;
                    unitC.MinTextValue = tempDetail.MinDisplayValue;
                    unitC.MaxTextValue = tempDetail.MaxDisplayValue;
                    unitC.PrimeTextValue = tempDetail.PrimeDisplayValue;
                    unitC.Frequency = tempDetail.Frequency;
                    unitC.CheckMethod = tempDetail.CheckMethod;
                    unitC.Version = tempDetail.Version.ToString();
                    unitC.Remark = tempDetail.Remark;
                    unitList.Add(unitC);

                    //选中已存在的指标
                    if (tempDetail.ActivateFlag == "1")
                    {
                        this.detailSelectionModel.SelectedRows.Add(new SelectedRow(unitC.DetailId));
                    }
                }
            }
            #endregion
        }
        if (unitList.Count > 0)
        {
            unitList.Reverse();
        }
        this.storeDetail.DataSource = unitList;
        this.storeDetail.DataBind();
        this.detailSelectionModel.UpdateSelection();
        return unitList.Count;
    }

    /// <summary>
    /// 校验数据合法性
    /// </summary>
    /// <returns></returns>
    private bool ValidateModifyIntegration()
    {

        if (string.IsNullOrEmpty(cbxModifyGoodOperator.Value.ToString()))
        { return true; }
        if (cbxModifyGoodOperator.Value.ToString() == "")
        {
            return true;
            msg.Alert("操作", "合格品运算符不能为空！");
            msg.Show();
            return false;
        }
        #region 合格品输入数据合法性
        if (cbxModifyGoodOperator.Value.ToString() == "－")
        {
            if ((txtModifyGoodMaxValue.Text == "") || (txtModifyGoodMaxValue.Text == null) || (txtModifyGoodMaxValue.Text == String.Empty) || (txtModifyGoodMinValue.Text == "") || (txtModifyGoodMinValue.Text == null) || (txtModifyGoodMinValue.Text == String.Empty))
            {
                msg.Alert("操作", "合格品数字标准不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtModifyGoodMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "合格品标准结束值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (!Regex.Match(txtModifyGoodMinValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "合格品标准起始值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (Convert.ToDecimal(txtModifyGoodMinValue.Value) > Convert.ToDecimal(txtModifyGoodMaxValue.Value))
            {
                msg.Alert("操作", "合格品数字标准结束值必须大于等于起始值！");
                msg.Show();
                return false;
            }
        }
        else
        {
            if ((txtModifyGoodMaxValue.Text == "") || (txtModifyGoodMaxValue.Text == null) || (txtModifyGoodMaxValue.Text == String.Empty))
            {
                msg.Alert("操作", "合格品数字标准不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtModifyGoodMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "合格品标准结束值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
        }
        #endregion

        #region 一级品输入数据合法性
        if (cbxModifyPrimeOperator.Value == null)
        {
            return true;
        }
        else if ((cbxModifyPrimeOperator.Value.ToString() == "无") || (cbxModifyPrimeOperator.Value.ToString() == ""))
        {
            return true;
        }
        else if (cbxModifyPrimeOperator.Value.ToString() == "－")
        {
            if ((txtModifyPrimeMaxValue.Text == "") || (txtModifyPrimeMaxValue.Text == null) || (txtModifyPrimeMaxValue.Text == String.Empty) || (txtModifyPrimeMinValue.Text == "") || (txtModifyPrimeMinValue.Text == null) || (txtModifyPrimeMinValue.Text == String.Empty))
            {
                msg.Alert("操作", "一级品数字标准不正确：已选运算符的情况下，数据不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtModifyPrimeMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "一级品数字标准结束值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (!Regex.Match(txtModifyPrimeMinValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "一级品数字标准起始值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (Convert.ToDecimal(txtModifyPrimeMinValue.Value) > Convert.ToDecimal(txtModifyPrimeMaxValue.Value))
            {
                msg.Alert("操作", "一级品数字标准结束值必须大于等于起始值！");
                msg.Show();
                return false;
            }
        }
        else
        {
            if ((txtModifyPrimeMaxValue.Text == "") || (txtModifyPrimeMaxValue.Text == null) || (txtModifyPrimeMaxValue.Text == String.Empty))
            {
                msg.Alert("操作", "一级品数字标准不正确：已选运算符的情况下，结束值不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtModifyPrimeMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "一级品数字标准结束值格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
        }
        #endregion

        return true;
    }

    /// <summary>
    /// 校验数据合法性
    /// </summary>
    /// <returns></returns>
    private bool ValidateAddIntegration()
    {
        if (cbxAddGoodOperator.Value.ToString() == "")
        {
            return true;
            msg.Alert("操作", "合格品运算符不能为空！");
            msg.Show();
            return false;
        }
        #region 合格品输入数据合法性
        if (cbxAddGoodOperator.Value.ToString() == "－")
        {
            if ((txtAddGoodMaxValue.Text == "") || (txtAddGoodMaxValue.Text == null) || (txtAddGoodMaxValue.Text == String.Empty) || (txtAddGoodMinValue.Text == "") || (txtAddGoodMinValue.Text == null) || (txtAddGoodMinValue.Text == String.Empty))
            {
                msg.Alert("操作", "合格品数字标准不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtAddGoodMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "合格品标准结束值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (!Regex.Match(txtAddGoodMinValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "合格品标准起始值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (Convert.ToDecimal(txtAddGoodMinValue.Value) > Convert.ToDecimal(txtAddGoodMaxValue.Value))
            {
                msg.Alert("操作", "合格品数字标准结束值必须大于等于起始值！");
                msg.Show();
                return false;
            }
        }
        else
        {
            if ((txtAddGoodMaxValue.Text == "") || (txtAddGoodMaxValue.Text == null) || (txtAddGoodMaxValue.Text == String.Empty))
            {
                msg.Alert("操作", "合格品数字标准不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtAddGoodMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "合格品标准结束值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
        }
        #endregion

        #region 一级品输入数据合法性
        if (cbxAddPrimeOperator.Value == null)
        {
            return true;
        }
        else if ((cbxAddPrimeOperator.Value.ToString() == "无") || (cbxAddPrimeOperator.Value.ToString() == ""))
        {
            return true;
        }
        else if (cbxAddPrimeOperator.Value.ToString() == "－")
        {
            if ((txtAddPrimeMaxValue.Text == "") || (txtAddPrimeMaxValue.Text == null) || (txtAddPrimeMaxValue.Text == String.Empty) || (txtAddPrimeMinValue.Text == "") || (txtAddPrimeMinValue.Text == null) || (txtAddPrimeMinValue.Text == String.Empty))
            {
                msg.Alert("操作", "一级品数字标准不正确：已选运算符的情况下，数据不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtAddPrimeMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "一级品数字标准结束值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (!Regex.Match(txtAddPrimeMinValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "一级品数字标准起始值数字格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
            else if (Convert.ToDecimal(txtAddPrimeMinValue.Value) > Convert.ToDecimal(txtAddPrimeMaxValue.Value))
            {
                msg.Alert("操作", "一级品数字标准结束值必须大于等于起始值！");
                msg.Show();
                return false;
            }
        }
        else
        {
            if ((txtAddPrimeMaxValue.Text == "") || (txtAddPrimeMaxValue.Text == null) || (txtAddPrimeMaxValue.Text == String.Empty))
            {
                msg.Alert("操作", "一级品数字标准不正确：已选运算符的情况下，结束值不能为空！");
                msg.Show();
                return false;
            }
            if (!Regex.Match(txtAddPrimeMaxValue.Value.ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
            {
                msg.Alert("操作", "一级品数字标准结束值格式不正确！应为8位有效数字，保留3位小数！");
                msg.Show();
                return false;
            }
        }
        #endregion

        return true;
    }

    /// <summary>
    /// 警告一级品值范围出错
    /// </summary>
    /// <returns></returns>
    private bool AlertPrimeValueOutOfRange()
    {
        msg.Alert("指标逻辑错误", "一级品指标范围在合格品范围之外！");
        msg.Show();
        return false;
    }

    /// <summary>
    /// 校验逻辑正确性
    /// </summary>
    /// <returns></returns>
    private bool ValidateModifyLogic()
    {
        #region 校验指标逻辑正确性
        if (cbxModifyPrimeOperator.Value == null)
        {
            return true;
        }
        else if ((cbxModifyPrimeOperator.Value.ToString() == "") || (cbxModifyPrimeOperator.Value.ToString() == "无"))
        {
            return true;
        }
        else
        {
            switch (cbxModifyGoodOperator.Value.ToString())
            {
                case "－":
                    if (Convert.ToDecimal(txtModifyGoodMinValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                    {
                        if (cbxModifyGoodIncludeMinBorder.Checked && cbxModifyGoodIncludeMaxBorder.Checked)
                        {
                            //skip
                        }
                        else
                        {
                            msg.Alert("指标逻辑错误", "合格品范围错误！");
                            msg.Show();
                            return false;
                        }
                    }
                    switch (cbxModifyPrimeOperator.Value.ToString())
                    {
                        case "－":
                            if (Convert.ToDecimal(txtModifyPrimeMinValue.Value) == Convert.ToDecimal(txtModifyPrimeMaxValue.Value))
                            {
                                if (cbxModifyPrimeIncludeMinBorder.Checked && cbxModifyPrimeIncludeMaxBorder.Checked)
                                {
                                    //skip
                                }
                                else
                                {
                                    msg.Alert("指标逻辑错误", "一级品范围错误！");
                                    msg.Show();
                                    return false;
                                }
                            }
                            else if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) > Convert.ToDecimal(txtModifyGoodMaxValue.Value)) || (Convert.ToDecimal(txtModifyPrimeMinValue.Value) < Convert.ToDecimal(txtModifyGoodMinValue.Value)))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value)) || (Convert.ToDecimal(txtModifyPrimeMinValue.Value) == Convert.ToDecimal(txtModifyGoodMinValue.Value)))
                            {
                                if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value)) && (cbxModifyPrimeIncludeMaxBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                                else if ((Convert.ToDecimal(txtModifyPrimeMinValue.Value) == Convert.ToDecimal(txtModifyGoodMinValue.Value)) && (cbxModifyPrimeIncludeMinBorder.Checked) && (!cbxModifyGoodIncludeMinBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case ">":
                            if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) > Convert.ToDecimal(txtModifyGoodMaxValue.Value)))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value)) && (cbxModifyPrimeIncludeMaxBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            break;
                        case "<":
                            if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) < Convert.ToDecimal(txtModifyGoodMaxValue.Value)))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if ((Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value)) && (cbxModifyPrimeIncludeMaxBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            break;
                        default:
                            return true;
                    }
                    break;
                case ">":
                    switch (cbxModifyPrimeOperator.Value.ToString())
                    {
                        case "－":
                            if (Convert.ToDecimal(txtModifyPrimeMinValue.Value) == Convert.ToDecimal(txtModifyPrimeMaxValue.Value))
                            {
                                if (cbxModifyPrimeIncludeMinBorder.Checked && cbxModifyPrimeIncludeMaxBorder.Checked)
                                {
                                    //skip
                                }
                                else
                                {
                                    msg.Alert("指标逻辑错误", "一级品范围错误！");
                                    msg.Show();
                                    return false;
                                }
                            }
                            if (Convert.ToDecimal(txtModifyPrimeMinValue.Value) < Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtModifyPrimeMinValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                if ((cbxModifyPrimeIncludeMinBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case ">":
                            if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) < Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                if ((cbxModifyPrimeIncludeMaxBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case "<":
                            if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) < Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                if ((cbxModifyPrimeIncludeMaxBorder.Checked) && (cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return true;
                                }
                                else
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        default:
                            return true;
                    }
                    break;
                case "<":
                    switch (cbxModifyPrimeOperator.Value.ToString())
                    {
                        case "－":
                            if (Convert.ToDecimal(txtModifyPrimeMinValue.Value) == Convert.ToDecimal(txtModifyPrimeMaxValue.Value))
                            {
                                if (cbxModifyPrimeIncludeMinBorder.Checked && cbxModifyPrimeIncludeMaxBorder.Checked)
                                {
                                    //skip
                                }
                                else
                                {
                                    msg.Alert("指标逻辑错误", "一级品范围错误！");
                                    msg.Show();
                                    return false;
                                }
                            }
                            if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) > Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                if ((cbxModifyPrimeIncludeMaxBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case ">":
                            if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) > Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                if ((cbxModifyPrimeIncludeMaxBorder.Checked) && (cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return true;
                                }
                                else
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case "<":
                            if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) > Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtModifyPrimeMaxValue.Value) == Convert.ToDecimal(txtModifyGoodMaxValue.Value))
                            {
                                if ((cbxModifyPrimeIncludeMaxBorder.Checked) && (!cbxModifyGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        default:
                            return true;
                    }
                    break;
                default:
                    return false;
            }
        }
        #endregion
        return true;
    }

    /// <summary>
    /// 校验逻辑正确性
    /// </summary>
    /// <returns></returns>
    private bool ValidateAddLogic()
    {
        #region 校验指标逻辑正确性
        if (cbxAddPrimeOperator.Value == null)
        {
            return true;
        }
        else if ((cbxAddPrimeOperator.Value.ToString() == "") || (cbxAddPrimeOperator.Value.ToString() == "无"))
        {
            return true;
        }
        else
        {
            switch (cbxAddGoodOperator.Value.ToString())
            {
                case "－":
                    if (Convert.ToDecimal(txtAddGoodMinValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                    {
                        if (cbxAddGoodIncludeMinBorder.Checked && cbxAddGoodIncludeMaxBorder.Checked)
                        {
                            //skip
                        }
                        else
                        {
                            msg.Alert("指标逻辑错误", "合格品范围错误！");
                            msg.Show();
                            return false;
                        }
                    }
                    switch (cbxAddPrimeOperator.Value.ToString())
                    {
                        case "－":
                            if (Convert.ToDecimal(txtAddPrimeMinValue.Value) == Convert.ToDecimal(txtAddPrimeMaxValue.Value))
                            {
                                if (cbxAddPrimeIncludeMinBorder.Checked && cbxAddPrimeIncludeMaxBorder.Checked)
                                {
                                    //skip
                                }
                                else
                                {
                                    msg.Alert("指标逻辑错误", "一级品范围错误！");
                                    msg.Show();
                                    return false;
                                }
                            }
                            else if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) > Convert.ToDecimal(txtAddGoodMaxValue.Value)) || (Convert.ToDecimal(txtAddPrimeMinValue.Value) < Convert.ToDecimal(txtAddGoodMinValue.Value)))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value)) || (Convert.ToDecimal(txtAddPrimeMinValue.Value) == Convert.ToDecimal(txtAddGoodMinValue.Value)))
                            {
                                if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value)) && (cbxAddPrimeIncludeMaxBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                                else if ((Convert.ToDecimal(txtAddPrimeMinValue.Value) == Convert.ToDecimal(txtAddGoodMinValue.Value)) && (cbxAddPrimeIncludeMinBorder.Checked) && (!cbxAddGoodIncludeMinBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case ">":
                            if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) > Convert.ToDecimal(txtAddGoodMaxValue.Value)))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value)) && (cbxAddPrimeIncludeMaxBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            break;
                        case "<":
                            if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) < Convert.ToDecimal(txtAddGoodMaxValue.Value)))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if ((Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value)) && (cbxAddPrimeIncludeMaxBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            break;
                        default:
                            return true;
                    }
                    break;
                case ">":
                    switch (cbxAddPrimeOperator.Value.ToString())
                    {
                        case "－":
                            if (Convert.ToDecimal(txtAddPrimeMinValue.Value) == Convert.ToDecimal(txtAddPrimeMaxValue.Value))
                            {
                                if (cbxAddPrimeIncludeMinBorder.Checked && cbxAddPrimeIncludeMaxBorder.Checked)
                                {
                                    //skip
                                }
                                else
                                {
                                    msg.Alert("指标逻辑错误", "一级品范围错误！");
                                    msg.Show();
                                    return false;
                                }
                            }
                            if (Convert.ToDecimal(txtAddPrimeMinValue.Value) < Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtAddPrimeMinValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                if ((cbxAddPrimeIncludeMinBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case ">":
                            if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) < Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                if ((cbxAddPrimeIncludeMaxBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case "<":
                            if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) < Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                if ((cbxAddPrimeIncludeMaxBorder.Checked) && (cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return true;
                                }
                                else
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        default:
                            return true;
                    }
                    break;
                case "<":
                    switch (cbxAddPrimeOperator.Value.ToString())
                    {
                        case "－":
                            if (Convert.ToDecimal(txtAddPrimeMinValue.Value) == Convert.ToDecimal(txtAddPrimeMaxValue.Value))
                            {
                                if (cbxAddPrimeIncludeMinBorder.Checked && cbxAddPrimeIncludeMaxBorder.Checked)
                                {
                                    //skip
                                }
                                else
                                {
                                    msg.Alert("指标逻辑错误", "一级品范围错误！");
                                    msg.Show();
                                    return false;
                                }
                            }
                            if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) > Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                if ((cbxAddPrimeIncludeMaxBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case ">":
                            if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) > Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                if ((cbxAddPrimeIncludeMaxBorder.Checked) && (cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return true;
                                }
                                else
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        case "<":
                            if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) > Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                return AlertPrimeValueOutOfRange();
                            }
                            else if (Convert.ToDecimal(txtAddPrimeMaxValue.Value) == Convert.ToDecimal(txtAddGoodMaxValue.Value))
                            {
                                if ((cbxAddPrimeIncludeMaxBorder.Checked) && (!cbxAddGoodIncludeMaxBorder.Checked))
                                {
                                    return AlertPrimeValueOutOfRange();
                                }
                            }
                            break;
                        default:
                            return true;
                    }
                    break;
                default:
                    return false;
            }
        }
        #endregion
        return true;
    }

    /// <summary>
    /// 加载新增指标中的选项
    /// </summary>
    private bool LoadAddItemAndFrequency()
    {
        string seriesId = "";
        EntityArrayList<BasMaterialMinorType> mTypeList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.ObjID == txtHiddenMaterialMinorTypeId.Value) && (BasMaterialMinorType._.DeleteFlag == "0"));
        if (mTypeList.Count > 0)
        {
            seriesId = mTypeList[0].ObjID.ToString();//获取当前的原材料系列Id
        }
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return false;
        }
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhereAndOrder((QmcCheckItem._.SeriesId == seriesId) && (QmcCheckItem._.DeleteFlag == "0") && (QmcCheckItem._.StandardId == standardId), QmcCheckItem._.ItemName.Asc);
        if (itemList.Count > 0)
        {
            foreach (QmcCheckItem item in itemList)
            {
                Ext.Net.ListItem listItem = new Ext.Net.ListItem();
                listItem.Text = item.ItemName;
                listItem.Value = item.ItemId.ToString();
                cbxAddItemName.Items.Add(listItem);
            }
            cbxAddItemName.ReRender();
            return true;
        }
        else
        {
            msg.Alert("操作", "此原材料系列在当前所选执行标准下无可用的检测项目！");
            msg.Show();
            return false;
        }
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
        string seriesId = txtHiddenMaterialMinorTypeId.Value.ToString();
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        //放开当前执行标准的删改权限
        //if (standard.ActivateFlag == "1")
        //{
        //    msg.Alert("操作", "所选标准已生效，不能执行此操作！");
        //    msg.Show();
        //    return;
        //}
        //else 
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能执行此操作！");
            msg.Show();
            return;
        }
        //获取检测项目
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhereAndOrder((QmcCheckItem._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString()) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"), QmcCheckItem._.ItemCode.Asc);
        //获取检测指标
        //Latest的才获取
        EntityArrayList<QmcCheckItemDetail> detailList = detailManager.GetListByWhereAndOrder((QmcCheckItemDetail._.MaterialCode == txtHiddenMaterialCode.Value.ToString()) && (QmcCheckItemDetail._.LatestFlag == "1") && (QmcCheckItemDetail._.DeleteFlag == "0"), QmcCheckItemDetail._.ItemDetailId.Asc);
        EntityArrayList<QmcCheckItemDetail> tempList = new EntityArrayList<QmcCheckItemDetail>();
        foreach (QmcCheckItem item in itemList)
        {
            #region 遍历指标
            if (detailList.Count > 0)
            {
                foreach (QmcCheckItemDetail detail in detailList)
                {
                    //若有指标则加载其指标
                    if (item.ItemId == detail.ItemId)
                    {
                        tempList.Add(detail);
                    }
                }
            }
            #endregion
        }
        if (tempList.Count > 0)
        {
            copyList.Clear();
            copySeriesId = seriesId;
            copyStandardId = standardId;
            foreach (QmcCheckItemDetail detail in tempList)
            {
                copyList.Add(detail);
            }
            msg.Alert("操作", "复制了" + copyList.Count + "条指标！");
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
            string standardId = String.Empty;
            if (cbxStandard.Value != null)
            {
                standardId = cbxStandard.Value.ToString();
            }
            else
            {
                msg.Alert("操作", "没有选择执行标准！");
                msg.Show();
                return;
            }
            QmcStandard standard = standardManager.GetById(standardId);
            //放开当前执行标准的删改权限
            //if (standard.ActivateFlag == "1")
            //{
            //    msg.Alert("操作", "所选标准已生效，不能执行此操作！");
            //    msg.Show();
            //    return;
            //}
            //else 
            if (standard.ActivateFlag == "2")
            {
                msg.Alert("操作", "所选标准已过期，不能执行此操作！");
                msg.Show();
                return;
            }
            //不能粘贴给自身
            if (copySeriesId != txtHiddenMaterialMinorTypeId.Value.ToString())
            {
                msg.Alert("操作", "不能粘贴到不同系列的原材料！");
                msg.Show();
                return;
            }
            if ((copyList[0].MaterialCode.ToString() == txtHiddenMaterialCode.Value.ToString()) && (copyStandardId == standardId))
            {
                msg.Alert("操作", "不能粘贴到复制源物料！");
                msg.Show();
                return;
            }
            if (copyStandardId != standardId)
            {
                msg.Alert("操作", "不能粘贴到不同执行标准！");
                msg.Show();
                return;
            }
            else
            {
                EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"));
                EntityArrayList<QmcCheckItemDetail> pasteFullList = detailManager.GetListByWhere(QmcCheckItemDetail._.MaterialCode == txtHiddenMaterialCode.Value.ToString() && QmcCheckItemDetail._.DeleteFlag == "0" && QmcCheckItemDetail._.LatestFlag == "1");
                EntityArrayList<QmcCheckItemDetail> pasteList = new EntityArrayList<QmcCheckItemDetail>();
                EntityArrayList<QmcCheckItemDetail> updateList = new EntityArrayList<QmcCheckItemDetail>();
                EntityArrayList<QmcCheckItemDetail> insertList = new EntityArrayList<QmcCheckItemDetail>();
                foreach (QmcCheckItemDetail token in pasteFullList)
                {
                    foreach (QmcCheckItem checkItem in itemList)
                    {
                        if (token.ItemId == checkItem.ItemId)
                        {
                            pasteList.Add(token);
                        }
                    }
                }
                int idPointer = 0;
                foreach (QmcCheckItemDetail detail in copyList)
                {
                    bool hasNative = false;
                    foreach (QmcCheckItemDetail pastedDetail in pasteList)
                    {
                        //建立要粘贴的数据
                        if ((detail.ItemId == pastedDetail.ItemId) && (detail.Frequency == pastedDetail.Frequency))
                        {
                            //原来粘贴对象的指标有数据的情况，更新数据
                            hasNative = true;
                            pastedDetail.MaterialCode = txtHiddenMaterialCode.Value.ToString();
                            pastedDetail.PrimeMaxValue = detail.PrimeMaxValue;
                            pastedDetail.PrimeMinValue = detail.PrimeMinValue;
                            pastedDetail.PrimeOperator = detail.PrimeOperator;
                            pastedDetail.PrimeTextValue = detail.PrimeTextValue;
                            pastedDetail.GoodMaxValue = detail.GoodMaxValue;
                            pastedDetail.GoodMinValue = detail.GoodMinValue;
                            pastedDetail.GoodOperator = detail.GoodOperator;
                            pastedDetail.GoodTextValue = detail.GoodTextValue;
                            pastedDetail.Frequency = detail.Frequency;
                            pastedDetail.CheckMethod = detail.CheckMethod;
                            pastedDetail.LastDate = detail.LastDate;
                            pastedDetail.Remark = detail.Remark;
                            pastedDetail.DeleteFlag = "0";
                            pastedDetail.LatestFlag = "1";
                            pastedDetail.ActivateFlag = detail.ActivateFlag;
                            pastedDetail.GoodIncludeMaxBorder = detail.GoodIncludeMaxBorder;
                            pastedDetail.GoodIncludeMinBorder = detail.GoodIncludeMinBorder;
                            pastedDetail.PrimeIncludeMaxBorder = detail.PrimeIncludeMaxBorder;
                            pastedDetail.PrimeIncludeMinBorder = detail.PrimeIncludeMinBorder;
                            pastedDetail.GoodDisplayValue = detail.GoodDisplayValue;
                            pastedDetail.PrimeDisplayValue = detail.PrimeDisplayValue;
                            pastedDetail.InputGoodMaxValue = detail.InputGoodMaxValue;
                            pastedDetail.InputGoodMinValue = detail.InputGoodMinValue;
                            pastedDetail.InputPrimeMaxValue = detail.InputPrimeMaxValue;
                            pastedDetail.InputPrimeMinValue = detail.InputPrimeMinValue;

                            pastedDetail.OrderID = detail.OrderID;
                            pastedDetail.TeXing = detail.TeXing;
                            pastedDetail.MinMaxValue = detail.MinMaxValue;
                            pastedDetail.MinMinValue = detail.MinMinValue;
                            pastedDetail.MinOperator = detail.MinOperator;
                            pastedDetail.MinTextValue = detail.MinTextValue;
                            pastedDetail.MinIncludeMaxBorder = detail.MinIncludeMaxBorder;
                            pastedDetail.MinIncludeMinBorder = detail.MinIncludeMinBorder;
                            pastedDetail.MinDisplayValue = detail.MinDisplayValue;
                            pastedDetail.InputMinMaxValue = detail.MinDisplayValue;
                            pastedDetail.InputMinMinValue = detail.InputMinMinValue;

                            pastedDetail.MaxMaxValue = detail.MaxMaxValue;
                            pastedDetail.MaxMinValue = detail.MaxMinValue;
                            pastedDetail.MaxOperator = detail.MaxOperator;
                            pastedDetail.MaxTextValue = detail.MaxTextValue;
                            pastedDetail.MaxIncludeMaxBorder = detail.MaxIncludeMaxBorder;
                            pastedDetail.MaxIncludeMinBorder = detail.MaxIncludeMinBorder;
                            pastedDetail.MaxDisplayValue = detail.MaxDisplayValue;
                            pastedDetail.InputMaxMaxValue = detail.InputMaxMaxValue;
                            pastedDetail.InputMaxMinValue = detail.InputMaxMinValue;



                            updateList.Add(pastedDetail);
                        }
                    }
                    if (!hasNative)
                    {
                        //原来粘贴对象的指标无数据的情况，新建数据
                        QmcCheckItemDetail createdDetail = new QmcCheckItemDetail();
                        createdDetail.ItemDetailId = Convert.ToInt32(detailManager.GetNextDetailId()) + idPointer;
                        idPointer++;
                        createdDetail.ItemId = detail.ItemId;
                        createdDetail.MaterialCode = txtHiddenMaterialCode.Value.ToString();
                        createdDetail.PrimeMaxValue = detail.PrimeMaxValue;
                        createdDetail.PrimeMinValue = detail.PrimeMinValue;
                        createdDetail.PrimeOperator = detail.PrimeOperator;
                        createdDetail.PrimeTextValue = detail.PrimeTextValue;
                        createdDetail.GoodMaxValue = detail.GoodMaxValue;
                        createdDetail.GoodMinValue = detail.GoodMinValue;
                        createdDetail.GoodOperator = detail.GoodOperator;
                        createdDetail.GoodTextValue = detail.GoodTextValue;
                        createdDetail.Frequency = detail.Frequency;
                        createdDetail.CheckMethod = detail.CheckMethod;
                        createdDetail.LastDate = detail.LastDate;
                        createdDetail.Remark = detail.Remark;
                        createdDetail.DeleteFlag = "0";
                        createdDetail.LatestFlag = "1";
                        createdDetail.Version = 1;
                        createdDetail.ActivateFlag = detail.ActivateFlag;
                        createdDetail.GoodIncludeMaxBorder = detail.GoodIncludeMaxBorder;
                        createdDetail.GoodIncludeMinBorder = detail.GoodIncludeMinBorder;
                        createdDetail.PrimeIncludeMaxBorder = detail.PrimeIncludeMaxBorder;
                        createdDetail.PrimeIncludeMinBorder = detail.PrimeIncludeMinBorder;
                        createdDetail.GoodDisplayValue = detail.GoodDisplayValue;
                        createdDetail.PrimeDisplayValue = detail.PrimeDisplayValue;
                        createdDetail.InputGoodMaxValue = detail.InputGoodMaxValue;
                        createdDetail.InputGoodMinValue = detail.InputGoodMinValue;
                        createdDetail.InputPrimeMaxValue = detail.InputPrimeMaxValue;
                        createdDetail.InputPrimeMinValue = detail.InputPrimeMinValue;

                        createdDetail.OrderID = detail.OrderID;
                        createdDetail.TeXing = detail.TeXing;
                        createdDetail.MinMaxValue = detail.MinMaxValue;
                        createdDetail.MinMinValue = detail.MinMinValue;
                        createdDetail.MinOperator = detail.MinOperator;
                        createdDetail.MinTextValue = detail.MinTextValue;
                        createdDetail.MinIncludeMaxBorder = detail.MinIncludeMaxBorder;
                        createdDetail.MinIncludeMinBorder = detail.MinIncludeMinBorder;
                        createdDetail.MinDisplayValue = detail.MinDisplayValue;
                        createdDetail.InputMinMaxValue = detail.MinDisplayValue;
                        createdDetail.InputMinMinValue = detail.InputMinMinValue;

                        createdDetail.MaxMaxValue = detail.MaxMaxValue;
                        createdDetail.MaxMinValue = detail.MaxMinValue;
                        createdDetail.MaxOperator = detail.MaxOperator;
                        createdDetail.MaxTextValue = detail.MaxTextValue;
                        createdDetail.MaxIncludeMaxBorder = detail.MaxIncludeMaxBorder;
                        createdDetail.MaxIncludeMinBorder = detail.MaxIncludeMinBorder;
                        createdDetail.MaxDisplayValue = detail.MaxDisplayValue;
                        createdDetail.InputMaxMaxValue = detail.InputMaxMaxValue;
                        createdDetail.InputMaxMinValue = detail.InputMaxMinValue;

                        insertList.Add(createdDetail);
                    }
                }
                //写入粘贴的数据
                try
                {
                    int pasteCount = 0;//粘贴成功计数
                    foreach (QmcCheckItemDetail detail in updateList)
                    {
                        detailManager.Update(detail);
                        pasteCount++;
                    }
                    foreach (QmcCheckItemDetail detail in insertList)
                    {
                        detailManager.Insert(detail);
                        pasteCount++;
                    }
                    this.AppendWebLog("检测指标粘贴", "指标数量：" + pasteCount);
                    pageToolBar2.DoRefresh();
                    this.detailSelectionModel.UpdateSelection();
                    msg.Alert("操作", "粘贴了" + pasteCount + "条指标！");
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
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        //获取检测项目
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhereAndOrder((QmcCheckItem._.SeriesId == txtHiddenMaterialMinorTypeId.Value.ToString()) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"), QmcCheckItem._.ItemCode.Asc);
        //获取检测指标
        //Latest的才获取
        EntityArrayList<QmcCheckItemDetail> detailList = detailManager.GetListByWhereAndOrder((QmcCheckItemDetail._.MaterialCode == txtHiddenMaterialCode.Value.ToString()) && (QmcCheckItemDetail._.LatestFlag == "1") && (QmcCheckItemDetail._.DeleteFlag == "0"), QmcCheckItemDetail._.ItemDetailId.Asc);
        List<DetailViewUnit> unitList = new List<DetailViewUnit>();//指标显示单元
        foreach (QmcCheckItem item in itemList)
        {
            #region 遍历指标
            QmcCheckItemDetail tempDetail = null;
            if (detailList.Count > 0)
            {
                foreach (QmcCheckItemDetail detail in detailList)
                {
                    //若有指标则加载其指标
                    if (item.ItemId == detail.ItemId)
                    {
                        tempDetail = detail;
                        //已无必要
                        //switch (item.ValueType)
                        //{
                        //    case "文字":
                        //        break;
                        //    case "数字":
                        //        detail.PrimeTextValue = detail.PrimeMinValue + detail.PrimeOperator + detail.PrimeMaxValue;
                        //        detail.GoodTextValue = detail.GoodMinValue + detail.GoodOperator + detail.GoodMaxValue;
                        //        break;
                        //    default:
                        //        break;
                        //}
                    }
                }
                if (tempDetail != null)
                {
                    //填充指标显示单元
                    DetailViewUnit unitC = new DetailViewUnit();
                    unitC.ItemId = tempDetail.ItemId.ToString();
                    unitC.DetailId = tempDetail.ItemDetailId.ToString();
                    unitC.MaterialName = (string)txtHiddenMaterialName.Value;
                    unitC.ItemName = item.ItemName;
                    //unitC.GoodTextValue = tempDetail.GoodTextValue;
                    //unitC.PrimeTextValue = tempDetail.PrimeTextValue;
                    unitC.GoodTextValue = tempDetail.GoodDisplayValue;
                    unitC.PrimeTextValue = tempDetail.PrimeDisplayValue;
                    unitC.Frequency = tempDetail.Frequency;
                    unitC.CheckMethod = tempDetail.CheckMethod;
                    unitC.Version = tempDetail.Version.ToString();
                    unitC.Remark = tempDetail.Remark;
                    unitList.Add(unitC);
                }
            }
            #endregion
        }
        //建立导出模板
        if (unitList.Count > 0)
        {
            unitList.Reverse();
            DataTable dt = new DataTable("viewUnit");
            DataColumn dc1 = new DataColumn("检测项目", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("检测频次", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("原材料型号", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("一级品指标", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("合格品指标", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("版本", Type.GetType("System.String"));
            DataColumn dc7 = new DataColumn("检测方法", Type.GetType("System.String"));
            DataColumn dc8 = new DataColumn("备注", Type.GetType("System.String"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            //填充导出模板
            foreach (DetailViewUnit unit in unitList)
            {
                DataRow dr = dt.NewRow();
                dr["检测项目"] = unit.ItemName;
                dr["检测频次"] = unit.Frequency;
                dr["原材料型号"] = unit.MaterialName;
                dr["一级品指标"] = unit.PrimeTextValue;
                dr["合格品指标"] = unit.GoodTextValue;
                dr["版本"] = unit.Version;
                dr["检测方法"] = unit.CheckMethod;
                dr["备注"] = unit.Remark;
                dt.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "检测指标报表-" + standard.StandardName + "-" + txtHiddenMaterialName.Text);//生成Excel文件下载
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击添加检测指标项按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_adddetail_Click(object sender, EventArgs e)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        //if (standard.ActivateFlag == "1")
        //{
        //    msg.Alert("操作", "所选标准已生效，不能执行此操作！");
        //    msg.Show();
        //    return;
        //}
        //else 
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能执行此操作！");
            msg.Show();
            return;
        }
        //初始化添加窗口
        pnlAddDetail.SetActiveTab(1);
        cbxAddActivated.Checked = true;
        cbxAddItemName.Items.Clear();
        cbxAddItemName.Text = "";
        cbxAddItemName.Value = "";
        txtAddCheckMethod.Value = "";
        txtAddRemark.Value = "";
        txtAddPrimeTextValue.Disable();
        txtAddPrimeMinValue.Disable();
        txtAddPrimeMaxValue.Disable();
        cbxAddPrimeOperator.Disable();
        txtAddPrimeTextValue.SetValue("");
        txtAddPrimeMinValue.SetValue("");
        txtAddPrimeMaxValue.SetValue("");
        cbxAddPrimeOperator.SetValue("");
        cbxAddPrimeIncludeMinBorder.Disable();
        cbxAddPrimeIncludeMaxBorder.Disable();
        cbxAddPrimeIncludeMinBorder.Checked = true;
        cbxAddPrimeIncludeMaxBorder.Checked = true;
        txtAddGoodTextValue.Disable();
        txtAddGoodMinValue.Disable();
        txtAddGoodMaxValue.Disable();
        cbxAddGoodOperator.Disable();
        txtAddGoodTextValue.SetValue("");
        txtAddGoodMinValue.SetValue("");
        txtAddGoodMaxValue.SetValue("");
        cbxAddGoodOperator.SetValue("");
        TextOrderID.SetValue("");
        TextTexing.SetValue("");
        cbxAddGoodIncludeMinBorder.Disable();
        cbxAddGoodIncludeMaxBorder.Disable();
        cbxAddGoodIncludeMinBorder.Checked = true;
        cbxAddGoodIncludeMaxBorder.Checked = true;

#region
        txtAddMaxTextValue.Disable();
        txtAddMaxMinValue.Disable();
        txtAddMaxMaxValue.Disable();
        cbxAddMaxOperator.Disable();
        txtAddMaxTextValue.SetValue("");
        txtAddMaxMinValue.SetValue("");
        txtAddMaxMaxValue.SetValue("");
        cbxAddMaxOperator.SetValue("");
        cbxAddMaxIncludeMinBorder.Disable();
        cbxAddMaxIncludeMaxBorder.Disable();
        cbxAddMaxIncludeMinBorder.Checked = true;
        cbxAddMaxIncludeMaxBorder.Checked = true;
        txtAddMinTextValue.Disable();
        txtAddMinMinValue.Disable();
        txtAddMinMaxValue.Disable();
        cbxAddMinOperator.Disable();
        txtAddMinTextValue.SetValue("");
        txtAddMinMinValue.SetValue("");
        txtAddMinMaxValue.SetValue("");
        cbxAddMinOperator.SetValue("");
        cbxAddMinIncludeMinBorder.Disable();
        cbxAddMinIncludeMaxBorder.Disable();
        cbxAddMinIncludeMinBorder.Checked = true;
        cbxAddMinIncludeMaxBorder.Checked = true;
#endregion
        btnAddDetailSave.Disable();
        txtAddMaterialName.Value = txtHiddenMaterialName.Value;
        if (txtHiddenMaterialCode.Value == null)
        {
            msg.Alert("错误", "请先选择一种原材料！");
            msg.Show();
            return;
        }
        if (LoadAddItemAndFrequency())
        {
            this.windowAddDetail.Show();
        }
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string detailId)
    {



        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能执行此操作！");
            msg.Show();
            return;
        }
        //初始化修改窗口
        pnlEditDetail.SetActiveTab(1);
        cbxModifyActivated.Checked = true;
        txtModifyPrimeTextValue.Disable();
        txtModifyPrimeMinValue.Disable();
        txtModifyPrimeMaxValue.Disable();
        cbxModifyPrimeOperator.Disable();
        cbxModifyPrimeIncludeMinBorder.Disable();
        cbxModifyPrimeIncludeMaxBorder.Disable();
        cbxModifyPrimeIncludeMinBorder.Checked = false;
        cbxModifyPrimeIncludeMaxBorder.Checked = false;
        txtModifyGoodTextValue.Disable();
        txtModifyGoodMinValue.Disable();
        txtModifyGoodMaxValue.Disable();
        cbxModifyGoodOperator.Disable();
        cbxModifyGoodIncludeMinBorder.Disable();
        cbxModifyGoodIncludeMaxBorder.Disable();
        cbxModifyGoodIncludeMinBorder.Checked = false;
        cbxModifyGoodIncludeMaxBorder.Checked = false;

        #region 最大最小
        txtModifyMinTextValue.Disable();
        txtModifyMinMinValue.Disable();
        txtModifyMinMaxValue.Disable();
        cbxModifyMinOperator.Disable();
        cbxModifyMinIncludeMinBorder.Disable();
        cbxModifyMinIncludeMaxBorder.Disable();
        cbxModifyMinIncludeMinBorder.Checked = false;
        cbxModifyMinIncludeMaxBorder.Checked = false;
        txtModifyMaxTextValue.Disable();
        txtModifyMaxMinValue.Disable();
        txtModifyMaxMaxValue.Disable();
        cbxModifyMaxOperator.Disable();
        cbxModifyMaxIncludeMinBorder.Disable();
        cbxModifyMaxIncludeMaxBorder.Disable();
        cbxModifyMaxIncludeMinBorder.Checked = false;
        cbxModifyMaxIncludeMaxBorder.Checked = false;
        #endregion
        QmcCheckItemDetail detail = detailManager.GetListByWhere(QmcCheckItemDetail._.ItemDetailId == detailId)[0];
        QmcCheckItem item = itemManager.GetById(detail.ItemId);
        //msg.Alert("操作", detailId + "a" + detail.ItemDetailId+"b" + item.ItemId);
        //msg.Show();
        //return;
        txtModifyDetailId.Value = detail.ItemDetailId;
        txtModifyItemId.Value = item.ItemId;
        txtModifyMaterialName.Value = txtHiddenMaterialName.Value;
        txtModifyItemName.Value = item.ItemName;
        cbxModifyFrequency.Value = detail.Frequency;
        txtModifyRemark.Value = detail.Remark;
        txtModifyCheckMethod.Value = detail.CheckMethod;
        txtModifyOrderID.Text = detail.OrderID.ToString();
        txtModifyTexing.Text = detail.TeXing;
        if (detail.PrimeIncludeMaxBorder == "1")
        {
            cbxModifyPrimeIncludeMaxBorder.Checked = true;
        }
        if (detail.PrimeIncludeMinBorder == "1")
        {
            cbxModifyPrimeIncludeMinBorder.Checked = true;
        }
        if (detail.GoodIncludeMaxBorder == "1")
        {
            cbxModifyGoodIncludeMaxBorder.Checked = true;
        }
        if (detail.GoodIncludeMinBorder == "1")
        {
            cbxModifyGoodIncludeMinBorder.Checked = true;
        }


        if (detail.MinIncludeMaxBorder == "1")
        {
            cbxModifyMinIncludeMaxBorder.Checked = true;
        }
        if (detail.MinIncludeMinBorder == "1")
        {
            cbxModifyMinIncludeMinBorder.Checked = true;
        }

        if (detail.MaxIncludeMaxBorder == "1")
        {
            cbxModifyMaxIncludeMaxBorder.Checked = true;
        }
        if (detail.MaxIncludeMinBorder == "1")
        {
            cbxModifyMaxIncludeMinBorder.Checked = true;
        }
        #region 加载指标
        switch (item.ValueType)
        {
            case "文字":
                txtModifyPrimeTextValue.Enable();
                txtModifyPrimeTextValue.Value = detail.PrimeTextValue;
                txtModifyPrimeMinValue.Value = null;
                txtModifyPrimeMaxValue.Value = null;
                txtModifyGoodTextValue.Enable();
                txtModifyGoodTextValue.Value = detail.GoodTextValue;
                txtModifyGoodMinValue.Value = null;
                txtModifyGoodMaxValue.Value = null;


#region 最大最小
                txtModifyMinTextValue.Enable();
                txtModifyMinTextValue.Value = detail.MinTextValue;
                txtModifyMinMinValue.Value = null;
                txtModifyMinMaxValue.Value = null;
                txtModifyMaxTextValue.Enable();
                txtModifyMaxTextValue.Value = detail.MaxTextValue;
                txtModifyMaxMinValue.Value = null;
                txtModifyMaxMaxValue.Value = null;
#endregion
                break;
            case "数字":
                cbxModifyPrimeOperator.Enable();
                txtModifyPrimeMinValue.Value = detail.InputPrimeMinValue;
                txtModifyPrimeMaxValue.Value = detail.InputPrimeMaxValue;
                cbxModifyPrimeOperator.Value = detail.PrimeOperator;
                txtModifyPrimeTextValue.Value = null;
                if (detail.PrimeOperator != null)
                {
                    switch (detail.PrimeOperator.ToString())
                    {
                        case "－":
                            txtModifyPrimeMinValue.Enable();
                            txtModifyPrimeMaxValue.Enable();
                            cbxModifyPrimeIncludeMinBorder.Enable();
                            cbxModifyPrimeIncludeMaxBorder.Enable();
                            break;
                        case ">":
                            txtModifyPrimeMinValue.Disable();
                            cbxModifyPrimeIncludeMinBorder.Disable();
                            txtModifyPrimeMaxValue.Enable();
                            cbxModifyPrimeIncludeMaxBorder.Enable();
                            break;
                        case "<":
                            txtModifyPrimeMinValue.Disable();
                            cbxModifyPrimeIncludeMinBorder.Disable();
                            txtModifyPrimeMaxValue.Enable();
                            cbxModifyPrimeIncludeMaxBorder.Enable();
                            break;
                        default:
                             txtModifyPrimeMinValue.Enable();
                            txtModifyPrimeMaxValue.Enable();
                            cbxModifyPrimeIncludeMinBorder.Enable();
                            cbxModifyPrimeIncludeMaxBorder.Enable();
                            break;
                    }
                }
                cbxModifyGoodOperator.Enable();
                txtModifyGoodMinValue.Value = detail.InputGoodMinValue;
                txtModifyGoodMaxValue.Text = detail.InputGoodMaxValue;

                cbxModifyGoodOperator.Value = detail.GoodOperator;
                //txtModifyGoodMaxValue.Text = detail.ItemId.ToString();
                //txtModifyGoodMaxValue.Text = detail.GoodOperator;
                txtModifyGoodTextValue.Value = "";
                if (detail.GoodOperator != null)
                {
                    switch (detail.GoodOperator.ToString())
                    {
                        case "－":
                            txtModifyGoodMinValue.Enable();
                            cbxModifyGoodIncludeMinBorder.Enable();
                            txtModifyGoodMaxValue.Enable();
                            cbxModifyGoodIncludeMaxBorder.Enable();
                            break;
                        case ">":
                            txtModifyGoodMinValue.Disable();
                            cbxModifyGoodIncludeMinBorder.Disable();
                            txtModifyGoodMaxValue.Enable();
                            cbxModifyGoodIncludeMaxBorder.Enable();
                            break;
                        case "<":
                            txtModifyGoodMinValue.Disable();
                            cbxModifyGoodIncludeMinBorder.Disable();
                            txtModifyGoodMaxValue.Enable();
                            cbxModifyGoodIncludeMaxBorder.Enable();
                            break;
                        default:
                             txtModifyGoodMinValue.Enable();
                            cbxModifyGoodIncludeMinBorder.Enable();
                            txtModifyGoodMaxValue.Enable();
                            cbxModifyGoodIncludeMaxBorder.Enable();
                            break;
                    }
                }


                #region 最大最小
                         cbxModifyMinOperator.Enable();
                txtModifyMinMinValue.Value = detail.InputMinMinValue;
                txtModifyMinMaxValue.Text = detail.InputMinMaxValue;

                cbxModifyMinOperator.Value = detail.MinOperator;
                //txtModifyMinMaxValue.Text = detail.ItemId.ToString();
                //txtModifyMinMaxValue.Text = detail.MinOperator;
                txtModifyMinTextValue.Value = "";
                if (detail.MinOperator != null)
                {
                    switch (detail.MinOperator.ToString())
                    {
                        case "－":
                            txtModifyMinMinValue.Enable();
                            cbxModifyMinIncludeMinBorder.Enable();
                            txtModifyMinMaxValue.Enable();
                            cbxModifyMinIncludeMaxBorder.Enable();
                            break;
                        case ">":
                            txtModifyMinMinValue.Disable();
                            cbxModifyMinIncludeMinBorder.Disable();
                            txtModifyMinMaxValue.Enable();
                            cbxModifyMinIncludeMaxBorder.Enable();
                            break;
                        case "<":
                            txtModifyMinMinValue.Disable();
                            cbxModifyMinIncludeMinBorder.Disable();
                            txtModifyMinMaxValue.Enable();
                            cbxModifyMinIncludeMaxBorder.Enable();
                            break;
                        default:
                             txtModifyMinMinValue.Enable();
                            cbxModifyMinIncludeMinBorder.Enable();
                            txtModifyMinMaxValue.Enable();
                            cbxModifyMinIncludeMaxBorder.Enable();
                            break;
                    }
                }
         cbxModifyMaxOperator.Enable();
                txtModifyMaxMinValue.Value = detail.InputMaxMinValue;
                txtModifyMaxMaxValue.Text = detail.InputMaxMaxValue;

                cbxModifyMaxOperator.Value = detail.MaxOperator;
                //txtModifyMaxMaxValue.Text = detail.ItemId.ToString();
                //txtModifyMaxMaxValue.Text = detail.MaxOperator;
                txtModifyMaxTextValue.Value = "";
                if (detail.MaxOperator != null)
                {
                    switch (detail.MaxOperator.ToString())
                    {
                        case "－":
                            txtModifyMaxMinValue.Enable();
                            cbxModifyMaxIncludeMinBorder.Enable();
                            txtModifyMaxMaxValue.Enable();
                            cbxModifyMaxIncludeMaxBorder.Enable();
                            break;
                        case ">":
                            txtModifyMaxMinValue.Disable();
                            cbxModifyMaxIncludeMinBorder.Disable();
                            txtModifyMaxMaxValue.Enable();
                            cbxModifyMaxIncludeMaxBorder.Enable();
                            break;
                        case "<":
                            txtModifyMaxMinValue.Disable();
                            cbxModifyMaxIncludeMinBorder.Disable();
                            txtModifyMaxMaxValue.Enable();
                            cbxModifyMaxIncludeMaxBorder.Enable();
                            break;
                        default:
                             txtModifyMaxMinValue.Enable();
                            cbxModifyMaxIncludeMinBorder.Enable();
                            txtModifyMaxMaxValue.Enable();
                            cbxModifyMaxIncludeMaxBorder.Enable();
                            break;
                    }
                }




                #endregion
                break;
            default:
                break;
        }
        #endregion
        //cbxModifyGoodOperator.Value = "P";
        //txtModifyGoodMaxValue.Text = "11";
        this.windowModifyDetail.Show();
    }

    /// <summary>
    /// 点击添加项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAddDetailSave_Click(object sender, EventArgs e)
    {
        #region 校验重复
        EntityArrayList<QmcCheckItemDetail> duplicatedList = detailManager.GetListByWhere((QmcCheckItemDetail._.ItemId == cbxAddItemName.Value) && (QmcCheckItemDetail._.MaterialCode == txtHiddenMaterialCode.Value) && (QmcCheckItemDetail._.DeleteFlag == "0"));
        if (duplicatedList.Count > 0)
        {
            msg.Alert("操作", "所选检测项目的指标已存在，请进行修改！");
            msg.Show();
            return;
        }
        #endregion

        #region 校验输入数据合法性
        QmcCheckItem originItem = itemManager.GetById(cbxAddItemName.Value.ToString());
        if (originItem.ValueType == "文字")
        {
            if ((txtAddGoodTextValue.Text == "") || (txtAddGoodTextValue.Text == null) || (txtAddGoodTextValue.Text == String.Empty))
            {
                msg.Alert("操作", "合格品文字标准不能为空！");
                msg.Show();
                return;
            }
        }
        if (originItem.ValueType == "数字")
        {
            if (!ValidateAddIntegration())
            {
                return;
            }
        }
        #endregion

        #region 校验输入逻辑正确性
        if (originItem.ValueType == "数字")
        {
            if (!ValidateAddLogic())
            {
                return;
            }
        }
        #endregion

       
        try
        {
            QmcCheckItemDetail item = new QmcCheckItemDetail();
            item.ItemId = Convert.ToInt32(cbxAddItemName.Value);
            if (cbxAddActivated.Checked)
            {
                item.ActivateFlag = "1";
            }
            else
            {
                item.ActivateFlag = "0";
            }
            item.DeleteFlag = "0";
            item.LatestFlag = "1";
            item.Version = 1;
            item.LastDate = DateTime.Now;
            item.CheckMethod = (string)txtAddCheckMethod.Value;
            item.Frequency = (string)cbxAddFrequency.Value;
            item.ItemDetailId = Convert.ToInt32(detailManager.GetNextDetailId());
            item.MaterialCode = txtHiddenMaterialCode.Text.ToString();
            try
            {
                item.OrderID = Convert.ToInt32(TextOrderID.Text);
            }
            catch (Exception e2)
            {

            }
          
            item.TeXing = TextTexing.Text;
            if (originItem.ValueType == "文字")
            {
                item.GoodTextValue = (string)txtAddGoodTextValue.Value;
                item.PrimeTextValue = (string)txtAddPrimeTextValue.Value;
                item.GoodDisplayValue = (string)txtAddGoodTextValue.Value;
                item.PrimeDisplayValue = (string)txtAddPrimeTextValue.Value;

                #region 最大最小
                item.MinTextValue = (string)txtAddMinTextValue.Value;
                item.MinDisplayValue = (string)txtAddMinTextValue.Value;
                item.MaxTextValue = (string)txtAddMaxTextValue.Value;
                item.MaxDisplayValue = (string)txtAddMaxTextValue.Value;
                #endregion
            }
            if (originItem.ValueType == "数字")
            {
                item.GoodMaxValue = Convert.ToDecimal(txtAddGoodMaxValue.Value);
                item.InputGoodMaxValue = (string)txtAddGoodMaxValue.Value;
                if (txtAddGoodMinValue.Value != null)
                {
                    item.GoodMinValue = Convert.ToDecimal(txtAddGoodMinValue.Value);
                    item.InputGoodMinValue = (string)txtAddGoodMinValue.Value;
                }
                else
                {
                    item.GoodMinValue = null;
                    item.InputGoodMinValue = null;
                }
                item.GoodOperator = (string)cbxAddGoodOperator.Value;


                #region 最大最小
                item.MinMaxValue = Convert.ToDecimal(txtAddMinMaxValue.Value);
                item.InputMinMaxValue = (string)txtAddMinMaxValue.Value;
                if (txtAddMinMinValue.Value != null)
                {
                    item.MinMinValue = Convert.ToDecimal(txtAddMinMinValue.Value);
                    item.InputMinMinValue = (string)txtAddMinMinValue.Value;
                }
                else
                {
                    item.MinMinValue = null;
                    item.InputMinMinValue = null;
                }
                item.MinOperator = (string)cbxAddMinOperator.Value;

                item.MaxMaxValue = Convert.ToDecimal(txtAddMaxMaxValue.Value);
                item.InputMaxMaxValue = (string)txtAddMaxMaxValue.Value;
                if (txtAddMaxMinValue.Value != null)
                {
                    item.MaxMinValue = Convert.ToDecimal(txtAddMaxMinValue.Value);
                    item.InputMaxMinValue = (string)txtAddMaxMinValue.Value;
                }
                else
                {
                    item.MaxMinValue = null;
                    item.InputMaxMinValue = null;
                }
                item.MaxOperator = (string)cbxAddMaxOperator.Value;
                #endregion
                if (cbxAddPrimeOperator.Value == null)
                {
                    item.PrimeMaxValue = null;
                    item.PrimeMinValue = null;
                    item.PrimeOperator = null;
                }
                else if ((cbxAddPrimeOperator.Value.ToString() == "无") || (cbxAddPrimeOperator.Value.ToString() == ""))
                {
                    item.PrimeMaxValue = null;
                    item.PrimeMinValue = null;
                    item.PrimeOperator = null;
                }
                else
                {
                    item.PrimeMaxValue = Convert.ToDecimal(txtAddPrimeMaxValue.Value);
                    item.InputPrimeMaxValue = (string)txtAddPrimeMaxValue.Value;
                    if (txtAddPrimeMinValue.Value != null)
                    {
                        item.PrimeMinValue = Convert.ToDecimal(txtAddPrimeMinValue.Value);
                        item.InputPrimeMinValue = (string)txtAddPrimeMinValue.Value;
                    }
                    else
                    {
                        item.PrimeMinValue = null;
                    }
                    item.PrimeOperator = Convert.ToString(cbxAddPrimeOperator.Value);
                }
            }
            if (cbxAddGoodIncludeMaxBorder.Checked && cbxAddGoodIncludeMaxBorder.Enabled)
            {
                item.GoodIncludeMaxBorder = "1";
            }
            else if (cbxAddGoodIncludeMaxBorder.Disabled)
            {
                item.GoodIncludeMaxBorder = null;
            }
            else
            {
                item.GoodIncludeMaxBorder = "0";
            }

            if (cbxAddGoodIncludeMinBorder.Checked && cbxAddGoodIncludeMinBorder.Enabled)
            {
                item.GoodIncludeMinBorder = "1";
            }
            else if (cbxAddGoodIncludeMinBorder.Disabled)
            {
                item.GoodIncludeMinBorder = null;
            }
            else
            {
                item.GoodIncludeMinBorder = "0";
            }


            #region 最大最小
            if (cbxAddMinIncludeMaxBorder.Checked && cbxAddMinIncludeMaxBorder.Enabled)
            {
                item.MinIncludeMaxBorder = "1";
            }
            else if (cbxAddMinIncludeMaxBorder.Disabled)
            {
                item.MinIncludeMaxBorder = null;
            }
            else
            {
                item.MinIncludeMaxBorder = "0";
            }

            if (cbxAddMinIncludeMinBorder.Checked && cbxAddMinIncludeMinBorder.Enabled)
            {
                item.MinIncludeMinBorder = "1";
            }
            else if (cbxAddMinIncludeMinBorder.Disabled)
            {
                item.MinIncludeMinBorder = null;
            }
            else
            {
                item.MinIncludeMinBorder = "0";
            }
            if (cbxAddMaxIncludeMaxBorder.Checked && cbxAddMaxIncludeMaxBorder.Enabled)
            {
                item.MaxIncludeMaxBorder = "1";
            }
            else if (cbxAddMaxIncludeMaxBorder.Disabled)
            {
                item.MaxIncludeMaxBorder = null;
            }
            else
            {
                item.MaxIncludeMaxBorder = "0";
            }

            if (cbxAddMaxIncludeMinBorder.Checked && cbxAddMaxIncludeMinBorder.Enabled)
            {
                item.MaxIncludeMinBorder = "1";
            }
            else if (cbxAddMaxIncludeMinBorder.Disabled)
            {
                item.MaxIncludeMinBorder = null;
            }
            else
            {
                item.MaxIncludeMinBorder = "0";
            }
            #endregion


            if (cbxAddPrimeIncludeMaxBorder.Checked && cbxAddPrimeIncludeMaxBorder.Enabled)
            {
                item.PrimeIncludeMaxBorder = "1";
            }
            else if (cbxAddPrimeIncludeMaxBorder.Disabled)
            {
                item.PrimeIncludeMaxBorder = null;
            }
            else
            {
                item.PrimeIncludeMaxBorder = "0";
            }

            if (cbxAddPrimeIncludeMinBorder.Checked && cbxAddPrimeIncludeMinBorder.Enabled)
            {
                item.PrimeIncludeMinBorder = "1";
            }
            else if (cbxAddPrimeIncludeMinBorder.Disabled)
            {
                item.PrimeIncludeMinBorder = null;
            }
            else
            {
                item.PrimeIncludeMinBorder = "0";
            }

            #region 写入数字显示值
            if (originItem.ValueType == "数字")
            {
                //合格品
                if (item.GoodOperator == "－")
                {
                    item.GoodDisplayValue = item.InputGoodMinValue + item.GoodOperator + item.InputGoodMaxValue;
                }
                if (item.GoodOperator == "P")
                {
                    item.GoodDisplayValue = item.InputGoodMaxValue;
                }
                if (item.GoodOperator == ">" || item.GoodOperator == "I")
                {
                    if (item.GoodIncludeMaxBorder == "1")
                    {
                        item.GoodDisplayValue = "≥" + item.InputGoodMaxValue;
                    }
                    else
                    {
                        item.GoodDisplayValue = item.GoodOperator + item.InputGoodMaxValue;
                    }
                }
                if (item.GoodOperator == "<" || item.GoodOperator == "A")
                {
                    if (item.GoodIncludeMaxBorder == "1")
                    {
                        item.GoodDisplayValue = "≤" + item.InputGoodMaxValue;
                    }
                    else
                    {
                        item.GoodDisplayValue = item.GoodOperator + item.InputGoodMaxValue;
                    }
                }
                if (item.GoodOperator == "－")
                {
                    item.GoodDisplayValue = item.InputGoodMinValue + item.GoodOperator + item.InputGoodMaxValue;
                }

                #region 最大最小
                if (item.MinOperator == "－")
                {
                    item.MinDisplayValue = item.InputMinMinValue + item.MinOperator + item.InputMinMaxValue;
                }
                if (item.MinOperator == "P")
                {
                    item.MinDisplayValue = item.InputMinMaxValue;
                }
                if (item.MinOperator == ">" || item.MinOperator == "I")
                {
                    if (item.MinIncludeMaxBorder == "1")
                    {
                        item.MinDisplayValue = "≥" + item.InputMinMaxValue;
                    }
                    else
                    {
                        item.MinDisplayValue = item.MinOperator + item.InputMinMaxValue;
                    }
                }
                if (item.MinOperator == "<" || item.MinOperator == "A")
                {
                    if (item.MinIncludeMaxBorder == "1")
                    {
                        item.MinDisplayValue = "≤" + item.InputMinMaxValue;
                    }
                    else
                    {
                        item.MinDisplayValue = item.MinOperator + item.InputMinMaxValue;
                    }
                }
                if (item.MinOperator == "－")
                {
                    item.MinDisplayValue = item.InputMinMinValue + item.MinOperator + item.InputMinMaxValue;
                }
                if (item.MaxOperator == "－")
                {
                    item.MaxDisplayValue = item.InputMaxMinValue + item.MaxOperator + item.InputMaxMaxValue;
                }
                if (item.MaxOperator == "P")
                {
                    item.MaxDisplayValue = item.InputMaxMaxValue;
                }
                if (item.MaxOperator == ">" || item.MaxOperator == "I")
                {
                    if (item.MaxIncludeMaxBorder == "1")
                    {
                        item.MaxDisplayValue = "≥" + item.InputMaxMaxValue;
                    }
                    else
                    {
                        item.MaxDisplayValue = item.MaxOperator + item.InputMaxMaxValue;
                    }
                }
                if (item.MaxOperator == "<" || item.MaxOperator == "A")
                {
                    if (item.MaxIncludeMaxBorder == "1")
                    {
                        item.MaxDisplayValue = "≤" + item.InputMaxMaxValue;
                    }
                    else
                    {
                        item.MaxDisplayValue = item.MaxOperator + item.InputMaxMaxValue;
                    }
                }
                if (item.MaxOperator == "－")
                {
                    item.MaxDisplayValue = item.InputMaxMinValue + item.MaxOperator + item.InputMaxMaxValue;
                }
                #endregion
                //一级品
                if (item.PrimeOperator == "－")
                {
                    item.PrimeDisplayValue = item.InputPrimeMinValue + item.PrimeOperator + item.InputPrimeMaxValue;
                }
                if (item.PrimeOperator == "P")
                {
                    item.PrimeDisplayValue = item.InputPrimeMaxValue;
                }
                if (item.PrimeOperator == ">" || item.PrimeOperator == "I")
                {
                    if (item.PrimeIncludeMaxBorder == "1")
                    {
                        item.PrimeDisplayValue = "≥" + item.InputPrimeMaxValue;
                    }
                    else
                    {
                        item.PrimeDisplayValue = item.PrimeOperator + item.InputPrimeMaxValue;
                    }
                }
                if (item.PrimeOperator == "<" || item.PrimeOperator == "A")
                {
                    if (item.PrimeIncludeMaxBorder == "1")
                    {
                        item.PrimeDisplayValue = "≤" + item.InputPrimeMaxValue;
                    }
                    else
                    {
                        item.PrimeDisplayValue = item.PrimeOperator + item.InputPrimeMaxValue;
                    }
                }
            }
            #endregion

            item.Remark = (string)txtAddRemark.Value;
            detailManager.Insert(item);
            this.AppendWebLog("检测指标新增", "项目编号：" + txtAddItemId.Text);
            pageToolBar2.DoRefresh();
            this.windowAddDetail.Close();
            msg.Alert("操作", "新增成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "新增失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改项目中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifyDetailSave_Click(object sender, EventArgs e)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能执行此操作！");
            msg.Show();
            return;
        }
        #region 校验输入数据合法性
        QmcCheckItem originItem = itemManager.GetById(txtModifyItemId.Value.ToString());
        if (originItem.ValueType == "文字")
        {
            if ((txtModifyGoodTextValue.Text == "") || (txtModifyGoodTextValue.Text == null) || (txtModifyGoodTextValue.Text == String.Empty))
            {
                msg.Alert("操作", "合格品文字标准不能为空！");
                msg.Show();
                return;
            }
        }
        if (originItem.ValueType == "数字")
        {
            if (!ValidateModifyIntegration())
            {
                return;
            }
        }
        #endregion

        #region 校验输入逻辑正确性
        if (originItem.ValueType == "数字")
        {
            if (!ValidateModifyLogic())
            {
                return;
            }
        }
        #endregion
        try
        {
            int i = Convert.ToInt32(txtModifyOrderID.Text);
        }
        catch (Exception e2)
        {
            msg.Alert("操作", "请输入合法的项目序号！");
            msg.Show();
            return;
        }
        try
        {
            QmcCheckItemDetail oldItem = detailManager.GetById(txtModifyDetailId.Text);
            QmcCheckItemDetail item = new QmcCheckItemDetail();

            #region 执行标准已启用则新建版本
            if (standard.ActivateFlag == "1")
            {
                item.ItemId = oldItem.ItemId;
                if (cbxModifyActivated.Checked)
                {
                    item.ActivateFlag = "1";
                }
                else
                {
                    item.ActivateFlag = "0";
                }
                item.CheckMethod = (string)txtModifyCheckMethod.Value;
                item.Frequency = (string)cbxModifyFrequency.Value;
                item.ItemDetailId = Convert.ToInt32(detailManager.GetNextDetailId());
                item.MaterialCode = txtHiddenMaterialCode.Text.ToString();
                item.OrderID = Convert.ToInt32(txtModifyOrderID.Text);
                item.TeXing = txtModifyTexing.Text;
                if (originItem.ValueType == "文字")
                {
                    item.GoodTextValue = (string)txtModifyGoodTextValue.Value;
                    item.PrimeTextValue = (string)txtModifyPrimeTextValue.Value;
                    item.GoodDisplayValue = (string)txtModifyGoodTextValue.Value;
                    item.PrimeDisplayValue = (string)txtModifyPrimeTextValue.Value;

                    item.MinTextValue = (string)txtModifyMinTextValue.Value;
                    item.MinDisplayValue = (string)txtModifyMinTextValue.Value;
                    item.MaxTextValue = (string)txtModifyMaxTextValue.Value;
                    item.MaxDisplayValue = (string)txtModifyMaxTextValue.Value;
                    
                }
                if (originItem.ValueType == "数字")
                {
                    item.GoodMaxValue = Convert.ToDecimal(txtModifyGoodMaxValue.Value);
                    item.InputGoodMaxValue = (string)txtModifyGoodMaxValue.Value;

                    item.MinMaxValue = Convert.ToDecimal(txtModifyMinMaxValue.Value);
                    item.InputMinMaxValue = (string)txtModifyMinMaxValue.Value;
                    item.MaxMaxValue = Convert.ToDecimal(txtModifyMaxMaxValue.Value);
                    item.InputMaxMaxValue = (string)txtModifyMaxMaxValue.Value;

                    if (txtModifyGoodMinValue.Value != null)
                    {
                        item.GoodMinValue = Convert.ToDecimal(txtModifyGoodMinValue.Value);
                        item.InputGoodMinValue = (string)txtModifyGoodMinValue.Value;
                    }
                    else
                    {
                        item.GoodMinValue = null;
                        item.InputGoodMinValue = null;
                    }
                    #region 起始值
                    if (txtModifyMinMinValue.Value != null)
                    {
                        item.MinMinValue = Convert.ToDecimal(txtModifyMinMinValue.Value);
                        item.InputMinMinValue = (string)txtModifyMinMinValue.Value;
                    }
                    else
                    {
                        item.MinMinValue = null;
                        item.InputMinMinValue = null;
                    }
                    if (txtModifyMaxMinValue.Value != null)
                    {
                        item.MaxMinValue = Convert.ToDecimal(txtModifyMaxMinValue.Value);
                        item.InputMaxMinValue = (string)txtModifyMaxMinValue.Value;
                    }
                    else
                    {
                        item.MaxMinValue = null;
                        item.InputMaxMinValue = null;
                    }
                    #endregion


                    item.GoodOperator = (string)cbxModifyGoodOperator.Value;
                    #region 关系符
                    item.MinOperator = (string)cbxModifyMinOperator.Value;
                    item.MaxOperator = (string)cbxModifyMaxOperator.Value;
                    #endregion
                    if (cbxModifyPrimeOperator.Value == null)
                    {
                        item.PrimeMaxValue = null;
                        item.PrimeMinValue = null;
                        item.PrimeOperator = null;
                    }
                    else if ((cbxModifyPrimeOperator.Value.ToString() == "无") || (cbxModifyPrimeOperator.Value.ToString() == ""))
                    {
                        item.PrimeMaxValue = null;
                        item.PrimeMinValue = null;
                        item.PrimeOperator = null;
                    }
                    else
                    {
                        item.PrimeMaxValue = Convert.ToDecimal(txtModifyPrimeMaxValue.Value);
                        item.InputPrimeMaxValue = (string)txtModifyPrimeMaxValue.Value;
                        if (txtModifyPrimeMinValue.Value != null)
                        {
                            item.PrimeMinValue = Convert.ToDecimal(txtModifyPrimeMinValue.Value);
                            item.InputPrimeMinValue = (string)txtModifyPrimeMinValue.Value;
                        }
                        else
                        {
                            item.PrimeMinValue = null;
                            item.InputPrimeMinValue = null;
                        }
                        item.PrimeOperator = Convert.ToString(cbxModifyPrimeOperator.Value);
                    }
                }
                if (cbxModifyGoodIncludeMaxBorder.Checked && cbxModifyGoodIncludeMaxBorder.Enabled)
                {
                    item.GoodIncludeMaxBorder = "1";
                }
                else if (cbxModifyGoodIncludeMaxBorder.Disabled)
                {
                    item.GoodIncludeMaxBorder = null;
                }
                else
                {
                    item.GoodIncludeMaxBorder = "0";
                }

                if (cbxModifyGoodIncludeMinBorder.Checked && cbxModifyGoodIncludeMinBorder.Enabled)
                {
                    item.GoodIncludeMinBorder = "1";
                }
                else if (cbxModifyGoodIncludeMinBorder.Disabled)
                {
                    item.GoodIncludeMinBorder = null;
                }
                else
                {
                    item.GoodIncludeMinBorder = "0";
                }

                #region 是否包含边界
                if (cbxModifyMinIncludeMaxBorder.Checked && cbxModifyMinIncludeMaxBorder.Enabled)
                {
                    item.MinIncludeMaxBorder = "1";
                }
                else if (cbxModifyMinIncludeMaxBorder.Disabled)
                {
                    item.MinIncludeMaxBorder = null;
                }
                else
                {
                    item.MinIncludeMaxBorder = "0";
                }

                if (cbxModifyMinIncludeMinBorder.Checked && cbxModifyMinIncludeMinBorder.Enabled)
                {
                    item.MinIncludeMinBorder = "1";
                }
                else if (cbxModifyMinIncludeMinBorder.Disabled)
                {
                    item.MinIncludeMinBorder = null;
                }
                else
                {
                    item.MinIncludeMinBorder = "0";
                }

                //max

                if (cbxModifyMaxIncludeMaxBorder.Checked && cbxModifyMaxIncludeMaxBorder.Enabled)
                {
                    item.MaxIncludeMaxBorder = "1";
                }
                else if (cbxModifyMaxIncludeMaxBorder.Disabled)
                {
                    item.MaxIncludeMaxBorder = null;
                }
                else
                {
                    item.MaxIncludeMaxBorder = "0";
                }

                if (cbxModifyMaxIncludeMinBorder.Checked && cbxModifyMaxIncludeMinBorder.Enabled)
                {
                    item.MaxIncludeMinBorder = "1";
                }
                else if (cbxModifyMaxIncludeMinBorder.Disabled)
                {
                    item.MaxIncludeMinBorder = null;
                }
                else
                {
                    item.MaxIncludeMinBorder = "0";
                }
                #endregion

                if (cbxModifyPrimeIncludeMaxBorder.Checked && cbxModifyPrimeIncludeMaxBorder.Enabled)
                {
                    item.PrimeIncludeMaxBorder = "1";
                }
                else if (cbxModifyPrimeIncludeMaxBorder.Disabled)
                {
                    item.PrimeIncludeMaxBorder = null;
                }
                else
                {
                    item.PrimeIncludeMaxBorder = "0";
                }

                if (cbxModifyPrimeIncludeMinBorder.Checked && cbxModifyPrimeIncludeMinBorder.Enabled)
                {
                    item.PrimeIncludeMinBorder = "1";
                }
                else if (cbxModifyPrimeIncludeMinBorder.Disabled)
                {
                    item.PrimeIncludeMinBorder = null;
                }
                else
                {
                    item.PrimeIncludeMinBorder = "0";
                }

                #region 写入数字显示值
                if (originItem.ValueType == "数字")
                {
                    //合格品
                    if (item.GoodOperator == "－")
                    {
                        item.GoodDisplayValue = item.InputGoodMinValue + item.GoodOperator + item.InputGoodMaxValue;
                    }
                    if (item.GoodOperator == "P")
                    {
                        item.GoodDisplayValue = item.InputGoodMaxValue;
                    }
                    if (item.GoodOperator == ">" || item.GoodOperator == "I")
                    {
                        if (item.GoodIncludeMaxBorder == "1")
                        {
                            item.GoodDisplayValue = "≥" + item.InputGoodMaxValue;
                        }
                        else
                        {
                            item.GoodDisplayValue = item.GoodOperator + item.InputGoodMaxValue;
                        }
                    }
                    if (item.GoodOperator == "<" || item.GoodOperator == "A")
                    {
                        if (item.GoodIncludeMaxBorder == "1")
                        {
                            item.GoodDisplayValue = "≤" + item.InputGoodMaxValue;
                        }
                        else
                        {
                            item.GoodDisplayValue = item.GoodOperator + item.InputGoodMaxValue;
                        }
                    }



                    #region 数字显示值
                    //min
                    if (item.MinOperator == "－")
                    {
                        item.MinDisplayValue = item.InputMinMinValue + item.MinOperator + item.InputMinMaxValue;
                    }
                    if (item.MinOperator == "P")
                    {
                        item.MinDisplayValue = item.InputMinMaxValue;
                    }
                    if (item.MinOperator == ">" || item.MinOperator == "I")
                    {
                        if (item.MinIncludeMaxBorder == "1")
                        {
                            item.MinDisplayValue = "≥" + item.InputMinMaxValue;
                        }
                        else
                        {
                            item.MinDisplayValue = item.MinOperator + item.InputMinMaxValue;
                        }
                    }
                    if (item.MinOperator == "<" || item.MinOperator == "A")
                    {
                        if (item.MinIncludeMaxBorder == "1")
                        {
                            item.MinDisplayValue = "≤" + item.InputMinMaxValue;
                        }
                        else
                        {
                            item.MinDisplayValue = item.MinOperator + item.InputMinMaxValue;
                        }
                    }

                    //max
                    if (item.MaxOperator == "－")
                    {
                        item.MaxDisplayValue = item.InputMaxMinValue + item.MaxOperator + item.InputMaxMaxValue;
                    }
                    if (item.MaxOperator == "P")
                    {
                        item.MaxDisplayValue = item.InputMaxMaxValue;
                    }
                    if (item.MaxOperator == ">" || item.MaxOperator == "I")
                    {
                        if (item.MaxIncludeMaxBorder == "1")
                        {
                            item.MaxDisplayValue = "≥" + item.InputMaxMaxValue;
                        }
                        else
                        {
                            item.MaxDisplayValue = item.MaxOperator + item.InputMaxMaxValue;
                        }
                    }
                    if (item.MaxOperator == "<" || item.MaxOperator == "A")
                    {
                        if (item.MaxIncludeMaxBorder == "1")
                        {
                            item.MaxDisplayValue = "≤" + item.InputMaxMaxValue;
                        }
                        else
                        {
                            item.MaxDisplayValue = item.MaxOperator + item.InputMaxMaxValue;
                        }
                    }
                    #endregion
                    //一级品
                    if (item.PrimeOperator == "－")
                    {
                        item.PrimeDisplayValue = item.InputPrimeMinValue + item.PrimeOperator + item.InputPrimeMaxValue;
                    }
                    if (item.PrimeOperator == "P")
                    {
                        item.PrimeOperator = item.InputPrimeMaxValue;
                    }
                    if (item.PrimeOperator == ">" || item.PrimeOperator == "I")
                    {
                        if (item.PrimeIncludeMaxBorder == "1")
                        {
                            item.PrimeDisplayValue = "≥" + item.InputPrimeMaxValue;
                        }
                        else
                        {
                            item.PrimeDisplayValue = item.PrimeOperator + item.InputPrimeMaxValue;
                        }
                    }
                    if (item.PrimeOperator == "<" || item.PrimeOperator == "A")
                    {
                        if (item.PrimeIncludeMaxBorder == "1")
                        {
                            item.PrimeDisplayValue = "≤" + item.InputPrimeMaxValue;
                        }
                        else
                        {
                            item.PrimeDisplayValue = item.PrimeOperator + item.InputPrimeMaxValue;
                        }
                    }
                }
                #endregion

                item.Version = oldItem.Version + 1;
                item.LatestFlag = "1";
                item.LastDate = DateTime.Now;
                item.Remark = (string)txtModifyRemark.Value;
                item.DeleteFlag = oldItem.DeleteFlag;
                oldItem.LatestFlag = "0";
               
                detailManager.Insert(item);
                
                detailManager.Update(oldItem);
              
                this.AppendWebLog("项目明细修改", "项目编号：" + txtModifyItemId.Text);
                pageToolBar2.DoRefresh();
                this.windowModifyDetail.Close();
                msg.Alert("操作", "更新成功");
                msg.Show();
              
            }
            #endregion

            #region 执行标准未启用则修改当前版本
            else if (standard.ActivateFlag == "0")
            {
                item.ItemId = oldItem.ItemId;
                if (cbxModifyActivated.Checked)
                {
                    item.ActivateFlag = "1";
                }
                else
                {
                    item.ActivateFlag = "0";
                }
                item.Attach();
                item.CheckMethod = (string)txtModifyCheckMethod.Value;
                item.Frequency = (string)cbxModifyFrequency.Value;
                item.ItemDetailId = Convert.ToInt32(txtModifyDetailId.Value);
                item.MaterialCode = txtHiddenMaterialCode.Text.ToString();
                if (originItem.ValueType == "文字")
                {
                    item.GoodTextValue = (string)txtModifyGoodTextValue.Value;
                    item.PrimeTextValue = (string)txtModifyPrimeTextValue.Value;
                    item.GoodDisplayValue = (string)txtModifyGoodTextValue.Value;
                    item.PrimeDisplayValue = (string)txtModifyPrimeTextValue.Value;

                    item.MinTextValue = (string)txtModifyMinTextValue.Value;
                    item.MinDisplayValue = (string)txtModifyMinTextValue.Value;
                    item.MaxTextValue = (string)txtModifyMaxTextValue.Value;
                    item.MaxDisplayValue = (string)txtModifyMaxTextValue.Value;
                }
                if (originItem.ValueType == "数字")
                {
                    item.GoodMaxValue = Convert.ToDecimal(txtModifyGoodMaxValue.Value);
                    item.InputGoodMaxValue = (string)txtModifyGoodMaxValue.Value;
                    if (txtModifyGoodMinValue.Value != null)
                    {
                        item.GoodMinValue = Convert.ToDecimal(txtModifyGoodMinValue.Value);
                        item.InputGoodMinValue = (string)txtModifyGoodMinValue.Value;
                    }
                    else
                    {
                        item.GoodMinValue = null;
                        item.InputGoodMinValue = null;
                    }

                    #region 起始值
                    if (txtModifyMinMinValue.Value != null)
                    {
                        item.MinMinValue = Convert.ToDecimal(txtModifyMinMinValue.Value);
                        item.InputMinMinValue = (string)txtModifyMinMinValue.Value;
                    }
                    else
                    {
                        item.MinMinValue = null;
                        item.InputMinMinValue = null;
                    }
                    if (txtModifyMaxMinValue.Value != null)
                    {
                        item.MaxMinValue = Convert.ToDecimal(txtModifyMaxMinValue.Value);
                        item.InputMaxMinValue = (string)txtModifyMaxMinValue.Value;
                    }
                    else
                    {
                        item.MaxMinValue = null;
                        item.InputMaxMinValue = null;
                    }
                    #endregion

                    item.GoodOperator = (string)cbxModifyGoodOperator.Value;
                    #region 关系符
                    item.MinOperator = (string)cbxModifyMinOperator.Value;
                    item.MaxOperator = (string)cbxModifyMaxOperator.Value;
                    #endregion
                    if (cbxModifyPrimeOperator.Value == null)
                    {
                        item.PrimeMaxValue = null;
                        item.PrimeMinValue = null;
                        item.PrimeOperator = null;
                    }
                    else if ((cbxModifyPrimeOperator.Value.ToString() == "无") || (cbxModifyPrimeOperator.Value.ToString() == ""))
                    {
                        item.PrimeMaxValue = null;
                        item.PrimeMinValue = null;
                        item.PrimeOperator = null;
                    }
                    else
                    {
                        item.PrimeMaxValue = Convert.ToDecimal(txtModifyPrimeMaxValue.Value);
                        item.InputPrimeMaxValue = (string)txtModifyPrimeMaxValue.Value;
                        if (txtModifyPrimeMinValue.Value != null)
                        {
                            item.PrimeMinValue = Convert.ToDecimal(txtModifyPrimeMinValue.Value);
                            item.InputPrimeMinValue = (string)txtModifyPrimeMinValue.Value;
                        }
                        else
                        {
                            item.PrimeMinValue = null;
                            item.InputPrimeMinValue = null;
                        }
                        item.PrimeOperator = Convert.ToString(cbxModifyPrimeOperator.Value);
                    }
                }
                if (cbxModifyGoodIncludeMaxBorder.Checked && cbxModifyGoodIncludeMaxBorder.Enabled)
                {
                    item.GoodIncludeMaxBorder = "1";
                }
                else if (cbxModifyGoodIncludeMaxBorder.Disabled)
                {
                    item.GoodIncludeMaxBorder = null;
                }
                else
                {
                    item.GoodIncludeMaxBorder = "0";
                }

                if (cbxModifyGoodIncludeMinBorder.Checked && cbxModifyGoodIncludeMinBorder.Enabled)
                {
                    item.GoodIncludeMinBorder = "1";
                }
                else if (cbxModifyGoodIncludeMinBorder.Disabled)
                {
                    item.GoodIncludeMinBorder = null;
                }
                else
                {
                    item.GoodIncludeMinBorder = "0";
                }
                #region 是否包含边界
                if (cbxModifyMinIncludeMaxBorder.Checked && cbxModifyMinIncludeMaxBorder.Enabled)
                {
                    item.MinIncludeMaxBorder = "1";
                }
                else if (cbxModifyMinIncludeMaxBorder.Disabled)
                {
                    item.MinIncludeMaxBorder = null;
                }
                else
                {
                    item.MinIncludeMaxBorder = "0";
                }

                if (cbxModifyMinIncludeMinBorder.Checked && cbxModifyMinIncludeMinBorder.Enabled)
                {
                    item.MinIncludeMinBorder = "1";
                }
                else if (cbxModifyMinIncludeMinBorder.Disabled)
                {
                    item.MinIncludeMinBorder = null;
                }
                else
                {
                    item.MinIncludeMinBorder = "0";
                }

                //max

                if (cbxModifyMaxIncludeMaxBorder.Checked && cbxModifyMaxIncludeMaxBorder.Enabled)
                {
                    item.MaxIncludeMaxBorder = "1";
                }
                else if (cbxModifyMaxIncludeMaxBorder.Disabled)
                {
                    item.MaxIncludeMaxBorder = null;
                }
                else
                {
                    item.MaxIncludeMaxBorder = "0";
                }

                if (cbxModifyMaxIncludeMinBorder.Checked && cbxModifyMaxIncludeMinBorder.Enabled)
                {
                    item.MaxIncludeMinBorder = "1";
                }
                else if (cbxModifyMaxIncludeMinBorder.Disabled)
                {
                    item.MaxIncludeMinBorder = null;
                }
                else
                {
                    item.MaxIncludeMinBorder = "0";
                }
                #endregion

                if (cbxModifyPrimeIncludeMaxBorder.Checked && cbxModifyPrimeIncludeMaxBorder.Enabled)
                {
                    item.PrimeIncludeMaxBorder = "1";
                }
                else if (cbxModifyPrimeIncludeMaxBorder.Disabled)
                {
                    item.PrimeIncludeMaxBorder = null;
                }
                else
                {
                    item.PrimeIncludeMaxBorder = "0";
                }

                if (cbxModifyPrimeIncludeMinBorder.Checked && cbxModifyPrimeIncludeMinBorder.Enabled)
                {
                    item.PrimeIncludeMinBorder = "1";
                }
                else if (cbxModifyPrimeIncludeMinBorder.Disabled)
                {
                    item.PrimeIncludeMinBorder = null;
                }
                else
                {
                    item.PrimeIncludeMinBorder = "0";
                }

                #region 写入数字显示值
                if (originItem.ValueType == "数字")
                {
                    //合格品
                    if (item.GoodOperator == "－")
                    {
                        item.GoodDisplayValue = item.InputGoodMinValue + item.GoodOperator + item.InputGoodMaxValue;
                    }
                    if (item.GoodOperator == "P")
                    {
                        item.GoodDisplayValue = item.InputGoodMaxValue;
                    }
                    if (item.GoodOperator == ">" || item.GoodOperator == "I")
                    {
                        if (item.GoodIncludeMaxBorder == "1")
                        {
                            item.GoodDisplayValue = "≥" + item.InputGoodMaxValue;
                        }
                        else
                        {
                            item.GoodDisplayValue = item.GoodOperator + item.InputGoodMaxValue;
                        }
                    }
                    if (item.GoodOperator == "<" || item.GoodOperator == "A")
                    {
                        if (item.GoodIncludeMaxBorder == "1")
                        {
                            item.GoodDisplayValue = "≤" + item.InputGoodMaxValue;
                        }
                        else
                        {
                            item.GoodDisplayValue = item.GoodOperator + item.InputGoodMaxValue;
                        }
                    }
                    #region 数字显示值
                    //min
                    if (item.MinOperator == "－")
                    {
                        item.MinDisplayValue = item.InputMinMinValue + item.MinOperator + item.InputMinMaxValue;
                    }
                    if (item.MinOperator == "P")
                    {
                        item.MinDisplayValue = item.InputMinMaxValue;
                    }
                    if (item.MinOperator == ">" || item.MinOperator == "I")
                    {
                        if (item.MinIncludeMaxBorder == "1")
                        {
                            item.MinDisplayValue = "≥" + item.InputMinMaxValue;
                        }
                        else
                        {
                            item.MinDisplayValue = item.MinOperator + item.InputMinMaxValue;
                        }
                    }
                    if (item.MinOperator == "<" || item.MinOperator == "A")
                    {
                        if (item.MinIncludeMaxBorder == "1")
                        {
                            item.MinDisplayValue = "≤" + item.InputMinMaxValue;
                        }
                        else
                        {
                            item.MinDisplayValue = item.MinOperator + item.InputMinMaxValue;
                        }
                    }

                    //max
                    if (item.MaxOperator == "－")
                    {
                        item.MaxDisplayValue = item.InputMaxMinValue + item.MaxOperator + item.InputMaxMaxValue;
                    }
                    if (item.MaxOperator == "P")
                    {
                        item.MaxDisplayValue = item.InputMaxMaxValue;
                    }
                    if (item.MaxOperator == ">" || item.MaxOperator == "I")
                    {
                        if (item.MaxIncludeMaxBorder == "1")
                        {
                            item.MaxDisplayValue = "≥" + item.InputMaxMaxValue;
                        }
                        else
                        {
                            item.MaxDisplayValue = item.MaxOperator + item.InputMaxMaxValue;
                        }
                    }
                    if (item.MaxOperator == "<" || item.MaxOperator == "A")
                    {
                        if (item.MaxIncludeMaxBorder == "1")
                        {
                            item.MaxDisplayValue = "≤" + item.InputMaxMaxValue;
                        }
                        else
                        {
                            item.MaxDisplayValue = item.MaxOperator + item.InputMaxMaxValue;
                        }
                    }
                    #endregion
                    //一级品
                    if (item.PrimeOperator == "－")
                    {
                        item.PrimeDisplayValue = item.InputPrimeMinValue + item.PrimeOperator + item.InputPrimeMaxValue;
                    }
                    if (item.PrimeOperator == "P")
                    {
                        item.PrimeDisplayValue =  item.InputPrimeMaxValue;
                    }
                    if (item.PrimeOperator == ">" || item.PrimeOperator == "I")
                    {
                        if (item.PrimeIncludeMaxBorder == "1")
                        {
                            item.PrimeDisplayValue = "≥" + item.InputPrimeMaxValue;
                        }
                        else
                        {
                            item.PrimeDisplayValue = item.PrimeOperator + item.InputPrimeMaxValue;
                        }
                    }
                    if (item.PrimeOperator == "<" || item.PrimeOperator == "A")
                    {
                        if (item.PrimeIncludeMaxBorder == "1")
                        {
                            item.PrimeDisplayValue = "≤" + item.InputPrimeMaxValue;
                        }
                        else
                        {
                            item.PrimeDisplayValue = item.PrimeOperator + item.InputPrimeMaxValue;
                        }
                    }
                }
                #endregion

                item.LatestFlag = "1";
                item.LastDate = DateTime.Now;
                item.Remark = (string)txtModifyRemark.Value;
                item.DeleteFlag = oldItem.DeleteFlag;
                detailManager.Update(item);
                this.AppendWebLog("检测指标修改", "项目编号：" + txtModifyItemId.Text);
                pageToolBar2.DoRefresh();
                this.windowModifyDetail.Close();
                msg.Alert("操作", "更新成功");
                msg.Show();
            }
            else
            {
                msg.Alert("操作", "执行标准出错，请检查执行标准！");
                msg.Show();
                return;
            }
            #endregion
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
        this.windowModifyDetail.Close();
        this.windowAddDetail.Close();
    }

    /// <summary>
    /// 新建指标时选择检测项目变更时的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxAddItemName_DirectChange(object sender, DirectEventArgs e)
    {
        if (cbxAddItemName.Value != null)
        {
            if ((string)cbxAddItemName.Value != "")
            {
                try
                {
                    QmcCheckItem item = itemManager.GetById(cbxAddItemName.Value);
                    switch (item.ValueType)
                    {
                        case "文字":
                            txtAddPrimeTextValue.Enable();
                            txtAddPrimeMinValue.Disable();
                            txtAddPrimeMaxValue.Disable();
                            txtAddPrimeMinValue.Value = null;
                            txtAddPrimeMaxValue.Value = null;
                            txtAddGoodTextValue.Enable();
                            txtAddGoodMinValue.Disable();
                            txtAddGoodMaxValue.Disable();
                            txtAddGoodMinValue.Value = null;
                            txtAddGoodMaxValue.Value = null;
                            cbxAddGoodOperator.Disable();
                            cbxAddPrimeOperator.Disable();
                            cbxAddGoodOperator.Value = null;
                            cbxAddPrimeOperator.Value = null;
                            cbxAddGoodIncludeMinBorder.Disable();
                            cbxAddGoodIncludeMaxBorder.Disable();
                            cbxAddPrimeIncludeMinBorder.Disable();
                            cbxAddPrimeIncludeMaxBorder.Disable();
                            cbxAddGoodIncludeMinBorder.Checked = false;
                            cbxAddGoodIncludeMaxBorder.Checked = false;
                            cbxAddPrimeIncludeMinBorder.Checked = false;
                            cbxAddPrimeIncludeMaxBorder.Checked = false;


#region
                               txtAddMinTextValue.Enable();
                            txtAddMinMinValue.Disable();
                            txtAddMinMaxValue.Disable();
                            txtAddMinMinValue.Value = null;
                            txtAddMinMaxValue.Value = null;
                            cbxAddMinOperator.Disable();
                            cbxAddMinOperator.Value = null;
                            cbxAddMinIncludeMinBorder.Disable();
                            cbxAddMinIncludeMaxBorder.Disable();
                            cbxAddMinIncludeMinBorder.Checked = false;
                            cbxAddMinIncludeMaxBorder.Checked = false;
                               txtAddMaxTextValue.Enable();
                            txtAddMaxMinValue.Disable();
                            txtAddMaxMaxValue.Disable();
                            txtAddMaxMinValue.Value = null;
                            txtAddMaxMaxValue.Value = null;
                            cbxAddMaxOperator.Disable();
                             cbxAddMaxOperator.Value = null;
                            cbxAddMaxIncludeMinBorder.Disable();
                            cbxAddMaxIncludeMaxBorder.Disable();
                            cbxAddMaxIncludeMinBorder.Checked = false;
                            cbxAddMaxIncludeMaxBorder.Checked = false;
#endregion
                            break;
                        case "数字":
                            cbxAddPrimeOperator.Enable();
                            txtAddPrimeTextValue.Value = null;
                            cbxAddGoodOperator.Enable();
                            txtAddGoodTextValue.Value = "";
                            txtAddPrimeTextValue.Disable();
                            txtAddGoodTextValue.Disable();
                            txtAddPrimeMinValue.Enable();
                            txtAddPrimeMaxValue.Enable();
                            txtAddGoodMinValue.Enable();
                            txtAddGoodMaxValue.Enable();
                            cbxAddGoodIncludeMinBorder.Enable();
                            cbxAddGoodIncludeMaxBorder.Enable();
                            cbxAddPrimeIncludeMinBorder.Enable();
                            cbxAddPrimeIncludeMaxBorder.Enable();
                            #region

                                   cbxAddMinOperator.Enable();
                            txtAddMinTextValue.Value = "";
                           txtAddMinTextValue.Disable();
                           txtAddMinMinValue.Enable();
                            txtAddMinMaxValue.Enable();
                            cbxAddMinIncludeMinBorder.Enable();
                            cbxAddMinIncludeMaxBorder.Enable();

                                   cbxAddMaxOperator.Enable();
                            txtAddMaxTextValue.Value = "";
                           txtAddMaxTextValue.Disable();
                           txtAddMaxMinValue.Enable();
                            txtAddMaxMaxValue.Enable();
                            cbxAddMaxIncludeMinBorder.Enable();
                            cbxAddMaxIncludeMaxBorder.Enable();
                            #endregion
                            break;
                        default:
                            msg.Alert("操作", "检测项目值类型错误，请更正该检测项目！");
                            msg.Show();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    msg.Alert("操作", ex.Message);
                    msg.Show();
                }
            }
        }
    }

    /// <summary>
    /// 选择一级品中更改关系类型时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void cbxModifyPrimeOperator_change(object sender, EventArgs e)
    {
        if (cbxModifyPrimeOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxModifyPrimeOperator.Value.ToString())
            {
                case "无":
                    cbxModifyPrimeOperator.Value = null;
                    txtModifyPrimeMinValue.Disable();
                    cbxModifyPrimeIncludeMinBorder.Disable();
                    txtModifyPrimeMinValue.Value = null;
                    txtModifyPrimeMaxValue.Disable();
                    cbxModifyPrimeIncludeMaxBorder.Disable();
                    txtModifyPrimeMaxValue.Value = null;
                    break;
                case "－":
                    txtModifyPrimeMinValue.Enable();
                    cbxModifyPrimeIncludeMinBorder.Enable();
                    txtModifyPrimeMaxValue.Enable();
                    cbxModifyPrimeIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtModifyPrimeMinValue.Disable();
                    cbxModifyPrimeIncludeMinBorder.Disable();
                    txtModifyPrimeMinValue.Value = null;
                    txtModifyPrimeMaxValue.Enable();
                    cbxModifyPrimeIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtModifyPrimeMinValue.Disable();
                    cbxModifyPrimeIncludeMinBorder.Disable();
                    txtModifyPrimeMinValue.Value = null;
                    txtModifyPrimeMaxValue.Enable();
                    cbxModifyPrimeIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 选择合格品中更改关系类型时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void cbxModifyGoodOperator_change(object sender, EventArgs e)
    {
        if (cbxModifyGoodOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxModifyGoodOperator.Value.ToString())
            {
                case "无":
                    cbxModifyGoodOperator.Value = null;
                    txtModifyGoodMinValue.Disable();
                    cbxModifyGoodIncludeMinBorder.Disable();
                    txtModifyGoodMinValue.Value = null;
                    txtModifyGoodMaxValue.Disable();
                    cbxModifyGoodIncludeMaxBorder.Disable();
                    txtModifyGoodMaxValue.Value = null;
                    break;
                case "－":
                    txtModifyGoodMinValue.Enable();
                    cbxModifyGoodIncludeMinBorder.Enable();
                    txtModifyGoodMaxValue.Enable();
                    cbxModifyGoodIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtModifyGoodMinValue.Disable();
                    cbxModifyGoodIncludeMinBorder.Disable();
                    txtModifyGoodMinValue.Value = null;
                    txtModifyGoodMaxValue.Enable();
                    cbxModifyGoodIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtModifyGoodMinValue.Disable();
                    cbxModifyGoodIncludeMinBorder.Disable();
                    txtModifyGoodMinValue.Value = null;
                    txtModifyGoodMaxValue.Enable();
                    cbxModifyGoodIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }

 
    public void cbxModifyMinOperator_change(object sender, EventArgs e)
    {
        if (cbxModifyMinOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxModifyMinOperator.Value.ToString())
            {
                case "无":
                    cbxModifyMinOperator.Value = null;
                    txtModifyMinMinValue.Disable();
                    cbxModifyMinIncludeMinBorder.Disable();
                    txtModifyMinMinValue.Value = null;
                    txtModifyMinMaxValue.Disable();
                    cbxModifyMinIncludeMaxBorder.Disable();
                    txtModifyMinMaxValue.Value = null;
                    break;
                case "－":
                    txtModifyMinMinValue.Enable();
                    cbxModifyMinIncludeMinBorder.Enable();
                    txtModifyMinMaxValue.Enable();
                    cbxModifyMinIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtModifyMinMinValue.Disable();
                    cbxModifyMinIncludeMinBorder.Disable();
                    txtModifyMinMinValue.Value = null;
                    txtModifyMinMaxValue.Enable();
                    cbxModifyMinIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtModifyMinMinValue.Disable();
                    cbxModifyMinIncludeMinBorder.Disable();
                    txtModifyMinMinValue.Value = null;
                    txtModifyMinMaxValue.Enable();
                    cbxModifyMinIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }

    public void cbxModifyMaxOperator_change(object sender, EventArgs e)
    {
        if (cbxModifyMaxOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxModifyMaxOperator.Value.ToString())
            {
                case "无":
                    cbxModifyMaxOperator.Value = null;
                    txtModifyMaxMaxValue.Disable();
                    cbxModifyMaxIncludeMaxBorder.Disable();
                    txtModifyMaxMaxValue.Value = null;
                    txtModifyMaxMaxValue.Disable();
                    cbxModifyMaxIncludeMaxBorder.Disable();
                    txtModifyMaxMaxValue.Value = null;
                    break;
                case "－":
                    txtModifyMaxMinValue.Enable();
                    cbxModifyMaxIncludeMinBorder.Enable();
                    txtModifyMaxMaxValue.Enable();
                    cbxModifyMaxIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtModifyMaxMinValue.Disable();
                    cbxModifyMaxIncludeMinBorder.Disable();
                    txtModifyMaxMaxValue.Value = null;
                    txtModifyMaxMaxValue.Enable();
                    cbxModifyMaxIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtModifyMaxMinValue.Disable();
                    cbxModifyMaxIncludeMinBorder.Disable();
                    txtModifyMaxMaxValue.Value = null;
                    txtModifyMaxMaxValue.Enable();
                    cbxModifyMaxIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 选择一级品中更改关系类型时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void cbxAddPrimeOperator_change(object sender, EventArgs e)
    {
        if (cbxAddPrimeOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxAddPrimeOperator.Value.ToString())
            {
                case "无":
                    cbxAddPrimeOperator.Value = null;
                    txtAddPrimeMinValue.Disable();
                    cbxAddPrimeIncludeMinBorder.Disable();
                    txtAddPrimeMinValue.Value = null;
                    txtAddPrimeMaxValue.Disable();
                    cbxAddPrimeIncludeMaxBorder.Disable();
                    txtAddPrimeMaxValue.Value = null;
                    break;
                case "－":
                    txtAddPrimeMinValue.Enable();
                    cbxAddPrimeIncludeMinBorder.Enable();
                    txtAddPrimeMaxValue.Enable();
                    cbxAddPrimeIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtAddPrimeMinValue.Disable();
                    cbxAddPrimeIncludeMinBorder.Disable();
                    txtAddPrimeMinValue.Value = null;
                    txtAddPrimeMaxValue.Enable();
                    cbxAddPrimeIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtAddPrimeMinValue.Disable();
                    cbxAddPrimeIncludeMinBorder.Disable();
                    txtAddPrimeMinValue.Value = null;
                    txtAddPrimeMaxValue.Enable();
                    cbxAddPrimeIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 选择合格品中更改关系类型时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void cbxAddGoodOperator_change(object sender, EventArgs e)
    {
        if (cbxAddGoodOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxAddGoodOperator.Value.ToString())
            {

                case "无":
                    cbxAddGoodOperator.Value = null;
                    txtAddGoodMinValue.Disable();
                    cbxAddGoodIncludeMinBorder.Disable();
                    txtAddGoodMinValue.Value = null;
                    txtAddGoodMaxValue.Disable();
                    cbxAddGoodIncludeMaxBorder.Disable();
                    txtAddGoodMaxValue.Value = null;
                    break;

                case "－":
                    txtAddGoodMinValue.Enable();
                    cbxAddGoodIncludeMinBorder.Enable();
                    txtAddGoodMaxValue.Enable();
                    cbxAddGoodIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtAddGoodMinValue.Disable();
                    cbxAddGoodIncludeMinBorder.Disable();
                    txtAddGoodMinValue.Value = null;
                    txtAddGoodMaxValue.Enable();
                    cbxAddGoodIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtAddGoodMinValue.Disable();
                    cbxAddGoodIncludeMinBorder.Disable();
                    txtAddGoodMinValue.Value = null;
                    txtAddGoodMaxValue.Enable();
                    cbxAddGoodIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }

    public void cbxAddMaxOperator_change(object sender, EventArgs e)
    {
        if (cbxAddMaxOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxAddMaxOperator.Value.ToString())
            {
                case "无":
                    cbxAddMaxOperator.Value = null;
                    txtAddMaxMinValue.Disable();
                    cbxAddMaxIncludeMinBorder.Disable();
                    txtAddMaxMinValue.Value = null;
                    txtAddMaxMaxValue.Disable();
                    cbxAddMaxIncludeMaxBorder.Disable();
                    txtAddMaxMaxValue.Value = null;
                    break;
                case "－":
                    txtAddMaxMinValue.Enable();
                    cbxAddMaxIncludeMinBorder.Enable();
                    txtAddMaxMaxValue.Enable();
                    cbxAddMaxIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtAddMaxMinValue.Disable();
                    cbxAddMaxIncludeMinBorder.Disable();
                    txtAddMaxMinValue.Value = null;
                    txtAddMaxMaxValue.Enable();
                    cbxAddMaxIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtAddMaxMinValue.Disable();
                    cbxAddMaxIncludeMinBorder.Disable();
                    txtAddMaxMinValue.Value = null;
                    txtAddMaxMaxValue.Enable();
                    cbxAddMaxIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }
    public void cbxAddMinOperator_change(object sender, EventArgs e)
    {
        if (cbxAddMinOperator.Value != null)
        {
            //根据运算符动态改变数值输入框的状态
            switch (cbxAddMinOperator.Value.ToString())
            {
                case "无":
                    cbxAddMinOperator.Value = null;
                    txtAddMinMinValue.Disable();
                    cbxAddMinIncludeMinBorder.Disable();
                    txtAddMinMinValue.Value = null;
                    txtAddMinMinValue.Disable();
                    cbxAddMinIncludeMinBorder.Disable();
                    txtAddMinMinValue.Value = null;
                    break;
                case "－":
                    txtAddMinMinValue.Enable();
                    cbxAddMinIncludeMinBorder.Enable();
                    txtAddMinMaxValue.Enable();
                    cbxAddMinIncludeMaxBorder.Enable();
                    break;
                case ">":
                    txtAddMinMinValue.Disable();
                    cbxAddMinIncludeMinBorder.Disable();
                    txtAddMinMinValue.Value = null;
                    txtAddMinMaxValue.Enable();
                    cbxAddMinIncludeMaxBorder.Enable();
                    break;
                case "<":
                    txtAddMinMinValue.Disable();
                    cbxAddMinIncludeMinBorder.Disable();
                    txtAddMinMinValue.Value = null;
                    txtAddMinMaxValue.Enable();
                    cbxAddMinIncludeMaxBorder.Enable();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 点击保存检测项按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_savedetail_Click(object sender, EventArgs e)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        QmcStandard standard = standardManager.GetById(standardId);
        if (standard.ActivateFlag == "2")
        {
            msg.Alert("操作", "所选标准已过期，不能执行此操作！");
            msg.Show();
            return;
        }
        try
        {
            EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"));
            EntityArrayList<QmcCheckItemDetail> detailFullList = detailManager.GetListByWhere(QmcCheckItemDetail._.MaterialCode == txtHiddenMaterialCode.Value.ToString() && QmcCheckItemDetail._.DeleteFlag == "0" && QmcCheckItemDetail._.LatestFlag == "1");
            EntityArrayList<QmcCheckItemDetail> detailList = new EntityArrayList<QmcCheckItemDetail>();
            foreach (QmcCheckItemDetail token in detailFullList)
            {
                foreach (QmcCheckItem checkItem in itemList)
                {
                    if (token.ItemId == checkItem.ItemId)
                    {
                        detailList.Add(token);
                    }
                }
            }
            int count = 0;
            foreach (QmcCheckItemDetail detailOrigin in detailList)
            {
                detailOrigin.ActivateFlag = "0";
                foreach (SelectedRow row in detailSelectionModel.SelectedRows)
                {
                    if (detailOrigin.ItemDetailId.ToString() == row.RecordID)
                    {
                        detailOrigin.ActivateFlag = "1";
                        count++;
                    }
                }
                detailManager.Update(detailOrigin);
            }
            this.AppendWebLog("检测指标生效", "条目数：" + count);
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

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string detailId)
    {
        try
        {
            QmcCheckItemDetail detail = detailManager.GetById(detailId);
            string standardId = String.Empty;
            if (cbxStandard.Value != null)
            {
                standardId = cbxStandard.Value.ToString();
            }
            else
            {
                return "删除失败：没有选择执行标准！";
            }
            QmcStandard standard = standardManager.GetById(standardId);
            //if (standard.ActivateFlag == "1")
            //{
            //    return "删除失败：所选标准已生效，不能删除检测项目！";
            //}
            //else 
            if (standard.ActivateFlag == "2")
            {
                return "删除失败：所选标准已过期，不能删除检测项目！";
            }
            detail.DeleteFlag = "1";
            detailManager.Update(detail);
            this.AppendWebLog("检测指标删除", "指标编号：" + detailId);
            pageToolBar2.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    #endregion

    #region 检测项显示单元
    [Serializable]
    private class DetailViewUnit
    {
        #region 私有字段
        private string itemId = String.Empty;
        private string detailId = String.Empty;
        private string itemName = String.Empty;
        private string materialName = String.Empty;
        private string primeTextValue = String.Empty;
        private string goodTextValue = String.Empty;
        private string minTextValue = String.Empty;
        private string maxTextValue = String.Empty;
        private string frequency = String.Empty;
        private string checkMethod = String.Empty;
        private string version = String.Empty;
        private string remark = String.Empty;
        #endregion

        #region 公有方法
        public string ItemId
        {
            set { this.itemId = value; }
            get { return this.itemId; }
        }

        public string DetailId
        {
            set { this.detailId = value; }
            get { return this.detailId; }
        }

        public string ItemName
        {
            set { this.itemName = value; }
            get { return this.itemName; }
        }

        public string MaterialName
        {
            set { this.materialName = value; }
            get { return this.materialName; }
        }

        public string PrimeTextValue
        {
            set { this.primeTextValue = value; }
            get { return this.primeTextValue; }
        }

        public string GoodTextValue
        {
            set { this.goodTextValue = value; }
            get { return this.goodTextValue; }
        }
        public string MinTextValue
        {
            set { this.minTextValue = value; }
            get { return this.minTextValue; }
        }
        public string MaxTextValue
        {
            set { this.maxTextValue = value; }
            get { return this.maxTextValue; }
        }
        public string Frequency
        {
            set { this.frequency = value; }
            get { return this.frequency; }
        }

        public string CheckMethod
        {
            set { this.checkMethod = value; }
            get { return this.checkMethod; }
        }

        public string Version
        {
            set { this.version = value; }
            get { return this.version; }
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