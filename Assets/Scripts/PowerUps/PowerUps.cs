namespace PowerUps
{
    using Configuration.DecorationConfiguration;

    using Interactables.Paddle;

    using Managers;

    using UnityEngine;

    public class PowerUps : MonoBehaviour
    {
        [SerializeField]
        private PaddlePowerup _paddlePowerUp;

        [SerializeField]
        private float _rotateMagnitude;

        private void Update()
        {
            transform.Rotate(0, 0, _rotateMagnitude * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time), transform.position.z);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayConstants.Ball.BALL_TAG))
            {
                GameManager.Instance.OnPowerUpCollected(_paddlePowerUp, gameObject);
            }
        }

        public void OnInitialized(PowerUpAttributes powerUpAttributes)
        {
            _rotateMagnitude = powerUpAttributes.rotateSpeed;
        }
    }
}