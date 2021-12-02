using System.Collections;
using UnityEngine;

namespace Platformer
{
    public class BarrelExplosion : MonoBehaviour
    {
        [SerializeField] private float _explosionDamage;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _timeToExplosion;
        [SerializeField] private GameObject _explosionEffect;
        [SerializeField] private ParticleSystem _wickEffect;

        private SpriteRenderer _barrelSprite;
        private Collider2D _collider;
        private HeathPoints _hp;

        private void Start()
        {
            _barrelSprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _hp = GetComponent<HeathPoints>();

            _hp.ObjectDead += StartBarrelExplosion;
        }

        private void StartBarrelExplosion()
        {
            StartCoroutine(StartExplosion());
        }

        private IEnumerator StartExplosion()
        {
            _collider.enabled = true;
            _wickEffect.Play();

            yield return new WaitForSeconds(_timeToExplosion);

            _wickEffect.gameObject.SetActive(false);
            DamageNearby();
            _explosionEffect.gameObject.SetActive(true);
            _barrelSprite.enabled = false;

            yield return new WaitForSeconds(1f);

            _collider.enabled = false;
            Destroy(gameObject);
        }

        private void DamageNearby()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

            foreach(var nearbyObject in colliders)
            {
                if(nearbyObject.CompareTag("Damagable"))
                {
                    nearbyObject.gameObject.GetComponent<HeathPoints>().TakeDamage(_explosionDamage);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}