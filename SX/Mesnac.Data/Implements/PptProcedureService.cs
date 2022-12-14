using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class PptProcedureService : BaseService<PptProcedure>, IPptProcedureService
    {
		#region 构造方法

        public PptProcedureService() : base(){ }

        public PptProcedureService(string connectStringKey) : base(connectStringKey){ }

        public PptProcedureService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
