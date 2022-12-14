using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using System.Data;
    public interface IBasStoragePlacePropService : IBaseService<BasStoragePlaceProp>
    {
        PageResult<BasStoragePlaceProp> GetTablePageDataBySql(Mesnac.Data.Implements.BasStoragePlacePropService.QueryParams queryParams);
        DataSet GetStoragePlaceState(string storagePlace);
        DataSet GetStoragePlaceGroup();
    }
}
