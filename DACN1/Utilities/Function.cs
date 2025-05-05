using System.Security.Cryptography;
using System.Text;

namespace DACN1.Utilities
{
    public class Function
    {
        public static int _UserID = 0;
        public static string _UserName = string.Empty;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;

        public static int _CustomerID = 0;
        public static string _CustomerName = string.Empty;
        public static string _EmailCustomer = string.Empty;
        public static string _MessageCustomer = string.Empty;
        public static string _MessageEmailCustomer = string.Empty;
        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static string MD5Password(string? text)
        {
            string str = MD5Hash(text);
            //Lập thêm 5 lần mã hóa xâu đảm bảo tỉnh bảo mật
            //Mỗi lần lặp nhân đôi xâu mã hóa, ở giữa thêm ""
            //Có thể làm các cách khác để tăng tính bảo mật ở đây
            for (int i = 0; i <= 5; i++)
                str = MD5Hash(str +"_" + str);
            return str;
        }
        public static bool Islogin()
        {
            if (string.IsNullOrEmpty(Function._UserName) || string.IsNullOrEmpty(Function._Email)||(Function._UserID <=0))
                return false;
            return true;
        }

        public static bool CustomerIsLogin()
        {
            if (string.IsNullOrEmpty(Function._CustomerName) || string.IsNullOrEmpty(Function._EmailCustomer) || (Function._CustomerID <= 0))
                return false;
            return true;
        }

    }
}
