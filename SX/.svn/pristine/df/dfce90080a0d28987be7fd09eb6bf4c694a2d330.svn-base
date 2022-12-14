using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mesnac.Entity;
using Mesnac.Entity.Custom;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using System.Data;


/// <summary>
/// Index 实现类
/// 孙本强 @ 2013-04-03 13:07:42
/// </summary>
/// <remarks></remarks>
public partial class Index : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:07:43
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:07:43
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
    
    #region 初始化数据版本(帐套)
    /// <summary>
    /// 初始化数据版本
    /// 孙本强 @ 2013-04-03 13:07:43
    /// </summary>
    /// <remarks></remarks>
    private void InitDbVersion()
    {
        Dictionary<int, CdbVersion> dbVersions = XmlDbConfigHandler.GetInstance().GetDbVersions();
        foreach (CdbVersion version in dbVersions.Values)
        {
            ListItem item = new ListItem();
            item.Value = version.ObjID.ToString();
            item.Text = version.DbVersion;
            this.ddlDbVersion.Items.Add(item);
        }
        if (this.ddlDbVersion.Items.Count <= 1)
        {
            this.trDbVersion.Visible = false;
        }
    }
    #endregion

    /// <summary>
    /// Handles the Click event of the btnSubmit control.
    /// 孙本强 @ 2013-04-03 13:07:43
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region 获取使用的数据版本(帐套)

        CdbVersion dbVersion = XmlDbConfigHandler.GetInstance().GetDbVersions()[Convert.ToInt32(this.ddlDbVersion.SelectedItem.Value)];
        CdbDatabase db = dbVersion.Databases["Default"];

        //保存当前用户使用的数据版本（帐套）
        Session["dbVersion"] = dbVersion;

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

        BasUser currUser = new BasUser();
        currUser.HRCode = this.txtUserName.Text.Trim();
        currUser.UserPWD = this.txtPassword.Text.Trim();
        if (this.basUserManager.Login(currUser, out currUser))
        {
            //保存当前用户使用的数据版本（帐套）
            //Session["dbVersion"] = dbVersion;

            String sql = "select * from SysUserCtrl where objid ='23'";
            DataSet ds = basUserManager.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                string daynum = ds.Tables[0].Rows[0]["ItemCode"].ToString();
            if(daynum != "0")
                {

                    sql = "select DateAdd(day,7,recordtime),* from Basuser where HRCode ='" + currUser.HRCode + "'and DateAdd(day," + daynum + ",recordtime)>getdate()";
                    ds = basUserManager.GetBySql(sql).ToDataSet();
                    if (ds.Tables[0].Rows.Count == 0 && (currUser.HRCode != "000001"))
                    {
                        //cv.ErrorMessage = "密码超期请修改密码";
                        //this.cv.IsValid = false;
                        //return;
                    
                    }
            
            }
            
            
            }




            Session["MyReturnUrl"] = this.Request.Url.AbsolutePath;  //保存起始登录页的访问路径
            this.AppendWebLog();
            Response.Redirect("~/Manager/MainFrame.aspx");
        }
        else
        {
            cv.ErrorMessage = "用户名称或密码错误";
            this.cv.IsValid = false;
        }
    }

    protected void btnChangePS_Click(object sender, EventArgs e)
    {
      
        //this.Response.Write(" <script language=javascript >   window.open('Manager/System/SysUser/MyUser.aspx','修改密码','width=200,height=200') </script>");

        form2.Visible = true;
    
    }


    protected void btnChangeC_Click(object sender, EventArgs e)
    {

      
        form2.Visible = false;

    }
    protected void btnChangeA_Click(object sender, EventArgs e)
    {


        form2.Visible = false;

    }
}