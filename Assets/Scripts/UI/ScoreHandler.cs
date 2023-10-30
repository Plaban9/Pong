namespace UI
{
    using Configuration.PlayerConfiguration;

    using System.Collections;

    using TMPro;

    using UnityEngine;

    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI[] _playerScoreArray;

        public void OnInitialize(PlayerConfiguration playerConfiguration, int size)
        {
            for (int i = 0; i < size; i++)
            {
                _playerScoreArray[i].color = playerConfiguration.playerAttributes[i].playerColour;
                _playerScoreArray[i].text = "0";
            }
        }

        public void OnScoreUpdate(int playerIndex, int score)
        {
            _playerScoreArray[playerIndex].text = score.ToString();
        }
    }
}
