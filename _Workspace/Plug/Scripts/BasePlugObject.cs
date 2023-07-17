using UnityEngine;

public abstract class BasePlugObject : MonoBehaviour
{
    protected GameObject _gameObject;

    protected virtual void Awake()
    {
        _gameObject = gameObject;
    }

    public void Show()
    {
        if (_gameObject == null)
            _gameObject = gameObject;

        _gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (_gameObject == null)
            _gameObject = gameObject;

        _gameObject.SetActive(false);
    }
}

