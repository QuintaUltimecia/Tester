using UnityEngine;

namespace WoodBlock
{
    public class GamePanel : CustomPanel
    {
        [field: SerializeField]
        public WoodBlockContainer WoodBlockContainer { get; private set; }

        [field: SerializeField]
        public LevelManager LevelManager { get; private set; }
    }
}
