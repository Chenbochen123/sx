using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
public partial class Manager_ProducingPlan_PlanExecMonitoring_PlanSetMonitoring : Mesnac.Web.UI.Page
{
    IBasEquipMonitorManager basEquipMonitorManager = new BasEquipMonitorManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

    public static string equip_code = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            LoadGridData();
        }
    }
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新增 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            删除 = new SysPageAction() { ActionID = 3, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 新增 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
    }
    #endregion

    public void LoadGridData()
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        Store store = this.GridPanel1.GetStore();
        store.DataSource = basEquipMonitorManager.GetAllList();// pptShiftimeManager.GetShiftTimeByTime(start, end, dept).Tables[0];
        store.DataBind();
    }
    /// <summary>
    /// 正则表达式初步验证IP地址
    /// </summary>
    /// <param name="flag">flag 为0 添加验证 1修改验证</param>
    /// <returns></returns>
    public bool CheckIP(int flag)
    {
        if (flag == 0)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtIP.Text.Trim(), @"^((0|(?:[1-9]\d{0,1})|(?:1\d{2})|(?:2[0-4]\d)|(?:25[0-5]))\.){3}((?:[1-9]\d{0,1})|(?:1\d{2})|(?:2[0-4]\d)|(?:25[0-5]))$"))
            {
                msg.Alert("提示", "输入的IP地址格式不对！");
                msg.Show();
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtPort.Text.Trim(), @"^[1-9]\d*$"))
            {
                msg.Alert("提示", "输入的端口号格式不对！");
                msg.Show();
                return false;
            }
            else if (Convert.ToInt32(txtPort.Text.Trim()) > 65535)
            {
                msg.Alert("提示", "输入的端口号不可以大于65535！");
                msg.Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (flag == 1)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtUpIP.Text.Trim(), @"^((0|(?:[1-9]\d{0,1})|(?:1\d{2})|(?:2[0-4]\d)|(?:25[0-5]))\.){3}((?:[1-9]\d{0,1})|(?:1\d{2})|(?:2[0-4]\d)|(?:25[0-5]))$"))
            {
                msg.Alert("提示", "输入的IP地址格式不对！");
                msg.Show();
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtUpPort.Text.Trim(), @"^[1-9]\d*$"))
            {
                msg.Alert("提示", "输入的端口号格式不对！");
                msg.Show();
                return false;
            }
            else if (Convert.ToInt32(txtUpPort.Text.Trim()) > 65535)
            {
                msg.Alert("提示", "输入的端口号不可以大于65535！");
                msg.Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        else
            return false;
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
       
        try
        {
            if (CheckIP(0))
            {
                string sql = "insert into ppt_EquipMonitor  values('" + this.hidden_select_equip_code.Text + "','" + this.txtEquipCode.Text + "','" + this.txtIP.Text + "',' ',' ',' ','',' ',' ',' ','" + Convert.ToInt32(this.txtPort.Text) + "')";
                DataSet ds = basEquipMonitorManager.GetBySql(sql).ToDataSet();
                LoadGridData();
                this.AddConfigWin.Close();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.IndexOf("重复") > 0)
            {
                X.Js.Alert("机台的IP信息已添加！");
               
            }
            else
                X.Js.Alert(ex.Message.ToString());
        }

    }

    /// <summary>
    /// 点击添加按钮激发的事件
    /// sunyj   2013年5月1日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.txtEquipCode.Text = "";
        this.txtIP.Text = "";
        this.txtPort.Text = "";
        btnAddSave.Disable(true);
        this.AddConfigWin.Show();
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// sunyj   2013年5月1日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.AddConfigWin.Close();
    }


    protected void MyData_Refresh(object sender, StoreReadDataEventArgs e)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        LoadGridData();
    }

    protected void BtnSeacherShift_Click(object sender, DirectEventArgs e)
    {
        LoadGridData();
    }
    public void BtnModifyCancel_Click(object sender, DirectEventArgs e)
    {
        this.ModifyConfigWin.Close();
    }
    public void BtnModifySave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            if (CheckIP(1))
            {
                //BasEquipMonitor up = basEquipMonitorManager.GetById(this.hidden_update_equipCode.Text);
                //up.EquipIP = this.txtUpIP.Text;
                //up.EquipPort = Convert.ToInt32(this.txtUpPort.Text);
                //basEquipMonitorManager.Update(up);
                //LoadGridData();
                //this.hidden_update_equipCode.Text = "";
                //this.ModifyConfigWin.Close();
                //msg.Alert("操作", "更新成功");
                //msg.Show();

                string sql = "update ppt_EquipMonitor set Equip_IP='" + this.txtUpIP.Text + "',Equip_Port = '" + Convert.ToInt32(this.txtUpPort.Text) + "' where Equip_Code='" + equip_code + "'";
                basEquipMonitorManager.GetBySql(sql).ToDataSet();
                LoadGridData();
                this.hidden_update_equipCode.Text = "";
                this.ModifyConfigWin.Close();
                msg.Alert("操作", "更新成功");
                msg.Show();
            }
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }

    }

     #region 删改操作
    /// <summary>
    /// 点击修改激发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {


        string sql = "select * from ppt_EquipMonitor where Equip_Code = '"+ objID + "'";
        DataSet ds =basEquipMonitorManager.GetBySql(sql).ToDataSet();

        this.hidden_update_equipCode.Text = objID;
        this.txtUpEuipName.Text = ds.Tables[0].Rows[0]["Equip_name"].ToString();     
        this.txtUpIP.Text = ds.Tables[0].Rows[0]["Equip_IP"].ToString();
        this.txtUpPort.Text = ds.Tables[0].Rows[0]["Equip_Port"].ToString();
        equip_code = objID;
        this.ModifyConfigWin.Show();
    }
    
    /// <summary>
    /// 点击删除触发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            string sql = "delete ppt_EquipMonitor where Equip_Code='"+ objID + "'";
            basEquipMonitorManager.GetBySql(sql).ToDataSet();
            LoadGridData();
            return "";
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
    }
     #endregion
}