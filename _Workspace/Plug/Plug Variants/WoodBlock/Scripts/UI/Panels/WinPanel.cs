using UnityEngine;

namespace WoodBlock
{
    public class WinPanel : CustomPanel
    {
        [field: SerializeField]
        public NextButton NextPanel { get; private set; }
    }
}
