namespace Interactables.Paddle
{
    using Configuration.PlayerConfiguration;

    using Managers;

    using System.Collections;

    using UI.PowerUp;

    using UnityEngine;

    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        private string _paddleName;

        [SerializeField]
        private bool _isStarterPaddle;

        [SerializeField]
        private PaddleControlType _paddleControlType = PaddleControlType.HUMAN;

        [SerializeField]
        private Vector3 _initialPosition;

        [SerializeField]
        private float _magnitude;
        private float _initialMagnitude;


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

        [SerializeField]
        private PowerUpHandler _powerUpHandler;

        [SerializeField]
        private AudioClip _freezeClip;
        [SerializeField]
        private AudioClip _turboClip;

        [SerializeField]
        private GameObject _ballGameObject;

        [SerializeField]
        private PaddlePositionType _paddlePositionType;

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
            _initialMagnitude = _magnitude;
        }

        // Start is called before the first frame update
        void Start()
        {
            _initialPosition = this.transform.position;
        }

        private void FixedUpdate()
        {
            DoMovement();
        }

        private void DoMovement()
        {
            switch (_paddleControlType)
            {
                case PaddleControlType.HUMAN:
                    HumanMovement();
                    break;
                case PaddleControlType.AI:
                    if (GameManager.Instance.IsInRally)
                    {
                        AIMovement();
                    }
                    break;
            }
        }

        private void AIMovement()
        {
            if (_ballGameObject == null)
            {
                _ballGameObject = GameManager.Instance.BallGameobjectReference;
                return;
            }

            if (_ballGameObject.transform.position.x < this.transform.position.x)
            {
                this.transform.position = GetNextPosition(this.transform.position, PaddleMovementType.LEFT, _paddlePositionType);
            }
            else if (_ballGameObject.transform.position.x > this.transform.position.x)
            {
                this.transform.position = GetNextPosition(this.transform.position, PaddleMovementType.RIGHT, _paddlePositionType);
            }
        }

        private void HumanMovement()
        {
            if (Input.GetKey(_leftMovementKeyCode))
            {
                this.transform.position = GetNextPosition(this.transform.position, PaddleMovementType.LEFT, _paddlePositionType);
            }
            else if (Input.GetKey(_rightMovementKeyCode))
            {
                this.transform.position = GetNextPosition(this.transform.position, PaddleMovementType.RIGHT, _paddlePositionType);
            }
        }

        private Vector3 GetNextPosition(Vector3 currentPosition, PaddleMovementType paddleMovementType, PaddlePositionType paddlePositionType)
        {
            switch (paddleMovementType)
            {
                case PaddleMovementType.UP:
                    return currentPosition;

                case PaddleMovementType.DOWN:
                    return currentPosition;

                case PaddleMovementType.LEFT:
                    if (paddlePositionType == PaddlePositionType.LEFT || paddlePositionType == PaddlePositionType.RIGHT)
                    {
                        return new Vector3(currentPosition.x, GetMovementWithBoundY(currentPosition.y + (_magnitude * Time.deltaTime / 2f)), currentPosition.z);
                    }
                    else if (paddlePositionType == PaddlePositionType.UP || paddlePositionType == PaddlePositionType.DOWN)
                    {
                        return new Vector3(GetMovementWithBoundX(currentPosition.x - (_magnitude * Time.deltaTime / 2f)), currentPosition.y, currentPosition.z);
                    }
                    break;
                case PaddleMovementType.RIGHT:
                    if (paddlePositionType == PaddlePositionType.LEFT || paddlePositionType == PaddlePositionType.RIGHT)
                    {
                        return new Vector3(currentPosition.x, GetMovementWithBoundY(currentPosition.y - (_magnitude * Time.deltaTime / 2f)), currentPosition.z);
                    }
                    else if (paddlePositionType == PaddlePositionType.UP || paddlePositionType == PaddlePositionType.DOWN)
                    {
                        return new Vector3(GetMovementWithBoundX(currentPosition.x + (_magnitude * Time.deltaTime / 2f)), currentPosition.y, currentPosition.z);
                    }
                    break;
            }

            return currentPosition;
        }

        private float GetMovementWithBoundX(float nextPositionX)
        {
            if (_minimumBounds != null && _maximumBounds != null)
            {
                if (!(_minimumBounds.x == _maximumBounds.x))
                {
                    return Mathf.Clamp(nextPositionX, _minimumBounds.x, _maximumBounds.x);
                }
            }

            return nextPositionX;
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

        public void OnInstantiated(int playerIndex, string name, bool isStarterPaddle, PlayerAttributes playerAttributes, PowerUpHandler powerUpHandler)
        {
            this.transform.localScale = playerAttributes.paddleTransform.playerScale;
            this.transform.localRotation = Quaternion.Euler(playerAttributes.paddleTransform.playerRotation);

            _initialPosition = playerAttributes.paddleTransform.playerPosition;
            this.transform.position = _initialPosition;

            this._magnitude = playerAttributes.magnitude;

            _minimumBounds = playerAttributes.playSpaceBounds.lowerBound;
            _maximumBounds = playerAttributes.playSpaceBounds.upperBound;

            this.name = this._paddleName = name;

            this._powerUpHandler = powerUpHandler;

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

            _paddlePositionType = playerAttributes.paddlePositionType;

            OnRallyStateChanged();
        }

        public PaddlePositionType GetPaddlePositionType()
        {
            return _paddlePositionType;
        }

        public void SetPaddleControlType(PaddleControlType paddleControlType)
        {
            _paddleControlType = paddleControlType;
        }

        public void OnReset(int playerIndex, bool isStarterPaddle)
        {
            _magnitude = _initialMagnitude;

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

        public PaddleControlType GetPaddleControlType()
        {
            return _paddleControlType;
        }

        public void ApplyPowerup(PaddlePowerup paddlePowerup)
        {
            switch (paddlePowerup)
            {
                case PaddlePowerup.FREEZE:
                    AudioSource.PlayClipAtPoint(_freezeClip, transform.position, 1f);
                    StartCoroutine(nameof(ApplyFreeze));
                    break;
                case PaddlePowerup.TURBO:
                    AudioSource.PlayClipAtPoint(_turboClip, transform.position, 1f);
                    StartCoroutine(nameof(ApplyTurbo));
                    break;
            }
        }

        private IEnumerator ApplyFreeze()
        {
            _powerUpHandler.ShowPowerUpAnimation("FREEZE", GameManager.Instance.DecorationManager.GetDecorationConfiguration().powerUpAttributes.freezeColor);

            float currentMagnitude = _magnitude;
            _magnitude = 0f;

            yield return new WaitForSeconds(GameplayConstants.PowerupConstants.FREEZE_TIMER);
            _magnitude = currentMagnitude;
        }

        private IEnumerator ApplyTurbo()
        {
            _powerUpHandler.ShowPowerUpAnimation("TURBO", GameManager.Instance.DecorationManager.GetDecorationConfiguration().powerUpAttributes.turboColor);

            float currentMagnitude = _magnitude;
            _magnitude *= 2;

            yield return new WaitForSeconds(GameplayConstants.PowerupConstants.TURBO_TIMER);
            _magnitude = currentMagnitude;
        }
    }
}