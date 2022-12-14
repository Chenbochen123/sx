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

public partial class Manager_ProducingPlan_RunHua : Mesnac.Web.UI.Page
{
    protected Ppt_RunHuaYouManager manager = new Ppt_RunHuaYouManager();
    protected BasEquipManager equipmanager = new BasEquipManager();
    protected Pmt_materialManager matermanager = new Pmt_materialManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            dateend.SelectedDate = DateTime.Now;
            datetime.SelectedDate = DateTime.Now;
            bindBasEquip();
            bindmater();
            this.winSave.Hidden = true;
            bindList();
        }
    }


    #region 初始化控件
    


    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }

    
    //润滑油型号
    private void bindmater()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_material> list = matermanager.GetListByWhere(Pmt_material._.Ikind_code == "12");
        foreach (Pmt_material main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Mater_name, main.Mater_code);
            cbxxinghao.Items.Add(item);
            cbxxinghao1.Items.Add(item);
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
           // cbxequip.Items.Add(item);
            //cbxequip1.Items.Add(item);
        }
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"SELECT t.serialid,t.equip_code,T.Plan_Date,T1.WorkShop_Name,T2.Equip_name,T.shift_id,T.shift_Class,T.Used_weight,T.factory,t3.Mater_name,T3.Plan_price,LYWeight,(LYWeight - Used_weight) Weight FROM ppt_runhuayou T
LEFT JOIN JCZL_WorkShop T1 ON T1.WorkShop_Code=T.Workshop
LEFT JOIN Pmt_equip T2 ON T2.Equip_code=T.Equip_Code
LEFT JOIN Pmt_material T3 ON T3.Mater_code=T.Mater_Code  where 1=1");

        if (!string.IsNullOrEmpty(datetime.Text))
        {
            sb.AppendLine("AND T.Plan_Date >='" + datetime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(dateend.Text))
        {
            sb.AppendLine("AND T.Plan_Date <='" + dateend.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(cbxshift.Text))
        {
            sb.AppendLine("AND T.shift_id='" + cbxshift.SelectedItem.Text + "'");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "润滑油录入导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string serialid)
    {
        EntityArrayList<Ppt_RunHuaYou> list = manager.GetListByWhere(Ppt_RunHuaYou._.Serialid == serialid);
        if(list.Count>0)
        {
            Ppt_RunHuaYou record = list[0];
            manager.Delete(record);
           // this.AppendWebLog("隔离剂删除", "删除机台号：" + record.Equip_Code.ToString());
            X.Msg.Alert("提示", "删除完成！").Show();
            bindList();
        }
        else
        {
            X.Msg.Alert("提示", "该记录不存在！").Show();
        }
    }
    [DirectMethod]
    public void pnlList_Add(object sender, EventArgs e)
    {
        Ppt_RunHuaYou record = new Ppt_RunHuaYou();

        //EntityArrayList<Ppt_GeLiJi> list0 = manager.GetListByWhere(Ppt_GeLiJi._.Plan_Date == datetime.SelectedDate.ToString("yyyy-MM-dd") && Ppt_GeLiJi._.Shift_Class == cbxclass.SelectedItem.Text && Ppt_GeLiJi._.Shift_id == cbxshift.SelectedItem.Text);
        //if (list0.Count > 0)
        //{ X.Msg.Alert("提示", "已存在该班组的隔离剂信息，请检查确认！").Show(); return; }

        record.Plan_Date = datetime.SelectedDate.ToString("yyyy-MM-dd");

        //EntityArrayList<BasEquip> list = equipmanager.GetListByWhere(BasEquip._.EquipName == cbxequip.SelectedItem.Text);
        //if (list.Count == 0)
        //{ X.Msg.Alert("提示", "没有该机台，请重试！").Show(); return; }

        //record.Equip_Code = list[0].EquipCode;
        //record.Workshop = Convert.ToSByte(list[0].WorkShopCode);

        if (cbxshift.SelectedItem.Text == "")
        { X.Msg.Alert("提示", "没有该班次，请重试！").Show(); return; }
        record.Shift_id = cbxshift.SelectedItem.Text;
        if (cbxclass.SelectedItem.Text == "")
        { X.Msg.Alert("提示", "没有该班组，请重试！").Show(); return; }
        record.Shift_Class = cbxclass.SelectedItem.Text;

        EntityArrayList<Pmt_material> list2 = matermanager.GetListByWhere(Pmt_material._.Mater_name == cbxxinghao.SelectedItem.Text);
        if (list2.Count == 0)
        { X.Msg.Alert("提示", "没有该润滑油，请重试！").Show(); return; }
        record.Mater_Code = list2[0].Mater_code;

        if (txtyongliang.Text == "")
        { X.Msg.Alert("提示", "请填写用量！").Show(); return; }
        record.Used_weight = Convert.ToDecimal(txtyongliang.Text);

        if (txtchangjia.Text == "")
        { X.Msg.Alert("提示", "请填写厂家！").Show(); return; }
        record.Factory = txtchangjia.Text;
        record.Price = list2[0].Plan_price;
        record.LYWeight = Convert.ToDecimal(cbxly.Text);


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

    [DirectMethod]
    public void pnlList_Edit(string serialid)
    {
        EntityArrayList<Ppt_RunHuaYou> list = manager.GetListByWhere(Ppt_RunHuaYou._.Serialid == serialid);
        if (list.Count > 0)
        {
            Ppt_RunHuaYou record = list[0];

            if (record != null)
            {
                datetime1.SetValue(record.Plan_Date);
               // cbxequip1.SetValue(record.Equip_Code);
                cbxshift1.SetValue(record.Shift_id);
                txtyongliang1.SetValue(record.Used_weight);
                cbxclass1.SetValue(record.Shift_Class);
                cbxxinghao1.SetValue(record.Mater_Code);
                txtchangjia1.SetValue(record.Factory);
                txtlingy.SetValue(record.LYWeight);

                hideObjID1.Text = record.Serialid.ToString();
                //hideObjID2.Text = record.Shift_id;
                //hideObjID3.Text = record.Shift_Class;
               // hideObjID.Text = ObjID;
                this.hideMode.Text = "Edit";

                this.winSave.Hidden = false;
            }
        }
        else
        {
            bindList();
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EntityArrayList<Ppt_RunHuaYou> list = manager.GetListByWhere(Ppt_RunHuaYou._.Serialid == hideObjID1.Text);
         if (list.Count > 0)
         {
             Ppt_RunHuaYou record = list[0];
             if (record != null)
             {
                 if (datetime1.SelectedDate.ToString("yyyy-MM-dd") != record.Plan_Date)
                 { X.Msg.Alert("提示", "日期不允许修改！").Show(); return; }
                 //if(cbxequip1.SelectedItem.Value!=record.Equip_Code)
                 //{ X.Msg.Alert("提示", "机台为主键，不允许修改！").Show(); return; }
                 if (cbxshift1.SelectedItem.Text != record.Shift_id)
                 { X.Msg.Alert("提示", "班次不允许修改！").Show(); return; }

                 //record.Plan_Date = datetime1.SelectedDate.ToString("yyyy-MM-dd");
                 //EntityArrayList<BasEquip> list1 = equipmanager.GetListByWhere(BasEquip._.EquipName == cbxequip1.SelectedItem.Text);
                 //if (list1.Count == 0)
                 //{ X.Msg.Alert("提示", "没有该机台，请重试！").Show(); return; }

                 //record.Equip_Code = list1[0].EquipCode;
                 //record.Workshop = Convert.ToSByte(list1[0].WorkShopCode);

                 if (cbxclass1.SelectedItem.Text == null)
                 { X.Msg.Alert("提示", "没有该班组，请重试！").Show(); return; }
                 record.Shift_Class = cbxclass1.SelectedItem.Text;

                 EntityArrayList<Pmt_material> list2 = matermanager.GetListByWhere(Pmt_material._.Mater_name == cbxxinghao1.SelectedItem.Text);
                 if (list2.Count == 0)
                 { X.Msg.Alert("提示", "没有该润滑油，请重试！").Show(); return; }
                 record.Mater_Code = list2[0].Mater_code;

                 if (txtyongliang1.Text == "")
                 { X.Msg.Alert("提示", "请填写用量！").Show(); return; }
                 record.Used_weight = Convert.ToDecimal(txtyongliang1.Text);

                 if (txtchangjia1.Text == "")
                 { X.Msg.Alert("提示", "请填写厂家！").Show(); return; }
                 record.Factory = txtchangjia1.Text;
                 record.Price = list2[0].Plan_price;
                 record.LYWeight = Convert.ToDecimal(txtlingy.Text);
               //  this.hideMode.Text = "Edit";

                 this.winSave.Hidden = false;

                 if (manager.Update(record) >= 0)
                 {
                    // this.AppendWebLog("隔离剂信息修改", "修改机台号：" + record.Equip_Code.ToString());
                     winSave.Hidden = true;
                     bindList();
                     X.Msg.Alert("提示", "修改完成！").Show();
                 }
                 else
                 {
                     X.Msg.Alert("提示", "修改失败！").Show();
                 }
             }
         }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
}