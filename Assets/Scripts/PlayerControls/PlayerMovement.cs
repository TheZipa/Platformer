using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AnimatorController))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsCutscenePlaying { get; set; } = false;
        public bool IsGrounded { get; private set; } = false;

        [Header("Movement Variables")]
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _jumpOffset;
        [SerializeField] private float _speed;

        [Header("Settings")]
        [SerializeField] private Transform _groundColliderTransform;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private AnimationCurve _speedCurve;

        
        private SpriteRenderer _renderer;
        private AnimatorController _animator;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<AnimatorController>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _animator.SetJumpAnimation(_rigidbody.velocity.y, IsGrounded);
        }

        private void FixedUpdate()
        {
            CheckIsGrounded();
        }

        private void CheckIsGrounded()
        {
            Vector3 overlapCirclePosition = _groundColliderTransform.position;
            IsGrounded = Physics2D.OverlapCircle(overlapCirclePosition, _jumpOffset, _groundMask);
        }

        private void Jump()
        {
            if(IsGrounded)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
            }
        }

        private void HorizontalMovement(float direction)
        {
            _rigidbody.velocity = new Vector2(_speedCurve.Evaluate(direction),_rigidbody.velocity.y);
        }

        private void CorrectFlip(float direction)
        {
            if (direction < 0)
                _renderer.flipX = true;
            else
                _renderer.flipX = false;
        }

        public void Move(float direction, bool isJumpButtonPressed)
        {
            if(!IsCutscenePlaying)
            {
                if (isJumpButtonPressed)
                {
                    Jump();
                }

                if(Mathf.Abs(direction) > 0.01f)
                {
                    HorizontalMovement(direction);
                    CorrectFlip(direction);

                    _animator.SetRunAnimation(true);
                }
                else
                {
                    _animator.SetRunAnimation(false);
                }
            }
        }
    }
}