using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Ext.Net;
using Mesnac.Entity;
using System.Data;
using Mesnac.Data.Components;

public partial class Manager_ProducingPlan_EquipAbility_EquipAbilityt : Mesnac.Web.UI.Page
{
    #region 属性注入

    IBasEquipManager baseEquipManager = new BasEquipManager();
    IPmtEquipAbilityManager pmtEquipAbilityManager = new PmtEquipAbilityManager();
    MessageBox mss = new MessageBox();//提示信息
    #endregion


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExcel" };
            汇总 = new SysPageAction() { ActionID = 3, ActionName = "btnSum" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 汇总 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if  (!X.IsAjaxRequest&&!IsPostBack)
        {
            InitTreeDept();
            this.txtStratPlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtEndPlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #region 树
    /// <summary>
    /// 初始化机台列表树
    /// </summary>
    private void InitTreeDept()
    {
        treeEquip.GetRootNode().RemoveAll();
        treeEquip.GetRootNode().AppendChild(getTreeNodeByDelLevel());
    }

    /// <summary>
    /// 获取机台树的算法
    /// </summary>
    /// <param name="dep_num"></param>
    /// <returns></returns>
    private Node getTreeNodeByDelLevel()
    {
        Node node = new Node();
        node.NodeID = "0";
        node.Text = "机台分组";
        node.Expanded = true;
        Dictionary<string, string> depChildFristList = new Dictionary<string, string>();
        var query = baseEquipManager.GetListByWhereAndOrder(BasEquip._.EquipType < "03" && BasEquip._.DeleteFlag == 0, BasEquip._.EquipGroup.Asc).GroupBy(pet => pet.EquipGroup).Where(pet => !string.IsNullOrEmpty(pet.Key));
        foreach (var info in query)
        {
            Node childNode = new Node();
            childNode.NodeID = info.Key;
            childNode.Text = info.Key;
            childNode.Expanded = false;
            var child = baseEquipManager.GetListByWhereAndOrder(BasEquip._.EquipGroup == info.Key & BasEquip._.EquipType < "03" && BasEquip._.DeleteFlag == 0, BasEquip._.EquipCode.Asc);
            foreach (var item in child)
            {
                Node nodeLeaf = new Node();
                nodeLeaf.Text = item.EquipName;
                nodeLeaf.Qtip = item.EquipCode;
                nodeLeaf.Leaf = true;
                childNode.Children.Add(nodeLeaf);
            }
            node.Children.Add(childNode);
        }
        return node;
    }
    #endregion


    /// <summary>
    /// 相应点击机台树事件
    /// </summary>
    /// <param name="equipCode"></param>
    [DirectMethod]
    public void LoadGridData(string equipCode)
    {
        #region 绑定信息
        hidden_select_equip_code .Text= equipCode;
        pageToolBar.DoRefresh();
        //获取对应机台的物料配方 删除标识未添加
        this.pnlList.Title = baseEquipManager.GetListByWhere(BasEquip._.EquipCode == equipCode)[0].EquipName;
        #endregion
    }

    #region 分页
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtEquipAbility> pageParams = new PageResult<PmtEquipAbility>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        //pageParams.Orderfld = " EquipCode ASC ";

        PageResult<PmtEquipAbility> lst = GetPageResultData(pageParams);

        DataTable data = lst.DataSet.Tables[0];
        this.Session["ExportEquipAbilityt"] = lst;
        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PmtEquipAbility> GetPageResultData(PageResult<PmtEquipAbility> pageParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        PmtEquipAbilityManager.QueryParams queryParams = new PmtEquipAbilityManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.planStartDate = Convert.ToDateTime(txtStratPlanDate.Text).ToString("yyyy-MM-dd");
        queryParams.planEndDate = Convert.ToDateTime(txtEndPlanDate.Text).ToString("yyyy-MM-dd");
        if (hidden_select_equip_code.Text != null)
        {
            queryParams.equipCode = hidden_select_equip_code.Text.ToString();
        }
        queryParams.statUser = this.UserID;
        return pmtEquipAbilityManager.GetTablePageDataBySql(queryParams);
    }
    #endregion
    #region 打印
    /// <summary>
    /// 打印调用方法
    /// sunyj 2013年3月29日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PmtEquipAbility> pageParams = new PageResult<PmtEquipAbility>();
        pageParams.PageSize = -100;
        PageResult<PmtEquipAbility> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "设备生产能力");
    }
    #endregion

    [DirectMethod]
    public void SumEquipAbilityt()
    {
        string startDate = Convert.ToDateTime(this.txtStratPlanDate.Text).ToString("yyyy-MM-dd") ;
        string endDate = Convert.ToDateTime(this.txtEndPlanDate.Text).ToString("yyyy-MM-dd");
        string shiftID = "0";
        pmtEquipAbilityManager.ExecProcEquipAbility(startDate, endDate, shiftID, this.UserID);
    }
}