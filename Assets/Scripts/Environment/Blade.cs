namespace Environment.Obstacle.Movable
{
    using Managers;

    using UnityEngine;

    public class Blade : MonoBehaviour
    {
        [SerializeField]
        private RotateObstacle _rotateObstacle;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayConstants.Ball.BALL_TAG))
            {
                _rotateObstacle.OnHitByBall();
            }
        }
    }
}
