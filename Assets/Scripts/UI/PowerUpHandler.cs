namespace UI.PowerUp
{
    using Managers;

    using TMPro;

    using UnityEngine;

    public class PowerUpHandler : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        public void ShowPowerUpAnimation(string text, Color color)
        {
            _textMeshProUGUI.text = text;
            _textMeshProUGUI.color = color;

            _animator.SetTrigger(GameplayConstants.PowerupConstants.ENTRY_ANIMATION);
        }
    }
}
