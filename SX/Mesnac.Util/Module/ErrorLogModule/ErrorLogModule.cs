using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mesnac.Util.Module.ErrorLogModule
{
    public class ErrorLogModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.Error += new EventHandler(app_Error);
        }

        public void Dispose()
        {
            return;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 处理出错日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void app_Error(object sender, EventArgs e)
        {
            HttpApplication ap = sender as HttpApplication;
            Exception ex = ap.Server.GetLastError();
            if (ex is HttpException)
            {
                HttpException hx = (HttpException)ex;
                if (hx.GetHttpCode() == 404)
                {
                    string page = ap.Request.PhysicalPath;
                    FileTxtLogs.WriteLog(string.Format("文件不存在:{0}", ap.Request.Url.AbsoluteUri));
                    return;
                }
            }
            if (ex.InnerException != null) ex = ex.InnerException;
            string logContent = "访问路径:" + CommonUtil.GetScriptUrl + "<br>" + ex.Source + " thrown " + ex.GetType().ToString() + "<br />" + ex.Message.Replace("\r", "").Replace("\n", "<br />") + "<br />" + ex.StackTrace.Replace("\r", "").Replace("\n", "<br />");
            FileTxtLogs.WriteLog(logContent);
            if (!CommonUtil.DispError)
            {
                if (CommonUtil.GetScriptName.ToLower().IndexOf("manager/errorframe.aspx") < 0)
                {
                    ap.Response.Redirect("~/Manager/ErrorFrame.aspx");
                }
            }
        }
    }
}
