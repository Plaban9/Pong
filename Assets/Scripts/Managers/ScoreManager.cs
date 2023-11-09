namespace Managers
{
    using System.Collections;
    using System.Collections.Generic;

    using UI;

    using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private Dictionary<int, int> _playerIndexToScoreDKV = new Dictionary<int, int>();

        [SerializeField]
        private ScoreHandler _scoreHandler;

        [SerializeField]
        private int _scoreToWin = 10;

        public void Initialize(int playerSize)
        {
            int initialScore = playerSize == 2 ? 0 : 9;

            for (int i = 0; i < playerSize; i++)
            {
                _playerIndexToScoreDKV.Add(i, initialScore);
            }

            _scoreHandler.OnInitialize(GameManager.Instance.PlayerManager.GetPlayerConfiguration(), playerSize, initialScore);
        }

        public void UpdateScore(int playerIndex)
        {
            int actualPlayerIndex;

            if (_playerIndexToScoreDKV.Count == 2)
            {
                actualPlayerIndex = (playerIndex + 1) % 2;
                _playerIndexToScoreDKV[actualPlayerIndex]++;
            }
            else
            {
                actualPlayerIndex = playerIndex;
                _playerIndexToScoreDKV[actualPlayerIndex]--;
            }


            GameManager.Instance.PlayerManager.UpdateScore(actualPlayerIndex, _playerIndexToScoreDKV[actualPlayerIndex]);
            _scoreHandler.OnScoreUpdate(actualPlayerIndex, _playerIndexToScoreDKV[actualPlayerIndex]);

            EvaluateWinCondition();
        }

        private void EvaluateWinCondition()
        {
            if (_playerIndexToScoreDKV.Count == 2)
            {
               EvaluateWinConditionForTwoPlayers();
            }
            else
            {
                EvaluateWinConditionForFourPlayers();
            }
        }

        private void EvaluateWinConditionForTwoPlayers()
        {
            int playerWonIndex = -1;

            for (int i = 0; i < _playerIndexToScoreDKV.Count; i++)
            {
                if (_playerIndexToScoreDKV[i] >= _scoreToWin)
                {
                    playerWonIndex = i;
                    break;
                }
            }

            if (playerWonIndex != -1)
            {
                GameManager.Instance.PlayerWonNotification(playerWonIndex);
            }
        }

        // Sudden Death
        // A single player with the highest score
        private void EvaluateWinConditionForFourPlayers()
        {
            int playerWonIndex = -1;
            int highestScore = -10;
            int playersWithHighestScore = 0;

            for (int i = 0; i < _playerIndexToScoreDKV.Count; i++)
            {
                if (_playerIndexToScoreDKV[i] > highestScore)
                {
                    highestScore = _playerIndexToScoreDKV[i];
                    playersWithHighestScore = 1;
                    playerWonIndex = i;
                }
                else if (_playerIndexToScoreDKV[i] == highestScore)
                {
                    playersWithHighestScore++;
                }
            }

            if (playersWithHighestScore == 1)
            {
                GameManager.Instance.PlayerWonNotification(playerWonIndex);
            }
        }

        public int GetLeadingPlayerIndex()
        {
            int leadingPlayerIndexScore = 0;
            int leadingPlayerIndex = 0;
            int countPlayerWithMaxScore = 0;

            for (int i = 0; i < _playerIndexToScoreDKV.Count; i++)
            {
                if (_playerIndexToScoreDKV[i] > leadingPlayerIndexScore)
                {
                    leadingPlayerIndex = i;
                    leadingPlayerIndexScore = _playerIndexToScoreDKV[i];
                    countPlayerWithMaxScore = 0;
                }
                else if (_playerIndexToScoreDKV[i] == leadingPlayerIndexScore)
                {
                    countPlayerWithMaxScore++;
                }
            }

            return leadingPlayerIndexScore == 0 || countPlayerWithMaxScore > 0 ? -1 : leadingPlayerIndex;
        }
    }
}
