namespace Interactables.Ball
{
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

        public void OnReset()
        {

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
    }
}
