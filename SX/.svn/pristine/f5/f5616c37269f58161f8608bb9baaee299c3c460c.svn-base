using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using NBear.Common;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;


public partial class Manager_RawMaterialQuality_QueryQmcCheckData : System.Web.UI.Page
{
    #region 属性注入
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected IBasUserManager userManager = new BasUserManager();
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IQmcLedgerManager ledgerManager = new QmcLedgerManager();
    protected IQmcLedgerKeyManager ledgerKeyManager = new QmcLedgerKeyManager();
    protected IQmcLedgerDetailManager ledgerDetailManager = new QmcLedgerDetailManager();
    protected IQmcSpecMappingManager specMappingManager = new QmcSpecMappingManager();
    protected IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
    protected IQmcCheckDataDetailManager detailManager = new QmcCheckDataDetailManager();
    protected IQmcSampleLedgerManager sampleLedgerManager = new QmcSampleLedgerManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 页面初始化

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitControls();
            DateFieldNorthCheckDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthCheckDate.SetRawValue(DateTime.Today.ToString("yyyy-MM-dd"));
            InitData();
        }

    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 原材料分类 
        IBasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
        EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
            BasMaterialMinorType._.DeleteFlag == "0"
            & BasMaterialMinorType._.MajorID == 1
            , BasMaterialMinorType._.ObjID.Asc);
        foreach (BasMaterialMinorType mBasMaterialMinorType in mBasMaterialMinorTypeList)
        {
            ComboBoxNorthMaterMinorType.AddItem(mBasMaterialMinorType.MinorTypeName, mBasMaterialMinorType.MinorTypeID);
        }
    }

    /// <summary>
    /// 初始化查询数据
    /// </summary>
    private void InitData()
    {
        StoreCenterDetail.RemoveAll();
        StoreCenterMaster.RemoveAll();
        IQmcCheckDataQueryParams paras = new QmcCheckDataQueryParams();
        paras.RecordStat = "1";//只显示已提交的检测记录
        if (ComboBoxNorthMaterMinorType.Value != null)
        {
            if (ComboBoxNorthMaterMinorType.Value.ToString() != "")
            {
                paras.MinorTypeID = ComboBoxNorthMaterMinorType.Value.ToString();
            }
        }
        if (ComboBoxNorthMater.Value != null)
        {
            if (ComboBoxNorthMater.Value.ToString() != "")
            {
                paras.MaterCode = ComboBoxNorthMater.Value.ToString();
            }
        }
        if (DateFieldNorthCheckDate.RawText != null && DateFieldNorthCheckDate.RawText != "")
        {
            paras.BeginCheckDate = DateFieldNorthCheckDate.RawText;
            paras.EndCheckDate = DateFieldNorthCheckDate.RawText;
        }
        if (ComboBoxNorthCheckResult.Value != null)
        {
            if (ComboBoxNorthCheckResult.Value.ToString() != "")
            {
                paras.CheckResult = ComboBoxNorthCheckResult.Value.ToString();
            }
        }
        if (TextFieldNorthBarcode.Text != null)
        {
            if (TextFieldNorthBarcode.Text.Trim() != "")
            {
                paras.Barcode = TextFieldNorthBarcode.Text.Trim();
            }
        }
        DataSet ds = checkDataManager.GetReportDataSetByParams(paras);
        DataTable dtValid = ds.Tables[0].Clone();
        dtValid.Rows.Clear();
        //去掉已录台账
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (ledgerManager.GetListByWhere(QmcLedger._.CheckId == dr["CheckId"].ToString() && QmcLedger._.DeleteFlag == "0").Count == 0)
                {
                    dtValid.ImportRow(dr);
                }
            }
        }
        ds.Tables.Clear();
        ds.Tables.Add(dtValid);
        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();
    }

    #endregion

    #region 事件处理
    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        StoreCenterDetail.RemoveAll();
        StoreCenterMaster.RemoveAll();
        IQmcCheckDataQueryParams paras = new QmcCheckDataQueryParams();
        paras.RecordStat = "1";//只显示已提交的检测记录
        if (ComboBoxNorthMaterMinorType.Value.ToString() != "")
        {
            paras.MinorTypeID = ComboBoxNorthMaterMinorType.Value.ToString();
        }
        if (ComboBoxNorthMater.Value.ToString() != "")
        {
            paras.MaterCode = ComboBoxNorthMater.Value.ToString();
        }
        if (DateFieldNorthCheckDate.RawText != null && DateFieldNorthCheckDate.RawText != "")
        {
            paras.BeginCheckDate = DateFieldNorthCheckDate.RawText;
            paras.EndCheckDate = DateFieldNorthCheckDate.RawText;
        }
        if (ComboBoxNorthCheckResult.Value.ToString() != "")
        {
            paras.CheckResult = ComboBoxNorthCheckResult.Value.ToString();
        }
        if (TextFieldNorthBarcode.Text.Trim() != "")
        {
            paras.Barcode = TextFieldNorthBarcode.Text.Trim();
        }
        DataSet ds = checkDataManager.GetReportDataSetByParams(paras);
        DataTable dtValid = ds.Tables[0].Clone();
        dtValid.Rows.Clear();
        //去掉已录台账
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (ledgerManager.GetListByWhere(QmcLedger._.CheckId == dr["CheckId"].ToString() && QmcLedger._.DeleteFlag == "0").Count == 0)
                {
                    dtValid.ImportRow(dr);
                }
            }
        }
        ds.Tables.Clear();
        ds.Tables.Add(dtValid);
        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();
    }

    /// <summary>
    /// 更换原材料分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthMaterMinorType_Change(object sender, DirectEventArgs e)
    {
        ComboBoxNorthMater.GetStore().RemoveAll();

        ComboBoxNorthMater.SetValue("");

        string minorTypeId = ComboBoxNorthMaterMinorType.Value.ToString();
        if (minorTypeId == "")
        {
            return;
        }

        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.DeleteFlag == "0"
            & BasMaterial._.MajorTypeID == 1
            & BasMaterial._.MinorTypeID == minorTypeId
            , BasMaterial._.MaterialName.Asc);
        foreach (BasMaterial mBasMaterial in mBasMaterialList)
        {
            ComboBoxNorthMater.AddItem(mBasMaterial.MaterialName, mBasMaterial.MaterialCode);
        }
    }

    /// <summary>
    /// 选中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckboxSelectionModelCenterMaster_SelectionChange(object sender, DirectEventArgs e)
    {
        string checkId = e.ExtraParams["CheckId"].ToString();
        StoreCenterDetail.RemoveAll();

        DataSet ds = detailManager.GetDataSetByCheckId(checkId);
        StoreCenterDetail.DataSource = ds;
        StoreCenterDetail.DataBind();
    }
    #endregion
}