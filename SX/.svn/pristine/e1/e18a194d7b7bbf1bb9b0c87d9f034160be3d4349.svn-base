using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Entity.Custom;

/// <summary>
/// Manager_Authentication_Login 实现类
/// 孙本强 @ 2013-04-03 13:09:55
/// </summary>
/// <remarks></remarks>
public partial class Manager_Authentication_Login : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:55
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:09:55
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.InitDbVersion();           //初始化数据版本(帐套)
        }
    }
    #endregion
    /// <summary>
    /// 用户登录
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:55
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Button1_Click(object sender, DirectEventArgs e)
    {
        #region 获取使用的数据版本(帐套)

        CdbVersion dbVersion = XmlDbConfigHandler.GetInstance().GetDbVersions()[Convert.ToInt32(this.ddlDbVersion.SelectedItem.Value)];
        CdbDatabase db = dbVersion.Databases["Default"];
        this.basUserManager = new BasUserManager(new NBear.Data.Gateway(db.AssemblyName, db.ClassName, db.ConnStr));

        #endregion

        #region 系统用户添加
        /*
       INSERT INTO dbo.BasUser
        ( UserName ,
          UserPWD ,
          RealName ,
          Sex ,
          Telephone ,
          WorkBarcode ,
          DeptID ,
          WorkID ,
          ShiftID ,
          WorkShopID ,
          DeleteFlag ,
          Remark
        )
VALUES  ( 'admin' , -- UserName - varchar(50)
          '42EE1EC67DA6' , -- UserPWD - varchar(100)
          '系统管理员' , -- RealName - varchar(50)
          '1' , -- Sex - char(1)
          '' , -- Telephone - varchar(50)
          '' , -- WorkBarcode - varchar(20)
          0 , -- DeptID - int
          0 , -- WorkID - int
          0 , -- ShiftID - int
          0 , -- WorkShopID - int
          '0' , -- DeleteFlag - char(1)
          ''  -- Remark - varchar(100)
        )
         */
        #endregion

        BasUser loginUser = new BasUser();
        loginUser.HRCode = this.txtUsername.Text;
        loginUser.UserPWD = this.txtPassword.Text;
        BasUser currUser = null;
        if (this.basUserManager.Login(loginUser, out currUser))
        {
            //保存当前用户使用的数据版本（帐套）
            Session["dbVersion"] = dbVersion;
            Session["MyReturnUrl"] = this.Request.Url.AbsolutePath;  //保存起始登录页的访问路径
            this.AppendWebLog();
            Response.Redirect("~/Manager/MainFrame.aspx");
        }
        else
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "用户名称或密码错误！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
        }
    }

    #region 初始化数据版本(帐套)
    /// <summary>
    /// 初始化数据版本
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:56
    /// </summary>
    /// <remarks></remarks>
    private void InitDbVersion()
    {
        Dictionary<int, CdbVersion> dbVersions = XmlDbConfigHandler.GetInstance().GetDbVersions();
        foreach (CdbVersion version in dbVersions.Values)
        {
            Ext.Net.ListItem item = new ListItem();
            item.Value = version.ObjID.ToString();
            item.Text = version.DbVersion;
            this.ddlDbVersion.Items.Add(item);
        }
        if (this.ddlDbVersion.Items.Count > 0) this.ddlDbVersion.Select(0);
    }
    #endregion
}