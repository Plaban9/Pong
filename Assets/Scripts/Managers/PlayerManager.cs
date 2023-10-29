namespace Managers
{
    using Agent;

    using Configuration.PlayerConfiguration;

    using Interactables.Ball;
    using Interactables.Paddle;

    using System.Collections.Generic;

    using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private Dictionary<int, Player> _playerIndexToPlayerDKV = new Dictionary<int, Player>();

        [SerializeField]
        private int _playerTurnIndex = 0;

        [SerializeField]
        private GameObject _interactablesParentObject;

        [SerializeField]
        private GameObject _paddleGameObject;

        [SerializeField]
        private GameObject _ballGameObject;

        [SerializeField]
        private PlayerConfiguration _playerConfiguration;

        public PlayerConfiguration GetPlayerConfiguration()
        {
            return _playerConfiguration;
        }

        public void InstantiatePlayer(int playerIndex)
        {
            _playerTurnIndex = 0;

            GameObject gameObject = Instantiate(_paddleGameObject);
            gameObject.transform.parent = _interactablesParentObject.transform;

            Player player = gameObject.GetComponent<Player>();
            player.OnInstantiated(playerIndex, _playerConfiguration.playerAttributes[playerIndex], playerIndex == 0);

            AddToPlayerDKV(playerIndex, player);
        }

        public void InstantiateBall()
        {
            GameObject gameObject = Instantiate(_ballGameObject);
            gameObject.transform.parent = _interactablesParentObject.transform;

            GameManager.Instance.BallGameobjectRefernce = gameObject;

            Ball ball = gameObject.GetComponent<Ball>();
            ball.paddle = _playerIndexToPlayerDKV[_playerTurnIndex].gameObject.GetComponent<Paddle>();

            ball.OnInstantiated(_playerConfiguration.ballAttributes);
        }

        public void ResetGame(int playerIndex)
        {
            _playerIndexToPlayerDKV[playerIndex].OnReset(playerIndex, _playerConfiguration.playerAttributes[playerIndex], playerIndex == _playerTurnIndex);
        }

        public void ResetBall()
        {
            Ball ball = GameManager.Instance.BallGameobjectRefernce.GetComponent<Ball>();
            ball.paddle = _playerIndexToPlayerDKV[_playerTurnIndex].gameObject.GetComponent<Paddle>();
            ball.OnReset();
        }

        private void AddToPlayerDKV(int playerIndex, Player player)
        {
            if (_playerIndexToPlayerDKV == null)
            {
                _playerIndexToPlayerDKV = new Dictionary<int, Player>();
            }

            _playerIndexToPlayerDKV.Add(playerIndex, player);
        }

        public void SetPlayerTurnIndex(int playerIndex)
        {
            _playerTurnIndex = playerIndex;
        }

        public void OnRallyStateChanged(int playerIndex)
        {
            _playerIndexToPlayerDKV[playerIndex].gameObject.GetComponent<Paddle>().OnRallyStateChanged();
        }
    }
}
