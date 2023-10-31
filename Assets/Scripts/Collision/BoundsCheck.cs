namespace Collision
{
    using Effects.Camera;
    using Effects.Fade;

    using Managers;

    using UnityEngine;

    public class BoundsCheck : MonoBehaviour
    {
        [SerializeField]
        private BoundLocation boundLocation;

        [SerializeField]
        private Fade[] _fadeBoundsArray;

        [SerializeField]
        private AudioClip[] _hitClips;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayConstants.Ball.BALL_TAG))
            {
                int playerIndex = 0;

                switch (boundLocation)
                {
                    case BoundLocation.TOP:
                        playerIndex = 3;
                        break;
                    case BoundLocation.BOTTOM:
                        playerIndex = 4;
                        break;
                    case BoundLocation.LEFT:
                        playerIndex = 0;
                        break;
                    case BoundLocation.RIGHT:
                        playerIndex = 1;
                        break;
                    default:
                        break;
                }

                this.gameObject.GetComponent<CameraShake>().Shakecamera();

                if (_hitClips != null)
                {
                    AudioSource.PlayClipAtPoint(_hitClips[Random.Range(0, _hitClips.Length)], transform.position, 1f);
                }

                GameManager.Instance.SetPlayerIndex(playerIndex);
                GameManager.Instance.ResetGame(playerIndex);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayConstants.Ball.BALL_TAG))
            {
                switch (boundLocation)
                {
                    case BoundLocation.TOP:
                    case BoundLocation.BOTTOM:
                        Color initialColor = GameManager.Instance.DecorationManager.GetDecorationConfiguration().boundsAttributes.GetRandomColorFromGradient();
                        _fadeBoundsArray[collision.transform.position.x < 0f ? 0 : 1].StartFade(initialColor);

                        this.gameObject.GetComponent<CameraShake>().Shakecamera();
                        if (_hitClips != null)
                        {
                            AudioSource.PlayClipAtPoint(_hitClips[Random.Range(0, _hitClips.Length)], transform.position, 1f);
                        }
                        break;

                    case BoundLocation.LEFT:
                    case BoundLocation.RIGHT:
                    default:
                        break;
                }
            }
        }
    }
}
