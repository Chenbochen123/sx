using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
public partial class Manager_ProducingPlan_PlanSumDayMater_PlanSumDayMater : Mesnac.Web.UI.Page
{
    #region 属性注入
    IPptPlanManager pptPlanManager = new PptPlanManager();
    protected IBasWorkShopManager basWorkShopManager = new BasWorkShopManager();
    private const string constSelectAllText = "---请选择---";
    #endregion
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 4, ActionName = "btnExcel" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 设备类型
            Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
            
            EntityArrayList<BasWorkShop> lstBasWorkShop = basWorkShopManager.GetListByWhere(BasWorkShop._.DeleteFlag == "0");
            cboType.Items.Clear();
            //cboType.Items.Add(allitem);
            foreach (BasWorkShop m in lstBasWorkShop)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkShopName;
                cboType.Items.Add(item);
            }
            if (cboType.Items.Count > 0)
            {
                cboType.Text = (cboType.Items[0].Value);
            }
            #endregion
            txtStratShiftDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        string type="00";
        string store="";
        //if (!String.IsNullOrEmpty(cboType.Text))
        //{
        //    type = this.cboType.SelectedItem.Value;
        //}
        type = cboType.Text.Replace(constSelectAllText, "");
        if (!String.IsNullOrEmpty(hidden_select_store_code.Text))
        {
            store=this.hidden_select_store_code.Text;
        }
        DataTable data = pptPlanManager.GetSumDayMater(Convert.ToDateTime(this.txtStratShiftDate.Text).ToString("yyyy-MM-dd"), type,store);
        int total = data.Rows.Count;
        return new { data, total };
    }

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// sunyj 2013年4月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        string type = "00";
        //huiw,2013-10-24注释
        //if (!String.IsNullOrEmpty(cboType.Text))
        //{
        //    type = this.cboType.SelectedItem.Value;
        //}
        //huiw,2013-10-24添加
        type = cboType.Text.Replace(constSelectAllText, "");
        string store = "";
        if (!String.IsNullOrEmpty(hidden_select_store_code.Text))
        {
            store = this.hidden_select_store_code.Text;
        }
        DataTable data = pptPlanManager.GetSumDayMater(Convert.ToDateTime(this.txtStratShiftDate.Text).ToString("yyyy-MM-dd"),type,store);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(data, "生产计划领料单");
    }
    #endregion
}