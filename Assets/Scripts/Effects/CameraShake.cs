namespace Effects.Camera
{
    using UnityEngine;

    public class CameraShake : MonoBehaviour
    {
        // Transform of the camera to shake. Grabs the gameObject's transform
        // if null.
        [SerializeField]
        private Transform camTransform;

        // How long the object should shake for.
        [SerializeField]
        private float shakeDuration = 0f;

        // Amplitude of the shake. A larger value shakes the camera harder.
        [SerializeField]
        private float shakeAmount = 0.7f;
        [SerializeField]
        private float decreaseFactor = 1.0f;

        [SerializeField]
        private bool shaketrue = false;

        Vector3 originalPos;
        float originalShakeDuration; //<--add this

        void Awake()
        {
            if (camTransform == null)
            {
                camTransform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            originalPos = camTransform.localPosition;
            originalShakeDuration = shakeDuration; //<--add this
        }

        void Update()
        {
            if (shaketrue)
            {
                if (shakeDuration > 0)
                {
                    camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);

                    shakeDuration -= Time.deltaTime * decreaseFactor;
                }
                else
                {
                    shakeDuration = originalShakeDuration; //<--add this
                    camTransform.localPosition = originalPos;
                    shaketrue = false;
                }
            }
        }

        public void Shakecamera()
        {
            shaketrue = true;
        }

        public void Shakecamera(float _shakeDuration, float _shakeAmount)
        {
            shaketrue = true;
            shakeDuration = _shakeDuration;
            shakeAmount = _shakeAmount;
        }
    }    
}