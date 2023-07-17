using UnityEngine;
using UnityEngine.Networking;
using Ugi.PlayInstallReferrerPlugin;
using System.Globalization;

public class ServerRequest
{
    public string InstallReferrer { get; private set; }
    public delegate void WebRequestCompletedHandler();
    public WebRequestCompletedHandler OnWebRequestCompleted;

    private string _requestHeader = "X-Requested-With";

    [SerializeField]
    private UnityWebRequest _webRequest;
    private ClientData _clientData;

    private delegate void InstallReferrerHandler();

    public ClientData GetClient() =>
        _clientData;

    public string GetRequestResult()
    {
        if (_webRequest == null)
            return null;

        return Base64Convertor.ReturnFromBase64(_webRequest.downloadHandler.text);
    }

    public void Init(System.Action externalCallBack)
    {
        GetAndroidAdvertiserId((string advertisingId) => 
        {
            CreateClientData(advertisingId);
            GetInstallReferrer(externalCallBack);
        });
    }

    public void GetInstallReferrer(System.Action callBack)
    {
        PlayInstallReferrer.GetInstallReferrerInfo((installReferrerDetails) =>
        {
            if (installReferrerDetails.InstallReferrer != null)
            {
                InstallReferrer = $"{installReferrerDetails.InstallReferrer}&it={installReferrerDetails.InstallBeginTimestampSeconds}";

                callBack?.Invoke();

                Debug.Log("Get install referrer");
            }
            else 
            {
                InstallReferrer = "null";

                callBack?.Invoke();

                Debug.Log("Get install referrer");
            }
        });
    }

    private void CreateClientData(string advertisingId)
    {
        _clientData = new ClientData(
            clientID: SystemInfo.deviceUniqueIdentifier,
            installerName: GetInstallerName(),
            carrierCode: CultureInfo.CurrentCulture.Parent.ToString(),
            appDeviceType: SystemInfo.deviceType.ToString(),
            appLocale: CultureInfo.CurrentCulture.Name,
            appDeviceName: SystemInfo.deviceName,
            appClientID: SystemInfo.deviceUniqueIdentifier,
            appAdvertisingID: advertisingId,
            package: Application.identifier);

        Debug.Log("Create client data");
    }

    private string GetInstallerName()
    {
        string installerName;

#if UNITY_EDITOR
        installerName = "com.android.vending";
#elif PLATFORM_ANDROID
        installerName = Application.installerName;
#endif

        return installerName;
    }

    private void GetAndroidAdvertiserId(System.Action<string> callBack)
    {
        string advertisingID = "null";

        MiniIT.Utils.AdvertisingIdFetcher.RequestAdvertisingId(advertisingId =>
        {
            advertisingID = advertisingId;

            callBack?.Invoke(advertisingID);
        });
    }

    public async void CreateRequest(string url, System.Action onComplete)
    {
        Debug.Log("Create Request");

        var clientJson = JsonUtility.ToJson(_clientData);
        var clientBase64 = Base64Convertor.ReturnInBase64(clientJson);

        var uwr = new UnityWebRequest(url + clientBase64, "GET");
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader(_requestHeader, _clientData.package);

        Debug.Log("Wait send request...");

        await uwr.SendWebRequest();

        Debug.Log($"Client: {clientJson}");

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Connectiond Error");

            _webRequest = null;
        }
        else
        {
            _webRequest = uwr;

            Debug.Log("Connectiond Success");
        }

        onComplete?.Invoke();

        uwr.Dispose();
    }
}
