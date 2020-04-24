using FlappyDank.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public class BasketsSpawner : MonoBehaviour
    {
        public event EventHandler<BasketHitEventArgs> BasketHitEvent;
        public event EventHandler BasketTouchedEvent;
        public event EventHandler<BasketLeftEventArgs> BaskedLeftEvent;

        [SerializeField]
        private GameObject _basketPrefab;
        [SerializeField]
        private int _poolCapacity = 5;
        [SerializeField]
        private float _topEdgeY = 3;
        [SerializeField]
        private float _bottomEdgeY = -2;
        [SerializeField]
        private float _clockwiseDegree = 15f;
        [SerializeField]
        private float _counterclockwiseDegree = 35f;
        [SerializeField]
        private float _rangeVisibility = 7;
        [SerializeField]
        private float _basketWidth;

        private ObjectPool _basketsPool;
        private Dictionary<Basket, bool> _visibleProcessedObjects;

        private void Awake()
        {
            _basketsPool = new ObjectPool(_basketPrefab, gameObject, _poolCapacity);
            _visibleProcessedObjects = new Dictionary<Basket, bool>();
        }

        public void CreateBasket(Vector2 position)
        {
            var basket = _basketsPool.Get().GetComponent<Basket>();
            basket.Reset();
            var positionY = UnityEngine.Random.Range(_bottomEdgeY, _topEdgeY);
            var rotationZ = UnityEngine.Random.Range(-_counterclockwiseDegree, _clockwiseDegree);
            basket.transform.position = new Vector3(position.x + _rangeVisibility, positionY, basket.transform.position.z);
            basket.transform.rotation = Quaternion.Euler(basket.transform.localRotation.eulerAngles.x, basket.transform.localRotation.eulerAngles.y, rotationZ);
            _visibleProcessedObjects.Add(basket, false);

            basket.BasketHitEvent  += Basket_OnBasketHitEventHandler;
            basket.EdgeTouchEvent  += Basket_OnEdgeTouchEventHandler;
            basket.LeftBehindEvent += Basket_OnLeftBehindEventHandler;
        }

        public void RefreshByPoint(Vector3 position)
        {
            var inactiveBaskets = new List<Basket>();

            foreach (var basket in _visibleProcessedObjects.Keys)
            {
                basket.IsAhead = basket.transform.position.x + _basketWidth > position.x;

                if (basket.transform.position.x > position.x - _rangeVisibility)
                    continue;

                inactiveBaskets.Add(basket);
            }

            foreach (var basket in inactiveBaskets)
            {
                _basketsPool.Return(basket.gameObject);
                _visibleProcessedObjects.Remove(basket);

                basket.BasketHitEvent  -= Basket_OnBasketHitEventHandler;
                basket.EdgeTouchEvent  -= Basket_OnEdgeTouchEventHandler;
                basket.LeftBehindEvent -= Basket_OnLeftBehindEventHandler;
            }
        }

        public void ResetBaskets()
        {
            foreach (var basket in _visibleProcessedObjects.Keys)
            {
                _basketsPool.Return(basket.gameObject);

                basket.BasketHitEvent -= Basket_OnBasketHitEventHandler;
                basket.EdgeTouchEvent -= Basket_OnEdgeTouchEventHandler;
                basket.LeftBehindEvent -= Basket_OnLeftBehindEventHandler;
            }

            _visibleProcessedObjects.Clear();
        }

        private void Basket_OnBasketHitEventHandler(object sender, EventArgs e)
        {
            var basket = sender as Basket;

            if (BasketTouchedEvent == null)
                return;

            var hasDirtyTouch = _visibleProcessedObjects[basket];
            var args = new BasketHitEventArgs(basket, hasDirtyTouch);
            BasketHitEvent?.Invoke(this, args);
        }

        private void Basket_OnEdgeTouchEventHandler(object sender, EventArgs e)
        {
            var basket = sender as Basket;
            _visibleProcessedObjects[basket] = true;
            BasketTouchedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void Basket_OnLeftBehindEventHandler(object sender, EventArgs e)
        {
            var basket = sender as Basket;
            BaskedLeftEvent?.Invoke(this, new BasketLeftEventArgs(basket));
        }
    }
}