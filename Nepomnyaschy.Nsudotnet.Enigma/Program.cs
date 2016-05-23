using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nepomnyaschy.Nsudotnet.Enigma
{
    class Program
    {
        public static string key = "file.key.txt";
        public static string usage = "usage: Enigma.exe [encrypt/decrypt] [input] [output.bin] [aes/des/rc2/rijndael]";

        public static void Encrypt(string input, string output, SymmetricAlgorithm alg)
        {

            using (var inputStream = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (var outputStream = new FileStream(output, FileMode.Create, FileAccess.Write))
                {
                    using (var cryptoStream = new CryptoStream(inputStream, alg.CreateEncryptor(), CryptoStreamMode.Read))
                    {
                        {
                            cryptoStream.CopyTo(outputStream);
                        }
                    }
                }
            }

            using (var outputStream = new FileStream(key, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(outputStream))
                {
                    streamWriter.WriteLine(Convert.ToBase64String(alg.Key));
                    streamWriter.WriteLine(Convert.ToBase64String(alg.IV));
                }
            }
        }

        public static void Decrypt(string input, string output, SymmetricAlgorithm alg)
        {
            using (var inputStream = new FileStream(key, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(inputStream))
                {
                    var keystr = streamReader.ReadLine();
                    if (keystr != null)
                        alg.Key = Convert.FromBase64String(keystr);

                    var ivstr = streamReader.ReadLine();
                    if (ivstr != null)
                        alg.IV = Convert.FromBase64String(ivstr);
                }
            }

            using (var inputStream = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (var outputStream = new FileStream(output, FileMode.Create, FileAccess.Write))
                {
                    using (
                        var cryptoStream = new CryptoStream(inputStream, alg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 4)
            {
                var alg = args[3].ToLower();
                if (alg == "aes" || alg == "des" || alg == "rc2" || alg == "rijndael")
                {
                    if (args[0].ToLower() == "encrypt")
                    {
                        if (File.Exists(args[1]))
                        {
                            var algorithm = SymmetricAlgorithm.Create(alg);
                            Encrypt(args[1], args[2], algorithm);
                            Console.WriteLine("encrypted");
                        }
                    }
                    else if (args[0].ToLower() == "decrypt")
                    {
                        if (File.Exists(args[1]))
                        {
                            var algorithm = SymmetricAlgorithm.Create(alg);
                            Decrypt(args[1], args[2], algorithm);
                            Console.WriteLine("decrypted");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(usage);
            }
        }
    }
}
