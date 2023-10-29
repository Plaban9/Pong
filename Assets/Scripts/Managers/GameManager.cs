namespace Managers
{
    //MASTER SINGLETON
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int playerSize = 2;

        public static GameManager Instance { get; private set; }
        public ScoreManager ScoreManager { get; private set; }
        public PlayerManager PlayerManager { get; private set; }
        public bool IsArenaReady { get; private set; }
        public bool ShowBallTrail { get => showBallTrail; set => showBallTrail = value; }

        [SerializeField]
        private bool showBallTrail = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            PlayerManager = GetComponentInChildren<PlayerManager>();
            ScoreManager = GetComponentInChildren<ScoreManager>();

            for (int i = 0; i < playerSize; i++)
            {
                PlayerManager.InstantiatePlayer(i);
            }

            PlayerManager.InstantiateBall();

            IsArenaReady = true;
        }

        public void ResetGame()
        {
            for (int i = 0; i < playerSize; i++)
            {
                PlayerManager.ResetGame(i);
            }
        }
    }
}