//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
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
    public partial class PstShopAdjustArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.PstShopAdjust> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.PstShopAdjust\" isReadOnly=\"false\" isAutoPreLoad=\"false\" isBa" +
        "tchUpdate=\"false\" isRelation=\"false\" mappingName=\"PstShopAdjust\" batchSize=\"10\">" +
        "\r\n  <Properties>\r\n    <Property name=\"StorageID\" type=\"System.String\" isInherite" +
        "d=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=" +
        "\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProp" +
        "erty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingNa" +
        "me=\"StorageID\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimar" +
        "yKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"StoragePlaceID\" type=\"Syste" +
        "m.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContai" +
        "ned=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey" +
        "=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgno" +
        "re=\"false\" mappingName=\"StoragePlaceID\" mappingColumnType=\"System.String\" sqlTyp" +
        "e=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"Ba" +
        "rcode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
        "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
        "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
        "sSerializationIgnore=\"false\" mappingName=\"Barcode\" mappingColumnType=\"System.Str" +
        "ing\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Proper" +
        "ty name=\"OrderID\" type=\"System.Int32\" isInherited=\"false\" isReadOnly=\"false\" isC" +
        "ompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLa" +
        "zyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc" +
        "=\"false\" isSerializationIgnore=\"false\" mappingName=\"OrderID\" mappingColumnType=\"" +
        "System.Int32\" sqlType=\"int\" isPrimaryKey=\"true\" isNotNull=\"true\" />\r\n    <Proper" +
        "ty name=\"MaterCode\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" " +
        "isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" i" +
        "sLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyD" +
        "esc=\"false\" isSerializationIgnore=\"false\" mappingName=\"MaterCode\" mappingColumnT" +
        "ype=\"System.String\" sqlType=\"nvarchar(13)\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"ProcDate\" type=\"System.Nullable`1[System.DateTime]\" is" +
        "Inherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" " +
        "isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isI" +
        "ndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" m" +
        "appingName=\"ProcDate\" mappingColumnType=\"System.Nullable`1[System.DateTime]\" sql" +
        "Type=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"A" +
        "djustNum\" type=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=" +
        "\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"" +
        "false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexP" +
        "ropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"AdjustNum\" mappin" +
        "gColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\"" +
        " isNotNull=\"false\" />\r\n    <Property name=\"AdjustWeight\" type=\"System.Nullable`1" +
        "[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" " +
        "isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRel" +
        "ationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializa" +
        "tionIgnore=\"false\" mappingName=\"AdjustWeight\" mappingColumnType=\"System.Nullable" +
        "`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r" +
        "\n    <Property name=\"ToStorageID\" type=\"System.String\" isInherited=\"false\" isRea" +
        "dOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFrien" +
        "dKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" is" +
        "IndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"ToStorageID" +
        "\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(36)\" isPrimaryKey=\"false\" " +
        "isNotNull=\"false\" />\r\n    <Property name=\"ToStoragePlaceID\" type=\"System.String\"" +
        " isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fals" +
        "e\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" " +
        "isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false" +
        "\" mappingName=\"ToStoragePlaceID\" mappingColumnType=\"System.String\" sqlType=\"nvar" +
        "char(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Inaccou" +
        "ntDate\" type=\"System.Nullable`1[System.DateTime]\" isInherited=\"false\" isReadOnly" +
        "=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=" +
        "\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndex" +
        "PropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"InaccountDate\" m" +
        "appingColumnType=\"System.Nullable`1[System.DateTime]\" sqlType=\"datetime\" isPrima" +
        "ryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ShiftID\" type=\"System.St" +
        "ring\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=" +
        "\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"fa" +
        "lse\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"" +
        "false\" mappingName=\"ShiftID\" mappingColumnType=\"System.String\" sqlType=\"char(1)\"" +
        " isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"ShiftClassID\" ty" +
        "pe=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\"" +
        " isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRe" +
        "lationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializ" +
        "ationIgnore=\"false\" mappingName=\"ShiftClassID\" mappingColumnType=\"System.String\"" +
        " sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name" +
        "=\"OperPerson\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isComp" +
        "oundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyL" +
        "oad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"f" +
        "alse\" isSerializationIgnore=\"false\" mappingName=\"OperPerson\" mappingColumnType=\"" +
        "System.String\" sqlType=\"nvarchar(50)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r" +
        "\n    <Property name=\"RecordDate\" type=\"System.Nullable`1[System.DateTime]\" isInh" +
        "erited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQ" +
        "uery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInde" +
        "xProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapp" +
        "ingName=\"RecordDate\" mappingColumnType=\"System.Nullable`1[System.DateTime]\" sqlT" +
        "ype=\"datetime\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties>\r\n</Ent" +
        "ityConfiguration>")]
    public partial class PstShopAdjust : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _PstShopAdjustEntityConfiguration;
        
        protected string _StorageID;
        
        protected string _StoragePlaceID;
        
        protected string _Barcode;
        
        protected int _OrderID;
        
        protected string _MaterCode;
        
        protected global::System.DateTime? _ProcDate;
        
        protected global::System.Int32? _AdjustNum;
        
        protected global::System.Decimal? _AdjustWeight;
        
        protected string _ToStorageID;
        
        protected string _ToStoragePlaceID;
        
        protected global::System.DateTime? _InaccountDate;
        
        protected string _ShiftID;
        
        protected string _ShiftClassID;
        
        protected string _OperPerson;
        
        protected global::System.DateTime? _RecordDate;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.PstShopAdjust left, global::Mesnac.Entity.PstShopAdjust right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.PstShopAdjust left, global::Mesnac.Entity.PstShopAdjust right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public string StorageID {
            get {
                return this._StorageID;
            }
            set {
                this.OnPropertyChanged("StorageID", this._StorageID, value);
                this._StorageID = value;
            }
        }
        
        public string StoragePlaceID {
            get {
                return this._StoragePlaceID;
            }
            set {
                this.OnPropertyChanged("StoragePlaceID", this._StoragePlaceID, value);
                this._StoragePlaceID = value;
            }
        }
        
        public string Barcode {
            get {
                return this._Barcode;
            }
            set {
                this.OnPropertyChanged("Barcode", this._Barcode, value);
                this._Barcode = value;
            }
        }
        
        public int OrderID {
            get {
                return this._OrderID;
            }
            set {
                this.OnPropertyChanged("OrderID", this._OrderID, value);
                this._OrderID = value;
            }
        }
        
        public string MaterCode {
            get {
                return this._MaterCode;
            }
            set {
                this.OnPropertyChanged("MaterCode", this._MaterCode, value);
                this._MaterCode = value;
            }
        }
        
        public global::System.DateTime? ProcDate {
            get {
                return this._ProcDate;
            }
            set {
                this.OnPropertyChanged("ProcDate", this._ProcDate, value);
                this._ProcDate = value;
            }
        }
        
        public global::System.Int32? AdjustNum {
            get {
                return this._AdjustNum;
            }
            set {
                this.OnPropertyChanged("AdjustNum", this._AdjustNum, value);
                this._AdjustNum = value;
            }
        }
        
        public global::System.Decimal? AdjustWeight {
            get {
                return this._AdjustWeight;
            }
            set {
                this.OnPropertyChanged("AdjustWeight", this._AdjustWeight, value);
                this._AdjustWeight = value;
            }
        }
        
        public string ToStorageID {
            get {
                return this._ToStorageID;
            }
            set {
                this.OnPropertyChanged("ToStorageID", this._ToStorageID, value);
                this._ToStorageID = value;
            }
        }
        
        public string ToStoragePlaceID {
            get {
                return this._ToStoragePlaceID;
            }
            set {
                this.OnPropertyChanged("ToStoragePlaceID", this._ToStoragePlaceID, value);
                this._ToStoragePlaceID = value;
            }
        }
        
        public global::System.DateTime? InaccountDate {
            get {
                return this._InaccountDate;
            }
            set {
                this.OnPropertyChanged("InaccountDate", this._InaccountDate, value);
                this._InaccountDate = value;
            }
        }
        
        public string ShiftID {
            get {
                return this._ShiftID;
            }
            set {
                this.OnPropertyChanged("ShiftID", this._ShiftID, value);
                this._ShiftID = value;
            }
        }
        
        public string ShiftClassID {
            get {
                return this._ShiftClassID;
            }
            set {
                this.OnPropertyChanged("ShiftClassID", this._ShiftClassID, value);
                this._ShiftClassID = value;
            }
        }
        
        public string OperPerson {
            get {
                return this._OperPerson;
            }
            set {
                this.OnPropertyChanged("OperPerson", this._OperPerson, value);
                this._OperPerson = value;
            }
        }
        
        public global::System.DateTime? RecordDate {
            get {
                return this._RecordDate;
            }
            set {
                this.OnPropertyChanged("RecordDate", this._RecordDate, value);
                this._RecordDate = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((PstShopAdjust._PstShopAdjustEntityConfiguration == null)) {
                PstShopAdjust._PstShopAdjustEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.PstShopAdjust");
            }
            return PstShopAdjust._PstShopAdjustEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._StorageID,
                    this._StoragePlaceID,
                    this._Barcode,
                    this._OrderID,
                    this._MaterCode,
                    this._ProcDate,
                    this._AdjustNum,
                    this._AdjustWeight,
                    this._ToStorageID,
                    this._ToStoragePlaceID,
                    this._InaccountDate,
                    this._ShiftID,
                    this._ShiftClassID,
                    this._OperPerson,
                    this._RecordDate};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._StorageID = reader.GetString(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._StoragePlaceID = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._Barcode = reader.GetString(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._OrderID = reader.GetInt32(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._MaterCode = reader.GetString(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._ProcDate = this.GetDateTime(reader, 5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._AdjustNum = reader.GetInt32(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._AdjustWeight = reader.GetDecimal(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._ToStorageID = reader.GetString(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._ToStoragePlaceID = reader.GetString(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._InaccountDate = this.GetDateTime(reader, 10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._ShiftID = reader.GetString(11);
            }
            if ((false == reader.IsDBNull(12))) {
                this._ShiftClassID = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._OperPerson = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._RecordDate = this.GetDateTime(reader, 14);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._StorageID = ((string)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._StoragePlaceID = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._Barcode = ((string)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._OrderID = ((int)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._MaterCode = ((string)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._ProcDate = this.GetDateTime(row, 5);
            }
            if ((false == row.IsNull(6))) {
                this._AdjustNum = ((System.Nullable<int>)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._AdjustWeight = ((System.Nullable<decimal>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._ToStorageID = ((string)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._ToStoragePlaceID = ((string)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._InaccountDate = this.GetDateTime(row, 10);
            }
            if ((false == row.IsNull(11))) {
                this._ShiftID = ((string)(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._ShiftClassID = ((string)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._OperPerson = ((string)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._RecordDate = this.GetDateTime(row, 14);
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
            if ((false == typeof(global::Mesnac.Entity.PstShopAdjust).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return (((((this.isAttached && ((global::Mesnac.Entity.PstShopAdjust)(obj)).isAttached) 
                        && (this.StorageID == ((global::Mesnac.Entity.PstShopAdjust)(obj)).StorageID)) 
                        && (this.StoragePlaceID == ((global::Mesnac.Entity.PstShopAdjust)(obj)).StoragePlaceID)) 
                        && (this.Barcode == ((global::Mesnac.Entity.PstShopAdjust)(obj)).Barcode)) 
                        && (this.OrderID == ((global::Mesnac.Entity.PstShopAdjust)(obj)).OrderID));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _StorageID = new NBear.Common.PropertyItem("StorageID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _StoragePlaceID = new NBear.Common.PropertyItem("StoragePlaceID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _Barcode = new NBear.Common.PropertyItem("Barcode", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _OrderID = new NBear.Common.PropertyItem("OrderID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _MaterCode = new NBear.Common.PropertyItem("MaterCode", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _ProcDate = new NBear.Common.PropertyItem("ProcDate", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _AdjustNum = new NBear.Common.PropertyItem("AdjustNum", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _AdjustWeight = new NBear.Common.PropertyItem("AdjustWeight", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _ToStorageID = new NBear.Common.PropertyItem("ToStorageID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _ToStoragePlaceID = new NBear.Common.PropertyItem("ToStoragePlaceID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _InaccountDate = new NBear.Common.PropertyItem("InaccountDate", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _ShiftID = new NBear.Common.PropertyItem("ShiftID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _ShiftClassID = new NBear.Common.PropertyItem("ShiftClassID", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _OperPerson = new NBear.Common.PropertyItem("OperPerson", "Mesnac.Entity.PstShopAdjust");
            
            protected NBear.Common.PropertyItem _RecordDate = new NBear.Common.PropertyItem("RecordDate", "Mesnac.Entity.PstShopAdjust");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem StorageID {
                get {
                    if ((aliasName == null)) {
                        return _StorageID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StorageID", _StorageID.EntityConfiguration, _StorageID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem StoragePlaceID {
                get {
                    if ((aliasName == null)) {
                        return _StoragePlaceID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StoragePlaceID", _StoragePlaceID.EntityConfiguration, _StoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Barcode {
                get {
                    if ((aliasName == null)) {
                        return _Barcode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Barcode", _Barcode.EntityConfiguration, _Barcode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OrderID {
                get {
                    if ((aliasName == null)) {
                        return _OrderID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OrderID", _OrderID.EntityConfiguration, _OrderID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem MaterCode {
                get {
                    if ((aliasName == null)) {
                        return _MaterCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("MaterCode", _MaterCode.EntityConfiguration, _MaterCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ProcDate {
                get {
                    if ((aliasName == null)) {
                        return _ProcDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ProcDate", _ProcDate.EntityConfiguration, _ProcDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem AdjustNum {
                get {
                    if ((aliasName == null)) {
                        return _AdjustNum;
                    }
                    else {
                        return new NBear.Common.PropertyItem("AdjustNum", _AdjustNum.EntityConfiguration, _AdjustNum.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem AdjustWeight {
                get {
                    if ((aliasName == null)) {
                        return _AdjustWeight;
                    }
                    else {
                        return new NBear.Common.PropertyItem("AdjustWeight", _AdjustWeight.EntityConfiguration, _AdjustWeight.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ToStorageID {
                get {
                    if ((aliasName == null)) {
                        return _ToStorageID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ToStorageID", _ToStorageID.EntityConfiguration, _ToStorageID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ToStoragePlaceID {
                get {
                    if ((aliasName == null)) {
                        return _ToStoragePlaceID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ToStoragePlaceID", _ToStoragePlaceID.EntityConfiguration, _ToStoragePlaceID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem InaccountDate {
                get {
                    if ((aliasName == null)) {
                        return _InaccountDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("InaccountDate", _InaccountDate.EntityConfiguration, _InaccountDate.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ShiftID {
                get {
                    if ((aliasName == null)) {
                        return _ShiftID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiftID", _ShiftID.EntityConfiguration, _ShiftID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ShiftClassID {
                get {
                    if ((aliasName == null)) {
                        return _ShiftClassID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ShiftClassID", _ShiftClassID.EntityConfiguration, _ShiftClassID.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem OperPerson {
                get {
                    if ((aliasName == null)) {
                        return _OperPerson;
                    }
                    else {
                        return new NBear.Common.PropertyItem("OperPerson", _OperPerson.EntityConfiguration, _OperPerson.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem RecordDate {
                get {
                    if ((aliasName == null)) {
                        return _RecordDate;
                    }
                    else {
                        return new NBear.Common.PropertyItem("RecordDate", _RecordDate.EntityConfiguration, _RecordDate.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
