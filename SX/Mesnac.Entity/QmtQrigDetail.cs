//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.42000
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
    public partial class QmtQrigDetailArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.QmtQrigDetail> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.QmtQrigDetail\" isReadOnly=\"true\" isAutoPreLoad=\"false\" isBat" +
        "chUpdate=\"false\" isRelation=\"false\" mappingName=\"QmtQrigDetail\" batchSize=\"10\">\r" +
        "\n  <Properties>\r\n    <Property name=\"IncId\" type=\"System.Int64\" isInherited=\"fal" +
        "se\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Inc" +
        "Id\" mappingColumnType=\"System.Int64\" sqlType=\"bigint\" isPrimaryKey=\"false\" isNot" +
        "Null=\"false\" />\r\n    <Property name=\"SeqNo\" type=\"System.Int64\" isInherited=\"fal" +
        "se\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Seq" +
        "No\" mappingColumnType=\"System.Int64\" sqlType=\"bigint\" isPrimaryKey=\"false\" isNot" +
        "Null=\"false\" />\r\n    <Property name=\"Itemcd\" type=\"System.String\" isInherited=\"f" +
        "alse\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fals" +
        "e\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=" +
        "\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"I" +
        "temcd\" mappingColumnType=\"System.String\" sqlType=\"char(3)\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n    <Property name=\"ItemCheck\" type=\"System.Nullable`1[Sys" +
        "tem.Decimal]\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isCon" +
        "tained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelation" +
        "Key=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationI" +
        "gnore=\"false\" mappingName=\"ItemCheck\" mappingColumnType=\"System.Nullable`1[Syste" +
        "m.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pr" +
        "operty name=\"StandID\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\"" +
        " isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
        "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
        "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"StandI" +
        "D\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKe" +
        "y=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"StandCode\" type=\"System.Null" +
        "able`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"fals" +
        "e\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" is" +
        "RelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerial" +
        "izationIgnore=\"false\" mappingName=\"StandCode\" mappingColumnType=\"System.Nullable" +
        "`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <" +
        "Property name=\"UnitName\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"tr" +
        "ue\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fals" +
        "e\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPrope" +
        "rtyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"UnitName\" mappingColu" +
        "mnType=\"System.String\" sqlType=\"varchar(10)\" isPrimaryKey=\"false\" isNotNull=\"fal" +
        "se\" />\r\n    <Property name=\"JudgeValue\" type=\"System.Nullable`1[System.Int32]\" i" +
        "sInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" " +
        "isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isI" +
        "ndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" m" +
        "appingName=\"JudgeValue\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlT" +
        "ype=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"CheckEq" +
        "uipCode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUn" +
        "it=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"f" +
        "alse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" " +
        "isSerializationIgnore=\"false\" mappingName=\"CheckEquipCode\" mappingColumnType=\"Sy" +
        "stem.String\" sqlType=\"varchar(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n   " +
        " <Property name=\"DeleteFlag\" type=\"System.String\" isInherited=\"false\" isReadOnly" +
        "=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"" +
        "false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexP" +
        "ropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappi" +
        "ngColumnType=\"System.String\" sqlType=\"varchar(1)\" isPrimaryKey=\"false\" isNotNull" +
        "=\"false\" />\r\n    <Property name=\"PlanDate\" type=\"System.String\" isInherited=\"fal" +
        "se\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Pla" +
        "nDate\" mappingColumnType=\"System.String\" sqlType=\"varchar(1)\" isPrimaryKey=\"fals" +
        "e\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class QmtQrigDetail : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _QmtQrigDetailEntityConfiguration;
        
        protected long _IncId;
        
        protected long _SeqNo;
        
        protected string _Itemcd;
        
        protected global::System.Decimal? _ItemCheck;
        
        protected global::System.Int32? _StandID;
        
        protected global::System.Int32? _StandCode;
        
        protected string _UnitName;
        
        protected global::System.Int32? _JudgeValue;
        
        protected string _CheckEquipCode;
        
        protected string _DeleteFlag;
        
        protected string _PlanDate;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.QmtQrigDetail left, global::Mesnac.Entity.QmtQrigDetail right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.QmtQrigDetail left, global::Mesnac.Entity.QmtQrigDetail right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public long IncId {
            get {
                return this._IncId;
            }
            set {
                this.OnPropertyChanged("IncId", this._IncId, value);
                this._IncId = value;
            }
        }
        
        public long SeqNo {
            get {
                return this._SeqNo;
            }
            set {
                this.OnPropertyChanged("SeqNo", this._SeqNo, value);
                this._SeqNo = value;
            }
        }
        
        public string Itemcd {
            get {
                return this._Itemcd;
            }
            set {
                this.OnPropertyChanged("Itemcd", this._Itemcd, value);
                this._Itemcd = value;
            }
        }
        
        public global::System.Decimal? ItemCheck {
            get {
                return this._ItemCheck;
            }
            set {
                this.OnPropertyChanged("ItemCheck", this._ItemCheck, value);
                this._ItemCheck = value;
            }
        }
        
        public global::System.Int32? StandID {
            get {
                return this._StandID;
            }
            set {
                this.OnPropertyChanged("StandID", this._StandID, value);
                this._StandID = value;
            }
        }
        
        public global::System.Int32? StandCode {
            get {
                return this._StandCode;
            }
            set {
                this.OnPropertyChanged("StandCode", this._StandCode, value);
                this._StandCode = value;
            }
        }
        
        public string UnitName {
            get {
                return this._UnitName;
            }
            set {
                this.OnPropertyChanged("UnitName", this._UnitName, value);
                this._UnitName = value;
            }
        }
        
        public global::System.Int32? JudgeValue {
            get {
                return this._JudgeValue;
            }
            set {
                this.OnPropertyChanged("JudgeValue", this._JudgeValue, value);
                this._JudgeValue = value;
            }
        }
        
        public string CheckEquipCode {
            get {
                return this._CheckEquipCode;
            }
            set {
                this.OnPropertyChanged("CheckEquipCode", this._CheckEquipCode, value);
                this._CheckEquipCode = value;
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
        
        public string PlanDate {
            get {
                return this._PlanDate;
            }
            set {
                this.OnPropertyChanged("PlanDate", this._PlanDate, value);
                this._PlanDate = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((QmtQrigDetail._QmtQrigDetailEntityConfiguration == null)) {
                QmtQrigDetail._QmtQrigDetailEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.QmtQrigDetail");
            }
            return QmtQrigDetail._QmtQrigDetailEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._IncId,
                    this._SeqNo,
                    this._Itemcd,
                    this._ItemCheck,
                    this._StandID,
                    this._StandCode,
                    this._UnitName,
                    this._JudgeValue,
                    this._CheckEquipCode,
                    this._DeleteFlag,
                    this._PlanDate};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._IncId = reader.GetInt64(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._SeqNo = reader.GetInt64(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Itemcd = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._ItemCheck = reader.GetDecimal(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._StandID = reader.GetInt32(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._StandCode = reader.GetInt32(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._UnitName = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._JudgeValue = reader.GetInt32(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._CheckEquipCode = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._DeleteFlag = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._PlanDate = reader.GetString(10);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._IncId = ((long)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._SeqNo = ((long)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Itemcd = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._ItemCheck = ((System.Nullable<decimal>)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._StandID = ((System.Nullable<int>)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._StandCode = ((System.Nullable<int>)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._UnitName = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._JudgeValue = ((System.Nullable<int>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._CheckEquipCode = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._DeleteFlag = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._PlanDate = ((string)(row[10]));
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
            if ((false == typeof(global::Mesnac.Entity.QmtQrigDetail).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.QmtQrigDetail)(obj)).isAttached) 
                        && base.Equals(obj));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _IncId = new NBear.Common.PropertyItem("IncId", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _SeqNo = new NBear.Common.PropertyItem("SeqNo", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _Itemcd = new NBear.Common.PropertyItem("Itemcd", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _ItemCheck = new NBear.Common.PropertyItem("ItemCheck", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _StandID = new NBear.Common.PropertyItem("StandID", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _StandCode = new NBear.Common.PropertyItem("StandCode", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _UnitName = new NBear.Common.PropertyItem("UnitName", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _JudgeValue = new NBear.Common.PropertyItem("JudgeValue", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _CheckEquipCode = new NBear.Common.PropertyItem("CheckEquipCode", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.QmtQrigDetail");
            
            protected NBear.Common.PropertyItem _PlanDate = new NBear.Common.PropertyItem("PlanDate", "Mesnac.Entity.QmtQrigDetail");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem IncId {
                get {
                    if ((aliasName == null)) {
                        return _IncId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("IncId", _IncId.EntityConfiguration, _IncId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem SeqNo {
                get {
                    if ((aliasName == null)) {
                        return _SeqNo;
                    }
                    else {
                        return new NBear.Common.PropertyItem("SeqNo", _SeqNo.EntityConfiguration, _SeqNo.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Itemcd {
                get {
                    if ((aliasName == null)) {
                        return _Itemcd;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Itemcd", _Itemcd.EntityConfiguration, _Itemcd.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ItemCheck {
                get {
                    if ((aliasName == null)) {
                        return _ItemCheck;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ItemCheck", _ItemCheck.EntityConfiguration, _ItemCheck.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StandID {
                get {
                    if ((aliasName == null)) {
                        return _StandID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StandID", _StandID.EntityConfiguration, _StandID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StandCode {
                get {
                    if ((aliasName == null)) {
                        return _StandCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StandCode", _StandCode.EntityConfiguration, _StandCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem UnitName {
                get {
                    if ((aliasName == null)) {
                        return _UnitName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("UnitName", _UnitName.EntityConfiguration, _UnitName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem JudgeValue {
                get {
                    if ((aliasName == null)) {
                        return _JudgeValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("JudgeValue", _JudgeValue.EntityConfiguration, _JudgeValue.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem CheckEquipCode {
                get {
                    if ((aliasName == null)) {
                        return _CheckEquipCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("CheckEquipCode", _CheckEquipCode.EntityConfiguration, _CheckEquipCode.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem PlanDate {
                get {
                    if ((aliasName == null)) {
                        return _PlanDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PlanDate", _PlanDate.EntityConfiguration, _PlanDate.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}