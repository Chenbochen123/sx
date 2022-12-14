using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Data;
public partial class Manager_Rubber_Report_CompoundOutput : System.Web.UI.Page
{
    protected PptPlanManager planManager = new PptPlanManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "";
            cbxChejian.Value = "2";
        }
    }
    private DataSet GetPageResultData(int page, int pagesize)
    {
        //首先判断是否最少包含一个分组,目前允许
        //if (!cbxByPlanDate.Checked && !cbxByShift.Checked && !cbxByClass.Checked && !cbxByEquip.Checked && !cbxByMaterCode.Checked)
        //{
        //    X.Msg.Alert("提示", "至少要有一个分组！").Show();
        //    return;
        //}
        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.StartDate = txtBeginTime.Text.ToString();
        queryParams.EndDate = txtEndTime.Text.ToString();
        if (cbxShiftClass.Value.ToString() == "0")
        {
            cbxShiftClass.Value = "";
        }
        queryParams.ClassID = cbxShiftClass.Value.ToString();
        if (cbxShift.Value.ToString() == "0")
        {
            cbxShift.Value = "";
        }
        queryParams.shiftID = cbxShift.Value.ToString();
        queryParams.StorageID = this.hiddenStorageID.Text.Trim();
        queryParams.StoragePlaceID = this.hiddenStoragePlaceID.Text.Trim();
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.BasMaterial = hiddenMaterCode.Text;
        if (cbxStock.Value.ToString() == "0")
        {
            cbxStock.Value = "";
        }
        queryParams.isout = cbxStock.Value.ToString();
        queryParams.ToStorageID = hiddenToStorageID.Text;
        queryParams.ToStoragePlaceID = hiddenToStoragePlaceID.Text;
        string type="";
        if(cbx1.Checked)
        {
            type+="001,";
        }
        if(cbx2.Checked)
        {
            type+="002,";
        }
        if(cbx3.Checked)
        {
            type+="003,";
        }
        if(cbx4.Checked)
        {
            type+="006,";
        }
        if(cbx5.Checked)
        {
            type+="007,";
        }
        if(cbx6.Checked)
        {
            type+="008,";
        }
        if(cbx7.Checked)
        {
            type+="009,";
        }
        if(type!="")
        {
            type=type.Substring(0,type.Length-1);
        }
        queryParams.type=type;
        if (cbxChejian.Value.ToString() == "0")
        {
            cbxChejian.Value = "";
        }
        queryParams.WorkShopCode = this.cbxChejian.Value.ToString();
        queryParams.page = page;
        queryParams.pagenum = pagesize;
        return planManager.CompoundQuery(queryParams);
    }
    private DataSet GetPageTotalResultData()
    {
        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.StartDate = txtBeginTime.Text.ToString();
        queryParams.EndDate = txtEndTime.Text.ToString();
        if (cbxShiftClass.Value.ToString() == "0")
        {
            cbxShiftClass.Value = "";
        }
        queryParams.ClassID = cbxShiftClass.Value.ToString();
        if (cbxShift.Value.ToString() == "0")
        {
            cbxShift.Value = "";
        }
        queryParams.shiftID = cbxShift.Value.ToString();
        queryParams.StorageID = this.hiddenStorageID.Text.Trim();
        queryParams.StoragePlaceID = this.hiddenStoragePlaceID.Text.Trim();
        queryParams.EquipCode = hiddenEquipCode.Text;
        queryParams.BasMaterial = hiddenMaterCode.Text;
        if (cbxStock.Value.ToString() == "0")
        {
            cbxStock.Value = "";
        }
        queryParams.isout = cbxStock.Value.ToString();
        queryParams.ToStorageID = hiddenToStorageID.Text;
        queryParams.ToStoragePlaceID = hiddenToStoragePlaceID.Text;
        string type = "";
        if (cbx1.Checked)
        {
            type += "001,";
        }
        if (cbx2.Checked)
        {
            type += "002,";
        }
        if (cbx3.Checked)
        {
            type += "003,";
        }
        if (cbx4.Checked)
        {
            type += "006,";
        }
        if (cbx5.Checked)
        {
            type += "007,";
        }
        if (cbx6.Checked)
        {
            type += "008,";
        }
        if (cbx7.Checked)
        {
            type += "009,";
        }
        if (type != "")
        {
            type = type.Substring(0, type.Length - 1);
        }
        queryParams.type = type;
        if (cbxChejian.Value.ToString() == "0")
        {
            cbxChejian.Value = "";
        }
        queryParams.WorkShopCode = this.cbxChejian.Value.ToString();

        return planManager.CompoundQueryMonth(queryParams);
    }
    [DirectMethod]
    public object GridTotalPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        DataSet lst = GetPageTotalResultData();
        DataTable data = lst.Tables[0];
        int total = data.Rows.Count;
        return new { data,total  };
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        DataSet lst = GetPageResultData(prms.Page, prms.Limit);
        DataTable data = lst.Tables[0];
        int total = string.IsNullOrEmpty(lst.Tables[1].Rows[0][1].ToString()) ? 0 : Convert.ToInt32(lst.Tables[1].Rows[0][1].ToString());
        return new { data, total };
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PptPlan> pageParams = new PageResult<PptPlan>();
        pageParams.PageSize = -100;
        DataSet lst = GetPageResultData(1, 99999);
        for (int i = 0; i < lst.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.Tables[0].Columns[i];
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
                lst.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst, "胶料产量报表");
    }
}