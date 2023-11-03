namespace Camera
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class CameraAspectRatio : MonoBehaviour
    {

        float _windowAspect;

        // Use this for initialization
        void Start()
        {
            _windowAspect = (float)Screen.width / (float)Screen.height;
            Rescale();
        }

        private void Update()
        {
            //Rescale();
        }

        private void Rescale()
        {
            if (_windowAspect != (float)Screen.width / (float)Screen.height)
            {
                Debug.Log("Rescale called");

                // set the desired aspect ratio, I set it to fit every screen 
                float targetaspect = 16f / 9f;

                // determine the game window's current aspect ratio
                float windowaspect = _windowAspect = (float)Screen.width / (float)Screen.height;

                // current viewport height should be scaled by this amount
                float scaleheight = windowaspect / targetaspect;

                // obtain camera component so we can modify its viewport
                Camera camera = GetComponent<Camera>();

                // if scaled height is less than current height, add letterbox
                if (scaleheight < 1.0f)
                {
                    Rect rect = camera.rect;

                    rect.width = 1.0f;
                    rect.height = scaleheight;
                    rect.x = 0;
                    rect.y = (1.0f - scaleheight) / 2.0f;

                    camera.rect = rect;
                }
                else // add container box
                {
                    float scalewidth = 1.0f / scaleheight;

                    Rect rect = camera.rect;

                    rect.width = scalewidth;
                    rect.height = 1.0f;
                    rect.x = (1.0f - scalewidth) / 2.0f;
                    rect.y = 0;

                    camera.rect = rect;
                }
            }
        }

    }
}
