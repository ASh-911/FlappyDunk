using System;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public interface ILevelController : IInitializable
    {
        void SetManager(LevelManager levelManager);
        void SetTopPanelView(TopPanelView topPanelView);
        void SetPlayer(BallScript ball);
    }

    public class LevelController : ILevelController
    {
        public bool IsInited { get; private set; }

        private LevelManager _levelManager;

        private BottomPanelView _bottomPanelView;
        private TopPanelView _topPanelView;

        private BallScript _player;

        private int _totalScore = 0;
        private int _comboValue = 0;

        public void Init()
        {
            if (IsInited)
            {
                Debug.LogError("LevelController has already inited");
                return;
            }

            IsInited = true;
        }

        public void SetManager(LevelManager levelManager)
        {
            _levelManager = levelManager;

            _levelManager.BasketHitEvent   += LevelManager_OnBasketHitEventHandler;
            _levelManager.BallMissedEvent  += LevelManager_OnBallMissedEventHandler;
            _levelManager.BasketTouchEvent += LevelManager_OnBasketTouchEventHandler;
        }

        public void SetTopPanelView(TopPanelView topPanelView)
        {
            _topPanelView = topPanelView;

            _topPanelView.RestartClickEvent += TopPanelView_OnRestartClickEventHandler;
        }

        public void SetPlayer(BallScript ball)
        {
            _player = ball;
        }

        private void AddScoreValue()
        {
            _totalScore += _levelManager.SimplePoint + _comboValue;
            _topPanelView.Score.SetScore(_totalScore);
        }

        private void ResetCombo()
        {
            SetComboValue(0);
        }

        private void SetComboValue(int value)
        {
            _comboValue = value;
            _topPanelView.Combo.SetValue(value);
        }

        private void RestartGame()
        {
            ResetCombo();
            _totalScore = 0;

            _player.Start();
            _levelManager.ResetLevel();
        }

        private void LevelManager_OnBasketHitEventHandler(object sender, BasketHitEventArgs e)
        {
            if (e.HasDirtyTouch)
                ResetCombo();
            else
                SetComboValue(++_comboValue);

            AddScoreValue();
        }

        private void LevelManager_OnBallMissedEventHandler(object sender, EventArgs e)
        {
            _player.Stop();
        }

        private void LevelManager_OnBasketTouchEventHandler(object sender, EventArgs e)
        {
            ResetCombo();
        }

        private void TopPanelView_OnRestartClickEventHandler(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}