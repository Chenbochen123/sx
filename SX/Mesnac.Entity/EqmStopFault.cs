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
    public partial class EqmStopFaultArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.EqmStopFault> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.EqmStopFault\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBat" +
        "chUpdate=\"false\" isRelation=\"false\" mappingName=\"EqmStopFault\" batchSize=\"10\">\r\n" +
        "  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited=\"fals" +
        "e\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\"" +
        " isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"f" +
        "alse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"Obj" +
        "ID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull" +
        "=\"true\" />\r\n    <Property name=\"TypeID\" type=\"System.String\" isInherited=\"false\"" +
        " isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" i" +
        "sFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fal" +
        "se\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"TypeI" +
        "D\" mappingColumnType=\"System.String\" sqlType=\"char(3)\" isPrimaryKey=\"false\" isNo" +
        "tNull=\"false\" />\r\n    <Property name=\"FaultCode\" type=\"System.String\" isInherite" +
        "d=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=" +
        "\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProp" +
        "erty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNa" +
        "me=\"FaultCode\" mappingColumnType=\"System.String\" sqlType=\"char(6)\" isPrimaryKey=" +
        "\"false\" isNotNull=\"false\" />\r\n    <Property name=\"FaultName\" type=\"System.String" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"FaultName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(6" +
        "0)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" t" +
        "ype=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false" +
        "\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isR" +
        "elationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeriali" +
        "zationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.String\" " +
        "sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</" +
        "EntityConfiguration>")]
    public partial class EqmStopFault : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _EqmStopFaultEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _TypeID;
        
        protected string _FaultCode;
        
        protected string _FaultName;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.EqmStopFault left, global::Mesnac.Entity.EqmStopFault right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.EqmStopFault left, global::Mesnac.Entity.EqmStopFault right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string TypeID {
            get {
                return this._TypeID;
            }
            set {
                this.OnPropertyChanged("TypeID", this._TypeID, value);
                this._TypeID = value;
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
            if ((EqmStopFault._EqmStopFaultEntityConfiguration == null)) {
                EqmStopFault._EqmStopFaultEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.EqmStopFault");
            }
            return EqmStopFault._EqmStopFaultEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._TypeID,
                    this._FaultCode,
                    this._FaultName,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._TypeID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._FaultCode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._FaultName = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._DeleteFlag = reader.GetString(4);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._TypeID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._FaultCode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._FaultName = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._DeleteFlag = ((string)(row[4]));
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
            if ((false == typeof(global::Mesnac.Entity.EqmStopFault).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.EqmStopFault)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.EqmStopFault)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.EqmStopFault");
            
            protected NBear.Common.PropertyItem _TypeID = new NBear.Common.PropertyItem("TypeID", "Mesnac.Entity.EqmStopFault");
            
            protected NBear.Common.PropertyItem _FaultCode = new NBear.Common.PropertyItem("FaultCode", "Mesnac.Entity.EqmStopFault");
            
            protected NBear.Common.PropertyItem _FaultName = new NBear.Common.PropertyItem("FaultName", "Mesnac.Entity.EqmStopFault");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.EqmStopFault");
            
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
            
            public NBear.Common.PropertyItem TypeID {
                get {
                    if ((aliasName == null)) {
                        return _TypeID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("TypeID", _TypeID.EntityConfiguration, _TypeID.PropertyConfiguration, aliasName);
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