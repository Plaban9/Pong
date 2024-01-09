namespace Environment
{
    using Configuration.DecorationConfiguration;

    using Effects.Camera;
    using Effects.Fade;

    using System;

    using UnityEngine;

    public class RotateObstacle : MonoBehaviour
    {
        [SerializeField]
        private bool _rotateClockwise = true;

        [SerializeField]
        private float _rotateMagnitude = 50f;

        [SerializeField]
        private FadeObstacle[] _blades;

        [SerializeField]
        private AudioClip[] _hitClips;

        DecorationConfiguration decorationConfiguration;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, _rotateMagnitude * Time.deltaTime); //rotates _rotateMagnitude degrees per second around z axis
        }

        public void OnInitialized(DecorationConfiguration decorationConfiguration)
        {
            this.decorationConfiguration = decorationConfiguration;
            _rotateMagnitude = decorationConfiguration.obstacleAttributes.movableObstacle.rotateSpeed;

            if (_rotateClockwise)
            {
                _rotateMagnitude *= -1f;
            }

            for (int i = 0; i < _blades.Length; i++)
            {
                _blades[i].OnInitialized(decorationConfiguration);
            }
        }

        public void OnReset()
        {
            for (int i = 0; i < _blades.Length; i++)
            {
                _blades[i].OnReset();
            }
        }

        public void OnHitByBall()
        {
            try
            {
                this.gameObject.GetComponent<CameraShake>().Shakecamera();
                if (_hitClips != null)
                {
                    AudioSource.PlayClipAtPoint(_hitClips[UnityEngine.Random.Range(0, _hitClips.Length)], transform.position, 1f);
                }

                Color initialColor = decorationConfiguration.obstacleAttributes.movableObstacle.GetRandomColorFromGradient();

                for (int i = 0; i < _blades.Length; i++)
                {
                    _blades[i].StartFade(initialColor);
                }
            }
            catch (Exception ignored)
            {

            }
        }
    }
}
