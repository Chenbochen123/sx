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
    public partial class BasEquipTypeArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.BasEquipType> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsd=\"http://w" +
        "ww.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "name=\"Mesnac.Entity.BasEquipType\" isReadOnly=\"true\" isAutoPreLoad=\"false\" isBatc" +
        "hUpdate=\"false\" isRelation=\"false\" mappingName=\"BasEquipType\" batchSize=\"10\">\r\n " +
        " <Properties>\r\n    <Property name=\"ObjID\" type=\"System.String\" isInherited=\"fals" +
        "e\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" " +
        "isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"fa" +
        "lse\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ObjI" +
        "D\" mappingColumnType=\"System.String\" sqlType=\"char(2)\" isPrimaryKey=\"false\" isNo" +
        "tNull=\"false\" />\r\n    <Property name=\"EquipTypeName\" type=\"System.String\" isInhe" +
        "rited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContained=\"false\" isQue" +
        "ry=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexP" +
        "roperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappin" +
        "gName=\"EquipTypeName\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(10)\" i" +
        "sPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Remark\" type=\"Syst" +
        "em.String\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"Remark\" mappingColumnType=\"System.String\" sqlType=\"nvarc" +
        "har(30)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFl" +
        "ag\" type=\"System.Int32\" isInherited=\"false\" isReadOnly=\"true\" isCompoundUnit=\"fa" +
        "lse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" " +
        "isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSeri" +
        "alizationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.Int32" +
        "\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</En" +
        "tityConfiguration>")]
    public partial class BasEquipType : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _BasEquipTypeEntityConfiguration;
        
        protected string _ObjID;
        
        protected string _EquipTypeName;
        
        protected string _Remark;
        
        protected int _DeleteFlag;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.BasEquipType left, global::Mesnac.Entity.BasEquipType right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.BasEquipType left, global::Mesnac.Entity.BasEquipType right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string ObjID {
            get {
                return this._ObjID;
            }
            set {
                this.OnPropertyChanged("ObjID", this._ObjID, value);
                this._ObjID = value;
            }
        }
        
        public string EquipTypeName {
            get {
                return this._EquipTypeName;
            }
            set {
                this.OnPropertyChanged("EquipTypeName", this._EquipTypeName, value);
                this._EquipTypeName = value;
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
        
        public int DeleteFlag {
            get {
                return this._DeleteFlag;
            }
            set {
                this.OnPropertyChanged("DeleteFlag", this._DeleteFlag, value);
                this._DeleteFlag = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((BasEquipType._BasEquipTypeEntityConfiguration == null)) {
                BasEquipType._BasEquipTypeEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.BasEquipType");
            }
            return BasEquipType._BasEquipTypeEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._ObjID,
                    this._EquipTypeName,
                    this._Remark,
                    this._DeleteFlag};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._ObjID = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._EquipTypeName = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Remark = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._DeleteFlag = reader.GetInt32(3);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._ObjID = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._EquipTypeName = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Remark = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._DeleteFlag = ((int)(row[3]));
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
            if ((false == typeof(global::Mesnac.Entity.BasEquipType).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.BasEquipType)(obj)).isAttached) 
                        && base.Equals(obj));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _ObjID = new NBear.Common.PropertyItem("ObjID", "Mesnac.Entity.BasEquipType");
            
            protected NBear.Common.PropertyItem _EquipTypeName = new NBear.Common.PropertyItem("EquipTypeName", "Mesnac.Entity.BasEquipType");
            
            protected NBear.Common.PropertyItem _Remark = new NBear.Common.PropertyItem("Remark", "Mesnac.Entity.BasEquipType");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.BasEquipType");
            
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
            
            public NBear.Common.PropertyItem EquipTypeName {
                get {
                    if ((aliasName == null)) {
                        return _EquipTypeName;
                    }
                    else {
                        return new NBear.Common.PropertyItem("EquipTypeName", _EquipTypeName.EntityConfiguration, _EquipTypeName.PropertyConfiguration, aliasName);
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