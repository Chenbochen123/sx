using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmcPropertyValueService : IBaseService<QmcPropertyValue>
    {
        //��ȡ��һ��ֵ���
        string GetNextValueId();
    }
}