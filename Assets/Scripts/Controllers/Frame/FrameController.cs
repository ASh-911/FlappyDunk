using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public interface IFrameController : IInitializable
    {
        void AddListener(IFrameListener frameListener);
    }

    public class FrameController : IFrameController
    {
        public bool IsInited { get; private set; }

        private readonly List<IFrameListener> _listeners;

        public FrameController()
        {
            _listeners = new List<IFrameListener>();
        }
        
        public void Init()
        {
            if (IsInited)
            {
                Debug.LogError("FrameController has already inited");
                return;
            }

            IsInited = true;

            var go = new GameObject("FrameController");
            var comp = go.AddComponent<FrameHandler>();
            UnityEngine.Object.DontDestroyOnLoad(go);

            comp.FrameUpdatedEvent += FrameHandlerComponent_OnFrameUpdatedEventHandler;
        }

        public void AddListener(IFrameListener frameListener)
        {
            _listeners.Add(frameListener);
        }

        private void FrameHandlerComponent_OnFrameUpdatedEventHandler(object sender, EventArgs e)
        {
            foreach (var listener in _listeners)
            {
                listener.UpdateFrame();
            }
        }
    }
}