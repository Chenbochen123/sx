//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.296
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
    public partial class BasStorageFlowArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.BasStorageFlow> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.BasStorageFlow\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isB" +
        "atchUpdate=\"false\" isRelation=\"false\" mappingName=\"BasStorageFlow\" batchSize=\"10" +
        "\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited=\"" +
        "false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fal" +
        "se\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty" +
        "=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"" +
        "ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotN" +
        "ull=\"true\" />\r\n    <Property name=\"FlowID\" type=\"System.String\" isInherited=\"fal" +
        "se\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false" +
        "\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"" +
        "false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Fl" +
        "owID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"fal" +
        "se\" isNotNull=\"false\" />\r\n    <Property name=\"SourceStorage\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"SourceStorage\" mappingColumnType=\"System.String\" sqlType=\"nvarch" +
        "ar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"TargetSt" +
        "orage\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
        "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
        "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
        "sSerializationIgnore=\"false\" mappingName=\"TargetStorage\" mappingColumnType=\"Syst" +
        "em.String\" sqlType=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  " +
        "  <Property name=\"LimitTimes\" type=\"System.Nullable`1[System.Int32]\" isInherited" +
        "=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"" +
        "false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPrope" +
        "rty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNam" +
        "e=\"LimitTimes\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ForbiddenFlag\" t" +
        "ype=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ForbiddenFlag\" mappingCo" +
        "lumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"SeqIdx\" type=\"System.Nullable`1[System.Int32]\" isInher" +
        "ited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQue" +
        "ry=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexP" +
        "roperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappin" +
        "gName=\"SeqIdx\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" type" +
        "=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"false\" is" +
        "CompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isL" +
        "azyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDes" +
        "c=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnTy" +
        "pe=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n" +
        "    <Property name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=" +
        "\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"" +
        "false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexP" +
        "ropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingCo" +
        "lumnType=\"System.String\" sqlType=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=" +
        "\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class BasStorageFlow : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _BasStorageFlowEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _FlowID;
        
        protected string _SourceStorage;
        
        protected string _TargetStorage;
        
        protected global::System.Int32? _LimitTimes;
        
        protected string _ForbiddenFlag;
        
        protected global::System.Int32? _SeqIdx;
        
        protected string _DeleteFlag;
        
        protected string _Remark;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.BasStorageFlow left, global::Mesnac.Entity.BasStorageFlow right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.BasStorageFlow left, global::Mesnac.Entity.BasStorageFlow right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string FlowID {
            get {
                return this._FlowID;
            }
            set {
                this.OnPropertyChanged("FlowID", this._FlowID, value);
                this._FlowID = value;
            }
        }
        
        public string SourceStorage {
            get {
                return this._SourceStorage;
            }
            set {
                this.OnPropertyChanged("SourceStorage", this._SourceStorage, value);
                this._SourceStorage = value;
            }
        }
        
        public string TargetStorage {
            get {
                return this._TargetStorage;
            }
            set {
                this.OnPropertyChanged("TargetStorage", this._TargetStorage, value);
                this._TargetStorage = value;
            }
        }
        
        public global::System.Int32? LimitTimes {
            get {
                return this._LimitTimes;
            }
            set {
                this.OnPropertyChanged("LimitTimes", this._LimitTimes, value);
                this._LimitTimes = value;
            }
        }
        
        public string ForbiddenFlag {
            get {
                return this._ForbiddenFlag;
            }
            set {
                this.OnPropertyChanged("ForbiddenFlag", this._ForbiddenFlag, value);
                this._ForbiddenFlag = value;
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
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((BasStorageFlow._BasStorageFlowEntityConfiguration == null)) {
                BasStorageFlow._BasStorageFlowEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.BasStorageFlow");
            }
            return BasStorageFlow._BasStorageFlowEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._FlowID,
                    this._SourceStorage,
                    this._TargetStorage,
                    this._LimitTimes,
                    this._ForbiddenFlag,
                    this._SeqIdx,
                    this._DeleteFlag,
                    this._Remark};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._FlowID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._SourceStorage = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._TargetStorage = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._LimitTimes = reader.GetInt32(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._ForbiddenFlag = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._SeqIdx = reader.GetInt32(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._DeleteFlag = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._Remark = reader.GetString(8);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._FlowID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._SourceStorage = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._TargetStorage = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._LimitTimes = ((System.Nullable<int>)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._ForbiddenFlag = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._SeqIdx = ((System.Nullable<int>)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._DeleteFlag = ((string)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._Remark = ((string)(row[8]));
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
            if ((false == typeof(global::Mesnac.Entity.BasStorageFlow).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.BasStorageFlow)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.BasStorageFlow)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _FlowID = new NBear.Common.PropertyItem("FlowID", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _SourceStorage = new NBear.Common.PropertyItem("SourceStorage", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _TargetStorage = new NBear.Common.PropertyItem("TargetStorage", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _LimitTimes = new NBear.Common.PropertyItem("LimitTimes", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _ForbiddenFlag = new NBear.Common.PropertyItem("ForbiddenFlag", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _SeqIdx = new NBear.Common.PropertyItem("SeqIdx", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.BasStorageFlow");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.BasStorageFlow");
            
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
            
            public NBear.Common.PropertyItem FlowID {
                get {
                    if ((aliasName == null)) {
                        return _FlowID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FlowID", _FlowID.EntityConfiguration, _FlowID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SourceStorage {
                get {
                    if ((aliasName == null)) {
                        return _SourceStorage;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SourceStorage", _SourceStorage.EntityConfiguration, _SourceStorage.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem TargetStorage {
                get {
                    if ((aliasName == null)) {
                        return _TargetStorage;
                    }
                    else {
                        return new NBear.Common.PropertyItem("TargetStorage", _TargetStorage.EntityConfiguration, _TargetStorage.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem LimitTimes {
                get {
                    if ((aliasName == null)) {
                        return _LimitTimes;
                    }
                    else {
                        return new NBear.Common.PropertyItem("LimitTimes", _LimitTimes.EntityConfiguration, _LimitTimes.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ForbiddenFlag {
                get {
                    if ((aliasName == null)) {
                        return _ForbiddenFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ForbiddenFlag", _ForbiddenFlag.EntityConfiguration, _ForbiddenFlag.PropertyConfiguration, aliasName);
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
        }
    }
}