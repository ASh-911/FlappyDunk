using UnityEngine;
using UnityEngine.UI;

namespace FlappyDank
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField]
        private Text _gameOverText;

        public void ShowText()
        {
            _gameOverText.gameObject.SetActive(true);
        }

        public void HideText()
        {
            _gameOverText.gameObject.SetActive(false);
        }
    }
}