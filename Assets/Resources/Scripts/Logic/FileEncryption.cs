using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

public class FileEncryption
{
    private static FileStream dataStream;
    private static byte[] savedKey = { 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15, 0x16, 0x15 };

    public static T ReadFile<T>(string path)
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

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            return JsonConvert.DeserializeObject<T>(text, settings);
        }
        return default(T);
    }

    public static void WriteFile(string path, object data)
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
}