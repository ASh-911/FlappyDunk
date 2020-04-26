using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlappyDank
{
    public class Basket : MonoBehaviour
    {
        [SerializeField]
        private float _delayBeforeDisappear = 0.5f;
        [SerializeField]
        private float _timeDisappearing = 1.0f;
        [SerializeField]
        private float _disappearingHeight = 20.0f;

        public event EventHandler BasketHitEvent;
        public event EventHandler EdgeTouchEvent;
        public event EventHandler LeftBehindEvent;

        private bool _isAhead;
        public bool IsAhead
        {
            get { return _isAhead; }
            set
            {
                if (_isAhead == value)
                    return;

                _isAhead = value;

                if (!value)
                    LeftBehindEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool WasHit { get; private set; }

        private List<Collider2D> _colliders;

        public void ResetBasket()
        {
            WasHit = false;

            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }

            gameObject.SetActive(true);
        }

        private void Awake()
        {
            _colliders = GetComponents<Collider2D>().ToList();

            ResetBasket();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EdgeTouchEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (WasHit)
                return;

            for (int i = 0; i < _colliders.Count; i++)
            {
                _colliders[i].enabled = false;
            }

            StartCoroutine(DisappearingCoroutine());

            WasHit = true;
            BasketHitEvent?.Invoke(this, EventArgs.Empty);
        }

        private IEnumerator DisappearingCoroutine()
        {
            yield return new WaitForSeconds(_delayBeforeDisappear);

            var startTime = Time.time;

            var interpolation = 0.0f;

            var targetPositionY = transform.position.y + _disappearingHeight;

            while (interpolation <= 1.0f)
            {
                yield return new WaitForEndOfFrame();

                interpolation = (Time.time - startTime) / _timeDisappearing;

                var newPositionY = Mathf.Lerp(transform.position.y, targetPositionY, interpolation);
                var newPosition = new Vector3(transform.position.x, newPositionY, transform.position.z);
                gameObject.transform.position = newPosition;
            }
        }
    }
}