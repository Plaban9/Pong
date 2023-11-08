namespace Managers.Scene
{
    using System.Collections;

    using UI.Dropdown;

    using UnityEngine;


    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _sceneName;

        public void Start()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
            {
                StartCoroutine(StopAnimator());
            }
        }

        IEnumerator StopAnimator()
        {
            yield return new WaitForSeconds(3.5f);
            //_animator.StopPlayback();// = false;
            _animator.enabled = false;
        }

        public void LoadNextScene()
        {
            Debug.Log("Loading Next Scene");

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
            {
                _sceneName = transform.GetComponent<PlayerDropDownHandler>().CurrentModeSelection.Equals("2 Players") ? "Gameplay" : "Gameplay_4v4";
            }

            StartCoroutine(LoadSceneCoRoutine(_sceneName));
        }

        public void LoadScene(string sceneName)
        {
            Debug.Log("Loading Next Scene");
            StartCoroutine(LoadSceneCoRoutine(sceneName));
        }

        IEnumerator LoadSceneCoRoutine(string sceneName)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
            {
                _animator.enabled = true;
            }

            if (sceneName.Equals("Gameplay") || sceneName.Equals("Gameplay_4v4"))
            {               
                _animator.SetTrigger("exit");
                yield return new WaitForSeconds(3.2f);
            }
            else
            {
                _animator.enabled = true;
                _animator.SetTrigger("exitFade");
                yield return new WaitForSeconds(1.5f);
            }


            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            StartCoroutine(ExitGame());
        }

        private IEnumerator ExitGame()
        {


            _animator.enabled = transform;
            _animator.SetTrigger("exitFade");
            yield return new WaitForSeconds(1.5f);

            Application.Quit();
        }
    }
}