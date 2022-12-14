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
using Mesnac.Web.UI;

public partial class Manager_Storage_StorageAlarm : BasePage
{
    //System.Web.UI.Page
    protected PstStorageManager storageManager = new PstStorageManager();
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 1, ActionName = "" };
            备注 = new SysPageAction() { ActionID = 2, ActionName = "Button1" };
        
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 备注 { get; private set; } //必须为 public
     
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            cbxAlarmFlag.Value = "0";
        }
    }

    private PageResult<PstStorage> GetPageResultData(PageResult<PstStorage> pageParams)
    {
        PstStorageManager.QueryParams queryParams = new PstStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        //queryParams.storageID = hiddenStorageID.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.alarmFlag = cbxAlarmFlag.SelectedItem.Value;
        
        return GetTablePageDataBySql2(queryParams);
    }

    public PageResult<PstStorage> GetTablePageDataBySql2(PstStorageManager.QueryParams queryParams)
    {
        PageResult<PstStorage> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select A.MaterCode, C.MaterialName, SUM(RealWeight) TotalWeight, C.MaxStock, C.MinStock,
             sum(       case when       CONVERT(varchar(10), DATEADD(DAY, c.ValidDate, A.ProcDate), 120) < getdate() then RealWeight else 0 end) as GFlag,
	                 sum(       case when  (     CONVERT(varchar(10), DATEADD(DAY, c.ValidDate, A.ProcDate), 120) >= getdate() and CONVERT(varchar(10), DATEADD(DAY, c.ValidDate-30, A.ProcDate), 120) < getdate()) then RealWeight else 0 end) as BFlag,
	                                case when SUM(RealWeight) > MaxStock and MaxStock > 0 then '1' when SUM(RealWeight) < MinStock then '2' else '0' end AlarmFlag
,PstStorageRemark.Reamrk
                                from PstStorage A
	                                left join BasMaterial C on A.MaterCode = C.MaterialCode
left join PstStorageRemark on A.MaterCode=PstStorageRemark.MaterialCode
                                where RealWeight > 0");
        if (!string.IsNullOrEmpty(queryParams.storageID))
        {
            sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }
        sqlstr.AppendLine(" group by A.MaterCode, C.MaterialName, C.MaxStock, C.MinStock,PstStorageRemark.Reamrk");
        if (queryParams.alarmFlag == "0")
            sqlstr.AppendLine(" having (SUM(RealWeight) > MaxStock and MaxStock > 0) or (SUM(RealWeight) < MinStock)");
        else if (queryParams.alarmFlag == "1")
            sqlstr.AppendLine(" having SUM(RealWeight) > MaxStock and MaxStock > 0");
        else if (queryParams.alarmFlag == "2")
            sqlstr.AppendLine(" having SUM(RealWeight) < MinStock");
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = storageManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return storageManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstStorage> pageParams = new PageResult<PstStorage>();
        String s = ""; DataTable data; PageResult<PstStorage> lst;
        if (pageParams.PageIndex == 1) { 
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = 9999;
        pageParams.Orderfld = "MaterCode asc";

         lst = GetPageResultData(pageParams);
         data = lst.DataSet.Tables[0];
         s = "";
        foreach (DataRow dr in data.Rows)
        {
            if (String.IsNullOrEmpty(dr["MaxStock"].ToString()) || String.IsNullOrEmpty(dr["MinStock"].ToString()))
            {
                s = s + dr["MaterialName"].ToString() + "需要设置库存信息！ ";
            }


        }
        if (s.Length > 0)
        { X.Js.Alert(s); }}

      

    





        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "MaterCode asc";

          lst = GetPageResultData(pageParams);
         data = lst.DataSet.Tables[0];

         //移除正常库存备注
         IPptShiftManager bPptShiftManager = new PptShiftManager();
         String sql = "select * from PstStorageRemark";
         DataSet ds = bPptShiftManager.GetBySql(sql).ToDataSet();
         if (ds.Tables[0].Rows.Count > 0)
         {

             foreach (DataRow dr2 in ds.Tables[0].Rows)
             {
                 int i = 0;
                 string mcode = dr2["MaterialCode"].ToString();

                 foreach (DataRow dr in data.Rows)
                 {
                     if (mcode == dr["MaterCode"].ToString())
                     {
                         i = 1; break;
                     
                     }
                 }
                 if (i == 0)
                 { sql = "delete from PstStorageRemark  where materialcode = '" + mcode + "'"; bPptShiftManager.GetBySql(sql).ToDataSet(); }


             }

         }







        //foreach (DataRow dr in data.Rows)
        //{
        //    if (String.IsNullOrEmpty(dr["MaxStock"].ToString()) || String.IsNullOrEmpty(dr["MinStock"].ToString()))
        //    {
        //        s = s + dr["MaterialName"].ToString() + "需要设置库存信息！ ";
        //    }
        
        
        //}
        //if(s.Length>0)
        //{X.Js.Alert(s);}
        int total = lst.RecordCount;
        return new { data, total };
    }

    public void cbxAlarmFlag_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }


    protected void ButtonSH_Click(object sender, DirectEventArgs e)
    {
        if (rowSelectMuti.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要放行的记录").Show();
            return;
        }
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        string MaterCode = rowSelectMuti.SelectedRecordID;
        TxtMatercode.Text = MaterCode;
        string sql = "select * from basmaterial where materialcode = '" + MaterCode + "'";
        DataSet ds = bPptShiftManager.GetBySql(sql).ToDataSet();
        TxtMaterialName.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();
        TxtRemark.Text = "";
        WindowCY.Show ();
    

    }

    protected void ButtonEditCY_Click(object sender, DirectEventArgs e)
    {
        
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        String sql = "delete from PstStorageRemark where materialcode = '" + TxtMatercode.Text + "'";
          DataSet ds = bPptShiftManager.GetBySql(sql).ToDataSet();
          sql = "insert into PstStorageRemark values('" + TxtMatercode.Text + "','" + TxtRemark.Text + "')";
          ds = bPptShiftManager.GetBySql(sql).ToDataSet();
          X.Msg.Alert("提示", "备注成功").Show();
          WindowCY.Close();
          X.Js.AddScript("  App.pageToolBar.doRefresh();");
    }
}