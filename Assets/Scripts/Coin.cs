using UnityEngine;

namespace Platformer
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _collectEffect;
        [SerializeField] private int _scoreForCollect = 50;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Rogue Knight")
            {
                _collectEffect.Play();
                gameObject.SetActive(false);
                FindObjectOfType<Score>().AddScore(_scoreForCollect);
            }
        }
    }
}