using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField]
    protected float _offset = 10;

    protected RectTransform _rectTransform;
    protected Vector3 _startPosition;

    public UnityEvent OnClick;

    protected void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = new Vector3(
            x: _rectTransform.anchoredPosition.x,
            y: _rectTransform.anchoredPosition.y - _offset);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _startPosition;
    }
}
