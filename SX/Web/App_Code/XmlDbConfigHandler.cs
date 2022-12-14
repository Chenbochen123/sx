using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Xml;

using Mesnac.Entity.Custom;

/// <summary>
///XmlConfigHandler 的摘要说明
/// </summary>
public class XmlDbConfigHandler
{
    private readonly string ConfigFilePath = "~/App_Data/DbConfig.xml";
    private static XmlDbConfigHandler instance = null;
    private Dictionary<int, CdbVersion> dicVersions = null;

    #region 默认构造方法

    private XmlDbConfigHandler()
    {
        lock (this)
        {
            this.dicVersions = this.GetCdbVersionList(HttpContext.Current.Server.MapPath(this.ConfigFilePath));
        }
    }

    #endregion

    #region 获取本类实例的静态方法

    /// <summary>
    /// 获取本类实例
    /// </summary>
    /// <returns>返回本类唯一实例</returns>
    public static XmlDbConfigHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new XmlDbConfigHandler();
        }
        return instance;
    }

    #endregion

    #region 获取系统数据版本列表
    /// <summary>
    /// 获取系统数据版本列表
    /// </summary>
    /// <returns>返回系统数据版本列表</returns>
    public Dictionary<int, CdbVersion> GetDbVersions()
    {
        return this.dicVersions;
    }

    #endregion

    #region 解析数据库配置文件
    /// <summary>
    /// 解析数据库配置文件
    /// </summary>
    /// <param name="configFilePath">数据库配置文件完整路径</param>
    /// <returns>返回解析后的数据版本字典</returns>
    private Dictionary<int, CdbVersion> GetCdbVersionList(string configFilePath)
    {
        using (XmlTextReader reader = new XmlTextReader(configFilePath))
        {
            reader.WhitespaceHandling = WhitespaceHandling.All;

            Dictionary<int, CdbVersion> dic = new Dictionary<int, CdbVersion>();
            CdbVersion version = null;
            CdbDatabase database = null;
            while (reader.Read())
            {
                #region 解析CdbVersions

                if (reader.Name == "Record")
                {
                    version = new CdbVersion();
                    while (reader.Read())
                    {
                        if (reader.Name == "ObjID")
                        {
                            reader.Read();
                            version.ObjID = Convert.ToInt32(reader.Value);
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "DbVersion")
                        {
                            reader.Read();
                            version.DbVersion = reader.Value;
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "Remark")
                        {
                            reader.Read();
                            version.Remark = reader.Value;
                            break;
                        }
                    }
                    dic.Add(version.ObjID, version);

                    reader.ReadOuterXml();
                    reader.ReadOuterXml();
                    reader.ReadEndElement();
                }

                #endregion

                #region 解析DbDatabases

                if (reader.Name == "DbDatabase")
                {
                    database = new CdbDatabase();

                    while (reader.Read())
                    {
                        if (reader.Name == "ObjID")
                        {
                            reader.Read();
                            database.ObjID = Convert.ToInt32(reader.Value);
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "DbVersionID")
                        {
                            reader.Read();
                            database.DbVersionID = Convert.ToInt32(reader.Value);
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "DbKey")
                        {
                            reader.Read();
                            database.DbKey = reader.Value;
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "AssemblyName")
                        {
                            reader.Read();
                            database.AssemblyName = reader.Value;
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "ClassName")
                        {
                            reader.Read();
                            database.ClassName = reader.Value;
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "ConnStr")
                        {
                            reader.Read();
                            database.ConnStr = reader.Value;
                            break;
                        }
                    }
                    while (reader.Read())
                    {
                        if (reader.Name == "Remark")
                        {
                            reader.Read();
                            database.Remark = reader.Value;
                            break;
                        }
                    }

                    dic[database.DbVersionID].Databases.Add(database.DbKey, database);
                    reader.ReadOuterXml();
                    reader.ReadOuterXml();
                    reader.ReadEndElement();
                }

                #endregion
            }
            reader.Close();
            return dic;
        }
    }

    #endregion

}