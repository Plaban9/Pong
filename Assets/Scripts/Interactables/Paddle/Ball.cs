namespace Interactables.Ball
{
    using Agent;

    using Configuration.PlayerConfiguration;

    using Effects.Ghosting;

    using Interactables.Paddle;

    using Managers;

    using UnityEngine;

    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private float _ghostEffectTime;
        [SerializeField]
        private float _ghostEffectSpeed;
        [SerializeField]
        private float _distanceBetweenGhostFrames = 0.1f;
        [SerializeField]
        private float _ghostEffectCooldown;

        private float _ghostEffectTimeLeft;
        private Vector2 _lastGhostEffectFramePosition = Vector2.zero;
        private float _lastGhostEffect = -100f;

        public Paddle paddle { get; set; }
        [SerializeField]
        private Vector2 magnitude = new Vector2(10f, 2.5f);

        [SerializeField]
        private GameObject _particleSystemPrefab;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private AudioClip[] _ballLaunchClips;

        [SerializeField]
        private int _lastPlayerIndex = 0;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.Instance.IsInRally)
            {
                MoveWithPaddle();
            }

            // Wait for a ball release
            if (paddle != null && Input.GetKeyDown(paddle.GetBallReleaseKeyCode()) && !GameManager.Instance.IsInRally)
            {
                ShootBall();
                GameManager.Instance.IsInRally = true;
            }

            if (GameManager.Instance.IsInRally && GameManager.Instance.ShowBallTrail)
            {
                ShowGhostingEffect();
            }
        }

        void ShootBall()
        {
            // Shoot the ball relative to screen width center w.r.t ball position
            if (this.transform.position.y > 0)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude.x, -1 * magnitude.y);
            }
            else
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude.x, magnitude.y);
            }

            if (_ballLaunchClips != null)
            {
                AudioSource.PlayClipAtPoint(_ballLaunchClips[Random.Range(0, _ballLaunchClips.Length)], transform.position, 1f);
            }
        }

        void MoveWithPaddle()
        {
            this.transform.position = paddle.GetBallPlaceholder().transform.position;
        }

        public void OnInstantiated(BallAttributes ballAttributes)
        {
            ApplyColour(ballAttributes.ballColour);
            this.transform.localScale = ballAttributes.ballScale;
            magnitude = ballAttributes.magnitude;
        }

        public void OnReset(int lastPlayer)
        {
            _lastPlayerIndex = lastPlayer;
        }

        private void ApplyColour(Color colour)
        {
            //Color color;

            //if (ColorUtility.TryParseHtmlString(colour, out color))
            //{
            //    this.GetComponent<SpriteRenderer>().color = color;
            //}
            this.GetComponent<SpriteRenderer>().color = colour;
        }

        private void ShowGhostingEffect()
        {
            if (Mathf.Abs(Vector2.Distance(_lastGhostEffectFramePosition, transform.position)) > _distanceBetweenGhostFrames)
            {
                GhostingEffectPool.Instance.ReclaimObjectFromPool();
                _lastGhostEffectFramePosition = transform.position;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject gameObject = collision.gameObject;

            if (gameObject.CompareTag(GameplayConstants.Walls.WALL_TAG) || gameObject.CompareTag(GameplayConstants.Paddle.PADDLE_TAG) || gameObject.CompareTag(GameplayConstants.Environment.MOVEABLE_OBSTACBLE) || gameObject.CompareTag(GameplayConstants.Environment.STATIC_OBSTACBLE))
            {
                if (GameManager.Instance.IsInRally)
                {
                    Instantiate(_particleSystemPrefab, this.transform.position, Quaternion.identity);
                    _spriteRenderer.color = GameManager.Instance.DecorationManager.GetDecorationConfiguration().boundsAttributes.GetRandomColorFromGradient();
                }

                if (gameObject.CompareTag(GameplayConstants.Paddle.PADDLE_TAG))
                {
                    _lastPlayerIndex = gameObject.GetComponent<Player>().PlayerIndex;
                }
            }
        }

        public int GetLastPlayerIndex()
        {
            return _lastPlayerIndex;
        }
    }
}
