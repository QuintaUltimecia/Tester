using UnityEngine;
using UnityEngine.EventSystems;

public class ResourcesLoader : MonoBehaviour
{
    private string _domainContainer = "Domain";

    private string _webviewShower = "WebviewShower";

    private string _webview = "Plugins/UniWebView";
    private string _oneSignal = "Plugins/OneSignal";

    private string _loader = "Loader";

    private string _mainCamera = "Main Camera";
    private string _canvas = "Canvas";
    private string _eventSystem = "EventSystem";

    private string _plugInitializer = "Plug Initializer";

    private EntryPoint _entryPoint;

    private void Awake()
    {
        Instantiate(Resources.Load<EventSystem>(_eventSystem));

        Canvas newCanvas;

        _entryPoint = new EntryPoint(
            canvas: newCanvas = Instantiate(Resources.Load<Canvas>(_canvas)),
            domainContainer: Instantiate(Resources.Load<DomainContainerSO>(_domainContainer)),
            webviewShower: Instantiate(Resources.Load<Network.WebviewShower>(_webviewShower)),
            webview: Instantiate(Resources.Load<UniWebViewController>(_webview)),
            oneSignalManager: Instantiate(Resources.Load<OneSignalManager>(_oneSignal)),
            loadAnimation: Instantiate(Resources.Load<LoadAnimation>(_loader), newCanvas.transform),
            plugInitializer: Instantiate(Resources.Load<PlugInitializer>(_plugInitializer)),
            camera: Instantiate(Resources.Load<Camera>(_mainCamera)));
    }

    private void Start()
    {
        _entryPoint.Start();
    }
}
