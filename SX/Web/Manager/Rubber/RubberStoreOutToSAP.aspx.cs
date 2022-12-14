using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_Rubber_RubberStoreOutToSAP : Mesnac.Web.UI.Page
{
    protected PpmRubber001Manager rubber001Manager = new PpmRubber001Manager();
    protected BasEquipManager basEquipManager = new BasEquipManager();
    protected SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();
    protected IncSapOrderReloadManager sapManager = new IncSapOrderReloadManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            DataSet ds = null;
            if (!string.IsNullOrEmpty(Request.QueryString["ObjID"]))
            {
                txtObjID.Text = Request.QueryString["ObjID"].ToString();

                ds = rubber001Manager.GetCondition(txtObjID.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbxChejian.Value = ds.Tables[0].Rows[0]["WorkShopCode"].ToString();
                    txtPlanDate.Text = ds.Tables[0].Rows[0]["PlanDate"].ToString();
                    cbxShiftID.Value = ds.Tables[0].Rows[0]["ShiftID"].ToString();
                    cbxStockType.Value = ds.Tables[0].Rows[0]["StockType"].ToString();
                }
            }
            if (ds == null)
            {
                txtPlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                cbxChejian.Value = "2";
                cbxShiftID.Value = "1";
                cbxStockType.Value = "1";
            }
        }
    }

    private PageResult<PpmRubber001> GetPageResultData(PageResult<PpmRubber001> pageParams)
    {
        PpmRubber001Manager.QueryParams queryParams = new PpmRubber001Manager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.chejianCode = cbxChejian.SelectedItem.Value;
        queryParams.planDate = Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd");
        queryParams.shiftID = cbxShiftID.SelectedItem.Value;
        queryParams.stockType = cbxStockType.SelectedItem.Value;
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.objID = txtObjID.Text;

        return rubber001Manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubber001> pageParams = new PageResult<PpmRubber001>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "Barcode desc";

        PageResult<PpmRubber001> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnUpload_Click(object sender, DirectEventArgs e)
    {
        //判断接口是否打开
        if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "0")
        {
            X.Msg.Alert("提示", "SAP接口已经关闭，请联系管理员处理！").Show();
            return;
        }
        //判断指定生产日期和班次下的信息是否已经上传，如果已经上传，不允许重复上传，提示手工报产
        string mesOrderCode = Convert.ToDateTime(txtPlanDate.Text).ToString("yyyyMMdd") + cbxShiftID.SelectedItem.Value + cbxChejian.SelectedItem.Value;
        string mesOrderType = "";
        if (cbxStockType.SelectedItem.Value == "1")
            mesOrderType = "0001";
        else
            mesOrderType = "0002";
        if (sapManager.IsExistData(mesOrderCode, mesOrderType))
        {
            X.Msg.Alert("提示", Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + cbxShiftID.SelectedItem.Text+"数据已上传！").Show();
            return;
        }

        try
        {
            WebReference.Service ws = new WebReference.Service();
            ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
            ws.SetProductionsUpload(cbxStockType.SelectedItem.Value, "0", Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd"), cbxShiftID.SelectedItem.Value, cbxChejian.SelectedItem.Value, "");
            X.Msg.Alert("提示", "上传成功！").Show();
            return;
        }
        catch (Exception ex)
        {
            X.Msg.Alert("错误提示", "上传失败：" + ex.Message).Show();
            return;
        }
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmRubber001> pageParams = new PageResult<PpmRubber001>();
        pageParams.PageSize = -100;
        PageResult<PpmRubber001> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "上传数据");
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenStockType.Text = e.ExtraParams["StockType"];
        hiddenBarcode.Text = e.ExtraParams["Barcode"];
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string barcode)
    {
        
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string barcode)
    {
        return "";
    }
}