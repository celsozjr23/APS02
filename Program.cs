using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Criptografia
{
    private static byte[] bIV =
    { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
        0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

    public static string cryptoKey =
            null;

    public static string Encrypt(string text)
    {
        try
        {
            if (!string.IsNullOrEmpty(text))
            {               
                byte[] bKey = Convert.FromBase64String(cryptoKey);
                byte[] bText = new ASCIIEncoding().GetBytes(text);

                Rijndael rijndael = new RijndaelManaged();
               
                // chaves possíves:                
                // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                rijndael.KeySize = 128;
            
                MemoryStream mStream = new MemoryStream();
                               
                CryptoStream encryptor = new CryptoStream(
                    mStream,
                    rijndael.CreateEncryptor(bKey, bIV),
                    CryptoStreamMode.Write);

                encryptor.Write(bText, 0, bText.Length);
                               
                encryptor.FlushFinalBlock();
                              
                return Convert.ToBase64String(mStream.ToArray());
            }
            else
            {             
                return null;
            }
        }
        catch (Exception ex)
        {      
            throw new ApplicationException("Erro ao criptografar", ex);
        }
    }
    public static string Decrypt(string text)
    {
        try
        {         
            if (!string.IsNullOrEmpty(text))
            {              
                byte[] bKey = Convert.FromBase64String(cryptoKey);
                byte[] bText = Convert.FromBase64String(text);
             
                Rijndael rijndael = new RijndaelManaged();

                // chaves possíves:                
                // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                rijndael.KeySize = 128;
           
                MemoryStream mStream = new MemoryStream();
       
                CryptoStream decryptor = new CryptoStream(
                    mStream,
                    rijndael.CreateDecryptor(bKey, bIV),
                    CryptoStreamMode.Write);

                decryptor.Write(bText, 0, bText.Length);
                              
                decryptor.FlushFinalBlock();
                        
                ASCIIEncoding aSCII = new ASCIIEncoding();
                      
                return aSCII.GetString(mStream.ToArray());
            }
            else
            {            
                return null;
            }
        }
        catch (Exception ex)
        {      
            throw new ApplicationException("Erro ao descriptografar", ex);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            String valor = null;
            bool continuar = true;
            String input = null;
            
            while (continuar==true)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("=======APS 2CC CRIPTOGRAFIA COM C#=======");
                Console.WriteLine("=========================================");

                Console.WriteLine("Digite o texto a ser criptografado: ");
                valor = Console.ReadLine();
                Console.WriteLine("=========================================");
                Console.WriteLine("Digite a chave a ser utilizada: ");
                cryptoKey = Console.ReadLine();
                Console.WriteLine("=========================================");
                
                

                input = "1";

                while (input == "1" || input == "2")
                {
                    Console.WriteLine("Para Criptografar a mensagem digite: 1");
                    Console.WriteLine("Para Descriptografar a mensagem digite: 2");
                    Console.WriteLine("Para escrever outra mensagem chave digite: 3");
                    Console.WriteLine("Para sair do programa digite: 4");
                    input = Console.ReadLine();
                    Console.WriteLine("\n=========================================\n");
                    String valorCriptografado = Criptografia.Encrypt(valor);
                    if (input == "4")
                        continuar = false;
                    else
                    {
                        if (input == "3")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            if (input == "1")
                            {
                                Console.Clear();
                                
                                Console.WriteLine("=========================================");
                                Console.WriteLine("=======APS 2CC CRIPTOGRAFIA COM C#=======");
                                Console.WriteLine("=========================================");
                                Console.WriteLine("Texto original digitado:\n");
                                Console.WriteLine(valor);
                                Console.WriteLine("\n=========================================");
                                Console.WriteLine("Texto criptografado:\n");
                                Console.WriteLine(valorCriptografado);
                                Console.WriteLine("\n=========================================\n");
                                
                            }
                            else if (input == "2")
                            {
                                Console.Clear();
                                valor = valorCriptografado;
                                String valorDescriptografado = Criptografia.Decrypt(valor);
                                Console.WriteLine("=========================================");
                                Console.WriteLine("=======APS 2CC CRIPTOGRAFIA COM C#=======");
                                Console.WriteLine("=========================================");
                                Console.WriteLine("Texto criptografado:\n");
                                Console.WriteLine(valor);
                                Console.WriteLine("\n=========================================");
                                Console.WriteLine("Texto descriptografado:\n");
                                Console.WriteLine(valorDescriptografado);
                                Console.WriteLine("\n=========================================\n");
                                valor = valorDescriptografado;
                            }
                        }
                    }
                }
            }
        }
    }
}
