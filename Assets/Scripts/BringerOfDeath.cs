using UnityEngine;

namespace Platformer
{
    public class BringerOfDeath : MonoBehaviour
    {
        [SerializeField] private float _speed, _timeToRevert, _damage;

        private BringerOfDeathState _currentState;
        private HeathPoints _hp;
        private SpriteRenderer _sp;
        private Animator _animator;
        private Rigidbody2D _rb;
        private float _currentTimeToRevert;
        private bool _isWalk = true;

        private enum BringerOfDeathState : byte
        {
            IDLE,
            WALK,
            REVERT,
        }

        private void Start()
        {
            _currentState = BringerOfDeathState.WALK;
            _hp = GetComponent<HeathPoints>();
            _rb = GetComponent<Rigidbody2D>();
            _sp = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _hp.ObjectDead += BringerOfDeathDied;
        }

        private void Update()
        {
            CheckState();
        }

        private void CheckState()
        {
            if (_hp.IsAlive)
            {
                if (_currentTimeToRevert >= _timeToRevert)
                {
                    _currentTimeToRevert = 0f;
                    _currentState = BringerOfDeathState.REVERT;
                }

                switch (_currentState)
                {
                    case BringerOfDeathState.IDLE:
                        _currentTimeToRevert += Time.deltaTime;
                        break;
                    case BringerOfDeathState.WALK:
                        _rb.velocity = Vector2.left * _speed;
                        break;
                    case BringerOfDeathState.REVERT:
                        _sp.flipX = !_sp.flipX;
                        _speed *= -1;
                        _isWalk = true;
                        _currentState = BringerOfDeathState.WALK;
                        break;
                }

                _animator.SetBool("IsWalk", _isWalk);
            }
        }

        private void BringerOfDeathDied()
        {
            _rb.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("IsDead", true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyStopper"))
            {
                _currentState = BringerOfDeathState.IDLE;
                _isWalk = false;
            }
            else if (collision.gameObject.name == "Rogue Knight")
            {
                _animator.SetTrigger("Attack");
                collision.gameObject.GetComponent<HeathPoints>().TakeDamage(_damage);
            }
        }
    }
}