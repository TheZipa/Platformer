using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(AnimatorController))]
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _playerDamage = 40f;
        [SerializeField] private float _attackRange = 0.2f;
        [SerializeField] private float _attackRate = 3f;
        [SerializeField] private LayerMask _attackMask;

        private float _nextAttackTime = 0f;
        private AnimatorController _animator;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<AnimatorController>();
        }

        private void Update()
        {
            if(Time.time >= _nextAttackTime && _playerMovement.IsGrounded)
            {
                if (Input.GetButtonDown(GlobalInputStrings.FIRE_1))
                {
                    MeleeAttack();
                    _nextAttackTime = Time.time + 1f / _attackRate;
                }
            }
        }

        private void MeleeAttack()
        {
            _animator.SetAttackAnimation();

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);
            
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.gameObject.GetComponent<HeathPoints>().TakeDamage(_playerDamage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null)
                return;

            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }
}