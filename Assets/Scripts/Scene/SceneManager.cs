namespace Managers.Scene
{
    using System.Collections;
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
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
            {
                _animator.enabled = transform;
                _animator.SetTrigger("exit");
                yield return new WaitForSeconds(3.2f);
            }
            else
            {
                _animator.SetTrigger("exit");
                yield return new WaitForSeconds(1.5f);
            }

            
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}