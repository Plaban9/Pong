namespace Managers
{
    using Data.PersistData;

    using Managers.Scene;
    //MASTER SINGLETON
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int playerSize = 2;

        public static GameManager Instance { get; private set; }
        public ScoreManager ScoreManager { get; private set; }
        public PlayerManager PlayerManager { get; private set; }
        public DecorationManager DecorationManager { get; private set; }
        public bool IsArenaReady { get; private set; } // When all required actors have instantiated
        public bool ShowBallTrail { get => showBallTrail; set => showBallTrail = value; }

        [SerializeField]
        private bool showBallTrail = true;

        private bool _isInRally;

        [SerializeField]
        private SceneManager _sceneManager;

        public bool IsInRally
        {
            get
            {
                return _isInRally;
            }

            set
            {
                _isInRally = value;

                for (int i = 0; i < playerSize; i++)
                {
                    PlayerManager.OnRallyStateChanged(i);
                }
            }
        } // When the ball is in play

        public GameObject BallGameobjectRefernce { get; internal set; }

        private void Awake()
        {
            PersistData.PlayerWonIndex = -1;

            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            PlayerManager = GetComponentInChildren<PlayerManager>();
            ScoreManager = GetComponentInChildren<ScoreManager>();
            DecorationManager = GetComponentInChildren<DecorationManager>();

            for (int i = 0; i < playerSize; i++)
            {
                PlayerManager.InstantiatePlayer(i);
            }

            PlayerManager.InstantiateBall();

            ScoreManager.Initialize(playerSize);

            DecorationManager.OnInitialized();

            IsArenaReady = true;
            IsInRally = false;
        }

        public void ResetGame(int playerIndex)
        {
            ScoreManager.UpdateScore(playerIndex);

            IsInRally = false;

            for (int i = 0; i < playerSize; i++)
            {
                PlayerManager.ResetGame(i);
            }

            PlayerManager.ResetBall();
            DecorationManager.OnReset(ScoreManager.GetLeadingPlayerIndex(), PlayerManager.GetPlayerConfiguration());
        }

        public void SetPlayerIndex(int playerIndex)
        {
            PlayerManager.SetPlayerTurnIndex(playerIndex);
        }

        public void PlayerWonNotification(int playerIndex)
        {
            PersistData.PlayerWonIndex = playerIndex;
            _sceneManager.LoadNextScene();
        }
    }

    public class GameplayConstants
    {
        public class Ball
        {
            public const string BALL_TAG = "Ball";
        }

        public class Paddle
        {
            public const string PADDLE_TAG = "Paddle";

            #region Animation
            public const string IDLE_ANIMATION = "isInRally";
            #endregion
        }

        public class Walls
        {
            public const string WALL_TAG = "Wall";
        }        
    }
}