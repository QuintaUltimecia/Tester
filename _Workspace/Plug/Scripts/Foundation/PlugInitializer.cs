using UnityEngine;

public class PlugInitializer : MonoBehaviour
{
    [SerializeField]
    private BasePlug _plug;

    private Canvas _canvas;

    public void Init(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void ShowPlug()
    {
        _plug.Init(_canvas);
        _plug.ShowPlug();
    }
}
