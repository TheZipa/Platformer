using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Shooter))]
    [RequireComponent(typeof(HeathPoints))]
    public class EnemyArcher : MonoBehaviour
    {
        [SerializeField, Range(1,5)] private float _shootIntervalMaxTime;
        [SerializeField] private int _scoreForKill = 140;

        private float _currentShootIntervalTime;
        private Animator _animator;
        private Shooter _shooter;
        private HeathPoints _hp;
        
        private void Start()
        {
            _shooter = GetComponent<Shooter>();
            _hp = GetComponent<HeathPoints>();
            _animator = GetComponent<Animator>();

            _hp.ObjectDead += EnemyArcherDied;
            _currentShootIntervalTime = _shootIntervalMaxTime;
        }

        private void EnemyArcherDied()
        {
            _animator.SetBool("IsDead", true);
            FindObjectOfType<Score>().AddScore(_scoreForKill);
        }

        private void Update()
        {
            if (_hp.IsAlive)
            {
                _currentShootIntervalTime -= Time.deltaTime;

                if(_currentShootIntervalTime <= 0)
                {
                    _animator.SetTrigger("Shoot");
                    _shooter.Shoot(-1f);
                    _currentShootIntervalTime = _shootIntervalMaxTime;
                }
            }
        }
    }
}