using UnityEngine;

namespace Platformer
{
    public class DamageDealler : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Damagable"))
            {
                collision.gameObject.GetComponent<HeathPoints>().TakeDamage(_damage);
            }

            Destroy(gameObject);
        }
    }
}