using System;
using System.Security.Cryptography;
using System.IO;
					
public class Program
{
	public static void Main()
	{
		//var EncryptionResponse= EncryptRequest("Taiwo");
		//Console.WriteLine("Encryption Response = " +EncryptionResponse);
		
		var DecryptionResponse= DecryptRequest("XVYlNFLuKRkXQOJjGMMGWw==");
		Console.WriteLine("Encryption Response = " +DecryptionResponse);
	}
	
	public static string EncryptRequest(string clearText)
    {
        string message = "";
        string cipherText = "";
        try
		{
            var key = Convert.FromBase64String("TaUI4+C0k2OMLkMPCGHVWXytnLh1vJC9uq/090aruoq=");
            var initVector = Convert.FromBase64String("41V0CbOtqCCy7tJVsEhqhC==");
            var plainText = clearText;
            Byte[] buffer;
            // Create a new AesManaged.
            using (AesManaged aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                // Create encryptor
                ICryptoTransform encryptor = aes.CreateEncryptor(key, initVector);
                // Create MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        buffer = ms.ToArray();
                    }
                }

            };
            return Convert.ToBase64String(buffer);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Padding is invalid"))
            {
                message = "Invalid Keys";
                return message;
            }
            if (ex.Message.Contains("The input is not a valid Base-64 string "))
            {
                message = ex.Message;
                return message;
            }
        }
        return cipherText;
    }
	
	public static string DecryptRequest(string clearText)
    {
        var message = "";
        try
        {
            var key = Convert.FromBase64String("TaUI4+C0k2OMLkMPCGHVWXytnLh1vJC9uq/090aruoq=");
            var initVector = Convert.FromBase64String("41V0CbOtqCCy7tJVsEhqhC==");
            var plainText = clearText;
            Byte[] buffer = Convert.FromBase64String(plainText);
            using (AesManaged aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(key, initVector);
                // Create the streams used for decryption.
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    // Create crypto stream
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream
                        using (StreamReader reader = new StreamReader(cs))
                            return reader.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Padding is invalid"))
            {
                message = "Invalid Keys";
                return message;
            }
            if (ex.Message.Contains("The input is not a valid Base-64 string "))
            {
                message = ex.Message;
                return message;
            }

        }
        return "";

    }
}