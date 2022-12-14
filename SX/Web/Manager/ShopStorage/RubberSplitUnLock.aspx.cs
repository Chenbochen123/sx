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
public partial class Manager_ShopStorage_RubberSplitUnLock : Mesnac.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstMaterialRubberSplitManager splitManager = new PstMaterialRubberSplitManager();
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            解锁 = new SysPageAction() { ActionID = 2, ActionName = "Button1" };

        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 解锁 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private PageResult<PstMaterialRubberSplit> GetPageResultData(PageResult<PstMaterialRubberSplit> pageParams)
    {
        PstMaterialRubberSplitManager.QueryParams queryParams = new PstMaterialRubberSplitManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.shelfid = this.txtshelf.Text;
        return splitManager.GetTableSplitUnLock(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialRubberSplit> pageParams = new PageResult<PstMaterialRubberSplit>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PstMaterialRubberSplit> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenBarcodeSplit.Text = e.ExtraParams["BarcodeSplit"];
    }

    protected void Search_Change(object sender, EventArgs e)
    {
        this.pageToolBar.DoRefresh();
    }

    protected void btnUnLock_Click(object sender, DirectEventArgs e)
    {
        if (string.IsNullOrEmpty(hiddenBarcodeSplit.Text))
        {
            X.Msg.Alert("错误提示", "请选择一个条码号").Show();
        }
        DataTable dt = splitManager.ProcUnReset(hiddenBarcodeSplit.Text.Trim(), this.UserID).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.pageToolBar.DoRefresh();
                X.Msg.Alert("提示", "解锁成功！").Show();
            }
            else
                X.Msg.Alert("提示", dt.Rows[0][0].ToString()).Show();
        }
        else
        {
            X.Msg.Alert("提示", "解锁失败！").Show();
        }

    }
    protected void btnAdd_Click(object sender, DirectEventArgs e)
    {
        if (string.IsNullOrEmpty(txtBarcode.Text))
        {
            X.Msg.Alert("错误提示", "请选择一个条码号").Show(); return;
        }
        if (string.IsNullOrEmpty(txtshelf.Text))
        {
            X.Msg.Alert("错误提示", "请输入车架子号").Show(); return;
        }
        if (txtBarcode.Text.Length != 25)
        {
            X.Msg.Alert("错误提示", "条码号必须为25位").Show(); return;
        }

        if(txtshelf.Text.Length!=9)
        {
            X.Msg.Alert("错误提示", "架子号必须为9位").Show(); return;
        }
        String barcode = txtBarcode.Text;
        String llshelfid = txtshelf.Text;
        String sql = "select * from dbo.BasLLShelf  where barcode = '" + barcode+"'";
        DataSet ds = splitManager.GetBySql(sql).ToDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        { X.Msg.Alert("错误提示", "条码存在架子信息 不能补录").Show(); return; }

        sql = "select * from dbo.BasLLShelf  where llshelfid = '" + llshelfid + "'";
         ds = splitManager.GetBySql(sql).ToDataSet();
         if (ds.Tables[0].Rows.Count == 0)
         {
             sql = "insert into BasLLShelf (LLShelfID,PrintDate,PrintNum,BarCode,FState,UpLoadDate,DownLoadDate) values ('" + llshelfid + "',getdate(),1,'" + barcode + "',1,null, convert(varchar(19),getdate(),121))";
             ds = splitManager.GetBySql(sql).ToDataSet();
             this.AppendWebLog("车架信息补录" + llshelfid, barcode);
             X.Msg.Alert("成功提示", "插入补录成功").Show(); return;

         }
         else
         {
             if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Barcode"].ToString()))
             {

                 sql = "update BasLLShelf set barocde = '" + barcode + "' , PrintDate= getdate(),PrintNum = '1',FState='1',DownLoadDate = convert(varchar(19),getdate(),121) where  llshelfid = '" + llshelfid + "'";
                 ds = splitManager.GetBySql(sql).ToDataSet();
                 this.AppendWebLog("车架信息补录" + llshelfid, barcode);
                 X.Msg.Alert("成功提示", "补录成功" ).Show(); return; 
             
             
             }
             else
             { X.Msg.Alert("错误提示", "此车架有其他条码 不能补录").Show(); return; }
         
         
         
         
         }


      

    }
}