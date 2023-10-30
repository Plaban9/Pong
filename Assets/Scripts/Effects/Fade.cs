namespace Effects.Fade
{
    using Configuration.DecorationConfiguration;

    using Managers;

    using UnityEngine;

    //Bare Bones Fade
    public class Fade : MonoBehaviour
    {
        private float _startTime;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private Color _finalColor = Color.white;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnInitialized(DecorationConfiguration decorationConfiguration)
        {
            _finalColor = _spriteRenderer.color = decorationConfiguration.boundsAttributes.boundColor;
            _duration = decorationConfiguration.boundsAttributes.fadeDuration;

            OnReset();
        }

        public void StartFade(Color initialColor)
        {
            _startTime = Time.time;
            _spriteRenderer.color = initialColor;
        }

        private void Update()
        {
            if (_startTime + _duration > Time.time)
            {
                _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _finalColor, Time.deltaTime * _duration);
            }
            else
            {
                _spriteRenderer.color = _finalColor;
            }
        }

        public void OnReset()
        {
            _startTime = 0f;
            _spriteRenderer.color = _finalColor;
        }
    }
}
