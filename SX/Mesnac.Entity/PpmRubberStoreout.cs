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
    public partial class PpmRubberStoreoutArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PpmRubberStoreout> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PpmRubberStoreout\" isReadOnly=\"false\" isAutoPreLoad=\"false\" " +
        "isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"PpmRubberStoreout\" batchSi" +
        "ze=\"10\">\r\n  <Properties>\r\n    <Property name=\"BillNo\" type=\"System.String\" isInh" +
        "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
        "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
        "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
        "ingName=\"BillNo\" mappingColumnType=\"System.String\" sqlType=\"varchar(36)\" isPrima" +
        "ryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StorageID\" type=\"System.St" +
        "ring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=" +
        "\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fa" +
        "lse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"" +
        "false\" mappingName=\"StorageID\" mappingColumnType=\"System.String\" sqlType=\"nvarch" +
        "ar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeptCode\"" +
        " type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"fal" +
        "se\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" i" +
        "sRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeria" +
        "lizationIgnore=\"false\" mappingName=\"DeptCode\" mappingColumnType=\"System.String\" " +
        "sqlType=\"nvarchar(13)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property " +
        "name=\"BillType\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCo" +
        "mpoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLaz" +
        "yLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=" +
        "\"false\" isSerializationIgnore=\"false\" mappingName=\"BillType\" mappingColumnType=\"" +
        "System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    " +
        "<Property name=\"RecordDate\" type=\"System.Nullable`1[System.DateTime]\" isInherite" +
        "d=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=" +
        "\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProp" +
        "erty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNa" +
        "me=\"RecordDate\" mappingColumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"" +
        "datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"OutputD" +
        "ate\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"f" +
        "alse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fa" +
        "lse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPro" +
        "pertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"OutputDate\" mapping" +
        "ColumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=" +
        "\"false\" isNotNull=\"false\" />\r\n    <Property name=\"LockedFlag\" type=\"System.Strin" +
        "g\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"LockedFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"FiledFlag\" type=" +
        "\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" is" +
        "Contained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelat" +
        "ionKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializati" +
        "onIgnore=\"false\" mappingName=\"FiledFlag\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Make" +
        "rPerson\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundU" +
        "nit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"" +
        "false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\"" +
        " isSerializationIgnore=\"false\" mappingName=\"MakerPerson\" mappingColumnType=\"Syst" +
        "em.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n   " +
        " <Property name=\"ChkPerson\" type=\"System.String\" isInherited=\"false\" isReadOnly=" +
        "\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"" +
        "false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexP" +
        "ropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ChkPerson\" mappin" +
        "gColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\" isNotNul" +
        "l=\"false\" />\r\n    <Property name=\"ChkResultFlag\" type=\"System.String\" isInherite" +
        "d=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCont" +
        "ained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationK" +
        "ey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIg" +
        "nore=\"false\" mappingName=\"ChkResultFlag\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ChkD" +
        "ate\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"f" +
        "alse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fa" +
        "lse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPro" +
        "pertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ChkDate\" mappingCol" +
        "umnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"fa" +
        "lse\" isNotNull=\"false\" />\r\n    <Property name=\"InaccountDuration\" type=\"System.S" +
        "tring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained" +
        "=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"f" +
        "alse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=" +
        "\"false\" mappingName=\"InaccountDuration\" mappingColumnType=\"System.String\" sqlTyp" +
        "e=\"nvarchar(6)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"D" +
        "eleteFlag\" type=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"(0)\" isRead" +
        "Only=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriend" +
        "Key=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isI" +
        "ndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" " +
        "mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNu" +
        "ll=\"false\" />\r\n    <Property name=\"Remark\" type=\"System.String\" isInherited=\"fal" +
        "se\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false" +
        "\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"" +
        "false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Re" +
        "mark\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(100)\" isPrimaryKey=\"fa" +
        "lse\" isNotNull=\"false\" />\r\n    <Property name=\"IsFeedBack\" type=\"System.String\" " +
        "isInherited=\"false\" sqlDefaultValue=\"(0)\" isReadOnly=\"false\" isCompoundUnit=\"fal" +
        "se\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" i" +
        "sRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeria" +
        "lizationIgnore=\"false\" mappingName=\"IsFeedBack\" mappingColumnType=\"System.String" +
        "\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n" +
        "</EntityConfiguration>")]
    public partial class PpmRubberStoreout : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PpmRubberStoreoutEntityConfiguration;
        
        protected string _BillNo;
        
        protected string _StorageID;
        
        protected string _DeptCode;
        
        protected string _BillType;
        
        protected global::System.DateTime? _RecordDate;
        
        protected global::System.DateTime? _OutputDate;
        
        protected string _LockedFlag;
        
        protected string _FiledFlag;
        
        protected string _MakerPerson;
        
        protected string _ChkPerson;
        
        protected string _ChkResultFlag;
        
        protected global::System.DateTime? _ChkDate;
        
        protected string _InaccountDuration;
        
        protected string _DeleteFlag;
        
        protected string _Remark;
        
        protected string _IsFeedBack;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PpmRubberStoreout left, global::Mesnac.Entity.PpmRubberStoreout right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PpmRubberStoreout left, global::Mesnac.Entity.PpmRubberStoreout right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string BillNo {
            get {
                return this._BillNo;
            }
            set {
                this.OnPropertyChanged("BillNo", this._BillNo, value);
                this._BillNo = value;
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
        
        public string DeptCode {
            get {
                return this._DeptCode;
            }
            set {
                this.OnPropertyChanged("DeptCode", this._DeptCode, value);
                this._DeptCode = value;
            }
        }
        
        public string BillType {
            get {
                return this._BillType;
            }
            set {
                this.OnPropertyChanged("BillType", this._BillType, value);
                this._BillType = value;
            }
        }
        
        public global::System.DateTime? RecordDate {
            get {
                return this._RecordDate;
            }
            set {
                this.OnPropertyChanged("RecordDate", this._RecordDate, value);
                this._RecordDate = value;
            }
        }
        
        public global::System.DateTime? OutputDate {
            get {
                return this._OutputDate;
            }
            set {
                this.OnPropertyChanged("OutputDate", this._OutputDate, value);
                this._OutputDate = value;
            }
        }
        
        public string LockedFlag {
            get {
                return this._LockedFlag;
            }
            set {
                this.OnPropertyChanged("LockedFlag", this._LockedFlag, value);
                this._LockedFlag = value;
            }
        }
        
        public string FiledFlag {
            get {
                return this._FiledFlag;
            }
            set {
                this.OnPropertyChanged("FiledFlag", this._FiledFlag, value);
                this._FiledFlag = value;
            }
        }
        
        public string MakerPerson {
            get {
                return this._MakerPerson;
            }
            set {
                this.OnPropertyChanged("MakerPerson", this._MakerPerson, value);
                this._MakerPerson = value;
            }
        }
        
        public string ChkPerson {
            get {
                return this._ChkPerson;
            }
            set {
                this.OnPropertyChanged("ChkPerson", this._ChkPerson, value);
                this._ChkPerson = value;
            }
        }
        
        public string ChkResultFlag {
            get {
                return this._ChkResultFlag;
            }
            set {
                this.OnPropertyChanged("ChkResultFlag", this._ChkResultFlag, value);
                this._ChkResultFlag = value;
            }
        }
        
        public global::System.DateTime? ChkDate {
            get {
                return this._ChkDate;
            }
            set {
                this.OnPropertyChanged("ChkDate", this._ChkDate, value);
                this._ChkDate = value;
            }
        }
        
        public string InaccountDuration {
            get {
                return this._InaccountDuration;
            }
            set {
                this.OnPropertyChanged("InaccountDuration", this._InaccountDuration, value);
                this._InaccountDuration = value;
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
        
        public string IsFeedBack {
            get {
                return this._IsFeedBack;
            }
            set {
                this.OnPropertyChanged("IsFeedBack", this._IsFeedBack, value);
                this._IsFeedBack = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PpmRubberStoreout._PpmRubberStoreoutEntityConfiguration == null)) {
                PpmRubberStoreout._PpmRubberStoreoutEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PpmRubberStoreout");
            }
            return PpmRubberStoreout._PpmRubberStoreoutEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._BillNo,
                    this._StorageID,
                    this._DeptCode,
                    this._BillType,
                    this._RecordDate,
                    this._OutputDate,
                    this._LockedFlag,
                    this._FiledFlag,
                    this._MakerPerson,
                    this._ChkPerson,
                    this._ChkResultFlag,
                    this._ChkDate,
                    this._InaccountDuration,
                    this._DeleteFlag,
                    this._Remark,
                    this._IsFeedBack};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._BillNo = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StorageID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._DeptCode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._BillType = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._RecordDate = this.GetDateTime(reader, 4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._OutputDate = this.GetDateTime(reader, 5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._LockedFlag = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._FiledFlag = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._MakerPerson = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._ChkPerson = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._ChkResultFlag = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._ChkDate = this.GetDateTime(reader, 11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._InaccountDuration = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._DeleteFlag = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._Remark = reader.GetString(14);
            }
            if ((false == reader.IsDBNull(15))) {
                this._IsFeedBack = reader.GetString(15);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._BillNo = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StorageID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._DeptCode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._BillType = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._RecordDate = this.GetDateTime(row, 4);
            }
            if ((false == row.IsNull(5))) {
                this._OutputDate = this.GetDateTime(row, 5);
            }
            if ((false == row.IsNull(6))) {
                this._LockedFlag = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._FiledFlag = ((string)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._MakerPerson = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._ChkPerson = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._ChkResultFlag = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._ChkDate = this.GetDateTime(row, 11);
            }
            if ((false == row.IsNull(12))) {
                this._InaccountDuration = ((string)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._DeleteFlag = ((string)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._Remark = ((string)(row[14]));
            }
            if ((false == row.IsNull(15))) {
                this._IsFeedBack = ((string)(row[15]));
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
            if ((false == typeof(global::Mesnac.Entity.PpmRubberStoreout).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.PpmRubberStoreout)(obj)).isAttached) 
                        && (this.BillNo == ((global::Mesnac.Entity.PpmRubberStoreout)(obj)).BillNo));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _BillNo = new NBear.Common.PropertyItem("BillNo", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _DeptCode = new NBear.Common.PropertyItem("DeptCode", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _BillType = new NBear.Common.PropertyItem("BillType", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _OutputDate = new NBear.Common.PropertyItem("OutputDate", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _LockedFlag = new NBear.Common.PropertyItem("LockedFlag", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _FiledFlag = new NBear.Common.PropertyItem("FiledFlag", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _MakerPerson = new NBear.Common.PropertyItem("MakerPerson", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _ChkPerson = new NBear.Common.PropertyItem("ChkPerson", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _ChkResultFlag = new NBear.Common.PropertyItem("ChkResultFlag", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _ChkDate = new NBear.Common.PropertyItem("ChkDate", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _InaccountDuration = new NBear.Common.PropertyItem("InaccountDuration", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.PpmRubberStoreout");
            
            protected NBear.Common.PropertyItem _IsFeedBack = new NBear.Common.PropertyItem("IsFeedBack", "Mesnac.Entity.PpmRubberStoreout");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem BillNo {
                get {
                    if ((aliasName == null)) {
                        return _BillNo;
                    }
                    else {
                        return new NBear.Common.PropertyItem("BillNo", _BillNo.EntityConfiguration, _BillNo.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem DeptCode {
                get {
                    if ((aliasName == null)) {
                        return _DeptCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DeptCode", _DeptCode.EntityConfiguration, _DeptCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem BillType {
                get {
                    if ((aliasName == null)) {
                        return _BillType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("BillType", _BillType.EntityConfiguration, _BillType.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RecordDate {
                get {
                    if ((aliasName == null)) {
                        return _RecordDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RecordDate", _RecordDate.EntityConfiguration, _RecordDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OutputDate {
                get {
                    if ((aliasName == null)) {
                        return _OutputDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OutputDate", _OutputDate.EntityConfiguration, _OutputDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem LockedFlag {
                get {
                    if ((aliasName == null)) {
                        return _LockedFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("LockedFlag", _LockedFlag.EntityConfiguration, _LockedFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem FiledFlag {
                get {
                    if ((aliasName == null)) {
                        return _FiledFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FiledFlag", _FiledFlag.EntityConfiguration, _FiledFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem MakerPerson {
                get {
                    if ((aliasName == null)) {
                        return _MakerPerson;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MakerPerson", _MakerPerson.EntityConfiguration, _MakerPerson.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ChkPerson {
                get {
                    if ((aliasName == null)) {
                        return _ChkPerson;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ChkPerson", _ChkPerson.EntityConfiguration, _ChkPerson.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ChkResultFlag {
                get {
                    if ((aliasName == null)) {
                        return _ChkResultFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ChkResultFlag", _ChkResultFlag.EntityConfiguration, _ChkResultFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ChkDate {
                get {
                    if ((aliasName == null)) {
                        return _ChkDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ChkDate", _ChkDate.EntityConfiguration, _ChkDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem InaccountDuration {
                get {
                    if ((aliasName == null)) {
                        return _InaccountDuration;
                    }
                    else {
                        return new NBear.Common.PropertyItem("InaccountDuration", _InaccountDuration.EntityConfiguration, _InaccountDuration.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem IsFeedBack {
                get {
                    if ((aliasName == null)) {
                        return _IsFeedBack;
                    }
                    else {
                        return new NBear.Common.PropertyItem("IsFeedBack", _IsFeedBack.EntityConfiguration, _IsFeedBack.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
