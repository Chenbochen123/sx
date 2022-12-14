using System;
using System.Collections.Generic;
using System.Text;
using NBear.Common;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    /// <summary>
    /// IPmtRecipeService �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:55:24
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeService : IBaseService<PmtRecipe>
    {
        /// <summary>
        /// �����������ƻ�ȡ�����䷽������Ϣ
        /// ���˽�
        /// 2013-2-25
        /// </summary>
        /// <param name="recipeMaterialName">��������</param>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetRecipeNameByRecipeMaterialName(string recipeMaterialName);

        /// <summary>
        /// ��ȡ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:55:24
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasMaterial> GetBasMaterial(PmtRecipeService.QueryParams queryParams);

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<PmtRecipe> GetTablePageDataBySql(PmtRecipeService.QueryParams queryParams);

        /// <summary>
        /// ͨ��ƴ����ȡǰ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchkey">The searchkey.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<PmtRecipe> GetDistinctRecipeMaterialNameAndCode(int top , string equipCode , string searchkey);

        /// <summary>
        /// ���湤���䷽��־
        /// �ﱾǿ @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="pmtRecipeID">The PMT recipe ID.</param>
        /// <remarks></remarks>
        void SavePmtRecipeLog(string pmtRecipeID);

        /// <summary>
        /// ˢ�¹����䷽����Ҫ���С���޸�
        /// �ﱾǿ @ 2013-04-03 12:55:25
        /// </summary>
        /// <param name="pmtRecipeID">The PMT recipe ID.</param>
        /// <remarks></remarks>
        void RefreshPmtRecipe(string pmtRecipeID);
    }
}
