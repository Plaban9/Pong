namespace Interactables.Paddle
{
    using Configuration.PlayerConfiguration;

    using Managers;

    using UnityEngine;

    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        private string _paddleName;

        [SerializeField]
        private bool _isStarterPaddle;


        [SerializeField]
        private Vector3 _initialPosition;

        [SerializeField]
        private float _magnitude;


        [SerializeField]
        private KeyCode _leftMovementKeyCode;

        [SerializeField]
        private KeyCode _rightMovementKeyCode;

        [SerializeField]
        private KeyCode _ballReleaseKeyCode;


        [SerializeField]
        private Vector3 _minimumBounds;

        [SerializeField]
        private Vector3 _maximumBounds;


        [SerializeField]
        private GameObject _ballPlaceholder; // For Reset

        [SerializeField]
        private Animator _paddleAnimation;

        [SerializeField]
        private ParticleSystem _ballHitParticle;

        [SerializeField]
        private AudioClip[] _ballHitClips;

        public void SetPaddleName(string name)
        {
            _paddleName = name;
        }

        public string GetPaddleName()
        {
            return _paddleName;
        }

        public bool IsStarterPaddle()
        {
            return _isStarterPaddle;
        }

        private void Awake()
        {
            _paddleAnimation = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _initialPosition = this.transform.position;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(_leftMovementKeyCode))
            {
                this.transform.position = GetNextPosition(this.transform.position, PaddleMovementType.LEFT);
            }
            else if (Input.GetKey(_rightMovementKeyCode))
            {
                this.transform.position = GetNextPosition(this.transform.position, PaddleMovementType.RIGHT);
            }
        }

        private Vector3 GetNextPosition(Vector3 currentPosition, PaddleMovementType paddleMovementType)
        {
            switch (paddleMovementType)
            {
                case PaddleMovementType.UP:
                    return currentPosition;

                case PaddleMovementType.DOWN:
                    return currentPosition;

                case PaddleMovementType.LEFT:
                    return new Vector3(currentPosition.x, GetMovementWithBoundY(currentPosition.y + (_magnitude * Time.deltaTime / 2f)), currentPosition.z);

                case PaddleMovementType.RIGHT:
                    return new Vector3(currentPosition.x, GetMovementWithBoundY(currentPosition.y - (_magnitude * Time.deltaTime / 2f)), currentPosition.z);
            }

            return currentPosition;
        }

        private float GetMovementWithBoundY(float nextPositionY)
        {
            if (_minimumBounds != null && _maximumBounds != null)
            {
                if (!(_minimumBounds.y == _maximumBounds.y))
                {
                    return Mathf.Clamp(nextPositionY, _minimumBounds.y, _maximumBounds.y);
                }
            }

            return nextPositionY;
        }

        #region GETTERS
        public GameObject GetBallPlaceholder()
        {
            return _ballPlaceholder;
        }

        public KeyCode GetBallReleaseKeyCode()
        {
            return _ballReleaseKeyCode;
        }
        #endregion

        public void OnInstantiated(int playerIndex, string name, bool isStarterPaddle, PlayerAttributes playerAttributes)
        {
            this.transform.localScale = playerAttributes.paddleTransform.playerScale;
            this.transform.localRotation = Quaternion.Euler(playerAttributes.paddleTransform.playerRotation);

            _initialPosition = playerAttributes.paddleTransform.playerPosition;
            this.transform.position = _initialPosition;

            this._magnitude = playerAttributes.magnitude;

            _minimumBounds = playerAttributes.playSpaceBounds.lowerBound;
            _maximumBounds = playerAttributes.playSpaceBounds.upperBound;

            this.name = this._paddleName = name;

            this._leftMovementKeyCode = playerAttributes.playerInput.leftMovementKeyCode;
            this._rightMovementKeyCode = playerAttributes.playerInput.rightMovementKeyCode;
            this._ballReleaseKeyCode = playerAttributes.playerInput.ballReleaseKeyCode;

            this._isStarterPaddle = isStarterPaddle;

            var main = _ballHitParticle.main;
            main.startColor = playerAttributes.playerColour;

            if (playerAttributes.paddlePositionType == PaddlePositionType.UP || playerAttributes.paddlePositionType == PaddlePositionType.DOWN)
            {
                main.startRotation = 0;
            }

            OnRallyStateChanged();
        }

        public void OnReset(int playerIndex, bool isStarterPaddle)
        {
            PaddleTransforms paddleTransforms = GameManager.Instance.PlayerManager.GetPlayerConfiguration().playerAttributes[playerIndex].paddleTransform;

            this.transform.localScale = paddleTransforms.playerScale;
            this.transform.localRotation = Quaternion.Euler(paddleTransforms.playerRotation);

            _initialPosition = paddleTransforms.playerPosition;
            this.transform.position = _initialPosition;

            this._isStarterPaddle = isStarterPaddle;

            OnRallyStateChanged();
        }

        public void OnRallyStateChanged()
        {
            _paddleAnimation.SetBool(GameplayConstants.Paddle.IDLE_ANIMATION, !GameManager.Instance.IsInRally);

            //if (GameManager.Instance.IsInRally)
            //{
            //    SpriteRenderer spriteRenderer = this.transform.GetComponent<SpriteRenderer>();

            //    this.transform.GetComponent<SpriteRenderer>().color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            //}
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (GameManager.Instance.IsInRally && collision.gameObject.CompareTag(GameplayConstants.Ball.BALL_TAG))
            {
                _ballHitParticle.Play();

                if (_ballHitClips != null)
                {
                    AudioSource.PlayClipAtPoint(_ballHitClips[Random.Range(0, _ballHitClips.Length)], transform.position, 1f);
                }
            }
        }
    }
}