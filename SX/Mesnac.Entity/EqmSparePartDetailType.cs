//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity {
    using System;
    using System.Xml.Serialization;
    using NBear.Common;
    
    
    [System.SerializableAttribute()]
    public partial class EqmSparePartDetailTypeArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.EqmSparePartDetailType> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.EqmSparePartDetailType\" isReadOnly=\"false\" isAutoPreLoad=\"fa" +
        "lse\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"EqmSparePartDetailTyp" +
        "e\" batchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int3" +
        "2\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryK" +
        "ey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"MainTypeID\" type=\"System.Int3" +
        "2\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"MainTypeID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPr" +
        "imaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DetailTypeCode\" type=" +
        "\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" is" +
        "Contained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelat" +
        "ionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializati" +
        "onIgnore=\"false\" mappingName=\"DetailTypeCode\" mappingColumnType=\"System.String\" " +
        "sqlType=\"char(4)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=" +
        "\"DetailTypeName\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isC" +
        "ompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLa" +
        "zyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc" +
        "=\"false\" isSerializationIgnore=\"false\" mappingName=\"DetailTypeName\" mappingColum" +
        "nType=\"System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"fal" +
        "se\" />\r\n    <Property name=\"AutoIn\" type=\"System.String\" isInherited=\"false\" sql" +
        "DefaultValue=\"(0)\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\"" +
        " isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" is" +
        "IndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" " +
        "mappingName=\"AutoIn\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrima" +
        "ryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" type=\"System.Str" +
        "ing\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"" +
        "false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fal" +
        "se\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"f" +
        "alse\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(5" +
        "0)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" t" +
        "ype=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColum" +
        "nType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" /" +
        ">\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class EqmSparePartDetailType : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _EqmSparePartDetailTypeEntityConfiguration;
        
        protected int _ObjID;
        
        protected int _MainTypeID;
        
        protected string _DetailTypeCode;
        
        protected string _DetailTypeName;
        
        protected string _AutoIn;
        
        protected string _Remark;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.EqmSparePartDetailType left, global::Mesnac.Entity.EqmSparePartDetailType right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.EqmSparePartDetailType left, global::Mesnac.Entity.EqmSparePartDetailType right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public int MainTypeID {
            get {
                return this._MainTypeID;
            }
            set {
                this.OnPropertyChanged("MainTypeID", this._MainTypeID, value);
                this._MainTypeID = value;
            }
        }
        
        public string DetailTypeCode {
            get {
                return this._DetailTypeCode;
            }
            set {
                this.OnPropertyChanged("DetailTypeCode", this._DetailTypeCode, value);
                this._DetailTypeCode = value;
            }
        }
        
        public string DetailTypeName {
            get {
                return this._DetailTypeName;
            }
            set {
                this.OnPropertyChanged("DetailTypeName", this._DetailTypeName, value);
                this._DetailTypeName = value;
            }
        }
        
        public string AutoIn {
            get {
                return this._AutoIn;
            }
            set {
                this.OnPropertyChanged("AutoIn", this._AutoIn, value);
                this._AutoIn = value;
            }
        }
        
        public string Remark {
            get {
                return this._Remark;
            }
            set {
                this.OnPropertyChanged("Remark", this._Remark, value);
                this._Remark = value;
            }
        }
        
        public string DeleteFlag {
            get {
                return this._DeleteFlag;
            }
            set {
                this.OnPropertyChanged("DeleteFlag", this._DeleteFlag, value);
                this._DeleteFlag = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((EqmSparePartDetailType._EqmSparePartDetailTypeEntityConfiguration == null)) {
                EqmSparePartDetailType._EqmSparePartDetailTypeEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.EqmSparePartDetailType");
            }
            return EqmSparePartDetailType._EqmSparePartDetailTypeEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._MainTypeID,
                    this._DetailTypeCode,
                    this._DetailTypeName,
                    this._AutoIn,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._MainTypeID = reader.GetInt32(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._DetailTypeCode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._DetailTypeName = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._AutoIn = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._Remark = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._DeleteFlag = reader.GetString(6);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._MainTypeID = ((int)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._DetailTypeCode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._DetailTypeName = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._AutoIn = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._Remark = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._DeleteFlag = ((string)(row[6]));
            }
            this.ReloadQueries(false);
        }
        
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        
        public override bool Equals(object obj) {
            if ((obj == null)) {
                return false;
            }
            if ((false == typeof(global::Mesnac.Entity.EqmSparePartDetailType).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.EqmSparePartDetailType)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.EqmSparePartDetailType)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.EqmSparePartDetailType");
            
            protected NBear.Common.PropertyItem _MainTypeID = new NBear.Common.PropertyItem("MainTypeID", "Mesnac.Entity.EqmSparePartDetailType");
            
            protected NBear.Common.PropertyItem _DetailTypeCode = new NBear.Common.PropertyItem("DetailTypeCode", "Mesnac.Entity.EqmSparePartDetailType");
            
            protected NBear.Common.PropertyItem _DetailTypeName = new NBear.Common.PropertyItem("DetailTypeName", "Mesnac.Entity.EqmSparePartDetailType");
            
            protected NBear.Common.PropertyItem _AutoIn = new NBear.Common.PropertyItem("AutoIn", "Mesnac.Entity.EqmSparePartDetailType");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.EqmSparePartDetailType");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.EqmSparePartDetailType");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem ObjID {
                get {
                    if ((aliasName == null)) {
                        return _ObjID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ObjID", _ObjID.EntityConfiguration, _ObjID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem MainTypeID {
                get {
                    if ((aliasName == null)) {
                        return _MainTypeID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MainTypeID", _MainTypeID.EntityConfiguration, _MainTypeID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem DetailTypeCode {
                get {
                    if ((aliasName == null)) {
                        return _DetailTypeCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DetailTypeCode", _DetailTypeCode.EntityConfiguration, _DetailTypeCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem DetailTypeName {
                get {
                    if ((aliasName == null)) {
                        return _DetailTypeName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DetailTypeName", _DetailTypeName.EntityConfiguration, _DetailTypeName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem AutoIn {
                get {
                    if ((aliasName == null)) {
                        return _AutoIn;
                    }
                    else {
                        return new NBear.Common.PropertyItem("AutoIn", _AutoIn.EntityConfiguration, _AutoIn.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Remark {
                get {
                    if ((aliasName == null)) {
                        return _Remark;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Remark", _Remark.EntityConfiguration, _Remark.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem DeleteFlag {
                get {
                    if ((aliasName == null)) {
                        return _DeleteFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DeleteFlag", _DeleteFlag.EntityConfiguration, _DeleteFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
