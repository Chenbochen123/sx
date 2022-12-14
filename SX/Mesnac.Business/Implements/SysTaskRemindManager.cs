using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class SysTaskRemindManager : BaseManager<SysTaskRemind>, ISysTaskRemindManager
    {
		#region 属性注入与构造方法
		
        private ISysTaskRemindService service;

        public SysTaskRemindManager()
        {
            this.service = new SysTaskRemindService();
            base.BaseService = this.service;
        }

		public SysTaskRemindManager(string connectStringKey)
        {
			this.service = new SysTaskRemindService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysTaskRemindManager(NBear.Data.Gateway way)
        {
			this.service = new SysTaskRemindService(way);
            base.BaseService = this.service;
        }

        #endregion

        public void AppTaskRemindSetting(string eventName, string details, string createUser, string recevieUser, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrEmpty(recevieUser))
            {
                return;
            }
            SysTaskRemind task = new SysTaskRemind();
            task.EventName = eventName;
            task.Details = details;
            task.StartDate = startDate;
            task.EndDate = endDate;
            task.ReceiveUser = recevieUser;
            task.CreateUser = createUser;
            string x = new Random().Next(200).ToString();
            string y = new Random().Next(200).ToString();
            string position = x + "x" + y + "x" + "10";
            task.XYZ = position;
            task.Color = "yellow";
            task.DeleteFlag = "0";
            this.service.Insert(task);
        }
    }
}
