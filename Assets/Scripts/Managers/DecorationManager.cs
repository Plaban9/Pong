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
        }

        public void OnReset()
        {
            for (int i = 0; i < _fadeBoundsArray.Length; i++)
            {
                _fadeBoundsArray[i].OnReset();
            }
        }
    }
}