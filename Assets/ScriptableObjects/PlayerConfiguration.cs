namespace Configuration.PlayerConfiguration
{
    using Interactables.Paddle;

    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "DataStorage/PlayerConfiguration", order = 1)]
    public class PlayerConfiguration : ScriptableObject
    {
        public BallAttributes ballAttributes;
        public PlayerAttributes[] playerAttributes; // In order - 0, 1, 2, 3
    }

    [System.Serializable]
    public class BallAttributes
    {
        public Color ballColour = Color.green;
        public Vector3 ballScale;
    }

    [System.Serializable]
    public class PlayerAttributes
    {
        public string playerName;
        public Color playerColour = Color.white;
        public float magnitude;

        public PaddlePositionType paddlePositionType;
        public PlayerInput playerInput;

        public PaddleTransforms paddleTransform;
        public PlaySpaceBounds playSpaceBounds;
    }

    [System.Serializable]
    public class PaddleTransforms
    {
        public Vector3 playerPosition;
        public Vector3 playerScale;
        public Vector3 playerRotation;
    }

    [System.Serializable]
    public class PlayerInput
    {
        public KeyCode leftMovementKeyCode;
        public KeyCode rightMovementKeyCode;
        public KeyCode ballReleaseKeyCode;
    }

    [System.Serializable]
    public class PlaySpaceBounds
    {
        public Vector3 lowerBound;
        public Vector3 upperBound;
    }
}