//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.1008
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
    public partial class EqmMixerFaultArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.EqmMixerFault> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.EqmMixerFault\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBa" +
        "tchUpdate=\"false\" isRelation=\"false\" mappingName=\"EqmMixerFault\" batchSize=\"10\">" +
        "\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited=\"fa" +
        "lse\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false" +
        "\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"" +
        "false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Ob" +
        "jID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNul" +
        "l=\"true\" />\r\n    <Property name=\"FaultCode\" type=\"System.String\" isInherited=\"fa" +
        "lse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fals" +
        "e\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=" +
        "\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"F" +
        "aultCode\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=" +
        "\"false\" isNotNull=\"false\" />\r\n    <Property name=\"FaultName\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"FaultName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(4" +
        "0)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"FaultPosition" +
        "\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"fa" +
        "lse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" " +
        "isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeri" +
        "alizationIgnore=\"false\" mappingName=\"FaultPosition\" mappingColumnType=\"System.St" +
        "ring\" sqlType=\"nvarchar(40)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Pro" +
        "perty name=\"AlarmState\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fal" +
        "se\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"fals" +
        "e\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPrope" +
        "rtyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"AlarmState\" mappingCo" +
        "lumnType=\"System.String\" sqlType=\"nvarchar(40)\" isPrimaryKey=\"false\" isNotNull=\"" +
        "false\" />\r\n    <Property name=\"FaultDate\" type=\"System.Nullable`1[System.DateTim" +
        "e]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"f" +
        "alse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fals" +
        "e\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fa" +
        "lse\" mappingName=\"FaultDate\" mappingColumnType=\"System.Nullable`1[System.DateTim" +
        "e]\" sqlType=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property " +
        "name=\"FaultType\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isC" +
        "ompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLa" +
        "zyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc" +
        "=\"false\" isSerializationIgnore=\"false\" mappingName=\"FaultType\" mappingColumnType" +
        "=\"System.String\" sqlType=\"nvarchar(40)\" isPrimaryKey=\"false\" isNotNull=\"false\" /" +
        ">\r\n    <Property name=\"EquipCode\" type=\"System.String\" isInherited=\"false\" isRea" +
        "dOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrien" +
        "dKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" is" +
        "IndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"EquipCode\" " +
        "mappingColumnType=\"System.String\" sqlType=\"char(5)\" isPrimaryKey=\"false\" isNotNu" +
        "ll=\"false\" />\r\n    <Property name=\"WorkShopCode\" type=\"System.Nullable`1[System." +
        "Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContaine" +
        "d=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"" +
        "false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore" +
        "=\"false\" mappingName=\"WorkShopCode\" mappingColumnType=\"System.Nullable`1[System." +
        "Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property n" +
        "ame=\"Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompo" +
        "undUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLo" +
        "ad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"fa" +
        "lse\" isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=\"Syste" +
        "m.String\" sqlType=\"nvarchar(200)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n   " +
        " <Property name=\"DeleteFlag\" type=\"System.String\" isInherited=\"false\" sqlDefault" +
        "Value=\"\'0\'\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"DeleteFlag\" mappingColumnType=\"System.String\" sqlType=\"char(1)\" isPrimaryK" +
        "ey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class EqmMixerFault : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _EqmMixerFaultEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _FaultCode;
        
        protected string _FaultName;
        
        protected string _FaultPosition;
        
        protected string _AlarmState;
        
        protected global::System.DateTime? _FaultDate;
        
        protected string _FaultType;
        
        protected string _EquipCode;
        
        protected global::System.Int32? _WorkShopCode;
        
        protected string _Remark;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.EqmMixerFault left, global::Mesnac.Entity.EqmMixerFault right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.EqmMixerFault left, global::Mesnac.Entity.EqmMixerFault right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string FaultCode {
            get {
                return this._FaultCode;
            }
            set {
                this.OnPropertyChanged("FaultCode", this._FaultCode, value);
                this._FaultCode = value;
            }
        }
        
        public string FaultName {
            get {
                return this._FaultName;
            }
            set {
                this.OnPropertyChanged("FaultName", this._FaultName, value);
                this._FaultName = value;
            }
        }
        
        public string FaultPosition {
            get {
                return this._FaultPosition;
            }
            set {
                this.OnPropertyChanged("FaultPosition", this._FaultPosition, value);
                this._FaultPosition = value;
            }
        }
        
        public string AlarmState {
            get {
                return this._AlarmState;
            }
            set {
                this.OnPropertyChanged("AlarmState", this._AlarmState, value);
                this._AlarmState = value;
            }
        }
        
        public global::System.DateTime? FaultDate {
            get {
                return this._FaultDate;
            }
            set {
                this.OnPropertyChanged("FaultDate", this._FaultDate, value);
                this._FaultDate = value;
            }
        }
        
        public string FaultType {
            get {
                return this._FaultType;
            }
            set {
                this.OnPropertyChanged("FaultType", this._FaultType, value);
                this._FaultType = value;
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
        
        public global::System.Int32? WorkShopCode {
            get {
                return this._WorkShopCode;
            }
            set {
                this.OnPropertyChanged("WorkShopCode", this._WorkShopCode, value);
                this._WorkShopCode = value;
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
            if ((EqmMixerFault._EqmMixerFaultEntityConfiguration == null)) {
                EqmMixerFault._EqmMixerFaultEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.EqmMixerFault");
            }
            return EqmMixerFault._EqmMixerFaultEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._FaultCode,
                    this._FaultName,
                    this._FaultPosition,
                    this._AlarmState,
                    this._FaultDate,
                    this._FaultType,
                    this._EquipCode,
                    this._WorkShopCode,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._FaultCode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._FaultName = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._FaultPosition = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._AlarmState = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._FaultDate = this.GetDateTime(reader, 5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._FaultType = reader.GetString(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._EquipCode = reader.GetString(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._WorkShopCode = reader.GetInt32(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._Remark = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._DeleteFlag = reader.GetString(10);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._FaultCode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._FaultName = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._FaultPosition = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._AlarmState = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._FaultDate = this.GetDateTime(row, 5);
            }
            if ((false == row.IsNull(6))) {
                this._FaultType = ((string)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._EquipCode = ((string)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._WorkShopCode = ((System.Nullable<int>)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._Remark = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._DeleteFlag = ((string)(row[10]));
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
            if ((false == typeof(global::Mesnac.Entity.EqmMixerFault).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.EqmMixerFault)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.EqmMixerFault)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _FaultCode = new NBear.Common.PropertyItem("FaultCode", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _FaultName = new NBear.Common.PropertyItem("FaultName", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _FaultPosition = new NBear.Common.PropertyItem("FaultPosition", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _AlarmState = new NBear.Common.PropertyItem("AlarmState", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _FaultDate = new NBear.Common.PropertyItem("FaultDate", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _FaultType = new NBear.Common.PropertyItem("FaultType", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _EquipCode = new NBear.Common.PropertyItem("EquipCode", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _WorkShopCode = new NBear.Common.PropertyItem("WorkShopCode", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.EqmMixerFault");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.EqmMixerFault");
            
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
            
            public NBear.Common.PropertyItem FaultCode {
                get {
                    if ((aliasName == null)) {
                        return _FaultCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FaultCode", _FaultCode.EntityConfiguration, _FaultCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem FaultName {
                get {
                    if ((aliasName == null)) {
                        return _FaultName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FaultName", _FaultName.EntityConfiguration, _FaultName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem FaultPosition {
                get {
                    if ((aliasName == null)) {
                        return _FaultPosition;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FaultPosition", _FaultPosition.EntityConfiguration, _FaultPosition.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem AlarmState {
                get {
                    if ((aliasName == null)) {
                        return _AlarmState;
                    }
                    else {
                        return new NBear.Common.PropertyItem("AlarmState", _AlarmState.EntityConfiguration, _AlarmState.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem FaultDate {
                get {
                    if ((aliasName == null)) {
                        return _FaultDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FaultDate", _FaultDate.EntityConfiguration, _FaultDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem FaultType {
                get {
                    if ((aliasName == null)) {
                        return _FaultType;
                    }
                    else {
                        return new NBear.Common.PropertyItem("FaultType", _FaultType.EntityConfiguration, _FaultType.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem WorkShopCode {
                get {
                    if ((aliasName == null)) {
                        return _WorkShopCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("WorkShopCode", _WorkShopCode.EntityConfiguration, _WorkShopCode.PropertyConfiguration, aliasName);
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