//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
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
    public partial class BasStoragePlaceSubArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.BasStoragePlaceSub> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.BasStoragePlaceSub\" isReadOnly=\"false\" isAutoPreLoad=\"false\"" +
        " isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"BasStoragePlaceSub\" batch" +
        "Size=\"10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInh" +
        "erited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQu" +
        "ery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndex" +
        "Property=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappi" +
        "ngName=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true" +
        "\" isNotNull=\"true\" />\r\n    <Property name=\"StoragePlaceID\" type=\"System.String\" " +
        "isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false" +
        "\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" i" +
        "sIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\"" +
        " mappingName=\"StoragePlaceID\" mappingColumnType=\"System.String\" sqlType=\"nvarcha" +
        "r(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"StoragePla" +
        "ceSubID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"StoragePlaceSubID\" mappingColumnType" +
        "=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" /" +
        ">\r\n    <Property name=\"StoragePlaceSubName\" type=\"System.String\" isInherited=\"fa" +
        "lse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fals" +
        "e\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=" +
        "\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"S" +
        "toragePlaceSubName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(100)\" is" +
        "PrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Barcode\" type=\"Syst" +
        "em.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isConta" +
        "ined=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKe" +
        "y=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgn" +
        "ore=\"false\" mappingName=\"Barcode\" mappingColumnType=\"System.String\" sqlType=\"nva" +
        "rchar(30)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"UseFla" +
        "g\" type=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"fa" +
        "lse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fal" +
        "se\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProp" +
        "ertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"UseFlag\" mappingColu" +
        "mnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" " +
        "/>\r\n    <Property name=\"LockFlag\" type=\"System.String\" isInherited=\"false\" isRea" +
        "dOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrien" +
        "dKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" is" +
        "IndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"LockFlag\" m" +
        "appingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNul" +
        "l=\"false\" />\r\n    <Property name=\"CancelFlag\" type=\"System.String\" isInherited=\"" +
        "false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fa" +
        "lse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPropert" +
        "y=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=" +
        "\"CancelFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"f" +
        "alse\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class BasStoragePlaceSub : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _BasStoragePlaceSubEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _StoragePlaceID;
        
        protected string _StoragePlaceSubID;
        
        protected string _StoragePlaceSubName;
        
        protected string _Barcode;
        
        protected string _UseFlag;
        
        protected string _LockFlag;
        
        protected string _CancelFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.BasStoragePlaceSub left, global::Mesnac.Entity.BasStoragePlaceSub right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.BasStoragePlaceSub left, global::Mesnac.Entity.BasStoragePlaceSub right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string StoragePlaceID {
            get {
                return this._StoragePlaceID;
            }
            set {
                this.OnPropertyChanged("StoragePlaceID", this._StoragePlaceID, value);
                this._StoragePlaceID = value;
            }
        }
        
        public string StoragePlaceSubID {
            get {
                return this._StoragePlaceSubID;
            }
            set {
                this.OnPropertyChanged("StoragePlaceSubID", this._StoragePlaceSubID, value);
                this._StoragePlaceSubID = value;
            }
        }
        
        public string StoragePlaceSubName {
            get {
                return this._StoragePlaceSubName;
            }
            set {
                this.OnPropertyChanged("StoragePlaceSubName", this._StoragePlaceSubName, value);
                this._StoragePlaceSubName = value;
            }
        }
        
        public string Barcode {
            get {
                return this._Barcode;
            }
            set {
                this.OnPropertyChanged("Barcode", this._Barcode, value);
                this._Barcode = value;
            }
        }
        
        public string UseFlag {
            get {
                return this._UseFlag;
            }
            set {
                this.OnPropertyChanged("UseFlag", this._UseFlag, value);
                this._UseFlag = value;
            }
        }
        
        public string LockFlag {
            get {
                return this._LockFlag;
            }
            set {
                this.OnPropertyChanged("LockFlag", this._LockFlag, value);
                this._LockFlag = value;
            }
        }
        
        public string CancelFlag {
            get {
                return this._CancelFlag;
            }
            set {
                this.OnPropertyChanged("CancelFlag", this._CancelFlag, value);
                this._CancelFlag = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((BasStoragePlaceSub._BasStoragePlaceSubEntityConfiguration == null)) {
                BasStoragePlaceSub._BasStoragePlaceSubEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.BasStoragePlaceSub");
            }
            return BasStoragePlaceSub._BasStoragePlaceSubEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._StoragePlaceID,
                    this._StoragePlaceSubID,
                    this._StoragePlaceSubName,
                    this._Barcode,
                    this._UseFlag,
                    this._LockFlag,
                    this._CancelFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StoragePlaceID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._StoragePlaceSubID = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._StoragePlaceSubName = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._Barcode = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._UseFlag = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._LockFlag = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._CancelFlag = reader.GetString(7);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StoragePlaceID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._StoragePlaceSubID = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._StoragePlaceSubName = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._Barcode = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._UseFlag = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._LockFlag = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._CancelFlag = ((string)(row[7]));
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
            if ((false == typeof(global::Mesnac.Entity.BasStoragePlaceSub).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.BasStoragePlaceSub)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.BasStoragePlaceSub)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _StoragePlaceSubID = new NBear.Common.PropertyItem("StoragePlaceSubID", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _StoragePlaceSubName = new NBear.Common.PropertyItem("StoragePlaceSubName", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _UseFlag = new NBear.Common.PropertyItem("UseFlag", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _LockFlag = new NBear.Common.PropertyItem("LockFlag", "Mesnac.Entity.BasStoragePlaceSub");
            
            protected NBear.Common.PropertyItem _CancelFlag = new NBear.Common.PropertyItem("CancelFlag", "Mesnac.Entity.BasStoragePlaceSub");
            
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
            
            public NBear.Common.PropertyItem StoragePlaceID {
                get {
                    if ((aliasName == null)) {
                        return _StoragePlaceID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoragePlaceID", _StoragePlaceID.EntityConfiguration, _StoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StoragePlaceSubID {
                get {
                    if ((aliasName == null)) {
                        return _StoragePlaceSubID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoragePlaceSubID", _StoragePlaceSubID.EntityConfiguration, _StoragePlaceSubID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StoragePlaceSubName {
                get {
                    if ((aliasName == null)) {
                        return _StoragePlaceSubName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoragePlaceSubName", _StoragePlaceSubName.EntityConfiguration, _StoragePlaceSubName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Barcode {
                get {
                    if ((aliasName == null)) {
                        return _Barcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Barcode", _Barcode.EntityConfiguration, _Barcode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem UseFlag {
                get {
                    if ((aliasName == null)) {
                        return _UseFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("UseFlag", _UseFlag.EntityConfiguration, _UseFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem LockFlag {
                get {
                    if ((aliasName == null)) {
                        return _LockFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("LockFlag", _LockFlag.EntityConfiguration, _LockFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem CancelFlag {
                get {
                    if ((aliasName == null)) {
                        return _CancelFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("CancelFlag", _CancelFlag.EntityConfiguration, _CancelFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
