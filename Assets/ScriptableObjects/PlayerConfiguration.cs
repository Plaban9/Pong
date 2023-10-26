namespace Configuration.PlayerConfiguration
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "ScriptableObjects/PlayerConfiguration", order = 1)]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField]
        private string[] _playerNames = new string[] { "Player_1", "Player_2", "Player_3", "Player_4" };
        [SerializeField]
        private string[] _playerColours = new string[] { "FF1178", "01FFF4", "FE0000", "7CFF01" }; // In order - 1, 2, 3, 4
        [SerializeField]
        private string _ballColour = "FFF205";
    }
}