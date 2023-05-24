using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloaderFile : Singleton<DownloaderFile>
{
    [SerializeField] private LinkParameters linkParameters;

    public bool IsDone { get; private set; }

    public void GetFile(string url)
    {
        StartCoroutine(ProcessRequest(url));
    }

    public IEnumerator ProcessRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                GameException.Instance.ShowError("Ошибка, попробуйте еще раз!");
                IsDone = false;
            }
            else
            {
                try
                {
                    string reqFileName = request.GetResponseHeader("content-disposition");
                    string fileName = reqFileName.Remove(0, reqFileName.IndexOf("\"")).Replace("\"", "");
                    byte[] bytes = request.downloadHandler.data;
#if UNITY_EDITOR
                    string path = Application.streamingAssetsPath + linkParameters.FolderPath + "/";
#elif UNITY_ANDROID
                string path = Application.persistentDataPath + linkParameters.FolderPath + "/";
#endif
                    try
                    {
                        FileOperations.WriteByteFile(path + fileName, bytes);
                        IsDone = true;
                    }
                    catch
                    {
                        GameException.Instance.ShowError("Ошибка, попробуйте еще раз!" + path + fileName);
                    }
                }
                catch
                {
                    GameException.Instance.ShowError("Не верная ссылка!");
                    IsDone = false;
                }
            }
        }
    }
}
