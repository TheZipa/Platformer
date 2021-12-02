using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class HeathPoints : MonoBehaviour
    {
        public delegate void DeadHandler();
        public event DeadHandler ObjectDead;

        public bool IsAlive { get; private set; }
        [SerializeField] private Image _healthBar;
        [SerializeField] private float _maxHP;

        private SpriteRenderer _spriteRenderer;
        private float _currentHP;
        
        private void Awake()
        {
            _currentHP = _maxHP;
            IsAlive = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void CheckIsAlive()
        {
            if (_currentHP <= 0)
            {
                IsAlive = false;
                gameObject.GetComponent<Collider2D>().enabled = false;

                ObjectDead?.Invoke();
            }
        }

        private void DisplayHP()
        {
            if (_healthBar != null)
            {
                if (_currentHP <= 0)
                    _healthBar.fillAmount = 0;
                else
                    _healthBar.fillAmount = _currentHP / _maxHP;
            }
        }

        private void SetHurt()
        {
            _spriteRenderer.color = new Color(255, 0, 0);
            StartCoroutine(HurtTimer());
        }

        private IEnumerator HurtTimer()
        {
            yield return new WaitForSeconds(0.15f);
            _spriteRenderer.color = new Color(255, 255, 255);
        }

        public void TakeDamage(float damage)
        {
            _currentHP -= damage;
            CheckIsAlive();
            DisplayHP();
            SetHurt();
        }

        public void Heal(float hp)
        {
            if (_currentHP + hp > _maxHP)
                _currentHP += hp - (_currentHP + hp - _maxHP);
            else
                _currentHP += hp;

            DisplayHP();
        }
    }
}