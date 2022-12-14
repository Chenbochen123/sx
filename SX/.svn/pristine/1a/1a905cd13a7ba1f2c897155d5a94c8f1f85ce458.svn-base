using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_RubberInfo_RubberInfo : Mesnac.Web.UI.Page
{
    protected BasRubInfoManager manager = new BasRubInfoManager();//业务对象
    protected BasUserManager userManager = new BasUserManager();//业务对象
    protected SysRubPowerUserManager rubPowerUserManager = new SysRubPowerUserManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected BasRubTypeManager rubTypeManager = new BasRubTypeManager();
    protected BasRubTyrePartManager tyrePartManager = new BasRubTyrePartManager();
    protected BasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    private EntityArrayList<BasUser> entityList;

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
            设置人员权限 = new SysPageAction() { ActionID = 8, ActionName = "btn_set_power_user" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 设置人员权限 { get; private set; } //必须为 public
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
    private PageResult<BasRubInfo> GetPageResultData(PageResult<BasRubInfo> pageParams)
    {
        BasRubInfoManager.QueryParams queryParams = new BasRubInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.rubName = txt_rub_name.Text.TrimEnd().TrimStart();
        queryParams.rubTypeCode = hidden_select_rub_type_id.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {

        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasRubInfo> pageParams = new PageResult<BasRubInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";
       
        PageResult<BasRubInfo> lst = GetPageResultData(pageParams);
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

        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        PageResult<BasRubInfo> pageParams = new PageResult<BasRubInfo>();
        pageParams.PageSize = -100;
        PageResult<BasRubInfo> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料信息");
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
        add_rub_name.Text = "";
        add_rub_other_name.Text = "";
        add_rub_type_id.Text = "";
        add_rub_purpose.Text = "";
        add_rub_rate.Text = "";
        add_factory_id.Text = "";
        add_remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }

    

    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        //初始化
        //modify_rub_purpose.Text = "";

        string sql = "select * from BasRubInfo where rubcode='"+ obj_id + "'";
        DataTable dt = manager.GetBySql(sql).ToDataSet().Tables[0];

        if (dt != null)
        {
            modify_obj_id.Value = dt.Rows[0]["ObjID"].ToString();
            modify_rub_code.Value = dt.Rows[0]["RubCode"].ToString();
            hidden_rubname.Value = dt.Rows[0]["RubName"].ToString();
            modify_rub_name.Value = dt.Rows[0]["RubName"].ToString();
            modify_rub_other_name.Value = dt.Rows[0]["RubOtherName"].ToString();
            hidden_rub_type_id.Value = dt.Rows[0]["RubTypeCode"].ToString();
            modify_rub_purpose.Text = dt.Rows[0]["RubPurpose"].ToString();
            int rubTypeCode = 0;
            if (!string.IsNullOrEmpty(dt.Rows[0]["RubTypeCode"].ToString()))
            {
                rubTypeCode = Convert.ToInt32(dt.Rows[0]["RubTypeCode"].ToString());
            }
            BasRubType rubType = rubTypeManager.GetById(dt.Rows[0]["RubTypeCode"].ToString());
            modify_rub_type_id.Value = rubType == null ? "" : rubType.RubTypeName;
            modify_rub_rate.Value = dt.Rows[0]["RubRate"].ToString();
            modify_remark.Value = dt.Rows[0]["Remark"].ToString();
            this.winModify.Show();
        }
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            String sql = "select * from basmaterial where rubcode ='" + obj_id + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
              
                return "此胶料已被使用,无法删除";
            }
             sql = "Delete from  Pmt_rub  where Rub_typecode ='"+ obj_id + "'";
             ds = manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("胶料信息删除", "胶料代码：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击恢激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            BasRubInfo rubInfo = manager.GetById(obj_id);
            BasRubType rubType = rubTypeManager.GetById(rubInfo.RubTypeCode);
            if (rubType.DeleteFlag.Equals("1"))
            {
                return "恢复失败：请先恢复胶料类别[" + rubType.RubTypeName + "]";
            }
            BasRubTyrePart rubTyrePart = tyrePartManager.GetById(rubInfo.TyrePartID);
            if (rubTyrePart != null && "1".Equals(rubTyrePart.DeleteFlag))
            {
                return "恢复失败：请先恢复胶料用途[" + rubTyrePart.TyrePartName + "]";
            }
            rubInfo.DeleteFlag = "0";
            manager.Update(rubInfo);
            this.AppendWebLog("胶料信息恢复", "胶料代码：" + rubInfo.RubCode);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
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
            try
            {
                EntityArrayList<BasRubInfo> workList = manager.GetListByWhere(BasRubInfo._.RubName == add_rub_name.Text.TrimStart().TrimEnd());
                if (workList.Count > 0)
                {
                    X.Msg.Alert("提示", "此胶料名称已被使用！").Show();
                    return;
                }
            }
            catch (Exception ex )
            { X.Msg.Alert("提示", "此胶料名称已被使用！").Show();
                    return;}
            //BasRubInfo rubInfo = new BasRubInfo();
            //string nextRubInfoCode = manager.GetNextRubInfoCode();
            //rubInfo.ObjID = Convert.ToInt32(nextRubInfoCode);
            //rubInfo.RubCode = nextRubInfoCode;
            //rubInfo.RubName = (string)(add_rub_name.Text);
            //rubInfo.RubOtherName = (string)(add_rub_other_name.Text);
            //rubInfo.RubTypeCode = hidden_rub_type_id.Text;
            //if (add_rub_purpose.Text != "")
            //{
            //    rubInfo.TyrePartID = Convert.ToInt32(hidden_rub_purpose.Text);
            //    rubInfo.RubPurpose = Convert.ToInt32(hidden_rub_purpose.Text).ToString();
            //}
            //rubInfo.RubRate = add_rub_rate.Text != "" ? Convert.ToInt32(add_rub_rate.Text) : 0;
            //rubInfo.FactoryID = Convert.ToInt32(hidden_factory_id.Text);
            //rubInfo.Remark = (string)(add_remark.Text);
            //rubInfo.DeleteFlag = "0";
            //manager.Insert(rubInfo);

            string sql = "select MAX(Rub_typecode)+1 from Pmt_rub where Rub_type = '"+hidden_rub_type_id.Text+"'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            string Rub_typecode = ds.Tables[0].Rows[0][0].ToString().PadLeft(4,'0');
            if (Rub_typecode == "0000")
            {
                Rub_typecode = hidden_rub_type_id.Text + "001";
            }

            string Rub_code = Rub_typecode.Substring(1,3);
            double rate = 0;
            if (add_rub_rate.Text == "")
            { rate = 0; }
            else { rate = Convert.ToDouble(add_rub_rate.Text); }
            sql = "insert into Pmt_rub(Rub_typecode,Rub_code,Rub_type,Rub_name,Rub_purpose,least_flag,rub_rate,remark,Rub_byname ) values('" + Rub_typecode + "','" + Rub_code + "','" + hidden_rub_type_id.Text + "','" + add_rub_name.Text + "','" + add_rub_purpose.Text + "','1','" + rate + "','" + add_remark.Text + "','" + add_rub_other_name.Text + "')";
			
			ds = manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("胶料信息添加", "胶料代码：" + Rub_code);
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
            string strsql = "select * from BasRubInfo where RubName='"+ modify_rub_name.Text + "'";
            DataSet dt = manager.GetBySql(strsql).ToDataSet();
            if (dt.Tables[0].Rows.Count>0) {
                if (dt.Tables[0].Rows[0]["RubName"].ToString() != hidden_rubname.Text)
                {
                    X.Msg.Alert("提示", "此胶料名称已被使用！").Show();
                    return;
                }
            }

            string sql;
            if (string.IsNullOrEmpty(modify_rub_rate.Text))
            {
                sql = @" update BasRubInfo set 
                            rubname = '" + modify_rub_name.Text + "' ,RubTypeCode = '" + hidden_rub_type_id.Value.ToString() + "' ,remark= '" + modify_remark.Text
                                                         + "',RubRate = NULL ,RubOtherName='" + modify_rub_other_name.Text + "', rubpurpose = '" + modify_rub_purpose.Text + "' where objid = '" + Convert.ToInt32(modify_obj_id.Text) + "'";
            }
            else
            {
                try { double d = double.Parse(modify_rub_rate.Text); }
                catch (Exception exx)
                {
                    X.Msg.Alert("提示", "含胶率必须为数字或空！").Show();
                    return;
                }
                sql = @" update BasRubInfo set 
                            rubname = '" + modify_rub_name.Text + "' ,RubTypeCode = '" + hidden_rub_type_id.Value.ToString() + "' ,remark= '" + modify_remark.Text
                                                      + "',RubRate ='" + modify_rub_rate.Text + "',RubOtherName='" + modify_rub_other_name.Text + "', rubpurpose = '" + modify_rub_purpose.Text + "' where objid = '" + Convert.ToInt32(modify_obj_id.Text) + "'";

            } DataSet ds = manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("胶料信息修改", "胶料代码：" + modify_obj_id.Text);
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
    /// 检查胶料名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckRubName(object sender, RemoteValidationEventArgs e)
    {
        return;
        TextField field = (TextField)sender;
        string rubname = field.Text;
        EntityArrayList<BasRubInfo> rubInfoList = manager.GetListByWhere(BasRubInfo._.RubName == rubname && BasRubInfo._.DeleteFlag == "0");
        if (rubInfoList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (rubInfoList[0].RubName.ToString() == hidden_rubname.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此胶料名称已被使用！";
            }
        }
    }
    #endregion

    #region 设置人员关联权限
    [Ext.Net.DirectMethod()]
    protected void set_Rub_Power_User(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] rubDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasRubInfo> rubList = new List<BasRubInfo>();
        foreach (Dictionary<string, string> row in rubDic)
        {
            BasRubInfo rub = new BasRubInfo();
            rub.RubCode = row["RubCode"];
            hidden_set_power_user_rub_code.Text = rub.RubCode;
            rubList.Add(rub);
        };
        if (rubList.Count > 0)
        {
            PageResult<SysRubPowerUser> lst = GetPageResultDataByRubCode(rubList[0].RubCode);
            DataTable dt = lst.DataSet.Tables[0];
            SetUserStore.Data = dt;
            SetUserStore.DataBind();
            AllUserPageToolbar.DoRefresh();
            winSetRubPower.Show();
        }
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnSetPowerCanel_Click(object sender, DirectEventArgs e)
    {
        txtUserName.Text = "";
        txtHRCode.Text = "";
        this.winSetRubPower.Close();
    }

    /// <summary>
    /// 点击确定按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnSetPowerSave_Click(object sender, DirectEventArgs e)
    {
        string rubCode = hidden_set_power_user_rub_code.Text;
        rubPowerUserManager.DeleteByWhere(SysRubPowerUser._.RubCode == rubCode);
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] rubDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in rubDic)
        {
            SysRubPowerUser rubPowerUser = new SysRubPowerUser();
            rubPowerUser.RubCode = rubCode;
            rubPowerUser.WorkBarcode = row["WorkBarcode"];
            rubPowerUser.DeleteFlag = "0";
            rubPowerUserManager.Insert(rubPowerUser);
        }
        this.winSetRubPower.Close(); 
        pageToolBar.DoRefresh();
        txtUserName.Text = "";
        txtHRCode.Text = "";
        this.msg.Notify("提示", "权限设置成功").Show();
    }

    /// <summary>
    /// 校验拖拽人员重复性的方法
    /// </summary>
    /// <param name="usercode"></param>
    /// <param name="jsonStr"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod]
    public string set_user_power_drop(string usercode, string jsonStr)
    {
        EntityArrayList<BasUser> userList = new EntityArrayList<BasUser>();
        Dictionary<string, string>[] rubDic = JSON.Deserialize<Dictionary<string, string>[]>(jsonStr);
        string workBarcodes = "";
        foreach (Dictionary<string, string> row in rubDic)
        {
            BasUser user = new BasUser();
            user.UserName = row["UserName"];
            user.WorkBarcode = row["WorkBarcode"];
            user.HRCode = row["HRCode"];
            userList.Add(user);
            workBarcodes += row["WorkBarcode"] + "|";
        }
        if (workBarcodes.IndexOf(usercode) == -1)
        {
            EntityArrayList<BasUser> addUser = userManager.GetListByWhere(BasUser._.WorkBarcode == usercode);
            if (addUser.Count > 0)
            {
                userList.Add(addUser[0]);
                SetUserStore.Data = userList;
                SetUserStore.DataBind();
                return "添加用户:"+addUser[0].UserName+"成功";
            }
            return "添加用户异常，请联系管理员";
        }
        else
        {
            SetUserStore.Data = userList;
            SetUserStore.DataBind();

            return "请勿重复添加此用户";
        }
        
    }
    /// <summary>
    /// 绑定胶料权限用户的查询分页方法
    /// 2013年11月21日 yuany
    /// </summary>
    /// <param name="rubCode"></param>
    /// <returns></returns>
    private PageResult<SysRubPowerUser> GetPageResultDataByRubCode(string rubCode)
    {
        SysRubPowerUserManager.QueryParams queryParams = new SysRubPowerUserManager.QueryParams();
        PageResult<SysRubPowerUser> pageParams = new PageResult<SysRubPowerUser>();
        queryParams.pageParams = pageParams;
        queryParams.rubCode = rubCode;
        queryParams.deleteFlag = "0";

        return rubPowerUserManager.GetTablePageDataBySql(queryParams);
    }

    /// <summary>
    /// 绑定用户的查询分页方法
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<BasUser> GetPageResultUserData(PageResult<BasUser> pageParams)
    {
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.userName = txtUserName.Text.TrimEnd().TrimStart();
        queryParams.hrcode = txtHRCode.Text.TrimEnd().TrimStart();

        return userManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindUserData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasUser> lst = GetPageResultUserData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}