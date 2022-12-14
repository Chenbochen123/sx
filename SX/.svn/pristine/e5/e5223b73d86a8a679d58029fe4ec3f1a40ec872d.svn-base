using System.IO;
using System.Text;

namespace Mesnac.Util.Cryptography
{
    /// <summary>
    /// symmetric key cryptography
    /// 对称加密算法
    /// 孙本强 @ 2013-04-03 13:11:55
    /// </summary>
    /// <remarks></remarks>
    public interface ISKCrypto
    {
        /// <summary>
        /// 加密
        /// 孙本强 @ 2013-04-03 13:11:56
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="key">The key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string EncryptString(string src, string key, Encoding encoding);
        /// <summary>
        /// 解密
        /// 孙本强 @ 2013-04-03 13:11:56
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="key">The key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string DecryptString(string src, string key, Encoding encoding);
    }
    /// <summary>
    /// 哈希算法
    /// 孙本强 @ 2013-04-03 13:11:56
    /// </summary>
    /// <remarks></remarks>
    public interface IHashCrypto
    {
        /// <summary>
        /// Hashes the buff.
        /// 孙本强 @ 2013-04-03 13:11:56
        /// </summary>
        /// <param name="buff">The buff.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string HashBuff(byte[] buff);
        /// <summary>
        /// Hashes the stream.
        /// 孙本强 @ 2013-04-03 13:11:56
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string HashStream(Stream stream);
        /// <summary>
        /// Hashes the file.
        /// 孙本强 @ 2013-04-03 13:11:56
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string HashFile(string path);
        /// <summary>
        /// Hashes the string.
        /// 孙本强 @ 2013-04-03 13:11:56
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        string HashString(string str, Encoding encoding);
    }
}