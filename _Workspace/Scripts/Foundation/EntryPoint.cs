using UnityEngine;
using Network;

public class EntryPoint 
{
    private DomainContainerSO _domainContainer;

    private WebviewShower _webviewShower;
    private LoadAnimation _loadAnimation;

    private UniWebViewController _webview;
    private OneSignalManager _oneSignalManager;

    private ServerRequest _serverRequest;

    private PlugInitializer _plugInitializer;

    private Camera _camera;
    private Canvas _canvas;

    public EntryPoint(Canvas canvas, DomainContainerSO domainContainer, WebviewShower webviewShower, UniWebViewController webview, OneSignalManager oneSignalManager, LoadAnimation loadAnimation,
        PlugInitializer plugInitializer, Camera camera)
    {
        _serverRequest = new ServerRequest();

        _domainContainer = domainContainer;
        _webviewShower = webviewShower;
        _webview = webview;
        _oneSignalManager = oneSignalManager;
        _loadAnimation = loadAnimation;

        _plugInitializer = plugInitializer;

        _camera = camera;
        _canvas = canvas;

        Init();
    }

    public void Start()
    {
        SomethingDo som = new SomethingDo(); //dont

        Application.targetFrameRate = 60;

        _canvas.worldCamera = _camera;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenPlug();

            return;
        }

        _serverRequest.Init(() =>
        {
            _serverRequest.CreateRequest(_domainContainer.Domain, delegate ()
            {
                DataStore dataStore = new DataStore(_serverRequest.GetRequestResult());

                if (dataStore.ServerRequest == null || dataStore.ServerRequest.error == true)
                {
                    OpenPlug();
                }
                else
                {
                    OpenWebview(dataStore);
                }
            });
        });
    }

    private void Init()
    {
        _plugInitializer.Init(_canvas);
    }


    private void OpenWebview(DataStore dataStore)
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        _webview.Init(false, _canvas.GetComponent<RectTransform>());

        _webviewShower.Init(_webview, _oneSignalManager, _serverRequest);
        _webviewShower.OpenWebview(dataStore.ServerRequest);
    }

    private void OpenPlug()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _plugInitializer.ShowPlug();
    }
}
