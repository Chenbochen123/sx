using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Mesnac.Business.Implements;

public partial class Manager_BasicInfo_UserInfo_ContractPhoto : System.Web.UI.Page
{
    protected SYS_USERManager Sysmanager = new SYS_USERManager();
    protected void Page_Load(object sender, EventArgs e)
    {
         //定义ProjectID获取QueryString中获取的ProjectID
            int projectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            //通过projectID获取Project信息


            try
            {


                SYS_USER project = Sysmanager.GetListByWhere(SYS_USER._.USER_ID == Request.QueryString["ProjectID"].ToString())[0];
                byte[] b_ContractPhotoImg = project.Pic;
                if (b_ContractPhotoImg.Length > 0)
                {
                    Response.Clear();
                    Response.ContentType = "image/jpeg";
                    Response.OutputStream.Write(b_ContractPhotoImg, 0, b_ContractPhotoImg.Length);
                    Response.End();
                }
            }
            catch (Exception ex)
            { }
    }
}