using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Text;

/// <summary>
/// Manager_Technology_Manage_MaterialRecipe 实现类
/// 孙本强 @ 2013-04-03 13:06:50
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Manage_MaterialRecipe : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "txtRubberName" };
            新增工艺配方 = new SysPageAction() { ActionID = 2, ActionName = "btnAddDownload" };
            配方删除 = new SysPageAction() { ActionID = 3, ActionName = "btnDeletePmtRecipe" };
            配方另存 = new SysPageAction() { ActionID = 4, ActionName = "btnCopyPmtRecipe" };
            显示全部版本 = new SysPageAction() { ActionID = 5, ActionName = "btnShowEnablePmt,btnShowAllPmt" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 新增工艺配方 { get; private set; } //必须为 public
        public SysPageAction 配方删除 { get; private set; } //必须为 public
        public SysPageAction 配方另存 { get; private set; } //必须为 public
        public SysPageAction 显示全部版本 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:51
    /// </summary>
    private ISysCodeManager sysCodeManager = new SysCodeManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:51
    /// </summary>
    private IPmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:51
    /// </summary>
    private IBasMaterialManager basMaterialManager = new BasMaterialManager();
    /// <summary>
    /// 
    /// 袁洋 @ 2013年11月23日11:44:09
    /// </summary>
    private ISysRubPowerUserManager sysRubPowerManager = new SysRubPowerUserManager();
    /// <summary>
    /// 
    /// 袁洋 @ 2014年3月20日10:20:18
    /// </summary>
    private IBasUserManager userManager = new BasUserManager();
    private IBasEquipManager equipManager = new BasEquipManager();
    #endregion

    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:51
    /// </summary>
    /// <remarks></remarks>
    private enum MaterialType
    {
        /// <summary>
        /// 
        /// </summary>
        原材料 = 1,
        /// <summary>
        /// 
        /// </summary>
        小料 = 2,
        /// <summary>
        /// 
        /// </summary>
        塑炼胶 = 3,
        /// <summary>
        /// 
        /// </summary>
        母炼胶 = 4,
        /// <summary>
        /// 
        /// </summary>
        终炼胶 = 5,
        /// <summary>
        /// 
        /// </summary>
        其他 = 100,
    }

    /// <summary>
    /// Gets the ICO.
    /// 孙本强 @ 2013-04-03 13:06:52
    /// </summary>
    /// <param name="materialType">Type of the material.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private Icon GetICO(int materialType)
    {
        Icon Result = Icon.Package;
        MaterialType m = MaterialType.其他;
        try
        {
            m = (MaterialType)materialType;
        }
        catch { }
        switch (m)
        {
            case MaterialType.原材料:
                Result = Icon.PackageDown;
                break;
            case MaterialType.小料:
                Result = Icon.PackageGo;
                break;
            case MaterialType.塑炼胶:
                Result = Icon.PackageGreen;
                break;
            case MaterialType.母炼胶:
                Result = Icon.PackageIn;
                break;
            case MaterialType.终炼胶:
                Result = Icon.PackageSe;
                break;
            default:
                Result = Icon.Package;
                break;
        }
        return Result;
    }


    #region 页面初始化
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:06:52
    /// </summary>
    private const string constSelectAllText = "---请选择---";
    /// <summary>
    /// Inis the combo box.
    /// 孙本强 @ 2013-04-03 13:06:52
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
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:06:52
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
        String sql = "";
        #region 配方类型
        //where = new WhereClip();
        //order = new OrderByClip();
        ////order = SysCode._.DisplayID;
        //where.And(SysCode._.TypeID == SysCodeManager.SysCodeType.PmtType.ToString());
        //EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhereAndOrder(where, order);
        //IniComboBox(txtPmtRecipeType, lst);

        sql = @"  select * from SysCode 
  where TYPEID='PmtType'
            order by DisplayID";

        DataSet ds = sysCodeManager.GetBySql(sql).ToDataSet();

        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        txtPmtRecipeType.Items.Clear();
        txtPmtRecipeType.Items.Add(allitem);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(dr["ItemName"].ToString(), dr["ItemCode"].ToString());
            txtPmtRecipeType.Items.Add(item);
        }
        if (txtPmtRecipeType.Items.Count > 0)
        {
            txtPmtRecipeType.Text = (txtPmtRecipeType.Items[0].Value);
        }




        #endregion

        #region 配方状态
        where = new WhereClip();
        order = new OrderByClip();
        where.And(SysCode._.TypeID == SysCodeManager.SysCodeType.PmtState.ToString());
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhereAndOrder(where, order);
        IniComboBox(txtPmtRecipeState, lst);
        #endregion
    }
    /// <summary>
    /// Reds the HTML.
    /// 孙本强 @ 2013-04-03 13:06:52
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// Defaults the HTML.
    /// 孙本强 @ 2013-04-03 13:06:53
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion

    #region 查询显示左侧 物料 树
    /// <summary>
    /// Determines whether [is exist in node] [the specified nodes].
    /// 孙本强 @ 2013-04-03 13:06:53
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="m">The m.</param>
    /// <returns><c>true</c> if [is exist in node] [the specified nodes]; otherwise, <c>false</c>.</returns>
    /// <remarks></remarks>
    private bool isExistInNode(NodeCollection nodes, BasMaterial m)
    {
        bool Result = false;
        foreach (Node node in nodes)
        {
            if (node.NodeID.EndsWith(m.MaterialCode))
            {
                return true;
            }
            if (node.Children.Count > 0)
            {
                Result = isExistInNode(node.Children, m);
                if (Result)
                {
                    return true;
                }
            }
        }
        return Result;
    }
    /// <summary>
    /// 树中不存在此节点
    /// 孙本强 @ 2013-04-03 13:06:53
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="m">The m.</param>
    /// <returns><c>true</c> if [is exist in tree] [the specified node]; otherwise, <c>false</c>.</returns>
    /// <remarks></remarks>
    private bool isExistInTree(Node node, BasMaterial m)
    {
        Node root = node;
        while (true)
        {
            if (root.ParentNode != null)
            {
                root = root.ParentNode;
            }
            else
            {
                break;
            }
        }
        return isExistInNode(root.Children, m);
    }
    /// <summary>
    /// 父节点中不存在此节点
    /// 孙本强 @ 2013-04-03 13:06:53
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="m">The m.</param>
    /// <returns><c>true</c> if [is exist in tree] [the specified node]; otherwise, <c>false</c>.</returns>
    /// <remarks></remarks>
    private bool isExistInParentNode(Node node, BasMaterial m)
    {
        Node root = node;
        if (root.NodeID.EndsWith(m.MaterialCode))
        {
            return true;
        }
        while (true)
        {
            if (root.NodeID.EndsWith(m.MaterialCode))
            {
                return true;
            }
            if (root.ParentNode != null)
            {
                root = root.ParentNode;
            }
            else
            {
                break;
            }
        }
        return false;
    }
    /// <summary>
    /// Gets the ICO file.
    /// 孙本强 @ 2013-04-03 13:06:53
    /// </summary>
    /// <param name="materialType">Type of the material.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetICOFile(int materialType)
    {
        string Result = "../../../resources/icons/Recipe/";
        MaterialType m = MaterialType.其他;
        try
        {
            m = (MaterialType)materialType;
        }
        catch { }
        switch (m)
        {
            case MaterialType.原材料:
                Result = Result + "返回胶和原材料.ico";
                break;
            case MaterialType.小料:
                Result = Result + "硫磺.ico";
                break;
            case MaterialType.塑炼胶:
                Result = Result + "塑炼胶.ico";
                break;
            case MaterialType.母炼胶:
                Result = Result + "无S胶.ico";
                break;
            case MaterialType.终炼胶:
                Result = Result + "加S胶.ico";
                break;
            default:
                Result = Result + "细料.ico";
                break;
        }
        return Result;

    }
    int iNoSameID = 0;
    private string NoSameID()
    {
        iNoSameID++;
        return iNoSameID.ToString();
    }
    private string getNodeText(BasMaterial m)
    {
        return m.MaterialName + "[" + m.MaterialCode + "]";
    }
    /// <summary>
    /// Inis the material recipe tree.
    /// 孙本强 @ 2013-04-03 13:06:54
    /// </summary>
    /// <param name="parnode">The parnode.</param>
    /// <param name="source">The source.</param>
    /// <param name="iLevel">The i level.</param>
    /// <param name="queryParams">查询参数</param>
    /// <remarks></remarks>
    private void IniMaterialRecipeTree(Node parnode, BasMaterial source, PmtRecipeManager.QueryParams queryParams)
    {
        queryParams.MaterialCode = source.MaterialCode;
        EntityArrayList<BasMaterial> lst = this.pmtRecipeManager.GetBasMaterial(queryParams);
        int iChildrenCount = 0;
        foreach (BasMaterial m in lst)
        {
            if (isExistInParentNode(parnode, m))
            {
                continue;
            }
            iChildrenCount++;
            Node node = new Node();
            node.NodeID = NoSameID().ToString() + "@" + source.MaterialCode + "=" + m.MaterialCode.ToString();
            node.Text = getNodeText(m);
            if (m.MajorTypeID != null)
            {
                node.IconFile = GetICOFile((int)m.MajorTypeID);
            }
            //node.Icon = GetICO((int)m.MajorTypeID);
            parnode.Children.Add(node);
            IniMaterialRecipeTree(node, m, queryParams);
        }
        parnode.Leaf = iChildrenCount == 0;
        parnode.Expanded = iChildrenCount > 0;

    }
    /// <summary>
    /// Trees the panel bind data.
    /// 孙本强 @ 2013-04-03 13:06:54
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object TreePanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //if (this._.查看.SeqIdx == 0)
        //{
        //    return null;
        //}
        if (string.IsNullOrWhiteSpace(txtRubberName.Text))
        {
            treePanelUser.Title = string.Empty;
            return string.Empty;
        }

        string rubbercode = hiddenRubberCode.Text.Trim();
        if (string.IsNullOrWhiteSpace(rubbercode))
        {
            treePanelUser.Title = string.Empty;
            txtRubberName.Text = string.Empty;
            return string.Empty;
        }
        PmtRecipeManager.QueryParams queryParams = new PmtRecipeManager.QueryParams();
        queryParams.MaterialCode = rubbercode;
        queryParams.RecipeType = txtPmtRecipeType.Text.Replace(constSelectAllText, "");
        queryParams.RecipeState = txtPmtRecipeState.Text.Replace(constSelectAllText, "");
        treePanelUser.Title = "胶料名称：" + txtRubberName.Text + "[" + rubbercode + "]";

        #region 更新树
        NodeCollection nodes = new Ext.Net.NodeCollection();
        Node root = new Node();
        root.NodeID = NoSameID().ToString() + "@" + rubbercode + "=" + rubbercode;
        root.Text = txtRubberName.Text;
        root.Icon = Icon.Folder;
        nodes.Add(root);
        EntityArrayList<BasMaterial> lst = GetBasMaterial(queryParams);//this.pmtRecipeManager.
        //X.Js.Alert(lst.Count.ToString()); return null;
        int maxmajor = 0;
        foreach (BasMaterial m in lst)
        {
            if (maxmajor < (int)m.MajorTypeID)
            {
                maxmajor = (int)m.MajorTypeID;
                if (maxmajor >= 5)
                {
                    maxmajor = 5;
                }
            }
        }
        int iChildrenCount = 0;
        foreach (BasMaterial m in lst)
        {
            if (isExistInTree(root, m))
            {
                continue;
            }
            iChildrenCount++;
            Node node = new Node();
            node.NodeID = NoSameID().ToString() + "@" + rubbercode + "=" + m.MaterialCode.ToString();
            node.Text = getNodeText(m);
            //node.Icon = GetICO((int)m.MajorTypeID);
            node.IconFile = GetICOFile((int)m.MajorTypeID);

            root.Children.Add(node);
            if ((int)m.MajorTypeID < maxmajor)
            {
                node.Leaf = true;
            }
            else
            {
                IniMaterialRecipeTree(node, m, queryParams);
            }
            //   IniMaterialRecipeTree(node, m, queryParams);
        }
        root.Leaf = iChildrenCount == 0;
        root.Expanded = iChildrenCount > 0;
        return nodes;
        #endregion
    }


    public EntityArrayList<BasMaterial> GetBasMaterial(PmtRecipeManager.QueryParams queryParams)
    {
        StringBuilder sqlstr = new StringBuilder();


        sqlstr.AppendLine(@"SELECT DISTINCT t2.* FROM dbo.PmtRecipe t0 ");
        if (queryParams.MaterialCode.Length == 13)
        {
            sqlstr.AppendLine(@"
                                INNER JOIN dbo.PmtRecipeWeight t1 ON t0.ObjID=t1.RecipeObjID
                                INNER JOIN dbo.BasMaterial t2  ON t1.MaterialCode=t2.MaterialCode
                                WHERE t1.RecipeMaterialCode='" + queryParams.MaterialCode + "'");
        }
        else
        {
            sqlstr.AppendLine(@"
                                INNER JOIN dbo.BasMaterial t2 ON t0.RecipeMaterialCode=t2.MaterialCode
                                WHERE t2.RubCode='" + queryParams.MaterialCode + "'");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.RecipeType))
        {
            sqlstr.AppendLine(" AND t0.RecipeType=" + queryParams.RecipeType + "");
        }
        if (!string.IsNullOrWhiteSpace(queryParams.RecipeState))
        {
            sqlstr.AppendLine(" AND t0.RecipeState='" + queryParams.RecipeState + "'");
        }
        sqlstr.AppendLine("  ORDER BY t2.MaterialCode DESC");
        return pmtRecipeManager.GetBySql(sqlstr.ToString()).ToArrayList<BasMaterial>();
    }
    #endregion

    #region 查询显示右侧
    /// <summary>
    /// Refreshes the grid info.
    /// 孙本强 @ 2013-04-03 13:06:54
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void RefreshGridInfo(object sender, EventArgs e)
    {
        TreePanel sendertree = sender as TreePanel;
        List<SubmittedNode> nodes = sendertree.SelectedNodes;
        string nodeid = "";
        if (nodes.Count > 0)
        {
            nodeid = nodes[0].NodeID;
        }
        string materialid = string.Empty;
        if (nodeid.Contains("="))
        {
            materialid = nodeid.Substring(nodeid.IndexOf("=") + 1);
        }
        if ((string.IsNullOrWhiteSpace(materialid)) && (materialid.Length != 13))
        {
            hiddenMaterialID.Text = string.Empty;
            gridPanelCenter.Title = "机台配方基本信息";
            X.Js.Call("gridPanelRefresh"); ;
            return;
        }
        EntityArrayList<BasMaterial> lst = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == materialid);
        if (lst.Count == 0)
        {
            hiddenMaterialID.Text = string.Empty;
            gridPanelCenter.Title = "机台配方基本信息";
            X.Js.Call("gridPanelRefresh"); ;
            return;
        }
        BasMaterial m = lst[0];
        hiddenMaterialID.Text = m.MaterialCode;
        gridPanelCenter.Title = "机台配方基本信息[" + m.MaterialName + "]";
        X.Js.Call("gridPanelRefresh"); ;
    }

    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:06:55
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        DataTable data = new DataTable();
        int total = 0;
        if (string.IsNullOrWhiteSpace(hiddenMaterialID.Text))
        {
            return new { data, total };
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PmtRecipeManager.QueryParams queryParams = new PmtRecipeManager.QueryParams();
        queryParams.PageParams.PageIndex = prms.Page;
        queryParams.PageParams.PageSize = prms.Limit;
        queryParams.PageParams.Orderfld = "RecipeEquipCode";
        queryParams.MaterialCode = hiddenMaterialID.Text.Trim();
        if (isShowAllPmt.Text == "1")
        {
            queryParams.RecipeState = "1";
        }
        else
        {
            queryParams.RecipeState = txtPmtRecipeState.Text.Replace(constSelectAllText, "");
        }
        queryParams.RecipeType = txtPmtRecipeType.Text.Replace(constSelectAllText, "");
        PageResult<PmtRecipe> lst = GetTablePageDataBySql(queryParams);//pmtRecipeManager.
        data = lst.DataSet.Tables[0];

        total = lst.RecordCount;
        return new { data, total };
    }

    public PageResult<PmtRecipe> GetTablePageDataBySql(PmtRecipeManager.QueryParams queryParams)
    {
        PageResult<PmtRecipe> pageParams = queryParams.PageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.Append(@" SELECT t1.*,t2.EquipName,t3.MaterialName,t4.ItemName AS RecipeTypeName,t8.USER_NAME opername,t9.USER_NAME auditname,
                            t5.ItemName AS RecipeStateName,
                            t6.ItemName AS AuditFlagName,t7.ItemName AS JieDuanName

                            FROM  dbo.PmtRecipe t1
                            INNER JOIN dbo.BasEquip t2 ON t1.RecipeEquipCode=t2.EquipCode
                            INNER JOIN dbo.BasMaterial t3 ON t1.RecipeMaterialCode = t3.MaterialCode
                            LEFT JOIN dbo.SysCode t4 ON t4.ItemCode=t1.RecipeType AND t4.TypeID='PmtType'
                            LEFT JOIN dbo.SysCode t5 ON t5.ItemCode=t1.RecipeState AND t5.TypeID='PmtState'
                            LEFT JOIN dbo.SysCode t6 ON t6.ItemCode=t1.AuditFlag AND t6.TypeID='Audit'
    LEFT JOIN dbo.SysCode t7 ON t7.ItemCode=t1.jieduan AND t7.TypeID='JieDuanCode'
	left join SYS_USER t8 on t8.USER_ID=t1.OperCode
	left join SYS_USER t9 on t9.USER_ID=t1.AuditUser
                            WHERE 1=1 ");
        if (!string.IsNullOrEmpty(queryParams.AuditFlag))
        {
            sqlstr.AppendLine("AND t1.AuditFlag= '" + queryParams.AuditFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.RubCode))
        {
            sqlstr.AppendLine("AND t3.RubCode= '" + queryParams.RubCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.BeginTime))
        {
            sqlstr.AppendLine("AND t1.RecipeModifyTime >= '" + queryParams.BeginTime + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.EndTime))
        {
            sqlstr.AppendLine("AND t1.RecipeModifyTime <= '" + queryParams.EndTime + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.EquipType))
        {
            sqlstr.AppendLine("AND t2.EquipType = '" + queryParams.EquipType + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.MaterialCode))
        {
            sqlstr.AppendLine("AND t1.RecipeMaterialCode = '" + queryParams.MaterialCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.RecipeEquipCode))
        {
            sqlstr.AppendLine("AND t1.RecipeEquipCode = '" + queryParams.RecipeEquipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.RecipeEquipName))
        {
            sqlstr.AppendLine("AND t2.EquipName LIKE '%" + queryParams.RecipeEquipName + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.RecipeMaterialName))
        {
            sqlstr.AppendLine("AND t3.MaterialName LIKE '%" + queryParams.RecipeMaterialName + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.RecipeVersionID))
        {
            sqlstr.AppendLine("AND t1.RecipeVersionID = '" + queryParams.RecipeVersionID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.RecipeType))
        {
            sqlstr.AppendLine("AND t1.RecipeType = '" + queryParams.RecipeType + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.RecipeState))
        {
            sqlstr.AppendLine("AND t1.RecipeState = '" + queryParams.RecipeState + "'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = pmtRecipeManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return pmtRecipeManager.GetPageDataBySql(pageParams);
        }
    }
    #endregion


    /// <summary>
    /// Deletes the PMT recipe.
    /// 孙本强 @ 2013-04-03 13:06:55
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string DeletePmtRecipe(string recipe)
    {
        string msg = string.Empty;
        if (string.IsNullOrWhiteSpace(recipe))
        {
            return "请选择配方进行删除！";
        }
        else
        {
            msg = pmtRecipeManager.DeletePmtRecipe(recipe, this.UserID);
        }
        return msg;
    }


    private bool ValidateRubPowerUser(string userId, string rubCode)
    {
        EntityArrayList<SysRubPowerUser> rubPowerUserList = sysRubPowerManager.GetListByWhere(SysRubPowerUser._.RubCode == rubCode 
            && SysRubPowerUser._.WorkBarcode == userId);
        if (rubPowerUserList.Count == 0 )
        {
            return false;
        }
        return true;
    }

    #region 判定人员与配方的权限
    /// <summary>
    /// yuany 2014年3月20日10:20:30
    /// 一部分是在人员信息中维护的车间信息对应配方的机台信息
    /// 一部分是在胶料维护中对应的人员姓名
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    [DirectMethod]
    public string IsHaveWorkShopPower(string recipe)
    {
      //  int iswork=0;
        return "SUCCESS";
        //try
        //{
        //    PmtRecipe pr = pmtRecipeManager.GetById(recipe);
        //    BasMaterial material = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pr.RecipeMaterialCode)[0];
        //    BasEquip equip = equipManager.GetListByWhere(BasEquip._.EquipCode == pr.RecipeEquipCode)[0];
        //    BasUser user = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0];
        //    string[] str = GetWorkShopIdS(user.HRCode).Split('+');
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        if (str[i].Contains(equip.WorkShopCode.ToString()))
        //        {
        //            iswork = 1;
        //            break;
        //        }
              
        //    }
        //    if (iswork==1)//   || ValidateRubPowerUser(this.UserID, material.RubCode) 2014年4月19日 胶料对应人员权限判定  因现场没有维护进行了去除
        //    {
        //        return "SUCCESS";
        //    }
        //    return "当前用户没有该配方的查看权限！";
        //}
        //catch (Exception)
        //{

        //    return "数据异常请联系管理员！";
        //}
    }
    #endregion
    #region 获取配方权限车间
    /// <summary>
    /// yuxp
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    public string GetWorkShopIdS(string userid)
    {
        SysUserActionManager sysUserActionManager = new SysUserActionManager();
        SysPageActionManager sysPageActionManager = new SysPageActionManager();
        string result = "0";
        EntityArrayList<SysUserAction> sysUserAS = sysUserActionManager.GetListByWhere(SysUserAction._.UserCode == userid);
        foreach (SysUserAction sysUserA in sysUserAS)
        {
            SysPageAction sysPage = sysPageActionManager.GetListByWhere(SysPageAction._.ObjID == sysUserA.ActionID)[0];
            if (sysPage.Remark == "车间")
            {
                result = result + "+" + sysPage.ActionName;
            }
        }
        return result;
    }
    #endregion
}