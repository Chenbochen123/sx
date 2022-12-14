﻿using System;
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

public partial class Manager_ShopStorage_Minstock : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
        
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
          
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
            放行 = new SysPageAction() { ActionID = 7, ActionName = "btnLock" };
            取消放行 = new SysPageAction() { ActionID = 7, ActionName = "btnUnLock" };
            质检 = new SysPageAction() { ActionID = 7, ActionName = "btnCheck" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
     
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
     
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 放行 { get; private set; } //必须为 public
        public SysPageAction 取消放行 { get; private set; } //必须为 public
        public SysPageAction 质检 { get; private set; } //必须为 public
    }
    #endregion

    private BasMaterialManager materManager = new BasMaterialManager();
    private BasEquipManager equipManager = new BasEquipManager();
    private Pst_MinstockManager PM = new Pst_MinstockManager();
    private PstShopStorageManager shopStorageManager = new PstShopStorageManager();
    private PstmmshopoutManager shopoutManager = new PstmmshopoutManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = "select * from pmt_factory";
            DataTable dt = PM.GetBySql(sql).ToDataSet().Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Ext.Net.ListItem li = new Ext.Net.ListItem(dr["Fac_name"].ToString(), dr["Fac_id"].ToString());
                AddFac.Items.Add(li);
                AddFac2.Items.Add(li);
                MoFac.Items.Add(li);
                MoFac2.Items.Add(li);
                
            }
            sql = "select * from SYS_USER";
             dt = PM.GetBySql(sql).ToDataSet().Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Ext.Net.ListItem li = new Ext.Net.ListItem(dr["USER_NAME"].ToString(), dr["Worker_barcode"].ToString());
                AddHr.Items.Add(li);
                MoHr.Items.Add(li);
             

            }
            sql = "select * from JCZL_store";
            dt = PM.GetBySql(sql).ToDataSet().Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Ext.Net.ListItem li = new Ext.Net.ListItem(dr["store_name"].ToString(), dr["store_num"].ToString());
                AddStock.Items.Add(li);
                MoStock.Items.Add(li);


            }
             Ext.Net.ListItem lt = new Ext.Net.ListItem("未放行", "0");
             MoFx.Items.Add(lt);
             lt = new Ext.Net.ListItem("放行", "1");
             MoFx.Items.Add(lt);

             lt = new Ext.Net.ListItem("不合格", "0");
             MoZj.Items.Add(lt);
             ComboBox7.Items.Add(lt);
             lt = new Ext.Net.ListItem("合格", "1");
             MoZj.Items.Add(lt); ComboBox7.Items.Add(lt);
             lt = new Ext.Net.ListItem("待检", "2");
             MoZj.Items.Add(lt); ComboBox7.Items.Add(lt);
             lt = new Ext.Net.ListItem("实验", "3");
             MoZj.Items.Add(lt); ComboBox7.Items.Add(lt);
             lt = new Ext.Net.ListItem("复检合格", "4");
             MoZj.Items.Add(lt); ComboBox7.Items.Add(lt);
        }
    }

    #region 分页相关方法
    private PageResult<Pstmmshopout> GetPageResultData(PageResult<Pstmmshopout> pageParams)
    {
        PstmmshopoutManager.QueryParams queryParams = new PstmmshopoutManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);


        queryParams.materCode = hiddenMaterCode.Text;


        return GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<Pstmmshopout> pageParams = new PageResult<Pstmmshopout>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "stock_date ASC";

        PageResult<Pstmmshopout> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        //aa.Text = sql;
        return new { data, total };
    }
    public PageResult<Pstmmshopout> GetTablePageDataBySql(PstmmshopoutManager.QueryParams queryParams)
    {
        PageResult<Pstmmshopout> pageParams = queryParams.pageParams;
        string sql = @" select top 100  case (Test_result) when '0' then '不合格' when '1' then '合格' when '2' then '待检' when '3' then '实验' when '4' then '复检合格' end as Result , 
case (Ep_sgn) when '0' then '' when '1' then '放行' end as sgn,Mater_name,Pst_Minstock.*,store.store_name,fac.Fac_fname,fac1.Fac_fname Fac_fname1,us.USER_NAME from Pst_Minstock
left join JCZL_store store on store.store_num= Pst_Minstock.Stock_code
left join  Pmt_factory fac  on fac.Fac_id=Pst_Minstock.Profac_name
left join  Pmt_factory fac1  on fac1.Fac_id=Pst_Minstock.Fac_id
left join SYS_USER us on us.Worker_barcode=Pst_Minstock.Receive_name

left join Pmt_material pm on Pst_Minstock.mater_code=pm.mater_code where stock_date >= '" + Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd") + "' and  stock_date <= '" + Convert.ToDateTime(txtEndTime.Text).ToString("yyyy-MM-dd") + "'";
        if (!string.IsNullOrEmpty(hiddenMaterCode.Text))
            sql = sql + " and  Pst_Minstock.mater_code = '" + hiddenMaterCode.Text + "'";
        //sql = sql + " order by stock_date";
        pageParams.QueryStr = sql;
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = shopoutManager.GetBySql(sql.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return shopoutManager.GetPageDataBySql(pageParams);
        }
    }
   
    #endregion

    #region 增删改查按钮激发的事件
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddStockDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        AddProDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        AddStock.Value = "";
        AddBatch.Text = "";
        AddFac.Value = "";
        AddFac2.Value = "";
        AddHr.Value = "";
        txtMaterialName1.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        AddNum.Text = "0";
        AddWeight.Text = "0";
        AddCargo_id.Text = "";
        AddRemark.Text = "";
        AddStand.Text = "";
     

        this.winAdd.Show();
    }

  

    protected void btnAddSave_Click(object sender, EventArgs e)
    {
        //X.Js.Alert(AddStock.Value.ToString());
       
        Pst_Minstock shopout = new Pst_Minstock();

      string bar =  DateTime.Now.ToString("yyMMdd");
      string sql = "select max(mater_barcode) from Pst_Minstock where mater_barcode like '"+bar+"%'";
   DataTable   dt = PM.GetBySql(sql).ToDataSet().Tables[0];
   int i = 1;
      try { i = int.Parse(dt.Rows[0][0].ToString().Substring(6,4))+1; }
      catch { i = 1; }
      string s = i.ToString();
      while (s.Length < 4) s = "0" + s;
      bar = bar + s;
      shopout.Mater_barcode = bar;

      shopout.Stock_date = DateTime.Parse(AddStockDate.Text).ToString("yyyy-MM-dd");
      shopout.Pro_Date = DateTime.Parse(AddProDate.Text).ToString("yyyy-MM-dd");
      shopout.Stock_code = short.Parse(AddStock.Value.ToString());
      shopout.Mater_batch = AddBatch.Text;
      shopout.Fac_id = int.Parse(AddFac.Value.ToString());
      shopout.Profac_name = int.Parse(AddFac2.Value.ToString());
      shopout.Receive_name = AddHr.Value.ToString();
      shopout.Handle_name = this.UserID;
      shopout.Mater_code = hiddenMaterCode.Text;
      shopout.Real_num = int.Parse(AddNum.Text.ToString());
      shopout.Real_weig = Decimal.Parse(AddWeight.Text.ToString());
      shopout.Cargo_id = AddCargo_id.Text;
      shopout.Mem_no = AddRemark.Text;
      shopout.Unit_Weight = Convert.ToDecimal(AddStand.Text);
      shopout.Test_result = "2";
      shopout.Ep_sgn = "0";
      shopout.Up_dwn_mark = 1;
      shopout.Bus_class = 10;
        PM.Insert(shopout);

        this.winAdd.Close();

        hiddenMaterCode.Text = string.Empty;
      

        pageToolBar.DoRefresh();
    }


    [Ext.Net.DirectMethod()]
    public string btnFxSend_Click()
    {

        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo = row.RecordID;


        }


        String sql = "update Pst_Minstock set Ep_sgn = '1' where Serial_Id = '" + strBillNo + "'  ";
        PM.GetBySql(sql).ToDataSet();
        this.AppendWebLog("原材料放行", "条码号：" + strBillNo);
        pageToolBar.DoRefresh();
        return "放行成功！";

    }
    [Ext.Net.DirectMethod()]
    public string btnFxSend_Click2()
    {

        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo = row.RecordID;


        }


        String sql = "update Pst_Minstock set Ep_sgn = '0' where Serial_Id = '" + strBillNo + "'  ";
        PM.GetBySql(sql).ToDataSet();
        this.AppendWebLog("取消原材料放行", "条码号：" + strBillNo);
        pageToolBar.DoRefresh();
        return "取消放行成功！";

    }
    [Ext.Net.DirectMethod()]
    public void btnFxSend_Click3()
    {

        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo = row.RecordID;


        }
        Pst_Minstock Mt = PM.GetById(strBillNo);
        TextField3.Text = strBillNo;
        ComboBox7.Value = Mt.Test_result.ToString();
        this.Window1.Show();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Window1.Close();
        this.winAdd.Close();
        this.winModify.Close();
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string ShopoutID)
    {
        txtchandi.Text = "";
        Pst_Minstock Mt = PM.GetById(ShopoutID);
        //PM.GetListByWhere(Pst_Minstock._.Serial_Id == ShopoutID)[0];
        if (!string.IsNullOrEmpty(Mt.Stock_date))
        {
            MoStockDate.Text = Mt.Stock_date;
        }
        if (!string.IsNullOrEmpty(Mt.Pro_Date))
        {
            MoProDate.Text = Mt.Pro_Date;
        }
        if (!string.IsNullOrEmpty(Mt.Stock_code.ToString()))
        {
            MoStock.Value = Mt.Stock_code.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Mater_batch))
        {
            MoBatch.Text = Mt.Mater_batch;
        }
        if (!string.IsNullOrEmpty(Mt.Fac_id.ToString()))
        {
            MoFac.Value = Mt.Fac_id.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Profac_name.ToString()))
        {
            MoFac2.Value = Mt.Profac_name.ToString();
        }
        if (string.IsNullOrEmpty(Mt.Receive_name))
        { MoHr.Value = null; }
        else
        {
            MoHr.Value = Mt.Receive_name.ToString();
        }
        //txtMaterName2.Text = materManager.GetListByWhere(BasMaterial._.MaterialCode == Mt.Mater_code)[0].MaterialName;
        EntityArrayList<BasMaterial> list = materManager.GetListByWhere(BasMaterial._.MaterialCode == Mt.Mater_code);
        if (list.Count > 0)
        { txtMaterName2.Text = list[0].MaterialName; }
        else { txtMaterName2.Text = null; }
        if (!string.IsNullOrEmpty(Mt.Real_num.ToString()))
        {
            MoNum.Text = Mt.Real_num.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Real_weig.ToString()))
        {
            MoWeight.Text = Mt.Real_weig.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Cargo_id))
        {
            MoCargo_id.Text = Mt.Cargo_id.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Mem_no))
        {
            MoRemark.Text = Mt.Mem_no.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Unit_Weight.ToString()))
        {
            MoStand.Text = Mt.Unit_Weight.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Ep_sgn))
        {
            MoFx.Value = Mt.Ep_sgn.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Test_result))
        {
            MoZj.Value = Mt.Test_result.ToString();
        }
        if (!string.IsNullOrEmpty(Mt.Area_Code))
        {
            txtchandi.Text = Mt.Area_Code.ToString();
        }



        this.winModify.Show();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        Pst_Minstock shopout = PM.GetById(hiddenSerialID.Text);

          shopout.Stock_date = DateTime.Parse(MoStockDate.Text).ToString("yyyy-MM-dd");
          shopout.Pro_Date = DateTime.Parse(MoProDate.Text).ToString("yyyy-MM-dd");
          shopout.Stock_code = short.Parse(MoStock.Value.ToString());
          shopout.Mater_batch = MoBatch.Text;
          shopout.Fac_id = int.Parse(MoFac.Value.ToString());
          shopout.Profac_name = int.Parse(MoFac2.Value.ToString());
          shopout.Receive_name = MoHr.Value.ToString();
          shopout.Handle_name = this.UserID;

          shopout.Real_num = int.Parse(MoNum.Text.ToString());
          shopout.Real_weig = Decimal.Parse(MoWeight.Text.ToString());
          shopout.Cargo_id = MoCargo_id.Text;
          shopout.Mem_no = MoRemark.Text;
          shopout.Unit_Weight = Convert.ToDecimal(MoStand.Text);
          shopout.Test_result = MoZj.Value.ToString();
          shopout.Ep_sgn = MoFx.Value.ToString();
          shopout.Area_Code = txtchandi.Text;
          PM.Update(shopout);



        pageToolBar.DoRefresh();
        this.winModify.Close();
        X.MessageBox.Alert("操作", "更新成功").Show();
    }
    protected void btnModify_Click2(object sender, EventArgs e)
    {
        Pst_Minstock shopout = PM.GetById(TextField3.Text);


        shopout.Test_result = ComboBox7.Value.ToString();
    
        PM.Update(shopout);



        pageToolBar.DoRefresh();
        this.Window1.Close();
        X.MessageBox.Alert("操作", "更新成功").Show();
    }
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string ShopoutID)
    {
        try
        {
            PM.DeleteByWhere(Pst_Minstock._.Serial_Id == ShopoutID);

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

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {


        string sql = @" select top 100  case (Test_result) when '0' then '不合格' when '1' then '合格' when '2' then '待检' when '3' then '实验' when '4' then '复检合格' end as Result , 
case (Ep_sgn) when '0' then '' when '1' then '放行' end as sgn,Mater_name,Pst_Minstock.* from Pst_Minstock

left join Pmt_material pm on Pst_Minstock.mater_code=pm.mater_code where stock_date >= '" + Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd") + "' and  stock_date <= '" + Convert.ToDateTime(txtEndTime.Text).ToString("yyyy-MM-dd") + "'";
        if (!string.IsNullOrEmpty(hiddenMaterCode.Text))
            sql = sql + " and  Pst_Minstock.mater_code = '" + hiddenMaterCode.Text + "'";
        DataSet ds = PM.GetBySql(sql).ToDataSet();

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
        //data.Columns[0].ColumnName = "车间";
        //"车间原料消耗"
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "仓库入库单");
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_detail(string ObjID)
    {
        DataSet ds = PM.GetBySql("select t.Mater_barcode,t1.Mater_name,t.Pro_Date,t2.Fac_name  from Pst_Minstock t left join Pmt_material t1 on t1.Mater_code = t.Mater_code left join Pmt_factory t2 on t2.Fac_id=t.Fac_id where Serial_Id = '" + ObjID + "'").ToDataSet();
        ds.Tables[0].TableName = "Minstock";
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            winDetail.Hidden = false;
            //加载模板
            FastReport.Report report = this.WebReport.Report;
            report.Load(Server.MapPath("../ShopStorage/Minstock.frx"));
            report.SetParameterValue("Mater_barcode", ds.Tables[0].Rows[0]["Mater_barcode"].ToString());
            //绑定信息
            report.RegisterData(ds.Tables[0], "Minstock");
            report.Refresh();
            WebReport.Update();
            WebReport.Refresh();
        }
    }
}