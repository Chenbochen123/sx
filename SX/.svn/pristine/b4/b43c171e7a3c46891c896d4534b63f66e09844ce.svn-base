using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;


/// <summary>
/// Manager_System_SysUser_MyUser 实现类
/// 孙本强 @ 2013-04-03 13:08:37
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_SysUser_MyUser : Mesnac.Web.UI.Page
{

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:08:38
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:08:38
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["PlanID"] == null)
        //{
        //    return;
        //}

        if (!X.IsAjaxRequest)
        {
       
            string userid = this.UserID;
         
            BasUser user = basUserManager.GetListByWhere(BasUser._.WorkBarcode == userid)[0];
            txtUserCode.Text = user.HRCode;
            txtUserName.Text = user.UserName;
            txtUserRealName.Text = user.RealName;
        }
    }
    #endregion

    /// <summary>
    /// 用户信息修改
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:38
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string userid = this.UserID;
        BasUser upuser = basUserManager.GetListByWhere(BasUser._.WorkBarcode == userid)[0];
        if (txtUserNewPassword1.Text.Trim().Length > 0)
        {
            if (txtUserNewPassword1.Text.Trim().Length == 0)
            {
                X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "修改密码必须输入原密码！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
                return;
            }
            if (txtUserNewPassword1.Text.Trim() != txtUserNewPassword2.Text.Trim())
            {
                X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "两次新密码不相同！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
                return;
            }
            BasUser user = basUserManager.GetListByWhere(BasUser._.WorkBarcode == userid)[0];
            string spassword = new Mesnac.Util.Cryptography.MesnacEngine().DecryptString(user.UserPWD.Trim(), string.Empty, Encoding.ASCII);
            if (spassword != txtUserPassword.Text)
            {
                X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "原密码不正确！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
                return;
            }
            upuser.UserPWD = new Mesnac.Util.Cryptography.MesnacEngine().EncryptString(txtUserNewPassword1.Text.Trim(), string.Empty, Encoding.ASCII);
        }
        upuser.RealName = txtUserRealName.Text == "" ? txtUserName.Text : txtUserRealName.Text;
        //basUserManager.Update(upuser);




        String sql = "update SYS_USER set real_name = '" + upuser.RealName + "',PASSWORD = '" + upuser.UserPWD + "' where Worker_barcode = '" + upuser.WorkBarcode + "'";
        basUserManager.GetBySql(sql).ToDataSet(); 
        txtUserPassword.Text = string.Empty;
        txtUserNewPassword1.Text = string.Empty;
        txtUserNewPassword2.Text = string.Empty;
        //this.AppendWebLog("用户信息修改成功", "用户信息修改成功");
      
        X.Msg.Show(new MessageBoxConfig { Title = "成功提示", Message = "用户信息修改成功！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
    }
}