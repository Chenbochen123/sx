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

public partial class Manager_Equipment_StopManage_MixerFault_MixerFaultMain : Mesnac.Web.UI.Page
{
    protected EqmMixerFaultManager manager = new EqmMixerFaultManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<EqmMixerFault> entityList;


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            txtBeginDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtBeginTime.Text = "00:00:00";
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = "00:00:00";
            String sql = "  select distinct AlarmState  from EqmMixerFault";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            Ext.Net.ListItem allitem = new Ext.Net.ListItem("全部", "全部");
            txtAlarmState.Items.Clear();
            txtAlarmState.Items.Add(allitem);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(dr[0].ToString(), dr[0].ToString());
                txtAlarmState.Items.Add(item);
            }
            if (txtAlarmState.Items.Count > 0)
            {
                txtAlarmState.Text = (txtAlarmState.Items[0].Value);
            }
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<EqmMixerFault> GetPageResultData(PageResult<EqmMixerFault> pageParams)
    {
        EqmMixerFaultManager.QueryParams queryParams = new EqmMixerFaultManager.QueryParams();
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
        queryParams.faultBeginDate = beginTime.ToString("yyyy-MM-dd HH:mm:ss");
        queryParams.faultEndDate = endTime.ToString("yyyy-MM-dd HH:mm:ss");
        queryParams.equipCode = hidden_equip_code.Value.ToString();
        queryParams.faultName = txt_fault_name.Text.TrimEnd().TrimStart();
        if (txtAlarmState.Text != "全部")
        queryParams.AlarmState = txtAlarmState.Text;
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return "";
        //}
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmMixerFault> pageParams = new PageResult<EqmMixerFault>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<EqmMixerFault> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<EqmMixerFault> pageParams = new PageResult<EqmMixerFault>();
        pageParams.PageSize = -100;
        PageResult<EqmMixerFault> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "密炼机故障信息");
    }
    #endregion

}