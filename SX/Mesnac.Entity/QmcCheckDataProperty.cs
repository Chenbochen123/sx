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
    public partial class QmcCheckDataPropertyArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.QmcCheckDataProperty> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.QmcCheckDataProperty\" isReadOnly=\"false\" isAutoPreLoad=\"fals" +
        "e\" isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"QmcCheckDataProperty\" b" +
        "atchSize=\"10\">\r\n  <Properties>\r\n    <Property name=\"PropertyId\" type=\"System.Int" +
        "32\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"PropertyId\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPr" +
        "imaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"CheckId\" type=\"System.N" +
        "ullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"" +
        "false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false" +
        "\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSe" +
        "rializationIgnore=\"false\" mappingName=\"CheckId\" mappingColumnType=\"System.Nullab" +
        "le`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n   " +
        " <Property name=\"ItemPropertyId\" type=\"System.Nullable`1[System.Int32]\" isInheri" +
        "ted=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"ItemPropertyId\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlTyp" +
        "e=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"PropertyV" +
        "alue\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit" +
        "=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fal" +
        "se\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" is" +
        "SerializationIgnore=\"false\" mappingName=\"PropertyValue\" mappingColumnType=\"Syste" +
        "m.String\" sqlType=\"nvarchar(200)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  <" +
        "/Properties>\r\n</EntityConfiguration>")]
    public partial class QmcCheckDataProperty : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _QmcCheckDataPropertyEntityConfiguration;
        
        protected int _PropertyId;
        
        protected global::System.Int32? _CheckId;
        
        protected global::System.Int32? _ItemPropertyId;
        
        protected string _PropertyValue;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.QmcCheckDataProperty left, global::Mesnac.Entity.QmcCheckDataProperty right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.QmcCheckDataProperty left, global::Mesnac.Entity.QmcCheckDataProperty right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int PropertyId {
            get {
                return this._PropertyId;
            }
            set {
                this.OnPropertyChanged("PropertyId", this._PropertyId, value);
                this._PropertyId = value;
            }
        }
        
        public global::System.Int32? CheckId {
            get {
                return this._CheckId;
            }
            set {
                this.OnPropertyChanged("CheckId", this._CheckId, value);
                this._CheckId = value;
            }
        }
        
        public global::System.Int32? ItemPropertyId {
            get {
                return this._ItemPropertyId;
            }
            set {
                this.OnPropertyChanged("ItemPropertyId", this._ItemPropertyId, value);
                this._ItemPropertyId = value;
            }
        }
        
        public string PropertyValue {
            get {
                return this._PropertyValue;
            }
            set {
                this.OnPropertyChanged("PropertyValue", this._PropertyValue, value);
                this._PropertyValue = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((QmcCheckDataProperty._QmcCheckDataPropertyEntityConfiguration == null)) {
                QmcCheckDataProperty._QmcCheckDataPropertyEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.QmcCheckDataProperty");
            }
            return QmcCheckDataProperty._QmcCheckDataPropertyEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._PropertyId,
                    this._CheckId,
                    this._ItemPropertyId,
                    this._PropertyValue};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._PropertyId = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._CheckId = reader.GetInt32(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._ItemPropertyId = reader.GetInt32(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._PropertyValue = reader.GetString(3);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._PropertyId = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._CheckId = ((System.Nullable<int>)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._ItemPropertyId = ((System.Nullable<int>)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._PropertyValue = ((string)(row[3]));
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
            if ((false == typeof(global::Mesnac.Entity.QmcCheckDataProperty).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.QmcCheckDataProperty)(obj)).isAttached) 
                        && (this.PropertyId == ((global::Mesnac.Entity.QmcCheckDataProperty)(obj)).PropertyId));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _PropertyId = new NBear.Common.PropertyItem("PropertyId", "Mesnac.Entity.QmcCheckDataProperty");
            
            protected NBear.Common.PropertyItem _CheckId = new NBear.Common.PropertyItem("CheckId", "Mesnac.Entity.QmcCheckDataProperty");
            
            protected NBear.Common.PropertyItem _ItemPropertyId = new NBear.Common.PropertyItem("ItemPropertyId", "Mesnac.Entity.QmcCheckDataProperty");
            
            protected NBear.Common.PropertyItem _PropertyValue = new NBear.Common.PropertyItem("PropertyValue", "Mesnac.Entity.QmcCheckDataProperty");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem PropertyId {
                get {
                    if ((aliasName == null)) {
                        return _PropertyId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PropertyId", _PropertyId.EntityConfiguration, _PropertyId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem CheckId {
                get {
                    if ((aliasName == null)) {
                        return _CheckId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("CheckId", _CheckId.EntityConfiguration, _CheckId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ItemPropertyId {
                get {
                    if ((aliasName == null)) {
                        return _ItemPropertyId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ItemPropertyId", _ItemPropertyId.EntityConfiguration, _ItemPropertyId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PropertyValue {
                get {
                    if ((aliasName == null)) {
                        return _PropertyValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PropertyValue", _PropertyValue.EntityConfiguration, _PropertyValue.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
