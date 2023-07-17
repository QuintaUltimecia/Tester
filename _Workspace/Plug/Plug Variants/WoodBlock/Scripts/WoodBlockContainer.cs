using UnityEngine;

namespace WoodBlock
{
    public class WoodBlockContainer : MonoBehaviour
    {
        public System.Action OnGameOver;

        [SerializeField]
        private int _maxCount;

        [SerializeField]
        private int _trueCount = 0;

        private WoodBlockSlot[] _woodBlockSlot;

        protected void Awake()
        {
            _woodBlockSlot = new WoodBlockSlot[transform.childCount];
            _maxCount = transform.childCount;

            for (int i = 0; i < transform.childCount; i++)
            {
                _woodBlockSlot[i] = transform.GetChild(i).GetComponent<WoodBlockSlot>();
                _woodBlockSlot[i].InitWoodBlockContainer(this);
            }
        }

        public void CalculateTrueCount()
        {
            int count = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (_woodBlockSlot[i].IsFill == true)
                    count++;
            }

            _trueCount = count;

            if (_trueCount == transform.childCount)
            {
                OnGameOver?.Invoke();
            }
        }
    }
}
