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

public partial class Manager_BasicInfo_MaterialInfo_MaterialAutoSet : Mesnac.Web.UI.Page
{
    protected BasMaterialManager manager = new BasMaterialManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected BasMaterialMajorTypeManager majorTypeManager = new BasMaterialMajorTypeManager();
    protected BasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected BasUnitManager unitManager = new BasUnitManager();
    protected BasRubInfoManager rubInfoManager = new BasRubInfoManager();
    protected BasMaterialStaticClassManager staticClassManager = new BasMaterialStaticClassManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected PmtMixTypeManager mixTypeManager = new PmtMixTypeManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
        }
    }
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };

            设置停放时间 = new SysPageAction() { ActionID = 9, ActionName = "btn_set_stock" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
      
        public SysPageAction 设置停放时间 { get; private set; } //必须为 public
    }
    #endregion
    /// <summary>
    /// 初始化物料分类列表树
    /// </summary>
    private void InitTreeDept()
    {
      
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
                nodeChild.NodeID = majorType.ObjID.ToString() + "|" + minorType.MinorTypeID.ToString();
                nodeChild.Text = minorType.MinorTypeName;
                nodeChild.Leaf = true;
                nodeChild.Icon = Icon.Box;
                node.Children.Add(nodeChild);
            }
            treeDept.GetRootNode().AppendChild(node);
        }
    }

    #region 分页相关方法
    private PageResult<BasMaterial> GetPageResultData(PageResult<BasMaterial> pageParams)
    {
        BasMaterialManager.QueryParams queryParams = new BasMaterialManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialCode = txt_material_code.Text.TrimEnd().TrimStart();
        queryParams.minorTypeID = hidden_minor_type_id.Text;
        queryParams.majorTypeID = hidden_major_type_id.Text;
        queryParams.materialName = txt_material_name.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;
        queryParams.workBarCode = this.cbxChejian.Value.ToString();
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_material_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_material_code.Text = "";
        }
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return "";
        //}
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterial> pageParams = new PageResult<BasMaterial>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterial> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    public void BtnSetStockSave_Click(object sender, DirectEventArgs e)
    {
        if (string.IsNullOrEmpty(this.txtSetTime.Text))
        {
            X.Msg.Alert("提示", "必须设置恢复时间！").Show();
            return;
        }
        string json = hidden_set_Stock.Value.ToString();
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = manager.GetListByWhere(BasMaterial._.MaterialCode == row["MaterialCode"])[0];
           
            if (cb_set_max_part_time.Checked == true)
            {
                material.MaxParkTime = Convert.ToDecimal(set_max_part_time.Text);
            }
            if (cb_set_min_part_time.Checked == true)
            {
                material.MinParkTime = Convert.ToDecimal(set_min_part_time.Text);
            }
            manager.UpdateMaterialTime(material.MaterialCode, (decimal)material.MaxParkTime, (decimal)material.MinParkTime, Convert.ToDateTime(this.txtSetTime.Text.Trim()).ToString("yyyy-MM-dd"), this.UserID.ToString());

            this.AppendWebLog("物料停放时间修改", "物料代码：" + material.MaterialCode);
        
        }
        hidden_set_Stock.Value = "";
        this.txtSetTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        this.winSetStock.Close();
        this.pageToolBar.DoRefresh();
        msg.Notify("提示", "批量修改完成!").Show();
    }

    public void BtnSetStockCancel_Click(object sender, DirectEventArgs e)
    {
        hidden_set_Stock.Value = "";
        this.winSetStock.Close();
    }

    [Ext.Net.DirectMethod()]
    protected void btn_set_stock_Click(object sender, DirectEventArgs e)
    {


        if (this._.设置停放时间.SeqIdx == 0)
        {
            msg.Notify("提示", "没有权限!").Show();
            return ;
        }
        set_max_part_time.Text = "0";
        set_max_part_time.Disabled = true;
        cb_set_max_part_time.Checked = false;
        set_min_part_time.Text = "0";
        set_min_part_time.Disabled = true;
        cb_set_min_part_time.Checked = false;
      
        string json = e.ExtraParams["StockValues"];
        hidden_set_Stock.Value = json;
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasMaterial> materialList = new List<BasMaterial>();
        foreach (Dictionary<string, string> row in materialDic)
        {
            BasMaterial material = new BasMaterial();
            material.MaterialName = row["MaterialName"];
            material.MaterialCode = row["MaterialCode"];
            material.MaxStock = Convert.ToDecimal(row["MaxStock"]);
            material.MinStock = Convert.ToDecimal(row["MinStock"]);
            material.MaxParkTime = Convert.ToDecimal(row["MaxParkTime"]);
            material.MinParkTime = Convert.ToDecimal(row["MinParkTime"]);
            material.IsQualityRateCount = row["IsQualityRateCount"];
            materialList.Add(material);
        }
        stockStore.Data = materialList;
        stockStore.DataBind();
        this.winSetStock.Show();
    }
    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(txt_material_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_material_code.Text = "";
        }
        PageResult<BasMaterial> pageParams = new PageResult<BasMaterial>();
        pageParams.PageSize = -100;
        PageResult<BasMaterial> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "物料信息");
    }
    #endregion
}