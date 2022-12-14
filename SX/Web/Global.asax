<%@ Application Language="C#" %>

<script runat="server">
   
    void Application_Start(object sender, EventArgs e)
    {
        //应用程序启动时运行的代码
        Application["count"] = 0;

        System.Timers.Timer myTimer = new System.Timers.Timer();//60秒
        myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
        myTimer.Interval = 60000;
        myTimer.Enabled = true;
     }

    private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
    {
        //你要定时执行的代码放这里
        //SJ m = new SJ();
        //m.CheckSJ(HttpContext.Current);
   
    } 
    void Application_End(object sender, EventArgs e)
    {
        // 在应用程序关闭时运行的代码
    }
    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码
    }
    void Session_Start(object sender, EventArgs e)
    {
        //对Appliaction加锁以防止并行性
        Application.Lock();
        //增加一个在线人数
        Application["count"] = (int)Application["count"] + 1;
        //解锁
        Application.UnLock();
    }
    void Session_End(object sender, EventArgs e)
    {
        Application.Lock();
        //减少一个在线人数
        Application["count"] = (int)Application["count"] - 1;
        Application.UnLock();
    }
       
</script>
