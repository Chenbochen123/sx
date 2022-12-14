namespace Mesnac.Web.UI
{
    /// <summary>
    /// Page 实现类
    /// 孙本强 @ 2013-04-03 13:10:56
    /// </summary>
    public class Page : Mesnac.Web.UI.BasePage
    {
        /// <summary>
        /// 获取页面地址，去掉了?及#后面的信息
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <returns>System.String.</returns>
        protected override string GetPageName()
        {
            string rawUrl = base.GetPageName();
            int indexOfstring = 0;
            indexOfstring = rawUrl.IndexOf("?");
            if (indexOfstring >= 0)
            {
                rawUrl = rawUrl.Substring(0, indexOfstring);
            }
            indexOfstring = rawUrl.IndexOf("#");
            if (indexOfstring >= 0)
            {
                rawUrl = rawUrl.Substring(0, indexOfstring);
            }
            if (rawUrl.StartsWith("/"))
            {
                rawUrl = rawUrl.Substring(1);
            }
            return rawUrl;
        }
    }
}