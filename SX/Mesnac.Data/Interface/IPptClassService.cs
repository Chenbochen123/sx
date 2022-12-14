using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IPptClassService : IBaseService<PptClass>
    {
        /// <summary>
        /// ���ݰ������Ʋ�ѯ������Ϣ
        /// ���˽�
        /// 2013-1-25
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PptClass GetClassByName(string name);

        /// <summary>
        /// ��ҳ������ȡ��������
        /// yuany
        /// 2013��1��29��
        /// </summary>
        Components.PageResult<PptClass> GetTablePageDataBySql(Implements.PptClassService.QueryParams queryParams);
    }
}
