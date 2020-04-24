using System;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public class FrameHandler : MonoBehaviour
    {
        public event EventHandler FrameUpdatedEvent;

        void Update()
        {
            FrameUpdatedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}