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
    public partial class SysCodeArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.SysCode> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.SysCode\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBatchUpd" +
        "ate=\"false\" isRelation=\"false\" mappingName=\"SysCode\" batchSize=\"10\">\r\n  <Propert" +
        "ies>\r\n    <Property name=\"TypeID\" type=\"System.String\" isInherited=\"false\" isRea" +
        "dOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrien" +
        "dKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" is" +
        "IndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"TypeID\" map" +
        "pingColumnType=\"System.String\" sqlType=\"varchar(20)\" isPrimaryKey=\"true\" isNotNu" +
        "ll=\"true\" />\r\n    <Property name=\"ItemCode\" type=\"System.String\" isInherited=\"fa" +
        "lse\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"fals" +
        "e\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=" +
        "\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"I" +
        "temCode\" mappingColumnType=\"System.String\" sqlType=\"varchar(10)\" isPrimaryKey=\"t" +
        "rue\" isNotNull=\"true\" />\r\n    <Property name=\"ItemName\" type=\"System.String\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"ItemName\" mappingColumnType=\"System.String\" sqlType=\"varchar(50)\" isP" +
        "rimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" type=\"System" +
        ".String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContain" +
        "ed=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=" +
        "\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnor" +
        "e=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sqlType=\"varcha" +
        "r(100)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConf" +
        "iguration>")]
    public partial class SysCode : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _SysCodeEntityConfiguration;
        
        protected string _TypeID;
        
        protected string _ItemCode;
        
        protected string _ItemName;
        
        protected string _Remark;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.SysCode left, global::Mesnac.Entity.SysCode right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.SysCode left, global::Mesnac.Entity.SysCode right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string TypeID {
            get {
                return this._TypeID;
            }
            set {
                this.OnPropertyChanged("TypeID", this._TypeID, value);
                this._TypeID = value;
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
        
        public string ItemName {
            get {
                return this._ItemName;
            }
            set {
                this.OnPropertyChanged("ItemName", this._ItemName, value);
                this._ItemName = value;
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
            if ((SysCode._SysCodeEntityConfiguration == null)) {
                SysCode._SysCodeEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.SysCode");
            }
            return SysCode._SysCodeEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._TypeID,
                    this._ItemCode,
                    this._ItemName,
                    this._Remark};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._TypeID = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._ItemCode = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._ItemName = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._Remark = reader.GetString(3);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._TypeID = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._ItemCode = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._ItemName = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._Remark = ((string)(row[3]));
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
            if ((false == typeof(global::Mesnac.Entity.SysCode).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return (((this.isAttached && ((global::Mesnac.Entity.SysCode)(obj)).isAttached) 
                        && (this.TypeID == ((global::Mesnac.Entity.SysCode)(obj)).TypeID)) 
                        && (this.ItemCode == ((global::Mesnac.Entity.SysCode)(obj)).ItemCode));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _TypeID = new NBear.Common.PropertyItem("TypeID", "Mesnac.Entity.SysCode");
            
            protected NBear.Common.PropertyItem _ItemCode = new NBear.Common.PropertyItem("ItemCode", "Mesnac.Entity.SysCode");
            
            protected NBear.Common.PropertyItem _ItemName = new NBear.Common.PropertyItem("ItemName", "Mesnac.Entity.SysCode");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.SysCode");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
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
