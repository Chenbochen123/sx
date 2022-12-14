using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterialCarryoverDetailService : BaseService<PstMaterialCarryoverDetail>, IPstMaterialCarryoverDetailService
    {
		#region ���췽��

        public PstMaterialCarryoverDetailService() : base(){ }

        public PstMaterialCarryoverDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialCarryoverDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
