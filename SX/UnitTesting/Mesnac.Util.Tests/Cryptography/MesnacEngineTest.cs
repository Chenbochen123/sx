using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mesnac.Util.Tests.Cryptography
{
    using Mesnac.Util.Cryptography;
    [TestClass]
    public class MesnacEngineTest
    {
        #region 属性注入
        private ISKCrypto test = new MesnacEngine();
        #endregion

        [Priority(1), Description("MesnacEngine类 解密"), Owner("孙本强"), TestMethod]
        public void DecryptString()
        {
            // 初始化
            string src = string.Empty;
            string key = string.Empty;
            Encoding encoding = Encoding.ASCII;

            src = "6FEA56C5BBA19E9D80";

            // 操作
            string result = test.DecryptString(src, key, encoding);

            // 断言
            Assert.AreEqual(false, string.IsNullOrEmpty(result));
            Assert.AreEqual("80211220", result);
            
        }


        [Priority(1), Description("MesnacEngine类 加密"), Owner("孙本强"), TestMethod]
        public void EncryptString()
        {
            // 初始化
            string src = string.Empty;
            string key = string.Empty;
            Encoding encoding = Encoding.ASCII;

            src = "6"; 

            // 操作
            string result = test.EncryptString(src, key, encoding);
            result = test.DecryptString(result, key, encoding);

            // 断言
            Assert.AreEqual(false, string.IsNullOrEmpty(result));
            Assert.AreEqual(src, result);

        }

    }
}
