//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.1026
//
//     �Դ��ļ��ĸ��Ŀ��ܻᵼ�²���ȷ����Ϊ���������
//     �������ɴ��룬��Щ���Ľ��ᶪʧ��
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mesnac.Entity {
    using System;
    using System.Xml.Serialization;
    using NBear.Common;
    
    
    [System.SerializableAttribute()]
    public partial class BasStoragePlaceArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.BasStoragePlace> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.BasStoragePlace\" isReadOnly=\"false\" isAutoPreLoad=\"false\" is" +
        "BatchUpdate=\"false\" isRelation=\"false\" mappingName=\"BasStoragePlace\" batchSize=\"" +
        "10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited" +
        "=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNo" +
        "tNull=\"true\" />\r\n    <Property name=\"StorageID\" type=\"System.String\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"StorageID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimary" +
        "Key=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"StoragePlaceID\" type=\"Syst" +
        "em.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isConta" +
        "ined=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKe" +
        "y=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgn" +
        "ore=\"false\" mappingName=\"StoragePlaceID\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"nvarchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=" +
        "\"StoragePlaceName\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" i" +
        "sCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" is" +
        "LazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDe" +
        "sc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StoragePlaceName\" mappingC" +
        "olumnType=\"System.String\" sqlType=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull" +
        "=\"false\" />\r\n    <Property name=\"DefaultFlag\" type=\"System.String\" isInherited=\"" +
        "false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContain" +
        "ed=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=" +
        "\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnor" +
        "e=\"false\" mappingName=\"DefaultFlag\" mappingColumnType=\"System.String\" sqlType=\"c" +
        "har(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"AutoGenFl" +
        "ag\" type=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"f" +
        "alse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fa" +
        "lse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPro" +
        "pertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"AutoGenFlag\" mappin" +
        "gColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"fa" +
        "lse\" />\r\n    <Property name=\"SeqIdx\" type=\"System.Nullable`1[System.Int32]\" isIn" +
        "herited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" is" +
        "Query=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInd" +
        "exProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" map" +
        "pingName=\"SeqIdx\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"i" +
        "nt\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"LockFlag\" typ" +
        "e=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"false\" i" +
        "sCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" is" +
        "LazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDe" +
        "sc=\"false\" isSerializationIgnore=\"false\" mappingName=\"LockFlag\" mappingColumnTyp" +
        "e=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n " +
        "   <Property name=\"CancelFlag\" type=\"System.String\" isInherited=\"false\" sqlDefau" +
        "ltValue=\"(0)\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQu" +
        "ery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndex" +
        "Property=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappi" +
        "ngName=\"CancelFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimar" +
        "yKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" type=\"System." +
        "String\" isInherited=\"false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"Syste" +
        "m.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Prop" +
        "erty name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" i" +
        "sCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" is" +
        "LazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDe" +
        "sc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=" +
        "\"System.String\" sqlType=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" /" +
        ">\r\n    <Property name=\"ShiYanFlag\" type=\"System.String\" isInherited=\"false\" isRe" +
        "adOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrie" +
        "ndKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" i" +
        "sIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ShiYanFlag" +
        "\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNot" +
        "Null=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class BasStoragePlace : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _BasStoragePlaceEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _StorageID;
        
        protected string _StoragePlaceID;
        
        protected string _StoragePlaceName;
        
        protected string _DefaultFlag;
        
        protected string _AutoGenFlag;
        
        protected global::System.Int32? _SeqIdx;
        
        protected string _LockFlag;
        
        protected string _CancelFlag;
        
        protected string _DeleteFlag;
        
        protected string _Remark;
        
        protected string _ShiYanFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.BasStoragePlace left, global::Mesnac.Entity.BasStoragePlace right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.BasStoragePlace left, global::Mesnac.Entity.BasStoragePlace right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string StorageID {
            get {
                return this._StorageID;
            }
            set {
                this.OnPropertyChanged("StorageID", this._StorageID, value);
                this._StorageID = value;
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
        
        public string StoragePlaceName {
            get {
                return this._StoragePlaceName;
            }
            set {
                this.OnPropertyChanged("StoragePlaceName", this._StoragePlaceName, value);
                this._StoragePlaceName = value;
            }
        }
        
        public string DefaultFlag {
            get {
                return this._DefaultFlag;
            }
            set {
                this.OnPropertyChanged("DefaultFlag", this._DefaultFlag, value);
                this._DefaultFlag = value;
            }
        }
        
        public string AutoGenFlag {
            get {
                return this._AutoGenFlag;
            }
            set {
                this.OnPropertyChanged("AutoGenFlag", this._AutoGenFlag, value);
                this._AutoGenFlag = value;
            }
        }
        
        public global::System.Int32? SeqIdx {
            get {
                return this._SeqIdx;
            }
            set {
                this.OnPropertyChanged("SeqIdx", this._SeqIdx, value);
                this._SeqIdx = value;
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
        
        public string DeleteFlag {
            get {
                return this._DeleteFlag;
            }
            set {
                this.OnPropertyChanged("DeleteFlag", this._DeleteFlag, value);
                this._DeleteFlag = value;
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
        
        public string ShiYanFlag {
            get {
                return this._ShiYanFlag;
            }
            set {
                this.OnPropertyChanged("ShiYanFlag", this._ShiYanFlag, value);
                this._ShiYanFlag = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((BasStoragePlace._BasStoragePlaceEntityConfiguration == null)) {
                BasStoragePlace._BasStoragePlaceEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.BasStoragePlace");
            }
            return BasStoragePlace._BasStoragePlaceEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._StorageID,
                    this._StoragePlaceID,
                    this._StoragePlaceName,
                    this._DefaultFlag,
                    this._AutoGenFlag,
                    this._SeqIdx,
                    this._LockFlag,
                    this._CancelFlag,
                    this._DeleteFlag,
                    this._Remark,
                    this._ShiYanFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StorageID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._StoragePlaceID = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._StoragePlaceName = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._DefaultFlag = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._AutoGenFlag = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._SeqIdx = reader.GetInt32(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._LockFlag = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._CancelFlag = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._DeleteFlag = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._Remark = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._ShiYanFlag = reader.GetString(11);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StorageID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._StoragePlaceID = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._StoragePlaceName = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._DefaultFlag = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._AutoGenFlag = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._SeqIdx = ((System.Nullable<int>)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._LockFlag = ((string)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._CancelFlag = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._DeleteFlag = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._Remark = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._ShiYanFlag = ((string)(row[11]));
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
            if ((false == typeof(global::Mesnac.Entity.BasStoragePlace).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.BasStoragePlace)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.BasStoragePlace)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _StoragePlaceName = new NBear.Common.PropertyItem("StoragePlaceName", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _DefaultFlag = new NBear.Common.PropertyItem("DefaultFlag", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _AutoGenFlag = new NBear.Common.PropertyItem("AutoGenFlag", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _SeqIdx = new NBear.Common.PropertyItem("SeqIdx", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _LockFlag = new NBear.Common.PropertyItem("LockFlag", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _CancelFlag = new NBear.Common.PropertyItem("CancelFlag", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.BasStoragePlace");
            
            protected NBear.Common.PropertyItem _ShiYanFlag = new NBear.Common.PropertyItem("ShiYanFlag", "Mesnac.Entity.BasStoragePlace");
            
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
            
            public NBear.Common.PropertyItem StorageID {
                get {
                    if ((aliasName == null)) {
                        return _StorageID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StorageID", _StorageID.EntityConfiguration, _StorageID.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem StoragePlaceName {
                get {
                    if ((aliasName == null)) {
                        return _StoragePlaceName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoragePlaceName", _StoragePlaceName.EntityConfiguration, _StoragePlaceName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem DefaultFlag {
                get {
                    if ((aliasName == null)) {
                        return _DefaultFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DefaultFlag", _DefaultFlag.EntityConfiguration, _DefaultFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem AutoGenFlag {
                get {
                    if ((aliasName == null)) {
                        return _AutoGenFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("AutoGenFlag", _AutoGenFlag.EntityConfiguration, _AutoGenFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SeqIdx {
                get {
                    if ((aliasName == null)) {
                        return _SeqIdx;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SeqIdx", _SeqIdx.EntityConfiguration, _SeqIdx.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem ShiYanFlag {
                get {
                    if ((aliasName == null)) {
                        return _ShiYanFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiYanFlag", _ShiYanFlag.EntityConfiguration, _ShiYanFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}