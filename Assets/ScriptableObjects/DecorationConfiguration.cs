namespace Configuration.DecorationConfiguration
{
    using Interactables.Paddle;

    using UnityEngine;

    [CreateAssetMenu(fileName = "DecorationConfiguration", menuName = "DataStorage/DecorationConfiguration", order = 2)]
    public class DecorationConfiguration : ScriptableObject
    {
        public BoundsAttributes boundsAttributes;
        public MidAreaAttributes midAreaAttributes;
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
}