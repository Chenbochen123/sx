using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using NBear.Common;
using System.Collections.Generic;
using System.Text;


public partial class Manager_ShopStorage_Pstmmshopout : Mesnac.Web.UI.Page
{
    protected PstmmshopoutManager Manager = new PstmmshopoutManager();
    protected PptShiftManager shiftManager = new PptShiftManager();
    protected BasWorkShopManager shopManager = new BasWorkShopManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            txtDate.SelectedDate = DateTime.Now;
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        bindshift();
        bindList();
        bindshop();
    }


    private void bindshift()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<PptShift> list = shiftManager.GetListByWhere(PptShift._.UseFlag == 1);
        foreach (PptShift main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.ShiftName, main.ObjID);
            cbxshift.Items.Add(item);
        }
    }

    private void bindshop()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasWorkShop> list = shopManager.GetListByWhere(where);
        foreach (BasWorkShop main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.WorkShopName, main.ObjID);
            cbxshop.Items.Add(item);
        }
    }

    private DataSet getList()
    {


        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"
select a.Plan_date,b.ShiftName,c.shift_ClassName,d.EquipName,e.MaterialName SCMater,f.MaterialName XHMater,a.consume_qty,Balance_Qty,Cons_qty,Sur_plus,cast((Sur_plus/consume_qty)*100 as decimal(18,2)) sunhao,g.MinorTypeName

from  Pst_mmshopout a
left join PptShift b on b.ObjID=a.Shift_id
left join Ppt_ShiftClass c on c.shift_ClassId=a.Shift_Class
left join BasEquip d on d.EquipCode=a.Equip_code
left join BasMaterial e on e.MaterialCode=a.Cost_code
left join BasMaterial f on f.MaterialCode=a.Mater_code
left join BasMaterialMinorType g on g.MajorID=f.MajorTypeID and g.MinorTypeID=f.MinorTypeID
");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND a.Plan_date ='" + txtDate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        if (!string.IsNullOrEmpty(cbxshift.Text))
        {
            sb.AppendLine("AND a.Shift_id='" + cbxshift.SelectedItem.Value + "'");
        }
        if (!string.IsNullOrEmpty(cbxshop.Text))
        {
            sb.AppendLine("AND d.WorkShopCode='" + cbxshop.SelectedItem.Value + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList();
        //huiw,2013-10-28添加，判断不为空时才导出excel
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "未查询出数据！").Show();
        }
        else
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                bool isshow = false;
                DataColumn dc = ds.Tables[0].Columns[i];
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
                    ds.Tables[0].Columns.Remove(dc.ColumnName);
                    i--;
                }
            }
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "车间原料消耗统计");
        }
    }

}

    #endregion

   

    
