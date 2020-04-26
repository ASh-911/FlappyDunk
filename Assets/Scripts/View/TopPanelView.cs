using System;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyDank
{
    public class TopPanelView : MonoBehaviour
    {
        public event EventHandler RestartClickEvent;

        [SerializeField]
        private ComboView _comboView;
        [SerializeField]
        private ScoreView _scoreView;
        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private GameObject _anchoredPosition;

        public ComboView Combo { get { return _comboView; } }
        public ScoreView Score { get { return _scoreView; } }

        public Vector3 AnchoredPosition { get { return _anchoredPosition.transform.position; } }

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            RestartClickEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}