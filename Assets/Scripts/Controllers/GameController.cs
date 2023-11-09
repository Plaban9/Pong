namespace Controller
{
    using System.Collections;

    using UnityEngine;

    public class GameController : MonoBehaviour
    {
        private bool connected = false;

        IEnumerator CheckForControllers()
        {
            while (true)
            {
                var controllers = Input.GetJoystickNames();

                if (!connected && controllers.Length > 0)
                {
                    for (int i = 0; i < controllers.Length; i++)
                    {
                        Debug.Log(controllers[i]);
                    }
                    connected = true;
                    Debug.Log("Connected");

                }
                else if (connected && controllers.Length == 0)
                {
                    connected = false;
                    Debug.Log("Disconnected");
                }

                yield return new WaitForSeconds(1f);
            }
        }

        void Awake()
        {
            StartCoroutine(CheckForControllers());
        }
    }
}
