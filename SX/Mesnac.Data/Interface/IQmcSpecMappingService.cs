using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcSpecMappingService : IBaseService<QmcSpecMapping>
    {
        //获取下一个映射编号
        string GetNextMappingId();
    }
}
