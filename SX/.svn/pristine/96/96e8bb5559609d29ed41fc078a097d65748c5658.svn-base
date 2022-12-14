using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Entity
{
    /// <summary>
    /// 实体类SearchKeyWord
    /// </summary>
    [Serializable]
    public class SearchKeyWord
    {
        private int id;
        private string keyWord = String.Empty;
        private int clicks = 0;

        public SearchKeyWord() { }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string KeyWord
        {
            get { return keyWord; }
            set { keyWord = value; }
        }

        public int Clicks
        {
            get { return clicks; }
            set { clicks = value; }
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("id:" + this.id + ",");
            sb.Append("keyWord:'" + this.keyWord + "',");
            sb.Append("clicks:" + this.clicks);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
