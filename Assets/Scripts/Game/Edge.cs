using System;
using UnityEngine;

namespace FlappyDank
{
    [RequireComponent(typeof(Collider2D))]
    public class Edge : MonoBehaviour
    {
        public event EventHandler CollisionEnterEvent;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnterEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}