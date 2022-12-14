using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_ShopStorage_PlanGetMaterial : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };

           
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
      
       
    }
    #endregion

    protected BasWorkShopManager basWorkShopManager = new BasWorkShopManager();
    protected PstPlanGetMaterManager getMaterManager = new PstPlanGetMaterManager();

    private const string constSelectAllText = "全部";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 绑定设备类型
            Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, "0");
            EntityArrayList<BasWorkShop> lstBasWorkShop = basWorkShopManager.GetListByWhere(BasWorkShop._.DeleteFlag == "0");
            cboType.Items.Clear();
            cboType.Items.Add(allitem);
            foreach (BasWorkShop m in lstBasWorkShop)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkShopName;
                cboType.Items.Add(item);
            }
            //cboType.Text = constSelectAllText;
            cboType.Value = "0";
            #endregion

            DF1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DF2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DF3.Text = DateTime.Now.ToString("yyyy-MM-dd");
            hiddenNowDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            hiddenAtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            CS1.Value = "1"; CS2.Value = "2"; CS3.Value = "3";
        }
    }

    #region 分页相关方法
  

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
   


        string sql = " exec Proc_SumDayMater1 '2019-06-18','1','2019-06-18','2','2019-06-18','3','all',0 ";
        if (cbxMaterialype.Value == "all" || string.IsNullOrEmpty(cbxMaterialype.Value.ToString()))
        {
            sql = "exec Proc_SumDayMater '" + Convert.ToDateTime(DF1.Text).ToString("yyyy-MM-dd") + "','" + CS1.Value + "','" + Convert.ToDateTime(DF2.Text).ToString("yyyy-MM-dd") + "','" + CS2.Value + "','" + Convert.ToDateTime(DF3.Text).ToString("yyyy-MM-dd") + "','" + CS3.Value + "'," + cboType.Value.ToString();
        
        
        }
        else

            sql = "exec Proc_SumDayMater1 '" + Convert.ToDateTime(DF1.Text).ToString("yyyy-MM-dd") + "','" + CS1.Value + "','" + Convert.ToDateTime(DF2.Text).ToString("yyyy-MM-dd") + "','" + CS2.Value + "','" + Convert.ToDateTime(DF3.Text).ToString("yyyy-MM-dd") + "','" + CS3.Value + "','" + cbxMaterialype.Value + "'," + cboType.Value.ToString();
        


        DataTable data = getMaterManager.GetBySql(sql).ToDataSet().Tables[0];

        int total = data.Rows.Count;
        //aa.Text = sql;
        return new { data, total };
    }
    #endregion

 
    [Ext.Net.DirectMethod()]
    public void btnGet()
    {
        getMaterManager.ReSetMater(Convert.ToDateTime(DF1.Text).ToString("yyyy-MM-dd"));
        pageToolBar.DoRefresh();
        X.MessageBox.Alert("提示", "信息获取成功!").Show();
        //txtEquipName2.Text = string.Empty;
    }
    public void btnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            PstPlanGetMater planMater = new PstPlanGetMater();
            planMater.PlanDate = Convert.ToDateTime(DF1.Text);
            planMater.StorageID = hiddenStorageID.Text;
            planMater.EquipCode = hiddenEquipCodeAdd.Text;
            planMater.MaterialCode = hiddenMaterCode.Text;
            planMater.TotalWeight = Convert.ToDecimal(txtPlanWeight2.Text);
            planMater.UserID = this.UserID;
            planMater.SourcePlace = this.txtSourcePlace2.Text.Trim();
            planMater.Remark = this.txtRemark2.Text.Trim();
            //getMaterManager.Insert(planMater);
            string sql = string.Empty;
            sql = @"insert into PstPlanGetMaterNew (PlanDate,StorageID,EquipCode,MaterialCode,TotalWeight,DeleteFlag,Remark,SourcePlace,UserID) values ('";
            sql += planMater.PlanDate.ToString("yyyy-MM-dd") + "','" + planMater.StorageID + "','" + planMater.EquipCode + "','" + planMater.MaterialCode + "','" + planMater.TotalWeight + "','0','" + planMater.Remark + "','" + planMater.SourcePlace + "','" + planMater.UserID + "')";
            getMaterManager.GetBySql(sql).ExecuteNonQuery();
      
            txtStorageName2.Text = string.Empty;
            txtEquipName2.Text = string.Empty;
            txtMaterName2.Text = string.Empty;
            txtPlanWeight2.Text = string.Empty;
            hiddenStorageID.Text = string.Empty;
            hiddenEquipCodeAdd.Text = string.Empty;
            hiddenMaterCode.Text = string.Empty;
            txtSourcePlace2.Text = string.Empty;
            txtRemark2.Text = string.Empty;
            pageToolBar.DoRefresh();
            this.winAdd.Close();

            X.MessageBox.Alert("提示", "添加成功").Show();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("提示", "信息添加失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
       
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
       
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        return "";
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        hiddenStorageID.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        hiddenEquipCodeAdd.Text = string.Empty;
        this.winAdd.Close();
        this.winModify.Close();
    }

 
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        string sql = " exec Proc_SumDayMater1 '2019-06-18','1','2019-06-18','2','2019-06-18','3','all',0 ";
        if (cbxMaterialype.Value == "all" || string.IsNullOrEmpty(cbxMaterialype.Value.ToString()))
        {
            sql = "exec Proc_SumDayMater '" + Convert.ToDateTime(DF1.Text).ToString("yyyy-MM-dd") + "','" + CS1.Value + "','" + Convert.ToDateTime(DF2.Text).ToString("yyyy-MM-dd") + "','" + CS2.Value + "','" + Convert.ToDateTime(DF3.Text).ToString("yyyy-MM-dd") + "','" + CS3.Value + "'," + cboType.Value.ToString();


        }
        else

            sql = "exec Proc_SumDayMater1 '" + Convert.ToDateTime(DF1.Text).ToString("yyyy-MM-dd") + "','" + CS1.Value + "','" + Convert.ToDateTime(DF2.Text).ToString("yyyy-MM-dd") + "','" + CS2.Value + "','" + Convert.ToDateTime(DF3.Text).ToString("yyyy-MM-dd") + "','" + CS3.Value + "','" + cbxMaterialype.Value + "'," + cboType.Value.ToString();


        DataSet ds = getMaterManager.GetBySql(sql).ToDataSet();
        DataTable data = ds.Tables[0];
        for (int i = 0; i < data.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = data.Columns[i];
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
                data.Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "计划领料表");
    }

  

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
      
    }

    protected void CheckCombo(object sender, RemoteValidationEventArgs e)
    {
        ComboBox combo = (ComboBox)sender;

        if (combo.SelectedItem.Value != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须选择！";
        }
    }
    #endregion
}