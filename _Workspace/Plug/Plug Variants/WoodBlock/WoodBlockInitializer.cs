using UnityEngine;

namespace WoodBlock
{
    public class WoodBlockInitializer : BasePlug
    {
        [SerializeField]
        private HeadPanel _headPanelPrefab;

        private HeadPanel _headPanel;

        public override void Init(Canvas canvas)
        {
            _headPanel = Instantiate(_headPanelPrefab, canvas.transform);

            Subs();
        }

        public override void ShowPlug()
        {
            _headPanel.MenuPanel.Show();
            _headPanel.GamePanel.Hide();
            _headPanel.WinPanel.Hide();
        }

        private  void Subs()
        {
            _headPanel.MenuPanel.StartButton.OnClick.AddListener(() => StartGame());
            _headPanel.GamePanel.WoodBlockContainer.OnGameOver += () => _headPanel.WinPanel.Show();
            _headPanel.WinPanel.NextPanel.OnClick.AddListener(() => NextLevel());
        }

        private void StartGame()
        {
            _headPanel.MenuPanel.Hide();
            _headPanel.GamePanel.Show();
            _headPanel.WinPanel.Hide();
        }

        private void NextLevel()
        {
            StartGame();

            _headPanel.GamePanel.LevelManager.StartNextLevel();
        }
    }
}
