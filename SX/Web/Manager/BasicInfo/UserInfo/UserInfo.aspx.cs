﻿using System;
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
using System.Text;
using System.IO;

public partial class Manager_BasicInfo_UserInfo_UserInfo : Mesnac.Web.UI.Page
{
    protected BasUserManager manager = new BasUserManager();//业务对象
    protected SYS_USERManager Sysmanager = new SYS_USERManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected BasDeptManager deptManager = new BasDeptManager();
    protected BasWorkManager workManager = new BasWorkManager();
    protected BasWorkShopManager workshopManager = new BasWorkShopManager();
    protected PptClassManager shiftManager = new PptClassManager();
    private EntityArrayList<BasUser> entityList;

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            删除 = new SysPageAction() { ActionID = 2, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
            查询 = new SysPageAction() { ActionID = 4, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 5, ActionName = "btn_history_search" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            导出 = new SysPageAction() { ActionID = 7, ActionName = "btnExport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            //EntityArrayList<SysCode> sexList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "Sex");
            //foreach (SysCode sex in sexList)
            //{
            //    Ext.Net.ListItem item = new Ext.Net.ListItem(sex.ItemName, sex.ItemCode);
            //    modify_sex.Items.Add(item);
            //    add_sex.Items.Add(item);
            //}

            string sql = "select * from JCZL_work";
            DataTable dt = manager.GetBySql(sql).ToDataSet().Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Ext.Net.ListItem li = new Ext.Net.ListItem(dr["work_name"].ToString(), dr["work_num"].ToString());
                workA.Items.Add(li);
                workB.Items.Add(li);
                workC.Items.Add(li);
                workD.Items.Add(li);
                workE.Items.Add(li);
                workF.Items.Add(li);
            }

        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasUser> GetPageResultData(PageResult<BasUser> pageParams)
    {
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams = pageParams;
        //queryParams.hrcode = txt_work_barcode.Text.TrimEnd().TrimStart();
        queryParams.userName = txt_user_name.Text.TrimEnd().TrimStart();
        queryParams.deptCode = hidden_select_depart_code.Text;
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasUser> GetTablePageDataBySql(BasUserManager.QueryParams queryParams)
    {
        PageResult<BasUser> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT	    u.ObjID as ObjID, u.UserName as UserName, u.UserPWD as UserPWD, u.RealName as RealName, 
                                            '' as Sex, u.Telephone as Telephone, u.WorkBarcode as WorkBarcode, dep.DepName as DeptCode, 
                                            work.WorkName as WorkID, shift.ClassName as ShiftID, workshop.WorkShopName as WorkShopID,  
                                            u.Remark , u.HRCode , u.ERPCode , u.DeleteFlag,
                                            workA.WorkName as WorkA,workB.WorkName as WorkB,workC.WorkName as WorkC,workD.WorkName as WorkD,workE.WorkName as WorkE,workF.WorkName as WorkF,
                                            CA.name as A,CB.name as B,CC.name as C,CD.name as D,CE.name as E,CF.name as F,
                                            A_fen,B_fen,C_fen,D_fen,E_fen,F_fen
                                 FROM	    BasUser u 
                                 LEFT JOIN  BasDept dep  on u.DeptCode = dep.DepCode 
                                 LEFT JOIN  BasWork work on u.WorkID = work.ObjID  
                                 LEFT JOIN  PptClass shift on u.ShiftID = shift.ObjID  
                                 LEFT JOIN  BasWorkShop workshop on u.WorkShopID = workshop.ObjID  
                               LEFT JOIN  BasWork workA on u.work_typeA = workA.ObjID  
                               LEFT JOIN  BasWork workB on u.work_typeB = workB.ObjID  
                               LEFT JOIN  BasWork workC on u.work_typeC = workC.ObjID  
                               LEFT JOIN  BasWork workD on u.work_typeD = workD.ObjID  
                               LEFT JOIN  BasWork workE on u.work_typeE = workE.ObjID  
                               LEFT JOIN  BasWork workF on u.work_typeF = workF.ObjID  
                               LEFT JOIN  SYS_Color CA on u.A = CA.ID  
                               LEFT JOIN  SYS_Color CB on u.B = CB.ID  
                               LEFT JOIN  SYS_Color CC on u.C = CC.ID  
                               LEFT JOIN  SYS_Color CD on u.D = CD.ID  
                               LEFT JOIN  SYS_Color CE on u.E = CE.ID  
                               LEFT JOIN  SYS_Color CF on u.F = CF.ID  
                                 WHERE      1 = 1 ");
        if (!string.IsNullOrEmpty(queryParams.objID))
        {
            sqlstr.AppendLine(" AND u.ObjID = " + queryParams.objID);
        }
        if (!string.IsNullOrEmpty(queryParams.realname))
        {
            sqlstr.AppendLine(" AND u.RealName like '%" + queryParams.realname + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.userName))
        {
            sqlstr.AppendLine(" AND u.UserName like '%" + queryParams.userName + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.workbarcode))
        {
            sqlstr.AppendLine(" AND u.WorkBarcode like '%" + queryParams.workbarcode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.deptCode))
        {
            sqlstr.AppendLine(" AND u.DeptCode = " + queryParams.deptCode);
        }
        if (!string.IsNullOrEmpty(queryParams.workID))
        {
            sqlstr.AppendLine(" AND u.WorkID = " + queryParams.workID);
        }
        if (!string.IsNullOrEmpty(queryParams.shiftID))
        {
            sqlstr.AppendLine(" AND u.ShiftID = " + queryParams.shiftID);
        }
        if (!string.IsNullOrEmpty(queryParams.workshopID))
        {
            sqlstr.AppendLine(" AND u.WorkShopID = " + queryParams.workshopID);
        }
        if (!string.IsNullOrEmpty(queryParams.hrcode))
        {
            sqlstr.AppendLine(" AND u.HRCode = '" + queryParams.hrcode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND u.DeleteFlag ='" + queryParams.deleteFlag + "'");
        }
        if (!string.IsNullOrEmpty(Tgw.Text))
        {
            sqlstr.AppendLine(" AND  work.WorkName like '%" + Tgw.Text + "%'");
        }
        sqlstr.AppendLine(" Order by u.WorkBarCode ");
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            //return this.GetPageDataBySql(pageParams);
            return manager.GetPageDataByReader(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //if (!Regex.IsMatch(txt_work_barcode.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        //{
        //    txt_work_barcode.Text = "";
        //}
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "WorkBarCode ASC";

        PageResult<BasUser> lst = GetPageResultData(pageParams);
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
        //if (!Regex.IsMatch(txt_work_barcode.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        //{
        //    txt_work_barcode.Text = "";
        //}
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageSize = -100;
        PageResult<BasUser> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "用户信息");
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
        add_user_name.Text = "";
        add_sex.Text = "";
        add_telephone.Text = "";
        add_dept_code.Text = "";
        add_work_id.Text = "";
        add_shift_id.Text = "";
        add_remark.Text = "";
        btnAddSave.Disable(true);
        Ahidden_work_id.Text = "";
        Ahidden_depart_code.Text = "";
        Ahidden_shift_id.Text = "";


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
        //modify_dept_code.Text = "";
        //modify_work_id.Text = "";
        //modify_shift_id.Text = "";

        string sql = "select * from BasUser where WorkBarcode='"+ obj_id + "'";
       DataSet ds= manager.GetBySql(sql).ToDataSet();

        modify_obj_id.Value = ds.Tables[0].Rows[0]["WorkBarcode"].ToString();
        hidden_username.Value = ds.Tables[0].Rows[0]["UserName"].ToString();
        modify_user_name.Value = ds.Tables[0].Rows[0]["UserName"].ToString();
        modify_real_name.Value = ds.Tables[0].Rows[0]["RealName"].ToString();
        //modify_sex.Select(user.Sex.ToString());
        modify_telephone.Value = ds.Tables[0].Rows[0]["Telephone"].ToString();
        hidden_workcode.Value = ds.Tables[0].Rows[0]["WorkBarcode"].ToString();
        modify_work_barcode.Value = ds.Tables[0].Rows[0]["WorkBarcode"].ToString();
        modify_remark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
        try
        {
            hidden_depart_code.Value = ds.Tables[0].Rows[0]["DeptCode"].ToString();
        }
        catch (Exception)
        {

        }
        try
        {
            hidden_work_id.Value = ds.Tables[0].Rows[0]["WorkID"].ToString();
        }
        catch (Exception)
        {

        }
        try
        {
            hidden_shift_id.Value = ds.Tables[0].Rows[0]["ShiftID"].ToString();
        }
        catch (Exception)
        {
        }
        try
        {
            hidden_workshop_id.Value = ds.Tables[0].Rows[0]["WorkShopID"].ToString(); 
        }
        catch (Exception)
        {
        }
        workA.Text = ds.Tables[0].Rows[0]["Work_typeA"].ToString();
        A.Text = ds.Tables[0].Rows[0]["A"].ToString();
        A_fen.Text = ds.Tables[0].Rows[0]["A_fen"].ToString();

        workB.Text = ds.Tables[0].Rows[0]["Work_typeB"].ToString();
        B.Text = ds.Tables[0].Rows[0]["B"].ToString();
        B_fen.Text = ds.Tables[0].Rows[0]["B_fen"].ToString();

        workC.Text = ds.Tables[0].Rows[0]["Work_typeC"].ToString();
        C.Text = ds.Tables[0].Rows[0]["C"].ToString();
        C_fen.Text = ds.Tables[0].Rows[0]["C_fen"].ToString();

        workD.Text = ds.Tables[0].Rows[0]["Work_typeD"].ToString();
        D.Text = ds.Tables[0].Rows[0]["D"].ToString();
        D_fen.Text = ds.Tables[0].Rows[0]["D_fen"].ToString();

        workE.Text = ds.Tables[0].Rows[0]["Work_typeE"].ToString();
        E.Text = ds.Tables[0].Rows[0]["E"].ToString();
        E_fen.Text = ds.Tables[0].Rows[0]["E_fen"].ToString();

        workF.Text = ds.Tables[0].Rows[0]["Work_typeF"].ToString();
        F.Text = ds.Tables[0].Rows[0]["F"].ToString();
        F_fen.Text = ds.Tables[0].Rows[0]["F_fen"].ToString();
        Im.ImageUrl = "ContractPhoto.aspx?ProjectID=" + ds.Tables[0].Rows[0]["WorkBarcode"].ToString();
        UploadImage.Clear();
        UploadImage.Reset();
      
    
        //UploadImage.ImageUrl = string.Empty;
        //UploadImage.FileName = "";
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
            BasUser user = manager.GetById(obj_id);
            user.DeleteFlag = "0";
            manager.Update(user);
            this.AppendWebLog("用户信息恢复", "用户工号：" + user.WorkBarcode);
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
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            if (obj_id == "00001")
            {
                return "超级管理员禁止删除!";
            }
            //BasUser user = manager.GetById(obj_id);
            //user.DeleteFlag = "1";

            string sql = "delete SYS_USER where  USER_ID='" + obj_id + "'";
            manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("用户信息删除", "用户工号：" + obj_id);
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
        //X.Js.Alert(Ahidden_depart_code.Text); return;
        try
        { //添加校验重复
            EntityArrayList<BasUser> userList = manager.GetListByWhere(BasUser._.UserName == add_user_name.Text.TrimStart().TrimEnd());
            if (userList.Count > 0)
            {
                X.Msg.Alert("提示", "此用户名称已被使用！").Show();
                return;
            }

            string sql = "select MAX(USER_ID)+1 from SYS_USER";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            string uid = ds.Tables[0].Rows[0][0].ToString();
            while (uid.Length < 5) uid = "0" + uid;
            SYS_USER User = new SYS_USER();
            User.USER_ID = uid;
            User.USER_NAME = add_user_name.Text;
            User.Real_name = add_user_name.Text;
            User.PASSWORD = "C5BBA09E";
            if (string.IsNullOrEmpty(Ahidden_work_id.Text)) User.Work_type = null;
            else
            User.Work_type = short.Parse(Ahidden_work_id.Text);
            User.Worker_barcode = uid;
            User.Depart_code = Ahidden_depart_code.Text;
            User.Shift_class = Ahidden_shift_id.Text;
            User.Mob_telephone = add_telephone.Text;
            User.Remark = add_remark.Text;
            User.Is_employee = Convert.ToBoolean(1);
            Sysmanager.Insert(User);
            //sql = "insert into SYS_USER(USER_ID,USER_NAME,PASSWORD,ROLE_ID,Worker_barcode,depart_code,Work_type,Mob_telephone) values('" +
            //    ds .Tables[0].Rows[0][0].ToString()+ "','"+ add_user_name.Text + "','C5BBA09E','"+ hidden_work_id.Text + "','" + 
            //    ds.Tables[0].Rows[0][0].ToString() + "','"+ hidden_depart_code.Text + "','"+ hidden_work_id.Text+ "','"+ add_telephone.Text + "')";
            //manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("用户信息添加", "用户工号：" + ds.Tables[0].Rows[0][0].ToString());
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Notify("操作", "保存成功").Show();
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
            //EntityArrayList<BasUser> userList = manager.GetListByWhere(BasUser._.UserName == modify_user_name.Text.TrimStart().TrimEnd());
            //if (userList.Count > 0)
            //{
            //    if (userList[0].UserName != hidden_username.Text)
            //    {
            //        X.Msg.Alert("提示", "此用户名称已被使用！").Show();
            //        return;
            //    }
            //}


         
            SYS_USER USER = Sysmanager.GetListByWhere(SYS_USER._.USER_ID == modify_obj_id.Text)[0];

            string fileFullName = this.UploadImage.FileName;  //文件完整名称
            string fileName = fileFullName.Substring(fileFullName.LastIndexOf("\\") + 1);   //文件名
            string type = fileName.Substring(fileName.LastIndexOf(".") + 1);    //文件类型
            //if (type.ToLower() == "bmp" || type.ToLower() == "jpg" || type.ToLower() == "gif" || type.ToLower() == "png")
            if (type.ToLower() == "bmp")
            {
                string fileFullPath = Server.MapPath("~/upload") + "\\" + fileName;
                UploadImage.PostedFile.SaveAs(fileFullPath);

                //将文件转化为数据流
                FileStream fs = new FileStream(fileFullPath, FileMode.Open);    //文件流
                byte[] imageBytes = new byte[fs.Length];  //设定字节流数组大小
                BinaryReader br = new BinaryReader(fs);     //读字节对象
                imageBytes = br.ReadBytes(Convert.ToInt32(fs.Length));  //将字节流读入字节数组中
                //Scrollable="Both"
                //将字节流对象赋值给Project对象
                USER.Pic = imageBytes;
                fs.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(type))
                {
                    X.Js.Alert("图片必须为bmp格式");
                    return;
                }
            }

           

            //X.Msg.Notify(hidden_work_id.Text, hidden_shift_id.Text).Show();
            //return;

            USER.Remark = modify_remark.Text;
            USER.Real_name = modify_real_name.Text;
            USER.Mob_telephone = modify_telephone.Text;
            if (string.IsNullOrEmpty(hidden_work_id.Text)) USER.Work_type = null;
            else
            USER.Work_type = short.Parse(hidden_work_id.Text);
            USER.Depart_code = hidden_depart_code.Text;
            USER.Shift_class = hidden_shift_id.Text;
      
            if (!string.IsNullOrWhiteSpace(workA.Value.ToString()))
                USER.Work_typeA = int.Parse(workA.Value.ToString());
       
            if (!string.IsNullOrWhiteSpace(A.Value.ToString()))
                USER.A = int.Parse(A.Value.ToString());
            USER.A_fen = A_fen.Text == "" ? 0 : Convert.ToInt32(A_fen.Text);


            if (!string.IsNullOrWhiteSpace(workB.Value.ToString()))
                USER.Work_typeB = int.Parse(workB.Value.ToString());

            if (!string.IsNullOrWhiteSpace(B.Value.ToString()))
                USER.B = int.Parse(B.Value.ToString());
            USER.B_fen = B_fen.Text == "" ? 0 : Convert.ToInt32(B_fen.Text);

            if (!string.IsNullOrWhiteSpace(workC.Value.ToString()))
                USER.Work_typeC = int.Parse(workC.Value.ToString());

            if (!string.IsNullOrWhiteSpace(C.Value.ToString()))
                USER.C = int.Parse(C.Value.ToString());
            USER.C_fen = C_fen.Text == "" ? 0 : Convert.ToInt32(C_fen.Text);

            if (!string.IsNullOrWhiteSpace(workD.Value.ToString()))
                USER.Work_typeD = int.Parse(workD.Value.ToString());

            if (!string.IsNullOrWhiteSpace(D.Value.ToString()))
                USER.D = int.Parse(D.Value.ToString());
            USER.D_fen = D_fen.Text == "" ? 0 : Convert.ToInt32(D_fen.Text);

            if (!string.IsNullOrWhiteSpace(workE.Value.ToString()))
                USER.Work_typeE = int.Parse(workE.Value.ToString());

            if (!string.IsNullOrWhiteSpace(E.Value.ToString()))
                USER.E = int.Parse(E.Value.ToString());
            USER.E_fen = E_fen.Text == "" ? 0 : Convert.ToInt32(E_fen.Text);

            if (!string.IsNullOrWhiteSpace(modify_user_name.Text))
                USER.USER_NAME = modify_user_name.Text;

            if (!string.IsNullOrWhiteSpace(workF.Value.ToString()))
                USER.Work_typeF = int.Parse(workF.Value.ToString());

            if (!string.IsNullOrWhiteSpace(F.Value.ToString()))
                USER.F = int.Parse(F.Value.ToString());
            USER.F_fen = F_fen.Text == "" ? 0 : Convert.ToInt32(F_fen.Text);
            Sysmanager.Update(USER);
            //string sql = "update  SYS_USER set depart_code='"+ hidden_depart_code.Text + "',Mob_telephone='"+ modify_telephone.Text + "',Shift_class='"+ hidden_shift_id.Text + "',Work_type='"+ hidden_work_id.Text+ "' where USER_ID='" + modify_obj_id.Text + "'";
            //manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("用户信息修改", "用户工号：" + modify_obj_id.Text);

            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "更新成功").Show();
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
    /// 检查工号是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckWorkBarcode(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string workcode = field.Text;
        EntityArrayList<BasUser> userList = manager.GetListByWhere(BasUser._.WorkBarcode == workcode);
        if (userList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (userList[0].WorkBarcode.ToString() == hidden_workcode.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此员工工号已被使用！";
            }
        }
    }
    /// <summary>
    /// 检查用户名是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckHRName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string hrname = field.Text;
        EntityArrayList<BasUser> userList = manager.GetListByWhere(BasUser._.HRCode == hrname);
        if (userList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (userList[0].UserName.ToString() == hidden_username.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此HR编号已被使用！";
            }
        }
    }
    #endregion
}