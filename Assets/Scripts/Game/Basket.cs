using System;
using UnityEngine;

namespace FlappyDank
{
    public class Basket : MonoBehaviour
    {
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

        public void Reset()
        {
            WasHit = false;
        }

        private void Awake()
        {
            Reset();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EdgeTouchEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (WasHit)
                return;

            WasHit = true;
            BasketHitEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}