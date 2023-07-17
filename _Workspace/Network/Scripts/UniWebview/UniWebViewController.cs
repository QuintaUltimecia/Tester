using UnityEngine;

[RequireComponent(typeof(UniWebView))]
public class UniWebViewController : MonoBehaviour
{
    private UniWebView _webview;

    public void Init(bool backButtonEnabled, RectTransform rectTransform)
    {
        _webview.SetBackButtonEnabled(backButtonEnabled);
        _webview.ReferenceRectTransform = rectTransform;
    }

    public UniWebView GetWebview() =>
        _webview;

    private void Awake()
    {
        _webview = GetComponent<UniWebView>();
    }
}
