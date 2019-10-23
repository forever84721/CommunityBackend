using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Service.Common
{
    public static class Utility
    {
        public static void SetCultureInfo()
        {
            CultureInfo.CurrentCulture = new CultureInfo("zh-TW");
            DateTime.Parse("", CultureInfo.CurrentCulture);
        }
        public static string PasswordEncoding(string Pd)
        {
#pragma warning disable IDE0067 // 必須在超出範圍前處置物件
            SHA256 sha256 = new SHA256CryptoServiceProvider();//建立一個SHA256
#pragma warning restore IDE0067 // 必須在超出範圍前處置物件
            byte[] source = System.Text.Encoding.Default.GetBytes(Pd);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }
    }
}
