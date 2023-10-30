namespace Managers
{
    using Configuration.DecorationConfiguration;
    using Configuration.PlayerConfiguration;
    using Effects.Fade;

    using UnityEngine;

    public class DecorationManager : MonoBehaviour
    {
        [SerializeField]
        private DecorationConfiguration _decorationConfiguration;

        [SerializeField]
        private Fade[] _fadeBoundsArray;

        [SerializeField]
        private SpriteRenderer[] _midAreaObjectsSpriteRenderer;

        public DecorationConfiguration GetDecorationConfiguration()
        {
            return _decorationConfiguration;
        }

        public void OnInitialized()
        {
            for (int i = 0; i < _fadeBoundsArray.Length; i++)
            {
                _fadeBoundsArray[i].OnInitialized(_decorationConfiguration);
            }

            for (int i = 0; i < _midAreaObjectsSpriteRenderer.Length; i++)
            {
                _midAreaObjectsSpriteRenderer[i].color = _decorationConfiguration.midAreaAttributes.midAreaColor;
            }
        }

        public void OnReset(int playerIndex, PlayerConfiguration playerConfiguration)
        {
            for (int i = 0; i < _fadeBoundsArray.Length; i++)
            {
                _fadeBoundsArray[i].OnReset();
            }

            Color leadingColor;

            if (playerIndex == -1)
            {
                leadingColor = _decorationConfiguration.midAreaAttributes.midAreaColor;
            }
            else
            {
                leadingColor = playerConfiguration.playerAttributes[playerIndex].playerColour;
            }

            for (int i = 0; i < _midAreaObjectsSpriteRenderer.Length; i++)
            {
                _midAreaObjectsSpriteRenderer[i].color = leadingColor;
            }
        }
    }
}