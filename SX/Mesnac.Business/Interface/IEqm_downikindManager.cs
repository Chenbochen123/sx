using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IEqm_downikindManager : IBaseManager<Eqm_downikind>
    {
        DataSet GetDataByParas(Eqm_downikind queryParams);
    }
}
