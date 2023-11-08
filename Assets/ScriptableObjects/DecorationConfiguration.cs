namespace Configuration.DecorationConfiguration
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "DecorationConfiguration", menuName = "DataStorage/DecorationConfiguration", order = 2)]
    public class DecorationConfiguration : ScriptableObject
    {
        public BoundsAttributes boundsAttributes;
        public MidAreaAttributes midAreaAttributes;
        public ObstacleAttributes obstacleAttributes;
        public PowerUpAttributes powerUpAttributes;
    }

    [System.Serializable]
    public class BoundsAttributes
    {
        public Color boundColor = Color.white;
        public float fadeDuration = 0.75f;

        [SerializeField]
        private Gradient _randomColorGradient;

        public Color GetRandomColorFromGradient()
        {
            return _randomColorGradient.Evaluate(Random.Range(0f, 1f));
        }
    }

    [System.Serializable]
    public class MidAreaAttributes
    {
        public Color midAreaColor = Color.white;
    }

    [System.Serializable]
    public class ObstacleAttributes
    {
        public MovableObstacle movableObstacle;
        public StaticObstacle staticObstacle;
    }

    [System.Serializable]
    public class MovableObstacle
    {
        public float rotateSpeed = 50.0f;
        public float fadeDuration = 0.75f;
        public Color obstacleColor = Color.white;

        [SerializeField]
        private Gradient _randomColorGradient;

        public Color GetRandomColorFromGradient()
        {
            return _randomColorGradient.Evaluate(Random.Range(0f, 1f));
        }
    }

    [System.Serializable]
    public class StaticObstacle
    {
        public float fadeDuration = 0.75f;
        public Color obstacleColor = Color.white;
    }

    [System.Serializable]
    public class PowerUpAttributes
    {
        public float spawnInterval;
        public float rotateSpeed;
        public Color freezeColor;
        public Color turboColor;
    }
}