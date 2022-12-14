using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Ext.Net;
using NBear.Common;
using Mesnac.Entity;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
public partial class Manager_BasicInfo_EquipmentInfo_EquipStorageInfo : System.Web.UI.Page
{
    protected BasEquipManager manager = new BasEquipManager();//业务对象
    protected BasEquipTypeManager equipTypeManager = new BasEquipTypeManager();
    protected BasEquipPartRelationManager relationManager = new BasEquipPartRelationManager();
    protected BasWorkShopManager workshopMananager = new BasWorkShopManager();
    protected BasEquipManager bbll = new BasEquipManager();
    protected BasWorkShopManager wbll = new BasWorkShopManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
            InitWorkShop();
        }
    }
    protected void InitWorkShop()
    {

        EntityArrayList<BasWorkShop> modellist = wbll.GetListByWhere(BasWorkShop._.DeleteFlag == "0");
        foreach (BasWorkShop model in modellist)
        {
            this.cbxChejian.Items.Add(new Ext.Net.ListItem(model.WorkShopName, model.ObjID.ToString()));
        }
        this.cbxChejian.Items.Insert(0, new Ext.Net.ListItem("全部", "0"));
        this.cbxChejian.EmptyText = "全部";
        this.cbxChejian.EmptyValue = "";
    }
    //<summary>
    //初始化设备分类列表树
    //</summary>
    private void InitTreeDept()
    {
        EntityArrayList<BasEquipType> equipTypeList = equipTypeManager.GetListByWhere(BasEquipType._.DeleteFlag == "0");
        treeDept.GetRootNode().RemoveAll();
        foreach (BasEquipType equipType in equipTypeList)
        {
            Node node = new Node();
            node.NodeID = equipType.ObjID.ToString();
            node.Text = equipType.EquipTypeName;
            node.Expanded = true;
            node.Icon = Icon.Monitor;
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    private DataSet GetPageResultData(int page, int pagesize)
    {
        BasEquipManager.QueryParams queryParams = new BasEquipManager.QueryParams();
        queryParams.equipCode = txt_equip_code.Text.Trim();
        queryParams.equipType = hidden_select_equip_type.Text;
        queryParams.equipName = txtEquipName.Text.Trim();
        queryParams.WorkShopCode = string.IsNullOrEmpty(cbxChejian.SelectedItem.Value) ? "" : cbxChejian.SelectedItem.Value;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.type = string.IsNullOrEmpty(cbxtype.SelectedItem.Value) ? "" : cbxtype.SelectedItem.Value;
        queryParams.page = page;
        queryParams.pagenum = pagesize;
        return bbll.EquipStorageQuery(queryParams);
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        DataSet lst = GetPageResultData(prms.Page, prms.Limit);
        DataTable data = lst.Tables[0];
        int total = string.IsNullOrEmpty(lst.Tables[1].Rows[0][1].ToString()) ? 0 : Convert.ToInt32(lst.Tables[1].Rows[0][1].ToString());
        return new { data, total };
    }
    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        BasEquipManager.QueryParams queryParams = new BasEquipManager.QueryParams();
        queryParams.equipCode = objID;
        DataTable dtnew = bbll.EquipStorageQueryByCode(queryParams).Tables[0];
        if (dtnew != null && dtnew.Rows.Count > 0)
        {
            DataRow dr = dtnew.Rows[0];
            this.modify_equipid.Text = dr["EquipCode"].ToString();
            this.modify_equipname.Text = dr["设备名称"].ToString();
            this.modify_yl.Text = dr["原料库房"].ToString();
            this.modify_yl2.Text = dr["原料库位"].ToString();
            this.hldmyl1.Value = dr["YLStockNO"].ToString();
            this.hldmyl2.Value = dr["YLStockKW"].ToString();
            this.modify_sj.Text = dr["生胶库房"].ToString();
            this.modify_sj2.Text = dr["生胶库位"].ToString();
            this.hldmsj1.Value = dr["SJStockNO"].ToString();
            this.hldmsj2.Value = dr["SJStockKW"].ToString();
            this.modify_xl.Text = dr["小料库房"].ToString();
            this.modify_xl2.Text = dr["小料库位"].ToString();
            this.hldmxl1.Value = dr["XLStockNO"].ToString();
            this.hldmxl2.Value = dr["XLStockKW"].ToString();
            this.modify_mj.Text = dr["母胶库房"].ToString();
            this.modify_mj2.Text = dr["母胶库位"].ToString();
            this.hldmmj1.Value = dr["MJStockNO"].ToString();
            this.hldmmj2.Value = dr["MJStockKW"].ToString();
            this.modify_zj.Text = dr["终胶库房"].ToString();
            this.modify_zj2.Text = dr["终胶库位"].ToString();
            this.hldmzj1.Value = dr["ZJStockNO"].ToString();
            this.hldmzj2.Value = dr["ZJStockKW"].ToString();
            this.modify_fl.Text = dr["反炼库房"].ToString();
            this.modify_fl2.Text = dr["反炼库位"].ToString();
            this.hldmfl1.Value = dr["FLStockNO"].ToString();
            this.hldmfl2.Value = dr["FLStockKW"].ToString();
            this.modify_fp.Text = dr["废品库房"].ToString();
            this.modify_fp2.Text = dr["废品库位"].ToString();
            this.hldmfp1.Value = dr["FPStockNO"].ToString();
            this.hldmfp2.Value = dr["FPStockKW"].ToString();
            this.modify_hg.Text = dr["合格库房"].ToString();
            this.modify_hg2.Text = dr["合格库位"].ToString();
            this.hldmhg1.Value = dr["HGStockNO"].ToString();
            this.hldmhg2.Value = dr["HGStockKW"].ToString();
            this.modify_no.Text = dr["不合格库房"].ToString();
            this.modify_no2.Text = dr["不合格库位"].ToString();
            this.hldmno1.Value = dr["NOStockNO"].ToString();
            this.hldmno2.Value = dr["NOStockKW"].ToString();
            this.modify_th.Text = dr["炭黑库房"].ToString();
            this.modify_th2.Text = dr["炭黑库位"].ToString();
            this.hldmth1.Value = dr["THStockNO"].ToString();
            this.hldmth2.Value = dr["THStockKW"].ToString();
        }
        this.winModify.Show();
    }
    /// <summary>
    /// 
    /// db   2014年2月26日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        BasEquipManager.QueryParams queryParams = new BasEquipManager.QueryParams();
        queryParams.equipCode = modify_equipid.Text.Trim();
        queryParams.FLStockNO = hldmfl1.Text.Trim();
        queryParams.FLStockKW = hldmfl2.Text.Trim();
        queryParams.YLStockNO = hldmyl1.Text.Trim();
        queryParams.YLStockKW = hldmyl2.Text.Trim();
        queryParams.SJStockNO = hldmsj1.Text.Trim();
        queryParams.SJStockKW = hldmsj2.Text.Trim();
        queryParams.XLStockNO = hldmxl1.Text.Trim();
        queryParams.XLStockKW = hldmxl2.Text.Trim();
        queryParams.MJStockNO = hldmmj1.Text.Trim();
        queryParams.MJStockKW = hldmmj2.Text.Trim();
        queryParams.ZJStockNO = hldmzj1.Text.Trim();
        queryParams.ZJStockKW = hldmzj2.Text.Trim();
        queryParams.HGStockNO = hldmhg1.Text.Trim();
        queryParams.HGStockKW = hldmhg2.Text.Trim();
        queryParams.NOStockNO = hldmno1.Text.Trim();
        queryParams.NOStockKW = hldmno2.Text.Trim();
        queryParams.FPStockNO = hldmfp1.Text.Trim();
        queryParams.FPStockKW = hldmfp2.Text.Trim();
        queryParams.THStockNO = hldmth1.Text.Trim();
        queryParams.THStockKW = hldmth2.Text.Trim();
        bbll.UpdateEquipStorage(queryParams);
        pageToolBar.DoRefresh();
        this.winModify.Close();
        msg.Alert("操作", "更新成功");
        msg.Show();

    }
    /// <summary>
    /// 
    /// db   2014年2月26日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        this.winModify.Close();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {

        DataSet lst = GetPageResultData(1, 99999);
        for (int i = 0; i < lst.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.Tables[0].Columns[i];
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
                lst.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst, "设备库房对应表");
    }
}