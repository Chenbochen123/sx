//------------------------------------------------------------------------------
// <auto-generated>
//     �˴����ɹ������ɡ�
//     ����ʱ�汾:4.0.30319.18052
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
    public partial class QmcLedgerDetailArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.QmcLedgerDetail> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.QmcLedgerDetail\" isReadOnly=\"false\" isAutoPreLoad=\"false\" is" +
        "BatchUpdate=\"false\" isRelation=\"false\" mappingName=\"QmcLedgerDetail\" batchSize=\"" +
        "10\">\r\n  <Properties>\r\n    <Property name=\"DetailId\" type=\"System.Int32\" isInheri" +
        "ted=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"DetailId\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=\"tru" +
        "e\" isNotNull=\"true\" />\r\n    <Property name=\"LedgerId\" type=\"System.Nullable`1[Sy" +
        "stem.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isCon" +
        "tained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelation" +
        "Key=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationI" +
        "gnore=\"false\" mappingName=\"LedgerId\" mappingColumnType=\"System.Nullable`1[System" +
        ".Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property " +
        "name=\"KeyId\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOn" +
        "ly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKe" +
        "y=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isInd" +
        "exPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"KeyId\" mapping" +
        "ColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n    <Property name=\"KeyValue\" type=\"System.String\" isInher" +
        "ited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQue" +
        "ry=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexP" +
        "roperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappin" +
        "gName=\"KeyValue\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(60)\" isPrim" +
        "aryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</EntityConfiguration>")]
    public partial class QmcLedgerDetail : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _QmcLedgerDetailEntityConfiguration;
        
        protected int _DetailId;
        
        protected global::System.Int32? _LedgerId;
        
        protected global::System.Int32? _KeyId;
        
        protected string _KeyValue;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.QmcLedgerDetail left, global::Mesnac.Entity.QmcLedgerDetail right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.QmcLedgerDetail left, global::Mesnac.Entity.QmcLedgerDetail right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int DetailId {
            get {
                return this._DetailId;
            }
            set {
                this.OnPropertyChanged("DetailId", this._DetailId, value);
                this._DetailId = value;
            }
        }
        
        public global::System.Int32? LedgerId {
            get {
                return this._LedgerId;
            }
            set {
                this.OnPropertyChanged("LedgerId", this._LedgerId, value);
                this._LedgerId = value;
            }
        }
        
        public global::System.Int32? KeyId {
            get {
                return this._KeyId;
            }
            set {
                this.OnPropertyChanged("KeyId", this._KeyId, value);
                this._KeyId = value;
            }
        }
        
        public string KeyValue {
            get {
                return this._KeyValue;
            }
            set {
                this.OnPropertyChanged("KeyValue", this._KeyValue, value);
                this._KeyValue = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((QmcLedgerDetail._QmcLedgerDetailEntityConfiguration == null)) {
                QmcLedgerDetail._QmcLedgerDetailEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.QmcLedgerDetail");
            }
            return QmcLedgerDetail._QmcLedgerDetailEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._DetailId,
                    this._LedgerId,
                    this._KeyId,
                    this._KeyValue};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._DetailId = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._LedgerId = reader.GetInt32(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._KeyId = reader.GetInt32(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._KeyValue = reader.GetString(3);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._DetailId = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._LedgerId = ((System.Nullable<int>)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._KeyId = ((System.Nullable<int>)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._KeyValue = ((string)(row[3]));
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
            if ((false == typeof(global::Mesnac.Entity.QmcLedgerDetail).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((this.isAttached && ((global::Mesnac.Entity.QmcLedgerDetail)(obj)).isAttached) 
                        && (this.DetailId == ((global::Mesnac.Entity.QmcLedgerDetail)(obj)).DetailId));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _DetailId = new NBear.Common.PropertyItem("DetailId", "Mesnac.Entity.QmcLedgerDetail");
            
            protected NBear.Common.PropertyItem _LedgerId = new NBear.Common.PropertyItem("LedgerId", "Mesnac.Entity.QmcLedgerDetail");
            
            protected NBear.Common.PropertyItem _KeyId = new NBear.Common.PropertyItem("KeyId", "Mesnac.Entity.QmcLedgerDetail");
            
            protected NBear.Common.PropertyItem _KeyValue = new NBear.Common.PropertyItem("KeyValue", "Mesnac.Entity.QmcLedgerDetail");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem DetailId {
                get {
                    if ((aliasName == null)) {
                        return _DetailId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DetailId", _DetailId.EntityConfiguration, _DetailId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem LedgerId {
                get {
                    if ((aliasName == null)) {
                        return _LedgerId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("LedgerId", _LedgerId.EntityConfiguration, _LedgerId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem KeyId {
                get {
                    if ((aliasName == null)) {
                        return _KeyId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("KeyId", _KeyId.EntityConfiguration, _KeyId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem KeyValue {
                get {
                    if ((aliasName == null)) {
                        return _KeyValue;
                    }
                    else {
                        return new NBear.Common.PropertyItem("KeyValue", _KeyValue.EntityConfiguration, _KeyValue.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}