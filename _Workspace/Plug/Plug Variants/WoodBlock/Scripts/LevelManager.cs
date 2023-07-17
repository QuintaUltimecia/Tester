using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace WoodBlock
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private List<WoodBlockSaver> _levels;

        [SerializeField]
        private int _count = 0;

        public System.Action<int> OnUpdateLevel;

        private void Start()
        {
            StartLevelAt(_count);
        }

        private void StartLevelAt(int index)
        {
            foreach (var item in _levels)
            {
                item.ResetAllWoodBlock();
                item.Disable();
            }

            _levels[index].Enable();
        }

        public void StartNextLevel()
        {
            _count++;

            if (_count > transform.childCount - 1)
                _count = 0;

            StartLevelAt(_count);

            OnUpdateLevel?.Invoke(_count + 1);
        }
    }
}
