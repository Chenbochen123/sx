
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// 权限定义基类
/// </summary>
public class ___
{
    #region 依赖注入
    /// <summary>
    /// 功能操作管理
    /// </summary>
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();
    /// <summary>
    /// 用户所有权限信息
    /// </summary>
    private ISysUserAllActionManager userAllActionManager = new SysUserAllActionManager();
    #endregion
    #region 初始化定义的权限列表
    /// <summary>
    /// 反射页面中的权限
    /// </summary>
    /// <returns>List{SysPageAction}.</returns>
    private List<SysPageAction> Reflection()
    {
        List<SysPageAction> lst = new List<SysPageAction>();
        Type type = this.GetType();
        #region 属性
        PropertyInfo[] piList = type.GetProperties();
        foreach (PropertyInfo pi in piList)
        {
            if (pi.PropertyType == typeof(SysPageAction))
            {
                SysPageAction m = (SysPageAction)pi.GetValue(this, null);
                if (string.IsNullOrWhiteSpace(m.ShowName))
                {
                    m.ShowName = pi.Name;
                }
                m.SeqIdx = 0;
                lst.Add(m);
            }
        }
        #endregion
        #region 字段
        FieldInfo[] miList = type.GetFields();
        foreach (FieldInfo pi in miList)
        {
            if (pi.FieldType == typeof(SysPageAction))
            {
                SysPageAction m = (SysPageAction)pi.GetValue(this);
                if (string.IsNullOrWhiteSpace(m.ShowName))
                {
                    m.ShowName = pi.Name;
                }
                m.SeqIdx = 0;
                lst.Add(m);
            }
        }
        #endregion
        #region 去除重复的ActionID
        List<SysPageAction> Result = new List<SysPageAction>();
        for (int i = lst.Count - 1; i >= 0; i--)
        {
            bool isExist = false;
            SysPageAction m = lst[i];
            if (m.ObjID > 0)
            {
                Result.Add(m);
                continue;
            }
            foreach (SysPageAction a in Result)
            {
                if (m.ActionID == a.ActionID)
                {
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                Result.Add(m);
            }
        }
        #endregion
        return Result;
    }
    /// <summary>
    /// 将权限信息插入到数据库中
    /// </summary>
    /// <param name="m">The m.</param>
    private void Insert(SysPageAction m)
    {
        SysPageAction a = new SysPageAction();
        a.ObjID = (int)sysPageActionManager.GetMaxValueByProperty(SysPageAction._.ObjID) + 1;
        a.PageMenuID = m.PageMenuID;
        a.ActionID = m.ActionID;
        a.ShowName = m.ShowName;
        a.ActionName = m.ActionName;
        a.SeqIdx = m.ActionID;
        a.DeleteFlag = m.DeleteFlag == null ? "0" : m.DeleteFlag; //"0";
        sysPageActionManager.Insert(a);
    }
    /// <summary>
    /// 更新权限信息
    /// </summary>
    /// <param name="m">The m.</param>
    private void Update(SysPageAction m)
    {
        sysPageActionManager.Update(m);
    }
    /// <summary>
    /// 删除权限信息
    /// </summary>
    /// <param name="m">The m.</param>
    private void Delete(SysPageAction m)
    {
        sysPageActionManager.Delete(m.ObjID);
    }
    /// <summary>
    /// 初始化当前页面的权限信息
    /// </summary>
    /// <param name="page">The page.</param>
    public void IniBind(SysPageMenu page)
    {
        List<SysPageAction> thisList = Reflection();
        EntityArrayList<SysPageAction> lst = sysPageActionManager.GetListByWhere(SysPageAction._.PageMenuID == page.ObjID);
        foreach (SysPageAction m in thisList)
        {
            m.PageMenuID = page.ObjID;
            m.SeqIdx = 0;
        }
        foreach (SysPageAction m in thisList)
        {
            int mode = 0;
            foreach (SysPageAction n in lst)
            {
                if (m.ObjID > 0)
                {
                    mode = 1;
                    continue;
                }
                if (m.ActionID == n.ActionID)
                {
                    mode = 1;
                    if ((!string.IsNullOrWhiteSpace(m.ActionName) && (n.ActionName != m.ActionName))
                       || (!string.IsNullOrWhiteSpace(m.ShowName) && (n.ShowName != m.ShowName)))
                    {
                        n.ActionName = m.ActionName;
                        n.ShowName = m.ShowName;
                        //Update(n);  //不需要修改数据
                    }
                    break;
                }
            }
            if (mode == 0)
            {
                Insert(m);
            }
        }
        if (false)  //不需要删除数据
        {
            foreach (SysPageAction m in lst)
            {
                int mode = 0;
                foreach (SysPageAction n in thisList)
                {
                    if (m.ActionID == n.ActionID)
                    {
                        mode = 1;
                        break;
                    }
                }
                if (mode == 0)
                {
                    Delete(m);
                }
            }
        }
        if (page.DeleteFlag == "1")
        {
            foreach (SysPageAction m in thisList)
            {
                m.SeqIdx = 1;
            }
        }
    }
    #endregion
    #region 初始化用户权限
    /// <summary>
    /// 设定用户具有的权限信息
    /// </summary>
    /// <param name="lst">The LST.</param>
    public void UserBind(List<SysPageAction> lst)
    {
        List<SysPageAction> thisList = Reflection();
        foreach (SysPageAction page in thisList)
        {
            if ((page.SeqIdx == 0) && (page.ObjID > 0))
            {
                if (userAllActionManager.GetRowCountByWhere(SysUserAllAction._.ActionID == page.ObjID) > 0)
                {
                    page.SeqIdx = 1;
                }
            }
        }
        foreach (SysPageAction user in lst)
        {
            foreach (SysPageAction page in thisList)
            {
                if (user.ActionID == page.ActionID)
                {
                    page.SeqIdx = 1;
                    break;
                }
            }
        }
    }
    #endregion
}
