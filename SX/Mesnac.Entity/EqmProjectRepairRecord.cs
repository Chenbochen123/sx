//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
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
    public partial class EqmProjectRepairRecordArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.EqmProjectRepairRecord> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.EqmProjectRepairRecord\" isReadOnly=\"false\" isAutoPreLoad=\"fa" +
        "lse\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"EqmProjectRepairRecor" +
        "d\" batchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int3" +
        "2\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimary" +
        "Key=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"MainDailyID\" type=\"System.St" +
        "ring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=" +
        "\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fa" +
        "lse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"" +
        "false\" mappingName=\"MainDailyID\" mappingColumnType=\"System.String\" sqlType=\"char" +
        "(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"EquipCode\" " +
        "type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"fals" +
        "e\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" is" +
        "RelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerial" +
        "izationIgnore=\"false\" mappingName=\"EquipCode\" mappingColumnType=\"System.String\" " +
        "sqlType=\"char(5)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=" +
        "\"ShiftID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
        "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
        "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
        "\" isSerializationIgnore=\"false\" mappingName=\"ShiftID\" mappingColumnType=\"System." +
        "String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Proper" +
        "ty name=\"RepairDate\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"RepairDate\" mappingColum" +
        "nType=\"System.String\" sqlType=\"char(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" " +
        "/>\r\n    <Property name=\"RepairStartDate\" type=\"System.String\" isInherited=\"false" +
        "\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Repa" +
        "irStartDate\" mappingColumnType=\"System.String\" sqlType=\"char(19)\" isPrimaryKey=\"" +
        "false\" isNotNull=\"false\" />\r\n    <Property name=\"RepairEndDate\" type=\"System.Str" +
        "ing\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"" +
        "false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fal" +
        "se\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"f" +
        "alse\" mappingName=\"RepairEndDate\" mappingColumnType=\"System.String\" sqlType=\"cha" +
        "r(19)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"RepairSpen" +
        "dTime\" type=\"System.Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"" +
        "false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"f" +
        "alse\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPr" +
        "opertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"RepairSpendTime\" m" +
        "appingColumnType=\"System.Nullable`1[System.Decimal]\" sqlType=\"decimal\" isPrimary" +
        "Key=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"RepairUser\" type=\"System.S" +
        "tring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained" +
        "=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"f" +
        "alse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=" +
        "\"false\" mappingName=\"RepairUser\" mappingColumnType=\"System.String\" sqlType=\"varc" +
        "har(20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"RepairTy" +
        "pe\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"" +
        "false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false" +
        "\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSe" +
        "rializationIgnore=\"false\" mappingName=\"RepairType\" mappingColumnType=\"System.Str" +
        "ing\" sqlType=\"nvarchar(200)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pro" +
        "perty name=\"RepairPart\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fal" +
        "se\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fals" +
        "e\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPrope" +
        "rtyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"RepairPart\" mappingCo" +
        "lumnType=\"System.String\" sqlType=\"char(6)\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"FaultDetail\" type=\"System.String\" isInherited=\"false\" " +
        "isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
        "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
        "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"FaultD" +
        "etail\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(500)\" isPrimaryKey=\"f" +
        "alse\" isNotNull=\"false\" />\r\n    <Property name=\"RepairResult\" type=\"System.Strin" +
        "g\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"RepairResult\" mappingColumnType=\"System.String\" sqlType=\"nvarch" +
        "ar(500)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"RecordDa" +
        "te\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly=\"fa" +
        "lse\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fal" +
        "se\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProp" +
        "ertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"RecordDate\" mappingC" +
        "olumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrimaryKey=\"" +
        "false\" isNotNull=\"false\" />\r\n    <Property name=\"RecordUser\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"RecordUser\" mappingColumnType=\"System.String\" sqlType=\"varchar(2" +
        "0)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" t" +
        "ype=\"System.String\" isInherited=\"false\" sqlDefaultValue=\"\'0\'\" isReadOnly=\"false\"" +
        " isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" " +
        "isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProperty" +
        "Desc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColum" +
        "nType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" /" +
        ">\r\n    <Property name=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOn" +
        "ly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKe" +
        "y=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInd" +
        "exPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappin" +
        "gColumnType=\"System.String\" sqlType=\"nvarchar(200)\" isPrimaryKey=\"false\" isNotNu" +
        "ll=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class EqmProjectRepairRecord : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _EqmProjectRepairRecordEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _MainDailyID;
        
        protected string _EquipCode;
        
        protected string _ShiftID;
        
        protected string _RepairDate;
        
        protected string _RepairStartDate;
        
        protected string _RepairEndDate;
        
        protected global::System.Decimal? _RepairSpendTime;
        
        protected string _RepairUser;
        
        protected string _RepairType;
        
        protected string _RepairPart;
        
        protected string _FaultDetail;
        
        protected string _RepairResult;
        
        protected global::System.DateTime? _RecordDate;
        
        protected string _RecordUser;
        
        protected string _DeleteFlag;
        
        protected string _Remark;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.EqmProjectRepairRecord left, global::Mesnac.Entity.EqmProjectRepairRecord right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.EqmProjectRepairRecord left, global::Mesnac.Entity.EqmProjectRepairRecord right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string MainDailyID {
            get {
                return this._MainDailyID;
            }
            set {
                this.OnPropertyChanged("MainDailyID", this._MainDailyID, value);
                this._MainDailyID = value;
            }
        }
        
        public string EquipCode {
            get {
                return this._EquipCode;
            }
            set {
                this.OnPropertyChanged("EquipCode", this._EquipCode, value);
                this._EquipCode = value;
            }
        }
        
        public string ShiftID {
            get {
                return this._ShiftID;
            }
            set {
                this.OnPropertyChanged("ShiftID", this._ShiftID, value);
                this._ShiftID = value;
            }
        }
        
        public string RepairDate {
            get {
                return this._RepairDate;
            }
            set {
                this.OnPropertyChanged("RepairDate", this._RepairDate, value);
                this._RepairDate = value;
            }
        }
        
        public string RepairStartDate {
            get {
                return this._RepairStartDate;
            }
            set {
                this.OnPropertyChanged("RepairStartDate", this._RepairStartDate, value);
                this._RepairStartDate = value;
            }
        }
        
        public string RepairEndDate {
            get {
                return this._RepairEndDate;
            }
            set {
                this.OnPropertyChanged("RepairEndDate", this._RepairEndDate, value);
                this._RepairEndDate = value;
            }
        }
        
        public global::System.Decimal? RepairSpendTime {
            get {
                return this._RepairSpendTime;
            }
            set {
                this.OnPropertyChanged("RepairSpendTime", this._RepairSpendTime, value);
                this._RepairSpendTime = value;
            }
        }
        
        public string RepairUser {
            get {
                return this._RepairUser;
            }
            set {
                this.OnPropertyChanged("RepairUser", this._RepairUser, value);
                this._RepairUser = value;
            }
        }
        
        public string RepairType {
            get {
                return this._RepairType;
            }
            set {
                this.OnPropertyChanged("RepairType", this._RepairType, value);
                this._RepairType = value;
            }
        }
        
        public string RepairPart {
            get {
                return this._RepairPart;
            }
            set {
                this.OnPropertyChanged("RepairPart", this._RepairPart, value);
                this._RepairPart = value;
            }
        }
        
        public string FaultDetail {
            get {
                return this._FaultDetail;
            }
            set {
                this.OnPropertyChanged("FaultDetail", this._FaultDetail, value);
                this._FaultDetail = value;
            }
        }
        
        public string RepairResult {
            get {
                return this._RepairResult;
            }
            set {
                this.OnPropertyChanged("RepairResult", this._RepairResult, value);
                this._RepairResult = value;
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
        
        public string RecordUser {
            get {
                return this._RecordUser;
            }
            set {
                this.OnPropertyChanged("RecordUser", this._RecordUser, value);
                this._RecordUser = value;
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
            if ((EqmProjectRepairRecord._EqmProjectRepairRecordEntityConfiguration == null)) {
                EqmProjectRepairRecord._EqmProjectRepairRecordEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.EqmProjectRepairRecord");
            }
            return EqmProjectRepairRecord._EqmProjectRepairRecordEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._MainDailyID,
                    this._EquipCode,
                    this._ShiftID,
                    this._RepairDate,
                    this._RepairStartDate,
                    this._RepairEndDate,
                    this._RepairSpendTime,
                    this._RepairUser,
                    this._RepairType,
                    this._RepairPart,
                    this._FaultDetail,
                    this._RepairResult,
                    this._RecordDate,
                    this._RecordUser,
                    this._DeleteFlag,
                    this._Remark};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._MainDailyID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._EquipCode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._ShiftID = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._RepairDate = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._RepairStartDate = reader.GetString(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._RepairEndDate = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._RepairSpendTime = reader.GetDecimal(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._RepairUser = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._RepairType = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._RepairPart = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._FaultDetail = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._RepairResult = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._RecordDate = this.GetDateTime(reader, 13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._RecordUser = reader.GetString(14);
            }
            if ((false == reader.IsDBNull(15))) {
                this._DeleteFlag = reader.GetString(15);
            }
            if ((false == reader.IsDBNull(16))) {
                this._Remark = reader.GetString(16);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._MainDailyID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._EquipCode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._ShiftID = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._RepairDate = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._RepairStartDate = ((string)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._RepairEndDate = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._RepairSpendTime = ((System.Nullable<decimal>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._RepairUser = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._RepairType = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._RepairPart = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._FaultDetail = ((string)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._RepairResult = ((string)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._RecordDate = this.GetDateTime(row, 13);
            }
            if ((false == row.IsNull(14))) {
                this._RecordUser = ((string)(row[14]));
            }
            if ((false == row.IsNull(15))) {
                this._DeleteFlag = ((string)(row[15]));
            }
            if ((false == row.IsNull(16))) {
                this._Remark = ((string)(row[16]));
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
            if ((false == typeof(global::Mesnac.Entity.EqmProjectRepairRecord).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.EqmProjectRepairRecord)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.EqmProjectRepairRecord)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _MainDailyID = new NBear.Common.PropertyItem("MainDailyID", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _EquipCode = new NBear.Common.PropertyItem("EquipCode", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _ShiftID = new NBear.Common.PropertyItem("ShiftID", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairDate = new NBear.Common.PropertyItem("RepairDate", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairStartDate = new NBear.Common.PropertyItem("RepairStartDate", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairEndDate = new NBear.Common.PropertyItem("RepairEndDate", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairSpendTime = new NBear.Common.PropertyItem("RepairSpendTime", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairUser = new NBear.Common.PropertyItem("RepairUser", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairType = new NBear.Common.PropertyItem("RepairType", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairPart = new NBear.Common.PropertyItem("RepairPart", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _FaultDetail = new NBear.Common.PropertyItem("FaultDetail", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RepairResult = new NBear.Common.PropertyItem("RepairResult", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _RecordUser = new NBear.Common.PropertyItem("RecordUser", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.EqmProjectRepairRecord");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.EqmProjectRepairRecord");
            
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
            
            public NBear.Common.PropertyItem MainDailyID {
                get {
                    if ((aliasName == null)) {
                        return _MainDailyID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MainDailyID", _MainDailyID.EntityConfiguration, _MainDailyID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem EquipCode {
                get {
                    if ((aliasName == null)) {
                        return _EquipCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("EquipCode", _EquipCode.EntityConfiguration, _EquipCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ShiftID {
                get {
                    if ((aliasName == null)) {
                        return _ShiftID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiftID", _ShiftID.EntityConfiguration, _ShiftID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairDate {
                get {
                    if ((aliasName == null)) {
                        return _RepairDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairDate", _RepairDate.EntityConfiguration, _RepairDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairStartDate {
                get {
                    if ((aliasName == null)) {
                        return _RepairStartDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairStartDate", _RepairStartDate.EntityConfiguration, _RepairStartDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairEndDate {
                get {
                    if ((aliasName == null)) {
                        return _RepairEndDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairEndDate", _RepairEndDate.EntityConfiguration, _RepairEndDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairSpendTime {
                get {
                    if ((aliasName == null)) {
                        return _RepairSpendTime;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairSpendTime", _RepairSpendTime.EntityConfiguration, _RepairSpendTime.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairUser {
                get {
                    if ((aliasName == null)) {
                        return _RepairUser;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairUser", _RepairUser.EntityConfiguration, _RepairUser.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairType {
                get {
                    if ((aliasName == null)) {
                        return _RepairType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairType", _RepairType.EntityConfiguration, _RepairType.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairPart {
                get {
                    if ((aliasName == null)) {
                        return _RepairPart;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairPart", _RepairPart.EntityConfiguration, _RepairPart.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem FaultDetail {
                get {
                    if ((aliasName == null)) {
                        return _FaultDetail;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FaultDetail", _FaultDetail.EntityConfiguration, _FaultDetail.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RepairResult {
                get {
                    if ((aliasName == null)) {
                        return _RepairResult;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RepairResult", _RepairResult.EntityConfiguration, _RepairResult.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem RecordUser {
                get {
                    if ((aliasName == null)) {
                        return _RecordUser;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RecordUser", _RecordUser.EntityConfiguration, _RecordUser.PropertyConfiguration, aliasName);
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
