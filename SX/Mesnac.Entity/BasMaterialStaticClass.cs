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
    public partial class BasMaterialStaticClassArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.BasMaterialStaticClass> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.BasMaterialStaticClass\" isReadOnly=\"false\" isAutoPreLoad=\"fa" +
        "lse\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"BasMaterialStaticClas" +
        "s\" batchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"ObjID\" type=\"System.Int3" +
        "2\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"ObjID\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimary" +
        "Key=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StaticClassName\" type=\"Syste" +
        "m.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"StaticClassName\" mappingColumnType=\"System.String\" sqlTy" +
        "pe=\"varchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"" +
        "Remark\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUn" +
        "it=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"f" +
        "alse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" " +
        "isSerializationIgnore=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.Str" +
        "ing\" sqlType=\"varchar(200)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Prop" +
        "erty name=\"DeleteFlag\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"fals" +
        "e\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false" +
        "\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexProper" +
        "tyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingCol" +
        "umnType=\"System.String\" sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\"" +
        " />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class BasMaterialStaticClass : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _BasMaterialStaticClassEntityConfiguration;
        
        protected int _ObjID;
        
        protected string _StaticClassName;
        
        protected string _Remark;
        
        protected string _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.BasMaterialStaticClass left, global::Mesnac.Entity.BasMaterialStaticClass right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.BasMaterialStaticClass left, global::Mesnac.Entity.BasMaterialStaticClass right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string StaticClassName {
            get {
                return this._StaticClassName;
            }
            set {
                this.OnPropertyChanged("StaticClassName", this._StaticClassName, value);
                this._StaticClassName = value;
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
            if ((BasMaterialStaticClass._BasMaterialStaticClassEntityConfiguration == null)) {
                BasMaterialStaticClass._BasMaterialStaticClassEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.BasMaterialStaticClass");
            }
            return BasMaterialStaticClass._BasMaterialStaticClassEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._StaticClassName,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StaticClassName = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Remark = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._DeleteFlag = reader.GetString(3);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StaticClassName = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Remark = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._DeleteFlag = ((string)(row[3]));
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
            if ((false == typeof(global::Mesnac.Entity.BasMaterialStaticClass).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.BasMaterialStaticClass)(obj)).isAttached) 
                        && (this.ObjID == ((global::Mesnac.Entity.BasMaterialStaticClass)(obj)).ObjID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.BasMaterialStaticClass");
            
            protected NBear.Common.PropertyItem _StaticClassName = new NBear.Common.PropertyItem("StaticClassName", "Mesnac.Entity.BasMaterialStaticClass");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.BasMaterialStaticClass");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.BasMaterialStaticClass");
            
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
            
            public NBear.Common.PropertyItem StaticClassName {
                get {
                    if ((aliasName == null)) {
                        return _StaticClassName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StaticClassName", _StaticClassName.EntityConfiguration, _StaticClassName.PropertyConfiguration, aliasName);
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
