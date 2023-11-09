namespace UI.Dropdown
{
    using Nova;

    using NovaSamples.Effects;
    using NovaSamples.UIControls;

    using UnityEngine;

    public class PlayerDropDownHandler : MonoBehaviour
    {
        [SerializeField]
        private UIBlock2D _leftPanel;
        [SerializeField]
        private BlurEffect _leftPanelEffect;

        [SerializeField]
        private UIBlock2D _rightPanel;
        [SerializeField]
        private BlurEffect _rightPanelEffect;

        [SerializeField]
        private UIBlock2D _background;

        [SerializeField]
        private Texture2D _twoPlayerTexture;

        [SerializeField]
        private Texture2D _fourPlayerTexture;

        [SerializeField]
        private Dropdown _dropDown;

        public string CurrentModeSelection { get; private set; }

        private void Start()
        {
            CurrentModeSelection = "2 Players";
            _dropDown.OnValueChanged.AddListener(OnUserSelectionReceived);
        }

        public void OnUserSelectionReceived(string value)
        {
            CurrentModeSelection = value;

            if (value.Equals("2 Players"))
            {
                _leftPanelEffect.InputTexture = _twoPlayerTexture;
                _rightPanelEffect.InputTexture = _twoPlayerTexture;

                _leftPanelEffect.Reblur();
                _rightPanelEffect.Reblur();

                DoPseuodoMove(_leftPanel, -0.01f);
                DoPseuodoMove(_rightPanel, -0.01f);

                _background.SetImage(_twoPlayerTexture);
                _background.Texture.Apply();
            }
            else if (value.Equals("4 Players"))
            {
                _leftPanelEffect.InputTexture = _fourPlayerTexture;
                _rightPanelEffect.InputTexture = _fourPlayerTexture;

                _leftPanelEffect.Reblur();
                _rightPanelEffect.Reblur();

                DoPseuodoMove(_leftPanel, 0.01f);
                DoPseuodoMove(_rightPanel, 0.01f);

                _background.SetImage(_fourPlayerTexture);
                _background.Texture.Apply();
            }
        }

        private void DoPseuodoMove(UIBlock2D uIBlock2D, float valueToAdd)
        {
            Vector3 position = uIBlock2D.transform.localPosition;

            uIBlock2D.transform.localPosition = new Vector3(position.x, position.y + valueToAdd, position.z);
            //uIBlock2D.transform.localPosition = new Vector3(position.x, position.y, position.z);
        }
    }
}
