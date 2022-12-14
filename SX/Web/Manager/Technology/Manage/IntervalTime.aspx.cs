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

public partial class Manager_Technology_Manage_IntervalTime : Mesnac.Web.UI.Page
{
    protected Ppt_LotManager manager = new Ppt_LotManager();
    protected BasEquipManager equipmanager = new BasEquipManager();
    protected Pmt_mstopkindManager daleimanager = new Pmt_mstopkindManager();
    protected Pmt_istopkindManager xiaoleimanager = new Pmt_istopkindManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            datetime.SelectedDate = DateTime.Now;
            cbxyuanyin.SelectedItem.Value = "1";
            bindBasEquip();
            binddalei();
            bindxiaolei();
            this.winSave.Hidden = true;
            bindList();
        }
    }


    private DataSet getList()
    {

        return GetDataByParas();
    }


    //大类
    private void binddalei()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_mstopkind> list = daleimanager.GetListByWhere(where);
        foreach (Pmt_mstopkind main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Mkind_name, main.Mkind_code);
            cbxdalei.Items.Add(item);
        }
    }
    //小类
    private void bindxiaolei()
    {
        cbxxiaolei.Clear();
        var xiaolei = cbxxiaolei.SelectedItem.Value;
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_istopkind> list = xiaoleimanager.GetListByWhere(Pmt_istopkind._.Ikind_Code.SubString(0, 2) == cbxdalei.SelectedItem.Value);
        this.storeXiaoLei.DataSource = list;
        this.storeXiaoLei.DataBind();
        cbxxiaolei.SetValue(xiaolei);
    }
    //机台
    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list = equipmanager.GetListByWhere(BasEquip._.EquipType == "01");
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
        sb.AppendLine(@"SELECT Barcode,t3.Equip_name,t4.ShiftName,Plan_Date,T.Mater_Name,T.Serial_Id,Start_Datetime,Bwb_Time,t1.mkind_name,t2.ikind_name FROM Ppt_Lot T
left join Pmt_mstopkind t1 on t1.mkind_code=t.MSTOPcode
left join Pmt_istopkind t2 on t2.ikind_Code=t.istopcode
left join Pmt_equip t3 on t.Equip_Code=t3.Equip_code
left join PptShift t4 on t4.ObjID=t.Shift_Id and t4.UseFlag=1 where t3.Equip_class='01' ");

        if (!string.IsNullOrEmpty(datetime.Text))
        {
            sb.AppendLine("AND T.Plan_Date='" + datetime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(cbxequip.Text))
        {
            sb.AppendLine("AND t.equip_code='" + cbxequip.SelectedItem.Value + "'");
        }
        if (!string.IsNullOrEmpty(cbxshift.Text))
        {
            sb.AppendLine("AND T.shift_id='" + cbxshift.SelectedItem.Value + "'");
        }
        if (cbxyuanyin.SelectedItem.Value == "2")
        {

        }
        if (cbxyuanyin.SelectedItem.Value == "1")
        {
            sb.AppendLine("AND (t.MSTOPcode is null or Serial_id = 1 or Bwb_time>300)");
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

    #endregion

    [DirectMethod]
    protected void cbxdalei_SelectChanged(object sender, EventArgs e)
    {
        bindxiaolei();
    }

    [DirectMethod]
    public void pnlList_Edit(string Barcode, string Plan_Date)
    {
        EntityArrayList<Ppt_Lot> list = manager.GetListByWhere(Ppt_Lot._.Barcode == Barcode && Ppt_Lot._.Plan_Date == Plan_Date);
        if (list.Count > 0)
        {
            Ppt_Lot record = list[0];

            if (record != null)
            {
                cbxdalei.SetValue(record.MSTOPcode);
                cbxxiaolei.SetValue(record.Istopcode);

                hideObjID1.Text = record.Barcode;
                hideObjID2.Text = record.Plan_Date.ToString();
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
        EntityArrayList<Ppt_Lot> list = manager.GetListByWhere(Ppt_Lot._.Barcode == hideObjID1.Text && Ppt_Lot._.Plan_Date == hideObjID2.Text);
        if (list.Count > 0)
        {
            Ppt_Lot record = list[0];
            if (record != null)
            {
                record.MSTOPcode = cbxdalei.SelectedItem.Value;
                record.Istopcode = cbxxiaolei.SelectedItem.Value;

                this.winSave.Hidden = false;

                if (manager.Update(record) >= 0)
                {
                    this.AppendWebLog("间隔时间信息修改", "修改机台号：" + record.Equip_Code.ToString());
                    winSave.Hidden = true;
                    bindList();
                    X.Msg.Alert("提示", "修改完成！").Show(); bindList();
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