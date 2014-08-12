using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public interface IEncryption
{
	string Encrypt(string plainText, string password);
	bool TryDecrypt(string cipherText, string password, out string plainText);
}

public class DESEncryption : IEncryption
{
	const int Iterations = 1000;

	public string Encrypt(string plainText, string password)
	{
		if(plainText == null)
		{
			throw new ArgumentNullException("plainText");
		}

		if(string.IsNullOrEmpty(password))
		{
			throw new ArgumentNullException("password");
		}

		// Creates an instance of the DES Cryptography provider
		var des = new DESCryptoServiceProvider();

		// Generates a random IV which will be used as a salt value for generating
		// the encryption key
		des.GenerateIV();

		// Generates a key from the password and IV, using derive bytes
		var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, des.IV, Iterations);

		// Generates a key from the password that is provided
		byte[] key = rfc2898DeriveBytes.GetBytes(8);

		// Encrypt the plainText
		using(var memoryStream = new MemoryStream())
		{
			using(var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, des.IV), CryptoStreamMode.Write))
			{
				// Write the salt first not encrypted
				memoryStream.Write (des.IV, 0, des.IV.Length);

				// Convert the plain text string into a byte array
				byte[] bytes = Encoding.UTF8.GetBytes(plainText);

				// Write the bytes into the crypto stream so that they are encrypted bytes
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();

				return Convert.ToBase64String(memoryStream.ToArray());
			}
		}
	}

	public bool TryDecrypt(string cipherText, string password, out string plainText)
	{
		// It's pointless trying to decrypt if the cipher text
		// or password has not been supplied
		if(string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(password))
		{
			plainText = "";
			return false;
		}

		try
		{
			byte[] cipherBytes = Convert.FromBase64String(cipherText);

			using(var memoryStream = new MemoryStream(cipherBytes))
			{
				var des = new DESCryptoServiceProvider();

				byte[] iv = new byte[8];
				memoryStream.Read(iv, 0, iv.Length);

				var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, iv, Iterations);

				byte[] key = rfc2898DeriveBytes.GetBytes(8);

				using(var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
				{
					using(var streamReader = new StreamReader(cryptoStream))
					{
						plainText = streamReader.ReadToEnd();
						return true;
					}
				}
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine (ex);

			plainText = "";
			return false;
		}
	}
}