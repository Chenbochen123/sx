//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity
{
    using System;
    using System.Xml.Serialization;
    using NBear.Common;


    [System.SerializableAttribute()]
    public partial class QmcLedgerKeyArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.QmcLedgerKey>
    {
    }

    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
    "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
    "name=\"Mesnac.Entity.QmcLedgerKey\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBat" +
    "chUpdate=\"false\" isRelation=\"false\" mappingName=\"QmcLedgerKey\" batchSize=\"10\">\r\n" +
    "  <Properties>\r\n    <Property name=\"KeyId\" type=\"System.Int32\" isInherited=\"fals" +
    "e\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
    " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
    "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Key" +
    "Id\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull" +
    "=\"true\" />\r\n    <Property name=\"KeyName\" type=\"System.String\" isInherited=\"false" +
    "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
    "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
    "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"KeyN" +
    "ame\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"fals" +
    "e\" isNotNull=\"false\" />\r\n    <Property name=\"KeyCode\" type=\"System.String\" isInh" +
    "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
    "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
    "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
    "ingName=\"KeyCode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(50)\" isPri" +
    "maryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ValueType\" type=\"Syste" +
    "m.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
    "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
    "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
    "re=\"false\" mappingName=\"ValueType\" mappingColumnType=\"System.String\" sqlType=\"nv" +
    "archar(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"HasSe" +
    "lection\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
    "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
    "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
    " isSerializationIgnore=\"false\" mappingName=\"HasSelection\" mappingColumnType=\"Sys" +
    "tem.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pr" +
    "operty name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\"" +
    " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
    "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
    "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnTyp" +
    "e=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" " +
    "/>\r\n    <Property name=\"DeleteFlag\" type=\"System.String\" isInherited=\"false\" isR" +
    "eadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFri" +
    "endKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" " +
    "isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFla" +
    "g\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNo" +
    "tNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class QmcLedgerKey : NBear.Common.Entity
    {

        protected static NBear.Common.EntityConfiguration _QmcLedgerKeyEntityConfiguration;

        protected int _KeyId;

        protected string _KeyName;

        protected string _KeyCode;

        protected string _ValueType;

        protected string _HasSelection;

        protected string _Remark;

        protected string _DeleteFlag;

        public static @__Columns _ = new @__Columns();

        public static bool operator ==(global::Mesnac.Entity.QmcLedgerKey left, global::Mesnac.Entity.QmcLedgerKey right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


        public static bool operator !=(global::Mesnac.Entity.QmcLedgerKey left, global::Mesnac.Entity.QmcLedgerKey right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }



        public int KeyId
        {
            get
            {
                return this._KeyId;
            }
            set
            {
                this.OnPropertyChanged("KeyId", this._KeyId, value);
                this._KeyId = value;
            }
        }

        public string KeyName
        {
            get
            {
                return this._KeyName;
            }
            set
            {
                this.OnPropertyChanged("KeyName", this._KeyName, value);
                this._KeyName = value;
            }
        }

        public string KeyCode
        {
            get
            {
                return this._KeyCode;
            }
            set
            {
                this.OnPropertyChanged("KeyCode", this._KeyCode, value);
                this._KeyCode = value;
            }
        }

        public string ValueType
        {
            get
            {
                return this._ValueType;
            }
            set
            {
                this.OnPropertyChanged("ValueType", this._ValueType, value);
                this._ValueType = value;
            }
        }

        public string HasSelection
        {
            get
            {
                return this._HasSelection;
            }
            set
            {
                this.OnPropertyChanged("HasSelection", this._HasSelection, value);
                this._HasSelection = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.OnPropertyChanged("Remark", this._Remark, value);
                this._Remark = value;
            }
        }

        public string DeleteFlag
        {
            get
            {
                return this._DeleteFlag;
            }
            set
            {
                this.OnPropertyChanged("DeleteFlag", this._DeleteFlag, value);
                this._DeleteFlag = value;
            }
        }

        public override NBear.Common.EntityConfiguration GetEntityConfiguration()
        {
            if ((QmcLedgerKey._QmcLedgerKeyEntityConfiguration == null))
            {
                QmcLedgerKey._QmcLedgerKeyEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.QmcLedgerKey");
            }
            return QmcLedgerKey._QmcLedgerKeyEntityConfiguration;
        }

        public override void ReloadQueries(bool includeLazyLoadQueries)
        {
        }

        public override object[] GetPropertyValues()
        {
            return new object[] {
                        this._KeyId,
                        this._KeyName,
                        this._KeyCode,
                        this._ValueType,
                        this._HasSelection,
                        this._Remark,
                        this._DeleteFlag};
        }

        public override void SetPropertyValues(System.Data.IDataReader reader)
        {
            if ((false == reader.IsDBNull(0)))
            {
                this._KeyId = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1)))
            {
                this._KeyName = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2)))
            {
                this._KeyCode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3)))
            {
                this._ValueType = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4)))
            {
                this._HasSelection = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5)))
            {
                this._Remark = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6)))
            {
                this._DeleteFlag = reader.GetString(6);
            }
            this.ReloadQueries(false);
        }

        public override void SetPropertyValues(System.Data.DataRow row)
        {
            if ((false == row.IsNull(0)))
            {
                this._KeyId = ((int)(row[0]));
            }
            if ((false == row.IsNull(1)))
            {
                this._KeyName = ((string)(row[1]));
            }
            if ((false == row.IsNull(2)))
            {
                this._KeyCode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3)))
            {
                this._ValueType = ((string)(row[3]));
            }
            if ((false == row.IsNull(4)))
            {
                this._HasSelection = ((string)(row[4]));
            }
            if ((false == row.IsNull(5)))
            {
                this._Remark = ((string)(row[5]));
            }
            if ((false == row.IsNull(6)))
            {
                this._DeleteFlag = ((string)(row[6]));
            }
            this.ReloadQueries(false);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null))
            {
                return false;
            }
            if ((false == typeof(global::Mesnac.Entity.QmcLedgerKey).IsAssignableFrom(obj.GetType())))
            {
                return false;
            }
            if ((((object)(this)) == ((object)(obj))))
            {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.QmcLedgerKey)(obj)).isAttached)
                        && (this.KeyId == ((global::Mesnac.Entity.QmcLedgerKey)(obj)).KeyId));
        }

        public static @__Columns @__Alias(string aliasName)
        {
            return new @__Columns(aliasName);
        }

        public class @__Columns
        {

            protected string aliasName;

            protected NBear.Common.PropertyItem _KeyId = new NBear.Common.PropertyItem("KeyId", "Mesnac.Entity.QmcLedgerKey");

            protected NBear.Common.PropertyItem _KeyName = new NBear.Common.PropertyItem("KeyName", "Mesnac.Entity.QmcLedgerKey");

            protected NBear.Common.PropertyItem _KeyCode = new NBear.Common.PropertyItem("KeyCode", "Mesnac.Entity.QmcLedgerKey");

            protected NBear.Common.PropertyItem _ValueType = new NBear.Common.PropertyItem("ValueType", "Mesnac.Entity.QmcLedgerKey");

            protected NBear.Common.PropertyItem _HasSelection = new NBear.Common.PropertyItem("HasSelection", "Mesnac.Entity.QmcLedgerKey");

            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.QmcLedgerKey");

            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.QmcLedgerKey");

            public @__Columns()
            {
            }

            public @__Columns(string aliasName)
            {
                this.aliasName = aliasName;
            }

            public NBear.Common.PropertyItem KeyId
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _KeyId;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("KeyId", _KeyId.EntityConfiguration, _KeyId.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem KeyName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _KeyName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("KeyName", _KeyName.EntityConfiguration, _KeyName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem KeyCode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _KeyCode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("KeyCode", _KeyCode.EntityConfiguration, _KeyCode.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ValueType
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ValueType;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ValueType", _ValueType.EntityConfiguration, _ValueType.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem HasSelection
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _HasSelection;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("HasSelection", _HasSelection.EntityConfiguration, _HasSelection.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem Remark
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Remark;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Remark", _Remark.EntityConfiguration, _Remark.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem DeleteFlag
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _DeleteFlag;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("DeleteFlag", _DeleteFlag.EntityConfiguration, _DeleteFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
