﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Ext.Net;
using Ext.Net.Utilities;

using NBear.Common;
using NBear.Data;

using Mesnac.Entity;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Web.UI;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

public partial class Manager_RubberQuality_BasicInfo_CheckStand : PageMark
{
    private string _Type;

    private object[] _MajorTypes = { 5 };

    #region 权限定义
    protected __ _;// = new __();

    IQmt_QuaStandMasterManager qm = new Qmt_QuaStandMasterManager();
    IQmt_QuaStandDetailManager qdm = new Qmt_QuaStandDetailManager();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
        }

        public __(string type)
            : this()
        {
            if (type == "1")
            {
                查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
                添加 = new SysPageAction() { ActionID = 2, ActionName = "ButtonMasterAdd" };
                修改 = new SysPageAction() { ActionID = 3, ActionName = "ButtonMasterEdit" };
                删除 = new SysPageAction() { ActionID = 4, ActionName = "ButtonMasterDelete" };
                启停 = new SysPageAction() { ActionID = 5, ActionName = "ButtonMasterKey" };
                复制 = new SysPageAction() { ActionID = 6, ActionName = "ButtonMasterCopy" };
                审核 = new SysPageAction() { ActionID = 7, ActionName = "ButtonMasterAudit", DeleteFlag = "1" };
                修改生效时间 = new SysPageAction() { ActionID = 8, ActionName = "ButtonMasterEditRegDateTime", DeleteFlag = "1" };
            }
            else if (type == "2")
            {
                查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
                添加 = new SysPageAction() { ActionID = 2, ActionName = "ButtonMasterAdd", DeleteFlag = "1" };
                修改 = new SysPageAction() { ActionID = 3, ActionName = "ButtonMasterEdit", DeleteFlag = "1" };
                删除 = new SysPageAction() { ActionID = 4, ActionName = "ButtonMasterDelete", DeleteFlag = "1" };
                //启停 = new SysPageAction() { ActionID = 5, ActionName = "ButtonMasterKey", DeleteFlag = "1" };
                启停 = new SysPageAction() { ActionID = 5, ActionName = "ButtonMasterKey"};
                复制 = new SysPageAction() { ActionID = 6, ActionName = "ButtonMasterCopy", DeleteFlag = "1" };
                审核 = new SysPageAction() { ActionID = 7, ActionName = "ButtonMasterAudit" };
                修改生效时间 = new SysPageAction() { ActionID = 8, ActionName = "ButtonMasterEditRegDateTime" };
            }
        }

        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 启停 { get; private set; } //必须为 public
        public SysPageAction 复制 { get; private set; } //必须为 public
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 修改生效时间 { get; private set; } //必须为 public
    }

    protected override void OnInit(EventArgs e)
    {
        _Type = Request.QueryString["Type"];
        _ = new __(_Type);

        base.OnInit(e);
    }

    #endregion

    #region 结构
    private enum EnumStandVisionStat
    {
        /// <summary>
        /// 已启用
        /// </summary>
        Enabled = 1,
        /// <summary>
        /// 已停用
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 已作废
        /// </summary>
        Invalid = 2,
        /// <summary>
        /// 未提交
        /// </summary>
        New = 3,
        /// <summary>
        /// 已提交
        /// </summary>
        Submitted = 4,
        /// <summary>
        /// 已退回
        /// </summary>
        Sendback = 5,

    }

    private enum EnumCommandName
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,

    }

    private struct DigitalRange
    {
        public int IfMin;
        public int IfMax;
        public decimal PermMin;
        public decimal PermMax;

        public DigitalRange(decimal _permMin, int _ifMin, decimal _permMax, int _ifMax)
        {
            IfMin = _ifMin;
            IfMax = _ifMax;
            PermMin = _permMin;
            PermMax = _permMax;
        }
    }

    #endregion 结构

    #region 页面加载

    /// <summary>
    /// 返回下级节点
    /// 修改标识：qusf 20131018
    /// 修改内容：1.增加对物料大类的条件
    /// </summary>
    /// <param name="majorTypeID"></param>
    /// <param name="minorTypeID"></param>
    /// <param name="materCode"></param>
    /// <returns></returns>
    private NodeCollection LoadChildNodes(string majorTypeID, string minorTypeID, string materCode)
    {
        NodeCollection nodes = new NodeCollection();

        // 加载
        if (majorTypeID == "" && minorTypeID == "" && materCode == "")
        {
            // 加载物料大类
            IBasMaterialMajorTypeManager bBasMaterialMajorTypeManager = new BasMaterialMajorTypeManager();
            EntityArrayList<BasMaterialMajorType> mBasMaterialMajorTypeList = bBasMaterialMajorTypeManager.GetListByWhereAndOrder(
                BasMaterialMajorType._.DeleteFlag == "0"
                & BasMaterialMajorType._.ObjID.In(_MajorTypes)
                , BasMaterialMajorType._.ObjID.Asc);
            foreach (BasMaterialMajorType mBasMaterialMajorType in mBasMaterialMajorTypeList)
            {
                Node asyncNode = new Node();
                asyncNode.Text = mBasMaterialMajorType.MajorTypeName;
                asyncNode.NodeID = "MajorType_" + mBasMaterialMajorType.ObjID.ToString();
                asyncNode.CustomAttributes.Add(new ConfigItem("MajorTypeID", mBasMaterialMajorType.ObjID.ToString(), ParameterMode.Value));
                asyncNode.CustomAttributes.Add(new ConfigItem("MinorTypeID", "", ParameterMode.Value));
                asyncNode.CustomAttributes.Add(new ConfigItem("MaterCode", "", ParameterMode.Value));
                nodes.Add(asyncNode);
            }

        }
        else if (minorTypeID == "" && materCode == "")
        {
            // 加载物料小类
            BasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
            EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
                BasMaterialMinorType._.DeleteFlag == "0"
                & BasMaterialMinorType._.MajorID == majorTypeID
                , BasMaterialMinorType._.MinorTypeID.Asc);
            foreach (BasMaterialMinorType mBasMaterialMinorType in mBasMaterialMinorTypeList)
            {
                Node asyncNode = new Node();
                asyncNode.Text = mBasMaterialMinorType.MinorTypeName;
                asyncNode.NodeID = "MinorType_" + mBasMaterialMinorType.MinorTypeID;
                asyncNode.CustomAttributes.Add(new ConfigItem("MajorTypeID", mBasMaterialMinorType.MajorID.Value.ToString(), ParameterMode.Value));
                asyncNode.CustomAttributes.Add(new ConfigItem("MinorTypeID", mBasMaterialMinorType.MinorTypeID, ParameterMode.Value));
                asyncNode.CustomAttributes.Add(new ConfigItem("MaterCode", "", ParameterMode.Value));
                nodes.Add(asyncNode);
            }
        }
        else if (materCode == "")
        {
            // 加载物料
            BasMaterialManager bBasMaterialManager = new BasMaterialManager();
            EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
                BasMaterial._.DeleteFlag == "0"
                & BasMaterial._.MajorTypeID == majorTypeID & BasMaterial._.MinorTypeID == minorTypeID
                , BasMaterial._.MaterialName.Asc);
            foreach (BasMaterial mBasMaterial in mBasMaterialList)
            {
                Node asyncNode = new Node();
                asyncNode.Leaf = true;
                asyncNode.Text = mBasMaterial.MaterialName;
                asyncNode.Qtip = mBasMaterial.MaterialName;
                asyncNode.NodeID = "Mater_" + mBasMaterial.ObjID.ToString();
                asyncNode.CustomAttributes.Add(new ConfigItem("MajorTypeID", mBasMaterial.MajorTypeID.Value.ToString(), ParameterMode.Value));
                asyncNode.CustomAttributes.Add(new ConfigItem("MinorTypeID", mBasMaterial.MinorTypeID, ParameterMode.Value));
                asyncNode.CustomAttributes.Add(new ConfigItem("MaterCode", mBasMaterial.MaterialCode.ToString(), ParameterMode.Value));
                nodes.Add(asyncNode);
            }
        }

        return nodes;
    }

    /// <summary>
    /// 初始化控件
    /// 修改标识：qusf 20131009
    /// 修改内容：1.增加对审核窗体的处理
    /// </summary>
    private void InitControls()
    {
        if (TreePanelMater.Root.Count() > 0)
        {
            #region 加载物料树
            Node rootNode = TreePanelMater.Root[0];
            NodeCollection nodes = LoadChildNodes("", "", "");
            rootNode.Children.AddRange(nodes);
            rootNode.Expanded = rootNode.Children.Count > 0;
            #endregion 加载物料树
        }

        #region 加载检验标准类型
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(QmtCheckStandType._.DeleteFlag == "0", QmtCheckStandType._.ObjID.Asc);
        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            ComboBoxMasterStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID));
            ComboBoxNorthStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID));
            ComboBoxMasterCopyStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID));
            ComboBoxMasterAimStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID));

            ComboBoxAuditStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID));
        }
        ComboBoxNorthStandCode.Items.Insert(0, new Ext.Net.ListItem("全部", -1));
        ComboBoxNorthStandCode.SetValue(-1);
        DataSet ds = bQmtCheckStandTypeManager.GetBySql(@"select * from SysCode
where TypeID = 'pmttype'
order by len(ItemCode),ItemCode").ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        { 
        ComboBoxRubType.Items.Add(new Ext.Net.ListItem(dr["ItemName"].ToString(), dr["ItemCode"].ToString()));
        }
        ComboBoxRubType.Items.Insert(0, new Ext.Net.ListItem("全部", -1));
        ComboBoxRubType.SetValue(-1);

        ds = bQmtCheckStandTypeManager.GetBySql(@"select * from dbo.Ppt_SetTime
where depttype ='1'").ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            RubType.Items.Add(new Ext.Net.ListItem(dr["shift_Name"].ToString(), dr["shift_id"].ToString()));
            ComboBoxShift.Items.Add(new Ext.Net.ListItem(dr["shift_Name"].ToString(), dr["shift_id"].ToString()));
        }

        ds = bQmtCheckStandTypeManager.GetBySql(@"select * from Qmt_dealnotion").ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ComboBoxDetailQuaFrequency.Items.Add(new Ext.Net.ListItem(dr["Deal_notion"].ToString(), dr["Deal_code"].ToString()));
        }


        ComboBoxJudge.Items.Add(new Ext.Net.ListItem("不合格", "0"));
        ComboBoxJudge.Items.Add(new Ext.Net.ListItem("合格", "1"));
        //RubType.SetValue(1);
        #endregion 加载检验标准类型

        ComboBoxMasterStandVisionStat.Items.Add(new Ext.Net.ListItem("未提交", Convert.ToInt32(EnumStandVisionStat.New).ToString()));
        ComboBoxMasterStandVisionStat.Items.Add(new Ext.Net.ListItem("已提交", Convert.ToInt32(EnumStandVisionStat.Submitted).ToString()));

        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("未提交", Convert.ToInt32(EnumStandVisionStat.New).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已提交", Convert.ToInt32(EnumStandVisionStat.Submitted).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已退回", Convert.ToInt32(EnumStandVisionStat.Sendback).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已启用", Convert.ToInt32(EnumStandVisionStat.Enabled).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已停用", Convert.ToInt32(EnumStandVisionStat.Disabled).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已作废", Convert.ToInt32(EnumStandVisionStat.Invalid).ToString()));
        ComboBoxNorthStandVisionStat.Items.Insert(0, new Ext.Net.ListItem("全部", "-1"));

        ComboBoxMasterCopyStandVisionStat.Items.Add(new Ext.Net.ListItem("已启用", Convert.ToInt32(EnumStandVisionStat.Enabled).ToString()));
        ComboBoxMasterCopyStandVisionStat.Items.Add(new Ext.Net.ListItem("已停用", Convert.ToInt32(EnumStandVisionStat.Disabled).ToString()));
        ComboBoxMasterCopyStandVisionStat.Items.Add(new Ext.Net.ListItem("已作废", Convert.ToInt32(EnumStandVisionStat.Invalid).ToString()));

        ComboBoxAuditStandVisionStat.Items.Add(new Ext.Net.ListItem("已提交", Convert.ToInt32(EnumStandVisionStat.Submitted).ToString()));

        #region 加载检测项目
        IQmtCheckItemManager bQmtCheckItemManager = new QmtCheckItemManager();
        EntityArrayList<QmtCheckItem> mQmtCheckItemList = bQmtCheckItemManager.GetListByWhereAndOrder(QmtCheckItem._.DeleteFlag == "0", QmtCheckItem._.ItemCode.Asc);
        foreach (QmtCheckItem mQmtCheckItem in mQmtCheckItemList)
        {
            ComboBoxDetailItemCd.Items.Add(new Ext.Net.ListItem(mQmtCheckItem.ItemName, mQmtCheckItem.ItemCode));
        }
        #endregion 加载检测项目

        #region 加载处理意见
        IQmtDealNotionManager bQmtDealNotionManager = new QmtDealNotionManager();
        EntityArrayList<QmtDealNotion> mQmtDealNotionList = bQmtDealNotionManager.GetListByWhereAndOrder(QmtDealNotion._.DeleteFlag == "0", QmtDealNotion._.ObjID.Asc);
        foreach (QmtDealNotion mQmtDealNotion in mQmtDealNotionList)
        {
          
            ComboBoxGradeDealCode.Items.Add(new Ext.Net.ListItem(mQmtDealNotion.DealNotion, mQmtDealNotion.ObjID));
            ComboBoxEquipDealCode.Items.Add(new Ext.Net.ListItem(mQmtDealNotion.DealNotion, mQmtDealNotion.ObjID));
            ComboBoxEquipGradeDealCode.Items.Add(new Ext.Net.ListItem(mQmtDealNotion.DealNotion, mQmtDealNotion.ObjID));
        }
        #endregion 加载处理意见

    }


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
            System.Web.UI.HtmlControls.HtmlGenericControl cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            #region 加载JS文件
            System.Web.UI.HtmlControls.HtmlGenericControl scriptLink = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "CheckStand.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            #region 处理按钮
            if (_Type == "1")
            {
                ButtonNorthQuery.Disabled = this._.查询.SeqIdx == 0 && this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0
                    && this._.删除.SeqIdx == 0 && this._.启停.SeqIdx == 0 && this._.复制.SeqIdx == 0;
                ButtonMasterAdd.Disabled = this._.添加.SeqIdx == 0;
                ButtonMasterEdit.Disabled = this._.修改.SeqIdx == 0;
                ButtonMasterDelete.Disabled = this._.删除.SeqIdx == 0;
                ButtonMasterKey.Disabled = this._.启停.SeqIdx == 0;
                ButtonMasterCopy.Disabled = this._.复制.SeqIdx == 0;

                ButtonDetailAdd.Disabled = this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0;
                ButtonDetailEdit.Disabled = this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0;
                ButtonDetailDelete.Disabled = this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0;

                ButtonGradeAdd.Disabled = this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0;
                ButtonGradeEdit.Disabled = this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0;
                ButtonGradeDelete.Disabled = this._.添加.SeqIdx == 0 && this._.修改.SeqIdx == 0;

                ButtonMasterAudit.Hidden = true;
                ButtonMasterEditRegDateTime.Hidden = true;
                ButtonMasterKey.Hidden = true;
            }
            else if (_Type == "2")
            {
                ButtonMasterAdd.Hidden = true;
                ButtonMasterEdit.Hidden = true;
                ButtonMasterDelete.Hidden = true;
                //ButtonMasterKey.Hidden = true;
                ButtonMasterCopy.Hidden = true;

                ButtonDetailAdd.Hidden = true;
                ButtonDetailEdit.Hidden = true;
                ButtonDetailDelete.Hidden = true;

                ButtonGradeAdd.Hidden = true;
                ButtonGradeEdit.Hidden = true;
                ButtonGradeDelete.Hidden = true;

                ButtonNorthQuery.Disabled = this._.查询.SeqIdx == 0 && this._.审核.SeqIdx == 0;
                ButtonMasterAudit.Disabled = this._.审核.SeqIdx == 0;
                ButtonMasterKey.Disabled = this._.启停.SeqIdx == 0;

                ButtonMasterEditRegDateTime.Disabled = this._.修改生效时间.SeqIdx == 0;
                ButtonMasterEditRegDateTime.Hidden = true;
                ButtonNothOperMater.Hidden = true;
            }

            #endregion 处理按钮

            InitControls();

            if (_Type == "1")
            {
                ComboBoxNorthStandVisionStat.SetValue(Convert.ToInt32(EnumStandVisionStat.Enabled).ToString());

                StoreMaster.Sorters.Add(new DataSorter { Property = "LastOperateTime", Direction = Ext.Net.SortDirection.DESC });
            }
            else if (_Type == "2")
            {
            ComboBoxNorthStandVisionStat.SetValue(Convert.ToInt32(EnumStandVisionStat.Submitted).ToString());
            }
        }

    }

    #endregion 页面加载

    #region 查询

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        //string materCode = HiddenNorthMaterCode.Value.ToString();
        //if (materCode == "" && _Type == "1")
        //{
        //    X.Msg.Alert("提示", "请选择要查询的胶料").Show();
        //    return;
        //}

        QueryGridPanelMaster("0");

    }
    /// <summary>
    /// 历史数据查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonHistory_Click(object sender, DirectEventArgs e)
    {
        //string materCode = HiddenNorthMaterCode.Value.ToString();
        //if (materCode == "" && _Type == "1")
        //{
        //    X.Msg.Alert("提示", "请选择要查询的胶料").Show();
        //    return;
        //}

        QueryGridPanelMaster("1");

    }

    /// <summary>
    /// 查询选择物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldNorthMaterName_Click(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"].ToString();
        if (index == "0")
        {
            TriggerFieldNorthMaterName.SetValue("");
            HiddenNorthMaterCode.SetValue("");
        }
        else if (index == "1")
        {
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();");
        }
        //((Window)ControlUtils.FindControl(this, "Manager_BasicInfo_CommonPage_QueryMaterial_Window")).Show();

    }

    #endregion 查询

    #region 物料树操作

    #region 筛选

    /// <summary>
    /// 返回筛选出的节点
    /// 修改标识：qusf 20131018
    /// 修改内容：1.增加对物料大类的条件
    /// </summary>
    /// <param name="filterText"></param>
    /// <returns></returns>
    private NodeCollection FilterNode(string filterText)
    {
        NodeCollection nodes = new NodeCollection();
        filterText = filterText.Trim();
        if (filterText != "")
        {
            // 加载物料大类
            IBasMaterialMajorTypeManager bBasMaterialMajorTypeManager = new BasMaterialMajorTypeManager();
            EntityArrayList<BasMaterialMajorType> mBasMaterialMajorTypeList = bBasMaterialMajorTypeManager.GetListByWhereAndOrder(
                BasMaterialMajorType._.DeleteFlag == "0"
                & BasMaterialMajorType._.ObjID.In(_MajorTypes)
                , BasMaterialMajorType._.ObjID.Asc);

            // 加载物料小类
            BasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
            EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
                BasMaterialMinorType._.DeleteFlag == "0"
                & BasMaterialMinorType._.MajorID.In(_MajorTypes)
                , BasMaterialMinorType._.MinorTypeID.Asc);

            // 加载物料
            BasMaterialManager bBasMaterialManager = new BasMaterialManager();
            EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
                BasMaterial._.DeleteFlag == "0"
                & BasMaterial._.MajorTypeID.In(_MajorTypes)
                & BasMaterial._.MaterialName.Like("%" + filterText + "%")
                , BasMaterial._.MajorTypeID.Asc & BasMaterial._.MinorTypeID.Asc & BasMaterial._.MaterialName.Asc);

            string majorTypeID = "";
            string minorTypeID = "";
            //string materCode = "";

            Node majorTypeNode = null;
            Node minorTypeNode = null;
            Node materNode = null;
            foreach (BasMaterial mBasMaterial in mBasMaterialList)
            {
                if (mBasMaterial.MajorTypeID.HasValue == false)
                {
                    majorTypeID = "";
                    minorTypeID = "";
                    continue;
                }

                if (majorTypeID != mBasMaterial.MajorTypeID.Value.ToString())
                {
                    majorTypeID = mBasMaterial.MajorTypeID.Value.ToString();
                    minorTypeID = "";
                    BasMaterialMajorType[] majorTypeArray = mBasMaterialMajorTypeList.Filter(BasMaterialMajorType._.ObjID == majorTypeID);
                    if (majorTypeArray.Length == 0)
                    {
                        //mBasMaterialList.Remove(mBasMaterial);
                        continue;
                    }
                    else
                    {
                        majorTypeNode = new Node();
                        majorTypeNode.Text = majorTypeArray[0].MajorTypeName;
                        majorTypeNode.NodeID = "MajorType_" + majorTypeID;
                        majorTypeNode.Expanded = true;
                        majorTypeNode.CustomAttributes.Add(new ConfigItem("MajorTypeID", majorTypeID, ParameterMode.Value));
                        majorTypeNode.CustomAttributes.Add(new ConfigItem("MinorTypeID", "", ParameterMode.Value));
                        majorTypeNode.CustomAttributes.Add(new ConfigItem("MaterCode", "", ParameterMode.Value));
                        nodes.Add(majorTypeNode);
                    }
                }
                if (StringUtils.IsEmpty(mBasMaterial.MinorTypeID) == true)
                {
                    minorTypeID = "";
                    continue;
                }
                if (minorTypeID != mBasMaterial.MinorTypeID)
                {
                    minorTypeID = mBasMaterial.MinorTypeID;
                    BasMaterialMinorType[] minorTypeArray = mBasMaterialMinorTypeList.Filter(BasMaterialMinorType._.MajorID == majorTypeID & BasMaterialMinorType._.MinorTypeID == minorTypeID);
                    if (minorTypeArray.Length == 0)
                    {
                        //mBasMaterialList.Remove(mBasMaterial);
                        continue;
                    }
                    else
                    {
                        minorTypeNode = new Node();
                        minorTypeNode.Text = minorTypeArray[0].MinorTypeName;
                        minorTypeNode.NodeID = "MinorType_" + minorTypeArray[0].ObjID.ToString();
                        minorTypeNode.Expanded = true;
                        minorTypeNode.CustomAttributes.Add(new ConfigItem("MajorTypeID", majorTypeID, ParameterMode.Value));
                        minorTypeNode.CustomAttributes.Add(new ConfigItem("MinorTypeID", minorTypeID, ParameterMode.Value));
                        minorTypeNode.CustomAttributes.Add(new ConfigItem("MaterCode", "", ParameterMode.Value));
                        majorTypeNode.Children.Add(minorTypeNode);
                    }
                }

                materNode = new Node();
                materNode.Leaf = true;
                materNode.Text = mBasMaterial.MaterialName;
                materNode.Qtip = mBasMaterial.MaterialName;
                materNode.NodeID = "Mater_" + mBasMaterial.ObjID.ToString();
                materNode.CustomAttributes.Add(new ConfigItem("MajorTypeID", mBasMaterial.MajorTypeID.Value.ToString(), ParameterMode.Value));
                materNode.CustomAttributes.Add(new ConfigItem("MinorTypeID", mBasMaterial.MinorTypeID, ParameterMode.Value));
                materNode.CustomAttributes.Add(new ConfigItem("MaterCode", mBasMaterial.MaterialCode.ToString(), ParameterMode.Value));
                minorTypeNode.Children.Add(materNode);
            }
        }
        return nodes;
    }

    /// <summary>
    /// 筛选(DirectEvent)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldMater_KeyUp(object sender, DirectEventArgs e)
    {
        string filterText = e.ExtraParams["FilterText"];

        NodeCollection nodes = FilterNode(filterText);

        NodeProxy rootNode = TreePanelMater.GetRootNode();
        rootNode.RemoveAll();
        if (nodes.Count > 0)
        {
            rootNode.AppendChild(nodes);
        }

        rootNode.Expand(true);

    }

    #endregion 筛选

    #region 清除筛选
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldMater_TriggerClick(object sender, DirectEventArgs e)
    {
        NodeCollection nodes = LoadChildNodes("", "", "");

        TriggerFieldMater.SetValue("");

        NodeProxy rootNode = TreePanelMater.GetRootNode();
        rootNode.RemoveAll();
        if (nodes.Count > 0)
        {
            rootNode.AppendChild(nodes);
        }

        rootNode.Expand(false);
    }
    #endregion 清除筛选

    #region 选择
    /// <summary>
    /// 选择物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TreePanelMater_ItemClick(object sender, DirectEventArgs e)
    {
        string nodeID = e.ExtraParams["NodeID"];
        string majorTypeID = e.ExtraParams["MajorTypeID"];
        string minorTypeID = e.ExtraParams["MinorTypeID"];
        string materCode = e.ExtraParams["MaterCode"];
        if (materCode == "")
        {
            return;
        }
        LoadGridPanelMaster(materCode);

    }
    #endregion 选择

    /// <summary>
    /// 加载下级节点
    /// </summary>
    /// <param name="nodeID"></param>
    /// <param name="majorTypeID"></param>
    /// <param name="minorTypeID"></param>
    /// <param name="materCode"></param>
    [DirectMethod]
    public string NodeLoad(string nodeID, string majorTypeID, string minorTypeID, string materCode)
    {
        NodeCollection nodes = LoadChildNodes(majorTypeID, minorTypeID, materCode);

        return nodes.ToJson();
    }

    /// <summary>
    /// 加载检验标准列表
    /// </summary>
    /// <param name="materCode"></param>
    private void LoadGridPanelMaster(string materCode)
    {
        StoreEquipGrade.RemoveAll();
        StoreEquip.RemoveAll();
        StoreGrade.RemoveAll();
        StoreDetail.RemoveAll();
        StoreMaster.RemoveAll();
        if (materCode != "")
        {
            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            IQmtCheckStandMasterParams p = new QmtCheckStandMasterParams();
            p.MaterCode = materCode;
            p.DeleteFlag = "0";
            DataSet ds = bQmtCheckStandMasterManager.GetDataByParas(p);
            GroupingMaster.ExpandAll();
            StoreMaster.DataSource = ds.Tables[0];
            StoreMaster.DataBind();
        }

    }

    #endregion 物料菜单树操作

    #region 检验标准操作

    /// <summary>
    /// 加载检验标准明细列表
    /// </summary>
    /// <param name="standId"></param>
    private void LoadGridPanelDetail(string standId)
    {
        StoreEquipGrade.RemoveAll();
        StoreEquip.RemoveAll();
        StoreGrade.RemoveAll();
        StoreDetail.RemoveAll();

        if (standId != "")
        {
            IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
            IQmtCheckStandDetailParams p = new QmtCheckStandDetailParams();
            p.StandId = standId;
            p.DeleteFlag = "0";
            DataSet ds = GetDataByParas(p);
            StoreDetail.DataSource = ds.Tables[0];
            StoreDetail.DataBind();
        }

    }
    public DataSet GetDataByParas(IQmtCheckStandDetailParams queryParams)
    {
        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"  SELECT TA.StandId,TA.ItemCd,TA.WeightId,TA.IfMin,TA.IfMax,
  case (JudgeResult) when '0' then ''  else '合格' end as JudgeResult,TA.Grade,TA.DrawMark,
  TA.DealCode,TA.CardMark2,TA.QuaFrequency,TA.DeleteFlag,TA.GUID,TB.ItemName,TC.DealNotion,
  case ItemCd when '203' then  cast( floor(PermMin/60) as varchar(2))+':'+right('0'+cast(floor(PermMin%60) as varchar(2)),2)  else cast(PermMin as varchar(10)) end as PermMin
  ,case ItemCd when '203' then  cast( floor(TA.PermMax/60) as varchar(2))+':'+right('0'+cast(floor(TA.PermMax%60) as varchar(2)),2)  else cast(TA.PermMax as varchar(10)) end as PermMax  
   FROM QmtCheckStandDetail TA
   LEFT JOIN QmtCheckItem TB ON TA.ItemCd=TB.ItemCode
   LEFT JOIN QmtDealNotion TC ON TA.DealCode=TC.ObjID
");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(queryParams.StandId))
            sb.AppendLine("AND TA.StandId=" + queryParams.StandId);
        if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
        {
            sb.AppendLine("AND TA.DeleteFlag='" + queryParams.DeleteFlag + "'");
        }

        sb.AppendLine("order by Display_id,DealCode");
        #endregion

        NBear.Data.CustomSqlSection css = bQmtCheckStandDetailManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    /// <summary>
    /// 选择检验标准
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelMaster_SelectionChange(object sender, DirectEventArgs e)
    {
        string standId = e.ExtraParams["StandId"];
        //X.Js.Alert(standId); return;
        if (standId != "")
        {
            //IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            //QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
            //HiddenStandId.SetValue(mQmtCheckStandMaster.StandId);
            HiddenStandId.SetValue(standId);

        }
        else
        {
            HiddenStandId.SetValue("");
        }
        LoadGridPanelDetail(standId);
    }

    #region 检验标准添加/修改

    /// <summary>
    /// 初始化--检验标准添加
    /// 修改标识：qusf 20131022
    /// 修改内容：1.增加玲珑版本的处理
    /// 修改标识：qusf 20131213
    /// 修改内容：1.增加生效日期和时间的处理
    /// </summary>
    private void ClearPanelMaster()
    {
        ComboBoxMasterStandCode.SetValue("");
        TriggerFieldMasterMaterName.SetValue("");
        ComboBoxMasterStandVisionStat.SetValue("");
        CheckboxMasterQuaCompute.SetValue(true);
        CheckboxMasterChoiceness.SetValue(false);
        TextFieldMasterLLStandVision.SetValue("");
        txtbanben.SetValue("");

        DateFieldMasterRegDateTime.SetValue("");
        TimeFieldMasterRegDateTime.SetValue("00:00:00");
    }

    /// <summary>
    /// 打开窗口--检验标准添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterAdd_Click(object sender, EventArgs e)
    {
        ClearPanelMaster();

        ComboBoxMasterStandCode.Disabled = false;
        ComboBoxMasterStandCode.Editable = false;
        TriggerFieldMasterMaterName.Disabled = false;
        TriggerFieldMasterMaterName.Editable = false;
        ButtonMasterSubmit.Hidden = true;

        HiddenButtonCommandName.SetValue(EnumCommandName.Add); //Add

        WindowMaster.Title = "添加检验标准信息";
        WindowMaster.Icon = Icon.PageAdd;
        WindowMaster.Show();

    }

    /// <summary>
    /// 打开窗口--检验标准修改
    /// 修改标识：qusf 20131022
    /// 修改内容：1.增加玲珑版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterEdit_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要修改/提交审核的检验标准").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        //X.Msg.Alert("提示", standId).Show();
        //return;
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId == standId)[0];
        if (mQmtCheckStandMaster != null && mQmtCheckStandMaster.StandId > 0 && mQmtCheckStandMaster.DeleteFlag == "0")
        {
            if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
                && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
            {
                string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
                X.Msg.Alert("提示", "版本状态为" + standVisionStatDesp + "，不允许修改").Show();
                return;
            }
            ClearPanelMaster();
            ComboBoxMasterStandCode.SetValue(mQmtCheckStandMaster.StandCode);
            HiddenMasterMaterCode.SetValue(mQmtCheckStandMaster.MaterCode);
            IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
            EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == mQmtCheckStandMaster.MaterCode);
            if (mBasMaterialList.Count > 0)
            {
                TriggerFieldMasterMaterName.SetValue(mBasMaterialList[0].MaterialName);
            }

            CheckboxMasterQuaCompute.SetValue(mQmtCheckStandMaster.QuaCompute);
            CheckboxMasterChoiceness.SetValue(mQmtCheckStandMaster.Choiceness);
            ComboBoxMasterStandVisionStat.SetValue(mQmtCheckStandMaster.StandVisionStat);
            RubType.SetValue(mQmtCheckStandMaster.PmtType);
            TextFieldMasterLLStandVision.SetValue(mQmtCheckStandMaster.LLStandVision);
            txtbanben.SetValue(mQmtCheckStandMaster.StandVision);
            DateFieldMasterRegDateTime.SetValue(mQmtCheckStandMaster.RegDateTime.Value.ToString("yyyy-MM-dd"));
            TimeFieldMasterRegDateTime.SetValue(mQmtCheckStandMaster.RegDateTime.Value.ToString("HH:mm:ss"));

            ComboBoxMasterStandCode.Disabled = true;
            TriggerFieldMasterMaterName.Disabled = true;

            ButtonMasterSubmit.Hidden = false;

            HiddenButtonCommandName.SetValue(EnumCommandName.Update); // Update

            WindowMaster.Title = "修改检验标准信息";
            WindowMaster.Icon = Icon.PageEdit;
            WindowMaster.Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要修改的检验标准").Show();
        }

    }

    /// <summary>
    /// 删除检验标准
    /// 修改标识：qusf 20140930
    /// 修改说明：1.删除记录日志
    ///           2.删除后状态标志为已启用的则改为已停用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterDelete_Click(object sender, EventArgs e)
    {
        string standId = HiddenStandId.Value.ToString();
        if (RowSelectionModelMaster.SelectedRows.Count == 0 || standId == "")
        {
            X.Msg.Alert("提示", "请选择要删除的检验标准").Show();
            return;
        }

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
        if (mQmtCheckStandMaster != null && mQmtCheckStandMaster.StandId > 0 && mQmtCheckStandMaster.DeleteFlag == "0")
        {
            if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
                && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
            {
                string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
                X.Msg.Alert("提示", "版本状态为" + standVisionStatDesp + "，不允许删除").Show();
                return;
                mQmtCheckStandMaster.DeleteFlag = "1";
                if (mQmtCheckStandMaster.StandVisionStat == Convert.ToInt32(EnumStandVisionStat.Enabled).ToString())
                {
                    mQmtCheckStandMaster.StandVisionStat = Convert.ToInt32(EnumStandVisionStat.Disabled).ToString();
                }
                mQmtCheckStandMaster.LastModifyTime = DateTime.Now;
                mQmtCheckStandMaster.WorkerBarcode = this.UserID;
                bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);
            }
            else
            {

                qm.DeleteByWhere(Qmt_QuaStandMaster._.Stand_id == standId);
                qdm.DeleteByWhere(Qmt_QuaStandDetail._.Stand_id == standId);
                //mQmtCheckStandMaster.LastModifyTime = DateTime.Now;
                //mQmtCheckStandMaster.WorkerBarcode = this.UserID;

                //bQmtCheckStandMasterManager.DeleteWithLogic(mQmtCheckStandMaster);
            }

            this.AppendWebLog("胶料质检标准--删除", "StandId=" + mQmtCheckStandMaster.StandId.ToString());

            string materCode = mQmtCheckStandMaster.MaterCode;

            //HiddenStandId.SetValue("");

            LoadGridPanelMaster(materCode);

            X.Msg.Alert("提示", "删除成功").Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要删除的检验标准").Show();
        }
    }
    /// <summary>
    /// 删除数据恢复
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ButtonDeleteRestore_Click(object sender, EventArgs e)
    {
        string standId = HiddenStandId.Value.ToString();
        if (RowSelectionModelMaster.SelectedRows.Count == 0 || standId == "")
        {
            X.Msg.Alert("提示", "请选择要删除的检验标准").Show();
            return;
        }

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
        if (mQmtCheckStandMaster.DeleteFlag == "0")
        {
            X.Msg.Alert("提示", "选择的标准未删除过，无法恢复！").Show();
            return;
        }
        mQmtCheckStandMaster.DeleteFlag = "0";
        bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);
        X.Msg.Alert("提示", "恢复成功").Show();
        return;
    }
    /// <summary>
    /// 启用/停用/作废检验标准
    /// </summary>
    /// <param name="standVisionStat"></param>
    private void SetStandVisionStat(EnumStandVisionStat standVisionStat)
    {
        string standVisionStatExp = "";
        switch (standVisionStat)
        {
            case EnumStandVisionStat.Enabled:
                standVisionStatExp = "启用";
                break;
            case EnumStandVisionStat.Disabled:
                standVisionStatExp = "停用";
                break;
            case EnumStandVisionStat.Invalid:
                standVisionStatExp = "作废";
                break;
            default:
                break;
        }

        if (RowSelectionModelMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要" + standVisionStatExp + "的检验标准").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
        if (mQmtCheckStandMaster != null && mQmtCheckStandMaster.StandId > 0 && mQmtCheckStandMaster.DeleteFlag == "0")
        {

            if (standVisionStatExp == "启用" && String.IsNullOrEmpty(mQmtCheckStandMaster.LastAuditUser))
            { X.Msg.Alert("提示", "该检验标准需先审核才能启用！").Show();
            return;
            }





            if (Convert.ToInt32(mQmtCheckStandMaster.StandVisionStat) == Convert.ToInt32(standVisionStat))
            {
                X.Msg.Alert("提示", "该检验标准已" + standVisionStatExp + "，无须再" + standVisionStatExp + "").Show();
            }
            else
            {
                mQmtCheckStandMaster.StandVisionStat = Convert.ToInt32(standVisionStat).ToString();
                switch (standVisionStat)
                {
                    case EnumStandVisionStat.Enabled:
                        //bQmtCheckStandMasterManager.Upgrade(mQmtCheckStandMaster);
                        //Upgrade(mQmtCheckStandMaster);
                        string sqlEnabled = "update dbo.Qmt_QuaStandMaster set Stand_VisionStat='" + mQmtCheckStandMaster.StandVisionStat + "' where Stand_id='" + standId + "'";
                        bQmtCheckStandMasterManager.GetBySql(sqlEnabled).ToDataSet();
                        break;
                    case EnumStandVisionStat.Disabled:
                        //bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);
                        string sqlDisabled = "update dbo.Qmt_QuaStandMaster set Stand_VisionStat='" + mQmtCheckStandMaster.StandVisionStat + "' where Stand_id='" + standId + "'";
                        bQmtCheckStandMasterManager.GetBySql(sqlDisabled).ToDataSet();
                        break;
                    case EnumStandVisionStat.Invalid:
                        //bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);
                        string sqlInvalid = "update dbo.Qmt_QuaStandMaster set Stand_VisionStat='" + mQmtCheckStandMaster.StandVisionStat + "' where Stand_id='" + standId + "'";
                        bQmtCheckStandMasterManager.GetBySql(sqlInvalid).ToDataSet();
                        break;
                    default:
                        break;
                }

                WindowMaster.Close();

                HiddenStandId.SetValue("");

                LoadGridPanelMaster(mQmtCheckStandMaster.MaterCode);
                this.AppendWebLog("修改胶料质检标准状态", standVisionStatExp + "   " + mQmtCheckStandMaster.StandId);
                X.Msg.Alert("提示", "" + standVisionStatExp + "成功").Show();
            }
        }
        else
        {
            X.Msg.Alert("提示", "未找到要" + standVisionStatExp + "的检验标准").Show();
        }

    }


    public void Upgrade(QmtCheckStandMaster entity)
    {
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
        {
            bQmtCheckStandMasterManager.Update(new PropertyItem[] { QmtCheckStandMaster._.StandVisionStat }
                , new object[] { 0 }
                , QmtCheckStandMaster._.DeleteFlag == "0"
                & QmtCheckStandMaster._.StandVisionStat == "1"
                & QmtCheckStandMaster._.StandCode == entity.StandCode
                & QmtCheckStandMaster._.MaterCode == entity.MaterCode
                 & QmtCheckStandMaster._.PmtType == entity.PmtType);
            entity.RegDateTime = DateTime.Now;
            if (entity.StandId == 0)
            {
                bQmtCheckStandMasterManager.Insert(entity);
            }
            else
            {
                bQmtCheckStandMasterManager.Update(entity);
            }
            scope.Complete();
            scope.Dispose();
        }

    }
    /// <summary>
    /// 启用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterEnabled_Click(object sender, DirectEventArgs e)
    {
        SetStandVisionStat(EnumStandVisionStat.Enabled);
    }

    /// <summary>
    /// 停用检验标准
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterDisabled_Click(object sender, DirectEventArgs e)
    {
        SetStandVisionStat(EnumStandVisionStat.Disabled);
    }

    /// <summary>
    /// 作废检验标准
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterInvalid_Click(object sender, DirectEventArgs e)
    {
        SetStandVisionStat(EnumStandVisionStat.Invalid);
    }

    /// <summary>
    /// 新增选择物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldMasterMaterName_TriggerClick(object sender, DirectEventArgs e)
    {
        X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();");
    }

    /// <summary>
    /// 验证检验标准 添加/修改
    /// 修改标识：qusf 20131022
    /// 修改内容：1.增加对生效日期的检验
    /// </summary>
    /// <returns></returns>
    private bool ValidateStandMaster()
    {
        string standCode = ComboBoxMasterStandCode.Value.ToString();
        if (standCode == "")
        {
            X.Msg.Alert("提示", "请选择检验标准分类").Show();
            return false;
        }
        string materCode = HiddenMasterMaterCode.Value.ToString();
        if (materCode == "")
        {
            X.Msg.Alert("提示", "请选择胶料").Show();
            return false;
        }
        string standVisionStat = ComboBoxMasterStandVisionStat.Value.ToString();
        if (standVisionStat == "")
        {
            X.Msg.Alert("提示", "请选择版本状态").Show();
            return false;
        }
        string regDate = DateFieldMasterRegDateTime.RawText;
        if (regDate == null || regDate == "")
        {
            X.Msg.Alert("提示", "请选择生效日期").Show();
            return false;
        }

        string regTime = TimeFieldMasterRegDateTime.RawText;
        if (regTime == null || regTime == "")
        {
            X.Msg.Alert("提示", "请选择生效时间").Show();
            return false;
        }

        if (Convert.ToDateTime(regDate + " " + regTime) < DateTime.Now.AddDays(-1))
        {
            X.Msg.Alert("提示", "生效日期和时间不能小于当前时间").Show();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 升级检验标准版本并添加新版本
    /// </summary>
    [DirectMethod]
    public void UpgradeStandMaster(string standId)
    {
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = GetStandMaster(standId);
        bQmtCheckStandMasterManager.Upgrade(mQmtCheckStandMaster);
        LoadGridPanelMaster(mQmtCheckStandMaster.MaterCode);

        WindowMaster.Close();

        if (standId == "")
        {
            X.Msg.Alert("提示", "添加并升级版本成功").Show();
        }
        else
        {
            X.Msg.Alert("提示", "修改成功").Show();
        }

    }
    public Qmt_QuaStandMaster ChangeStandMaster(QmtCheckStandMaster qc)
    {
        Qmt_QuaStandMaster qq = new Qmt_QuaStandMaster();
        qq.Stand_id = qc.StandId;
        qq.Stand_code = (int)qc.StandCode;
        qq.Mater_code = qc.MaterCode;
        qq.Define_date = qc.DefineDate;
        qq.Worker_barcode = qc.WorkerBarcode;
        qq.Stand_vision = qc.StandVision;
        //qq.Stand_Date = DateFieldMasterRegDateTime.RawText;
        //qq.Recipe_Version = int.Parse(TextFieldMasterLLStandVision.Text);
        //qq.Start_shift = int.Parse(RubType.SelectedItem.Value);
        qq.Stand_VisionStat = qc.StandVisionStat;
        //if (qq.Stand_VisionStat != "1") qq.Stand_VisionStat = "0";
        return qq;
    }

    /// <summary>
    /// 添加
    /// 修改标识：qusf 20131008
    /// 修改内容：因增加审核功能，不再处理版本号信息
    /// </summary>
    [DirectMethod]
    public void AddStandMaster()
    {
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        IQmt_QuaStandMasterManager QM = new Qmt_QuaStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = GetStandMaster("");
        Qmt_QuaStandMaster qq = ChangeStandMaster(mQmtCheckStandMaster);
        qq.Recipe_Version = int.Parse(TextFieldMasterLLStandVision.Text);
        qq.Stand_vision = int.Parse(txtbanben.Text);
        qq.Start_shift = int.Parse(RubType.SelectedItem.Value);
        qq.Stand_Date = DateFieldMasterRegDateTime.RawText;
        QM.Insert(qq);
        //bQmtCheckStandMasterManager.Insert(mQmtCheckStandMaster);
        LoadGridPanelMaster(mQmtCheckStandMaster.MaterCode);

        WindowMaster.Close();

        if (mQmtCheckStandMaster.StandVisionStat == Convert.ToInt32(EnumStandVisionStat.New).ToString())
        {
            X.Msg.Alert("提示", "添加成功").Show();
        }
        else if (mQmtCheckStandMaster.StandVisionStat == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            X.Msg.Alert("提示", "提交成功").Show();
        }
        return;

       
    }

    /// <summary>
    /// 获取检验标准
    /// 修改标识：qusf 20131009
    /// 修改内容：1.增加修改时间、提交时间、提交人的处理
    ///           2.状态为修改时，版本状态StandVisionStat不赋值
    /// </summary>
    private QmtCheckStandMaster GetStandMaster(string standId)
    {
        if (standId == "")
        {
            string standCode = ComboBoxMasterStandCode.Value.ToString();
            string materCode = HiddenMasterMaterCode.Value.ToString();


            string quaCompute = "0";
            if (CheckboxMasterQuaCompute.Checked == true)
            {
                quaCompute = "1";
            }
            string choiceness = "0";
            if (CheckboxMasterChoiceness.Checked == true)
            {
                choiceness = "1";
            }

            string llStandVision = TextFieldMasterLLStandVision.Text.Trim();
            string regDateTime = DateFieldMasterRegDateTime.RawText + " " + TimeFieldMasterRegDateTime.RawText;
          
            String pmtType = RubType.Value.ToString();
            String pmtTypeName = RubType.SelectedItem.Text;
            QmtCheckStandMaster mQmtCheckStandMaster = new QmtCheckStandMaster();
            mQmtCheckStandMaster.StandId = 0;
            mQmtCheckStandMaster.StandCode = Convert.ToInt32(standCode);
            mQmtCheckStandMaster.MaterCode = materCode;
            mQmtCheckStandMaster.WorkerBarcode = this.UserID;
            mQmtCheckStandMaster.DefineDate = DateTime.Today.ToString("yyyy-MM-dd");
            mQmtCheckStandMaster.StandDate = DateTime.Today.ToString("yyyy-MM-dd");
            mQmtCheckStandMaster.QuaCompute = quaCompute;
            mQmtCheckStandMaster.Choiceness = choiceness;
            mQmtCheckStandMaster.RegDateTime = Convert.ToDateTime(regDateTime);
            mQmtCheckStandMaster.DeleteFlag = "0";

            mQmtCheckStandMaster.StandVision = -1;
            mQmtCheckStandMaster.LastModifyTime = DateTime.Now;
            string standVisionStat = ComboBoxMasterStandVisionStat.Value.ToString();
            mQmtCheckStandMaster.StandVisionStat = standVisionStat;

            mQmtCheckStandMaster.LLStandVision = llStandVision;
            mQmtCheckStandMaster.PmtType = pmtType;
            mQmtCheckStandMaster.PmtTypeName = pmtTypeName;
            if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.New).ToString())
            {
                //状态为未提交

            }
            else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
            {
                //状态为已提交
                mQmtCheckStandMaster.LastSubmitUser = this.UserID;
                mQmtCheckStandMaster.LastSubmitTime = DateTime.Now;
            }

            return mQmtCheckStandMaster;
        }
        else
        {

            string quaCompute = "0";
            if (CheckboxMasterQuaCompute.Checked == true)
            {
                quaCompute = "1";
            }
            string choiceness = "0";
            if (CheckboxMasterChoiceness.Checked == true)
            {
                choiceness = "1";
            }

            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();

            QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
            if (mQmtCheckStandMaster == null)
            {
                return null;
            }
            String pmtType = RubType.Value.ToString();
            String pmtTypeName = RubType.SelectedItem.Text;
            mQmtCheckStandMaster.WorkerBarcode = this.UserID;
            mQmtCheckStandMaster.StandDate = DateTime.Today.ToString("yyyy-MM-dd");
            mQmtCheckStandMaster.QuaCompute = quaCompute;
            mQmtCheckStandMaster.Choiceness = choiceness;

            mQmtCheckStandMaster.LastModifyTime = DateTime.Now;

            string llStandVision = TextFieldMasterLLStandVision.Text.Trim();
            mQmtCheckStandMaster.LLStandVision = llStandVision;

            string regDateTime = DateFieldMasterRegDateTime.RawText + " " + TimeFieldMasterRegDateTime.RawText;
            mQmtCheckStandMaster.RegDateTime = Convert.ToDateTime(regDateTime);

            string standVisionStat = ComboBoxMasterStandVisionStat.Value.ToString();
            //mQmtCheckStandMaster.StandVisionStat = standVisionStat;
            if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.New).ToString())
            {
                //状态为未提交

            }
            else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
            {
                //状态为已提交
                mQmtCheckStandMaster.LastSubmitUser = this.UserID;
                mQmtCheckStandMaster.LastSubmitTime = DateTime.Now;
            }
            mQmtCheckStandMaster.PmtType = pmtType;
            mQmtCheckStandMaster.PmtTypeName = pmtTypeName;
            return mQmtCheckStandMaster;
        }
    }

    /// <summary>
    /// 检验标准修改(DirectMethod)
    /// </summary>
    [DirectMethod]
    private void UpdateStandMaster()
    {
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();

        string standId = HiddenStandId.Value.ToString();

        if (ComboBoxMasterStandVisionStat.Value.ToString() == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            // 提交审核
            IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
            EntityArrayList<QmtCheckStandDetail> mQmtCheckDetailList = bQmtCheckStandDetailManager.GetListByWhere(QmtCheckStandDetail._.StandId == standId
                & QmtCheckStandDetail._.DeleteFlag == "0");
            if (mQmtCheckDetailList.Count == 0)
            {
                X.Msg.Alert("提示", "没有标准明细信息，不允许提交审核").Show();
                return;
            }
        }

        QmtCheckStandMaster mQmtCheckStandMaster = GetStandMaster(standId);

        if (mQmtCheckStandMaster == null)
        {
            X.Msg.Alert("提示", "未找到要修改的记录，请核实").Show();
            return;
        }
        if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
            && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
        {
            string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
            X.Msg.Alert("提示", "版本状态为" + standVisionStatDesp + "，不允许修改，请核实").Show();
            return;
        }

        mQmtCheckStandMaster.StandVisionStat = ComboBoxMasterStandVisionStat.Value.ToString();

        //bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);


        IQmt_QuaStandMasterManager QM = new Qmt_QuaStandMasterManager();
        Qmt_QuaStandMaster qq = QM.GetById(standId);
  
        qq.Define_date = mQmtCheckStandMaster.DefineDate;
        qq.Worker_barcode = mQmtCheckStandMaster.WorkerBarcode;
        qq.Stand_Date = DateFieldMasterRegDateTime.RawText;
        qq.Recipe_Version = int.Parse(TextFieldMasterLLStandVision.Text);
        qq.Stand_vision = int.Parse(txtbanben.Text);
        qq.Start_shift = int.Parse(RubType.SelectedItem.Value);
        qq.Stand_VisionStat = mQmtCheckStandMaster.StandVisionStat;
        QM.Update(qq);



        LoadGridPanelMaster(mQmtCheckStandMaster.MaterCode);

        WindowMaster.Close();

        if (mQmtCheckStandMaster.StandVisionStat == Convert.ToInt32(EnumStandVisionStat.New).ToString())
        {
            X.Msg.Alert("提示", "修改成功").Show();
        }
        else if (mQmtCheckStandMaster.StandVisionStat == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            X.Msg.Alert("提示", "提交成功").Show();
        }
    }

    /// <summary>
    /// 根据版本状态得到中文状态
    /// </summary>
    /// <param name="standVisionStat"></param>
    /// <returns></returns>
    private string GetStandVisionStatDesp(string standVisionStat)
    {
        string standVisionStatDesp = "";

        if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            standVisionStatDesp = "已提交";
        }
        else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Enabled).ToString())
        {
            standVisionStatDesp = "已启用";
        }
        else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Disabled).ToString())
        {
            standVisionStatDesp = "已停用";
        }
        else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Invalid).ToString())
        {
            standVisionStatDesp = "已作废";
        }
        else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
        {
            standVisionStatDesp = "已退回";
        }
        else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.New).ToString())
        {
            standVisionStatDesp = "未提交";
        }
        else if (standVisionStat == Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            standVisionStatDesp = "已提交";
        }

        return standVisionStatDesp;
    }

    /// <summary>
    /// 保存不提交--检验标准添加/修改
    /// 修改标识：qusf 20131008
    /// 修改内容：保存不再处理其他版本的记录，只将状态改为-1，版本为0
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterAccept_Click(object sender, EventArgs e)
    {
        string commandName = HiddenButtonCommandName.Value.ToString(); //Add Update

        ComboBoxMasterStandVisionStat.SetValue(Convert.ToInt32(EnumStandVisionStat.New).ToString());
        if (commandName == EnumCommandName.Add.ToString())
        {
            //新增检验标准

            if (ValidateStandMaster() == false)
            {
                //WindowMaster.Close();
                return;
            }

            AddStandMaster();

            return;

            string standCode = ComboBoxMasterStandCode.Value.ToString();
            string standName = ComboBoxMasterStandCode.SelectedItem.Text;
            string materCode = HiddenMasterMaterCode.Value.ToString();
            string materName = TriggerFieldMasterMaterName.Value.ToString();
            string standVisionStat = ComboBoxMasterStandVisionStat.Value.ToString();
            String qmtType = RubType.Value.ToString();
            String qmtTypeName = RubType.SelectedItem.Text;
            //是否存在相同分类及物料的检验标准
            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList = bQmtCheckStandMasterManager.GetListByWhereAndOrder(QmtCheckStandMaster._.StandCode == standCode
                    & QmtCheckStandMaster._.MaterCode == materCode
                    & QmtCheckStandMaster._.DeleteFlag == "0"
                    & QmtCheckStandMaster ._.PmtType== qmtType
                    , QmtCheckStandMaster._.StandVision.Desc);

            if (mQmtCheckStandMasterList.Count == 0)
            {
                AddStandMaster();
            }
            else if (Convert.ToInt32(standVisionStat) != Convert.ToInt32(EnumStandVisionStat.Enabled)
                || mQmtCheckStandMasterList.Filter(QmtCheckStandMaster._.StandVisionStat == EnumStandVisionStat.Enabled).Length == 0)
            {
                MessageBoxButtonConfig m = new MessageBoxButtonConfig();
                X.Msg.Confirm("提示", "胶料【" + materName + "】类型【" + qmtTypeName + "】 在分类【" + standName + "】的检验标准已存在，确定继续吗？<br>如果继续将会使当前物料版本升级", new MessageBoxButtonsConfig
                {
                    Yes = new MessageBoxButtonConfig
                    {
                        Handler = "App.direct.AddStandMaster()",
                        Text = "继续"
                    },
                    No = new MessageBoxButtonConfig
                    {
                        Text = "取消"
                    }
                }).Show();
            }
            else
            {
                X.Msg.Confirm("提示", "胶料【" + materName + "】类型【" + qmtTypeName + "】在分类【" + standName + "】的检验标准存在已启用的版本，确定继续吗？<br>如果继续将会使当前物料版本升级并停用已启用的版本", new MessageBoxButtonsConfig
                {
                    Yes = new MessageBoxButtonConfig
                    {
                        Handler = string.Format("App.direct.UpgradeStandMaster({0})", "\"\""),
                        Text = "继续"
                    },
                    No = new MessageBoxButtonConfig
                    {
                        Text = "取消"
                    }
                }).Show();
            }
        }
        else if (commandName == EnumCommandName.Update.ToString())
        {
            //修改检验标准

            if (ValidateStandMaster() == false)
            {
                //WindowMaster.Close();
                return;
            }
            UpdateStandMaster();

            return;

            string standId = HiddenStandId.Value.ToString();
            string standCode = ComboBoxMasterStandCode.Value.ToString();
            string standName = ComboBoxMasterStandCode.SelectedItem.Text;
            string materCode = HiddenMasterMaterCode.Value.ToString();
            string materName = TriggerFieldMasterMaterName.Value.ToString();
            string standVisionStat = ComboBoxMasterStandVisionStat.Value.ToString();
            String qmtType = RubType.Value.ToString();
            String qmtTypeName = RubType.SelectedItem.Text;
            //是否存在相同分类及物料的检验标准
            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList = bQmtCheckStandMasterManager.GetListByWhereAndOrder(QmtCheckStandMaster._.StandCode == standCode
                    & QmtCheckStandMaster._.MaterCode == materCode
                    & QmtCheckStandMaster._.DeleteFlag == "0"
                    & QmtCheckStandMaster._.StandId != standId
                      & QmtCheckStandMaster._.PmtType == qmtType
                    , QmtCheckStandMaster._.StandVision.Desc);

            if (mQmtCheckStandMasterList.Count == 0)
            {
                UpdateStandMaster();
            }
            else if (Convert.ToInt32(standVisionStat) != Convert.ToInt32(EnumStandVisionStat.Enabled)
                || mQmtCheckStandMasterList.Filter(QmtCheckStandMaster._.StandVisionStat == EnumStandVisionStat.Enabled).Length == 0)
            {
                UpdateStandMaster();
            }
            else
            {
                X.Msg.Confirm("提示", "胶料【" + materName + "】类型【" + qmtTypeName + "】在分类【" + standName + "】的检验标准存在已启用的版本，确定继续吗？\r\n如果继续将会停用已启用的版本", new MessageBoxButtonsConfig
                {
                    Yes = new MessageBoxButtonConfig
                    {
                        Handler = string.Format("App.direct.UpgradeStandMaster({0})", "\"" + standId + "\""),
                        Text = "继续"
                    },
                    No = new MessageBoxButtonConfig
                    {
                        Text = "取消"
                    }
                }).Show();
            }
        }
    }

    /// <summary>
    /// 保存并提交--检验标准添加/修改
    /// 修改标识：qusf 20131213
    /// 修改内容：1.增加对生效日期和时间的校验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterSubmit_Click(object sender, EventArgs e)
    {
        string commandName = HiddenButtonCommandName.Value.ToString(); //Add Update

        ComboBoxMasterStandVisionStat.SetValue(Convert.ToInt32(EnumStandVisionStat.Submitted).ToString());
        if (commandName == EnumCommandName.Add.ToString())
        {
            //新增检验标准

            if (ValidateStandMaster() == false)
            {
                //WindowMaster.Close();
                return;
            }

            X.Msg.Alert("提示", "没有标准明细信息，不允许提交审核").Show();

        }
        else if (commandName == EnumCommandName.Update.ToString())
        {
            //修改检验标准

            if (ValidateStandMaster() == false)
            {
                //WindowMaster.Close();
                return;
            }

            UpdateStandMaster();

        }

    }
    /// <summary>
    /// 关闭窗口--检验标准添加/修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterCancel_Click(object sender, EventArgs e)
    {
        WindowMaster.Close();
    }

    #endregion 检验标准添加/修改

    #endregion 检验标准操作

    #region 检验标准明细操作

    /// <summary>
    /// 加载检验标准等级列表
    /// </summary>
    /// <param name="standId"></param>
    /// <param name="itemCd"></param>
    private void LoadGridPanelGrade(string standId, string itemCd)
    {
        StoreGrade.RemoveAll();
        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true)
        {
            IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
            IQmtCheckStandGradeParams p = new QmtCheckStandGradeParams();
            p.StandId = standId;
            p.ItemCd = itemCd;
            p.DeleteFlag = "0";
            DataSet ds = bQmtCheckStandGradeManager.GetDataByParas(p);
            StoreGrade.DataSource = ds.Tables[0];
            StoreGrade.DataBind();
        }
    }

    /// <summary>
    /// 选择检验标准明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelDetail_SelectionChange(object sender, DirectEventArgs e)
    {
      
        string standId = e.ExtraParams["StandId"];
        string itemCd = e.ExtraParams["ItemCd"];
        string weightId = e.ExtraParams["WeightId"];
        HiddenStandId.SetValue(standId);
        HiddenItemCd.SetValue(itemCd);
        HiddenDetailWeightId.SetValue(weightId);
        //X.Js.Alert( itemCd);

        return;//方法意义不明
        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true
            && StringUtils.IsNotEmpty(weightId) == true)
        {
            IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
            QmtCheckStandDetail mQmtCheckStandDetail = bQmtCheckStandDetailManager.GetById(new object[] { standId, itemCd, weightId });
            HiddenStandId.SetValue(mQmtCheckStandDetail.StandId);
            HiddenItemCd.SetValue(mQmtCheckStandDetail.ItemCd);
            HiddenDetailWeightId.SetValue(mQmtCheckStandDetail.WeightId);
        }
        else
        {
            HiddenItemCd.SetValue("");
            HiddenDetailWeightId.SetValue("");
        }
        return;//方法意义不明
        //LoadGridPanelGrade(standId, itemCd);
        //LoadGridPanelEquip(standId, itemCd);
    }

    #region 检验标准明细添加/修改

    /// <summary>
    /// 初始化--检验标准明细添加/修改
    /// 修改标识：qusf 20131022
    /// 修改内容：1.增加对处理意见、划胶标识、等级、子标识、检验频率的默认处理
    /// </summary>
    private void ClearPanelDetail()
    {
        ComboBoxDetailItemCd.SetValue("");
        NumberFieldDetailPermMin.SetValue("");
        CheckBoxDetailIfMin.SetValue(true);
        NumberFieldDetailPermMax.SetValue("");
        CheckBoxDetailIfMax.SetValue(true);
        ComboBoxJudge.SetValue("1");
        ComboBoxDetailQuaFrequency.SetValue("");
    }

    /// <summary>
    /// 打开窗口--检验标准明细添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDetailAdd_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
        if (mQmtCheckStandMaster == null)
        {
            X.Msg.Alert("提示", "未找到选中的检验标准，请核实").Show();
            return;
        }
        if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
            && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
        {
            string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
            X.Msg.Alert("提示", "检验标准版本状态为" + standVisionStatDesp + "，不允许添加标准明细").Show();
            return;
        }

        ClearPanelDetail();

        //string standId = RowSelectionModelMaster.SelectedRow.RecordID;
        //HiddenStandId.SetValue(standId);

        HiddenButtonCommandName.SetValue(EnumCommandName.Add); // Add

        ComboBoxDetailItemCd.Disabled = false;
        ComboBoxDetailItemCd.Editable = false;

        WindowDetail.Title = "添加检验标准明细信息";
        WindowDetail.Icon = Icon.TableAdd;
        WindowDetail.Show();
    }

    /// <summary>
    /// 验证检验标准明细
    /// </summary>
    /// <returns></returns>
    private bool ValidateStandDetail()
    {
        string standId = HiddenStandId.Value.ToString();
        if (standId == "")
        {
            X.Msg.Alert("提示", "请选择一条标准信息").Show();
            return false;
        }
        string itemCd = ComboBoxDetailItemCd.Value.ToString();
        if (itemCd == "")
        {
            X.Msg.Alert("提示", "请选择检验项目").Show();
            return false;
        }
        string itemName = ComboBoxDetailItemCd.SelectedItem.Text.Trim();
        string permMin = NumberFieldDetailPermMin.Number.ToString();
        if (permMin == "")
        {
            X.Msg.Alert("提示", "请填写下限值").Show();
            return false;
        }
        decimal dPermMin = 0;
        if (decimal.TryParse(permMin, out dPermMin) == false)
        {
            X.Msg.Alert("提示", "下限值必须是数字").Show();
            return false;
        }
        string permMax = NumberFieldDetailPermMax.Number.ToString();
        if (permMax == "")
        {
            X.Msg.Alert("提示", "请填写上限值").Show();
            return false;
        }
        decimal dPermMax = 0;
        if (decimal.TryParse(permMax, out dPermMax) == false)
        {
            X.Msg.Alert("提示", "上限值必须是数字").Show();
            return false;
        }
        if (Convert.ToDecimal(permMax) <= Convert.ToDecimal(permMin))
        {
            X.Msg.Alert("提示", "上限值必须大于下限值").Show();
            return false;
        }
       
        string quaFrequency = ComboBoxDetailQuaFrequency.Value.ToString();
       

        string weightId = "";
        if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Add.ToString())
        {
            weightId = "0";
        }
        else if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Update.ToString())
        {
            weightId = HiddenDetailWeightId.Value.ToString();
        }

        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList = bQmtCheckStandDetailManager.GetListByWhere(QmtCheckStandDetail._.DeleteFlag == "0"
            & QmtCheckStandDetail._.StandId == standId
            & QmtCheckStandDetail._.ItemCd == itemCd
            & QmtCheckStandDetail._.WeightId != weightId);
        if (mQmtCheckStandDetailList.Count > 0)
        {
            string ifMin = CheckBoxDetailIfMin.Checked == true ? "1" : "0";
            string ifMax = CheckBoxDetailIfMax.Checked == true ? "1" : "0";

            DigitalRange aDigitalRange = new DigitalRange(decimal.Parse(permMin), int.Parse(ifMin), decimal.Parse(permMax), int.Parse(ifMax));

            List<DigitalRange> bDigitalRangeList = new List<DigitalRange>();
            foreach (QmtCheckStandDetail entity in mQmtCheckStandDetailList)
            {
                bDigitalRangeList.Add(new DigitalRange(entity.PermMin.Value, entity.IfMin.Value, entity.PermMax.Value, entity.IfMax.Value));
            }

            if (ValidateDigitalRange(aDigitalRange, bDigitalRangeList) == false)
            {
                X.Msg.Alert("提示", "检验项目【" + itemName + "】上下限区间与其他记录有重叠").Show();
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 判断是否有区间重叠
    /// </summary>
    /// <param name="aDigitalRange"></param>
    /// <param name="bDigitalRangeList"></param>
    /// <returns></returns>
    private bool ValidateDigitalRange(DigitalRange aDigitalRange, List<DigitalRange> bDigitalRangeList)
    {
        for (int i = 0; i < bDigitalRangeList.Count; i++)
        {
            if (!(aDigitalRange.IfMin == 0 && aDigitalRange.PermMin >= bDigitalRangeList[i].PermMax
                || aDigitalRange.IfMin == 1 && bDigitalRangeList[i].IfMax == 1 && aDigitalRange.PermMin > bDigitalRangeList[i].PermMax
                || aDigitalRange.IfMin == 1 && bDigitalRangeList[i].IfMax == 0 && aDigitalRange.PermMin >= bDigitalRangeList[i].PermMax
                || aDigitalRange.IfMax == 0 && aDigitalRange.PermMax <= bDigitalRangeList[i].PermMin
                || aDigitalRange.IfMax == 1 && bDigitalRangeList[i].IfMin == 1 && aDigitalRange.PermMax < bDigitalRangeList[i].PermMin
                || aDigitalRange.IfMax == 1 && bDigitalRangeList[i].IfMin == 0 && aDigitalRange.PermMax <= bDigitalRangeList[i].PermMin))
            {
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// 添加标准明细
    /// </summary>
    private void AddStandDetail()
    {
        string standId = HiddenStandId.Value.ToString();

        string itemCd = ComboBoxDetailItemCd.Value.ToString();
        string permMin = NumberFieldDetailPermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxDetailIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldDetailPermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxDetailIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = ComboBoxJudge.Value.ToString();

        string dealCode = ComboBoxDetailQuaFrequency.Value.ToString();
      
        string drawMark = "";
        string grade ="0";
        string cardMark2 = "";
        string quaFrequency = dealCode;
     

        QmtCheckStandDetail mQmtCheckStandDetail = new QmtCheckStandDetail();
        mQmtCheckStandDetail.StandId = Convert.ToInt32(standId);
        mQmtCheckStandDetail.ItemCd = itemCd;
        mQmtCheckStandDetail.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandDetail.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandDetail.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandDetail.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandDetail.Grade = Convert.ToInt32(grade);
        if (dealCode == "") mQmtCheckStandDetail.DealCode = null;
        else
        mQmtCheckStandDetail.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandDetail.DrawMark = drawMark;
        mQmtCheckStandDetail.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandDetail.CardMark2 = cardMark2;
        mQmtCheckStandDetail.QuaFrequency = quaFrequency;
        mQmtCheckStandDetail.DeleteFlag = "0";


        Qmt_QuaStandDetail qq = changeStandDetail(mQmtCheckStandDetail);
        IQmt_QuaStandDetailManager qm = new Qmt_QuaStandDetailManager();
        qm.Insert(qq);
        //IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        //bQmtCheckStandDetailManager.Insert(mQmtCheckStandDetail);

        LoadGridPanelDetail(standId);

        WindowDetail.Close();

        X.Msg.Alert("提示", "添加成功").Show();
    }


    public Qmt_QuaStandDetail changeStandDetail(QmtCheckStandDetail qc)
    {
        Qmt_QuaStandDetail qq = new Qmt_QuaStandDetail();
        qq.Stand_id = qc.StandId;
        qq.Item_cd = qc.ItemCd;
        String sql = "select MAX(Weight_id) from Qmt_QuaStandDetail where Stand_id = '" + qq.Stand_id + "' and Item_cd ='" + qq.Item_cd + "'";
        IQmt_QuaStandDetailManager qm = new Qmt_QuaStandDetailManager();
        DataSet ds = qm.GetBySql(sql).ToDataSet();
        if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) ) { qq.Weight_id = 1; }
        else { qq.Weight_id = int.Parse(ds.Tables[0].Rows[0][0].ToString())+1; }




        //qq.Weight_id = (short)qc.WeightId;
        qq.Perm_min = qc.PermMin;
        qq.If_min = qc.IfMin;
        qq.Perm_max = qc.PermMax;
        qq.If_max = qc.IfMax;
        if (qc.DealCode == null) qq.Deal_code = null;
        else
            qq.Deal_code = (byte)qc.DealCode;
        qq.Judge_result = qc.JudgeResult;

        return qq;
    
    }
    /// <summary>
    /// 修改标准明细
    /// </summary>
    private void UpdateStandDetail()
    {
        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenDetailWeightId.Value.ToString();
        string permMin = NumberFieldDetailPermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxDetailIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldDetailPermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxDetailIfMax.Checked == true)
        {
            ifMax = "1";
        }


        string dealCode = ComboBoxDetailQuaFrequency.Value.ToString();
      
        string quaFrequency = ComboBoxDetailQuaFrequency.Value.ToString();
        IQmt_QuaStandDetailManager bm = new Qmt_QuaStandDetailManager();
        Qmt_QuaStandDetail mQmtCheckStandDetail = bm.GetById(new object[] { standId, itemCd, weightId });




     
        mQmtCheckStandDetail.Perm_min = Convert.ToDecimal(permMin);
        mQmtCheckStandDetail.If_min = Convert.ToInt32(ifMin);
        mQmtCheckStandDetail.Perm_max = Convert.ToDecimal(permMax);
        mQmtCheckStandDetail.If_max = Convert.ToInt32(ifMax);
        mQmtCheckStandDetail.Judge_result = Convert.ToInt32( ComboBoxJudge.Value.ToString());

        if (dealCode == "") mQmtCheckStandDetail.Deal_code = null;
        else
        mQmtCheckStandDetail.Deal_code = (byte)int.Parse(dealCode);
    

        bm.Update(mQmtCheckStandDetail);

        LoadGridPanelDetail(standId);

        WindowDetail.Close();

        X.Msg.Alert("提示", "修改成功").Show();
    }

    /// <summary>
    /// 保存--检验标准明细添加/修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDetailAccept_Click(object sender, EventArgs e)
    {
        string commandName = HiddenButtonCommandName.Value.ToString();
        if (commandName == EnumCommandName.Add.ToString())
        {
            if (ValidateStandDetail() == false)
            {
                return;
            }
            string standId = HiddenStandId.Value.ToString();

            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
            if (mQmtCheckStandMaster == null)
            {
                X.Msg.Alert("提示", "未找到选中的检验标准，请核实").Show();
                return;
            }
            if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
                && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
            {
                string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
                X.Msg.Alert("提示", "检验标准版本状态为" + standVisionStatDesp + "，不允许添加标准明细").Show();
                return;
            }

            AddStandDetail();
        }
        else if (commandName == EnumCommandName.Update.ToString())
        {
            string standId = HiddenStandId.Value.ToString();

            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
            if (mQmtCheckStandMaster == null)
            {
                X.Msg.Alert("提示", "未找到选中的检验标准，请核实").Show();
                return;
            }
            if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
                && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
            {
                string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
                X.Msg.Alert("提示", "检验标准版本状态为" + standVisionStatDesp + "，不允许修改标准明细").Show();
                return;
            }

            if (ValidateStandDetail() == false)
            {
                return;
            }
            UpdateStandDetail();
        }
    }

    /// <summary>
    /// 关闭窗口--检验标准明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDetailCancel_Click(object sender, EventArgs e)
    {
        WindowDetail.Close();
    }

    /// <summary>
    /// 打开窗口--检验标准明细修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDetailEdit_Click(object sender, EventArgs e)
    {
        string a = HiddenItemCd.Value.ToString();
        if (RowSelectionModelDetail.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准明细").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
        if (mQmtCheckStandMaster == null)
        {
            X.Msg.Alert("提示", "未找到选中的检验标准，请核实").Show();
            return;
        }
        if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
            && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
        {
            string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
            X.Msg.Alert("提示", "检验标准版本状态为" + standVisionStatDesp + "，不允许修改标准明细").Show();
            return;
        }

        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenDetailWeightId.Value.ToString();
        IQmt_QuaStandDetailManager bQmtCheckStandDetailManager = new Qmt_QuaStandDetailManager();
        Qmt_QuaStandDetail mQmtCheckStandDetail = bQmtCheckStandDetailManager.GetById(new object[] { standId, itemCd, weightId });
        if (mQmtCheckStandDetail != null && mQmtCheckStandDetail.Stand_id > 0)
        {
          
            ComboBoxDetailItemCd.SetValue(mQmtCheckStandDetail.Item_cd);
            NumberFieldDetailPermMin.SetValue(mQmtCheckStandDetail.Perm_min);

            //X.Msg.Alert(a, HiddenItemCd.Value.ToString()).Show();
            //return;
            CheckBoxDetailIfMin.SetValue(mQmtCheckStandDetail.If_min);
            NumberFieldDetailPermMax.SetValue(mQmtCheckStandDetail.Perm_max);
            CheckBoxDetailIfMax.SetValue(mQmtCheckStandDetail.If_max);
          
            ComboBoxDetailQuaFrequency.SetValue(mQmtCheckStandDetail.Deal_code.ToString());

            ComboBoxJudge.SetValue(mQmtCheckStandDetail.Judge_result.ToString());
            HiddenButtonCommandName.SetValue(EnumCommandName.Update); // Update

            ComboBoxDetailItemCd.Disabled = true;

            WindowDetail.Title = "修改检验标准明细信息";
            //WindowDetail.Title = a;
            WindowDetail.Icon = Icon.TableEdit;
            WindowDetail.Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要修改的检验标准明细").Show();
        }
    }

    #endregion 检验标准明细添加/修改

    #region 检验标准明细删除
    /// <summary>
    /// 检验标准明细删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDetailDelete_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelDetail.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要删除的检验标准明细").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);
        if (mQmtCheckStandMaster == null)
        {
            X.Msg.Alert("提示", "未找到选中的检验标准，请核实").Show();
            return;
        }
        if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.New).ToString()
            && mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Sendback).ToString())
        {
            string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
            X.Msg.Alert("提示", "检验标准版本状态为" + standVisionStatDesp + "，不允许删除标准明细").Show();
            return;
        }

        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenDetailWeightId.Value.ToString();
        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        QmtCheckStandDetail mQmtCheckStandDetail = bQmtCheckStandDetailManager.GetById(new object[] { standId, itemCd, weightId });
        if (mQmtCheckStandDetail != null && mQmtCheckStandDetail.StandId > 0)
        {
            //X.Msg.Alert(standId, itemCd +"   "+weightId).Show(); return;
            //bQmtCheckStandDetailManager.DeleteWithLogic(mQmtCheckStandDetail);
            IQmt_QuaStandDetailManager qm = new Qmt_QuaStandDetailManager();
            qm.DeleteByWhere(Qmt_QuaStandDetail._.Stand_id == standId && Qmt_QuaStandDetail._.Item_cd == itemCd && Qmt_QuaStandDetail._.Weight_id == weightId);
            HiddenDetailWeightId.SetValue("");
            HiddenItemCd.SetValue("");
            //HiddenStandId.SetValue("");
            LoadGridPanelDetail(standId);

            X.Msg.Alert("提示", "删除成功").Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要删除的检验标准明细").Show();
        }
    }

    #endregion 检验标准明细删除

    #endregion 检验标准明细操作

    #region 检验标准明细等级操作

    /// <summary>
    /// 选择检验标准明细等级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelGrade_SelectionChange(object sender, DirectEventArgs e)
    {
        string standId = e.ExtraParams["StandId"];
        string itemCd = e.ExtraParams["ItemCd"];
        string weightId = e.ExtraParams["WeightId"];

        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true
            && StringUtils.IsNotEmpty(weightId) == true)
        {
            IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
            QmtCheckStandGrade mQmtCheckStandGrade = bQmtCheckStandGradeManager.GetById(new object[] { standId, itemCd, weightId });
            HiddenStandId.SetValue(mQmtCheckStandGrade.StandId);
            HiddenItemCd.SetValue(mQmtCheckStandGrade.ItemCd);
            HiddenGradeWeightId.SetValue(mQmtCheckStandGrade.WeightId);
        }
        else
        {
            HiddenGradeWeightId.SetValue("");
        }

    }

    #region 检验标准明细等级添加/修改

    /// <summary>
    /// 初始化--检验标准明细等级添加/修改
    /// </summary>
    private void ClearPanelGrade()
    {
        NumberFieldGradePermMin.SetValue("");
        CheckBoxGradeIfMin.SetValue(true);
        NumberFieldGradePermMax.SetValue("");
        CheckBoxGradeIfMax.SetValue(true);
        CheckBoxGradeJudgeResult.SetValue(true);
        ComboBoxGradeDealCode.SetValue("");
        TextFieldGradeDrawMark.SetValue("");
        NumberFieldGradeGrade.SetValue("");
        TextFieldGradeCardMark2.SetValue("");
    }

    /// <summary>
    /// 打开窗口--检验标准明细等级添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonGradeAdd_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelDetail.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准明细").Show();
            return;
        }

        ClearPanelGrade();

        HiddenButtonCommandName.SetValue(EnumCommandName.Add); // Add

        WindowGrade.Title = "添加检验标准明细等级信息";
        WindowGrade.Icon = Icon.MonitorAdd;
        WindowGrade.Show();
    }

    /// <summary>
    /// 验证检验标准明细等级
    /// </summary>
    /// <returns></returns>
    private bool ValidateStandGrade()
    {
        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        if (standId == "" || itemCd == "")
        {
            X.Msg.Alert("提示", "请选择一条标准明细信息").Show();
            return false;
        }
        string permMin = NumberFieldGradePermMin.Number.ToString();
        if (permMin == "")
        {
            X.Msg.Alert("提示", "请填写下限值").Show();
            return false;
        }
        decimal dPermMin = 0;
        if (decimal.TryParse(permMin, out dPermMin) == false)
        {
            X.Msg.Alert("提示", "下限值必须是数字").Show();
            return false;
        }
        string permMax = NumberFieldGradePermMax.Number.ToString();
        if (permMax == "")
        {
            X.Msg.Alert("提示", "请填写上限值").Show();
            return false;
        }
        decimal dPermMax = 0;
        if (decimal.TryParse(permMax, out dPermMax) == false)
        {
            X.Msg.Alert("提示", "上限值必须是数字").Show();
            return false;
        }
        if (Convert.ToDecimal(permMax) <= Convert.ToDecimal(permMin))
        {
            X.Msg.Alert("提示", "上限值必须大于下限值").Show();
            return false;
        }
        string dealCode ="0";
        //if (dealCode == "")
        //{
        //    X.Msg.Alert("提示", "请选择检测意见").Show();
        //    return false;
        //}
        string grade = NumberFieldGradeGrade.Number.ToString();
        if (grade == "")
        {
            X.Msg.Alert("提示", "请填写等级").Show();
            return false;
        }

        string weightId = "";
        if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Add.ToString())
        {
            weightId = "0";
        }
        else if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Update.ToString())
        {
            weightId = HiddenGradeWeightId.Value.ToString();
        }

        IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
        EntityArrayList<QmtCheckStandGrade> mQmtCheckStandGradeList = bQmtCheckStandGradeManager.GetListByWhere(QmtCheckStandGrade._.DeleteFlag == "0"
            & QmtCheckStandGrade._.StandId == standId
            & QmtCheckStandGrade._.ItemCd == itemCd
            & QmtCheckStandGrade._.WeightId != weightId);
        if (mQmtCheckStandGradeList.Count > 0)
        {
            string ifMin = CheckBoxGradeIfMin.Checked == true ? "1" : "0";
            string ifMax = CheckBoxGradeIfMax.Checked == true ? "1" : "0";

            DigitalRange aDigitalRange = new DigitalRange(decimal.Parse(permMin), int.Parse(ifMin), decimal.Parse(permMax), int.Parse(ifMax));

            List<DigitalRange> bDigitalRangeList = new List<DigitalRange>();
            foreach (QmtCheckStandGrade entity in mQmtCheckStandGradeList)
            {
                bDigitalRangeList.Add(new DigitalRange(entity.PermMin.Value, entity.IfMin.Value, entity.PermMax.Value, entity.IfMax.Value));
            }

            if (ValidateDigitalRange(aDigitalRange, bDigitalRangeList) == false)
            {
                X.Msg.Alert("提示", "检验等级上下限区间与其他记录有重叠").Show();
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 添加标准明细等级
    /// </summary>
    private void AddStandGrade()
    {
        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string permMin = NumberFieldGradePermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxGradeIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldGradePermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxGradeIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = "0";
        if (CheckBoxGradeJudgeResult.Checked == true)
        {
            judgeResult = "1";
        }
        string dealCode = ComboBoxGradeDealCode.Value.ToString();
        string drawMark = TextFieldGradeDrawMark.Text.Trim();
        string grade = NumberFieldGradeGrade.Number.ToString();
        string cardMark2 = TextFieldGradeCardMark2.Text.Trim();

        QmtCheckStandGrade mQmtCheckStandGrade = new QmtCheckStandGrade();
        mQmtCheckStandGrade.StandId = Convert.ToInt32(standId);
        mQmtCheckStandGrade.ItemCd = itemCd;
        mQmtCheckStandGrade.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandGrade.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandGrade.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandGrade.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandGrade.Grade = Convert.ToInt32(grade);
        mQmtCheckStandGrade.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandGrade.DrawMark = drawMark;
        mQmtCheckStandGrade.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandGrade.CardMark2 = cardMark2;
        mQmtCheckStandGrade.DeleteFlag = "0";

        IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
        bQmtCheckStandGradeManager.Insert(mQmtCheckStandGrade);

        LoadGridPanelGrade(standId, itemCd);

        WindowGrade.Close();

        X.Msg.Alert("提示", "添加成功").Show();
    }

    /// <summary>
    /// 修改标准明细等级
    /// </summary>
    private void UpdateStandGrade()
    {
        IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenGradeWeightId.Value.ToString();
        string permMin = NumberFieldGradePermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxGradeIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldGradePermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxGradeIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = "0";
        if (CheckBoxGradeJudgeResult.Checked == true)
        {
            judgeResult = "1";
        }
        string dealCode = ComboBoxGradeDealCode.Value.ToString();
        string drawMark = TextFieldGradeDrawMark.Text.Trim();
        string grade = NumberFieldGradeGrade.Number.ToString();
        string cardMark2 = TextFieldGradeCardMark2.Text.Trim();

        QmtCheckStandGrade mQmtCheckStandGrade = bQmtCheckStandGradeManager.GetById(new object[] { standId, itemCd, weightId });

        mQmtCheckStandGrade.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandGrade.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandGrade.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandGrade.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandGrade.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandGrade.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandGrade.DrawMark = drawMark;
        mQmtCheckStandGrade.Grade = Convert.ToInt32(grade);
        mQmtCheckStandGrade.CardMark2 = cardMark2;

        bQmtCheckStandGradeManager.Update(mQmtCheckStandGrade);

        LoadGridPanelGrade(standId, itemCd);

        WindowGrade.Close();

        X.Msg.Alert("提示", "修改成功").Show();
    }

    /// <summary>
    /// 保存--检验标准明细等级添加/修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonGradeAccept_Click(object sender, EventArgs e)
    {
        string commandName = HiddenButtonCommandName.Value.ToString();
        if (commandName == EnumCommandName.Add.ToString())
        {
            if (ValidateStandGrade() == false)
            {
                return;
            }
            AddStandGrade();
        }
        else if (commandName == EnumCommandName.Update.ToString())
        {
            if (ValidateStandGrade() == false)
            {
                return;
            }
            UpdateStandGrade();
        }
    }

    /// <summary>
    /// 关闭窗口--检验标准明细等级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonGradeCancel_Click(object sender, EventArgs e)
    {
        WindowGrade.Close();
    }

    /// <summary>
    /// 打开窗口--检验标准明细等级修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonGradeEdit_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelGrade.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准明细等级").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenGradeWeightId.Value.ToString();
        IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
        QmtCheckStandGrade mQmtCheckStandGrade = bQmtCheckStandGradeManager.GetById(new object[] { standId, itemCd, weightId });
        if (mQmtCheckStandGrade != null && mQmtCheckStandGrade.StandId > 0)
        {
            NumberFieldGradePermMin.SetValue(mQmtCheckStandGrade.PermMin);
            CheckBoxGradeIfMin.SetValue(mQmtCheckStandGrade.IfMin);
            NumberFieldGradePermMax.SetValue(mQmtCheckStandGrade.PermMax);
            CheckBoxGradeIfMax.SetValue(mQmtCheckStandGrade.IfMax);
            CheckBoxGradeJudgeResult.SetValue(mQmtCheckStandGrade.JudgeResult);
            ComboBoxGradeDealCode.SetValue(mQmtCheckStandGrade.DealCode);
            TextFieldGradeDrawMark.SetValue(mQmtCheckStandGrade.DrawMark);
            NumberFieldGradeGrade.SetValue(mQmtCheckStandGrade.Grade);
            TextFieldGradeCardMark2.SetValue(mQmtCheckStandGrade.CardMark2);

            HiddenButtonCommandName.SetValue(EnumCommandName.Update); // Update

            WindowGrade.Title = "修改检验标准明细等级信息";
            WindowGrade.Icon = Icon.MonitorEdit;
            WindowGrade.Show();
        }
        else
        {
            X.MessageBox.Alert("提示", "未找到要修改的检验标准明细等级").Show();
        }
    }

    #endregion 检验标准明细等级添加/修改

    #region 检验标准明细等级删除
    protected void ButtonGradeDelete_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelGrade.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要删除的检验明细等级").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenGradeWeightId.Value.ToString();
        IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
        QmtCheckStandGrade mQmtCheckStandGrade = bQmtCheckStandGradeManager.GetById(new object[] { standId, itemCd, weightId });
        if (mQmtCheckStandGrade != null && mQmtCheckStandGrade.StandId > 0)
        {
            bQmtCheckStandGradeManager.DeleteWithLogic(mQmtCheckStandGrade);

            HiddenGradeWeightId.SetValue("");

            LoadGridPanelGrade(standId, itemCd);

            X.Msg.Alert("提示", "删除成功").Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要删除的检验明细等级").Show();
        }
    }

    #endregion 检验标准明细等级删除

    #endregion 检验标准明细等级操作

    #region 检验标准复制

    /// <summary>
    /// 清空拷贝信息
    /// 修改标识：qusf 20131213
    /// 修改内容：1.增加清空生效日期和生效时间
    /// </summary>
    private void ClearPanelMasterCopy()
    {
        TriggerFieldMasterCopyFromMaterName.SetValue("");
        ComboBoxMasterCopyStandCode.SetValue("");
        TextFieldMasterCopyDefineDate.SetValue("");
        NumberFieldMasterCopyStandVision.SetValue("");
        ComboBoxMasterCopyStandVisionStat.SetValue("");
        DateFieldMasterCopyRegDateTime.SetValue("");
        CheckBoxMasterCopyQuaCompute.SetValue(false);
        CheckBoxMasterCopyChoiceness.SetValue(false);
        TriggerFieldMasterCopyToMaterName.SetValue("");
        HiddenMasterCopyToMaterCode.SetValue("");

        DateFieldMasterCopyToRegDateTime.SetValue("");
        NumberEdt.SetValue("");
        ComboBoxShift.SetValue("");
        //TimeFieldMasterCopyToRegDateTime.SetValue("00:00:00");

        FieldSetMasterCopy.Collapse();

    }

    /// <summary>
    /// 检验标准复制--打开窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterCopy_Click(object sender, DirectEventArgs e)
    {
        string standId = HiddenStandId.Value.ToString();
        if (RowSelectionModelMaster.SelectedRows.Count == 0
            || standId == "")
        {
            X.Msg.Alert("提示", "请选择要复制的检验标准").Show();
            return;
        }

        ClearPanelMasterCopy();

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(standId);

        if (mQmtCheckStandMaster != null && mQmtCheckStandMaster.StandId > 0)
        {
            IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
            EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == mQmtCheckStandMaster.MaterCode);
            if (mBasMaterialList.Count > 0)
            {
                TriggerFieldMasterCopyFromMaterName.SetValue(mBasMaterialList[0].MaterialName);
            }

            ComboBoxMasterCopyStandCode.SetValue(mQmtCheckStandMaster.StandCode);
            TextFieldMasterCopyDefineDate.SetValue(mQmtCheckStandMaster.DefineDate);
            NumberFieldMasterCopyStandVision.SetValue(mQmtCheckStandMaster.StandVision);
            ComboBoxMasterCopyStandVisionStat.SetValue(mQmtCheckStandMaster.StandVisionStat);
            DateFieldMasterCopyRegDateTime.SetValue(mQmtCheckStandMaster.RegDateTime.Value);
            CheckBoxMasterCopyQuaCompute.SetValue(mQmtCheckStandMaster.QuaCompute);
            CheckBoxMasterCopyChoiceness.SetValue(mQmtCheckStandMaster.Choiceness);

            IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
            IQmtCheckStandDetailParams p = new QmtCheckStandDetailParams();
            p.StandId = mQmtCheckStandMaster.StandId.ToString();
            p.DeleteFlag = "0";
            DataSet ds = bQmtCheckStandDetailManager.GetDataByParas(p);
            //CheckboxGroupMasterCopy.cle;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Ext.Net.CheckboxBase chk = new Ext.Net.Checkbox();
                //chk.Name = "CheckBoxMasterCopy" + dr["ItemCd"].ToString();
                //chk.SetRawValue(dr["ItemCd"]);
                chk.InputValue = dr["ItemCd"].ToString();
                chk.BoxLabel = dr["ItemName"].ToString();
                chk.Checked = true;
                CheckboxGroupMasterCopy.Add(chk);
            }
            CheckboxGroupMasterCopy.ReRender();

            WindowMasterCopy.Title = "检验标准复制";
            WindowMasterCopy.Icon = Icon.PageCopy;
            WindowMasterCopy.Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要复制的检验标准").Show();
        }


    }

    /// <summary>
    /// 复制选择复制到目的物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldMasterCopyToMaterName_TriggerClick(object sender, DirectEventArgs e)
    {
        X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();");
    }

    /// <summary>
    /// 获取复制到目的胶料
    /// 修改标识：qusf 20131009
    /// 修改内容：1.因增加审核功能，所以复制后的版本状态为未提交
    /// 修改标识：qusf 20131022
    /// 修改内容：1.生效时间取复制前的时间
    /// 修改标识：qusf 20131213
    /// 修改内容：1.生效时间必为手工填写
    /// </summary>
    /// <returns></returns>
    private QmtCheckStandMaster GetCopyStandMaster()
    {
        string standId = HiddenStandId.Value.ToString();
        string materCodeCopyTo = HiddenMasterCopyToMaterCode.Value.ToString();
        string regDateTime = DateFieldMasterCopyToRegDateTime.RawText ;

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();

        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
        //mQmtCheckStandMaster.StandId = 0;
        mQmtCheckStandMaster.MaterCode = materCodeCopyTo;
        mQmtCheckStandMaster.WorkerBarcode = this.UserID;
        mQmtCheckStandMaster.DefineDate = DateTime.Today.ToString("yyyy-MM-dd");
        mQmtCheckStandMaster.StandDate = DateTime.Today.ToString("yyyy-MM-dd");
        mQmtCheckStandMaster.RegDateTime = Convert.ToDateTime(regDateTime);
        //mQmtCheckStandMaster.RegDateTime = DateTime.Now;
        mQmtCheckStandMaster.StandVision = -1;
        mQmtCheckStandMaster.StandVisionStat = Convert.ToInt32(EnumStandVisionStat.New).ToString();
        mQmtCheckStandMaster.LastModifyTime = DateTime.Now;
        mQmtCheckStandMaster.LastSubmitTime = null;
        mQmtCheckStandMaster.LastSubmitUser = "";
        mQmtCheckStandMaster.LastAuditTime = null;
        mQmtCheckStandMaster.LastAuditMemo = "";
        mQmtCheckStandMaster.LastAuditUser = "";
        mQmtCheckStandMaster.GUID = "";

        return mQmtCheckStandMaster;

    }

    /// <summary>
    /// 添加复制到目的胶料的检验标准
    /// </summary>
    [DirectMethod]
    public void AddCopyStandMaster(string itemCdList)
    {
        
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = GetCopyStandMaster();

        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList = bQmtCheckStandDetailManager.GetListByWhereAndOrder(
            QmtCheckStandDetail._.DeleteFlag == "0"
            & QmtCheckStandDetail._.StandId == mQmtCheckStandMaster.StandId
            & QmtCheckStandDetail._.ItemCd.In(itemCdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            , QmtCheckStandDetail._.ItemCd.Asc & QmtCheckStandDetail._.WeightId.Asc);

    
    AddCopy(mQmtCheckStandMaster, mQmtCheckStandDetailList);

        LoadGridPanelMaster(mQmtCheckStandMaster.MaterCode);

        WindowMasterCopy.Close();

        X.Msg.Alert("提示", "复制成功，下一步可以提交审核").Show();

    }

    public void AddCopy(QmtCheckStandMaster mQmtCheckStandMaster, EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList
       )
    {
      
        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
        {

            mQmtCheckStandMaster.Detach();

         
            IQmt_QuaStandMasterManager QM = new Qmt_QuaStandMasterManager();
           
            Qmt_QuaStandMaster qq = ChangeStandMaster(mQmtCheckStandMaster);
            qq.Recipe_Version =int.Parse (NumberEdt.Text);
            qq.Start_shift = int.Parse (ComboBoxShift.Value.ToString());
            qq.Stand_Date = DateFieldMasterCopyToRegDateTime.RawText;
            qq.Stand_code = Convert.ToInt32(ComboBoxMasterAimStandCode.Value.ToString());
            QM.Insert(qq);
          
            //int standIdCopyTo = this.Insert(mQmtCheckStandMaster);
            int standIdCopyTo = qq.Stand_id;
            IQmtCheckStandDetailService bQmtCheckStandDetailService = new QmtCheckStandDetailService();
            IQmt_QuaStandDetailManager qm = new Qmt_QuaStandDetailManager();
          
            foreach (QmtCheckStandDetail mQmtCheckStandDetail in mQmtCheckStandDetailList)
            {
                mQmtCheckStandDetail.Detach();
                mQmtCheckStandDetail.StandId = standIdCopyTo;
                Qmt_QuaStandDetail qd = changeStandDetail(mQmtCheckStandDetail);
              
                qm.Insert(qd);
                //bQmtCheckStandDetailService.Insert(mQmtCheckStandDetail);
            }

         

            scope.Complete();
            scope.Dispose();

        }

    }
    /// <summary>
    /// 升级复制到目的胶料的检验标准
    /// </summary>
    /// <param name="itemCdList"></param>
    [DirectMethod]
    public void UpgradeCopyStandMaster(string itemCdList)
    {
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = GetCopyStandMaster();

        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList = bQmtCheckStandDetailManager.GetListByWhereAndOrder(
            QmtCheckStandDetail._.DeleteFlag == "0"
            & QmtCheckStandDetail._.StandId == mQmtCheckStandMaster.StandId
            & QmtCheckStandDetail._.ItemCd.In(itemCdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            , QmtCheckStandDetail._.ItemCd.Asc & QmtCheckStandDetail._.WeightId.Asc);

        IQmtCheckStandGradeManager bQmtCheckStandGradeManager = new QmtCheckStandGradeManager();
        EntityArrayList<QmtCheckStandGrade> mQmtCheckStandGradeList = bQmtCheckStandGradeManager.GetListByWhereAndOrder(
            QmtCheckStandGrade._.DeleteFlag == "0"
            & QmtCheckStandGrade._.StandId == mQmtCheckStandMaster.StandId
            & QmtCheckStandGrade._.ItemCd.In(itemCdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            , QmtCheckStandGrade._.ItemCd.Asc & QmtCheckStandGrade._.WeightId.Asc);

        IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
        EntityArrayList<QmtCheckStandEquip> mQmtCheckStandEquipList = bQmtCheckStandEquipManager.GetListByWhereAndOrder(
            QmtCheckStandEquip._.DeleteFlag == "0"
            & QmtCheckStandEquip._.StandId == mQmtCheckStandMaster.StandId
            & QmtCheckStandEquip._.ItemCd.In(itemCdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            , QmtCheckStandEquip._.ItemCd.Asc & QmtCheckStandEquip._.CheckEquipCode.Asc & QmtCheckStandEquip._.WeightId.Asc);

        IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
        EntityArrayList<QmtCheckStandEquipGrade> mQmtCheckStandEquipGradeList = bQmtCheckStandEquipGradeManager.GetListByWhereAndOrder(
            QmtCheckStandEquipGrade._.DeleteFlag == "0"
            & QmtCheckStandEquipGrade._.StandId == mQmtCheckStandMaster.StandId
            & QmtCheckStandEquipGrade._.ItemCd.In(itemCdList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            , QmtCheckStandEquipGrade._.ItemCd.Asc & QmtCheckStandEquipGrade._.CheckEquipCode.Asc & QmtCheckStandEquipGrade._.WeightId.Asc);

        bQmtCheckStandMasterManager.UpgradeCopy(mQmtCheckStandMaster, mQmtCheckStandDetailList, mQmtCheckStandGradeList
            , mQmtCheckStandEquipList, mQmtCheckStandEquipGradeList);
        LoadGridPanelMaster(mQmtCheckStandMaster.MaterCode);

        WindowMasterCopy.Close();

        X.Msg.Alert("提示", "复制并升级版本成功").Show();

    }

    /// <summary>
    /// 复制
    /// 修改标识：qusf 20131009
    /// 修改内容：1.因增加审核功能，所以复制后的标准为未提交状态
    /// 修改标识：qusf 20131213
    /// 修改内容：1.增加对生效日期及生效时间的校验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterCopyAccept_Click(object sender, DirectEventArgs e)
    {
        string standId = HiddenStandId.Value.ToString();
        if (standId == "")
        {
            X.Msg.Alert("提示", "请选择复制源胶料").Show();
            return;
        }
        string regDate = DateFieldMasterCopyToRegDateTime.RawText;
        if (regDate == null || regDate == "")
        {
            X.Msg.Alert("提示", "请选择生效日期").Show();
            return;
        }

        string regTime = "";

        if (Convert.ToDateTime(regDate + " " + regTime) < DateTime.Now.AddDays(-1))
        {
            X.Msg.Alert("提示", "生效日期和时间不能小于当前时间").Show();
            return;
        }

        string standNameCopyTo = ComboBoxMasterCopyStandCode.SelectedItem.Text;
        string materCodeCopyTo = HiddenMasterCopyToMaterCode.Value.ToString();
        string materNameCopyTo = TriggerFieldMasterCopyToMaterName.Value.ToString();
        if (StringUtils.IsEmpty(materCodeCopyTo) == true)
        {
            X.Msg.Alert("提示", "请选择复制到目的胶料").Show();
            return;
        }
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetById(new object[] { standId });
        if (mQmtCheckStandMaster == null || mQmtCheckStandMaster.DeleteFlag == "1")
        {
            X.Msg.Alert("提示", "未找到要复制的源胶料标准").Show();
            return;
        }
        string standCodeCopyTo = mQmtCheckStandMaster.StandCode.ToString();

        string itemCdList = "";
        string json = e.ExtraParams["ItemCdList"];

        JObject obj = JsonConvert.DeserializeObject(json) as JObject;

        foreach (KeyValuePair<string, JToken> token in obj)
        {
            itemCdList = itemCdList + "," + token.Value.ToString();
        }
        if (itemCdList.Length > 0)
        {
            itemCdList = itemCdList.Remove(0, 1);
        }

        AddCopyStandMaster(itemCdList);

    }

    /// <summary>
    /// 关闭胶料检验标准复制窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterCopyCancel_Click(object sender, DirectEventArgs e)
    {
        WindowMasterCopy.Close();
    }

    #endregion 检验标准复制


    #region 检验标准机台明细操作

    /// <summary>
    /// 初始化--检验标准机台明细等级添加/修改
    /// </summary>
    private void ClearPanelEquip()
    {
        HiddenEquipCheckEquipCode.SetValue("");
        TriggerFieldEquipCheckEquipName.SetValue("");
        NumberFieldEquipPermMin.SetValue("");
        CheckBoxEquipIfMin.SetValue(true);
        NumberFieldEquipPermMax.SetValue("");
        CheckBoxEquipIfMax.SetValue(true);
        CheckBoxEquipJudgeResult.SetValue(true);
        ComboBoxEquipDealCode.SetValue("");
        TextFieldEquipDrawMark.SetValue("");
        NumberFieldEquipGrade.SetValue("");
        TextFieldEquipCardMark2.SetValue("");
    }

    /// <summary>
    /// 打开窗口--检验标准机台明细添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipAdd_Click(object sender, DirectEventArgs e)
    {
        if (RowSelectionModelDetail.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准明细").Show();
            return;
        }

        ClearPanelEquip();

        HiddenButtonCommandName.SetValue(EnumCommandName.Add); // Add

        WindowEquip.Title = "添加检验标准机台明细信息";
        WindowEquip.Icon = Icon.MonitorAdd;
        WindowEquip.Show();

    }

    /// <summary>
    /// 打开窗口--检验标准机台明细修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipEdit_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelEquip.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准机台明细").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenEquipWeightId.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
        QmtCheckStandEquip mQmtCheckStandEquip = bQmtCheckStandEquipManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });
        if (mQmtCheckStandEquip != null && mQmtCheckStandEquip.StandId > 0)
        {
            NumberFieldEquipPermMin.SetValue(mQmtCheckStandEquip.PermMin);
            CheckBoxEquipIfMin.SetValue(mQmtCheckStandEquip.IfMin);
            NumberFieldEquipPermMax.SetValue(mQmtCheckStandEquip.PermMax);
            CheckBoxEquipIfMax.SetValue(mQmtCheckStandEquip.IfMax);
            CheckBoxEquipJudgeResult.SetValue(mQmtCheckStandEquip.JudgeResult);
            ComboBoxEquipDealCode.SetValue(mQmtCheckStandEquip.DealCode);
            TextFieldEquipDrawMark.SetValue(mQmtCheckStandEquip.DrawMark);
            NumberFieldEquipGrade.SetValue(mQmtCheckStandEquip.Grade);
            TextFieldEquipCardMark2.SetValue(mQmtCheckStandEquip.CardMark2);

            HiddenButtonCommandName.SetValue(EnumCommandName.Update); // Update

            WindowEquip.Title = "修改检验标准机台明细信息";
            WindowEquip.Icon = Icon.MonitorEdit;
            WindowEquip.Show();
        }
        else
        {
            X.MessageBox.Alert("提示", "未找到要修改的检验标准明细").Show();
        }
    }

    /// <summary>
    /// 清空或查找 新增/修改窗体中的检验机台
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TriggerFieldEquipCheckEquipName_TriggerClick(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"];
        if (index == "0")
        {
            HiddenEquipCheckEquipCode.SetValue("");
            TriggerFieldEquipCheckEquipName.SetValue("");
        }
        else if (index == "1")
        {
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();");
        }

    }


    /// <summary>
    /// 验证检验标准机台明细
    /// </summary>
    /// <returns></returns>
    private bool ValidateStandEquip()
    {
        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        if (standId == "" || itemCd == "")
        {
            X.Msg.Alert("提示", "请选择一条标准明细信息").Show();
            return false;
        }
        string permMin = NumberFieldEquipPermMin.Number.ToString();
        if (permMin == "")
        {
            X.Msg.Alert("提示", "请填写下限值").Show();
            return false;
        }
        decimal dPermMin = 0;
        if (decimal.TryParse(permMin, out dPermMin) == false)
        {
            X.Msg.Alert("提示", "下限值必须是数字").Show();
            return false;
        }
        string permMax = NumberFieldEquipPermMax.Number.ToString();
        if (permMax == "")
        {
            X.Msg.Alert("提示", "请填写上限值").Show();
            return false;
        }
        decimal dPermMax = 0;
        if (decimal.TryParse(permMax, out dPermMax) == false)
        {
            X.Msg.Alert("提示", "上限值必须是数字").Show();
            return false;
        }
        if (Convert.ToDecimal(permMax) <= Convert.ToDecimal(permMin))
        {
            X.Msg.Alert("提示", "上限值必须大于下限值").Show();
            return false;
        }
        string dealCode = ComboBoxEquipDealCode.Value.ToString();
        if (dealCode == "")
        {
            X.Msg.Alert("提示", "请选择检测意见").Show();
            return false;
        }
        string grade = NumberFieldEquipGrade.Number.ToString();
        if (grade == "")
        {
            X.Msg.Alert("提示", "请填写等级").Show();
            return false;
        }

        string weightId = "";
        if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Add.ToString())
        {
            weightId = "0";
        }
        else if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Update.ToString())
        {
            weightId = HiddenEquipWeightId.Value.ToString();
        }

        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
        EntityArrayList<QmtCheckStandEquip> mQmtCheckStandEquipList = bQmtCheckStandEquipManager.GetListByWhere(QmtCheckStandEquip._.DeleteFlag == "0"
            & QmtCheckStandEquip._.StandId == standId
            & QmtCheckStandEquip._.ItemCd == itemCd
            & QmtCheckStandEquip._.CheckEquipCode == checkEquipCode
            & QmtCheckStandEquip._.WeightId != weightId);
        if (mQmtCheckStandEquipList.Count > 0)
        {
            string ifMin = CheckBoxEquipIfMin.Checked == true ? "1" : "0";
            string ifMax = CheckBoxEquipIfMax.Checked == true ? "1" : "0";

            DigitalRange aDigitalRange = new DigitalRange(decimal.Parse(permMin), int.Parse(ifMin), decimal.Parse(permMax), int.Parse(ifMax));

            List<DigitalRange> bDigitalRangeList = new List<DigitalRange>();
            foreach (QmtCheckStandEquip entity in mQmtCheckStandEquipList)
            {
                bDigitalRangeList.Add(new DigitalRange(entity.PermMin.Value, entity.IfMin.Value, entity.PermMax.Value, entity.IfMax.Value));
            }

            if (ValidateDigitalRange(aDigitalRange, bDigitalRangeList) == false)
            {
                X.Msg.Alert("提示", "检验机台明细上下限区间与其他记录有重叠").Show();
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 加载检验机台标准列表
    /// </summary>
    /// <param name="standId"></param>
    /// <param name="itemCd"></param>
    private void LoadGridPanelEquip(string standId, string itemCd)
    {
        StoreEquipGrade.RemoveAll();
        StoreEquip.RemoveAll();
        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true)
        {
            IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
            IQmtCheckStandEquipParams p = new QmtCheckStandEquipParams();
            p.StandId = standId;
            p.ItemCd = itemCd;
            p.DeleteFlag = "0";
            DataSet ds = bQmtCheckStandEquipManager.GetDataByParas(p);
            StoreEquip.DataSource = ds.Tables[0];
            StoreEquip.DataBind();
        }
    }

    /// <summary>
    /// 添加标准机台明细
    /// </summary>
    private void AddStandEquip()
    {
        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        string permMin = NumberFieldEquipPermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxEquipIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldEquipPermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxEquipIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = "0";
        if (CheckBoxEquipJudgeResult.Checked == true)
        {
            judgeResult = "1";
        }
        string dealCode = ComboBoxEquipDealCode.Value.ToString();
        string drawMark = TextFieldEquipDrawMark.Text.Trim();
        string grade = NumberFieldEquipGrade.Number.ToString();
        string cardMark2 = TextFieldEquipCardMark2.Text.Trim();

        QmtCheckStandEquip mQmtCheckStandEquip = new QmtCheckStandEquip();
        mQmtCheckStandEquip.StandId = Convert.ToInt32(standId);
        mQmtCheckStandEquip.ItemCd = itemCd;
        mQmtCheckStandEquip.CheckEquipCode = checkEquipCode;
        mQmtCheckStandEquip.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandEquip.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandEquip.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandEquip.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandEquip.Grade = Convert.ToInt32(grade);
        mQmtCheckStandEquip.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandEquip.DrawMark = drawMark;
        mQmtCheckStandEquip.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandEquip.CardMark2 = cardMark2;
        mQmtCheckStandEquip.DeleteFlag = "0";

        IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
        bQmtCheckStandEquipManager.Insert(mQmtCheckStandEquip);

        LoadGridPanelEquip(standId, itemCd);

        WindowEquip.Close();

        X.Msg.Alert("提示", "添加成功").Show();
    }

    /// <summary>
    /// 修改标准机台明细
    /// </summary>
    private void UpdateStandEquip()
    {
        IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenEquipWeightId.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        string permMin = NumberFieldEquipPermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxEquipIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldEquipPermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxEquipIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = "0";
        if (CheckBoxEquipJudgeResult.Checked == true)
        {
            judgeResult = "1";
        }
        string dealCode = ComboBoxEquipDealCode.Value.ToString();
        string drawMark = TextFieldEquipDrawMark.Text.Trim();
        string grade = NumberFieldEquipGrade.Number.ToString();
        string cardMark2 = TextFieldEquipCardMark2.Text.Trim();

        QmtCheckStandEquip mQmtCheckStandEquip = bQmtCheckStandEquipManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });

        mQmtCheckStandEquip.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandEquip.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandEquip.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandEquip.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandEquip.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandEquip.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandEquip.DrawMark = drawMark;
        mQmtCheckStandEquip.Grade = Convert.ToInt32(grade);
        mQmtCheckStandEquip.CardMark2 = cardMark2;

        bQmtCheckStandEquipManager.Update(mQmtCheckStandEquip);

        LoadGridPanelEquip(standId, itemCd);

        WindowEquip.Close();

        X.Msg.Alert("提示", "修改成功").Show();
    }

    /// <summary>
    /// 保存--检验标准机台明细等级添加/修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipAccept_Click(object sender, EventArgs e)
    {
        string commandName = HiddenButtonCommandName.Value.ToString();
        if (commandName == EnumCommandName.Add.ToString())
        {
            if (ValidateStandEquip() == false)
            {
                return;
            }
            AddStandEquip();
        }
        else if (commandName == EnumCommandName.Update.ToString())
        {
            if (ValidateStandEquip() == false)
            {
                return;
            }
            UpdateStandEquip();
        }
    }

    /// <summary>
    /// 删除--检验标准机台明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipDelete_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelEquip.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要删除的检验机台明细").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenEquipWeightId.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
        QmtCheckStandEquip mQmtCheckStandEquip = bQmtCheckStandEquipManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });
        if (mQmtCheckStandEquip != null && mQmtCheckStandEquip.StandId > 0)
        {
            bQmtCheckStandEquipManager.DeleteWithLogic(mQmtCheckStandEquip);

            HiddenEquipWeightId.SetValue("");

            LoadGridPanelEquip(standId, itemCd);

            X.Msg.Alert("提示", "删除成功").Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要删除的检验机台明细级").Show();
        }
    }

    /// <summary>
    /// 选择检验标准机台明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelEquip_SelectionChange(object sender, DirectEventArgs e)
    {
        string standId = e.ExtraParams["StandId"];
        string itemCd = e.ExtraParams["ItemCd"];
        string weightId = e.ExtraParams["WeightId"];
        string checkEquipCode = e.ExtraParams["CheckEquipCode"];

        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true
            && StringUtils.IsNotEmpty(weightId) == true
            && StringUtils.IsNotEmpty(checkEquipCode) == true)
        {
            IQmtCheckStandEquipManager bQmtCheckStandEquipManager = new QmtCheckStandEquipManager();
            QmtCheckStandEquip mQmtCheckStandEquip = bQmtCheckStandEquipManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });
            HiddenStandId.SetValue(mQmtCheckStandEquip.StandId);
            HiddenItemCd.SetValue(mQmtCheckStandEquip.ItemCd);
            HiddenEquipWeightId.SetValue(mQmtCheckStandEquip.WeightId);
            HiddenEquipCheckEquipCode.SetValue(mQmtCheckStandEquip.CheckEquipCode);

            IBasEquipManager bBasEquipManager = new BasEquipManager();
            EntityArrayList<BasEquip> mBasEquipList = bBasEquipManager.GetListByWhere(BasEquip._.EquipCode == mQmtCheckStandEquip.CheckEquipCode);
            if (mBasEquipList.Count > 0)
            {
                TriggerFieldEquipCheckEquipName.SetValue(mBasEquipList[0].EquipName);
            }

        }
        else
        {
            HiddenEquipWeightId.SetValue("");
        }
        LoadGridPanelEquipGrade(standId, itemCd, checkEquipCode);

    }

    /// <summary>
    /// 关闭窗口--检验标准机台明细
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipCancel_Click(object sender, EventArgs e)
    {
        WindowEquip.Close();
    }


    #endregion 检验标准机台明细操作

    #region 检验标准机台等级操作

    /// <summary>
    /// 初始化--检验标准机台等级添加/修改
    /// </summary>
    private void ClearPanelEquipGrade()
    {
        NumberFieldEquipGradePermMin.SetValue("");
        CheckBoxEquipGradeIfMin.SetValue(true);
        NumberFieldEquipGradePermMax.SetValue("");
        CheckBoxEquipGradeIfMax.SetValue(true);
        CheckBoxEquipGradeJudgeResult.SetValue(true);
        ComboBoxEquipGradeDealCode.SetValue("");
        TextFieldEquipGradeDrawMark.SetValue("");
        NumberFieldEquipGradeGrade.SetValue("");
        TextFieldEquipGradeCardMark2.SetValue("");
    }


    /// <summary>
    /// 打开窗口--检验标准机台等级添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipGradeAdd_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelEquip.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准机台").Show();
            return;
        }

        ClearPanelEquipGrade();

        HiddenButtonCommandName.SetValue(EnumCommandName.Add); // Add

        WindowEquipGrade.Title = "添加检验标准明细等级信息";
        WindowEquipGrade.Icon = Icon.MonitorAdd;
        WindowEquipGrade.Show();
    }


    /// <summary>
    /// 打开窗口--检验标准机台等级修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipGradeEdit_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelEquipGrade.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择检验标准机台等级").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenEquipGradeWeightId.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
        QmtCheckStandEquipGrade mQmtCheckStandEquipGrade = bQmtCheckStandEquipGradeManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });
        if (mQmtCheckStandEquipGrade != null && mQmtCheckStandEquipGrade.StandId > 0)
        {
            NumberFieldEquipGradePermMin.SetValue(mQmtCheckStandEquipGrade.PermMin);
            CheckBoxEquipGradeIfMin.SetValue(mQmtCheckStandEquipGrade.IfMin);
            NumberFieldEquipGradePermMax.SetValue(mQmtCheckStandEquipGrade.PermMax);
            CheckBoxEquipGradeIfMax.SetValue(mQmtCheckStandEquipGrade.IfMax);
            CheckBoxEquipGradeJudgeResult.SetValue(mQmtCheckStandEquipGrade.JudgeResult);
            ComboBoxEquipGradeDealCode.SetValue(mQmtCheckStandEquipGrade.DealCode);
            TextFieldEquipGradeDrawMark.SetValue(mQmtCheckStandEquipGrade.DrawMark);
            NumberFieldEquipGradeGrade.SetValue(mQmtCheckStandEquipGrade.Grade);
            TextFieldEquipGradeCardMark2.SetValue(mQmtCheckStandEquipGrade.CardMark2);

            HiddenButtonCommandName.SetValue(EnumCommandName.Update); // Update

            WindowEquipGrade.Title = "修改检验标准机台等级信息";
            WindowEquipGrade.Icon = Icon.MonitorEdit;
            WindowEquipGrade.Show();
        }
        else
        {
            X.MessageBox.Alert("提示", "未找到要修改的检验机台标准").Show();
        }
    }

    /// <summary>
    /// 加载检验标准机台等级列表
    /// </summary>
    /// <param name="standId"></param>
    /// <param name="itemCd"></param>
    /// <param name="checkEquipCode"></param>
    private void LoadGridPanelEquipGrade(string standId, string itemCd, string checkEquipCode)
    {
        StoreEquipGrade.RemoveAll();
        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true
            && StringUtils.IsNotEmpty(checkEquipCode) == true)
        {
            IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
            IQmtCheckStandEquipGradeParams p = new QmtCheckStandEquipGradeParams();
            p.StandId = standId;
            p.ItemCd = itemCd;
            p.CheckEquipCode = checkEquipCode;
            p.DeleteFlag = "0";
            DataSet ds = bQmtCheckStandEquipGradeManager.GetDataByParas(p);
            StoreEquipGrade.DataSource = ds.Tables[0];
            StoreEquipGrade.DataBind();
        }
    }


    #region 检验标准机台等级删除
    /// <summary>
    /// 删除--检验标准机台等级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipGradeDelete_Click(object sender, EventArgs e)
    {
        if (RowSelectionModelEquipGrade.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要删除的检验机台等级").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenEquipGradeWeightId.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
        QmtCheckStandEquipGrade mQmtCheckStandEquipGrade = bQmtCheckStandEquipGradeManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });
        if (mQmtCheckStandEquipGrade != null && mQmtCheckStandEquipGrade.StandId > 0)
        {
            bQmtCheckStandEquipGradeManager.DeleteWithLogic(mQmtCheckStandEquipGrade);

            HiddenEquipGradeWeightId.SetValue("");

            LoadGridPanelEquipGrade(standId, itemCd, checkEquipCode);

            X.Msg.Alert("提示", "删除成功").Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要删除的检验机台等级").Show();
        }
    }

    #endregion 检验标准机台等级删除


    /// <summary>
    /// 选择检验标准机台等级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelEquipGrade_SelectionChange(object sender, DirectEventArgs e)
    {
        string standId = e.ExtraParams["StandId"];
        string itemCd = e.ExtraParams["ItemCd"];
        string weightId = e.ExtraParams["WeightId"];
        string checkEquipCode = e.ExtraParams["CheckEquipCode"];

        if (StringUtils.IsNotEmpty(standId) == true
            && StringUtils.IsNotEmpty(itemCd) == true
            && StringUtils.IsNotEmpty(weightId) == true
            && StringUtils.IsNotEmpty(checkEquipCode) == true)
        {
            IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
            QmtCheckStandEquipGrade mQmtCheckStandEquipGrade = bQmtCheckStandEquipGradeManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });
            HiddenStandId.SetValue(mQmtCheckStandEquipGrade.StandId);
            HiddenItemCd.SetValue(mQmtCheckStandEquipGrade.ItemCd);
            HiddenEquipGradeWeightId.SetValue(mQmtCheckStandEquipGrade.WeightId);
            HiddenEquipCheckEquipCode.SetValue(mQmtCheckStandEquipGrade.CheckEquipCode);
        }
        else
        {
            HiddenEquipGradeWeightId.SetValue("");
        }

    }

    /// <summary>
    /// 验证检验标准机台等级
    /// </summary>
    /// <returns></returns>
    private bool ValidateStandEquipGrade()
    {
        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        if (standId == "" || itemCd == "")
        {
            X.Msg.Alert("提示", "请选择一条标准明细信息").Show();
            return false;
        }
        string permMin = NumberFieldEquipGradePermMin.Number.ToString();
        if (permMin == "")
        {
            X.Msg.Alert("提示", "请填写下限值").Show();
            return false;
        }
        decimal dPermMin = 0;
        if (decimal.TryParse(permMin, out dPermMin) == false)
        {
            X.Msg.Alert("提示", "下限值必须是数字").Show();
            return false;
        }
        string permMax = NumberFieldEquipGradePermMax.Number.ToString();
        if (permMax == "")
        {
            X.Msg.Alert("提示", "请填写上限值").Show();
            return false;
        }
        decimal dPermMax = 0;
        if (decimal.TryParse(permMax, out dPermMax) == false)
        {
            X.Msg.Alert("提示", "上限值必须是数字").Show();
            return false;
        }
        if (Convert.ToDecimal(permMax) <= Convert.ToDecimal(permMin))
        {
            X.Msg.Alert("提示", "上限值必须大于下限值").Show();
            return false;
        }
        string dealCode = ComboBoxEquipGradeDealCode.Value.ToString();
        if (dealCode == "")
        {
            X.Msg.Alert("提示", "请选择检测意见").Show();
            return false;
        }
        string grade = NumberFieldEquipGradeGrade.Number.ToString();
        if (grade == "")
        {
            X.Msg.Alert("提示", "请填写等级").Show();
            return false;
        }

        string weightId = "";
        if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Add.ToString())
        {
            weightId = "0";
        }
        else if (HiddenButtonCommandName.Value.ToString() == EnumCommandName.Update.ToString())
        {
            weightId = HiddenEquipGradeWeightId.Value.ToString();
        }

        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();

        IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
        EntityArrayList<QmtCheckStandEquipGrade> mQmtCheckStandEquipGradeList = bQmtCheckStandEquipGradeManager.GetListByWhere(QmtCheckStandEquipGrade._.DeleteFlag == "0"
            & QmtCheckStandEquipGrade._.StandId == standId
            & QmtCheckStandEquipGrade._.ItemCd == itemCd
            & QmtCheckStandEquipGrade._.CheckEquipCode == checkEquipCode
            & QmtCheckStandEquipGrade._.WeightId != weightId);
        if (mQmtCheckStandEquipGradeList.Count > 0)
        {
            string ifMin = CheckBoxEquipGradeIfMin.Checked == true ? "1" : "0";
            string ifMax = CheckBoxEquipGradeIfMax.Checked == true ? "1" : "0";

            DigitalRange aDigitalRange = new DigitalRange(decimal.Parse(permMin), int.Parse(ifMin), decimal.Parse(permMax), int.Parse(ifMax));

            List<DigitalRange> bDigitalRangeList = new List<DigitalRange>();
            foreach (QmtCheckStandEquipGrade entity in mQmtCheckStandEquipGradeList)
            {
                bDigitalRangeList.Add(new DigitalRange(entity.PermMin.Value, entity.IfMin.Value, entity.PermMax.Value, entity.IfMax.Value));
            }

            if (ValidateDigitalRange(aDigitalRange, bDigitalRangeList) == false)
            {
                X.Msg.Alert("提示", "检验机台等级上下限区间与其他记录有重叠").Show();
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 添加标准机台等级
    /// </summary>
    private void AddStandEquipGrade()
    {
        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string permMin = NumberFieldEquipGradePermMin.Number.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        string ifMin = "0";
        if (CheckBoxEquipGradeIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldEquipGradePermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxEquipGradeIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = "0";
        if (CheckBoxEquipGradeJudgeResult.Checked == true)
        {
            judgeResult = "1";
        }
        string dealCode = ComboBoxEquipGradeDealCode.Value.ToString();
        string drawMark = TextFieldEquipGradeDrawMark.Text.Trim();
        string grade = NumberFieldEquipGradeGrade.Number.ToString();
        string cardMark2 = TextFieldEquipGradeCardMark2.Text.Trim();

        QmtCheckStandEquipGrade mQmtCheckStandEquipGrade = new QmtCheckStandEquipGrade();
        mQmtCheckStandEquipGrade.StandId = Convert.ToInt32(standId);
        mQmtCheckStandEquipGrade.ItemCd = itemCd;
        mQmtCheckStandEquipGrade.CheckEquipCode = checkEquipCode;
        mQmtCheckStandEquipGrade.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandEquipGrade.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandEquipGrade.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandEquipGrade.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandEquipGrade.Grade = Convert.ToInt32(grade);
        mQmtCheckStandEquipGrade.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandEquipGrade.DrawMark = drawMark;
        mQmtCheckStandEquipGrade.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandEquipGrade.CardMark2 = cardMark2;
        mQmtCheckStandEquipGrade.DeleteFlag = "0";

        IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();
        bQmtCheckStandEquipGradeManager.Insert(mQmtCheckStandEquipGrade);

        LoadGridPanelEquipGrade(standId, itemCd, checkEquipCode);

        WindowEquipGrade.Close();

        X.Msg.Alert("提示", "添加成功").Show();
    }

    /// <summary>
    /// 修改标准机台等级
    /// </summary>
    private void UpdateStandEquipGrade()
    {
        IQmtCheckStandEquipGradeManager bQmtCheckStandEquipGradeManager = new QmtCheckStandEquipGradeManager();

        string standId = HiddenStandId.Value.ToString();
        string itemCd = HiddenItemCd.Value.ToString();
        string weightId = HiddenEquipGradeWeightId.Value.ToString();
        string checkEquipCode = HiddenEquipCheckEquipCode.Value.ToString();
        string permMin = NumberFieldEquipGradePermMin.Number.ToString();
        string ifMin = "0";
        if (CheckBoxEquipGradeIfMin.Checked == true)
        {
            ifMin = "1";
        }
        string permMax = NumberFieldEquipGradePermMax.Number.ToString();
        string ifMax = "0";
        if (CheckBoxEquipGradeIfMax.Checked == true)
        {
            ifMax = "1";
        }
        string judgeResult = "0";
        if (CheckBoxEquipGradeJudgeResult.Checked == true)
        {
            judgeResult = "1";
        }
        string dealCode = ComboBoxEquipGradeDealCode.Value.ToString();
        string drawMark = TextFieldEquipGradeDrawMark.Text.Trim();
        string grade = NumberFieldEquipGradeGrade.Number.ToString();
        string cardMark2 = TextFieldEquipGradeCardMark2.Text.Trim();

        QmtCheckStandEquipGrade mQmtCheckStandEquipGrade = bQmtCheckStandEquipGradeManager.GetById(new object[] { standId, itemCd, weightId, checkEquipCode });

        mQmtCheckStandEquipGrade.PermMin = Convert.ToDecimal(permMin);
        mQmtCheckStandEquipGrade.IfMin = Convert.ToInt32(ifMin);
        mQmtCheckStandEquipGrade.PermMax = Convert.ToDecimal(permMax);
        mQmtCheckStandEquipGrade.IfMax = Convert.ToInt32(ifMax);
        mQmtCheckStandEquipGrade.JudgeResult = Convert.ToInt32(judgeResult);
        mQmtCheckStandEquipGrade.DealCode = Convert.ToInt32(dealCode);
        mQmtCheckStandEquipGrade.DrawMark = drawMark;
        mQmtCheckStandEquipGrade.Grade = Convert.ToInt32(grade);
        mQmtCheckStandEquipGrade.CardMark2 = cardMark2;

        bQmtCheckStandEquipGradeManager.Update(mQmtCheckStandEquipGrade);

        LoadGridPanelEquipGrade(standId, itemCd, checkEquipCode);

        WindowEquipGrade.Close();

        X.Msg.Alert("提示", "修改成功").Show();
    }

    /// <summary>
    /// 保存--检验标准机台等级添加/修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipGradeAccept_Click(object sender, EventArgs e)
    {
        string commandName = HiddenButtonCommandName.Value.ToString();
        if (commandName == EnumCommandName.Add.ToString())
        {
            if (ValidateStandEquipGrade() == false)
            {
                return;
            }
            AddStandEquipGrade();
        }
        else if (commandName == EnumCommandName.Update.ToString())
        {
            if (ValidateStandGrade() == false)
            {
                return;
            }
            UpdateStandEquipGrade();
        }
    }

    /// <summary>
    /// 关闭窗口--检验标准明细等级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEquipGradeCancel_Click(object sender, EventArgs e)
    {
        WindowEquipGrade.Close();
    }


    #endregion 检验标准机台等级操作


    #region 审核操作

    /// <summary>
    /// 检验标准审核--打开窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonMasterAudit_Click(object sender, DirectEventArgs e)
    {
        if (RowSelectionModelMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要审核的检验标准").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
        if (mQmtCheckStandMaster != null && mQmtCheckStandMaster.StandId > 0 && mQmtCheckStandMaster.DeleteFlag == "0")
        {
            if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
            {
                string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
                X.Msg.Alert("提示", "版本状态为" + standVisionStatDesp + "，不允许审核").Show();
                return;
            }
            ClearPanelAudit();
            ComboBoxAuditStandCode.SetValue(mQmtCheckStandMaster.StandCode);
            IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
            EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == mQmtCheckStandMaster.MaterCode);
            if (mBasMaterialList.Count > 0)
            {
                TriggerFieldAuditMaterName.SetValue(mBasMaterialList[0].MaterialName);
            }

            CheckboxAuditQuaCompute.SetValue(mQmtCheckStandMaster.QuaCompute);
            CheckboxAuditChoiceness.SetValue(mQmtCheckStandMaster.Choiceness);
            ComboBoxAuditStandVisionStat.SetValue(mQmtCheckStandMaster.StandVisionStat);
            TextAreaAuditAuditMemo.SetValue("");

            TextFieldAuditLLStandVision.SetValue(mQmtCheckStandMaster.LLStandVision);
            TextFieldAuditRegDateTime.SetValue(mQmtCheckStandMaster.RegDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            ComboBoxAuditStandCode.Disabled = false;
            ComboBoxAuditStandCode.ReadOnly = true;
            ComboBoxAuditStandCode.Editable = false;

            TriggerFieldAuditMaterName.Disabled = false;
            TriggerFieldAuditMaterName.Editable = false;
            TriggerFieldAuditMaterName.ReadOnly = true;

            CheckboxAuditQuaCompute.Disabled = false;
            CheckboxAuditQuaCompute.ReadOnly = true;

            TextFieldAuditLLStandVision.Disabled = false;
            TextFieldAuditLLStandVision.ReadOnly = true;

            TextFieldAuditRegDateTime.Disabled = false;
            TextFieldAuditRegDateTime.ReadOnly = true;

            CheckboxAuditChoiceness.Hidden = true;



            WindowAudit.Title = "审核检验标准信息";
            WindowAudit.Icon = Icon.PageGear;
            WindowAudit.Show();
        }
        else
        {
            X.Msg.Alert("提示", "未找到要审核的检验标准").Show();
        }


    }

    private void ClearPanelAudit()
    {
        ComboBoxAuditStandCode.SetValue("");
        TriggerFieldAuditMaterName.SetValue("");
        ComboBoxMasterStandVisionStat.SetValue("");
        CheckboxMasterQuaCompute.SetValue(true);
        CheckboxMasterChoiceness.SetValue(false);

    }


    /// <summary>
    /// 审核通过
    /// 修改标识：qusf 20131022
    /// 修改内容：1.不再更新生效时间
    /// 修改标识：qusf 20131207
    /// 修改内容：1.审核通过时，生效时间不能早于当前时间
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAuditAccept_Click(object sender, EventArgs e)
    {
        string auditMemo = TextAreaAuditAuditMemo.Text.ToString().Trim();

        string standId = HiddenStandId.Value.ToString();
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
        if (mQmtCheckStandMaster == null)
        {
            X.Msg.Alert("提示", "未找到要审核的质检标准，请核实").Show();
            return;
        }

        if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
            X.Msg.Alert("提示", "质检标准版本状态为" + standVisionStatDesp + "，不允许审核通过").Show();
            return;
        }

        //if (mQmtCheckStandMaster.RegDateTime.HasValue == true && mQmtCheckStandMaster.RegDateTime.Value < DateTime.Now)
        //{
        //    X.Msg.Alert("提示", "质检标准生效时间不能早于当前审核时间").Show();
        //    return;

        //}

        mQmtCheckStandMaster.LastAuditUser = this.UserID;
        mQmtCheckStandMaster.LastAuditTime = DateTime.Now;
        mQmtCheckStandMaster.LastAuditMemo = auditMemo;
        mQmtCheckStandMaster.StandVisionStat = Convert.ToInt32(EnumStandVisionStat.Enabled).ToString();

        //mQmtCheckStandMaster.RegDateTime = DateTime.Now;

        //bQmtCheckStandMasterManager.Audit(mQmtCheckStandMaster);
        Audit(mQmtCheckStandMaster);
        QueryGridPanelMaster("0");

        WindowAudit.Close();

        X.Msg.Alert("提示", "审核完毕").Show();

    }
    public void Audit(QmtCheckStandMaster entity)
    {
        QmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        EntityArrayList<QmtCheckStandMaster> entities =
            bQmtCheckStandMasterManager.GetListByWhereAndOrder(QmtCheckStandMaster._.StandCode == entity.StandCode
            & QmtCheckStandMaster._.MaterCode == entity.MaterCode
            & QmtCheckStandMaster._.DeleteFlag == "0"
            & QmtCheckStandMaster._.StandVision > 0
            , QmtCheckStandMaster._.StandVision.Desc);
        if (entities.Count > 0)
        {
            entity.StandVision = entities[0].StandVision.Value;
        }
        else
        {
            entity.StandVision = 1;
        }

        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
        {
            // yzx   不再默认启用 QmtCheckStandMasterManager.Update(new PropertyItem[] { QmtCheckStandMaster._.StandVisionStat }
            //    , new object[] { 0 }
            //    , QmtCheckStandMaster._.StandVisionStat == "1"
            //    & QmtCheckStandMaster._.StandCode == entity.StandCode
            //      & QmtCheckStandMaster._.PmtType == entity.PmtType
            //    & QmtCheckStandMaster._.MaterCode == entity.MaterCode);


            IQmt_QuaStandMasterManager QM = new Qmt_QuaStandMasterManager();
            Qmt_QuaStandMaster qq = QM.GetById(entity.StandId);

            qq.Auditing = 1;
            qq.AuditUser = entity.LastAuditUser;
            qq.Stand_vision = entity.StandVision;

            qq.Stand_VisionStat = entity.StandVisionStat;
            QM.Update(qq);
            //bQmtCheckStandMasterManager.Update(entity);

            scope.Complete();
            scope.Dispose();
        }

    }
    /// <summary>
    /// 退回修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAuditSendback_Click(object sender, EventArgs e)
    {
        string auditMemo = TextAreaAuditAuditMemo.Text.Trim();
        //if (auditMemo == "")
        //{
        //    X.Msg.Alert("提示", "审核意见不能为空").Show();
        //    return;
        //}

        string standId = HiddenStandId.Value.ToString();
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        QmtCheckStandMaster mQmtCheckStandMaster = bQmtCheckStandMasterManager.GetListByWhere(QmtCheckStandMaster._.StandId==standId)[0];
        if (mQmtCheckStandMaster == null)
        {
            X.Msg.Alert("提示", "未找到要审核的质检标准，请核实").Show();
            return;
        }

        if (mQmtCheckStandMaster.StandVisionStat != Convert.ToInt32(EnumStandVisionStat.Submitted).ToString())
        {
            string standVisionStatDesp = GetStandVisionStatDesp(mQmtCheckStandMaster.StandVisionStat);
            X.Msg.Alert("提示", "质检标准版本状态为" + standVisionStatDesp + "，不允许退回修改").Show();
            return;
        }

        mQmtCheckStandMaster.LastAuditUser = this.UserID;
        mQmtCheckStandMaster.LastAuditTime = DateTime.Now;
        mQmtCheckStandMaster.LastAuditMemo = auditMemo;
        mQmtCheckStandMaster.StandVisionStat = Convert.ToInt32(EnumStandVisionStat.Sendback).ToString();


        IQmt_QuaStandMasterManager QM = new Qmt_QuaStandMasterManager();
        Qmt_QuaStandMaster qq = QM.GetById(mQmtCheckStandMaster.StandId);

        //qq.Auditing = 1;
        qq.AuditUser = mQmtCheckStandMaster.LastAuditUser;
        //qq.Stand_vision = entity.StandVision;

        qq.Stand_VisionStat = mQmtCheckStandMaster.StandVisionStat;
        QM.Update(qq);


        //bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);

        QueryGridPanelMaster("0");

        WindowAudit.Close();

        X.Msg.Alert("提示", "退回完毕").Show();

    }

    /// <summary>
    /// 关闭审核窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAuditCancel_Click(object sender, EventArgs e)
    {
        WindowAudit.Close();
    }

    /// <summary>
    /// 加载检验标准列表
    /// </summary>
    /// <param name="materCode"></param>
    private void QueryGridPanelMaster(string DeleteFlag)
    {
        IQmtCheckStandMasterParams paras = new QmtCheckStandMasterParams();
        string materCode = HiddenNorthMaterCode.Value.ToString();
        paras.MaterCode = materCode;
        if (DeleteFlag == "0")
        {
            paras.DeleteFlag = DeleteFlag;
        }
        string standCode = ComboBoxNorthStandCode.Value.ToString();
        if (standCode != "-1")
        {
            paras.StandCode = standCode;
        }
        string standVisionStat = ComboBoxNorthStandVisionStat.Value.ToString();
        if (standVisionStat != "-1")
        {
            paras.StandVisionStat = standVisionStat;
        }
        paras.PmtType = ComboBoxRubType.Value.ToString();
        //X.Js.Alert(paras.PmtType); return;
        StoreEquipGrade.RemoveAll();
        StoreEquip.RemoveAll();
        StoreGrade.RemoveAll();
        StoreDetail.RemoveAll();
        StoreMaster.RemoveAll();

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        DataSet ds = bQmtCheckStandMasterManager.GetDataByParas(paras);
        GroupingMaster.ExpandAll();
        StoreMaster.DataSource = ds.Tables[0];
        StoreMaster.DataBind();

    }



    #endregion 审核操作

    #region 修改生效时间操作

    protected void ButtonMasterEditRegDateTime_Click(object sender, DirectEventArgs e)
    {
        if (RowSelectionModelMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要修改的记录").Show();
            return;
        }
        string standId = HiddenStandId.Value.ToString();
        if (standId == "")
        {
            X.Msg.Alert("提示", "未找到记录编号").Show();
            return;
        }
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList = bQmtCheckStandMasterManager.GetListByWhereAndOrder(
            QmtCheckStandMaster._.DeleteFlag == "0"
            & QmtCheckStandMaster._.StandId == standId
            , QmtCheckStandMaster._.DeleteFlag.Asc);
        if (mQmtCheckStandMasterList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到相应的记录").Show();
            return;
        }
        QmtCheckStandMaster mQmtCheckStandMaster = mQmtCheckStandMasterList[0];
        if (mQmtCheckStandMaster.StandVision.HasValue == false || mQmtCheckStandMaster.StandVision.Value <= 0)
        {
            X.Msg.Alert("提示", "记录版本小于0，不允许修改生效时间").Show();
            return;
        }
        if (mQmtCheckStandMaster.RegDateTime.HasValue == false || mQmtCheckStandMaster.RegDateTime.Value < DateTime.Now.AddHours(-1))
        {
            X.Msg.Alert("提示", "该记录已生效使用，不允许修改生效时间").Show();
            return;
        }

        string materName = "";
        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.MaterialCode == mQmtCheckStandMaster.MaterCode
            , BasMaterial._.DeleteFlag.Asc);
        if (mBasMaterialList.Count > 0)
        {
            materName = mBasMaterialList[0].MaterialName.Trim();
        }

        TriggerFieldSpecMaterName.SetValue(materName);
        if (mQmtCheckStandMaster.RegDateTime.HasValue == true)
        {
            DateFieldSpecRegDateTime.SetValue(mQmtCheckStandMaster.RegDateTime.Value.ToString("yyyy-MM-dd"));
            TimeFieldSpecRegDateTime.SetValue(mQmtCheckStandMaster.RegDateTime.Value.ToString("HH:mm:ss"));
        }
        WindowSpec.Show();

        string standName = "";
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.ObjID == mQmtCheckStandMaster.StandCode
            , QmtCheckStandType._.DeleteFlag.Asc);
        if (mQmtCheckStandTypeList.Count > 0)
        {
            standName = mQmtCheckStandTypeList[0].StandTypeName;
        }
        TextFieldSpecStandName.SetValue(standName);
    }

    protected void ButtonSpecAccept_Click(object sender, DirectEventArgs e)
    {
        string dateRegDateTime = DateFieldSpecRegDateTime.RawText;
        string timeRegDateTime = TimeFieldSpecRegDateTime.RawText;
        if (dateRegDateTime == "")
        {
            X.Msg.Alert("提示", "生效日期不能为空").Show();
            return;
        }
        if (timeRegDateTime == "")
        {
            X.Msg.Alert("提示", "生效时间不能为空").Show();
            return;
        }
        // 生效时间不能早于当前时间1小时
        if (DateTime.Parse(dateRegDateTime + " " + timeRegDateTime) < DateTime.Now.AddHours(-1))
        {
            X.Msg.Alert("提示", "生效时间不能早于当前时间前一小时").Show();
            return;
        }

        string standId = HiddenStandId.Value.ToString();
        if (standId == "")
        {
            X.Msg.Alert("提示", "未找到要修改的记录编号").Show();
            return;
        }

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList = bQmtCheckStandMasterManager.GetListByWhereAndOrder(
            QmtCheckStandMaster._.DeleteFlag == "0"
            & QmtCheckStandMaster._.StandId == standId
            , QmtCheckStandMaster._.DeleteFlag.Asc);

        if (mQmtCheckStandMasterList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到要修改的记录").Show();
            return;
        }

        QmtCheckStandMaster mQmtCheckStandMaster = mQmtCheckStandMasterList[0];
        if (mQmtCheckStandMaster.StandVision <= 0)
        {
            X.Msg.Alert("提示", "记录版本小于0，不允许修改生效时间").Show();
            return;
        }

        System.Text.StringBuilder sbMethodResultLog = new System.Text.StringBuilder();
        sbMethodResultLog.AppendFormat("StandId={0}", standId);
        sbMethodResultLog.AppendFormat(",原生效时间OldRegDateTime={0}", mQmtCheckStandMaster.RegDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        sbMethodResultLog.AppendFormat(",新生效时间NewRegDateTime={0}", dateRegDateTime + " " + timeRegDateTime);

        mQmtCheckStandMaster.RegDateTime = DateTime.Parse(dateRegDateTime + " " + timeRegDateTime);
        mQmtCheckStandMaster.LastModifyTime = DateTime.Now;
        mQmtCheckStandMaster.WorkerBarcode = this.UserID;

        bQmtCheckStandMasterManager.Update(mQmtCheckStandMaster);

        this.AppendWebLog("胶料质检标准修改(生效时间)", sbMethodResultLog.ToString());


        X.Msg.Alert("提示", "生效时间修改成功").Show();

        WindowSpec.Close();

        QueryGridPanelMaster("0");


    }

    #endregion 修改生效时间操作
}
