using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using System.Text;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneLoader : MonoBehaviour
{
    private string id = "upload";
    private string password = "123456";

    public string nextSceneAddress;
    public List<string> addressToLoad = new List<string>();

    public class CustomCerificationHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    private void Start()
    {
        Addressables.WebRequestOverride = ModfyWebRequest;
        //Addressables.LoadSceneAsync(nextSceneAddress);

        StartCoroutine(CoDownloadAllAssets());
    }

    private void ModfyWebRequest(UnityWebRequest request)
    {
        string auth = $"{id}:{password}";
        string authBase64 = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));
        request.SetRequestHeader("Authorization", "Basic " + authBase64);
        request.certificateHandler = new CustomCerificationHandler();
    }

    IEnumerator CoDownloadAllAssets()
    {
        var sizeHandlers = new List<AsyncOperationHandle<long>>();
        long totalSize = 0;

        foreach(var address in addressToLoad)
        {
            var sizeHandle = Addressables.GetDownloadSizeAsync(address);
            sizeHandlers.Add(sizeHandle);
            yield return sizeHandle;
        }

        foreach(var handler in sizeHandlers)
        {
            totalSize += handler.Result;
            Addressables.Release(handler);
        }

        Debug.Log($"Total DownloadSize {totalSize / (1024 * 1024)} MB");

        foreach (var address in addressToLoad)
        {
            var downloadHandle = Addressables.DownloadDependenciesAsync(address);

            while(!downloadHandle.IsDone)
            {
                Debug.Log($"Download Percent : {downloadHandle.PercentComplete * 100f}%");
                yield return null;
            }
            Addressables.Release(downloadHandle);
        }

        Debug.Log("Complete Download");

        Addressables.LoadSceneAsync(nextSceneAddress);
        //yield return null;
    }
}
