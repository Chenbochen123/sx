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


public partial class Manager_Equipment_SparePart_PptAlarm : Mesnac.Web.UI.Page
{
    protected PptAlarmManager Manager = new PptAlarmManager();
    protected BasEquipManager BasEquipManager = new BasEquipManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        bindBasEquip();
        daDateTime.SelectedDate = DateTime.Now;
        bindList();
    }
    //机台
    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list = BasEquipManager.GetListByWhere(BasEquip._.EquipType == "01");
        foreach (BasEquip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.EquipName, main.EquipCode);
            cbxequip.Items.Add(item);
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
        sb.AppendLine(@"select opertime,AlarmStr,b.EquipName from ppt_alarm a
left join BasEquip b on a.EquipCode=b.EquipCode");
        sb.AppendLine(" WHERE 1=1 ");

        if (!string.IsNullOrEmpty(daDateTime.SelectedDate.ToString()))
            sb.AppendLine(" AND SUBSTRING(opertime,0,11) ='" + daDateTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        if (!string.IsNullOrEmpty(cbxequip.SelectedItem.Value))
            sb.AppendLine(" AND a.EquipCode='" + cbxequip.SelectedItem.Value + "'");
       
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
    protected void btnSearch_Click( object sender , EventArgs e )
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "报警记录查询记录");
        }
    }
  
    #endregion

    
}