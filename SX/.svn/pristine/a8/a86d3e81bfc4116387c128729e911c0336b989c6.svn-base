using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Ext.Net;
using Ext.Net.Utilities;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Web.UI;

using NBear.Common;
using System.Text;

public partial class Manager_RubberQuality_BasicInfo_CheckStandInfo : BasePage
{

    private object[] _MajorTypes = { 5 };

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
        }

        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
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
    #endregion 结构

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
            scriptLink.Attributes.Add("src", "CheckStandInfo.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            ButtonNothOperMater.Disabled = this._.查询.SeqIdx == 0;

            InitControls();
        }
    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 标准分类
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.DeleteFlag == "0"
            , QmtCheckStandType._.ObjID.Asc);
        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            ComboBoxNorthStandCode.AddItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString());
        }

        // 版本状态
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("未提交", Convert.ToInt32(EnumStandVisionStat.New).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已提交", Convert.ToInt32(EnumStandVisionStat.Submitted).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已退回", Convert.ToInt32(EnumStandVisionStat.Sendback).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已启用", Convert.ToInt32(EnumStandVisionStat.Enabled).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已停用", Convert.ToInt32(EnumStandVisionStat.Disabled).ToString()));
        ComboBoxNorthStandVisionStat.Items.Add(new Ext.Net.ListItem("已作废", Convert.ToInt32(EnumStandVisionStat.Invalid).ToString()));

        //配方类型
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(SysCode._.TypeID == "PmtType");
        IniComboBox(cboType, lst);
    }

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

    /// <summary>
    /// 选择标准
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModelMaster_SelectionChange(object sender, DirectEventArgs e)
    {
        string standId = e.ExtraParams["StandId"];

        LoadGridPanelDetail(standId);

    }

    /// <summary>
    /// 加载检验标准明细列表
    /// </summary>
    /// <param name="standId"></param>
    private void LoadGridPanelDetail(string standId)
    {
        StoreDetail.RemoveAll();

        if (standId != "")
        {
            IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
            IQmtCheckStandDetailParams p = new QmtCheckStandDetailParams();
            p.StandId = standId;
            p.DeleteFlag = "0";
            DataSet ds = bQmtCheckStandDetailManager.GetDataByParas(p);
            StoreDetail.DataSource = ds.Tables[0];
            StoreDetail.DataBind();
        }

    }


    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        if (HiddenNorthMaterCode.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择胶料").Show();
            return;
        }

        QueryGridPanelMaster();
    }


    /// <summary>
    /// 加载检验标准列表
    /// </summary>
    /// <param name="materCode"></param>
    private void QueryGridPanelMaster()
    {
        IQmtCheckStandMasterQueryInfoParams paras = new QmtCheckStandMasterQueryInfoParams();
        string materCode = HiddenNorthMaterCode.Value.ToString();
        paras.MaterCode = materCode;
        string standCode = ComboBoxNorthStandCode.Value.ToString();
        if (standCode != "")
        {
            paras.StandCode = standCode;
        }
        string standVisionStat = ComboBoxNorthStandVisionStat.Value.ToString();
        if (standVisionStat != "")
        {
            paras.StandVisionStat = standVisionStat;
        }

        StoreDetail.RemoveAll();
        StoreMaster.RemoveAll();

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        //DataSet ds = bQmtCheckStandMasterManager.GetCheckStandInfoByParas(paras);
        DataSet ds = GetCheckStandInfoByParas(paras);
        GroupingMaster.ExpandAll();
        StoreMaster.DataSource = ds.Tables[0];
        StoreMaster.DataBind();

    }


    public DataSet GetCheckStandInfoByParas(IQmtCheckStandMasterQueryInfoParams queryParams)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT * FROM (");
        sb.AppendLine("    SELECT A.MaterialName MaterName,  SS.ItemName ,case when A.bm is null then ( C.MaterialName + SA.ItemName ) else '' end  GroupName, B.*");
        sb.AppendLine("     , CASE B.StandVisionStat WHEN '0' THEN '已停用' WHEN '1' THEN '已启用' WHEN '2' THEN '已作废' WHEN '3' THEN '未提交' WHEN '4' THEN '已提交' WHEN '5' THEN '已退回' ELSE '' END AS StandVisionStatExp");
        sb.AppendLine("     , CASE WHEN LastModifyTime > ISNULL(LastSubmitTime, '2000-01-01') THEN CASE WHEN LastModifyTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastModifyTime ELSE LastAuditTime END ELSE CASE WHEN LastSubmitTime > ISNULL(LastAuditTime, '2000-01-01') THEN LastSubmitTime ELSE LastAuditTime END END LastOperateTime");
        sb.AppendLine("     , G.StandTypeName");
        sb.AppendLine("     , D.UserName LastModifyUserName");
        sb.AppendLine("     , E.UserName LastSubmitUserName");
        sb.AppendLine("     , F.UserName LastAuditUserName");
        sb.AppendLine(@"    FROM ( select distinct RecipeMaterialName as MaterialName, RecipeMaterialCode, a.RecipeType,b.MaterCode BM,
ISNULL(b.MaterCode, left(dbo.FuncGetGroupName(GroupName),13)) M
,ISNULL(b.PmtType,SUBSTRING( dbo.FuncGetGroupName(GroupName),14,LEN(dbo.FuncGetGroupName(GroupName)) -13)) T 
from dbo.PmtRecipe a
left join QmtCheckStandMaster b on a.RecipeMaterialCode = b.MaterCode
and a.recipetype=b.PmtType and b.StandVisionStat='1'
left join QmtCheckDT c on a.RecipeMaterialCode=c.MaterialID 
and a.recipetype =c.RecipeType
where RecipeState = '1'
and ISNULL(b.MaterCode, left(dbo.FuncGetGroupName(GroupName),13))  <>'1234567890123'

");
     
     
        sb.AppendFormat("    and   a.RecipeMaterialCode = '{0}'", queryParams.MaterCode);
   
        sb.AppendLine("    ) A ");
        sb.AppendLine("    INNER JOIN QmtCheckStandMaster B ON A.M = B.MaterCode and A.T=b.PmtType AND B.DeleteFlag = '0'");
      
        sb.AppendLine("     LEFT JOIN BasMaterial C ON A.M = C.MaterialCode AND C.DeleteFlag = '0'");
        sb.AppendLine("     LEFT JOIN BasUser D ON B.WorkerBarcode = D.WorkBarcode AND D.DeleteFlag = '0'");
        sb.AppendLine("     LEFT JOIN BasUser E ON B.LastSubmitUser = E.WorkBarcode AND E.DeleteFlag = '0'");
        sb.AppendLine("     LEFT JOIN BasUser F ON B.LastAuditUser = F.WorkBarcode AND F.DeleteFlag = '0'");
        sb.AppendLine("     LEFT JOIN QmtCheckStandType G ON B.StandCode = G.ObjID");
        //sb.AppendLine("     LEFT JOIN SysCode SS ON B.PmtType = SS.ItemCode AND SS.TypeID = 'PmtType'");
        sb.AppendLine("     LEFT JOIN SysCode SS ON A.RecipeType = SS.ItemCode AND SS.TypeID = 'PmtType'");
        sb.AppendLine("     LEFT JOIN SysCode SA ON A.T = SA.ItemCode AND SA.TypeID = 'PmtType'");
        sb.AppendLine("    WHERE 1 = 1");

        if (queryParams.StandCode != null && queryParams.StandCode != "")
        {
            sb.AppendFormat("    AND B.StandCode = {0}", queryParams.StandCode);
            sb.AppendLine();
        }

        if (!string.IsNullOrEmpty(cboType.Value.ToString()) && (!"---请选择---".Equals(cboType.SelectedItem.Text)))
        {
            sb.AppendLine(" AND SS.ItemCode='" + cboType.Value.ToString() + "'");
            sb.AppendLine();
        }

        if (queryParams.StandVisionStat != null && queryParams.StandVisionStat != "")
        {
            sb.AppendFormat("    AND B.StandVisionStat = {0}", queryParams.StandVisionStat);
            sb.AppendLine();
        }
        sb.AppendLine(") A");

        sb.AppendLine("    ORDER BY A.MaterName, A.LastOperateTime DESC, A.StandVision DESC, A.StandVisionStat");
        //txtRecipeName.Text = sb.ToString();
        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        return bQmtCheckStandMasterManager.GetBySql(sb.ToString()).ToDataSet();
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

    /// <summary>
    /// 返回筛选出的节点
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

    /// <summary>
    /// 返回下级节点
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
        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.MaterialCode == materCode
            , BasMaterial._.DeleteFlag.Asc);
        if (mBasMaterialList.Count > 0)
        {
            TriggerFieldNorthMaterName.SetValue(mBasMaterialList[0].MaterialName);
            HiddenNorthMaterCode.SetValue(materCode);
        }
        LoadGridPanelMaster(materCode);

    }
    #endregion 选择

    /// <summary>
    /// 加载检验标准列表
    /// </summary>
    /// <param name="materCode"></param>
    private void LoadGridPanelMaster(string materCode)
    {
        StoreDetail.RemoveAll();
        StoreMaster.RemoveAll();
        if (materCode != "")
        {
            IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
            IQmtCheckStandMasterQueryInfoParams p = new QmtCheckStandMasterQueryInfoParams();
            p.MaterCode = materCode;
            DataSet ds = bQmtCheckStandMasterManager.GetCheckStandInfoByParas(p);
            GroupingMaster.ExpandAll();
            StoreMaster.DataSource = ds.Tables[0];
            StoreMaster.DataBind();
        }

    }

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



}