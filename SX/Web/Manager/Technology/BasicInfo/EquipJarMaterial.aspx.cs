using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using System.Text;


/// <summary>
/// Manager_Technology_BasicInfo_EquipJarMaterial 实现类
/// 孙本强 @ 2013-04-03 13:04:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_BasicInfo_EquipJarMaterial : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 1 };
            修改 = new SysPageAction() { ActionID = 2, ActionName = "Edit" };
            清空 = new SysPageAction() { ActionID = 3, ActionName = "Delete" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 清空 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:21
    /// </summary>
    private ISysCodeManager sysCodeManager = new SysCodeManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:21
    /// </summary>
    private IBasEquipManager basEquipManager = new BasEquipManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:21
    /// </summary>
    private IBasEquipTypeManager basEquipTypeManager = new BasEquipTypeManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:21
    /// </summary>
    private IPmtEquipJarStoreManager pmtEquipJarStoreManager = new PmtEquipJarStoreManager();


    /// <summary>
    /// yuany @ 2014-04-02 09:02:16
    /// </summary>
    private IPmtEquipJarStoreLogManager logManager = new PmtEquipJarStoreLogManager();
    #endregion

    #region 私有常量定义
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:21
    /// </summary>
    private readonly string constEquipTypeStarWith = "EquipType=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:21
    /// </summary>
    private readonly string constEquipCodeStarWith = "EquipCode=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:22
    /// </summary>
    private readonly string contEquipJarTypeStarWith = "EquipJarType=";
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:04:22
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        StatusBar1.Text = "";
        StatusBar1.Html = "";
        if (!X.IsAjaxRequest)
        {
            EntityArrayList<BasEquipType> lst = pmtEquipJarStoreManager.GetEquipTypeHaveJar();
            foreach (BasEquipType menu in lst)
            {
                Node node = new Node();
                node.NodeID = this.constEquipTypeStarWith + menu.ObjID.ToString();
                node.Text = menu.EquipTypeName;
                treePanel1.GetRootNode().AppendChild(node);
            }
            treePanel1.GetRootNode().Expand(false);
            StatusBar1.Html = DefaultHtml("数据查询成功！一级菜单数为：" + lst.Count.ToString());
        }
    }
    /// <summary>
    /// Reds the HTML.
    /// 孙本强 @ 2013-04-03 13:04:22
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
    /// 孙本强 @ 2013-04-03 13:04:22
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion

    #region 查询显示左侧 料仓 树
    /// <summary>
    /// 查询显示左侧 料仓 树
    /// 孙本强 @ 2013-04-03 13:04:23
    /// </summary>
    /// <param name="equipType">Type of the equip.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string treePanelBeforeLoad(string equipType)
    {
        if (equipType.StartsWith(this.constEquipTypeStarWith))
        {
            equipType = equipType.Substring(this.constEquipTypeStarWith.Length);
            EntityArrayList<BasEquip> lst = pmtEquipJarStoreManager.GetEquipHaveJar(equipType);
            NodeCollection nodes = new Ext.Net.NodeCollection();
            foreach (BasEquip menu in lst)
            {
                Node node = new Node();
                node.NodeID = this.constEquipCodeStarWith + menu.EquipCode.ToString();
                node.Text = menu.EquipName;
                node.Leaf = false;
                nodes.Add(node);
            }
            if (nodes.Count == 0)
            {
                Node node = new Node();
                node.NodeID = this.constEquipCodeStarWith + "NoHave=" + DateTime.Now.ToString("yyMMddHHmmss");
                node.Text = "无信息";
                node.Leaf = true;
                nodes.Add(node);
            }
            return nodes.ToJson();
        }
        else
        {
            string equipCode = equipType.Substring(this.constEquipCodeStarWith.Length);
            //pmtEquipJarStoreManager.IniPmtEquipJarStoreByCount(equipCode);
            EntityArrayList<SysCode> lst = pmtEquipJarStoreManager.GetEquipJarTypeHaveJar(equipCode);
            NodeCollection nodes = new Ext.Net.NodeCollection();
            foreach (SysCode menu in lst)
            {
                Node node = new Node();
                node.NodeID = this.contEquipJarTypeStarWith + equipCode + "|" + menu.ItemCode.ToString();
                node.Text = menu.ItemName;
                node.Leaf = true;
                nodes.Add(node);
            }
            if (nodes.Count == 0)
            {
                Node node = new Node();
                node.NodeID = this.contEquipJarTypeStarWith + "NoHave=" + DateTime.Now.ToString("yyMMddHHmmss");
                node.Text = "无信息";
                node.Leaf = true;
                nodes.Add(node);
            }
            return nodes.ToJson();
        }
    }
    #endregion

    #region 查询显示右侧
    /// <summary>
    /// Refreshes the actin user grid.
    /// 孙本强 @ 2013-04-03 13:04:23
    /// </summary>
    /// <param name="equipid">The equipid.</param>
    /// <remarks></remarks>
    private void RefreshActinUserGrid(string equipid)
    {
        if (equipid.Length == 0)
        {
            return;
        }
        if (!equipid.StartsWith(this.contEquipJarTypeStarWith))
        {
            return;
        }
        txtEquipID.Text = string.Empty;
        txtJarTypeID.Text = string.Empty;
        txtEquipJarType.Text = string.Empty;
        txtEquipCode.Text = string.Empty;
        txtEquipName.Text = string.Empty;
        txtJarName.Text = string.Empty;
        try
        {
            equipid = equipid.Substring(this.contEquipJarTypeStarWith.Length);
            string[] ss = equipid.Split('|');
            if (ss.Length < 2)
            {
                return;
            }
            BasEquip equip = basEquipManager.GetListByWhere(BasEquip._.DeleteFlag == 0 && BasEquip._.EquipCode == ss[0])[0];
            BasEquipType equipType = basEquipTypeManager.GetListByWhere(BasEquipType._.ObjID == equip.EquipType)[0];
            SysCode equipJarType = sysCodeManager.GetListByWhere(SysCode._.TypeID == SysCodeManager.SysCodeType.EquipJar.ToString() && SysCode._.ItemCode == ss[1])[0];

            txtEquipID.Text = equip.EquipCode.ToString();
            txtJarTypeID.Text = ss[1];
            txtEquipJarType.Text = equipType.EquipTypeName;
            txtEquipCode.Text = equip.EquipCode;
            txtEquipName.Text = equip.EquipName;
            txtJarName.Text = equipJarType.ItemName;
            txtMaterialMinorID.Text = "";
            txtMaterialMinorName.Text = "";
        }
        catch
        {
        }
        finally
        {
            X.Js.Call("gridPanelRefresh()");
        }
    }
    /// <summary>
    /// Binds the grid panel data.
    /// 孙本强 @ 2013-04-03 13:04:23
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void BindGridPanelData(object sender, EventArgs e)
    {
        TreePanel sendertree = sender as TreePanel;
        List<SubmittedNode> nodes = sendertree.SelectedNodes;
        string equipid = "";
        if (nodes.Count > 0)
        {
            equipid = nodes[0].NodeID;
        }
        RefreshActinUserGrid(equipid);
    }
    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:04:23
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
        string equipCode = txtEquipID.Text;
        string jarTypeID = txtJarTypeID.Text;
        if (equipCode.Length == 0)
        {
            return new { data, total };
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PmtEquipJarStoreManager.QueryParams queryParams = new PmtEquipJarStoreManager.QueryParams();
        queryParams.PageParams.PageIndex = prms.Page;
        queryParams.PageParams.PageSize = prms.Limit;
        queryParams.PageParams.Orderfld = "JarNum";
        queryParams.EquipCode = equipCode;
        queryParams.JarType = jarTypeID;
        PageResult<PmtEquipJarStore> lst = GetTablePageDataBySql(queryParams);// pmtEquipJarStoreManager.
        data = lst.DataSet.Tables[0];

        total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 取消
    /// <summary>
    /// Closes the windows.
    /// 孙本强 @ 2013-04-03 13:04:24
    /// </summary>
    /// <remarks></remarks>
    private void closeWindows()
    {
        this.winModify.Close();
    }
    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// 孙本强 @ 2013-04-03 13:04:24
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        closeWindows();
    }
    #endregion

    #region 修改
    /// <summary>
    /// Commandcolumn_direct_edits the specified objid.
    /// 孙本强 @ 2013-04-03 13:04:24
    /// </summary>
    /// <param name="objid">The objid.</param>
    /// <remarks></remarks>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objid)
    {
        if (this._.修改.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行修改的权限！").Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtJarTypeID.Text.Trim()))
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "请选择料仓类型！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            return;
        }
        if (string.IsNullOrWhiteSpace(objid.Trim()))
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "请选择料仓！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            return;
        }
        winModifytxtJarObjID.Text = objid;
        winModifytxtEquipJarType.Text = txtEquipJarType.Text;
        winModifytxtEquipCode.Text = txtEquipCode.Text;
        winModifytxtEquipName.Text = txtEquipName.Text;
        winModifytxtJarName.Text = txtJarName.Text;


        PmtEquipJarStoreManager.QueryParams queryParams = new PmtEquipJarStoreManager.QueryParams();
        queryParams.PageParams.PageIndex = 1;
        queryParams.PageParams.PageSize = 1;
        queryParams.ObjID = objid;
        PageResult<PmtEquipJarStore> lst = GetTablePageDataBySql(queryParams);
        foreach (DataRow row in lst.DataSet.Tables[0].Rows)
        {
            winModifytxtStorageName.Text = row["StorageName"].ToString();
            winModifytxtStoragePlaceName.Text = row["StoragePlaceName"].ToString();
            winModifytxtWork.Text = row["WorkID"].ToString();
            TextCD.Text = row["Supply"].ToString();
            winModifytxtMaterialName.Text = row["MaterialName"].ToString();
            winMadifycbDeleteFlag.Checked = row["DeleteFlag"].ToString() == "0";

            winModifytxtStorageID.Text = row["StorageID"].ToString();
            winModifytxtStoragePlaceID.Text = row["StoragePlaceID"].ToString();
            winModifytxtMaterialID.Text = row["MaterialCode"].ToString();
            TextJarNum.Text = row["JarNum"].ToString();
            if (string.IsNullOrEmpty(row["SingleWeight"].ToString())) TextWeight.Text = "0";
            else 
                TextWeight.Text = row["SingleWeight"].ToString();
        }
        this.winModify.Show();
    }

    public PageResult<PmtEquipJarStore> GetTablePageDataBySql(PmtEquipJarStoreManager.QueryParams queryParams)
    {
        PageResult<PmtEquipJarStore> pageParams = queryParams.PageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT t1.ObjID,t1.Priority,t1.JarNum,
                                t2.StoragePlaceID,t2.StoragePlaceName,
                                t3.StorageID,t3.StorageName,
                                t5.MaterialCode,t5.MaterialName,
                                t6.ObjID as MinorTypeID,t6.MinorTypeName, 
                                t1.WorkID,t1.SingleWeight,
                                t1.DeleteFlag,t1.SeqIdx,t1.Supply FROM  dbo.PmtEquipJarStore t1
                                LEFT JOIN dbo.BasStoragePlace t2 ON t1.StoragePlaceCode=t2.StoragePlaceID
                                LEFT JOIN dbo.BasStorage t3 ON t2.StorageID=t3.StorageID
                                LEFT JOIN dbo.BasMaterial t5 ON t1.MaterialCode=t5.MaterialCode
                                LEFT JOIN dbo.BasMaterialMinorType t6 ON t5.MinorTypeID=t6.MinorTypeID AND t6.MajorID=t5.MajorTypeID
                                WHERE 1=1 ");
        if (!string.IsNullOrEmpty(queryParams.ObjID))
        {
            sqlstr.AppendLine(" AND t1.ObjID = " + queryParams.ObjID);
        }
        if (!string.IsNullOrEmpty(queryParams.EquipCode))
        {
            sqlstr.AppendLine(" AND t1.EquipCode = '" + queryParams.EquipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.JarType))
        {
            sqlstr.AppendLine(" AND t1.JarType = '" + queryParams.JarType + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
        {
            sqlstr.AppendLine(" AND t1.DeleteFlag = " + queryParams.DeleteFlag);
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = pmtEquipJarStoreManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return pmtEquipJarStoreManager.GetPageDataBySql(pageParams);
        }
    }
  
    /// <summary>
    /// Handles the Click event of the btnModifySave control.
    /// 孙本强 @ 2013-04-03 13:04:24
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "修改成功", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            string storagePlace = winModifytxtStoragePlaceID.Text;
            string material = winModifytxtMaterialID.Text;
            string work = winModifytxtWork.Text;
            string ObjID = winModifytxtJarObjID.Text;

            //if (string.IsNullOrWhiteSpace(storagePlace))
            //{
            //    X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "请选择相应的库位", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(material))
            //{
            //    X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "请选择相应的物料", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            //    return;
            //}

            //PmtEquipJarStoreLog log = new PmtEquipJarStoreLog();
            //PmtEquipJarStore m = pmtEquipJarStoreManager.GetById(ObjID);

            ////日志记录
            //log.EquipCode = m.EquipCode;
            //log.JarType = m.JarType;
            //log.Priority = m.Priority;

            //log.StoragePlaceCodeBefore = m.StoragePlaceCode;
            //log.StoragePlaceCodeAfter = storagePlace;

            //log.MaterialCodeBefore = m.MaterialCode;
            //log.MaterialCodeAfter = material;

            //log.WorkIDBefore = m.WorkID;
            //log.WorkIDAfter = work;

            //log.OperCode = this.UserID;
            //log.OperDate = DateTime.Now;
            //log.OperMethod = "Modify";
            //log.DeleteFlag = "0";
            //logManager.Insert(log);

            //m.ObjID = Convert.ToInt32(ObjID);
            //m.Attach();
            //m.MaterialCode = material;
            //m.StoragePlaceCode = storagePlace;
            //m.WorkID = work;
            //m.DeleteFlag = winMadifycbDeleteFlag.Checked ? "0" : "1";
            //m.Supply = TextCD.Text;
            //m.SingleWeight = int.Parse(TextWeight.Text);



            String sql = "update Pmt_wm set mater_code ='" + material + "' where equip_code ='" + winModifytxtEquipCode.Text + "' and jar_type ='" +
                winModifytxtJarName.Text + "' and ware_num ='" + TextJarNum.Text + "'";
            pmtEquipJarStoreManager.GetBySql(sql).ToDataSet();



            
            closeWindows();
            X.Js.Call("gridPanelRefresh()");
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "修改失败：" + ex.Message, Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return;
        }
    }
    #endregion

    #region 清空
    /// <summary>
    /// 清空信息
    /// 孙本强 @ 2013-04-03 13:04:24
    /// </summary>
    /// <param name="objid">The objid.</param>
    /// <remarks></remarks>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_delete(string objid)
    {
        if (this._.清空.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行清空的权限！").Show();
            return;
        }
        try
        {
            PmtEquipJarStoreLog log = new PmtEquipJarStoreLog();
            PmtEquipJarStore m = pmtEquipJarStoreManager.GetById(objid);

            //日志记录
            log.EquipCode = m.EquipCode;
            log.JarType = m.JarType;
            log.Priority = m.Priority;

            log.StoragePlaceCodeBefore = m.StoragePlaceCode;
            log.StoragePlaceCodeAfter = string.Empty;

            log.MaterialCodeBefore = m.MaterialCode;
            log.MaterialCodeAfter = string.Empty;

            log.WorkIDBefore = m.WorkID;
            log.WorkIDAfter = string.Empty;

            log.OperCode = this.UserID;
            log.OperDate = DateTime.Now;
            log.OperMethod = "Clear";
            log.DeleteFlag = "0";
            logManager.Insert(log);

            m.ObjID = Convert.ToInt32(objid);
            m.Attach();
            m.MaterialCode = string.Empty;
            m.StoragePlaceCode = string.Empty;
            m.WorkID = string.Empty;
            m.DeleteFlag = "0";
            m.Priority = 0;
            m.Remark = string.Empty;
            pmtEquipJarStoreManager.Update(m);
            X.Js.Call("gridPanelRefresh()");
        }
        catch (Exception ex)
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "清空失败：" + ex.Message, Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return;
        }
    }
    #endregion

}