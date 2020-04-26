using System;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public interface ILevelController : IInitializable
    {
        void SetManager(LevelManager levelManager);
        void SetTopPanelView(TopPanelView topPanelView);
        void SetPlayer(BallScript ball);
        void SetGameOverView(GameOverView gameOverView);
        void SetBackCarousel(SimpleCarousel backCarousel);
    }

    public class LevelController : ILevelController
    {
        public bool IsInited { get; private set; }

        private LevelManager _levelManager;

        private BottomPanelView _bottomPanelView;
        private TopPanelView _topPanelView;
        private GameOverView _gameOverView;
        private SimpleCarousel _baclCarousel;

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
            _levelManager.LevelFailedEvent  += LevelManager_OnLevelFailedEventHandler;
            _levelManager.BasketTouchEvent += LevelManager_OnBasketTouchEventHandler;
        }

        public void SetGameOverView(GameOverView gameOverView)
        {
            _gameOverView = gameOverView;
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

        public void SetBackCarousel(SimpleCarousel backCarousel)
        {
            _baclCarousel = backCarousel;
        }

        private void RestartGame()
        {
            ResetCombo();
            _totalScore = 0;

            _gameOverView.HideText();
            _player.Begin();
            _levelManager.ResetLevel();
            _baclCarousel.ResetCarousel();
        }

        private void LevelManager_OnBasketHitEventHandler(object sender, BasketHitEventArgs e)
        {
            if (e.HasDirtyTouch)
                ResetCombo();
            else
                SetComboValue(++_comboValue);

            AddScoreValue();
        }

        private void LevelManager_OnLevelFailedEventHandler(object sender, EventArgs e)
        {
            _gameOverView.ShowText();
            _player.Finish();
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