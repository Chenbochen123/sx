using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysUserCtrlService : BaseService<SysUserCtrl>, ISysUserCtrlService
    {
		#region 构造方法

        public SysUserCtrlService() : base(){ }

        public SysUserCtrlService(string connectStringKey) : base(connectStringKey){ }

        public SysUserCtrlService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public string GetItemCtrl(string TypeID)
        {
            string sql = "select ItemCode from SysUserCtrl where TypeID = '" + TypeID + "'";

            return this.GetBySql(sql).ToScalar().ToString();
        }
    }
}
