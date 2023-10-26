namespace Interactables.Paddle
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        private string _paddleName;

        [SerializeField]
        private bool _isStarterPaddle;


        [SerializeField]
        private Vector3 _paddlePosition;

        [SerializeField]
        private float _magnitude;


        [SerializeField]
        private KeyCode _leftMovementKeyCode;

        [SerializeField]
        private KeyCode _rightMovementKeyCode;


        [SerializeField]
        private Vector3 _minimumBounds;

        [SerializeField]
        private Vector3 _maximumBounds;


        [SerializeField]
        private GameObject _ballPlaceholder; // For Reset


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
        }

        // Start is called before the first frame update
        void Start()
        {
            _paddlePosition = this.transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (Input.GetKey(_leftMovementKeyCode))
            {
                _paddlePosition = GetNextPosition(this.transform.position, PaddleMovementType.LEFT);
            }
            else if (Input.GetKey(_rightMovementKeyCode))
            {
                _paddlePosition = GetNextPosition(this.transform.position, PaddleMovementType.RIGHT);
            }

            this.transform.position = _paddlePosition;
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

        #endregion
    }
}