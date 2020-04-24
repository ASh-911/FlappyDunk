using UnityEngine;

namespace FlappyDank
{
    public class BottomPanelView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _anchoredGround;

        private RectTransform _rectTransform;

        public float Height { get { return _rectTransform.rect.height; } }
        public Vector3 AnchoredPosition { get { return _anchoredGround.transform.position; } }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}