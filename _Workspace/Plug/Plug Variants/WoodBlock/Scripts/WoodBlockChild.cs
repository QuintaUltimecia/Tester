using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WoodBlock
{
    public class WoodBlockChild : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private WoodBlockItem _woodBlockItem;

        private RectTransform _rectTransform;
        private Camera _camera;
        private Image _image;

        private EventSystem _eventSystem;
        private PointerEventData _pointerEventData;
        private GraphicRaycaster _graphicRaycaster;
        private Canvas _canvas;

        private WoodBlockSlot _lastContainer;
        private Vector2 _positionInParent;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
            _eventSystem = EventSystem.current;
            _pointerEventData = new PointerEventData(_eventSystem);
            _canvas = FindObjectOfType<Canvas>();
            _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
            _image = GetComponent<Image>();
            _positionInParent = _rectTransform.localPosition;
        }

        private T GetGraphicRaycastItem<T>()
        {
            Vector2 touchPosition = new Vector2(_camera.WorldToScreenPoint(_rectTransform.position).x, _camera.WorldToScreenPoint(_rectTransform.position).y);
            _pointerEventData.position = touchPosition;

            List<RaycastResult> results = new List<RaycastResult>();

            _graphicRaycaster.Raycast(_pointerEventData, results);

            var item = results.Count == 0
                ? default
                : results[0].gameObject.GetComponent<T>();

            return item;
        }

        public void SetRaycastTarget(bool value)
        {
            _image.raycastTarget = value;
        }

        public void ResetLastContainer()
        {
            _lastContainer = null;
        }

        public void InitWoodBlockItem(WoodBlockItem woodBlockItem)
        {
            _woodBlockItem = woodBlockItem;
        }

        public void ReturnToLastSlot()
        {
            if (_lastContainer != null)
                _lastContainer.Fill(_rectTransform);
        }

        public void ReturnToParent()
        {
            if (_lastContainer != null)
                _lastContainer.ToEmpty(_rectTransform);

            _rectTransform.SetParent(_woodBlockItem.transform);
            _rectTransform.localPosition = _positionInParent;
        }

        public bool CheckFill(Vector2 touchPosition)
        {
            WoodBlockSlot container = GetGraphicRaycastItem<WoodBlockSlot>();

            if (container == null || container.IsFill == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void FillContainer(Vector2 touchPosition)
        {
            WoodBlockSlot container = GetGraphicRaycastItem<WoodBlockSlot>();

            if (container == null || container.IsFill == true)
            {
                _woodBlockItem.ReturnToLast();
            }
            else
            {
                container.Fill(_rectTransform);
                _lastContainer = container;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _woodBlockItem.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _woodBlockItem.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _woodBlockItem.OnEndDrag(eventData);
        }
    }
}
