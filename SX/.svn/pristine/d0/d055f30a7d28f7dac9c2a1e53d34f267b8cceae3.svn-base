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
using System.Data;
using System.Text.RegularExpressions;
using Mesnac.Business.Interface;

public partial class Manager_BasicInfo_WorkInfo_EquipProductInfoInfo : Mesnac.Web.UI.Page
{
    protected BasEquipManager equipmanager = new BasEquipManager();//业务对象
    protected PptShiftManager shiftmanager = new PptShiftManager();//业务对象
    protected PptShiftConfigManager shiftConfigmanager = new PptShiftConfigManager();//业务对象

    protected BasWorkManager manager = new BasWorkManager();//业务对象
    protected BasWorkCoefficientManager coefficientmanager = new BasWorkCoefficientManager();//业务对象
    protected BasWorkUserInfoManager workUserInfomanager = new BasWorkUserInfoManager();//业务对象
    protected PptPlanManager pptPlanmanager = new PptPlanManager();//业务对象

    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasWork> entityList;


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            删除 = new SysPageAction() { ActionID = 2, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
            查询 = new SysPageAction() { ActionID = 4, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 5, ActionName = "btn_history_search" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            导出 = new SysPageAction() { ActionID = 7, ActionName = "btnExport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
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
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 岗位名称
            txtStratDate.Text = DateTime.Now.ToString("yy-MM-dd");
            txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yy-MM-dd");
            EntityArrayList<BasEquip> lstEquip = equipmanager.GetListByWhereAndOrder(BasEquip._.EquipType == "01", BasEquip._.EquipName.Asc);
            EntityArrayList<PptShift> lstShift = shiftmanager.GetListByWhere(PptShift._.UseFlag == "1");
            EntityArrayList<BasWork> lstWork = manager.GetListByWhereAndOrder(BasWork._.DeleteFlag == "0",BasWork._.WorkName.Asc);
            cmoShiftID.Items.Clear();
            Ext.Net.ListItem it = new Ext.Net.ListItem();
            it.Value = "0";
            it.Text = "——全部——";
            cmoShiftID.Items.Add(it);
            foreach (PptShift m in lstShift)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.ShiftName;
                cmoShiftID.Items.Add(item);
            }
            if (cmoShiftID.Items.Count > 0)
            {
                cmoShiftID.Text = (cmoShiftID.Items[0].Value);
            }


            #endregion
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasWorkUserInfo> GetPageResultData(PageResult<BasWorkUserInfo> pageParams)
    {
        BasWorkUserInfoManager.QueryParams queryParams = new BasWorkUserInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.startpdtDate = txtStratDate.Text.TrimEnd().TrimStart();
        queryParams.endpdtDate = txtEndDate.Text.TrimEnd().TrimStart();
        queryParams.recordWorkBarcode = txtRecordWorkBarcode.Text.TrimEnd().TrimStart();
        if (string.IsNullOrEmpty(queryParams.recordWorkBarcode))
        {
            queryParams.recordWorkBarcode = "000000";
        }
        queryParams.shiftID = cmoShiftID.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return workUserInfomanager.GetTablePageDataBySql(queryParams);
    }

    private DataSet GetPageResultData_ds(PageResult<BasWorkUserInfo> pageParams)
    {
        BasWorkUserInfoManager.QueryParams queryParams = new BasWorkUserInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.startpdtDate = txtStratDate.Text.TrimEnd().TrimStart();
        queryParams.endpdtDate = txtEndDate.Text.TrimEnd().TrimStart();
        queryParams.recordWorkBarcode = txtRecordWorkBarcode.Text.TrimEnd().TrimStart();
        if (string.IsNullOrEmpty(queryParams.recordWorkBarcode))
        {
            queryParams.recordWorkBarcode = "000000";
        }
        queryParams.shiftID = cmoShiftID.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return workUserInfomanager.UserQueryByCode(queryParams);
    }

    private PageResult<PptShiftConfig> GetPageResultData2(PageResult<PptShiftConfig> pageParams)
    {
        PptShiftConfigManager.QueryParams queryParams = new PptShiftConfigManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.startPlanDate = txtStratDate.Text.TrimEnd().TrimStart();
        queryParams.endPlanDate = txtEndDate.Text.TrimEnd().TrimStart();
        queryParams.ZJSID = txtRecordWorkBarcode.Text.TrimEnd().TrimStart();
        if (string.IsNullOrEmpty(queryParams.ZJSID))
        {
            queryParams.ZJSID = "000000";
        }
        queryParams.shiftID = cmoShiftID.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return shiftConfigmanager.GetTablePageDataBySql2(queryParams);
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasWorkUserInfo> pageParams = new PageResult<BasWorkUserInfo>();
        pageParams.PageIndex = -100;
        DataSet ds = GetPageResultData_ds(pageParams);
        PageResult<PptShiftConfig> pageParams2 = new PageResult<PptShiftConfig>();
        pageParams2.PageIndex = -100;
        PageResult<PptShiftConfig> lstPlan = GetPageResultData2(pageParams2);
        DataTable data = ds.Tables[0];
        DataTable data2 = lstPlan.DataSet.Tables[0];
        for (int i = 0; i < data2.Rows.Count; i++)
        {
            string str_PlanDate=data2.Rows[i]["PlanDate"].ToString();
            string str_RecipeMaterialName=data2.Rows[i]["MaterialName"].ToString();
            string str_RealWeight=data2.Rows[i]["RealWeight"].ToString();

            for (int j = 0; j  < data.Rows.Count; j++)
			{
			 string str_PdtDate=data.Rows[j]["PdtDate"].ToString();
             string str_UserRecipeMaterialName = data.Rows[j]["RecipeMaterialName"].ToString();
             string str_UserRealWeight = data.Rows[j]["RealWeight"].ToString();

                 if (str_PlanDate==str_PdtDate&&string.IsNullOrWhiteSpace(str_UserRecipeMaterialName)&&string.IsNullOrWhiteSpace(str_UserRealWeight))
	             {
                     data.Rows[j]["RecipeMaterialName"] = str_RecipeMaterialName;
                     data.Rows[j]["RealWeight"] = str_RealWeight;
                     break;
	             }

			}

        }
        int total = data.Rows.Count;
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
        PageResult<BasWorkUserInfo> pageParams = new PageResult<BasWorkUserInfo>();
        pageParams.PageIndex = -100;
        DataSet ds = GetPageResultData_ds(pageParams);
        PageResult<PptShiftConfig> pageParams2 = new PageResult<PptShiftConfig>();
        pageParams2.PageIndex = -100;
        PageResult<PptShiftConfig> lstPlan = GetPageResultData2(pageParams2);
        DataTable data = ds.Tables[0];
        DataTable data2 = lstPlan.DataSet.Tables[0];
        for (int i = 0; i < data2.Rows.Count; i++)
        {
            string str_PlanDate = data2.Rows[i]["PlanDate"].ToString();
            string str_RecipeMaterialName = data2.Rows[i]["MaterialName"].ToString();
            string str_RealWeight = data2.Rows[i]["RealWeight"].ToString();

            for (int j = 0; j < data.Rows.Count; j++)
            {
                string str_PdtDate = data.Rows[j]["PdtDate"].ToString();
                string str_UserRecipeMaterialName = data.Rows[j]["RecipeMaterialName"].ToString();
                string str_UserRealWeight = data.Rows[j]["RealWeight"].ToString();

                if (str_PlanDate == str_PdtDate && string.IsNullOrWhiteSpace(str_UserRecipeMaterialName) && string.IsNullOrWhiteSpace(str_UserRealWeight))
                {
                    data.Rows[j]["RecipeMaterialName"] = str_RecipeMaterialName;
                    data.Rows[j]["RealWeight"] = str_RealWeight;
                    break;
                }

            }

        }
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
        DataTable data_Copy = data.Copy();
        DataSet ds_Excel = new DataSet();
        ds_Excel.Tables.Add(data_Copy);
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds_Excel, "机台生产信息");
    }
    #endregion


}