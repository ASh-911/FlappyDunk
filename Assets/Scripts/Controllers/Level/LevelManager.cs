using System;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public class LevelManager : MonoBehaviour, IFrameListener
    {
        public event EventHandler<BasketHitEventArgs> BasketHitEvent;
        public event EventHandler BallMissedEvent;
        public event EventHandler BasketTouchEvent;

        [SerializeField]
        private BasketsSpawner _basketsSpawner;

        [Header("Scenario")]
        [SerializeField]
        private float _frequencyCreating = 10;
        [SerializeField]
        private int _simplePoint = 1;

        public int SimplePoint { get { return _simplePoint; } }

        private GameObject _target;
        private float _lastCreatedPosition = float.MinValue;
        private float _bottomEdge = float.MinValue;

        private void Awake()
        {
            _basketsSpawner.BasketHitEvent += BasketsSpawner_OnBasketHitEventHandler;
            _basketsSpawner.BaskedLeftEvent += BasketsSpawner_OnBaskedLeftEventHandler;
            _basketsSpawner.BasketTouchedEvent += BasketsSpawner_OnBasketTouchedEventHandler;
        }

        public void SetBottomEdge(float bottomEdge)
        {
            _bottomEdge = bottomEdge;
        }

        public void SetFollowedTarget(GameObject target)
        {
            _target = target;
        }

        public void ResetLevel()
        {
            _target.transform.position = new Vector3(0, 0, _target.transform.position.z);
            _lastCreatedPosition = float.MinValue;
            _basketsSpawner.ResetBaskets();
        }

        public void UpdateFrame()
        {
            var nextPositionForCreating = _lastCreatedPosition + _frequencyCreating;

            if (_target.transform.position.y < _bottomEdge)
            {
                BallMissedEvent?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (_target.transform.position.x > nextPositionForCreating)
            {
                _basketsSpawner.CreateBasket(_target.transform.position);
                _lastCreatedPosition = _target.transform.position.x;
            }

            _basketsSpawner.RefreshByPoint(_target.transform.position);
        }

        private void BasketsSpawner_OnBasketTouchedEventHandler(object sender, EventArgs e)
        {
            BasketTouchEvent?.Invoke(this, EventArgs.Empty);
        }

        private void BasketsSpawner_OnBasketHitEventHandler(object sender, BasketHitEventArgs e)
        {
            BasketHitEvent?.Invoke(this, e);
        }

        private void BasketsSpawner_OnBaskedLeftEventHandler(object sender, BasketLeftEventArgs e)
        {
            var wasHit = e.LeftObject.WasHit;
            if (wasHit)
                return;

            BallMissedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}