using System;
using UnityEngine;

namespace FlappyDank
{
    public class InputHandler : MonoBehaviour
    {
        public event EventHandler MouseClickedEvent;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                MouseClickedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}