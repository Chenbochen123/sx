using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class Pst_mmshopcheckService : BaseService<Pst_mmshopcheck>, IPst_mmshopcheckService
    {
		#region 构造方法

        public Pst_mmshopcheckService() : base(){ }

        public Pst_mmshopcheckService(string connectStringKey) : base(connectStringKey){ }

        public Pst_mmshopcheckService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
