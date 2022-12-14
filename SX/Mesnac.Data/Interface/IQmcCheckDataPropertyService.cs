using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcCheckDataPropertyService : IBaseService<QmcCheckDataProperty>
    {
        DataSet GetDataSetByCheckId(string checkId);
    }
}
