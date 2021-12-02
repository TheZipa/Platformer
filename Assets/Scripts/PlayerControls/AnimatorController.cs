using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetRunAnimation(bool isRunning)
        {
            _animator.SetBool("IsRunning", isRunning);
        }

        public void SetAttackAnimation()
        {
            _animator.SetTrigger("Attack");
        }

        public void SetSpellAnimation()
        {
            _animator.SetTrigger("Spell");
        }

        public void SetJumpAnimation(float velocity, bool isGrounded)
        {
            _animator.SetFloat("Velocity", velocity);
            _animator.SetBool("IsGrounded", isGrounded);
        }
    }
}