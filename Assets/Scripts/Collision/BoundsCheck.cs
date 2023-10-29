namespace Collision
{
    using Interactables.Paddle;

    using Managers;

    using UnityEngine;

    public class BoundsCheck : MonoBehaviour
    {
        [SerializeField]
        private PaddlePositionType paddlePositionType;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayConstants.Ball.BALL_TAG))
            {
                switch (paddlePositionType)
                {
                    case PaddlePositionType.UP:
                        GameManager.Instance.SetPlayerIndex(3);
                        break;
                    case PaddlePositionType.DOWN:
                        GameManager.Instance.SetPlayerIndex(4);
                        break;
                    case PaddlePositionType.LEFT:
                        GameManager.Instance.SetPlayerIndex(0);
                        break;
                    case PaddlePositionType.RIGHT:
                        GameManager.Instance.SetPlayerIndex(1);
                        break;
                    default:
                        break;
                }

                GameManager.Instance.ResetGame();
            }
        }
    }
}
