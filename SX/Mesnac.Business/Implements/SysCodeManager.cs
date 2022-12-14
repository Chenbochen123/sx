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
        #region ����ע���빹�췽��

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
            EquipJar, //�ϲ�����
            PmtType,  //�䷽����
            PmtTypeNew,  //���䷽����
            PmtState, //�䷽״̬
            Audit,    //���״̬
            YesNo,    //�Ƿ�
        }
    }
}
