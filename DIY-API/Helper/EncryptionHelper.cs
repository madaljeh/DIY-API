
//using System.Security.Cryptography;
//using System.Text;

//namespace DIY_API.Helpers
//{
//    public static class EncryptionHelper
//    {
//        //two values one for the value i want to encrypt and the other bute of array for the key
//        public static EncyptDataDTO  Encrypt(string value, byte[] key)
//        {
//            //احدث نسخة من AES وهي AES-GCM
//            using var aesGcm = new AesGcm(key);
//            //مفتاح تشفير اولي 
//            var nonce = RandomNumberGenerator.GetBytes(12);
//            //احول القيمة الداخلة 
//            var bytes = Encoding.UTF8.GetBytes(value);

//            var tag = new byte[16];
//            //البيانات المشفرة
//            var cipsherByter = new byte[bytes.Length];
//            // بيدخلو على الميموري و بنعمل عليهم ابديت 
//            aesGcm.Encrypt(nonce, bytes, cipsherByter, tag);
//            // برجعلهم للمستخدم 
//            return new EncyptDataDTO
//            {
//                CipherText = Convert.ToBase64String(cipsherByter),
//                Nonce = Convert.ToBase64String(nonce),
//                Tag = Convert.ToBase64String(tag)   
//            };
//        }
//        //فك التشفير 
//        public static string Decrypt(string CipherText , string Nonce , string Tag , byte[] key)
//        {
//            using var aesGcm = new AesGcm(key);

//            var cipherBytes = Convert.FromBase64String(CipherText);
//            var nonce = Convert.FromBase64String(Nonce);
//            var tag = Convert.FromBase64String(Tag);
//            var plainBytes = new byte[cipherBytes.Length];

//            aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes);

//            return Encoding.UTF8.GetString(plainBytes);
//        }
//    }
//}
