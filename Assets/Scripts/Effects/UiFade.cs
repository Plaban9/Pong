namespace Effects.Fade
{
    using Configuration.DecorationConfiguration;

    using System.Collections;
    using System.Collections.Generic;

    using TMPro;

    using UnityEngine;

    public class UiFade : MonoBehaviour
    {
        private float _startTime;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private Color _finalColor = Color.white;

        [SerializeField]
        private TextMeshPro _textMeshPro;

        [SerializeField]
        private DecorationConfiguration decorationConfiguration;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
        }

        private void Start()
        {
            StartFade(_textMeshPro.color, decorationConfiguration.boundsAttributes.GetRandomColorFromGradient());
        }

        public void StartFade(Color initialColor, Color finalColor)
        {
            _startTime = Time.time;
            _textMeshPro.color = initialColor;
            _finalColor = finalColor;
        }

        private void Update()
        {
            if (_startTime + _duration > Time.time)
            {
                _textMeshPro.color = Color.Lerp(_textMeshPro.color, _finalColor, Time.deltaTime * _duration);
            }
            else
            {
                StartFade(_textMeshPro.color, decorationConfiguration.boundsAttributes.GetRandomColorFromGradient());
            }
        }
    }
}