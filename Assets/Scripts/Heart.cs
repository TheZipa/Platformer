using UnityEngine;

namespace Platformer
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _collectEffect;
        [SerializeField] private int _hpForCollect = 25;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Rogue Knight")
            {
                _collectEffect.Play();
                gameObject.SetActive(false);
                collision.gameObject.GetComponent<HeathPoints>().Heal(_hpForCollect);
            }
        }
    }
}