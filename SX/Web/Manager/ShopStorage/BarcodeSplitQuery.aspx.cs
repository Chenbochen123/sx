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

public partial class Manager_ShopStorage_BarcodeSplitQuery : Mesnac.Web.UI.Page
{
    protected PstMaterialRubberSplitManager splitManager = new PstMaterialRubberSplitManager();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtStorageName.Text) || string.IsNullOrEmpty(txtBarcode.Text))
        {
            X.Msg.Alert("提示", "请选择库房并输入条码号！").Show();
            return;
        }
        DataSet ds = splitManager.GetBarcodeSplitQuery(txtBarcode.Text, hiddenStorageID.Text, hiddenStoragePlaceID.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtBarcode1.Text = txtBarcode.Text;
            txtMaterName1.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();
            txtStorageName1.Text = txtStorageName.Text;
            txtStoragePlaceName1.Text = txtStoragePlaceName.Text;
            txtStoreInWeight1.Text = ds.Tables[0].Rows[0]["StoreInWeight"].ToString();
            txtStoreOutWeight1.Text = ds.Tables[0].Rows[0]["StoreOutWeight"].ToString();
            txtRealWeight1.Text = ds.Tables[0].Rows[0]["RealWeight"].ToString();
            txtChaiWeight1.Text = ds.Tables[0].Rows[0]["ChaiWeight"].ToString();
            txtUnChaiWeight1.Text = ds.Tables[0].Rows[0]["UnChaiWeight"].ToString();
        }
        else
        {
            txtBarcode1.Text = string.Empty;
            txtStorageName1.Text = string.Empty;
            txtStoragePlaceName1.Text = string.Empty;
            txtStoreInWeight1.Text = string.Empty;
            txtStoreOutWeight1.Text = string.Empty;
            txtRealWeight1.Text = string.Empty;
            txtChaiWeight1.Text = string.Empty;
            txtUnChaiWeight1.Text = string.Empty;

            X.Msg.Alert("提示", "无数据，请检查查询条件！").Show();
            return;
        }
    }
}