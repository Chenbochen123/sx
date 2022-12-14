using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mesnac.Util.Module.RegisterModule
{
    public class RegisterModule : IHttpModule
    {
        private readonly string RegFilePath = "~/App_Data/host.dat";
        public HttpApplication app;
        public void Init(HttpApplication httpApplication)
        {
            app = httpApplication;
            app.BeginRequest += new EventHandler(context_BeginRequest);
        }

        public void Dispose()
        {
            return;
            throw new NotImplementedException();
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext context = Application.Context;
            // 做些实际的工作，HttpContext对象都获得了，剩下的基本可以自由发挥了
            if (CommonUtil.GetScriptName.ToLower().IndexOf(".aspx") > 0)
            {
                if (!IsRegister())
                {
                    string url = "Manager/Authentication/HostRegister.aspx";
                    if (CommonUtil.GetScriptName.ToLower().IndexOf(url.ToLower()) < 0)
                    {
                        Application.Response.Redirect("~/" + url);
                    }
                }
            }
        }

        protected bool IsRegister()
        {
            if (app.Application["IsRegister"] is bool)
            {
                bool b = (bool)app.Application["IsRegister"];
                if (b)
                {
                    return true;
                }
            }
            string regCode = this.GetRegCode();

            string realRegFilePath = app.Context.Server.MapPath(this.RegFilePath);

            if (System.IO.File.Exists(realRegFilePath))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream fs = new System.IO.FileStream(realRegFilePath, System.IO.FileMode.Open);
                string str = formater.Deserialize(fs) as string;
                fs.Close();
                if (regCode == str)
                {
                    app.Application["IsRegister"] = true;
                    return true;
                }
            }
            return false;
        }

        private string GetRegCode()
        {
            string cpuId = Mesnac.Util.CommonUtil.GetCpuId();
            return Mesnac.Util.CommonUtil.EncryptString(cpuId + Mesnac.Util.CommonUtil.GetMainHardDiskId() + Mesnac.Util.CommonUtil.GetNetWorkAdapterId(), Mesnac.Util.CommonUtil.EnBase64(cpuId));

        }

    }
}
