using UnityEngine;
using UnityEngine.UI;

namespace FlappyDank
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreValue;

        public void SetScore(int scoreValue)
        {
            _scoreValue.text = scoreValue.ToString();
        }
    }
}