using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GrpcProxy.Helpers
{
    public class Encryption
    {
        public static string EncryptTrypleDES(string toEncrypt, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        public static string DecryptTrypleDES(string cypherText, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(cypherText);

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
        }

        public static void GenerateRSA()
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);

            string privKey = csp.ToXmlString(true);
            string pubKey = csp.ToXmlString(false);
        }

        public static string EncryptRSA(string cypherText, RSAParameters pubKey)
        {
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);

            //for encryption, always handle bytes...
            var bytesPlainTextData = Encoding.Unicode.GetBytes(cypherText);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, true);

            //we might want a string representation of our cypher text... base64 will do
            return Convert.ToBase64String(bytesCypherText);
        }

        public static string DecryptRSA(string cypherText, RSAParameters privateKey)
        {
            //first, get our bytes back from the base64 string ...
            byte[] bytesCypherText = Convert.FromBase64String(cypherText);

            //we want to decrypt, therefore we need a csp and load our private key
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privateKey);

            //decrypt and strip pkcs#1.5 padding
            byte[] bytesPlainTextData = csp.Decrypt(bytesCypherText, true);

            //get our original plainText back...
            return Encoding.Unicode.GetString(bytesPlainTextData);
        }
    }
}
