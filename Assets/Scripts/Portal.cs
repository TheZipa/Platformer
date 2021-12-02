using UnityEngine;

namespace Platformer
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private GameObject _endLevelPopup;
        [SerializeField] private GameObject[] _disableObjects;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Rogue Knight")
            {
                SetEndOfLevel();
            }
        }

        private void SetEndOfLevel()
        {
            _levelController.ChangeLevelPauseState();
            _endLevelPopup.SetActive(true);
            _animator.SetTrigger("End");

            foreach(var obj in _disableObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}