using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;
using NBear.Common;
public partial class Manager_BasicInfo_CommonPage_canAuditUser : System.Web.UI.Page
{
    PmtConfigManager pmtConfigManager = new PmtConfigManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        FillCheckboxGroup(PmtConfigManager.TypeCode.AuditUser, CheckboxGroupAuditUser);
        
    }

    private void FillCheckboxGroup(PmtConfigManager.TypeCode typeCode, Ext.Net.CheckboxGroup cb)
    {
        WhereClip where = new WhereClip();
        where.And(PmtConfig._.DeleteFlag == 0);
        where.And(PmtConfig._.TypeCode == typeCode.ToString());
        string sqlstr = pmtConfigManager.GetListByWhere(where)[0].ItemInfo;
        DataSet data = pmtConfigManager.GetBySql(sqlstr).ToDataSet();
        cb.Items.Clear();
        if (data != null && data.Tables.Count > 0)
        {
            foreach (DataRow dr in data.Tables[0].Rows)
            {
                Ext.Net.Checkbox item = new Ext.Net.Checkbox();
                item.BoxLabel = dr["ShowInfo"].ToString();
                item.InputValue = dr["ValueInfo"].ToString();
                cb.Add(item);
            }
        }
    }
}