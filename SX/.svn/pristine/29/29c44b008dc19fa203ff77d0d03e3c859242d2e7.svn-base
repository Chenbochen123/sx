using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class SysCodeManager : BaseManager<SysCode>, ISysCodeManager
    {
        #region 属性注入与构造方法

        private ISysCodeService service;

        public SysCodeManager()
        {
            this.service = new SysCodeService();
            base.BaseService = this.service;
        }

        public SysCodeManager(string connectStringKey)
        {
            this.service = new SysCodeService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysCodeManager(NBear.Data.Gateway way)
        {
            this.service = new SysCodeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public enum SysCodeType
        {
            DeptLevel = 0,
            Sex,
            EquipJar, //料仓类型
            PmtType,  //配方类型
            PmtTypeNew,  //新配方类型
            PmtState, //配方状态
            Audit,    //审核状态
            YesNo,    //是否
        }
    }
}
