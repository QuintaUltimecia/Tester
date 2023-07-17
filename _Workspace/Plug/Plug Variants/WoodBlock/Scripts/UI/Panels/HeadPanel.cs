using UnityEngine;

namespace WoodBlock
{
    public class HeadPanel : CustomPanel
    {
        [field: SerializeField]
        public MenuPanel MenuPanel { get; private set; }

        [field: SerializeField]
        public GamePanel GamePanel { get; private set; }

        [field: SerializeField]
        public WinPanel WinPanel { get; private set; }
    }
}

