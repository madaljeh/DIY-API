using System.Security.Cryptography;
using System.Text;

namespace DIY_API.Helpers
{
    public static class HashingHelper
    {
        // algorithm 1
        public static string HashValue(string inputValue)
        {
            //convert string to bytes array
            var inputBytes = Encoding.UTF8.GetBytes(inputValue);


            //inlization hashing alogrthim object 
            var hasher = SHA256.Create();

            //compute hash
            var hashedByte = hasher.ComputeHash(inputBytes);


            //convert hashed byte to string 
            return BitConverter.ToString(hashedByte).Replace("-", "").ToLower();



            //another way to convert hashed byte to string  --> use string builder
            //StringBuilder stringBuilder = new StringBuilder();
            //foreach(var b in hashedByte)
            //{
            //    stringBuilder.Append(b.ToString("x2"));
            //}
            //return stringBuilder.ToString();
        }



        // algorithm 2
        public static string HashValueWith384(string inputValue)
        {
            //convert string to bytes array
            var inputBytes = Encoding.UTF8.GetBytes(inputValue);
            //inlization hashing alogrthim object 
            var hasher = SHA384.Create();
            //compute hash
            var hashedByte = hasher.ComputeHash(inputBytes);
            //convert hashed byte to string 
            return BitConverter.ToString(hashedByte).Replace("-", "").ToLower();
        }

        // algorithm 3
        public static string HashValueWith512(string inputValue)
        {
            //convert string to bytes array
            var inputBytes = Encoding.UTF8.GetBytes(inputValue);
            //inlization hashing alogrthim object 
            var hasher = SHA512.Create();
            //compute hash
            var hashedByte = hasher.ComputeHash(inputBytes);
            //convert hashed byte to string 
            return BitConverter.ToString(hashedByte).Replace("-", "").ToLower();
        }
    }
}
