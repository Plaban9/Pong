namespace Agent
{
    using Configuration.PlayerConfiguration;

    using Interactables.Paddle;

    using UI.PowerUp;

    using UnityEngine;
    using static Managers.GameplayConstants;

    using Paddle = Interactables.Paddle.Paddle;

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int _score; // For Debug in Inspector
        public int Score { get => _score; set => _score = value; }
        public string Name { get; private set; }
        public int PlayerIndex { get; private set; }
        public Color PlayerColour { get; private set; }
        public PaddlePositionType PositionType { get; private set; }

        public void OnInstantiated(int playerIndex, PlayerAttributes playerAttributes, PowerUpHandler powerUpHandler, bool isStarterPaddle)
        {
            Score = 0;
            Name = playerAttributes.playerName;
            PlayerIndex = playerIndex;
            PlayerColour = playerAttributes.playerColour;
            PositionType = playerAttributes.paddlePositionType;

            PerformConfigurationOperations();
            SetPaddleProperties(isStarterPaddle, playerAttributes, powerUpHandler);
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

        private void SetPaddleProperties(bool isStarterPaddle, PlayerAttributes playerAttributes, PowerUpHandler powerUpHandler)
        {
            Paddle paddle = this.GetComponent<Paddle>();
            paddle.OnInstantiated(PlayerIndex, Name, isStarterPaddle, playerAttributes, powerUpHandler);

            if (PlayerIndex >= 2)
            {
                paddle.SetPaddleControlType(PaddleControlType.AI);
            }
        }

        public void SetPaddleControllerType(PaddleControlType paddleControlType)
        {
            Paddle paddle = this.GetComponent<Paddle>();
            paddle.SetPaddleControlType(paddleControlType);
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
