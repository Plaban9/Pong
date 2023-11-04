namespace Agent
{
    using Configuration.PlayerConfiguration;

    using Interactables.Paddle;

    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int _score; // For Debug in Inspector
        public int Score { get => _score; set => _score = value; }
        public string Name { get; private set; }
        public int PlayerIndex { get; private set; }
        public Color PlayerColour { get; private set; }
        public PaddlePositionType PositionType { get; private set; }

        public void OnInstantiated(int playerIndex, PlayerAttributes playerAttributes, bool isStarterPaddle)
        {
            Score = 0;
            Name = playerAttributes.playerName;
            PlayerIndex = playerIndex;
            PlayerColour = playerAttributes.playerColour;
            PositionType = playerAttributes.paddlePositionType;

            PerformConfigurationOperations();
            SetPaddleProperties(isStarterPaddle, playerAttributes);
        }

        public void OnReset(int playerIndex, PlayerAttributes playerAttributes, bool isStarterPaddle)
        {
            PerformConfigurationOperations();
            OnPaddleReset(isStarterPaddle);
        }

        private void PerformConfigurationOperations()
        {
            ApplyColour(PlayerColour);
            SetNameToPaddle(Name);
        }

        private void ApplyColour(Color colour)
        {
            //Color color;

            //if (ColorUtility.TryParseHtmlString(colour, out color))
            //{
            //    this.GetComponent<SpriteRenderer>().color = color;
            //}

            this.GetComponent<SpriteRenderer>().color = colour;
        }

        private void SetNameToPaddle(string name)
        {
            this.GetComponent<Paddle>().SetPaddleName(name);
        }

        private void SetPaddleProperties(bool isStarterPaddle, PlayerAttributes playerAttributes)
        {
            this.GetComponent<Paddle>().OnInstantiated(PlayerIndex, Name, isStarterPaddle, playerAttributes);
        }

        private void OnPaddleReset(bool isStarterPaddle)
        {
            this.GetComponent<Paddle>().OnReset(PlayerIndex, isStarterPaddle);
        }

        public void ApplyPowerup(PaddlePowerup paddlePowerup)
        {
            this.GetComponent<Paddle>().ApplyPowerup(paddlePowerup);
        }
    }
}
