using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;
using System.Text.RegularExpressions;
using NBear.Common;
using System.Text;

public partial class Manager_BasicInfo_SupplierCustomerInfo_FactoryInfo : Mesnac.Web.UI.Page
{
    protected BasFactoryInfoManager manager = new BasFactoryInfoManager();//业务对象
    protected BasFactoryTypeManager facTypeManager = new BasFactoryTypeManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasFactoryInfo> GetPageResultData(PageResult<BasFactoryInfo> pageParams)
    {
        BasFactoryInfoManager.QueryParams queryParams = new BasFactoryInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        //queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.facName = txt_fac_name.Text.TrimEnd().TrimStart();
        queryParams.facType = hidden_select_fac_type.Text;
        //queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasFactoryInfo> GetTablePageDataBySql(BasFactoryInfoManager.QueryParams queryParams)
    {
        PageResult<BasFactoryInfo> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" SELECT info.ObjID, type.FactoryTypeName as FacType, info.FacName, 
                                        info.FacSimpleName, info.FacAddress, info.FacPostCode, 
                                        info.Corporation, info.ContactTel, info.ContactMan, 
                                        info.DutyMan, info.Email, info.Remark, info.DeleteFlag, info.DisplayId,
                                        info.HRCode , info.ERPCode
                                 FROM BasFactoryInfo info
                                 LEFT JOIN BasFactoryType type ON info.FacType = type.ObjID
                                 WHERE 1=1 ");
        if (!string.IsNullOrEmpty(queryParams.objID))
        {
            sqlstr.AppendLine(" AND info.ObjID = " + queryParams.objID);
        }
        if (!string.IsNullOrEmpty(queryParams.facName))
        {
            sqlstr.AppendLine(" AND (info.facName like '%" + queryParams.facName + "%' or info.FacSimpleName like '%" + queryParams.facName + "%')");
        }
        if (!string.IsNullOrEmpty(queryParams.facType))
        {
            sqlstr.AppendLine(" AND info.FacType = " + queryParams.facType);
        }
        if (!string.IsNullOrEmpty(queryParams.facAddress))
        {
            sqlstr.AppendLine(" AND info.FacAddress like '%" + queryParams.facAddress + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.corporation))
        {
            sqlstr.AppendLine(" AND info.Corporation like '%" + queryParams.corporation + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.contactMan))
        {
            sqlstr.AppendLine(" AND info.ContactMan like '%" + queryParams.contactMan + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.dutyMan))
        {
            sqlstr.AppendLine(" AND info.DutyMan like '%" + queryParams.dutyMan + "%'");
        }
        //if (!string.IsNullOrEmpty(txterp.Text))
        //{
        //    sqlstr.AppendLine(" AND info.erpcode like '%" + txterp.Text + "%'");
        //}
        if (!string.IsNullOrEmpty(queryParams.email))
        {
            sqlstr.AppendLine(" AND info.Email like '%" + queryParams.email + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.remark))
        {
            sqlstr.AppendLine(" AND info.Remark like '%" + queryParams.remark + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND info.DeleteFlag ='" + queryParams.deleteFlag + "'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        //{
        //    txt_obj_id.Text = "";
        //}
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasFactoryInfo> pageParams = new PageResult<BasFactoryInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasFactoryInfo> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        //if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        //{
        //    txt_obj_id.Text = "";
        //}
        PageResult<BasFactoryInfo> pageParams = new PageResult<BasFactoryInfo>();
        pageParams.PageSize = -100;
        PageResult<BasFactoryInfo> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "厂商信息");
    }
    #endregion

    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        add_fac_name.Text = "";
        hidden_fac_name.Text = "";
        add_fac_simple_name.Text = "";
        add_fac_type.Text = "";
        add_fac_address.Text = "";
        add_fac_post_code.Text = "";
        add_corporation.Text = "";
        add_contact_tel.Text = "";
        add_contact_man.Text = "";
        add_duty_man.Text = "";
        add_email.Text = "";
        add_display_id.Text = "";
        add_hr_code.Text = "";
        add_erp_code.Text = "";
        add_remark.Text = "";
         
        //btnAddSave.Disable(true);
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {

        //X.Msg.Notify(objID,"").Show();

        string sql = "select * from BasFactoryInfo where objid = '" + objID + "'";
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        //BasFactoryInfo factoryInfo = manager.GetById(Convert.ToInt32(objID));orporation ContactTel ContactMan DutyMan Email DisplayId HRCode ERPCode Remark
        modify_obj_id.Text = ds.Tables[0].Rows[0]["ObjID"].ToString(); 
        modify_fac_name.Text= ds.Tables[0].Rows[0]["FacName"].ToString();
        hidden_fac_name.Text = ds.Tables[0].Rows[0]["FacName"].ToString();
        modify_fac_simple_name.Text= ds.Tables[0].Rows[0]["FacSimpleName"].ToString();
        sql = "select * from BasFactoryType where objid = '" + ds.Tables[0].Rows[0]["FacType"].ToString() + "'";
        DataSet ds2 = manager.GetBySql(sql).ToDataSet();
        if (ds2.Tables[0].Rows.Count > 0)
        { modify_fac_type.Text = ds2.Tables[0].Rows[0]["FactoryTypeName"].ToString(); ; }
        hidden_fac_type.Text = ds.Tables[0].Rows[0]["FacType"].ToString();
        modify_fac_address.Text= ds.Tables[0].Rows[0]["FacAddress"].ToString();
        modify_fac_post_code.Text= ds.Tables[0].Rows[0]["FacPostCode"].ToString();
        modify_corporation.Text= ds.Tables[0].Rows[0]["Corporation"].ToString();
        modify_contact_tel.Text= ds.Tables[0].Rows[0]["ContactTel"].ToString();
        modify_contact_man.Text= ds.Tables[0].Rows[0]["ContactMan"].ToString();
        modify_duty_man.Text= ds.Tables[0].Rows[0]["DutyMan"].ToString();
        modify_email.Text= ds.Tables[0].Rows[0]["Email"].ToString();
        modify_display_id.Text = ds.Tables[0].Rows[0]["DisplayId"].ToString();
        modify_hr_code.Text = ds.Tables[0].Rows[0]["HRCode"].ToString();
        modify_erp_code.Text = ds.Tables[0].Rows[0]["ERPCode"].ToString();
        modify_remark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();





        this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            BasFactoryInfo factoryInfo = manager.GetById(obj_id);
            BasFactoryType facType = facTypeManager.GetById(factoryInfo.FacType);

            if (facType.DeleteFlag.Equals("1"))
            {
                return "恢复失败：请先恢复厂商类别[" + facType.FactoryTypeName + "]";
            }
            factoryInfo.DeleteFlag = "0";
            manager.Update(factoryInfo);
            this.AppendWebLog("厂商信息恢复", "厂商编号：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            //BasFactoryInfo factoryInfo = manager.GetById(objID);
            //factoryInfo.DeleteFlag = "1";
            //manager.Update(factoryInfo);

            String sql = "delete Pmt_factory where Fac_id='"+ objID + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("厂商信息删除", "厂商编号：" + objID);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }


    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            if(string.IsNullOrEmpty(add_fac_name.Text.TrimStart().TrimEnd()))
            {
                X.Msg.Alert("提示", "此厂商名称不能为空！").Show();
                return;
            }


            EntityArrayList<BasFactoryInfo> facList = manager.GetListByWhere(BasFactoryInfo._.FacName == add_fac_name.Text.TrimStart().TrimEnd());
            if (facList.Count > 0)
            {
                X.Msg.Alert("提示", "此厂商名称已被使用！").Show();
                return;
            }
            //BasFactoryInfo factoryInfo = new BasFactoryInfo();
            //factoryInfo.ObjID = Convert.ToInt32(manager.GetNextFactoryCode());
            //factoryInfo.FacName = (string)(add_fac_name.Text);
            //factoryInfo.FacSimpleName = add_fac_simple_name.Text;
            //factoryInfo.FacType = Convert.ToInt32(hidden_fac_type.Text);
            //factoryInfo.FacAddress = add_fac_address.Text;
            //factoryInfo.FacPostCode = add_fac_post_code.Text;
            //factoryInfo.Corporation = add_corporation.Text;
            //factoryInfo.ContactTel = add_contact_tel.Text;
            //factoryInfo.ContactMan = add_contact_man.Text;
            //factoryInfo.DutyMan = add_duty_man.Text;
            //factoryInfo.Email = add_email.Text;
            //factoryInfo.DisplayId = Convert.ToInt32(add_display_id.Text == "" ? factoryInfo.ObjID.ToString() : add_display_id.Text);
            //factoryInfo.HRCode = add_hr_code.Text;
            //factoryInfo.ERPCode = add_erp_code.Text;
            //factoryInfo.SAPFacCode = add_erp_code.Text;
            //factoryInfo.Remark = (string)(add_remark.Text);
            //factoryInfo.DeleteFlag = "0";
            //manager.Insert(factoryInfo);

            string sql = "select max(fac_id)+1 from Pmt_factory";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into Pmt_factory values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + hidden_fac_type.Text + "','" + add_fac_name.Text + "','" + add_fac_simple_name.Text + "','" + add_fac_address.Text + "','','','" + add_contact_tel.Text + "','" + add_contact_man.Text + "','" + add_duty_man.Text + "','" + add_email.Text + "','" + add_remark.Text + "','','" + add_erp_code.Text + "')";

            //add_fac_name.Text = sql;
            //return;
            
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("厂商信息添加", "厂商编号：" + ds.Tables[0].Rows[0][0].ToString());
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            EntityArrayList<BasFactoryInfo> workList = manager.GetListByWhere(BasFactoryInfo._.FacName == modify_fac_name.Text.TrimStart().TrimEnd());
            //if (workList.Count > 0)
            //{
            //    if (workList[0].FacName != hidden_fac_name.Text)
            //    {
            //        X.Msg.Alert("提示", "此厂商名称已被使用！").Show();
            //        return;
            //    }
            //}


            string sql = "update Pmt_factory set Fac_name='" + modify_fac_name.Text + "',Fac_addr='" + modify_fac_address.Text + "',Contact_tel='" + modify_contact_tel.Text + "',Contact_email='" + modify_email.Text + "',Contact_lman='" + modify_contact_man.Text + "',Contact_pman='',facs_code='" + hidden_fac_type.Text
                + "' , sap_code ='" + modify_erp_code.Text + "',Fac_fname = '" + modify_fac_simple_name.Text + "' where Fac_id = '" + modify_obj_id.Text + "'";
            //modify_fac_address.Text = sql;
            //return;
            manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("厂商信息修改", "厂商编号：" + modify_obj_id.Text);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 校验方法
    /// <summary>
    /// 验证填写的是否是正整数方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckFieldInt(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        Regex regex = new Regex(@"^[\d]+$");
        if (regex.IsMatch(field.Text) || field.Text == "")
        {
                e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此填入项必须为正整数!";
        }
    }
    /// <summary>
    /// 验证邮编和手机号填写的是否是正整数，且位数为7位方法
    /// huiw   2013年10月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckFieldInttwo(object sender, RemoteValidationEventArgs e)
    {
        return;
        TextField field = (TextField)sender;
        Regex regex = new Regex(@"^[\d]+$");
        if (regex.IsMatch(field.Text.Trim()) || field.Text.Trim() == "")
        {
            if (field.Text.Trim().Length == 6 || field.Text.Trim().Length == 11)
            {
                e.Success = true;
            }
            else
            {
                e.ErrorMessage = "不符合位数要求!";
            }
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此填入项必须为正整数!";
        }
    }

    /// <summary>
    /// 检查厂商名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckFacName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string facname = field.Text;
        EntityArrayList<BasFactoryInfo> unitList = manager.GetListByWhere(BasFactoryInfo._.FacName == facname);
        if (unitList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (unitList[0].FacName.ToString() == hidden_fac_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此厂商名称已被使用！";
            }
        }
    }
    #endregion
}