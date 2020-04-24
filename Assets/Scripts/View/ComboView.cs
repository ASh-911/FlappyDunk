using UnityEngine;
using UnityEngine.UI;

namespace FlappyDank
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField]
        private Text _comboValue;

        public void SetValue(int comboValue)
        {
            _comboValue.text = comboValue.ToString();
        }
    }
}