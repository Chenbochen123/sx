//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
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
    public partial class QmtCheckStandGradeArrayList : NBear.Common.EntityArrayList<Mesnac.Entity.QmtCheckStandGrade> {
    }
    
    [System.SerializableAttribute()]
    [NBear.Common.EmbeddedEntityConfigurationAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<EntityConfiguration xmlns:xsi=\"http://w" +
        "ww.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
        "name=\"Mesnac.Entity.QmtCheckStandGrade\" isReadOnly=\"false\" isAutoPreLoad=\"false\"" +
        " isBatchUpdate=\"false\" isRelation=\"false\" mappingName=\"QmtCheckStandGrade\" batch" +
        "Size=\"10\">\r\n  <Properties>\r\n    <Property name=\"StandId\" type=\"System.Int32\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"StandId\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrimaryKey=" +
        "\"true\" isNotNull=\"true\" />\r\n    <Property name=\"ItemCd\" type=\"System.String\" isI" +
        "nherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" i" +
        "sQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIn" +
        "dexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" ma" +
        "ppingName=\"ItemCd\" mappingColumnType=\"System.String\" sqlType=\"char(3)\" isPrimary" +
        "Key=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"WeightId\" type=\"System.Int32" +
        "\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fal" +
        "se\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\"" +
        " isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fals" +
        "e\" mappingName=\"WeightId\" mappingColumnType=\"System.Int32\" sqlType=\"int\" isPrima" +
        "ryKey=\"true\" isNotNull=\"true\" />\r\n    <Property name=\"PermMin\" type=\"System.Null" +
        "able`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"f" +
        "alse\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\"" +
        " isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSer" +
        "ializationIgnore=\"false\" mappingName=\"PermMin\" mappingColumnType=\"System.Nullabl" +
        "e`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false\" />" +
        "\r\n    <Property name=\"IfMin\" type=\"System.Nullable`1[System.Int32]\" isInherited=" +
        "\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"f" +
        "alse\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProper" +
        "ty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName" +
        "=\"IfMin\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" isPri" +
        "maryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"PermMax\" type=\"System." +
        "Nullable`1[System.Decimal]\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUni" +
        "t=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"fa" +
        "lse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" i" +
        "sSerializationIgnore=\"false\" mappingName=\"PermMax\" mappingColumnType=\"System.Nul" +
        "lable`1[System.Decimal]\" sqlType=\"decimal\" isPrimaryKey=\"false\" isNotNull=\"false" +
        "\" />\r\n    <Property name=\"IfMax\" type=\"System.Nullable`1[System.Int32]\" isInheri" +
        "ted=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuer" +
        "y=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexPr" +
        "operty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mapping" +
        "Name=\"IfMax\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlType=\"int\" i" +
        "sPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DealCode\" type=\"Sy" +
        "stem.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isCompound" +
        "Unit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=" +
        "\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false" +
        "\" isSerializationIgnore=\"false\" mappingName=\"DealCode\" mappingColumnType=\"System" +
        ".Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" " +
        "/>\r\n    <Property name=\"JudgeResult\" type=\"System.Nullable`1[System.Int32]\" isIn" +
        "herited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" is" +
        "Query=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isInd" +
        "exProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" map" +
        "pingName=\"JudgeResult\" mappingColumnType=\"System.Nullable`1[System.Int32]\" sqlTy" +
        "pe=\"int\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"Grade\" t" +
        "ype=\"System.Nullable`1[System.Int32]\" isInherited=\"false\" isReadOnly=\"false\" isC" +
        "ompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLa" +
        "zyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc" +
        "=\"false\" isSerializationIgnore=\"false\" mappingName=\"Grade\" mappingColumnType=\"Sy" +
        "stem.Nullable`1[System.Int32]\" sqlType=\"int\" isPrimaryKey=\"false\" isNotNull=\"fal" +
        "se\" />\r\n    <Property name=\"DrawMark\" type=\"System.String\" isInherited=\"false\" i" +
        "sReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQuery=\"false\" isF" +
        "riendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndexProperty=\"false" +
        "\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappingName=\"DrawMar" +
        "k\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(20)\" isPrimaryKey=\"false\"" +
        " isNotNull=\"false\" />\r\n    <Property name=\"CardMark\" type=\"System.Byte[]\" isInhe" +
        "rited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"false\" isQu" +
        "ery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false\" isIndex" +
        "Property=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"false\" mappi" +
        "ngName=\"CardMark\" mappingColumnType=\"System.Byte[]\" sqlType=\"image\" isPrimaryKey" +
        "=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"CardMark2\" type=\"System.Strin" +
        "g\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"false\" isContained=\"fa" +
        "lse\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" isRelationKey=\"false" +
        "\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerializationIgnore=\"fal" +
        "se\" mappingName=\"CardMark2\" mappingColumnType=\"System.String\" sqlType=\"nvarchar(" +
        "20)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name=\"DeleteFlag\" " +
        "type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUnit=\"fals" +
        "e\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"false\" is" +
        "RelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" isSerial" +
        "izationIgnore=\"false\" mappingName=\"DeleteFlag\" mappingColumnType=\"System.String\"" +
        " sqlType=\"char(1)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n    <Property name" +
        "=\"GUID\" type=\"System.String\" isInherited=\"false\" isReadOnly=\"false\" isCompoundUn" +
        "it=\"false\" isContained=\"false\" isQuery=\"false\" isFriendKey=\"false\" isLazyLoad=\"f" +
        "alse\" isRelationKey=\"false\" isIndexProperty=\"false\" isIndexPropertyDesc=\"false\" " +
        "isSerializationIgnore=\"false\" mappingName=\"GUID\" mappingColumnType=\"System.Strin" +
        "g\" sqlType=\"nchar(36)\" isPrimaryKey=\"false\" isNotNull=\"false\" />\r\n  </Properties" +
        ">\r\n</EntityConfiguration>")]
    public partial class QmtCheckStandGrade : NBear.Common.Entity {
        
        protected static NBear.Common.EntityConfiguration _QmtCheckStandGradeEntityConfiguration;
        
        protected int _StandId;
        
        protected string _ItemCd;
        
        protected int _WeightId;
        
        protected global::System.Decimal? _PermMin;
        
        protected global::System.Int32? _IfMin;
        
        protected global::System.Decimal? _PermMax;
        
        protected global::System.Int32? _IfMax;
        
        protected global::System.Int32? _DealCode;
        
        protected global::System.Int32? _JudgeResult;
        
        protected global::System.Int32? _Grade;
        
        protected string _DrawMark;
        
        protected byte[] _CardMark;
        
        protected string _CardMark2;
        
        protected string _DeleteFlag;
        
        protected string _GUID;
        
        public static @__Columns _ = new @__Columns();
        
		public static bool operator ==(global::Mesnac.Entity.QmtCheckStandGrade left, global::Mesnac.Entity.QmtCheckStandGrade right) { return ((object)left) != null ? left.Equals(right) : ((object)right) == null ? true : false; }


		public static bool operator !=(global::Mesnac.Entity.QmtCheckStandGrade left, global::Mesnac.Entity.QmtCheckStandGrade right) { return ((object)left) != null ? !left.Equals(right) : ((object)right) == null ? false : true; }


        
        public int StandId {
            get {
                return this._StandId;
            }
            set {
                this.OnPropertyChanged("StandId", this._StandId, value);
                this._StandId = value;
            }
        }
        
        public string ItemCd {
            get {
                return this._ItemCd;
            }
            set {
                this.OnPropertyChanged("ItemCd", this._ItemCd, value);
                this._ItemCd = value;
            }
        }
        
        public int WeightId {
            get {
                return this._WeightId;
            }
            set {
                this.OnPropertyChanged("WeightId", this._WeightId, value);
                this._WeightId = value;
            }
        }
        
        public global::System.Decimal? PermMin {
            get {
                return this._PermMin;
            }
            set {
                this.OnPropertyChanged("PermMin", this._PermMin, value);
                this._PermMin = value;
            }
        }
        
        public global::System.Int32? IfMin {
            get {
                return this._IfMin;
            }
            set {
                this.OnPropertyChanged("IfMin", this._IfMin, value);
                this._IfMin = value;
            }
        }
        
        public global::System.Decimal? PermMax {
            get {
                return this._PermMax;
            }
            set {
                this.OnPropertyChanged("PermMax", this._PermMax, value);
                this._PermMax = value;
            }
        }
        
        public global::System.Int32? IfMax {
            get {
                return this._IfMax;
            }
            set {
                this.OnPropertyChanged("IfMax", this._IfMax, value);
                this._IfMax = value;
            }
        }
        
        public global::System.Int32? DealCode {
            get {
                return this._DealCode;
            }
            set {
                this.OnPropertyChanged("DealCode", this._DealCode, value);
                this._DealCode = value;
            }
        }
        
        public global::System.Int32? JudgeResult {
            get {
                return this._JudgeResult;
            }
            set {
                this.OnPropertyChanged("JudgeResult", this._JudgeResult, value);
                this._JudgeResult = value;
            }
        }
        
        public global::System.Int32? Grade {
            get {
                return this._Grade;
            }
            set {
                this.OnPropertyChanged("Grade", this._Grade, value);
                this._Grade = value;
            }
        }
        
        public string DrawMark {
            get {
                return this._DrawMark;
            }
            set {
                this.OnPropertyChanged("DrawMark", this._DrawMark, value);
                this._DrawMark = value;
            }
        }
        
        public byte[] CardMark {
            get {
                return this._CardMark;
            }
            set {
                this.OnPropertyChanged("CardMark", this._CardMark, value);
                this._CardMark = value;
            }
        }
        
        public string CardMark2 {
            get {
                return this._CardMark2;
            }
            set {
                this.OnPropertyChanged("CardMark2", this._CardMark2, value);
                this._CardMark2 = value;
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
        
        public string GUID {
            get {
                return this._GUID;
            }
            set {
                this.OnPropertyChanged("GUID", this._GUID, value);
                this._GUID = value;
            }
        }
        
        public override NBear.Common.EntityConfiguration GetEntityConfiguration() {
            if ((QmtCheckStandGrade._QmtCheckStandGradeEntityConfiguration == null)) {
                QmtCheckStandGrade._QmtCheckStandGradeEntityConfiguration = NBear.Common.MetaDataManager.GetEntityConfiguration("Mesnac.Entity.QmtCheckStandGrade");
            }
            return QmtCheckStandGrade._QmtCheckStandGradeEntityConfiguration;
        }
        
        public override void ReloadQueries(bool includeLazyLoadQueries) {
        }
        
        public override object[] GetPropertyValues() {
            return new object[] {
                    this._StandId,
                    this._ItemCd,
                    this._WeightId,
                    this._PermMin,
                    this._IfMin,
                    this._PermMax,
                    this._IfMax,
                    this._DealCode,
                    this._JudgeResult,
                    this._Grade,
                    this._DrawMark,
                    this._CardMark,
                    this._CardMark2,
                    this._DeleteFlag,
                    this._GUID};
        }
        
        public override void SetPropertyValues(System.Data.IDataReader reader) {
            if ((false == reader.IsDBNull(0))) {
                this._StandId = reader.GetInt32(0);
            }
            if ((false == reader.IsDBNull(1))) {
                this._ItemCd = reader.GetString(1);
            }
            if ((false == reader.IsDBNull(2))) {
                this._WeightId = reader.GetInt32(2);
            }
            if ((false == reader.IsDBNull(3))) {
                this._PermMin = reader.GetDecimal(3);
            }
            if ((false == reader.IsDBNull(4))) {
                this._IfMin = reader.GetInt32(4);
            }
            if ((false == reader.IsDBNull(5))) {
                this._PermMax = reader.GetDecimal(5);
            }
            if ((false == reader.IsDBNull(6))) {
                this._IfMax = reader.GetInt32(6);
            }
            if ((false == reader.IsDBNull(7))) {
                this._DealCode = reader.GetInt32(7);
            }
            if ((false == reader.IsDBNull(8))) {
                this._JudgeResult = reader.GetInt32(8);
            }
            if ((false == reader.IsDBNull(9))) {
                this._Grade = reader.GetInt32(9);
            }
            if ((false == reader.IsDBNull(10))) {
                this._DrawMark = reader.GetString(10);
            }
            if ((false == reader.IsDBNull(11))) {
                this._CardMark = ((byte[])(reader.GetValue(11)));
            }
            if ((false == reader.IsDBNull(12))) {
                this._CardMark2 = reader.GetString(12);
            }
            if ((false == reader.IsDBNull(13))) {
                this._DeleteFlag = reader.GetString(13);
            }
            if ((false == reader.IsDBNull(14))) {
                this._GUID = reader.GetString(14);
            }
            this.ReloadQueries(false);
        }
        
        public override void SetPropertyValues(System.Data.DataRow row) {
            if ((false == row.IsNull(0))) {
                this._StandId = ((int)(row[0]));
            }
            if ((false == row.IsNull(1))) {
                this._ItemCd = ((string)(row[1]));
            }
            if ((false == row.IsNull(2))) {
                this._WeightId = ((int)(row[2]));
            }
            if ((false == row.IsNull(3))) {
                this._PermMin = ((System.Nullable<decimal>)(row[3]));
            }
            if ((false == row.IsNull(4))) {
                this._IfMin = ((System.Nullable<int>)(row[4]));
            }
            if ((false == row.IsNull(5))) {
                this._PermMax = ((System.Nullable<decimal>)(row[5]));
            }
            if ((false == row.IsNull(6))) {
                this._IfMax = ((System.Nullable<int>)(row[6]));
            }
            if ((false == row.IsNull(7))) {
                this._DealCode = ((System.Nullable<int>)(row[7]));
            }
            if ((false == row.IsNull(8))) {
                this._JudgeResult = ((System.Nullable<int>)(row[8]));
            }
            if ((false == row.IsNull(9))) {
                this._Grade = ((System.Nullable<int>)(row[9]));
            }
            if ((false == row.IsNull(10))) {
                this._DrawMark = ((string)(row[10]));
            }
            if ((false == row.IsNull(11))) {
                this._CardMark = ((byte[])(row[11]));
            }
            if ((false == row.IsNull(12))) {
                this._CardMark2 = ((string)(row[12]));
            }
            if ((false == row.IsNull(13))) {
                this._DeleteFlag = ((string)(row[13]));
            }
            if ((false == row.IsNull(14))) {
                this._GUID = ((string)(row[14]));
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
            if ((false == typeof(global::Mesnac.Entity.QmtCheckStandGrade).IsAssignableFrom(obj.GetType()))) {
                return false;
            }
            if ((((object)(this)) == ((object)(obj)))) {
                return true;
            }
            return ((((this.isAttached && ((global::Mesnac.Entity.QmtCheckStandGrade)(obj)).isAttached) 
                        && (this.StandId == ((global::Mesnac.Entity.QmtCheckStandGrade)(obj)).StandId)) 
                        && (this.ItemCd == ((global::Mesnac.Entity.QmtCheckStandGrade)(obj)).ItemCd)) 
                        && (this.WeightId == ((global::Mesnac.Entity.QmtCheckStandGrade)(obj)).WeightId));
        }
        
        public static @__Columns @__Alias(string aliasName) {
            return new @__Columns(aliasName);
        }
        
        public class @__Columns {
            
            protected string aliasName;
            
            protected NBear.Common.PropertyItem _StandId = new NBear.Common.PropertyItem("StandId", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _ItemCd = new NBear.Common.PropertyItem("ItemCd", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _WeightId = new NBear.Common.PropertyItem("WeightId", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _PermMin = new NBear.Common.PropertyItem("PermMin", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _IfMin = new NBear.Common.PropertyItem("IfMin", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _PermMax = new NBear.Common.PropertyItem("PermMax", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _IfMax = new NBear.Common.PropertyItem("IfMax", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _DealCode = new NBear.Common.PropertyItem("DealCode", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _JudgeResult = new NBear.Common.PropertyItem("JudgeResult", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _Grade = new NBear.Common.PropertyItem("Grade", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _DrawMark = new NBear.Common.PropertyItem("DrawMark", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _CardMark = new NBear.Common.PropertyItem("CardMark", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _CardMark2 = new NBear.Common.PropertyItem("CardMark2", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _DeleteFlag = new NBear.Common.PropertyItem("DeleteFlag", "Mesnac.Entity.QmtCheckStandGrade");
            
            protected NBear.Common.PropertyItem _GUID = new NBear.Common.PropertyItem("GUID", "Mesnac.Entity.QmtCheckStandGrade");
            
            public @__Columns() {
            }
            
            public @__Columns(string aliasName) {
                this.aliasName = aliasName;
            }
            
            public NBear.Common.PropertyItem StandId {
                get {
                    if ((aliasName == null)) {
                        return _StandId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("StandId", _StandId.EntityConfiguration, _StandId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem ItemCd {
                get {
                    if ((aliasName == null)) {
                        return _ItemCd;
                    }
                    else {
                        return new NBear.Common.PropertyItem("ItemCd", _ItemCd.EntityConfiguration, _ItemCd.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem WeightId {
                get {
                    if ((aliasName == null)) {
                        return _WeightId;
                    }
                    else {
                        return new NBear.Common.PropertyItem("WeightId", _WeightId.EntityConfiguration, _WeightId.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PermMin {
                get {
                    if ((aliasName == null)) {
                        return _PermMin;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PermMin", _PermMin.EntityConfiguration, _PermMin.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem IfMin {
                get {
                    if ((aliasName == null)) {
                        return _IfMin;
                    }
                    else {
                        return new NBear.Common.PropertyItem("IfMin", _IfMin.EntityConfiguration, _IfMin.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem PermMax {
                get {
                    if ((aliasName == null)) {
                        return _PermMax;
                    }
                    else {
                        return new NBear.Common.PropertyItem("PermMax", _PermMax.EntityConfiguration, _PermMax.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem IfMax {
                get {
                    if ((aliasName == null)) {
                        return _IfMax;
                    }
                    else {
                        return new NBear.Common.PropertyItem("IfMax", _IfMax.EntityConfiguration, _IfMax.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem DealCode {
                get {
                    if ((aliasName == null)) {
                        return _DealCode;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DealCode", _DealCode.EntityConfiguration, _DealCode.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem JudgeResult {
                get {
                    if ((aliasName == null)) {
                        return _JudgeResult;
                    }
                    else {
                        return new NBear.Common.PropertyItem("JudgeResult", _JudgeResult.EntityConfiguration, _JudgeResult.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem Grade {
                get {
                    if ((aliasName == null)) {
                        return _Grade;
                    }
                    else {
                        return new NBear.Common.PropertyItem("Grade", _Grade.EntityConfiguration, _Grade.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem DrawMark {
                get {
                    if ((aliasName == null)) {
                        return _DrawMark;
                    }
                    else {
                        return new NBear.Common.PropertyItem("DrawMark", _DrawMark.EntityConfiguration, _DrawMark.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem CardMark {
                get {
                    if ((aliasName == null)) {
                        return _CardMark;
                    }
                    else {
                        return new NBear.Common.PropertyItem("CardMark", _CardMark.EntityConfiguration, _CardMark.PropertyConfiguration, aliasName);
                    }
                }
            }
            
            public NBear.Common.PropertyItem CardMark2 {
                get {
                    if ((aliasName == null)) {
                        return _CardMark2;
                    }
                    else {
                        return new NBear.Common.PropertyItem("CardMark2", _CardMark2.EntityConfiguration, _CardMark2.PropertyConfiguration, aliasName);
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
            
            public NBear.Common.PropertyItem GUID {
                get {
                    if ((aliasName == null)) {
                        return _GUID;
                    }
                    else {
                        return new NBear.Common.PropertyItem("GUID", _GUID.EntityConfiguration, _GUID.PropertyConfiguration, aliasName);
                    }
                }
            }
        }
    }
}
