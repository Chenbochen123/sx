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
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using Mesnac.Business.Interface;
public partial class Manager_Technology_RubWeightSetting_RubWeightScanBarcodeLog : Mesnac.Web.UI.Page
{
    protected IPptScanBarcodeLogManager manager = new PptScanBarcodeLogManager();//业务对象

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((!X.IsAjaxRequest)&&(!this.IsPostBack))
        {
            txtBeginDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtBeginTime.Text = "00:00:00";
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = "23:59:59";

            txt_info_type.Items.Add(new Ext.Net.ListItem("正常扫描", "1"));
            txt_info_type.Items.Add(new Ext.Net.ListItem("异常扫描", "0"));
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<PptScanBarcodeLog> GetPageResultData(PageResult<PptScanBarcodeLog> pageParams)
    {
        PptScanBarcodeLogManager.QueryParams queryParams = new PptScanBarcodeLogManager.QueryParams();
        queryParams.pageParams = pageParams;
        DateTime beginTime = DateTime.Now;
        try
        {
            beginTime = Convert.ToDateTime(((DateTime)txtBeginDate.Value).ToString("yyyy-MM-dd") + " " + txtBeginTime.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的开始时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
        }
        DateTime endTime = DateTime.Now;
        try
        {
            endTime = Convert.ToDateTime(((DateTime)txtEndDate.Value).ToString("yyyy-MM-dd") + " " + txtEndTime.Text);
        }
        catch
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请填写正确的结束时间！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
        }
        queryParams.dtBegin = beginTime.ToString("yyyy-MM-dd HH:mm:ss");
        queryParams.dtEnd = endTime.ToString("yyyy-MM-dd HH:mm:ss");
        queryParams.scanBarCode = txt_scan_barcode.Text;
        queryParams.equipCode = hidden_equip_code.Value.ToString();
        queryParams.MateName = MateName.Text;
        queryParams.ProName = ProName.Text;
        string cbxs = cbxst.Value.ToString();
        if(cbxs=="-1"||cbxs=="全部")
        {
            cbxs = "";
        }
        queryParams.scanUsedBarMsg = cbxs;
        if (txt_info_type.SelectedItem != null)
        {
            
            queryParams.infoType = txt_info_type.SelectedItem.Value;
            
        }
        
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptScanBarcodeLog> pageParams = new PageResult<PptScanBarcodeLog>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<PptScanBarcodeLog> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PptScanBarcodeLog> pageParams = new PageResult<PptScanBarcodeLog>();
        pageParams.PageSize = -100;
        PageResult<PptScanBarcodeLog> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料称扫描日志报表");
    }
}