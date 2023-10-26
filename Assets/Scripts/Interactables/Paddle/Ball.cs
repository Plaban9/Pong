namespace Interactables.Ball
{
    using Interactables.Paddle;

    using Unity.VisualScripting;

    using UnityEngine;

    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private Paddle paddle;

        private bool _hasStarted = false;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_hasStarted)
            {
                MoveWithPaddle();
            }            

            // Wait for a mouse left click to launch
            if (Input.GetMouseButtonDown(0))
            {
                ShootBall();
                _hasStarted = true;
            }
        }

        void ShootBall()
        {

            // Shoot the ball relative to screen width center w.r.t ball position
            if (this.transform.position.y < Screen.height / 2f)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, -2.5f);
            }

            else
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 2.5f);
            }
        }

        void MoveWithPaddle()
        {
            this.transform.position = paddle.GetBallPlaceholder().transform.position;
        }
    }
}
