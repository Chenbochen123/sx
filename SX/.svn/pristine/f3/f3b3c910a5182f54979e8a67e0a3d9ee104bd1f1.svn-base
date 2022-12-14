using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Manager_Authentication_HostRegister 实现类
/// 孙本强 @ 2013-04-03 13:09:48
/// </summary>
/// <remarks></remarks>
public partial class Manager_Authentication_HostRegister : Mesnac.Web.UI.Page
{
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:49
    /// </summary>
    private readonly string RegFilePath = "~/App_Data/host.dat";

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:09:49
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsRegister())
        {
            this.Response.Redirect("~/");
        }
        else
        {
            string strRegCode = this.txtRegCode.Text;
            if (!String.IsNullOrWhiteSpace(strRegCode))
            {
                if (strRegCode == this.GetRegCode())
                {
                    string realRegFilePath = this.Server.MapPath(this.RegFilePath);
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.FileStream fs = new System.IO.FileStream(realRegFilePath, System.IO.FileMode.Create);
                    formater.Serialize(fs, strRegCode);
                    fs.Flush();
                    fs.Close();

                    this.Application["regCode"] = strRegCode;
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('您输入的注册码不正确!');</script>");
                }
            }
        }

    }
    #endregion

    /// <summary>
    /// Determines whether this instance is register.
    /// 孙本强 @ 2013-04-03 13:09:49
    /// </summary>
    /// <returns><c>true</c> if this instance is register; otherwise, <c>false</c>.</returns>
    /// <remarks></remarks>
    protected bool IsRegister()
    {
        string regCode = this.GetRegCode();

        string realRegFilePath = this.Server.MapPath(this.RegFilePath);

        if (System.IO.File.Exists(realRegFilePath))
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.FileStream fs = new System.IO.FileStream(realRegFilePath, System.IO.FileMode.Open);
            string str = formater.Deserialize(fs) as string;
            fs.Close();
            if (regCode == str)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Gets the reg code.
    /// 孙本强 @ 2013-04-03 13:09:49
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRegCode()
    {
        string regCode = this.Application["regCode"] as string;
        if (String.IsNullOrWhiteSpace(regCode))
        {
            string cpuId = Mesnac.Util.CommonUtil.GetCpuId();
            regCode = Mesnac.Util.CommonUtil.EncryptString(cpuId + Mesnac.Util.CommonUtil.GetMainHardDiskId() + Mesnac.Util.CommonUtil.GetNetWorkAdapterId(), Mesnac.Util.CommonUtil.EnBase64(cpuId));
            this.Application["regCode"] = regCode;
        }
        return regCode;
    }
}