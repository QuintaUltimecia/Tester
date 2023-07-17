using UnityEngine;
using TMPro;

namespace WoodBlock
{
    public class WinsCounter : MonoBehaviour
    {
        [SerializeField]
        private int _count;

        [SerializeField]
        private LevelManager _levelManager;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _levelManager.OnUpdateLevel += (int value) => UpdateText(value);
        }

        private void UpdateText(int number)
        {
            _text.text = $"lvl {number}";
        }
    }
}
