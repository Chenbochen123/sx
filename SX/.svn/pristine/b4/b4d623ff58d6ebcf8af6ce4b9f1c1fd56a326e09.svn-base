using System;
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

public partial class Manager_ProducingPlan_ShiftNum : Mesnac.Web.UI.Page
{
    protected Ppt_shiftNumManager manager = new Ppt_shiftNumManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindList();
        }
    }


    #region 初始化控件



    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }


    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"
select t.objid,t1.Equip_name,t3.ClassName,t.shift_num from ppt_shiftnum t
left join Pmt_equip t1 on t1.Equip_code=t.Equip_code
left join PptClass t3 on t3.ObjID=t.shift_Class and t3.UseFlag=1
");
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


    #region 信息列表事件响应
   
    [DirectMethod]
    public void pnlList_Add(int objid, string Equip_name, string ClassName, int shift_num)
    {
        Ppt_shiftNum record = manager.GetById(objid);
        record.Shift_num = shift_num;
            if (manager.Update(record) >= 0)
            {
                X.Msg.Alert("提示", "修改完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "修改失败！").Show();
            }
        
    }
    #endregion


}