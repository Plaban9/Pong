namespace Managers
{
    using Data.PersistData;

    using Interactables.Ball;
    using Interactables.Paddle;

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
        public PowerUpManager PowerUpManager { get; private set; }
        public ControllerManager ControllerManager { get; private set; }
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

        public GameObject BallGameobjectReference { get; internal set; }

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
            PowerUpManager = GetComponentInChildren<PowerUpManager>();
            ControllerManager = GetComponentInChildren<ControllerManager>();

            for (int i = 0; i < playerSize; i++)
            {
                PlayerManager.InstantiatePlayer(i);
            }

            PlayerManager.InstantiateBall();
            ScoreManager.Initialize(playerSize);
            try
            {
                ControllerManager.Initialize();
            }
            catch (System.Exception ignored)
            {

            }
            

            IsArenaReady = true;
            IsInRally = false;
        }

        public void Start()
        {
            DecorationManager.OnInitialized();
            PowerUpManager.OnInitialized(DecorationManager.GetDecorationConfiguration());
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
            PowerUpManager.OnReset(DecorationManager.GetDecorationConfiguration());
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

        public void OnPowerUpCollected(PaddlePowerup paddlePowerup, GameObject powerUpObject)
        {
            PowerUpManager.OnPowerUpCollected(powerUpObject);
            PlayerManager.ApplyPowerup(BallGameobjectReference.GetComponent<Ball>().GetLastPlayerIndex(), paddlePowerup);
        }

        public void OnControllerConnected(int controllerCount)
        {
            PlayerManager.SetControllers(controllerCount);
        }

        public void OnControllerDisconnected(int controllerCount)
        {
            PlayerManager.SetControllers(controllerCount);
        }

        #region TEST
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    PlayerManager.ApplyPowerup(BallGameobjectReference.GetComponent<Ball>().GetLastPlayerIndex(), Interactables.Paddle.PaddlePowerup.FREEZE);
            //}
            //else if (Input.GetKeyDown(KeyCode.T))
            //{
            //    PlayerManager.ApplyPowerup(BallGameobjectReference.GetComponent<Ball>().GetLastPlayerIndex(), Interactables.Paddle.PaddlePowerup.TURBO);
            //}
        }
        #endregion
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

        public class Environment
        {
            public const string MOVEABLE_OBSTACBLE = "MoveableObstacle";
            public const string STATIC_OBSTACBLE = "StaticObstacle";
        }

        public class PowerupConstants
        {
            public const float FREEZE_TIMER = 2f;
            public const float TURBO_TIMER = 5f;

            #region Animation
            public const string ENTRY_ANIMATION = "entry";
            #endregion
        }
    }
}