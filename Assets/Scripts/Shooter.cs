using UnityEngine;

namespace Platformer
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _firePower;
        [SerializeField] private Transform _firePoint;

        public void Shoot(float direction)
        {
            GameObject currentBullet = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
            Rigidbody2D currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();

            currentBulletVelocity.velocity = new Vector2(_firePower * (direction > 0 ? 1 : -1),currentBulletVelocity.velocity.y);
        }
    }
}