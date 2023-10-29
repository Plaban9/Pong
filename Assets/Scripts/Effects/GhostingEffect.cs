namespace Effects.Ghosting
{
    using UnityEngine;

    public class GhostingEffect : MonoBehaviour
    {
        [SerializeField]
        private float effectActiveTime = 0.1f;
        [SerializeField]
        private float timeActivated;

        [SerializeField]
        private Transform _objectToGhost;
        //[SerializeField]
        private SpriteRenderer _objectSpriteRenderer;

        //[SerializeField]
        private SpriteRenderer _ghostEffectRenderer;

        [SerializeField]
        private Color _ghostEffectColor; // To Change Alpha Overtime
        [SerializeField]
        private Color _objectToGhostColor; // To Change Alpha Overtime

        [SerializeField]
        private float _currentEffectAlpha;
        [SerializeField]
        [Range(0f, 1f)]
        private float _initialEffectAlpha = 0.8f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _multiplierEffectAlpha = 0.85f; // Smaller the Number, faster the Sprite Fades

        [SerializeField]
        private float _currentEffectScale;
        [SerializeField]
        private float _initialEffectScale = 1f;
        [SerializeField]
        private float _multiplierEffectScale = 0.95f; // Smaller the Number, faster the Sprite Scales Down

        [SerializeField]
        private string _tagToGhost;

        private void OnEnable()
        {
            _ghostEffectRenderer = GetComponent<SpriteRenderer>();
            _objectToGhost = GameObject.FindGameObjectWithTag(_tagToGhost).transform; // If not assigned (Specific to this game)
            _objectSpriteRenderer = _objectToGhost.GetComponent<SpriteRenderer>();

            _currentEffectAlpha = _initialEffectAlpha;
            _ghostEffectRenderer.sprite = _objectSpriteRenderer.sprite;
            _objectToGhostColor = _objectSpriteRenderer.color;
            _ghostEffectRenderer.color = _objectToGhostColor;

            this.transform.SetPositionAndRotation(_objectToGhost.position, _objectToGhost.rotation);
            this.transform.localScale = _objectToGhost.localScale;

            timeActivated = Time.time;
        }

        private void Update()
        {
            _currentEffectAlpha *= _multiplierEffectAlpha;
            _ghostEffectColor = new Color(_objectToGhostColor.r, _objectToGhostColor.g, _objectToGhostColor.b, _currentEffectAlpha);
            _ghostEffectRenderer.color = _ghostEffectColor;
            this.transform.localScale *= _multiplierEffectScale;

            if (Time.time >= timeActivated + effectActiveTime)
            {
                // ADD back to Pool
                GhostingEffectPool.Instance.AddToPool(gameObject);
            }
        }

        internal void SetTagToGhost(string tag)
        {
            _tagToGhost = tag;
        }
    }
}