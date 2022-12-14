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

public partial class Manager_Rubber_RubberJZByShiftReport : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btnAdd" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    private PpmRubberJZByShiftManager jzByShiftManager = new PpmRubberJZByShiftManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtPlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<PpmRubberJZByShift> GetPageResultData(PageResult<PpmRubberJZByShift> pageParams)
    {
        PpmRubberJZByShiftManager.QueryParams queryParams = new PpmRubberJZByShiftManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.planDate = Convert.ToDateTime(txtPlanDate.Text);
        queryParams.workShopCode = cbxWorkShopCode.SelectedItem.Value;
        queryParams.rubType = cbxRubType.SelectedItem.Value;

        if (cbxShiftID.SelectedItem.Value != "all")
            queryParams.shiftID = cbxShiftID.SelectedItem.Value;

        return jzByShiftManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubberJZByShift> pageParams = new PageResult<PpmRubberJZByShift>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RubCode asc, MaterType asc";

        PageResult<PpmRubberJZByShift> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        hiddenMaterCode.Text = string.Empty;
        txtPlanDate1.Text = txtPlanDate.Text;
        cbxShiftID1.Value = cbxShiftID.SelectedItem.Value;
        cbxWorkShopCode1.Value = cbxWorkShopCode.SelectedItem.Value;

        this.winAdd.Show();
    }

    protected void btnAddSave_Click(object sender, EventArgs e)
    {
        //PpmRubberJZByShift mminjar = new PpmRubberJZByShift();
        //mminjar.Materbarcode = txtMaterBarcode1.Text;

        //injarManager.Insert(mminjar);

        //txtUserName1.Text = string.Empty;
        //hiddenUserID.Text = string.Empty;
        //hiddenNoStorageID.Text = string.Empty;
        //pageToolBar.DoRefresh();
        string IsAllowedAdd = jzByShiftManager.DataAllowAddJZ(txtPlanDate1.Text, cbxWorkShopCode1.SelectedItem.Value, cbxShiftID1.SelectedItem.Value, hiddenMaterCode.Text, "1");
        if (IsAllowedAdd != "OK")
        {
            X.Msg.Alert("提示", IsAllowedAdd).Show();
            return;
        }

        PpmRubberJZByShift jzByShift = new PpmRubberJZByShift();
        jzByShift.PlanDate = Convert.ToDateTime(txtPlanDate1.Text);
        jzByShift.ShiftID = cbxShiftID1.SelectedItem.Value;
        jzByShift.WorkShopCode = Convert.ToInt32(cbxWorkShopCode1.SelectedItem.Value);
        jzByShift.MaterCode = hiddenMaterCode.Text;
        jzByShift.RubCode = hiddenMaterCode.Text.Substring(hiddenMaterCode.Text.Length - 4, 4);
        jzByShift.MaterType = hiddenMaterCode.Text.Substring(0, 3);
        jzByShift.LastJZ = 0;
        jzByShift.CurrentCL = 0;
        jzByShift.CurrentXH = 0;
        jzByShift.CurrentTZ = Convert.ToDecimal(txtTZWeight1.Text);
        jzByShift.CurrentJZ = jzByShift.CurrentTZ;

        jzByShiftManager.Insert(jzByShift);

        this.winAdd.Close();
        txtPlanDate1.Text = DateTime.Now.ToString("yyyy-MM-dd");
        cbxShiftID1.Value = "1";
        cbxWorkShopCode1.Value = "2";
        txtMaterialName1.Text = "";
        hiddenMaterCode.Text = "";
        txtTZWeight1.Text = "";
        X.Msg.Alert("提示", "添加成功！").Show();
        return;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winAdd.Close();
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string FID, string currentTZ)
    {
        //PpmRubberJZByShift injar = injarManager.GetById(Convert.ToInt32(JarID));
        //txtJarID2.Text = injar.JarID.ToString();
        PpmRubberJZByShift jzByShift = jzByShiftManager.GetById(Convert.ToInt32(FID));

        string IsAllowedAdd = jzByShiftManager.DataAllowAddJZ(jzByShift.PlanDate.ToString(), jzByShift.WorkShopCode.ToString(), jzByShift.ShiftID, jzByShift.MaterCode, "0");
        if (IsAllowedAdd != "OK")
        {
            X.Msg.Alert("提示", IsAllowedAdd).Show();
            return;
        }

        jzByShift.CurrentTZ = Convert.ToDecimal(currentTZ);
        jzByShift.CurrentJZ = jzByShift.LastJZ + jzByShift.CurrentCL - jzByShift.CurrentXH + jzByShift.CurrentTZ;

        jzByShiftManager.Update(jzByShift);

        pageToolBar.DoRefresh();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        //PpmRubberJZByShift injar = injarManager.GetById(Convert.ToInt32(txtJarID2.Text));
        //injar.ShiftID = Convert.ToInt16(cbxShiftID2.SelectedItem.Value);
        //injar.EquipCode = hiddenEquipCode.Text;
        //injarManager.Update(injar);
        //hiddenNoStorageID.Text = string.Empty;
        //hiddenEquipCode.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winModify.Close();
        X.MessageBox.Alert("操作", "更新成功").Show();
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmRubberJZByShift> pageParams = new PageResult<PpmRubberJZByShift>();
        pageParams.PageSize = -100;
        PageResult<PpmRubberJZByShift> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料结转");
    }

    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        //string storageName = field.Text;
        //EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.StorageName == storageName && BasStorage._.DeleteFlag == "0");
        //if (storageList.Count == 0)
        //{
        //    e.Success = true;
        //}
        //else
        //{
        //    if (storageList[0].StorageName.ToString() == hiddenStorageName.Text)
        //    {
        //        e.Success = true;
        //    }
        //    else
        //    {
        //        e.Success = false;
        //        e.ErrorMessage = "此库房名称已被使用！";
        //    }
        //}
    }
    #endregion
}
