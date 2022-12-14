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

public partial class Manager_Rubber_ReturnRubberReport : System.Web.UI.Page
{
    protected PpmReturnRubberManager returnRubberManager = new PpmReturnRubberManager();
    protected BasEquipManager basEquipManager = new BasEquipManager();
    protected BasMaterialManager basMaterialManager = new BasMaterialManager();
    protected BasStorageManager basStorageManager = new BasStorageManager();
    protected BasStoragePlaceManager basStoragePlaceManager = new BasStoragePlaceManager();
    protected PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();
    protected BasEquipFacManager facManager = new BasEquipFacManager();
    protected SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 绑定设备类型
            Ext.Net.ListItem allitem = new Ext.Net.ListItem("全部", "");
            EntityArrayList<BasEquipFac> lstBasEquipFac = facManager.GetListByWhere(BasEquipFac._.DeleteFlag == "0");
            cbxEquip.Items.Clear();
            cbxEquip.Items.Add(allitem);
            foreach (BasEquipFac m in lstBasEquipFac)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.EquipCode.ToString();
                item.Text = m.EquipName;
                cbxEquip.Items.Add(item);
            }
            cbxEquip.Text = "全部";
            #endregion
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "all";
            cbxChejian.Value = "all";
        }
    }

    private PageResult<PpmReturnRubber> GetPageResultData(PageResult<PpmReturnRubber> pageParams)
    {
        PpmReturnRubberManager.QueryParams queryParams = new PpmReturnRubberManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.equipCode = cbxEquip.SelectedItem.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.stockInFlag = "1";
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.shiftClassID = cbxShiftClass.SelectedItem.Value;
        queryParams.chejianCode = cbxChejian.SelectedItem.Value;

        return returnRubberManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmReturnRubber> pageParams = new PageResult<PpmReturnRubber>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "OperDate desc";

        PageResult<PpmReturnRubber> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }


    public void cbxShiftClass_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxInStock_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxChejian_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmReturnRubber> pageParams = new PageResult<PpmReturnRubber>();
        pageParams.PageSize = -100;
        PageResult<PpmReturnRubber> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "返回胶报表");
    }












    public void txtBeginTime_change(object sender, EventArgs e)
    {
        if ((txtBeginTime.Text != DateTime.MinValue.ToString()) && (txtEndTime.Text != DateTime.MinValue.ToString()))
        {
            if (Convert.ToDateTime(txtBeginTime.Text) > Convert.ToDateTime(txtEndTime.Text))
            {
                X.MessageBox.Alert("提示", "开始时间不能大于结束时间!").Show();
                return;
            }
        }
        this.pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        if ((txtBeginTime.Text != DateTime.MinValue.ToString()) && (txtEndTime.Text != DateTime.MinValue.ToString()))
        {
            if (Convert.ToDateTime(txtBeginTime.Text) > Convert.ToDateTime(txtEndTime.Text))
            {
                X.MessageBox.Alert("提示", "开始时间不能大于结束时间!").Show();
                return;
            }
        }
        this.pageToolBar.DoRefresh();
    }

}