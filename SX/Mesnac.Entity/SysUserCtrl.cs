//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.1022
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
    public partial class SysUserCtrlArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.SysUserCtrl> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.SysUserCtrl\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatc" +
        "hUpdate=\"false\" isRelation=\"false\" mappingName=\"SysUserCtrl\" batchSize=\"10\">\r\n  " +
        "<Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int32\" isInherited=\"false\"" +
        " isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" is" +
        "FriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fals" +
        "e\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ObjID\"" +
        " mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull=\"t" +
        "rue\" />\r\n    <Property name=\"TypeID\" type=\"System.String\" isInherited=\"false\" is" +
        "ReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFr" +
        "iendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\"" +
        " isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"TypeID\" " +
        "mappingColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\" is" +
        "NotNull=\"false\" />\r\n    <Property name=\"ItemName\" type=\"System.String\" isInherit" +
        "ed=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery" +
        "=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPro" +
        "perty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingN" +
        "ame=\"ItemName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(50)\" isPrimar" +
        "yKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ItemCode\" type=\"System.St" +
        "ring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=" +
        "\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fa" +
        "lse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"" +
        "false\" mappingName=\"ItemCode\" mappingColumnType=\"System.String\" sqlType=\"nvarcha" +
        "r(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" ty" +
        "pe=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\"" +
        " isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRe" +
        "lationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializ" +
        "ationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"nvarchar(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n<" +
        "/EntityConfiguration>")]
    public partial class SysUserCtrl : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _SysUserCtrlEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _TypeID;
        
        protected string _ItemName;
        
        protected string _ItemCode;
        
        protected string _Remark;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.SysUserCtrl left, global::Mesnac.Entity.SysUserCtrl right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.SysUserCtrl left, global::Mesnac.Entity.SysUserCtrl right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
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
        
        public string ItemName {
            get {
                return this._ItemName;
            }
            set {
                this.OnPropertyChanged("ItemName", this._ItemName, value);
                this._ItemName = value;
            }
        }
        
        public string ItemCode {
            get {
                return this._ItemCode;
            }
            set {
                this.OnPropertyChanged("ItemCode", this._ItemCode, value);
                this._ItemCode = value;
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
            if ((SysUserCtrl._SysUserCtrlEntityConfiguration == null)) {
                SysUserCtrl._SysUserCtrlEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.SysUserCtrl");
            }
            return SysUserCtrl._SysUserCtrlEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._TypeID,
                    this._ItemName,
                    this._ItemCode,
                    this._Remark};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._TypeID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._ItemName = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._ItemCode = reader.GetString(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._Remark = reader.GetString(4);
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
                this._ItemName = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._ItemCode = ((string)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._Remark = ((string)(row[4]));
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
            if ((false == typeof(global::Mesnac.Entity.SysUserCtrl).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.SysUserCtrl)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.SysUserCtrl)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.SysUserCtrl");
            
            protected NBear.Common.PropertyItem _TypeID = new NBear.Common.PropertyItem("TypeID", "Mesnac.Entity.SysUserCtrl");
            
            protected NBear.Common.PropertyItem _ItemName = new NBear.Common.PropertyItem("ItemName", "Mesnac.Entity.SysUserCtrl");
            
            protected NBear.Common.PropertyItem _ItemCode = new NBear.Common.PropertyItem("ItemCode", "Mesnac.Entity.SysUserCtrl");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.SysUserCtrl");
            
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
            
            public NBear.Common.PropertyItem ItemName {
                get {
                    if ((aliasName == null)) {
                        return _ItemName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ItemName", _ItemName.EntityConfiguration, _ItemName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ItemCode {
                get {
                    if ((aliasName == null)) {
                        return _ItemCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ItemCode", _ItemCode.EntityConfiguration, _ItemCode.PropertyConfiguration, aliasName);
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