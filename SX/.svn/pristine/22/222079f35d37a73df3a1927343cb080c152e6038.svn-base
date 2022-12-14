using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Business.Implements;
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using System.Data;
    public interface IBasStoragePlacePropManager : IBaseManager<BasStoragePlaceProp>
    {
        PageResult<BasStoragePlaceProp> GetTablePageDataBySql(BasStoragePlacePropManager.QueryParams queryParams);
        DataSet GetStoragePlaceState(string storagePlace);
        DataSet GetStoragePlaceGroup();
    }
}
