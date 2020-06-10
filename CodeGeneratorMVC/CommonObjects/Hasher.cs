using System.Text;
using Microsoft.VisualBasic;

namespace Tools {
    public class Hasher {
        /// <summary>
		/// 		''' create SHA1 Hash from given String
		/// 		''' </summary>
		/// 		''' <param name="hashString">String to Hash</param>
		/// 		''' <returns>40 Character Hex Hash for given string.</returns>
		/// 		''' <remarks></remarks>
        public static string CreateSHA1Hash(string hashString) {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(hashString);
            System.Security.Cryptography.SHA1 hasher;
            hasher = System.Security.Cryptography.SHA1.Create();
            byte[] hash = hasher.ComputeHash(data);
            StringBuilder stB = new StringBuilder();
            foreach (byte b in hash)
                stB.Append(Conversion.Hex(b).PadLeft(2, '0'));
            return stB.ToString();
        }
    }
}
