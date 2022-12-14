﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Ext.Net;

using Mesnac.Business;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Data.Components;
using Mesnac.Entity;
using Mesnac.Web.UI;

using NBear.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;


public partial class Manager_RubberQuality_Manage_QrigInfo : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonQuery" };
            //添加 = new SysPageAction() { ActionID = 2, ActionName = "ButtonMasterAdd" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "ButtonEdit" };
            删除 = new SysPageAction() { ActionID = 4, ActionName = "ButtonDelete" };
            导出 = new SysPageAction() { ActionID = 5, ActionName = "ButtonExcel" };
            修改明细 = new SysPageAction() { ActionID = 6, ActionName = "ButtonDetailEdit" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        //public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; }
        public SysPageAction 修改明细 { get; private set; }
        //必须为 public
    }
    #endregion

    private ISysCodeManager sysCodeManager = new SysCodeManager();

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            #region 加载CSS样式
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            #region 加载JS文件
            HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "QrigInfo.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            #region 处理按钮

           // ButtonDetailEdit.Disabled = this._.修改.SeqIdx == 0;
           // ButtonDetailDelete.Disabled = this._.删除.SeqIdx == 0;

            #endregion 处理按钮

            InitControls();

            DateFieldQuerySDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldQueryEDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

            if (this._.修改.SeqIdx > 0 || this._.删除.SeqIdx > 0)
            {
                ButtonQuery.Disabled = false;
            }
            if (this._.修改明细.SeqIdx <= 0)
            {
               // ButtonDetailEdit.Disabled = true;
            }
            else
            {
              //  ButtonDetailEdit.Disabled = false;
            }
        }
    }

    private void InitControls()
    {
        //班次
        IPptSetTimeManager bPptSetTimeManager = new PptSetTimeManager();
        EntityArrayList<PptSetTime> mPptSetTimeList = bPptSetTimeManager.GetListByWhereAndOrder(PptSetTime._.UseFlag == "1"
            & PptSetTime._.ProcedureID == 1
            , PptSetTime._.ShiftID.Asc);

        IPptShiftManager bPptShiftManager = new PptShiftManager();
        foreach (PptSetTime mPptSetTime in mPptSetTimeList)
        {
            string shiftId = mPptSetTime.ShiftID.Value.ToString();
            string shiftName = "";
            PptShift mPptShift = bPptShiftManager.GetById(new object[] { mPptSetTime.ShiftID });
            if (mPptShift != null)
            {
                shiftName = mPptShift.ShiftName;
            }

            ComboBoxQueryShiftId.Items.Add(new Ext.Net.ListItem(shiftName, shiftId));
        }

        string sql = "  select distinct TestType from QmtQrigMaster";
        DataSet ds = bPptSetTimeManager.GetBySql(sql).ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            cbxtype.Items.Add(new Ext.Net.ListItem { Text = dr[0].ToString(), Value = dr[0].ToString() });
        }
        
    }
    /// <summary>
    /// 初始化控件
    /// 修改标识：qusf 20131111
    /// 修改说明：1.增加对检验项目分类的初始化
    /// </summary>
    //private void InitControls()
    //{
    //    //班次
    //    IPptSetTimeManager bPptSetTimeManager = new PptSetTimeManager();
    //    EntityArrayList<PptSetTime> mPptSetTimeList = bPptSetTimeManager.GetListByWhereAndOrder(PptSetTime._.UseFlag == "1"
    //        & PptSetTime._.ProcedureID == 1
    //        , PptSetTime._.ShiftID.Asc);

    //    IPptShiftManager bPptShiftManager = new PptShiftManager();
    //    foreach (PptSetTime mPptSetTime in mPptSetTimeList)
    //    {
    //        string shiftId = mPptSetTime.ShiftID.Value.ToString();
    //        string shiftName = "";
    //        PptShift mPptShift = bPptShiftManager.GetById(new object[] { mPptSetTime.ShiftID });
    //        if (mPptShift != null)
    //        {
    //            shiftName = mPptShift.ShiftName;
    //        }

    //        ComboBoxQueryShiftId.Items.Add(new Ext.Net.ListItem(shiftName, shiftId));
    //        ComboBoxEditShiftId.Items.Add(new Ext.Net.ListItem(shiftName, shiftId));
    //        ComboBoxEditShiftCheckId.AddItem(shiftName, shiftId);
    //    }

    //    //班组
    //    IPptClassManager bPptClassManager = new PptClassManager();
    //    EntityArrayList<PptClass> mPptClassList = bPptClassManager.GetListByWhereAndOrder(PptClass._.UseFlag == "1"
    //        , PptClass._.ObjID.Asc);
    //    foreach (PptClass mPptClass in mPptClassList)
    //    {
    //        ComboBoxQueryShiftClass.Items.Add(new Ext.Net.ListItem(mPptClass.ClassName, mPptClass.ObjID.ToString()));
    //        ComboBoxQueryCheckPlanClass.Items.Add(new Ext.Net.ListItem(mPptClass.ClassName, mPptClass.ObjID.ToString()));
    //        ComboBoxEditShiftClass.Items.Add(new Ext.Net.ListItem(mPptClass.ClassName, mPptClass.ObjID.ToString()));
    //        ComboBoxEditCheckPlanClass.Items.Add(new Ext.Net.ListItem(mPptClass.ClassName, mPptClass.ObjID.ToString()));
    //    }

    //    //分类标准
    //    IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
    //    //EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
    //    //    QmtCheckStandType._.DeleteFlag == "0"
    //    //    & QmtCheckStandType._.CheckTypeCode.In(new object[] { 2, 3 })
    //    //    , QmtCheckStandType._.ObjID.Asc);
    //    //foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
    //    //{
    //    //    ComboBoxQueryStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString()));
    //    //    ComboBoxEditStandCode.AddItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString());
    //    //}
    //    ComboBoxQueryStandCode.Items.Add(new Ext.Net.ListItem("正常"));
    //    ComboBoxQueryStandCode.Items.Add(new Ext.Net.ListItem("试制"));
    //    ComboBoxQueryStandCode.Items.Add(new Ext.Net.ListItem("试验"));
    //    ComboBoxQueryStandCode.Items.Add(new Ext.Net.ListItem("返炼"));
    //    //检验项目
    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem("门尼粘度"));
    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem("门尼焦烧"));
    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem("硫变"));
    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem("比重"));
    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem("硬度"));
    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem("物性"));
    //    //IQmtCheckItemManager bQmtCheckItemManager = new QmtCheckItemManager();
    //    //EntityArrayList<QmtCheckItem> mQmtCheckItemList = bQmtCheckItemManager.GetListByWhereAndOrder(QmtCheckItem._.DeleteFlag == "0"
    //    //    , QmtCheckItem._.ItemCode.Asc);
    //    //foreach (QmtCheckItem mQmtCheckItem in mQmtCheckItemList)
    //    //{
    //    //    ComboBoxQueryItemName.Items.Add(new Ext.Net.ListItem(mQmtCheckItem.ItemName));
    //    //}

    //    //检验项目分类
    //    IQmtCheckItemTypeManager bQmtCheckItemTypeManager = new QmtCheckItemTypeManager();
    //    EntityArrayList<QmtCheckItemType> mQmtCheckItemTypeList = bQmtCheckItemTypeManager.GetListByWhereAndOrder(
    //        QmtCheckItemType._.DeleteFlag == "0"
    //        , QmtCheckItemType._.ItemTypeID.Asc);
    //    foreach (QmtCheckItemType mQmtCheckItemType in mQmtCheckItemTypeList)
    //    {
    //        ComboBoxQueryCheckItemTypeId.Items.Add(new Ext.Net.ListItem(mQmtCheckItemType.ItemTypeName, mQmtCheckItemType.ObjID.ToString()));
    //    }
    //    ComboBoxQueryCheckItemTypeId.Items.Add(new Ext.Net.ListItem("T25'","208"));
    
    //    // 主机手
    //    IBasMainHanderManager bBasMainHanderManager = new BasMainHanderManager();
    //    DataSet dsBasMainHander = bBasMainHanderManager.GetMixMainHanderInfo();
    //    foreach (DataRow drBasMainHander in dsBasMainHander.Tables[0].Rows)
    //    {
    //        ComboBoxEditZJSID.AddItem("[" + drBasMainHander["MainHanderCode"].ToString() + "]" + drBasMainHander["UserName"].ToString(), drBasMainHander["MainHanderCode"].ToString());
    //    }

    //    //配方类型
    //    EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(SysCode._.TypeID == "PmtType");
    //    IniComboBox(cboType, lst);
    //}

    /// </summary>
    private const string constSelectAllText = "---请选择---";
    /// <summary>
    /// <summary>
    /// 初始化ComboBox
    /// </summary>
    /// <param name="cb">The cb.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniComboBox(ComboBox cb, EntityArrayList<SysCode> lst)
    {
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        cb.Items.Clear();
        cb.Items.Add(allitem);
        foreach (SysCode m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(m.ItemName, m.ItemCode.ToString());
            cb.Items.Add(item);
        }
        if (cb.Items.Count > 0)
        {
            cb.Text = (cb.Items[0].Value);
        }
    }

    #region 查询
    /// <summary>
    /// 打开查询窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQuery_Click(object sender, DirectEventArgs e)
    {
        WindowQuery.Show();
    }

    /// <summary>
    /// 绑定主数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void StoreMaster_ReadData(object sender, StoreReadDataEventArgs e)
    {
        if (RadioGroupQueryType.CheckedItems.Count == 0)
        {
            X.Msg.Alert("提示", "请选择查询日期类型").Show();
            return;
        }
        if (DateFieldQuerySDate.Value == null || DateFieldQuerySDate.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择开始日期").Show();
            return;
        }
        if (DateFieldQueryEDate.Value == null || DateFieldQueryEDate.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择截止日期").Show();
            return;
        }

        IQmtQrigMasterQueryParams queryParams = GetQmtQrigMasterQueryParams();

        PageResult<QmtQrigMaster> pageParams = new PageResult<QmtQrigMaster>();
        pageParams.PageIndex = e.Page;
        pageParams.PageSize = e.Limit;
        if (e.Sort.Length > 0)
        {
            pageParams.Orderfld = e.Sort[0].Property + " " + e.Sort[0].Direction.ToString();
        }
        else
        {
            pageParams.Orderfld = "";
        }
        queryParams.PageParams = pageParams;

        IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();

        PageResult<QmtQrigMaster> pageResult = GetDataByQueryParams(queryParams);

        e.Total = pageResult.RecordCount;
        DataTable dt = pageResult.DataSet.Tables[0];

        RowSelectionModelMaster.DeselectAll();
        if (dt.Rows.Count > 0)
        {
            StoreDetail.RemoveAll();
            StoreMaster.DataSource = dt;
        }
        else
        {
            StoreDetail.RemoveAll();
            StoreMaster.RemoveAll();
        }

        PagingToolbarMaster.HideRefresh = false;

    }
    public PageResult<QmtQrigMaster> GetDataByQueryParams(IQmtQrigMasterQueryParams queryParams)
    {
        IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
        StringBuilder sb = new StringBuilder();
        #region

        sb.AppendLine("SELECT TA.*,case TA.checkresult when 1 then '合格' when 0 then '不合格' else '无标准' end CHECKFALG");
        sb.AppendLine(",TA.CheckDate + ' ' + TA.CheckTime FullCheckTime");
        sb.AppendLine(",TB.EquipName,TC.ShiftName,TD.ClassName,TE.MaterialName MaterName");
       
        sb.AppendLine(",TJ.ShiftName CheckShiftName ");
        sb.AppendLine("FROM dbo.QmtQrigMaster TA ");
        sb.AppendLine("LEFT JOIN dbo.BasEquip TB ON TA.EquipCode = TB.EquipCode");
        sb.AppendLine("LEFT JOIN dbo.PptShift TC ON TA.ShiftId = TC.Shiftname");
        sb.AppendLine("LEFT JOIN dbo.PptClass TD ON TA.ShiftClass = TD.ClassName");
        sb.AppendLine("LEFT JOIN dbo.BasMaterial TE ON TA.MaterCode = TE.MaterialCode");
      
        sb.AppendLine("LEFT JOIN dbo.PptShift TJ ON TA.ShiftCheckId = TJ.Shiftname");

        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(queryParams.SPlanDate))
            sb.AppendLine("AND TA.PlanDate>='" + queryParams.SPlanDate + "'");
        if (!string.IsNullOrEmpty(queryParams.EPlanDate))
            sb.AppendLine("AND TA.PlanDate<='" + queryParams.EPlanDate + "'");
        if (!string.IsNullOrEmpty(queryParams.SCheckDate))
            sb.AppendLine("AND TA.CheckPlan_Date>='" + queryParams.SCheckDate + "'");
        if (!string.IsNullOrEmpty(queryParams.ECheckDate))
            sb.AppendLine("AND TA.CheckPlan_Date<'" + queryParams.ECheckDate + "'");
        if (!string.IsNullOrEmpty(queryParams.EquipCode))
            sb.AppendLine("AND TA.EquipCode='" + queryParams.EquipCode + "'");
        if (!string.IsNullOrEmpty(ComboBoxQueryShiftId.SelectedItem.Text))
            sb.AppendLine("AND TA.ShiftId = '" + ComboBoxQueryShiftId.SelectedItem.Text + "'");
        if (!string.IsNullOrEmpty(queryParams.ShiftClass))
            sb.AppendLine("AND TA.ShiftClass='" + queryParams.ShiftClass + "'");
        if (!string.IsNullOrEmpty(queryParams.CheckPlanClass))
            sb.AppendLine("AND TA.CheckClassName ='" + queryParams.CheckPlanClass + "'");
        if (!string.IsNullOrEmpty(queryParams.WorkerBarcode))
            sb.AppendLine("AND TA.WorkerBarcode='" + queryParams.WorkerBarcode + "'");
        if (!string.IsNullOrEmpty(queryParams.CheckEquipCode))
            sb.AppendLine("AND TA.CheckEquipCode='" + queryParams.CheckEquipCode + "'");
        if (!string.IsNullOrEmpty(queryParams.MaterCode))
            sb.AppendLine("AND TA.MaterCode='" + queryParams.MaterCode + "'");
        if (!string.IsNullOrEmpty(queryParams.TestType))
            sb.AppendLine("AND TA.TestType LIKE '%" + queryParams.TestType + "%'");
        if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
            sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");
        if (!string.IsNullOrEmpty(cbxtype.Text) && cbxtype.Text!="全部")
            sb.AppendLine("AND TA.TestType='" + cbxtype.Text + "'");


   

      
        if (!string.IsNullOrEmpty(queryParams.CheckItemTypeId))
        {
            sb.AppendLine("AND EXISTS (");
            if (queryParams.CheckItemTypeId == "208")
            {
                sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.ItemCode = '208'");
            }
            else
            {
                sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.TypeID = " + queryParams.CheckItemTypeId);
            }
            //sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.TypeID = " + queryParams.CheckItemTypeId);
            sb.AppendLine(")");
        }
        if (!string.IsNullOrEmpty(queryParams.StandCode))
        {
           
                sb.AppendLine("AND TA.StandCode = '" + queryParams.StandCode+"' ");
           
        }

        PageResult<QmtQrigMaster> pageParams = queryParams.PageParams;

        if (pageParams != null && pageParams.PageSize > 0)
        {
            pageParams.QueryStr = sb.ToString();
            if (pageParams.Orderfld == "" || pageParams.Orderfld.ToLower() == "seqno")
            {
                pageParams.Orderfld = "PlanDate,ShiftId,EquipCode,MaterCode,SerialId,LLSerialID,CheckDate,CheckTime";
            }
            ts.Text = sb.ToString();
            return bQmtQrigMasterManager.GetPageDataBySql(pageParams);
        }
        else
        {
            sb.AppendLine("ORDER BY TA.PlanDate,TA.ShiftId,TA.EquipCode,TA.MaterCode,TA.SerialId,TA.LLSerialID,TA.CheckDate,TA.CheckTime");
            ts.Text = sb.ToString();
            NBear.Data.CustomSqlSection css = bQmtQrigMasterManager.GetBySql(sb.ToString());
            if (pageParams == null)
            {
                pageParams = new PageResult<QmtQrigMaster>();
            }
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }

        #endregion

    }
    /// <summary>
    /// 修改标识：qusf 20131111
    /// 修改说明：1.质检日期不再从前一天20:15:00至当天20:15:00，而是直接取到日期
    ///           2.增加检验项目类型
    /// </summary>
    /// <returns></returns>
    private IQmtQrigMasterQueryParams GetQmtQrigMasterQueryParams()
    {
        IQmtQrigMasterQueryParams queryParams = new QmtQrigMasterQueryParams();

        if (RadioGroupQueryType.CheckedItems[0].InputValue == "1")
        {
            queryParams.SPlanDate = DateFieldQuerySDate.SelectedDate.ToString("yyyy-MM-dd");
            queryParams.EPlanDate = DateFieldQueryEDate.SelectedDate.ToString("yyyy-MM-dd");
        }
        else if (RadioGroupQueryType.CheckedItems[0].InputValue == "2")
        {
            queryParams.SCheckDate = DateFieldQuerySDate.SelectedDate.ToString("yyyy-MM-dd");
            queryParams.ECheckDate = DateFieldQueryEDate.SelectedDate.ToString("yyyy-MM-dd");
        }
        if (HiddenQueryEquipCode.Value.ToString() != "")
        {
            queryParams.EquipCode = HiddenQueryEquipCode.Value.ToString();
        }
        //if (ComboBoxQueryShiftId.Value.ToString() != "")
        //{
        //    queryParams.ShiftId = ComboBoxQueryShiftId.Value.ToString();
        //}
        if (ComboBoxQueryShiftClass.Value.ToString() != "")
        {
            queryParams.ShiftClass = ComboBoxQueryShiftClass.SelectedItem.Text.ToString();
        }
        if (ComboBoxQueryCheckPlanClass.Value.ToString() != "")
        {
            queryParams.CheckPlanClass = ComboBoxQueryCheckPlanClass.SelectedItem.Text.ToString();
        }
        if (HiddenQueryCheckUserCode.Value.ToString() != "")
        {
            queryParams.WorkerBarcode = HiddenQueryCheckUserCode.Value.ToString();
        }
        if (HiddenQueryCheckEquipCode.Value.ToString() != "")
        {
            queryParams.CheckEquipCode = HiddenQueryCheckEquipCode.Value.ToString();
        }
        if (HiddenQueryMaterCode.Value.ToString() != "")
        {
            queryParams.MaterCode = HiddenQueryMaterCode.Value.ToString();
        }
        if (ComboBoxQueryStandCode.Value != "")
        {
            queryParams.StandCode = ComboBoxQueryStandCode.Value.ToString();
        }
        if (ComboBoxQueryItemName.Text.Trim() != "")
        {
            queryParams.TestType = ComboBoxQueryItemName.Text.Trim();
        }

        if (TextFieldQueryZJSID.Text.Trim() != "")
        {
            queryParams.ZJSID = TextFieldQueryZJSID.Text.Trim();
        }
        if (ComboBoxQueryCheckItemTypeId.Value.ToString() != "")
        {
            queryParams.CheckItemTypeId = ComboBoxQueryCheckItemTypeId.Value.ToString();
        }

        queryParams.DeleteFlag = "0";

        return queryParams;


    }

    /// <summary>
    /// 更改查询日期方式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioGroupQueryType_Change(object sender, DirectEventArgs e)
    {
        string queryType = "";
        string json = e.ExtraParams["QueryType"];
        JObject obj = JsonConvert.DeserializeObject(json) as JObject;
        foreach (KeyValuePair<string, JToken> token in obj)
        {
            queryType = token.Value.ToString();
        }

        if (queryType == "1")
        {
            DateFieldQuerySDate.FieldLabel = "开始生产日期";
            DateFieldQueryEDate.FieldLabel = "截止生产日期";
        }
        else if (queryType == "2")
        {
            DateFieldQuerySDate.FieldLabel = "开始检验日期";
            DateFieldQueryEDate.FieldLabel = "截止检验日期";
        }
    }

    /// <summary>
    /// 查找或清空 查询窗体中的生产机台
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldQueryEquipName_TriggerClick(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"];
        if (index == "0")
        {
            HiddenQueryEquipCode.SetValue("");
            TriggerFieldQueryEquipName.SetValue("");
        }
        else if (index == "1")
        {
            HiddenEquipType.SetValue("QueryEquip");
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();");
        }
    }

    /// <summary>
    /// 查找或清空 查询窗体中的检验机台
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldQueryCheckEquipName_TriggerClick(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"];
        if (index == "0")
        {
            HiddenQueryCheckEquipCode.SetValue("");
            TriggerFieldQueryCheckEquipName.SetValue("");
        }
        else if (index == "1")
        {
            HiddenEquipType.SetValue("QueryCheck");
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();");
        }
    }

    /// <summary>
    /// 查找或清空 查询窗体中的检验人员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldQueryCheckUserName_TriggerClick(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"];
        if (index == "0")
        {
            HiddenQueryCheckUserCode.SetValue("");
            TriggerFieldQueryCheckUserName.SetValue("");
        }
        else if (index == "1")
        {
            HiddenUserType.SetValue("QueryCheck");
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();");
        }

    }

    /// <summary>
    /// 查找或清空 查询窗体中的物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldQueryMaterName_TriggerClick(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"];
        if (index == "0")
        {
            HiddenQueryMaterCode.SetValue("");
            TriggerFieldQueryMaterName.SetValue("");
        }
        else if (index == "1")
        {
            HiddenMaterType.SetValue("QueryMater");
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();");
        }

    }

    /// <summary>
    /// 取消 查询窗体关闭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQueryCancel_Click(object sender, DirectEventArgs e)
    {
        //FormPanelQuery.Reset(); //qusf 20130801 注释掉
        WindowQuery.Hide();

    }

    /// <summary>
    /// 确定 查询窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQueryAccept_Click(object sender, DirectEventArgs e)
    {
        //FormPanelQuery.UpdateContent(); //qusf 20130801 注释掉
        StoreMaster.LoadPage(1);

        WindowQuery.Hide();
    }

    #endregion 查询

    #region 选择主数据

    /// <summary>
    /// 绑定明细数据
    /// </summary>
    /// <param name="seqNo"></param>
    private void LoadGridPanelDetail(string seqNo)
    {
        //X.Js.Alert(seqNo); return;
        IQmtQrigDetailManager bQmtQrigDetailManager = new QmtQrigDetailManager();
        IQmtQrigDetailParams paras = new QmtQrigDetailParams();
        paras.SeqNo = seqNo;
        paras.DeleteFlag = "0";
        DataSet ds = bQmtQrigDetailManager.GetDataByParas(paras);
        StoreDetail.DataSource = bQmtQrigDetailManager.GetDataByParas(paras);
        StoreDetail.DataBind();
    }

    /// <summary>
    /// 选择主数据行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelMaster_Select(object sender, DirectEventArgs e)
    {
        
        string seqNo = RowSelectionModelMaster.SelectedRow.RecordID;
        LoadGridPanelDetail(seqNo);
    }

    #endregion 选择主数据

    //#region 修改/删除主数据

    ///// <summary>
    ///// 清空 修改窗体的控件值
    ///// </summary>
    //private void ClearEditFormPanel()
    //{
    //    HiddenEditSeqNo.SetValue("");
    //    DateFieldEditPlanDate.SetValue("");
    //    HiddenEditEquipCode.SetValue("");
    //    TriggerFieldEditEquipName.SetValue("");
    //    HiddenEditMaterCode.SetValue("");
    //    TriggerFieldEditMaterName.SetValue("");
    //    ComboBoxEditShiftId.SetValue("");
    //    ComboBoxEditShiftClass.SetValue("");
    //    NumberFieldEditSerialId.SetValue("");
    //    NumberFieldEditLLSerialID.SetValue("");
    //    NumberFieldEditCheckNum.SetValue("");
    //    ComboBoxEditCheckPlanClass.SetValue("");
    //    ComboBoxEditShiftCheckId.SetValue("");
    //    HiddenEditCheckUserCode.SetValue("");
    //    TriggerFieldEditCheckUserName.SetValue("");

    //    DateFieldEditCheckPlanDate.SetValue("");
    //    DateFieldEditCheckDate.SetValue("");
    //    TimeFieldEditCheckTime.SetValue("00:00:00");
    //}

    ///// <summary>
    ///// 修改 打开修改窗体
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ////protected void ButtonEdit_Click(object sender, DirectEventArgs e)
    ////{
    ////    if (RowSelectionModelMaster.SelectedRows.Count == 0)
    ////    {
    ////        X.Msg.Alert("提示", "请选择要修改的质检记录").Show();
    ////        return;
    ////    }

    ////    ClearEditFormPanel();

    ////    string seqNo = RowSelectionModelMaster.SelectedRows[0].RecordID;

    ////    IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
    ////    QmtQrigMaster mQmtQrigMaster = bQmtQrigMasterManager.GetById(new object[] { seqNo });
    ////    if (mQmtQrigMaster != null)
    ////    {
    ////        HiddenEditSeqNo.SetValue(mQmtQrigMaster.SeqNo.ToString());
    ////        DateFieldEditPlanDate.SetValue(mQmtQrigMaster.PlanDate);
    ////        HiddenEditEquipCode.SetValue(mQmtQrigMaster.EquipCode);
    ////        IBasEquipManager bBasEquipManager = new BasEquipManager();
    ////        EntityArrayList<BasEquip> mBasEquipList = bBasEquipManager.GetListByWhereAndOrder(
    ////            BasEquip._.EquipCode == mQmtQrigMaster.EquipCode
    ////            , BasEquip._.DeleteFlag.Asc);
    ////        if (mBasEquipList.Count > 0)
    ////        {
    ////            TriggerFieldEditEquipName.SetValue(mBasEquipList[0].EquipName);
    ////        }
    ////        HiddenEditMaterCode.SetValue(mQmtQrigMaster.MaterCode);
    ////        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
    ////        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
    ////            BasMaterial._.MaterialCode == mQmtQrigMaster.MaterCode
    ////            , BasMaterial._.DeleteFlag.Asc);
    ////        if (mBasMaterialList.Count > 0)
    ////        {
    ////            TriggerFieldEditMaterName.SetValue(mBasMaterialList[0].MaterialName);
    ////        }
    ////        ComboBoxEditShiftId.SetValue(mQmtQrigMaster.ShiftId.Value.ToString());
    ////        ComboBoxEditShiftClass.SetValue(mQmtQrigMaster.ShiftClass.Value.ToString());
    ////        ComboBoxEditZJSID.SetValue(mQmtQrigMaster.ZJSID);
    ////        NumberFieldEditSerialId.SetValue(mQmtQrigMaster.SerialId.Value);
    ////        NumberFieldEditLLSerialID.SetValue(mQmtQrigMaster.LLSerialID.Value);
    ////        NumberFieldEditCheckNum.SetValue(mQmtQrigMaster.CheckNum.Value);
    ////        ComboBoxEditCheckPlanClass.SetValue(mQmtQrigMaster.CheckPlan_Class.Value.ToString());
    ////        ComboBoxEditShiftCheckId.SetValue(mQmtQrigMaster.ShiftCheckId.Value.ToString());
    ////        HiddenEditCheckUserCode.SetValue(mQmtQrigMaster.WorkerBarcode);
    ////        DateFieldEditCheckPlanDate.SetValue(mQmtQrigMaster.CheckPlan_Date);
    ////        DateFieldEditCheckDate.SetValue(mQmtQrigMaster.CheckDate);
    ////        TimeFieldEditCheckTime.SetValue(mQmtQrigMaster.CheckTime);
    ////        HiddenEditCheckEquipCode.SetValue(mQmtQrigMaster.CheckEquipCode);
    ////        mBasEquipList = bBasEquipManager.GetListByWhereAndOrder(
    ////            BasEquip._.EquipCode == mQmtQrigMaster.CheckEquipCode
    ////            , BasEquip._.DeleteFlag.Asc);
    ////        if (mBasEquipList.Count > 0)
    ////        {
    ////            TriggerFieldEditCheckEquipName.SetValue(mBasEquipList[0].EquipName);
    ////        }
    ////        ComboBoxEditStandCode.SetValue(mQmtQrigMaster.StandCode.Value.ToString());

    ////        IBasUserManager bBasUserManager = new BasUserManager();
    ////        EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
    ////            BasUser._.WorkBarcode == mQmtQrigMaster.WorkerBarcode
    ////            , BasUser._.DeleteFlag.Asc);
    ////        if (mBasUserList.Count > 0)
    ////        {
    ////            TriggerFieldEditCheckUserName.SetValue(mBasUserList[0].UserName);
    ////        }

    ////        WindowEdit.Show();
    ////    }
    ////    else
    ////    {
    ////        X.Msg.Alert("提示", "未找到要修改的质检记录").Show();
    ////    }

    ////}

    ///// <summary>
    ///// 清空或查找 修改窗体中的生产机台
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void TriggerFieldEditEquipName_TriggerClick(object sender, DirectEventArgs e)
    //{
    //    string index = e.ExtraParams["index"];
    //    if (index == "0")
    //    {
    //        HiddenEditEquipCode.SetValue("");
    //        TriggerFieldEditEquipName.SetValue("");
    //    }
    //    else if (index == "1")
    //    {
    //        HiddenEquipType.SetValue("EditEquip");
    //        X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();");
    //    }

    //}

    ///// <summary>
    ///// 清空或查找 修改窗体中的检验机台
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void TriggerFieldEditCheckEquipName_TriggerClick(object sender, DirectEventArgs e)
    //{
    //    string index = e.ExtraParams["index"];
    //    if (index == "0")
    //    {
    //        HiddenEditCheckEquipCode.SetValue("");
    //        TriggerFieldEditCheckEquipName.SetValue("");
    //    }
    //    else if (index == "1")
    //    {
    //        HiddenEquipType.SetValue("EditCheckEquip");
    //        X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();");
    //    }

    //}

    ///// <summary>
    ///// 清空或查找 修改窗体中的物料
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void TriggerFieldEditMaterName_TriggerClick(object sender, DirectEventArgs e)
    //{
    //    string index = e.ExtraParams["index"];
    //    if (index == "0")
    //    {
    //        HiddenEditMaterCode.SetValue("");
    //        TriggerFieldEditMaterName.SetValue("");
    //    }
    //    else if (index == "1")
    //    {
    //        HiddenMaterType.SetValue("EditMater");
    //        X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();");
    //    }

    //}

    ///// <summary>
    ///// 清空或查找 修改窗体中的检验人员
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void TriggerFieldEditCheckUserName_TriggerClick(object sender, DirectEventArgs e)
    //{
    //    string index = e.ExtraParams["index"];
    //    if (index == "0")
    //    {
    //        HiddenEditCheckUserCode.SetValue("");
    //        TriggerFieldEditCheckUserName.SetValue("");
    //    }
    //    else if (index == "1")
    //    {
    //        HiddenUserType.SetValue("EditCheck");
    //        X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();");
    //    }

    //}

    ///// <summary>
    ///// 关闭 修改窗体
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void ButtonEditCancel_Click(object sender, DirectEventArgs e)
    //{
    //    WindowEdit.Close();
    //}

    ///// <summary>
    ///// 数据校验 修改窗体
    ///// </summary>
    ///// <returns></returns>
    //private bool ValidateEditFormPanel()
    //{
    //    if (HiddenEditSeqNo.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "未找到要修改的记录").Show();
    //        return false;
    //    }

    //    if (DateFieldEditPlanDate.SelectedDate == null || DateFieldEditPlanDate.SelectedDate.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择生产日期").Show();
    //        return false;
    //    }

    //    if (TriggerFieldEditEquipName.Text.Trim() == "" || HiddenEditEquipCode.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择生产机台").Show();
    //        return false;
    //    }

    //    if (TriggerFieldEditMaterName.Text.Trim() == "" || HiddenEditMaterCode.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择物料").Show();
    //        return false;
    //    }

    //    if (ComboBoxEditShiftId.SelectedItem == null || ComboBoxEditShiftId.SelectedItem.Value == "")
    //    {
    //        X.Msg.Alert("提示", "请选择生产班次").Show();
    //        return false;
    //    }

    //    if (ComboBoxEditShiftClass.SelectedItem == null || ComboBoxEditShiftClass.SelectedItem.Value == "")
    //    {
    //        X.Msg.Alert("提示", "请选择生产班组").Show();
    //        return false;
    //    }

    //    if (ComboBoxEditZJSID.Value == "")
    //    {
    //        X.Msg.Alert("提示", "请选择主机手").Show();
    //        return false;
    //    }

    //    if (NumberFieldEditSerialId.RawText.Trim() == "")
    //    {
    //        X.Msg.Alert("提示", "请填写车次").Show();
    //        return false;
    //    }

    //    if (NumberFieldEditLLSerialID.RawText.Trim() == "")
    //    {
    //        X.Msg.Alert("提示", "请填写玲珑车次").Show();
    //        return false;
    //    }

    //    if (NumberFieldEditCheckNum.RawText.Trim() == "")
    //    {
    //        X.Msg.Alert("提示", "请填写检验次数").Show();
    //        return false;
    //    }
    //    if (ComboBoxEditCheckPlanClass.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择检验班组").Show();
    //        return false;
    //    }

    //    if (ComboBoxEditShiftCheckId.SelectedItem == null || ComboBoxEditShiftCheckId.SelectedItem.Value == "")
    //    {
    //        X.Msg.Alert("提示", "请选择检验班次").Show();
    //        return false;
    //    }

    //    if (DateFieldEditCheckPlanDate.SelectedDate == null || DateFieldEditCheckPlanDate.SelectedDate.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择检验日期").Show();
    //        return false;
    //    }

    //    if (DateFieldEditCheckDate.SelectedDate == null || DateFieldEditCheckDate.SelectedDate.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择检验时间日期部分").Show();
    //        return false;
    //    }

    //    if (TimeFieldEditCheckTime.SelectedTime == null || TimeFieldEditCheckTime.SelectedTime.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择检验时间时间部分").Show();
    //        return false;
    //    }

    //    if (ComboBoxEditStandCode.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请选择检验标准分类").Show();
    //        return false;
    //    }



    //    return true;
    //}

    ///// <summary>
    ///// 修改
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ////protected void ButtonEditAccept_Click(object sender, DirectEventArgs e)
    ////{
    ////    if (ValidateEditFormPanel() == false)
    ////    {
    ////        return;
    ////    }

    ////    string seqNo = HiddenEditSeqNo.Value.ToString();

    ////    IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
    ////    QmtQrigMaster mQmtQrigMaster = bQmtQrigMasterManager.GetById(new object[] { seqNo });

    ////    if (mQmtQrigMaster != null)
    ////    {
    ////        mQmtQrigMaster.PlanDate = DateFieldEditPlanDate.SelectedDate.ToString("yyyy-MM-dd");
    ////        mQmtQrigMaster.EquipCode = HiddenEditEquipCode.Value.ToString();
    ////        mQmtQrigMaster.MaterCode = HiddenEditMaterCode.Value.ToString();
    ////        mQmtQrigMaster.ShiftId = Convert.ToInt32(ComboBoxEditShiftId.SelectedItem.Value);
    ////        mQmtQrigMaster.ShiftClass = Convert.ToInt32(ComboBoxEditShiftClass.SelectedItem.Value);
    ////        mQmtQrigMaster.ZJSID = ComboBoxEditZJSID.Value.ToString();
    ////        mQmtQrigMaster.SerialId = Convert.ToInt32(NumberFieldEditSerialId.Number);
    ////        mQmtQrigMaster.LLSerialID = Convert.ToInt32(NumberFieldEditLLSerialID.Number);
    ////        mQmtQrigMaster.CheckNum = Convert.ToInt32(NumberFieldEditCheckNum.Number);
    ////        mQmtQrigMaster.CheckPlan_Class = Convert.ToInt32(ComboBoxEditCheckPlanClass.Value.ToString());
    ////        mQmtQrigMaster.ShiftCheckId = Convert.ToInt32(ComboBoxEditShiftCheckId.SelectedItem.Value);
    ////        mQmtQrigMaster.WorkerBarcode = HiddenEditCheckUserCode.Value.ToString();
    ////        mQmtQrigMaster.CheckPlan_Date = DateFieldEditCheckPlanDate.SelectedDate.ToString("yyyy-MM-dd");
    ////        mQmtQrigMaster.CheckDate = DateFieldEditCheckDate.SelectedDate.ToString("yyyy-MM-dd");
    ////        mQmtQrigMaster.CheckTime = TimeFieldEditCheckTime.RawText;
    ////        mQmtQrigMaster.CheckEquipCode = HiddenEditCheckEquipCode.Value.ToString();
    ////        mQmtQrigMaster.StandCode = Convert.ToInt32(ComboBoxEditStandCode.Value.ToString());

    ////        mQmtQrigMaster.NeedReJudgeGrade = "1";

    ////        bQmtQrigMasterManager.LogicUpdate(mQmtQrigMaster);

    ////        WindowEdit.Close();

    ////        StoreMaster.LoadProxy();

    ////        X.Msg.Alert("提示", "修改成功").Show();
    ////    }
    ////    else
    ////    {
    ////        X.Msg.Alert("提示", "未找到要修改的记录").Show();
    ////    }

    ////}

    ///// <summary>
    ///// 删除
    ///// 修改标识：qusf 20131112
    ///// 修改说明：1.改为调用Manager类的逻辑删除
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    protected void ButtonDelete_Click(object sender, DirectEventArgs e)
    {
        if (RowSelectionModelMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要删除的质检记录").Show();
            return;
        }

        string seqNo = RowSelectionModelMaster.SelectedRows[0].RecordID;

        IQmtQrigDetailManager bQmtQrigDetailManager = new QmtQrigDetailManager();
        IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
        QmtQrigMaster mQmtQrigMaster = bQmtQrigMasterManager.GetById(new object[] { seqNo });
        if (mQmtQrigMaster != null)
        {
            // bQmtQrigMasterManager.LogicDelete(mQmtQrigMaster);
            string sql = "delete from QmtQrigMaster where seqNo = '" + seqNo + "'";
            string sql1 = "delete from QmtQrigDetail where seqNo = '" + seqNo + "'";
            bQmtQrigMasterManager.GetBySql(sql).ToDataSet();
            bQmtQrigDetailManager.GetBySql(sql1).ToDataSet();
            StoreMaster.LoadProxy();

            X.Msg.Alert("提示", "删除成功").Show();

        }
        else
        {
            X.Msg.Alert("提示", "未找到要删除的质检记录").Show();
        }

    }

    //#endregion 修改/删除主数据

    #region 修改/删除明细数据

    /// <summary>
    /// 选择质检明细
    /// 创建标识：qusf 20131113
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelDetail_Select(object sender, DirectEventArgs e)
    {
        string seqNo = e.ExtraParams.GetParameter("SeqNo").Value;
        string itemCd = e.ExtraParams.GetParameter("ItemCd").Value;

        HiddenDetailEditSeqNo.SetValue(seqNo);
        HiddenDetailEditItemCd.SetValue(itemCd);

    }

    /// <summary>
    /// 删除明细
    /// 创建标识：qusf 20131113
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ButtonDetailDelete_Click(object sender, DirectEventArgs e)
    //{
    //    if (RowSelectionModelDetail.SelectedRows.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "请选择要删除的质检记录").Show();
    //        return;
    //    }

    //    string seqNo = HiddenDetailEditSeqNo.Value.ToString();
    //    string itemCd = HiddenDetailEditItemCd.Value.ToString();

    //    IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
    //    EntityArrayList<QmtQrigMaster> mQmtQrigMasterList = bQmtQrigMasterManager.GetListByWhereAndOrder(
    //        QmtQrigMaster._.DeleteFlag == "0"
    //        & QmtQrigMaster._.SeqNo == seqNo
    //        , QmtQrigMaster._.DeleteFlag.Asc);
    //    if (mQmtQrigMasterList.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未找到要删除的质检记录").Show();
    //        return;
    //    }
    //    QmtQrigMaster mQmtQrigMaster = mQmtQrigMasterList[0];

    //    IQmtQrigDetailManager bQmtQrigDetailManager = new QmtQrigDetailManager();
    //    EntityArrayList<QmtQrigDetail> mQmtQrigDetailList = bQmtQrigDetailManager.GetListByWhereAndOrder(
    //        QmtQrigDetail._.DeleteFlag == "0"
    //        & QmtQrigDetail._.SeqNo == seqNo
    //        & QmtQrigDetail._.ItemCd == itemCd
    //        , QmtQrigDetail._.DeleteFlag.Asc);

    //    if (mQmtQrigDetailList.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未找到要删除的质检明细记录").Show();
    //        return;
    //    }
    //    else if (mQmtQrigDetailList.Count > 1)
    //    {
    //        X.Msg.Alert("提示", "找到的质检明细记录多于一条").Show();
    //        return;
    //    }

    //    QmtQrigDetail mQmtQrigDetail = mQmtQrigDetailList[0];
    //    bQmtQrigDetailManager.LogicDelete(mQmtQrigMaster, mQmtQrigDetail);

    //    LoadGridPanelDetail(seqNo);

    //    X.Msg.Alert("提示", "删除成功").Show();


    //}

    /// <summary>
    /// 弹出修改明细窗口
    /// 创建标识：qusf 20131113
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ButtonDetailEdit_Click(object sender, DirectEventArgs e)
    //{

    //    if (RowSelectionModelDetail.SelectedRows.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "请选择要修改的质检明细记录").Show();
    //        return;
    //    }

    //    ClearDetailEditFormPanel();

    //    string seqNo = HiddenDetailEditSeqNo.Value.ToString();
    //    string itemCd = HiddenDetailEditItemCd.Value.ToString();

    //    IQmtQrigDetailManager bQmtQrigDetailManager = new QmtQrigDetailManager();
    //    EntityArrayList<QmtQrigDetail> mQmtQrigDetailList = bQmtQrigDetailManager.GetListByWhereAndOrder(
    //        QmtQrigDetail._.DeleteFlag == "0"
    //        & QmtQrigDetail._.SeqNo == seqNo
    //        & QmtQrigDetail._.ItemCd == itemCd
    //        , QmtQrigDetail._.DeleteFlag.Asc);
    //    if (mQmtQrigDetailList.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未找到要修改的质检明细记录").Show();
    //        return;
    //    }
    //    else if (mQmtQrigDetailList.Count > 1)
    //    {
    //        X.Msg.Alert("提示", "要修改的质检明细记录多于一条").Show();
    //        return;
    //    }

    //    QmtQrigDetail mQmtQrigDetail = mQmtQrigDetailList[0];
    //    string itemName = "";
    //    IQmtCheckItemManager bQmtCheckItemManager = new QmtCheckItemManager();
    //    EntityArrayList<QmtCheckItem> mQmtCheckItemList = bQmtCheckItemManager.GetListByWhereAndOrder(
    //        QmtCheckItem._.ItemCode == itemCd
    //        , QmtCheckItem._.DeleteFlag.Asc);
    //    if (mQmtCheckItemList.Count > 0)
    //    {
    //        itemName = mQmtCheckItemList[0].ItemName;
    //    }
    //    DisplayFieldDetailEditItemName.SetValue(itemName);
    //    if (mQmtQrigDetail.ItemCheck.HasValue == true)
    //    {
    //        NumberFieldDetailEditItemCheck.SetValue(mQmtQrigDetail.ItemCheck.Value);
    //    }

    //    WindowDetailEdit.Show();

    //}

    /// <summary>
    /// 清空明细数据
    /// 创建标识：qusf 20131113
    /// </summary>
    //private void ClearDetailEditFormPanel()
    //{
    //    DisplayFieldDetailEditItemName.SetValue("");
    //    NumberFieldDetailEditItemCheck.SetValue("");
    //}

    /// <summary>
    /// 关闭明细修改窗体
    /// 创建标识：qusf 20131113
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ButtonDetailEditCancel_Click(object sender, DirectEventArgs e)
    //{
    //    WindowDetailEdit.Close();
    //}

    /// <summary>
    /// 修改明细数据信息
    /// 创建标识：qusf 20131113
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ButtonDetailEditAccept_Click(object sender, DirectEventArgs e)
    //{
    //    if (ValidateDetailEditFormPanel() == false)
    //    {
    //        return;
    //    }

    //    string seqNo = HiddenDetailEditSeqNo.Value.ToString();
    //    string itemCd = HiddenDetailEditItemCd.Value.ToString();

    //    IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
    //    EntityArrayList<QmtQrigMaster> mQmtQrigMasterList = bQmtQrigMasterManager.GetListByWhereAndOrder(
    //        QmtQrigMaster._.DeleteFlag == "0"
    //        & QmtQrigMaster._.SeqNo == seqNo
    //        , QmtQrigMaster._.DeleteFlag.Asc);

    //    if (mQmtQrigMasterList.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未找到要修改的主数据记录").Show();
    //        return;
    //    }
    //    QmtQrigMaster mQmtQrigMaster = mQmtQrigMasterList[0];
    //    mQmtQrigMaster.NeedReJudgeGrade = "1";

    //    IQmtQrigDetailManager bQmtQrigDetailManager = new QmtQrigDetailManager();
    //    EntityArrayList<QmtQrigDetail> mQmtQrigDetailList = bQmtQrigDetailManager.GetListByWhereAndOrder(
    //        QmtQrigDetail._.DeleteFlag == "0"
    //        & QmtQrigDetail._.SeqNo == seqNo
    //        & QmtQrigDetail._.ItemCd == itemCd
    //        , QmtQrigDetail._.DeleteFlag.Asc);

    //    if (mQmtQrigDetailList.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未找到要修改的明细数据记录").Show();
    //        return;
    //    }
    //    else if (mQmtQrigDetailList.Count > 1)
    //    {
    //        X.Msg.Alert("提示", "要修改的明细数据记录多于一条").Show();
    //        return;
    //    }
    //    QmtQrigDetail mQmtQrigDetail = mQmtQrigDetailList[0];
    //    mQmtQrigDetail.ItemCheck = Convert.ToDecimal(NumberFieldDetailEditItemCheck.Number);

    //    bQmtQrigDetailManager.LogicUpdate(mQmtQrigMaster, mQmtQrigDetail);

    //    WindowDetailEdit.Close();

    //    LoadGridPanelDetail(seqNo);

    //    X.Msg.Alert("提示", "修改成功").Show();

    //}

    ///// <summary>
    ///// 数据校验 修改明细窗体
    ///// </summary>
    ///// <returns></returns>
    //private bool ValidateDetailEditFormPanel()
    //{
    //    if (HiddenDetailEditSeqNo.Value.ToString() == "" || HiddenDetailEditItemCd.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "未找到要修改的明细记录").Show();
    //        return false;
    //    }

    //    if (NumberFieldDetailEditItemCheck.Value == null || NumberFieldDetailEditItemCheck.Value.ToString() == "")
    //    {
    //        X.Msg.Alert("提示", "请填写检验值").Show();
    //        return false;
    //    }

    //    return true;
    //}

    #endregion 修改/删除明细数据

    #region 导出Excel

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonExcel_Click(object sender, DirectEventArgs e)
    {
        string count = e.ExtraParams["count"];
        if (count == "0")
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        else if (Convert.ToInt32(count) > 65535)
        {
            X.Msg.Alert("提示", "导出的记录数不能超过65535").Show();
            return;
        }

        DataTable dtExcel = new DataTable();

        foreach (ColumnBase c in GridPanelMaster.ColumnModel.Columns)
        {
            if (c.Hidden == false)
            {
                DataColumn dcExcel = new DataColumn();
                dcExcel.Caption = c.Text;
                dcExcel.ColumnName = c.DataIndex;
                dtExcel.Columns.Add(dcExcel);
            }
        }

        IQmtQrigMasterQueryParams queryParams = GetQmtQrigMasterQueryParams();

        PageResult<QmtQrigMaster> pageParams = new PageResult<QmtQrigMaster>();

        IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
        PageResult<QmtQrigMaster> pageResult = GetDataByQueryParams(queryParams);
        //DataTable dt = GetDetailDataByQueryParams(queryParams).DataSet.Tables[0];
        DataTable dt = GetDetailDataByQueryParams(queryParams).DataSet.Tables[0];
        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        else if (dt.Rows.Count > 65534)
        {
            X.Msg.Alert("提示", "导出的记录数不能超过65534").Show();
            return;
        }

        Mesnac.Util.Excel.ExcelDownload ed = new Mesnac.Util.Excel.ExcelDownload();
        ed.ExcelFileDown(dt, "检验数据查询");

    }
    public PageResult<QmtQrigMaster> GetDetailDataByQueryParams(IQmtQrigMasterQueryParams queryParams)
    {
       
        IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();
        StringBuilder sb = new StringBuilder();
        #region

        sb.AppendLine("Select TA.PlanDate [生产日期], TB.EquipName [生产机台],  TD.ClassName [生产班组]");
        sb.AppendLine(",  TE.MaterialName [胶料名称], TA.SerialId [车次]");
        sb.AppendLine(", TA.TestType [检验类型], TA.CheckNum [检验次数], TA.CheckPlan_Date [检验日期], TA.CheckDate + ' ' + TA.CheckTime [检验时间]");
        sb.AppendLine(", TA.CheckClassName [检验班组],  TA.StandCode [检验标准分类], TA.CheckEquipName [检验机台]");
        sb.AppendLine(", TL.ItemName [检验项], TK.ItemCheck [检验值], TM.PermMin [最小值], TM.PermMax [最大值]");
        sb.AppendLine("FROM dbo.QmtQrigMaster TA");
        sb.AppendLine("LEFT JOIN dbo.BasEquip TB ON TA.EquipCode = TB.EquipCode");
     
        sb.AppendLine("LEFT JOIN dbo.PptClass TD ON TA.ShiftClass = TD.ClassName");
        sb.AppendLine("LEFT JOIN dbo.BasMaterial TE ON TA.MaterCode = TE.MaterialCode");
    sb.AppendLine("Join dbo.QmtQrigDetail TK On TA.SeqNo = TK.SeqNo");
        sb.AppendLine("Left Join dbo.QmtCheckItem TL On TK.ItemCd = TL.ItemCode");
        sb.AppendLine("Left Join dbo.QmtCheckStandDetail TM On TK.StandId = TM.StandId And TK.ItemCd = TM.ItemCd and TM.JudgeResult=1");

        sb.AppendLine("WHERE  1=1");
        if (!string.IsNullOrEmpty(queryParams.SPlanDate))
            sb.AppendLine("AND TA.PlanDate>='" + queryParams.SPlanDate + "'");
        if (!string.IsNullOrEmpty(queryParams.EPlanDate))
            sb.AppendLine("AND TA.PlanDate<='" + queryParams.EPlanDate + "'");
        if (!string.IsNullOrEmpty(queryParams.SCheckDate))
            sb.AppendLine("AND TA.CheckPlan_Date>='" + queryParams.SCheckDate + "'");
        if (!string.IsNullOrEmpty(queryParams.ECheckDate))
            sb.AppendLine("AND TA.CheckPlan_Date<'" + queryParams.ECheckDate + "'");
        if (!string.IsNullOrEmpty(queryParams.EquipCode))
            sb.AppendLine("AND TA.EquipCode='" + queryParams.EquipCode + "'");
        if (!string.IsNullOrEmpty(queryParams.ShiftId))
            sb.AppendLine("AND TA.ShiftId = '" + queryParams.ShiftId + "'");
        if (!string.IsNullOrEmpty(queryParams.ShiftClass))
            sb.AppendLine("AND TA.ShiftClass='" + queryParams.ShiftClass + "'");
        if (!string.IsNullOrEmpty(queryParams.CheckPlanClass))
            sb.AppendLine("AND TA.CheckPlan_Class='" + queryParams.CheckPlanClass + "'");
        if (!string.IsNullOrEmpty(queryParams.WorkerBarcode))
            sb.AppendLine("AND TA.WorkerBarcode='" + queryParams.WorkerBarcode + "'");
        if (!string.IsNullOrEmpty(queryParams.CheckEquipCode))
            sb.AppendLine("AND TA.CheckEquipCode='" + queryParams.CheckEquipCode + "'");
        if (!string.IsNullOrEmpty(queryParams.MaterCode))
            sb.AppendLine("AND TA.MaterCode='" + queryParams.MaterCode + "'");
        if (!string.IsNullOrEmpty(queryParams.TestType))
            sb.AppendLine("AND TA.TestType LIKE '%" + queryParams.TestType + "%'");
        if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
            sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");

        if (!string.IsNullOrEmpty(queryParams.ZJSID))
        {
            sb.AppendLine("AND TA.ZJSID='" + queryParams.ZJSID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.CheckItemTypeId))
        {
            sb.AppendLine("AND EXISTS (");
            if (queryParams.CheckItemTypeId == "208")
            {
                sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.ItemCode = '208'" );
            }
            else
            {
                sb.AppendLine(" SELECT * FROM QmtQrigDetail A INNER JOIN QmtCheckItem B ON A.ItemCd = B.ItemCode WHERE TA.SeqNo = A.SeqNo AND B.TypeID = " + queryParams.CheckItemTypeId);
            } sb.AppendLine(")");
        }
        if (!string.IsNullOrEmpty(queryParams.StandCode))
        {
           
                sb.AppendLine("AND TA.StandCode = '" + queryParams.StandCode +"' ");
           
        }
      
        PageResult<QmtQrigMaster> pageParams = queryParams.PageParams;

        if (pageParams != null && pageParams.PageSize > 0)
        {
            pageParams.QueryStr = sb.ToString();
            if (pageParams.Orderfld == "" || pageParams.Orderfld.ToLower() == "seqno")
            {
                pageParams.Orderfld = "PlanDate,ShiftId,EquipCode,ZJSID,MaterCode,SerialId,LLSerialID,CheckDate,CheckTime";
            }
            return bQmtQrigMasterManager.GetPageDataBySql(pageParams);
        }
        else
        {
            sb.AppendLine("ORDER BY TA.PlanDate,TA.ShiftId,TA.EquipCode,TA.ZJSID,TA.MaterCode,TA.SerialId,TA.LLSerialID,TA.CheckDate,TA.CheckTime");

            NBear.Data.CustomSqlSection css = bQmtQrigMasterManager.GetBySql(sb.ToString());
            if (pageParams == null)
            {
                pageParams = new PageResult<QmtQrigMaster>();
            }
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }

        #endregion

    }
    #endregion 导出Excel
}