namespace UI
{
    using Configuration.PlayerConfiguration;

    using TMPro;

    using UnityEngine;

    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI[] _playerScoreArray;

        public void OnInitialize(PlayerConfiguration playerConfiguration, int size, int initialScore)
        {
            for (int i = 0; i < size; i++)
            {
                _playerScoreArray[i].color = playerConfiguration.playerAttributes[i].playerColour;
                _playerScoreArray[i].text = initialScore.ToString();
            }
        }

        public void OnScoreUpdate(int playerIndex, int score)
        {
            _playerScoreArray[playerIndex].text = score.ToString();
        }
    }
}
