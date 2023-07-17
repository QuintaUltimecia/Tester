using UnityEngine;

namespace WoodBlock
{
    public class WoodBlockSaver : MonoBehaviour
    {
        private WoodBlockItem[] _woodBlockItem;

        private void Awake()
        {
            _woodBlockItem = new WoodBlockItem[transform.childCount];

            for (int i = 0; i < _woodBlockItem.Length; i++)
            {
                _woodBlockItem[i] = transform.GetChild(i).GetComponent<WoodBlockItem>();
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void ResetAllWoodBlock()
        {
            if (_woodBlockItem == null)
                return;

            for (int i = 0; i < _woodBlockItem.Length; i++)
            {
                _woodBlockItem[i].ReturnToBlockSaver(this);
            }
        }
    }
}
