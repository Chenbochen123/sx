using System;
using System.Text;

namespace Mesnac.Util.Cryptography
{
    /// <summary>
    /// 数据加密
    /// 孙本强 @ 2013-04-03 13:12:01
    /// </summary>
    /// <remarks></remarks>
    public class MesnacEngine : ISKCrypto
    {
        /// <summary>
        /// 加密
        /// 孙本强 @ 2013-04-03 13:12:02
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="key">The key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EncryptString(string src, string key, Encoding encoding)
        {
            int KeyLen;
            int KeyPos;
            int offset;
            string dest;
            int SrcPos;
            int SrcAsc;

            KeyLen = key.Length;
            if (KeyLen == 0)
            {
                key = "Mesnac";
            }
            KeyPos = 0;
            SrcPos = 0;
            SrcAsc = 0;

            System.Random r = new Random();
            offset = r.Next() % 256 + 1;
            dest = string.Format("{0:X}", offset);
            if (dest.Length == 1)
            {
                dest = "0" + dest;
            }
            for (SrcPos = 0; SrcPos < src.Length; SrcPos++)
            {
                SrcAsc = ((int)src[SrcPos] + offset) % 255;
                if (KeyPos < KeyLen)
                {
                    KeyPos = KeyPos + 1;
                }
                else { KeyPos = 0; }

                SrcAsc = SrcAsc ^ (int)key[KeyPos]; //异或
                string tempSrcAsc = string.Format("{0:X}", SrcAsc);
                if (tempSrcAsc.Length == 1)
                {
                    tempSrcAsc = "0" + tempSrcAsc;
                }
                dest = dest + tempSrcAsc;
                offset = SrcAsc;
            }
            return dest;
        }
        /// <summary>
        /// 解密
        /// 孙本强 @ 2013-04-03 13:12:02
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="key">The key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DecryptString(string src, string key, Encoding encoding)
        {
            int KeyLen;
            int KeyPos;
            int offset;
            string dest;
            int SrcPos;
            int SrcAsc;
            int TmpSrcAsc;

            KeyLen = key.Length;
            if (KeyLen == 0)
            {
                key = "Mesnac";
            }
            KeyPos = 0;
            SrcPos = 0;
            SrcAsc = 0;
            if (src.Length <= 2)
            {
                return string.Empty;
            }
            dest = "";
            offset = Convert.ToInt32(src.Substring(0, 2), 16);
            SrcPos = 2;
            while (SrcPos < src.Length)
            {
                SrcAsc = Convert.ToInt32(src.Substring(SrcPos, 2), 16);
                if (KeyPos < KeyLen)
                {
                    KeyPos = KeyPos + 1;
                }
                else { KeyPos = 0; }

                TmpSrcAsc = SrcAsc ^ (int)key[KeyPos]; //异或
                if (TmpSrcAsc <= offset)
                {
                    TmpSrcAsc = 255 + TmpSrcAsc - offset;
                }
                else
                {
                    TmpSrcAsc = TmpSrcAsc - offset;
                }
                dest = dest + (char)TmpSrcAsc;
                offset = SrcAsc;
                SrcPos = SrcPos + 2;
            }
            return dest;
        }

    }
}
