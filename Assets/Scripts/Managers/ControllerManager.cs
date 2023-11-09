namespace Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;
    using UnityEngine.InputSystem.XR;

    public class ControllerManager : MonoBehaviour
    {
        private bool connected = false;
        private List<string> _controllerNames = new List<string>();

        void Awake()
        {
            _controllerNames.Clear();
        }

        public void Initialize()
        {
            StartCoroutine(CheckForControllers());
        }

        IEnumerator CheckForControllers()
        {
            while (true)
            {
                var controllers = Input.GetJoystickNames();

                if ((!connected && controllers.Length > 0) || (controllers.Length != _controllerNames.Count) || (controllers.Length == 1 && controllers[0].Equals("")))
                {
                    if (controllers.Length > _controllerNames.Count)
                    {
                        for (int i = 0; i < controllers.Length; i++)
                        {
                            if (!_controllerNames.Contains(controllers[i]))
                            {
                                _controllerNames.Add(controllers[i]);
                                OnControllerAdded(_controllerNames.Count);
                            }
                        }
                    }
                    else if (controllers.Length < _controllerNames.Count)
                    {
                        foreach (var controller in _controllerNames)
                        {
                            if (!controllers.Contains(controller))
                            {
                                OnControllerRemoved(_controllerNames.Count);
                                _controllerNames.Remove(controller);
                            }
                        }
                    }
                    else if (controllers.Length == _controllerNames.Count)
                    {
                        if (controllers.Length == 1 && controllers[0].Equals(""))
                        {
                            OnControllerRemoved(0);
                        }
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

        private void OnControllerAdded(int count)
        {
            GameManager.Instance.OnControllerConnected(count);
        }

        private void OnControllerRemoved(int count)
        {
            GameManager.Instance.OnControllerDisconnected(count);
        }
    }
}