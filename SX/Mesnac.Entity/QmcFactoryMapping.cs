//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.18052
//
//     �Դ��ļ��ĸ��Ŀ��ܻᵼ�²���ȷ����Ϊ���������
//     �������ɴ��룬��Щ���Ľ��ᶪʧ��
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity
{
    using System;
    using System.Xml.Serialization;
    using NBear.Common;


    [System.SerializableAttribute()]
    public partial class QmcFactoryMappingArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.QmcFactoryMapping>
    {
    }

    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
    "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
    "name=\"Mesnac.Entity.QmcFactoryMapping\" isReadOnly=\"false\" isAutoPreLoad=\"false\" " +
    "isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"QmcFactoryMapping\" batchSi" +
    "ze=\"10\">\r\n  <Properties>\r\n    <Property name=\"MappingId\" type=\"System.Int32\" isI" +
    "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
    "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
    "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
    "ppingName=\"MappingId\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKe" +
    "y=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"SeriesId\" type=\"System.String\"" +
    " isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fals" +
    "e\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" " +
    "isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false" +
    "\" mappingName=\"SeriesId\" mappingColumnType=\"System.String\" sqlType=\"char(2)\" isP" +
    "rimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"SupplierId\" type=\"Sy" +
    "stem.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
    "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
    "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
    "\" isSerializationIgnore=\"false\" mappingName=\"SupplierId\" mappingColumnType=\"Syst" +
    "em.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false" +
    "\" />\r\n    <Property name=\"ManufacturerId\" type=\"System.Nullable`1[System.Int32]\"" +
    " isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fals" +
    "e\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" " +
    "isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false" +
    "\" mappingName=\"ManufacturerId\" mappingColumnType=\"System.Nullable`1[System.Int32" +
    "]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"" +
    "SeriesName\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompou" +
    "ndUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoa" +
    "d=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"fal" +
    "se\" isSerializationIgnore=\"false\" mappingName=\"SeriesName\" mappingColumnType=\"Sy" +
    "stem.String\" sqlType=\"varchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  " +
    "  <Property name=\"SupplierName\" type=\"System.String\" isInherited=\"false\" isReadO" +
    "nly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendK" +
    "ey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIn" +
    "dexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"SupplierName\"" +
    " mappingColumnType=\"System.String\" sqlType=\"varchar(50)\" isPrimaryKey=\"false\" is" +
    "NotNull=\"false\" />\r\n    <Property name=\"ManufacturerName\" type=\"System.String\" i" +
    "sInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\"" +
    " isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" is" +
    "IndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" " +
    "mappingName=\"ManufacturerName\" mappingColumnType=\"System.String\" sqlType=\"varcha" +
    "r(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"SupplierER" +
    "PCode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
    "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
    "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
    "sSerializationIgnore=\"false\" mappingName=\"SupplierERPCode\" mappingColumnType=\"Sy" +
    "stem.String\" sqlType=\"varchar(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  " +
    "  <Property name=\"ManufacturerERPCode\" type=\"System.String\" isInherited=\"false\" " +
    "isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
    "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
    "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Manufa" +
    "cturerERPCode\" mappingColumnType=\"System.String\" sqlType=\"varchar(10)\" isPrimary" +
    "Key=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" type=\"System.Strin" +
    "g\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
    "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
    "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
    "se\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(50)" +
    "\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" typ" +
    "e=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" " +
    "isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRel" +
    "ationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializa" +
    "tionIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.String\" sq" +
    "lType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</En" +
    "tityConfiguration>")]
    public partial class QmcFactoryMapping : NBear.Common.Entity
    {

        protected static NBear.Common.EntityConfiguration _QmcFactoryMappingEntityConfiguration;

        protected int _MappingId;

        protected string _SeriesId;

        protected global::System.Int32? _SupplierId;

        protected global::System.Int32? _ManufacturerId;

        protected string _SeriesName;

        protected string _SupplierName;

        protected string _ManufacturerName;

        protected string _SupplierERPCode;

        protected string _ManufacturerERPCode;

        protected string _Remark;

        protected string _DeleteFlag;

        public static @__Columns _ = new @__Columns();

        public static bool operator ==(global::Mesnac.Entity.QmcFactoryMapping left, global::Mesnac.Entity.QmcFactoryMapping right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


        public static bool operator !=(global::Mesnac.Entity.QmcFactoryMapping left, global::Mesnac.Entity.QmcFactoryMapping right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }



        public int MappingId
        {
            get
            {
                return this._MappingId;
            }
            set
            {
                this.OnPropertyChanged("MappingId", this._MappingId, value);
                this._MappingId = value;
            }
        }

        public string SeriesId
        {
            get
            {
                return this._SeriesId;
            }
            set
            {
                this.OnPropertyChanged("SeriesId", this._SeriesId, value);
                this._SeriesId = value;
            }
        }

        public global::System.Int32? SupplierId
        {
            get
            {
                return this._SupplierId;
            }
            set
            {
                this.OnPropertyChanged("SupplierId", this._SupplierId, value);
                this._SupplierId = value;
            }
        }

        public global::System.Int32? ManufacturerId
        {
            get
            {
                return this._ManufacturerId;
            }
            set
            {
                this.OnPropertyChanged("ManufacturerId", this._ManufacturerId, value);
                this._ManufacturerId = value;
            }
        }

        public string SeriesName
        {
            get
            {
                return this._SeriesName;
            }
            set
            {
                this.OnPropertyChanged("SeriesName", this._SeriesName, value);
                this._SeriesName = value;
            }
        }

        public string SupplierName
        {
            get
            {
                return this._SupplierName;
            }
            set
            {
                this.OnPropertyChanged("SupplierName", this._SupplierName, value);
                this._SupplierName = value;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return this._ManufacturerName;
            }
            set
            {
                this.OnPropertyChanged("ManufacturerName", this._ManufacturerName, value);
                this._ManufacturerName = value;
            }
        }

        public string SupplierERPCode
        {
            get
            {
                return this._SupplierERPCode;
            }
            set
            {
                this.OnPropertyChanged("SupplierERPCode", this._SupplierERPCode, value);
                this._SupplierERPCode = value;
            }
        }

        public string ManufacturerERPCode
        {
            get
            {
                return this._ManufacturerERPCode;
            }
            set
            {
                this.OnPropertyChanged("ManufacturerERPCode", this._ManufacturerERPCode, value);
                this._ManufacturerERPCode = value;
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
            if ((QmcFactoryMapping._QmcFactoryMappingEntityConfiguration == null))
            {
                QmcFactoryMapping._QmcFactoryMappingEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.QmcFactoryMapping");
            }
            return QmcFactoryMapping._QmcFactoryMappingEntityConfiguration;
        }

        public override void ReloadQueries(bool includeLazyLoadQueries)
        {
        }

        public override object[] GetPropertyValues()
        {
            return new object[] {
                        this._MappingId,
                        this._SeriesId,
                        this._SupplierId,
                        this._ManufacturerId,
                        this._SeriesName,
                        this._SupplierName,
                        this._ManufacturerName,
                        this._SupplierERPCode,
                        this._ManufacturerERPCode,
                        this._Remark,
                        this._DeleteFlag};
        }

        public override void SetPropertyValues(System.Data.IDataReader reader)
        {
            if ((false == reader.IsDBNull(0)))
            {
                this._MappingId = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1)))
            {
                this._SeriesId = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2)))
            {
                this._SupplierId = reader.GetInt32(2);
            }
            if ((false == reader.IsDBNull(3)))
            {
                this._ManufacturerId = reader.GetInt32(3);
            }
            if ((false == reader.IsDBNull(4)))
            {
                this._SeriesName = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5)))
            {
                this._SupplierName = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6)))
            {
                this._ManufacturerName = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7)))
            {
                this._SupplierERPCode = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8)))
            {
                this._ManufacturerERPCode = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9)))
            {
                this._Remark = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10)))
            {
                this._DeleteFlag = reader.GetString(10);
            }
            this.ReloadQueries(false);
        }

        public override void SetPropertyValues(System.Data.DataRow row)
        {
            if ((false == row.IsNull(0)))
            {
                this._MappingId = ((int)(row[0]));
            }
            if ((false == row.IsNull(1)))
            {
                this._SeriesId = ((string)(row[1]));
            }
            if ((false == row.IsNull(2)))
            {
                this._SupplierId = ((System.Nullable<int>)(row[2]));
            }
            if ((false == row.IsNull(3)))
            {
                this._ManufacturerId = ((System.Nullable<int>)(row[3]));
            }
            if ((false == row.IsNull(4)))
            {
                this._SeriesName = ((string)(row[4]));
            }
            if ((false == row.IsNull(5)))
            {
                this._SupplierName = ((string)(row[5]));
            }
            if ((false == row.IsNull(6)))
            {
                this._ManufacturerName = ((string)(row[6]));
            }
            if ((false == row.IsNull(7)))
            {
                this._SupplierERPCode = ((string)(row[7]));
            }
            if ((false == row.IsNull(8)))
            {
                this._ManufacturerERPCode = ((string)(row[8]));
            }
            if ((false == row.IsNull(9)))
            {
                this._Remark = ((string)(row[9]));
            }
            if ((false == row.IsNull(10)))
            {
                this._DeleteFlag = ((string)(row[10]));
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
            if ((false == typeof(global::Mesnac.Entity.QmcFactoryMapping).IsAssignableFrom(obj.GetType())))
            {
                return false;
            }
            if ((((object)(this)) == ((object)(obj))))
            {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.QmcFactoryMapping)(obj)).isAttached)
                        && (this.MappingId == ((global::Mesnac.Entity.QmcFactoryMapping)(obj)).MappingId));
        }

        public static @__Columns @__Alias(string aliasName)
        {
            return new @__Columns(aliasName);
        }

        public class @__Columns
        {

            protected string aliasName;

            protected NBear.Common.PropertyItem _MappingId = new NBear.Common.PropertyItem("MappingId", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _SeriesId = new NBear.Common.PropertyItem("SeriesId", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _SupplierId = new NBear.Common.PropertyItem("SupplierId", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _ManufacturerId = new NBear.Common.PropertyItem("ManufacturerId", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _SeriesName = new NBear.Common.PropertyItem("SeriesName", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _SupplierName = new NBear.Common.PropertyItem("SupplierName", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _ManufacturerName = new NBear.Common.PropertyItem("ManufacturerName", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _SupplierERPCode = new NBear.Common.PropertyItem("SupplierERPCode", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _ManufacturerERPCode = new NBear.Common.PropertyItem("ManufacturerERPCode", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.QmcFactoryMapping");

            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.QmcFactoryMapping");

            public @__Columns()
            {
            }

            public @__Columns(string aliasName)
            {
                this.aliasName = aliasName;
            }

            public NBear.Common.PropertyItem MappingId
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _MappingId;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("MappingId", _MappingId.EntityConfiguration, _MappingId.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SeriesId
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SeriesId;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SeriesId", _SeriesId.EntityConfiguration, _SeriesId.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SupplierId
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SupplierId;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SupplierId", _SupplierId.EntityConfiguration, _SupplierId.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ManufacturerId
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ManufacturerId;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ManufacturerId", _ManufacturerId.EntityConfiguration, _ManufacturerId.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SeriesName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SeriesName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SeriesName", _SeriesName.EntityConfiguration, _SeriesName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SupplierName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SupplierName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SupplierName", _SupplierName.EntityConfiguration, _SupplierName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ManufacturerName
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ManufacturerName;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ManufacturerName", _ManufacturerName.EntityConfiguration, _ManufacturerName.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem SupplierERPCode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _SupplierERPCode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("SupplierERPCode", _SupplierERPCode.EntityConfiguration, _SupplierERPCode.PropertyConfiguration, aliasName);
                    }
                }
            }

            public NBear.Common.PropertyItem ManufacturerERPCode
            {
                get
                {
                    if ((aliasName == null))
                    {
                        return _ManufacturerERPCode;
                    }
                    else
                    {
                        return new NBear.Common.PropertyItem("ManufacturerERPCode", _ManufacturerERPCode.EntityConfiguration, _ManufacturerERPCode.PropertyConfiguration, aliasName);
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