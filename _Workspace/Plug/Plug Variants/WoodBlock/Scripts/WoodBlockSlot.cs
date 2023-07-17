using UnityEngine;
using UnityEngine.UI;

namespace WoodBlock
{
    public class WoodBlockSlot : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsFill { get; private set; } = false;

        private WoodBlockContainer _woodBlockContainer;

        private Canvas _canvas;
        private Transform _transform;
        private Image _image;

        private Vector4 _defaultColor;

        private void Awake()
        {
            _transform = transform;
            _canvas = FindObjectOfType<Canvas>();
            _image = GetComponent<Image>();

            _defaultColor = _image.color;
        }

        public void InitWoodBlockContainer(WoodBlockContainer woodBlockContainer)
        {
            _woodBlockContainer = woodBlockContainer;
        }

        public void Fill(Transform transform)
        {
            if (IsFill == false)
            {
                transform.SetParent(_transform);
                transform.localPosition = Vector2.zero;
                IsFill = true;
                _image.color = Color.black;
            }

            _woodBlockContainer.CalculateTrueCount();
        }

        public void ToEmpty(Transform transform)
        {
            if (IsFill == true)
            {
                transform.SetParent(_canvas.transform);
                IsFill = false;
                _image.color = _defaultColor;
            }
        }
    }
}
