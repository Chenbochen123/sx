using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class TblWeightService : BaseService<TblWeight>, ITblWeightService
    {
		#region ���췽��

        public TblWeightService() : base(){ }

        public TblWeightService(string connectStringKey) : base(connectStringKey){ }

        public TblWeightService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
