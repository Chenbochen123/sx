/************************************************************************************
 *      Copyright (C) 2012 mesnac.com,All Rights Reserved
 *      File:
 *				IBaseManager.cs
 *      Description:
 *				 ҵ���߼������ӿ�
 *      Author:
 *				֣����
 *				zhenglb@mesnac.com
 *				http://www.mesnac.com
 *      Finish DateTime:
 *				2013��01��28��
 *      History:
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business
{
    using Mesnac.Data;
    public interface IBaseManager<T> : IBaseService<T> where T : NBear.Common.Entity, new()
    {

    }
}
