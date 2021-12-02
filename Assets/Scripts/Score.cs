using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text _totalScore;

        private int _score;
        private Text _scoreCount;
        
        private void Start()
        {
            _scoreCount = GetComponent<Text>();
        }

        private void DisplayScore()
        {
            _totalScore.text = _scoreCount.text = _score.ToString();
        }

        public void AddScore(int score)
        {
            _score += score;
            DisplayScore();
        }
    }
}