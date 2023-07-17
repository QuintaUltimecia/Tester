using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WoodBlock
{
    public class WoodBlockItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private WoodBlockChild[] _woodBlockChild;

        private RectTransform _rectTransform;
        private Camera _camera;
        private Image _image;

        private EventSystem _eventSystem;
        private PointerEventData _pointerEventData;
        private GraphicRaycaster _graphicRaycaster;
        private Canvas _canvas;

        private WoodBlockSlot _lastContainer;
        private WoodBlockSaver _woodBlockSaver;

        [SerializeField]
        private bool _isTrue = false;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
            _eventSystem = EventSystem.current;
            _canvas = FindObjectOfType<Canvas>();
            _pointerEventData = new PointerEventData(_eventSystem);
            _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
            _image = GetComponent<Image>();

            _woodBlockChild = new WoodBlockChild[_rectTransform.childCount];
            _woodBlockSaver = _rectTransform.parent.GetComponent<WoodBlockSaver>();

            for (int i = 0; i < _rectTransform.childCount; i++)
            {
                _woodBlockChild[i] = _rectTransform.GetChild(i).GetComponent<WoodBlockChild>();
                _woodBlockChild[i].InitWoodBlockItem(this);
            }
        }

        private T GetGraphicRaycastItem<T>(Vector2 touchPosition)
        {
            _pointerEventData.position = touchPosition;

            List<RaycastResult> results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(_pointerEventData, results);

            var item = results.Count == 0
                ? default
                : results[0].gameObject.GetComponent<T>();

            return item;
        }

        public void ReturnToBlockSaver(WoodBlockSaver saver)
        {
            if (_lastContainer != null)
            {
                _lastContainer.ToEmpty(_rectTransform);
                _lastContainer = null;
            }

            _rectTransform.SetParent(saver.transform);
            _rectTransform.anchoredPosition = Vector2.zero;

            if (_woodBlockChild.Length > 0)
            {
                for (int i = 0; i < _woodBlockChild.Length; i++)
                {
                    _woodBlockChild[i].ReturnToParent();
                    _woodBlockChild[i].ResetLastContainer();
                }
            }
        }

        public void ReturnToLast()
        {
            if (_lastContainer != null)
                _lastContainer.Fill(_rectTransform);
            else ReturnToBlockSaver(_woodBlockSaver);

            for (int i = 0; i < _woodBlockChild.Length; i++)
                _woodBlockChild[i].ReturnToLastSlot();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_lastContainer != null)
                _lastContainer.ToEmpty(_rectTransform);
            else _rectTransform.SetParent(_canvas.transform);

            if (_woodBlockChild.Length > 0)
            {
                for (int i = 0; i < _woodBlockChild.Length; i++)
                {
                    _woodBlockChild[i].ReturnToParent();
                    _woodBlockChild[i].SetRaycastTarget(false);
                }
            }

            _image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 touchPosition = Input.mousePosition;

            Vector2 position = new Vector2(_camera.pixelWidth / 2, _camera.pixelHeight / 2);
            Vector2 currentPosition = new Vector2(touchPosition.x - position.x, touchPosition.y - position.y);

            _rectTransform.anchoredPosition = currentPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 touchPosition = new Vector2(_camera.WorldToScreenPoint(_rectTransform.position).x, _camera.WorldToScreenPoint(_rectTransform.position).y);

            WoodBlockSlot container = GetGraphicRaycastItem<WoodBlockSlot>(touchPosition);

            if (container == null || container.IsFill == true)
            {
                WoodBlockSaver saver = GetGraphicRaycastItem<WoodBlockSaver>(touchPosition);

                if (saver != null)
                {
                    ReturnToBlockSaver(saver);
                }
                else
                {
                    ReturnToLast();
                }
            }
            else
            {
                _rectTransform.SetParent(container.transform);
                _rectTransform.anchoredPosition = Vector2.zero;

                bool isFill = true;

                for (int i = 0; i < _woodBlockChild.Length; i++)
                {
                    if (_woodBlockChild[i].CheckFill(touchPosition) == false)
                    {
                        isFill = false;
                        break;
                    }
                }

                if (isFill == true)
                {
                    for (int i = 0; i < _woodBlockChild.Length; i++)
                        _woodBlockChild[i].FillContainer(touchPosition);

                    container.Fill(_rectTransform);
                    _lastContainer = container;
                }
                else
                {
                    ReturnToLast();
                }
            }

            _image.raycastTarget = true;

            if (_woodBlockChild.Length > 0)
                for (int i = 0; i < _woodBlockChild.Length; i++)
                    _woodBlockChild[i].SetRaycastTarget(true);
        }
    }
}
