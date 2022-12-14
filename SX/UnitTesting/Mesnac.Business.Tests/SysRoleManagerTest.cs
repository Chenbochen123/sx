using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Mesnac.Business.Tests
{
    using Mesnac.Entity;
    using Mesnac.Business.Interface;
    using Mesnac.Business.Implements;


    using Newtonsoft.Json;
    [TestClass]
    public class SysRoleManagerTest
    {
        #region 属性注入
        private ISysRoleManager test = new SysRoleManager();
        #endregion

        [Priority(1), Description("用户登录测试"), Owner("孙本强"), TestMethod]
        public void Login()
        {
            // 初始化
            SysRole sysRole = new SysRole();
            sysRole.Remark = DateTime.Now.ToString();


            // 操作  
            //因函数使用了页面类的Session和Cookie等功能，因此测试不能通过
            bool result = test.Insert(sysRole)>0;

            // 断言
            Assert.AreEqual(true, result);

        }
        private string WriteValue(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            return obj.ToString();
        }

        [Priority(1), Description("用户登录测试"), Owner("孙本强"), TestMethod]
        public void JsonFill()
        {
            // 初始化
            string jsonText = "[{ID:'1',Name:'John',Other:[{Age:'21',Sex:'0'},{Age:'211',Sex:'10'}]},{ID:'2',Name:'Good',Other:[{Age:'36',Sex:'1'}]}]";

            StringBuilder sb = new StringBuilder(); 
            JavaScriptArray javascript = (JavaScriptArray)JavaScriptConvert.DeserializeObject(jsonText);

            for (int i = 0; i < javascript.Count; i++)
            {
                JavaScriptObject obj = (JavaScriptObject)javascript[i];
                sb.AppendLine("ID：" + obj["ID"].ToString());
                sb.AppendLine("Name：" + obj["Name"].ToString());
                JavaScriptArray json = (JavaScriptArray)obj["Other"];

                for (int j = 0; j < json.Count; j++)
                {
                    JavaScriptObject jsonobj = (JavaScriptObject)json[j];
                    sb.AppendLine("Age：" + jsonobj["Age"].ToString());
                    sb.AppendLine("Sex：" + jsonobj["Sex"].ToString());
                }  
            }
            System.Diagnostics.Trace.WriteLine(sb);


        }

        
    }
}
