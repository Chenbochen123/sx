﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;

public partial class Manager_ProducingPlan_WorkerJob : Mesnac.Web.UI.Page
{
    protected Ppt_WorkerManager manager = new Ppt_WorkerManager();
    protected BasWorkShopManager WSmanager = new BasWorkShopManager();
    protected BasEquipManager equipmanager = new BasEquipManager();

    protected JCZL_workManager workmanager = new JCZL_workManager();
    protected PptShiftManager shiftmanager = new PptShiftManager();
    protected PptClassManager classmanager = new PptClassManager();
    protected SYS_USERManager usermanager = new SYS_USERManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            datetime.SelectedDate = DateTime.Now;
            bindBasEquip();
            bindWorkshop();
            bindWorkUser();
            bindWork();
            bindList();
            shift();
        }
    }


    #region 初始化控件
    


    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }

    //班次
    private void shift()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<PptShift> list = shiftmanager.GetListByWhere(PptShift._.UseFlag == 1);
        foreach (PptShift main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.ShiftName, main.ObjID);
            cbxshift.Items.Add(item);
        }
    }


    //车间
    private void bindWorkshop()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasWorkShop> list = WSmanager.GetListByWhere(where);
        foreach (BasWorkShop main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.WorkShop_Name, main.WorkShop_Code);
            cbxworkshop.Items.Add(item);
        }
    }
    //岗位
    private void bindWork()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_work> list = workmanager.GetListByWhere(where);
        foreach (JCZL_work main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Work_name, main.Worktype);
            cbxwork.Items.Add(item);
        }
    }
    //操作人
    private void bindWorkUser()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<SYS_USER> list = usermanager.GetListByWhere(where);
        foreach (SYS_USER main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Real_name, main.USER_ID);
            cbxUser.Items.Add(item);
        }
    }
    //机台
    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list = equipmanager.GetListByWhere(BasEquip._.EquipType=="01");
        foreach (BasEquip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.EquipName, main.EquipCode);
            cbxequip.Items.Add(item);
        }
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"SELECT t.Serial_id,T.Plan_date,T1.EquipName,T2.ShiftName,T3.ClassName,T.Work_typeName,T.User_name,T4.WorkShop_Name FROM ppt_worker T
LEFT JOIN BasEquip T1 ON T1.EquipCode=T.Equip_Code
LEFT JOIN PptShift T2 ON T2.ObjID=T.Shift_id
LEFT JOIN PptClass T3 ON T3.ObjID=T.shift_Classid
LEFT JOIN BasWorkShop T4 ON T4.WorkShop_Code=T.Work_Shop where 1=1");

        if (!string.IsNullOrEmpty(datetime.Text))
        {
            sb.AppendLine("AND T.Plan_date='" + datetime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(cbxshift.Text))
        {
            sb.AppendLine("AND T.Shift_id='" + cbxshift.SelectedItem.Value + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }

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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "员工岗位导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(int ObjID)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@"delete from Ppt_Worker where Serial_id = '" + ObjID + "'");
        manager.GetBySql(sb.ToString()).ToDataSet();
        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(object sender, EventArgs e)
    {
            Ppt_Worker record = new Ppt_Worker();

            record.Plan_date = datetime.SelectedDate.ToString("yyyy-MM-dd");

            EntityArrayList<BasEquip> list = equipmanager.GetListByWhere(BasEquip._.EquipName == cbxequip.SelectedItem.Text);
            if (list.Count == 0)
            { record.Equip_Code = ""; }
            else
            {
                record.Equip_Code = list[0].EquipCode;
            }

            EntityArrayList<PptShift> list1 = shiftmanager.GetListByWhere(PptShift._.ShiftName == cbxshift.SelectedItem.Text);
            if (list1.Count == 0)
            { X.Msg.Alert("提示", "没有该班次，请重试！").Show(); return; }
            record.Shift_id = Convert.ToSByte(list1[0].ObjID);

            EntityArrayList<JCZL_work> list2 = workmanager.GetListByWhere(JCZL_work._.Work_name == cbxwork.SelectedItem.Text);
            if (list2.Count == 0)
            { X.Msg.Alert("提示", "没有该岗位，请重试！").Show(); return; }
            record.Work_Type = Convert.ToSByte(list2[0].Worktype);
            record.Work_typeName = list2[0].Work_name;

            EntityArrayList<PptClass> list3 = classmanager.GetListByWhere(PptClass._.ClassName == cbxclass.SelectedItem.Text);
            if (list3.Count == 0)
            { X.Msg.Alert("提示", "没有该班组，请重试！").Show(); return; }
            record.Shift_Classid = Convert.ToSByte(list3[0].ObjID);

            EntityArrayList<SYS_USER> list4 = usermanager.GetListByWhere(SYS_USER._.Real_name == cbxUser.SelectedItem.Text);
            if (list4.Count == 0)
            { X.Msg.Alert("提示", "没有该员工，请重试！").Show(); return; }
            record.User_id = list4[0].USER_ID;
            record.User_name = list4[0].Real_name;

            EntityArrayList<BasWorkShop> list5 = WSmanager.GetListByWhere(BasWorkShop._.WorkShop_Name == cbxworkshop.SelectedItem.Text);
            if (list5.Count == 0)
            { X.Msg.Alert("提示", "没有该车间，请重试！").Show(); return; }
            record.Work_Shop = Convert.ToSByte(list5[0].WorkShop_Code);
            
            if (manager.Insert(record) >= 0)
            {
                X.Msg.Alert("提示", "添加完成！").Show(); bindList();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
    }
    #endregion
}