using UnityEngine;

namespace Platformer
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private HeathPoints _playerHP;
        [SerializeField] private GameObject _playerDeadWindow;

        private void Start()
        {
            _playerHP.ObjectDead += PlayerDied;
        }

        private void PlayerDied()
        {
            _playerDeadWindow.SetActive(true);
            _levelController.ChangeLevelPauseState();
        }
    }
}