using System;
using UnityEngine;

namespace FlappyDank
{
    public class PhysicsHandler : MonoBehaviour
    {
        public event EventHandler FixedUpdateEvent;

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}