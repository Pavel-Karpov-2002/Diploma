using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

public class FileEncryption
{
    private static FileStream dataStream;
    private static byte[] savedKey = { 0x14, 0x13, 0x14, 0x15, 0x13, 0x15, 0x12, 0x15, 0x16, 0x16, 0x16, 0x16, 0x16, 0x12, 0x13, 0x15 };

    public static string ReadFile(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                dataStream = new FileStream(path, FileMode.Open);

                Aes oAes = Aes.Create();
                byte[] outputIV = new byte[oAes.IV.Length];
                dataStream.Read(outputIV, 0, outputIV.Length);

                CryptoStream oStream = new CryptoStream(
                       dataStream,
                       oAes.CreateDecryptor(savedKey, outputIV),
                       CryptoStreamMode.Read);

                StreamReader reader = new StreamReader(oStream);
                string text = reader.ReadToEnd();
                reader.Close();

                return text;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }
        return null;
    }

    public static void WriteFile(string path, object data)
    {
        try
        {
            Aes iAes = Aes.Create();
            dataStream = new FileStream(path, FileMode.Create);

            byte[] inputIV = iAes.IV;
            dataStream.Write(inputIV, 0, inputIV.Length);

            CryptoStream iStream = new CryptoStream(
                    dataStream,
                    iAes.CreateEncryptor(savedKey, iAes.IV),
                    CryptoStreamMode.Write);

            StreamWriter sWriter = new StreamWriter(iStream);

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string jsonString = JsonConvert.SerializeObject(data, settings);

            sWriter.Write(jsonString);
            sWriter.Close();
            iStream.Close();
            dataStream.Close();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }
    }
}