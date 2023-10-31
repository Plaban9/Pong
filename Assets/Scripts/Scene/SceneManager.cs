namespace Managers.Scene
{
    using System.Collections;

    using Unity.VisualScripting;

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _sceneName;

        IEnumerator LoadScene()
        { 
            _animator.SetTrigger("end");
            yield return new WaitForSeconds(1.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
        }
    }
}