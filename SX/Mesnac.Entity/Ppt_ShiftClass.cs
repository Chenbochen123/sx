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
    public partial class Ppt_ShiftClassArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.Ppt_ShiftClass> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.Ppt_ShiftClass\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isB" +
        "atchUpdate=\"false\" isRelation=\"false\" mappingName=\"Ppt_ShiftClass\" batchSize=\"10" +
        "\">\r\n  <Properties>\r\n    <Property name=\"Shift_ClassId\" type=\"System.Int16\" isInh" +
        "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
        "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
        "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
        "ingName=\"Shift_ClassId\" mappingColumnType=\"System.Int16\" sqlType=\"smallint\" isPr" +
        "imaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Shift_ClassName\" type=\"" +
        "System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isC" +
        "ontained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelati" +
        "onKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializatio" +
        "nIgnore=\"false\" mappingName=\"Shift_ClassName\" mappingColumnType=\"System.String\" " +
        "sqlType=\"nvarchar(10)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property " +
        "name=\"UseFlag\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isRead" +
        "Only=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriend" +
        "Key=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isI" +
        "ndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"UseFlag\" map" +
        "pingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"fal" +
        "se\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class Ppt_ShiftClass : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _Ppt_ShiftClassEntityConfiguration;
        
        protected short _Shift_ClassId;
        
        protected string _Shift_ClassName;
        
        protected global::System.Int32? _UseFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.Ppt_ShiftClass left, global::Mesnac.Entity.Ppt_ShiftClass right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.Ppt_ShiftClass left, global::Mesnac.Entity.Ppt_ShiftClass right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public short Shift_ClassId {
            get {
                return this._Shift_ClassId;
            }
            set {
                this.OnPropertyChanged("Shift_ClassId", this._Shift_ClassId, value);
                this._Shift_ClassId = value;
            }
        }
        
        public string Shift_ClassName {
            get {
                return this._Shift_ClassName;
            }
            set {
                this.OnPropertyChanged("Shift_ClassName", this._Shift_ClassName, value);
                this._Shift_ClassName = value;
            }
        }
        
        public global::System.Int32? UseFlag {
            get {
                return this._UseFlag;
            }
            set {
                this.OnPropertyChanged("UseFlag", this._UseFlag, value);
                this._UseFlag = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((Ppt_ShiftClass._Ppt_ShiftClassEntityConfiguration == null)) {
                Ppt_ShiftClass._Ppt_ShiftClassEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.Ppt_ShiftClass");
            }
            return Ppt_ShiftClass._Ppt_ShiftClassEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._Shift_ClassId,
                    this._Shift_ClassName,
                    this._UseFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._Shift_ClassId = reader.GetInt16(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._Shift_ClassName = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._UseFlag = reader.GetInt32(2);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._Shift_ClassId = ((short)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._Shift_ClassName = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._UseFlag = ((System.Nullable<int>)(row[2]));
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
            if ((false == typeof(global::Mesnac.Entity.Ppt_ShiftClass).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.Ppt_ShiftClass)(obj)).isAttached) 
                        && (this.Shift_ClassId == ((global::Mesnac.Entity.Ppt_ShiftClass)(obj)).Shift_ClassId));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _Shift_ClassId = new NBear.Common.PropertyItem("Shift_ClassId", "Mesnac.Entity.Ppt_ShiftClass");
            
            protected NBear.Common.PropertyItem _Shift_ClassName = new NBear.Common.PropertyItem("Shift_ClassName", "Mesnac.Entity.Ppt_ShiftClass");
            
            protected NBear.Common.PropertyItem _UseFlag = new NBear.Common.PropertyItem("UseFlag", "Mesnac.Entity.Ppt_ShiftClass");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem Shift_ClassId {
                get {
                    if ((aliasName == null)) {
                        return _Shift_ClassId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Shift_ClassId", _Shift_ClassId.EntityConfiguration, _Shift_ClassId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Shift_ClassName {
                get {
                    if ((aliasName == null)) {
                        return _Shift_ClassName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Shift_ClassName", _Shift_ClassName.EntityConfiguration, _Shift_ClassName.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem UseFlag {
                get {
                    if ((aliasName == null)) {
                        return _UseFlag;
                    }
                    else {
                        return new NBear.Common.PropertyItem("UseFlag", _UseFlag.EntityConfiguration, _UseFlag.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}