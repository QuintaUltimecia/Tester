using UnityEngine;

namespace WoodBlock
{
    public class MenuPanel : CustomPanel
    {
        [field: SerializeField]
        public StartButton StartButton { get; private set; }
    }
}
