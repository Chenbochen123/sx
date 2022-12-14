using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

public partial class Manager_ShopStorage_MaterialStaticGroup : Mesnac.Web.UI.Page
{
    protected BasMaterialManager manager = new BasMaterialManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected BasMaterialMajorTypeManager majorTypeManager = new BasMaterialMajorTypeManager();
    protected BasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected BasMaterialStaticClassManager staticClassManager = new BasMaterialStaticClassManager();
    protected BasMaterialStaticGroupManager staticGroupManager = new BasMaterialStaticGroupManager();

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            设置物料分组 = new SysPageAction() { ActionID = 8, ActionName = "btn_material_group" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 设置物料分组 { get; private set; } //必须为 public
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
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
        }
    }
    /// <summary>
    /// 初始化物料分类列表树
    /// </summary>
    private void InitTreeDept()
    {
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return;
        //}
        EntityArrayList<BasMaterialMajorType> majorList = majorTypeManager.GetListByWhere(BasMaterialMajorType._.DeleteFlag == 0);
        treeDept.GetRootNode().RemoveAll();
        foreach (BasMaterialMajorType majorType in majorList)
        {
            Node node = new Node();
            node.NodeID = majorType.ObjID.ToString();
            node.Text = majorType.MajorTypeName;
            node.Icon = Icon.Brick;
            EntityArrayList<BasMaterialMinorType> minorList = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MajorID == majorType.ObjID && BasMaterialMinorType._.DeleteFlag == 0);
            foreach (BasMaterialMinorType minorType in minorList)
            {
                Node nodeChild = new Node();
                nodeChild.NodeID = majorType.ObjID.ToString() +"|" + minorType.MinorTypeID.ToString();
                nodeChild.Text = minorType.MinorTypeName;
                nodeChild.Leaf = true;
                nodeChild.Icon = Icon.Box;
                node.Children.Add(nodeChild);
            }
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterialStaticGroup> GetPageResultData(PageResult<BasMaterialStaticGroup> pageParams)
    {
        BasMaterialStaticGroupManager.QueryParams queryParams = new BasMaterialStaticGroupManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialCode = txtMaterialCode.Text.TrimEnd().TrimStart();
        queryParams.materialName = txtMaterialName.Text.TrimEnd().TrimStart();
        queryParams.majorTypeID = hiddenMajorTypeID.Text;
        queryParams.minorTypeID = hiddenMinorTypeID.Text;
        queryParams.deleteFlag = "0";

        return staticGroupManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txtMaterialCode.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txtMaterialCode.Text = "";
        }
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return "";
        //}
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialStaticGroup> pageParams = new PageResult<BasMaterialStaticGroup>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterialStaticGroup> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 设置分组
    [Ext.Net.DirectMethod()]
    protected void btnGroupClick(object sender, DirectEventArgs e)
    {
        cboGroup.ClearValue();
        string json = e.ExtraParams["Values"];
        hidden_material_group.Value = json;
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasMaterialStaticGroup> materialList = new List<BasMaterialStaticGroup>();
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterialStaticGroup material = new BasMaterialStaticGroup();
            material.MaterialName = row["MaterialName"];
            material.MaterialCode = row["MaterialCode"];
            material.MaterialGroup = row["MaterialGroupName"];
            string materialcode = row["MaterialCode"];
            string materialname = row["MaterialName"];
            data.Add(new { MaterialName = materialname + "-" + materialcode, MaterialCode = materialcode });
            materialList.Add(material);
        }
        groupStore.Data = materialList;
        groupStore.DataBind();

        materialgroupstore.Data = data;
        materialgroupstore.DataBind();
        this.winMaterialGroup.Show();
    }

    public void BtnGroupSave_Click(object sender, DirectEventArgs e)
    {
        string json = hidden_material_group.Value.ToString();
        if (hidden_material_group_code.Value == "")
        {
            this.msg.Alert("错误", "物料分组值不能为空!").Show();
            return;
        }
        Regex rgx = new Regex(@"^\d{13}$");
        if (hidden_material_group_code.Value.ToString().Length != 13 || !rgx.IsMatch(hidden_material_group_code.Value.ToString()))
        {
            this.msg.Alert("错误", "请选择有效物料分组值!").Show();
            return;
        }

        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterialStaticGroup material = staticGroupManager.GetListByWhere(BasMaterialStaticGroup._.MaterialCode == row["MaterialCode"])[0];
            material.MaterialGroup = hidden_material_group_code.Value.ToString();
            staticGroupManager.Update(material);
        }
        hidden_material_group.Value = "";
        this.winMaterialGroup.Close();
        this.pageToolBar.DoRefresh();
        hidden_material_group_code.Value = "";
        this.msg.Notify("提示", "设置物料分组值成功!").Show();
    }

    public void BtnGroupClear_Click(object sender, DirectEventArgs e)
    {
        string json = hidden_material_group.Value.ToString();

        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterialStaticGroup material = staticGroupManager.GetListByWhere(BasMaterialStaticGroup._.MaterialCode == row["MaterialCode"])[0];
            material.MaterialGroup = "";
            staticGroupManager.Update(material);
        }
        hidden_material_group.Value = "";
        this.winMaterialGroup.Close();
        this.pageToolBar.DoRefresh();
        hidden_material_group_code.Value = "";
        this.msg.Notify("提示", "清除物料分组值成功!").Show();
    }

    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winMaterialGroup.Close();
        hidden_material_group_code.Value = "";
    }

    [Ext.Net.DirectMethod()]
    public void MaterialGroupData_DbClick(string MaterialName, string MaterialCode)
    { 
        List<object> data = new List<object>();
        data.Add(new { MaterialName = MaterialName + "-" + MaterialCode, MaterialCode = MaterialCode });
        materialgroupstore.Data = data;
        materialgroupstore.DataBind();
        cboGroup.Select(MaterialName + "-" + MaterialCode);
        hidden_material_group_code.Value = MaterialCode;
        cboGroup.ShowTrigger(0);
    }

    protected void MaterialGroupStoreRefresh(object sender, StoreReadDataEventArgs e)
    {
        EntityArrayList<BasMaterial> materialList = manager.GetMaterialNameAndCodeBySearchKey(20, hiddenMajorTypeID.Value.ToString(), cboGroup.Text);
        this.materialgroupstore.DataSource = materialList;
        this.materialgroupstore.DataBind();
    }
    #endregion
}