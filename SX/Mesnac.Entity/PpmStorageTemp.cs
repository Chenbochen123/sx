//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
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
    public partial class PpmStorageTempArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PpmStorageTemp>
    {
    }

    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PpmStorageTemp\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isB" +
        "atchUpdate=\"false\" isRelation=\"false\" mappingName=\"PpmStorageTemp\" batchSize=\"10" +
        "\">\r\n  <Properties>\r\n    <Property name=\"Barcode\" type=\"System.String\" isInherite" +
        "d=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=" +
        "\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProp" +
        "erty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNa" +
        "me=\"Barcode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(18)\" isPrimaryK" +
        "ey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeptCode\" type=\"System.Stri" +
        "ng\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"f" +
        "alse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fals" +
        "e\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fa" +
        "lse\" mappingName=\"DeptCode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(" +
        "10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeptName\" ty" +
        "pe=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\"" +
        " isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRe" +
        "lationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializ" +
        "ationIgnore=\"false\" mappingName=\"DeptName\" mappingColumnType=\"System.String\" sql" +
        "Type=\"nvarchar(40)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property nam" +
        "e=\"StorageID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isComp" +
        "oundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyL" +
        "oad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"f" +
        "alse\" isSerializationIgnore=\"false\" mappingName=\"StorageID\" mappingColumnType=\"S" +
        "ystem.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n" +
        "    <Property name=\"StorageName\" type=\"System.String\" isInherited=\"false\" isRead" +
        "Only=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriend" +
        "Key=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isI" +
        "ndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StorageName\"" +
        " mappingColumnType=\"System.String\" sqlType=\"nvarchar(40)\" isPrimaryKey=\"false\" i" +
        "sNotNull=\"false\" />\r\n    <Property name=\"StoragePlaceID\" type=\"System.String\" is" +
        "Inherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" " +
        "isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isI" +
        "ndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" m" +
        "appingName=\"StoragePlaceID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(" +
        "36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"StoragePlace" +
        "Name\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"StoragePlaceName\" mappingColumnType=\"Sy" +
        "stem.String\" sqlType=\"nvarchar(40)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n " +
        "   <Property name=\"MaterCode\" type=\"System.String\" isInherited=\"false\" isReadOnl" +
        "y=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey" +
        "=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInde" +
        "xPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"MaterCode\" mapp" +
        "ingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotN" +
        "ull=\"false\" />\r\n    <Property name=\"MaterialName\" type=\"System.String\" isInherit" +
        "ed=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery" +
        "=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPro" +
        "perty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingN" +
        "ame=\"MaterialName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPr" +
        "imaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ProcDate\" type=\"Syste" +
        "m.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
        "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
        "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
        "\" isSerializationIgnore=\"false\" mappingName=\"ProcDate\" mappingColumnType=\"System" +
        ".Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n    <Property name=\"InputDate\" type=\"System.Nullable`1[System.DateTi" +
        "me]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"" +
        "false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fal" +
        "se\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"f" +
        "alse\" mappingName=\"InputDate\" mappingColumnType=\"System.Nullable`1[System.DateTi" +
        "me]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property" +
        " name=\"Num\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnl" +
        "y=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey" +
        "=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInde" +
        "xPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Num\" mappingCol" +
        "umnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isN" +
        "otNull=\"false\" />\r\n    <Property name=\"RealWeight\" type=\"System.Nullable`1[Syste" +
        "m.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"RealWeight\" mappingColumnType=\"System.Nullable`1[Syste" +
        "m.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pr" +
        "operty name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnTyp" +
        "e=\"System.String\" sqlType=\"nchar(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r" +
        "\n    <Property name=\"NewNum\" type=\"System.Nullable`1[System.Int32]\" isInherited=" +
        "\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"NewNum\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPr" +
        "imaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class PpmStorageTemp : NBear.Common.Entity
    {

        protected static NBear.Common.EntityConfiguration _PpmStorageTempEntityConfiguration;

        protected string _Barcode;

        protected string _DeptCode;

        protected string _DeptName;

        protected string _StorageID;

        protected string _StorageName;

        protected string _StoragePlaceID;

        protected string _StoragePlaceName;

        protected string _MaterCode;

        protected string _MaterialName;

        protected global::System.DateTime? _ProcDate;

        protected global::System.DateTime? _InputDate;

        protected global::System.Int32? _Num;

        protected global::System.Decimal? _RealWeight;

        protected string _Remark;

        protected global::System.Int32? _NewNum;

        public static @__Columns _ = new @__Columns();

        public static bool operator ==(global::Mesnac.Entity.PpmStorageTemp left, global::Mesnac.Entity.PpmStorageTemp right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


        public static bool operator !=(global::Mesnac.Entity.PpmStorageTemp left, global::Mesnac.Entity.PpmStorageTemp right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }



        public string Barcode
        {
            get
            {
                return this._Barcode;
            }
            set
            {
                this.OnPropertyChanged("Barcode", this._Barcode, value);
                this._Barcode = value;
            }
        }

        public string DeptCode
        {
            get
            {
                return this._DeptCode;
            }
            set
            {
                this.OnPropertyChanged("DeptCode", this._DeptCode, value);
                this._DeptCode = value;
            }
        }

        public string DeptName
        {
            get
            {
                return this._DeptName;
            }
            set
            {
                this.OnPropertyChanged("DeptName", this._DeptName, value);
                this._DeptName = value;
            }
        }

        public string StorageID
        {
            get
            {
                return this._StorageID;
            }
            set
            {
                this.OnPropertyChanged("StorageID", this._StorageID, value);
                this._StorageID = value;
            }
        }

        public string StorageName
        {
            get
            {
                return this._StorageName;
            }
            set
            {
                this.OnPropertyChanged("StorageName", this._StorageName, value);
                this._StorageName = value;
            }
        }

        public string StoragePlaceID
        {
            get
            {
                return this._StoragePlaceID;
            }
            set
            {
                this.OnPropertyChanged("StoragePlaceID", this._StoragePlaceID, value);
                this._StoragePlaceID = value;
            }
        }

        public string StoragePlaceName
        {
            get
            {
                return this._StoragePlaceName;
            }
            set
            {
                this.OnPropertyChanged("StoragePlaceName", this._StoragePlaceName, value);
                this._StoragePlaceName = value;
            }
        }

        public string MaterCode
        {
            get
            {
                return this._MaterCode;
            }
            set
            {
                this.OnPropertyChanged("MaterCode", this._MaterCode, value);
                this._MaterCode = value;
            }
        }

        public string MaterialName
        {
            get
            {
                return this._MaterialName;
            }
            set
            {
                this.OnPropertyChanged("MaterialName", this._MaterialName, value);
                this._MaterialName = value;
            }
        }

        public global::System.DateTime? ProcDate
        {
            get
            {
                return this._ProcDate;
            }
            set
            {
                this.OnPropertyChanged("ProcDate", this._ProcDate, value);
                this._ProcDate = value;
            }
        }

        public global::System.DateTime? InputDate
        {
            get
            {
                return this._InputDate;
            }
            set
            {
                this.OnPropertyChanged("InputDate", this._InputDate, value);
                this._InputDate = value;
            }
        }

        public global::System.Int32? Num
        {
            get
            {
                return this._Num;
            }
            set
            {
                this.OnPropertyChanged("Num", this._Num, value);
                this._Num = value;
            }
        }

        public global::System.Decimal? RealWeight
        {
            get
            {
                return this._RealWeight;
            }
            set
            {
                this.OnPropertyChanged("RealWeight", this._RealWeight, value);
                this._RealWeight = value;
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

        public global::System.Int32? NewNum
        {
            get
            {
                return this._NewNum;
            }
            set
            {
                this.OnPropertyChanged("NewNum", this._NewNum, value);
                this._NewNum = value;
            }
        }

        public override NBear.Common.EntityConfiguration GetEntityConfiguration()
        {
            if ((PpmStorageTemp._PpmStorageTempEntityConfiguration == null))
            {
                PpmStorageTemp._PpmStorageTempEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PpmStorageTemp");
            }
            return PpmStorageTemp._PpmStorageTempEntityConfiguration;
        }

        public override void ReloadQueries(bool includeLazyLoadQueries)
        {
        }

        public override object[] GetPropertyValues()
        {
            return new object[] {
                    this._Barcode,
                    this._DeptCode,
                    this._DeptName,
                    this._StorageID,
                    this._StorageName,
                    this._StoragePlaceID,
                    this._StoragePlaceName,
                    this._MaterCode,
                    this._MaterialName,
                    this._ProcDate,
                    this._InputDate,
                    this._Num,
                    this._RealWeight,
                    this._Remark,
                    this._NewNum};
        }

        public override void SetPropertyValues(System.Data.IDataReader reader)
        {
            if ((false == reader.IsDBNull(0)))
            {
                this._Barcode = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1)))
            {
                this._DeptCode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2)))
            {
                this._DeptName = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3)))
            {
                this._StorageID = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4)))
            {
                this._StorageName = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5)))
            {
                this._StoragePlaceID = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6)))
            {
                this._StoragePlaceName = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7)))
            {
                this._MaterCode = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8)))
            {
                this._MaterialName = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9)))
            {
                this._ProcDate = this.GetDateTime(reader, 9);
            }
            if ((false == reader.IsDBNull(10)))
            {
                this._InputDate = this.GetDateTime(reader, 10);
            }
            if ((false == reader.IsDBNull(11)))
            {
                this._Num = reader.GetInt32(11);
            }
            if ((false == reader.IsDBNull(12)))
            {
                this._RealWeight = reader.GetDecimal(12);
            }
            if ((false == reader.IsDBNull(13)))
            {
                this._Remark = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14)))
            {
                this._NewNum = reader.GetInt32(14);
            }
            this.ReloadQueries(false);
        }

        public override void SetPropertyValues(System.Data.DataRow row)
        {
            if ((false == row.IsNull(0)))
            {
                this._Barcode = ((string)(row[0]));
            }
            if ((false == row.IsNull(1)))
            {
                this._DeptCode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2)))
            {
                this._DeptName = ((string)(row[2]));
            }
            if ((false == row.IsNull(3)))
            {
                this._StorageID = ((string)(row[3]));
            }
            if ((false == row.IsNull(4)))
            {
                this._StorageName = ((string)(row[4]));
            }
            if ((false == row.IsNull(5)))
            {
                this._StoragePlaceID = ((string)(row[5]));
            }
            if ((false == row.IsNull(6)))
            {
                this._StoragePlaceName = ((string)(row[6]));
            }
            if ((false == row.IsNull(7)))
            {
                this._MaterCode = ((string)(row[7]));
            }
            if ((false == row.IsNull(8)))
            {
                this._MaterialName = ((string)(row[8]));
            }
            if ((false == row.IsNull(9)))
            {
                this._ProcDate = this.GetDateTime(row, 9);
            }
            if ((false == row.IsNull(10)))
            {
                this._InputDate = this.GetDateTime(row, 10);
            }
            if ((false == row.IsNull(11)))
            {
                this._Num = ((System.Nullable<int>)(row[11]));
            }
            if ((false == row.IsNull(12)))
            {
                this._RealWeight = ((System.Nullable<decimal>)(row[12]));
            }
            if ((false == row.IsNull(13)))
            {
                this._Remark = ((string)(row[13]));
            }
            if ((false == row.IsNull(14)))
            {
                this._NewNum = ((System.Nullable<int>)(row[14]));
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
            if ((false == typeof(global::Mesnac.Entity.PpmStorageTemp).IsAssignableFrom(obj.GetType())))
            {
                return false;
            }
            if ((((object)(this)) == ((object)(obj))))
            {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PpmStorageTemp)(obj)).isAttached)
                        && base.Equals(obj));
        }

        public static @__Columns @__Alias(string aliasName)
        {
            return new @__Columns(aliasName);
        }

        public class @__Columns
        {

            protected string aliasName;

            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _DeptCode = new NBear.Common.PropertyItem("DeptCode", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _DeptName = new NBear.Common.PropertyItem("DeptName", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _StorageName = new NBear.Common.PropertyItem("StorageName", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _StoragePlaceName = new NBear.Common.PropertyItem("StoragePlaceName", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _MaterialName = new NBear.Common.PropertyItem("MaterialName", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _ProcDate = new NBear.Common.PropertyItem("ProcDate", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _InputDate = new NBear.Common.PropertyItem("InputDate", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _Num = new NBear.Common.PropertyItem("Num", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _RealWeight = new NBear.Common.PropertyItem("RealWeight", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PpmStorageTemp");

            protected NBear.Common.PropertyItem _NewNum = new NBear.Common.PropertyItem("NewNum", "Mesnac.Entity.PpmStorageTemp");

            public @__Columns()
            {
            }

            public @__Columns(string aliasName)
            {
                this.aliasName = aliasName;
            }

            public NBear.Common.PropertyItem Barcode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Barcode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Barcode", _Barcode.EntityConfiguration, _Barcode.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem DeptCode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _DeptCode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("DeptCode", _DeptCode.EntityConfiguration, _DeptCode.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem DeptName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _DeptName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("DeptName", _DeptName.EntityConfiguration, _DeptName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StorageID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StorageID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StorageID", _StorageID.EntityConfiguration, _StorageID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StorageName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StorageName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StorageName", _StorageName.EntityConfiguration, _StorageName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StoragePlaceID
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StoragePlaceID;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StoragePlaceID", _StoragePlaceID.EntityConfiguration, _StoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem StoragePlaceName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _StoragePlaceName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("StoragePlaceName", _StoragePlaceName.EntityConfiguration, _StoragePlaceName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem MaterCode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _MaterCode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("MaterCode", _MaterCode.EntityConfiguration, _MaterCode.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem MaterialName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _MaterialName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("MaterialName", _MaterialName.EntityConfiguration, _MaterialName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ProcDate
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ProcDate;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ProcDate", _ProcDate.EntityConfiguration, _ProcDate.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem InputDate
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _InputDate;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("InputDate", _InputDate.EntityConfiguration, _InputDate.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem Num
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _Num;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("Num", _Num.EntityConfiguration, _Num.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem RealWeight
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _RealWeight;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("RealWeight", _RealWeight.EntityConfiguration, _RealWeight.PropertyConfiguration, aliasName);
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

            public NBear.Common.PropertyItem NewNum
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _NewNum;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("NewNum", _NewNum.EntityConfiguration, _NewNum.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
