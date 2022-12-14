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


/// <summary>
/// Manager_Technology_BasicInfo_MixingModel 实现类
/// 孙本强 @ 2013-04-03 13:04:58
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_BasicInfo_MixingModel : Mesnac.Web.UI.Page
{

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    private ISysCodeManager sysCodeManager = new SysCodeManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    private IPmtTermManager pmtTermManager = new PmtTermManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    private IPmtActionManager pmtActionManager = new PmtActionManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    private IPmtMixingModelManager pmtMixingModelManager = new PmtMixingModelManager();



    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    private IBasEquipManager basEquipManager = new BasEquipManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    private IPmtEquipJarStoreManager pmtEquipJarStoreManager = new PmtEquipJarStoreManager();
    #endregion

    #region 私有常量定义
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:04:59
    /// </summary>
    readonly string EquipType_SysCodeNodeIDStarWith = "EquipType_SysCode=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:00
    /// </summary>
    readonly string Equip_SysCodeNodeIDStarWith = "Equip_SysCode=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:00
    /// </summary>
    readonly string EquipJarType_SysCodeNodeIDStarWith = "EquipJarType_SysCode=";
    #endregion

    #region 页面初始化
    /// <summary>
    /// Fillsets the tem code.
    /// 孙本强 @ 2013-04-03 13:05:00
    /// </summary>
    /// <remarks></remarks>
    private void FillsetTemCode()
    {
        WhereClip where = new WhereClip();
        where.And(PmtTerm._.DeleteFlag == 0);
        OrderByClip order = new OrderByClip();
        order = PmtTerm._.SeqIdx.Asc;
        EntityArrayList<PmtTerm> lst = pmtTermManager.GetListByWhereAndOrder(where, order);
        setTemCode.Items.Clear();
        foreach (PmtTerm m in lst)
        {
            ListItem item = new ListItem();
            item.Text = m.ShowName;
            item.Value = m.TermCode;
            setTemCode.Items.Add(item);
        }
    }
    /// <summary>
    /// Fillsets the action code.
    /// 孙本强 @ 2013-04-03 13:05:00
    /// </summary>
    /// <remarks></remarks>
    private void FillsetActionCode()
    {
        WhereClip where = new WhereClip();
        where.And(PmtAction._.DeleteFlag == 0);
        OrderByClip order = new OrderByClip();
        order = PmtAction._.SeqIdx.Asc;
        EntityArrayList<PmtAction> lst = pmtActionManager.GetListByWhereAndOrder(where, order);
        setActionCode.Items.Clear();
        foreach (PmtAction m in lst)
        {
            ListItem item = new ListItem();
            item.Text = m.ShowName;
            item.Value = m.ActionCode;
            setActionCode.Items.Add(item);
        }
    }
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:05:01
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        StatusBar1.Text = "";
        StatusBar1.Html = "";
        if (X.IsAjaxRequest)
        {
            return;
        }
        WhereClip where = new WhereClip();
        //where.And(SysCode._.TypeID == SysCodeManager.SysCodeType.Equip.ToString());
        OrderByClip order = new OrderByClip();
        order = SysCode._.ItemCode.Asc;
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhereAndOrder(where, order);
        foreach (SysCode menu in lst)
        {
            Node node = new Node();
            node.NodeID = this.EquipType_SysCodeNodeIDStarWith + menu.ItemCode.ToString();
            node.Text = menu.ItemName;
            treePanelUser.GetRootNode().AppendChild(node);
        }
        treePanelUser.GetRootNode().Expand(false);
        StatusBar1.Html = DefaultHtml("数据查询成功！一级菜单数为：" + lst.Count.ToString());
        GridPanelBindData();

        FillsetTemCode();
        FillsetActionCode();
    }
    /// <summary>
    /// Reds the HTML.
    /// 孙本强 @ 2013-04-03 13:05:01
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
    /// 孙本强 @ 2013-04-03 13:05:01
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
    /// Trees the panel action node load.
    /// 孙本强 @ 2013-04-03 13:05:01
    /// </summary>
    /// <param name="equipType">Type of the equip.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string treePanelActionNodeLoad(string equipType)
    {
        if (equipType.StartsWith(this.EquipType_SysCodeNodeIDStarWith))
        {
            equipType = equipType.Substring(this.EquipType_SysCodeNodeIDStarWith.Length);
            WhereClip where = new WhereClip();
            where.And(BasEquip._.DeleteFlag == 0);
            where.And(BasEquip._.EquipType == equipType);
            EntityArrayList<BasEquip> lst = basEquipManager.GetListByWhere(where);

            NodeCollection nodes = new Ext.Net.NodeCollection();
            foreach (BasEquip menu in lst)
            {
                Node node = new Node();
                node.NodeID = this.Equip_SysCodeNodeIDStarWith + menu.ObjID.ToString();
                node.Text = menu.EquipName;
                node.Leaf = false;
                nodes.Add(node);
            }
            if (nodes.Count == 0)
            {
                Node node = new Node();
                node.NodeID = this.Equip_SysCodeNodeIDStarWith + "NoHave=" + DateTime.Now.ToString("yyMMddHHmmss");
                node.Text = "无信息";
                node.Leaf = true;
                nodes.Add(node);
            }
            return nodes.ToJson();
        }
        else
        {
            equipType = equipType.Substring(this.Equip_SysCodeNodeIDStarWith.Length);
            WhereClip where = new WhereClip();
            where.And(SysCode._.TypeID == SysCodeManager.SysCodeType.EquipJar.ToString());
            EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(where);
            NodeCollection nodes = new Ext.Net.NodeCollection();
            foreach (SysCode menu in lst)
            {
                Node node = new Node();
                node.NodeID = this.EquipJarType_SysCodeNodeIDStarWith + equipType + "|" + menu.ItemCode.ToString();
                node.Text = menu.ItemName;
                node.Leaf = true;
                nodes.Add(node);
            }
            if (nodes.Count == 0)
            {
                Node node = new Node();
                node.NodeID = this.EquipJarType_SysCodeNodeIDStarWith + "NoHave=" + DateTime.Now.ToString("yyMMddHHmmss");
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
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:05:02
    /// </summary>
    /// <remarks></remarks>
    public void GridPanelBindData()
    {
        int total = 0;
        string equipID = txtEquipID.Text;
        string jarTypeID = txtJarTypeID.Text;
        string modelcode = "1";

        EntityArrayList<PmtMixingModel> lst = new EntityArrayList<PmtMixingModel>();

        lst = pmtMixingModelManager.GetListByWhere(PmtMixingModel._.ModelCode == modelcode);

        int modelcount = lst.Count;
        int pagesize = 30;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtMixingModel m = new PmtMixingModel();
            m.SeqIdx = i;
            lst.Add(m);
        }
        total = lst.Count;

        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        data = new Mesnac.Util.BaseInfo.DictionaryList().GenericListTo(lst.ToArray());
        for (int i = 0; i < data.Count; i++)
        {
            Dictionary<string, object> dic = data[i];
            string key = "TemCode";
            if (dic[key] != null)
            {
                string value = dic[key].ToString();
                dic[key] = "添加" + value;
            }
        }

        this.store.DataSource = data;
        this.store.DataBind();
    }
    /// <summary>
    /// Refreshes the actin user grid.
    /// 孙本强 @ 2013-04-03 13:05:02
    /// </summary>
    /// <param name="equipid">The equipid.</param>
    /// <remarks></remarks>
    private void RefreshActinUserGrid(string equipid)
    {
    //    if (equipid.Length == 0)
    //    {
    //        return;
    //    }
    //    if (!equipid.StartsWith(this.EquipJarType_SysCodeNodeIDStarWith))
    //    {
    //        return;
    //    }
    //    equipid = equipid.Substring(this.EquipJarType_SysCodeNodeIDStarWith.Length);
    //    string[] ss = equipid.Split('|');
    //    if (ss.Length < 2)
    //    {
    //        return;
    //    }
    //    txtEquipID.Text = equipid;
    //    txtJarTypeID.Text = ss[1];
    //    BasEquip equip = basEquipManager.GetListByWhere(BasEquip._.DeleteFlag == 0 && BasEquip._.ObjID == ss[0])[0];
    //    SysCode equipType = sysCodeManager.GetListByWhere(SysCode._.TypeID == SysCodeManager.SysCodeType.Equip.ToString() && SysCode._.ItemCode == ss[0])[0];
    //    SysCode equipJarType = sysCodeManager.GetListByWhere(SysCode._.TypeID == SysCodeManager.SysCodeType.EquipJar.ToString() && SysCode._.ItemCode == ss[1])[0];
    //    txtEquipJarType.Text = equipType.ItemName;
    //    txtEquipCode.Text = equip.EquipCode;
    //    txtEquipName.Text = equip.EquipName;
    //    txtJarName.Text = equipJarType.ItemName;
    //    X.Js.Call("gridPanelRefresh()");
    }
    /// <summary>
    /// Gets the actin user grid.
    /// 孙本强 @ 2013-04-03 13:05:02
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void GetActinUserGrid(object sender, EventArgs e)
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
    /// 孙本强 @ 2013-04-03 13:05:03
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        int total = 0;
        string equipID = txtEquipID.Text;
        string jarTypeID = txtJarTypeID.Text;
        string modelcode = txtJarName.Text;

        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        EntityArrayList<PmtMixingModel> lst = new EntityArrayList<PmtMixingModel>();

        lst = pmtMixingModelManager.GetListByWhere(PmtMixingModel._.ModelCode == modelcode);

        int modelcount = lst.Count;
        int pagesize = prms.Limit;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtMixingModel m = new PmtMixingModel();
            m.SeqIdx = i;
            lst.Add(m);
        }
        total = lst.Count;

        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        data = new Mesnac.Util.BaseInfo.DictionaryList().GenericListTo(lst.ToArray());
        for (int i = 0; i < data.Count; i++)
        {
            Dictionary<string, object> dic = data[i];
            string key = "TemCode";
            if (dic[key] != null)
            {
                string value = dic[key].ToString();
                dic[key] = value;
            }
        }
        return new { data = data, total = total };
    }
    #endregion
    #region 添加
    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// 孙本强 @ 2013-04-03 13:05:03
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        cellEditing.Visible = !cellEditing.Visible;
    }



    /// <summary>
    /// Saves the grid info.
    /// 孙本强 @ 2013-04-03 13:05:03
    /// </summary>
    /// <param name="typejsonstr">The typejsonstr.</param>
    /// <param name="datajsonstr">The datajsonstr.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string SaveGridInfo(string typejsonstr, string datajsonstr)
    {
        string Result = string.Empty;
        #region 获取模版类型
        JavaScriptArray typejson = (JavaScriptArray)JavaScriptConvert.DeserializeObject(typejsonstr);
        string ModelCode = string.Empty;
        for (int i = 0; i < typejson.Count; i++)
        {
            JavaScriptObject obj = (JavaScriptObject)typejson[i];
            ModelCode = obj["ModelCode"].ToString();
        }
        if (string.IsNullOrWhiteSpace(ModelCode))
        {
            ModelCode = DateTime.Now.ToString("HHmmss");
        }
        txtJarName.Text = ModelCode;
        #endregion

        #region 获取模版信息
        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();
        JavaScriptArray datajson = (JavaScriptArray)JavaScriptConvert.DeserializeObject(datajsonstr);

        List<PmtMixingModel> lst = new List<PmtMixingModel>();

        for (int i = 0; i < datajson.Count; i++)
        {
            JavaScriptObject record = (JavaScriptObject)datajson[i];
            PmtMixingModel m = new PmtMixingModel();
            m.ModelCode = ModelCode;
            m.TemCode = converter.ToString(record["TemCode"]);
            m.ActionCode = converter.ToString(record["ActionCode"]);
            if (m.ActionCode == null)
            {
                continue;
            }
            m.TempValue = converter.ToDecimal(record["TempValue"]);
            m.PresValue = converter.ToDecimal(record["PresValue"]);
            m.RotaValue = converter.ToDecimal(record["RotaValue"]);
            m.PowerValue = converter.ToDecimal(record["PowerValue"]);
            m.EnerValue = converter.ToDecimal(record["EnerValue"]);
            m.TimeValue = converter.ToDecimal(record["TimeValue"]);
            m.SeqIdx = converter.ToInt(record["SeqIdx"]);
            lst.Add(m);
        }
        #endregion
        return pmtMixingModelManager.SaveModel(lst);
    }
    #endregion
}