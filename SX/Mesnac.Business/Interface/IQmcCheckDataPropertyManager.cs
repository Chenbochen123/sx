using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    public interface IQmcCheckDataPropertyManager : IBaseManager<QmcCheckDataProperty>
    {
        DataSet GetDataSetByCheckId(string checkId);
    }
}
