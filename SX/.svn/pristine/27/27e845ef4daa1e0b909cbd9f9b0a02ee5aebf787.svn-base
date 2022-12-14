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
using System.Text;

public partial class Manager_ShopStorage_RubInJar : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            //查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            //历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            //导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            //修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            //删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            //恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            //添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        //public SysPageAction 查询 { get; private set; } //必须为 public
        //public SysPageAction 历史查询 { get; private set; } //必须为 public
        //public SysPageAction 导出 { get; private set; } //必须为 public
        //public SysPageAction 修改 { get; private set; } //必须为 public
        //public SysPageAction 删除 { get; private set; } //必须为 public
        //public SysPageAction 恢复 { get; private set; } //必须为 public
        //public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    private BasMaterialManager materManager = new BasMaterialManager();
    private BasEquipManager equipManager = new BasEquipManager();
    private PstmminjarManager injarManager = new PstmminjarManager();
    private BasUserManager userManager = new BasUserManager();
    private Pst_mminjarManager PM = new Pst_mminjarManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            //EntityArrayList<BasEquip> lstBasEquip = equipManager.GetListByWhere(BasEquip._.DeleteFlag == "0");
            //foreach (BasEquip m in lstBasEquip)
            //{
            //    Ext.Net.ListItem item = new Ext.Net.ListItem();
            //    item.Value = m.EquipCode;
            //    item.Text = m.EquipName;
            //    cbxEquip2.Items.Add(item);
            //}
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<Pstmminjar> GetPageResultData(PageResult<Pstmminjar> pageParams)
    {
        PstmminjarManager.QueryParams queryParams = new PstmminjarManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.storagePlaceId = hiddenStorageID.Text == "" ? "" : hiddenStorageID.Text + "001";
        queryParams.equipCode = hiddenEquipCode.Text;

        if (cbxShiftID.SelectedItem.Value != "all")
            queryParams.shiftID = cbxShiftID.SelectedItem.Value;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<Pstmminjar> GetTablePageDataBySql(PstmminjarManager.QueryParams queryParams)
    {
        PageResult<Pstmminjar> pageParams = queryParams.pageParams;//A.Usedweigh
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select A.JarID, A.Materbarcode, A.StockDate, A.MaterCode, B.MaterialName, A.EquipCode, C.EquipName, A.ShiftDate, 
                                    A.ShiftID, A.InTime, A.RealNum, A.RealWeight, A.handLename,  isnull(A.Usedweigh,'0') as Usedweigh, A.UsedFlag, 
                                    A.ClearFlag, A.AuditFlag, A.InaccountDuration, A.InaccountFlag,D.User_Name,  FeedjarNo as JarNum
                                from  Pstmminjar A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join sys_user D on A.handLename = D.User_id
                            
                                
                             
                                where A.DeleteFlag = '0'");
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        
        if (!string.IsNullOrEmpty(queryParams.shiftID))
        {
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
        }
        if (!string.IsNullOrEmpty(txtMaterName.Text))
        {
            sqlstr.AppendLine(" AND  B.MaterialName like '%" + txtMaterName.Text + "%'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.Intime >= '" + queryParams.beginDate.ToString("yyyy-MM-dd") + "'");
        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.Intime <= '" + queryParams.endDate.AddDays(1).ToString("yyyy-MM-dd") + "'");
        pageParams.QueryStr = sqlstr.ToString();
        //txtBarcode.Text = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = materManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return injarManager.GetPageDataBySql(pageParams);
            //return null;
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<Pstmminjar> pageParams = new PageResult<Pstmminjar>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "InTime DESC";

        PageResult<Pstmminjar> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {

        //DataSet ds = (DataSet)Session["RubInJar"];
      
        PageResult<Pstmminjar> pageParams = new PageResult<Pstmminjar>();
       
        pageParams.PageSize = 99999;
        pageParams.Orderfld = "InTime DESC";

        PageResult<Pstmminjar> lst = GetPageResultData(pageParams);
        DataSet ds = lst.DataSet;

        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = ds.Tables[0].Columns[i];
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
                ds.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "料仓投料");
       
    }
    #region 增删改查按钮激发的事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cbxShiftID1.SelectedItem.Value = "1";
        txtEquipName1.Text = string.Empty;
        hiddenEquipCode.Text = string.Empty;
        txtMaterialName1.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        txtMaterBarcode1.Text = string.Empty;
        txtRealNum1.Text = string.Empty;
        txtRealWeight1.Text = string.Empty;
        txtUserName1.Text = string.Empty;
        hiddenUserID.Text = string.Empty;
        TNum.Text = "1";
        Ctype.Text = "";
        this.winAdd.Show();
    }

    protected void btnAddSave_Click(object sender, EventArgs e)
    {
        Pst_mminjar mminjar = new Pst_mminjar();
        //mminjar.Materbarcode = txtMaterBarcode1.Text;
        //mminjar.StockDate = DateTime.Now;
        //mminjar.MaterCode = hiddenMaterCode.Text;
        //mminjar.EquipCode = hiddenEquipCode.Text;
        //mminjar.ShiftDate = DateTime.Now;
        //mminjar.StoragePlaceID = hiddenNoStorageID.Text + "001";
        //mminjar.ShiftID = Convert.ToInt16(cbxShiftID1.SelectedItem.Value);
        //mminjar.InTime = DateTime.Now;
        //mminjar.RealNum = Convert.ToInt32(txtRealNum1.Text);
        //mminjar.RealWeight = Convert.ToDecimal(txtRealWeight1.Text);
        //if (!string.IsNullOrEmpty(hiddenUserID.Text))
        //    mminjar.HandLename = hiddenUserID.Text;
        //else
        //    mminjar.HandLename = this.UserID;
        //mminjar.InaccountDuration = string.Format("{0:yyyyMM}", DateTime.Now);
        //mminjar.InaccountFlag = "0";
        //mminjar.DeleteFlag = "0";

        mminjar.Mater_barcode = txtMaterBarcode1.Text;
        mminjar.Stock_date = DateTime.Now.ToString("yyyy-MM-dd");
        mminjar.Mater_code = hiddenMaterCode.Text;
        mminjar.Equip_code = hiddenEquipCode.Text;
        mminjar.Shift_id = Convert.ToInt16(cbxShiftID1.SelectedItem.Value);
        mminjar.In_Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); ;
        mminjar.Real_num = Convert.ToInt32(txtRealNum1.Text);
        mminjar.Real_weight = Convert.ToDecimal(txtRealWeight1.Text);
        if (!string.IsNullOrEmpty(hiddenUserID.Text))
            mminjar.Handle_name = hiddenUserID.Text;
        else
            mminjar.Handle_name = this.UserID;
        mminjar.Ware_type = Ctype.Text;
        mminjar.Ware_num = int.Parse(TNum.Text);
        PM.Insert(mminjar);



        cbxShiftID1.SelectedItem.Value = "1";
        txtEquipName1.Text = string.Empty;
        txtStorageName1.Text = string.Empty;
        hiddenEquipCode.Text = string.Empty;
        txtMaterialName1.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        txtMaterBarcode1.Text = string.Empty;
        txtRealNum1.Text = string.Empty;
        txtRealWeight1.Text = string.Empty;
        txtUserName1.Text = string.Empty;
        hiddenUserID.Text = string.Empty;
        hiddenNoStorageID.Text = string.Empty;
        pageToolBar.DoRefresh();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string JarID)
    {
        Pstmminjar injar = injarManager.GetById(Convert.ToInt32(JarID));
        txtJarID2.Text = injar.JarID.ToString();
        txtBarcode2.Text = injar.Materbarcode;
        txtStockDate2.Text = injar.StockDate.ToString();
        if (!string.IsNullOrEmpty(injar.MaterCode))
            txtMaterName2.Text = materManager.GetMaterName(injar.MaterCode);
        if (injar.ShiftID.ToString() == "1")
        {
            cbxShiftID2.Text = "早";
            cbxShiftID2.Value = "1";
        }
        else if (injar.ShiftID.ToString() == "2")
        {
            cbxShiftID2.Text = "中";
            cbxShiftID2.Value = "2";
        }
        else if (injar.ShiftID.ToString() == "3")
        {
            cbxShiftID2.Text = "夜";
            cbxShiftID2.Value = "3";
        }
        txtEquipName2.Text = equipManager.GetListByWhere(BasEquip._.EquipCode == injar.EquipCode)[0].EquipName;
        hiddenEquipCode.Text = injar.EquipCode;
        txtRealNum2.Text = injar.RealNum.ToString();
        txtRealWeight2.Text = injar.RealWeight.ToString();
        if (!string.IsNullOrEmpty(injar.HandLename.Trim()))
        {
            BasUser u = userManager.GetListByWhere(BasUser._.HRCode == injar.HandLename)[0];
            if (u != null)
            {
                txtUserName2.Text = u.UserName;
            }
            //txtUserName2.Value = injar.HandLename;
            hiddenUserID.Text = injar.HandLename;
        }
        else
        {
            txtUserName2.Value = string.Empty;
            hiddenUserID.Text = string.Empty;
        }

        this.winModify.Show();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        Pstmminjar injar = injarManager.GetById(Convert.ToInt32(txtJarID2.Text));
        injar.ShiftID = Convert.ToInt16(cbxShiftID2.SelectedItem.Value);
        injar.EquipCode = hiddenEquipCode.Text;
        injar.RealNum = Convert.ToInt32(txtRealNum2.Text);
        injar.RealWeight = Convert.ToDecimal(txtRealWeight2.Text);
        injar.HandLename = hiddenUserID.Text;
        injarManager.Update(injar);
        hiddenNoStorageID.Text = string.Empty;
        hiddenEquipCode.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winModify.Close();
        X.MessageBox.Alert("操作", "更新成功").Show();
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchSend_Click()
    {
       
        return "审核成功";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string jarID)
    {
        try
        {

            PM.DeleteByWhere(Pst_mminjar._.Jar_id == jarID);

            pageToolBar.DoRefresh();

            return "删除成功";
          
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
    }
    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
       
    }
    #endregion
}