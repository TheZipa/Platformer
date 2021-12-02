using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private float _transitionTime = 2f;
        [SerializeField] private Animator _animator;

        private bool _isPaused;

        private IEnumerator LoadLevel(int levelIndex)
        {
            _animator.SetTrigger("Start");
            yield return new WaitForSeconds(_transitionTime);
            SceneManager.LoadScene(levelIndex);
        }

        public void LoadLevelByIndex(int index)
        {
            StartCoroutine(LoadLevel(index));
        }

        public void RestartLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void ChangeLevelPauseState()
        {
            Time.timeScale = (_isPaused ? 1 : 0);
            _isPaused = !_isPaused;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}