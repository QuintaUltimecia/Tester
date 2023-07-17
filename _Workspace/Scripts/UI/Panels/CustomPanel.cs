using UnityEngine;

public abstract class CustomPanel : MonoBehaviour
{
    protected GameObject _gameObject;

    protected void Awake()
    {
        _gameObject = gameObject;
    }

    public void Hide()
    {
        if (_gameObject == null)
            _gameObject = gameObject;

        _gameObject.SetActive(false);
    }

    public void Show()
    {
        if (_gameObject == null)
            _gameObject = gameObject;

        _gameObject.SetActive(true);
    }
}
