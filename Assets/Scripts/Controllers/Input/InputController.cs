using System;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public interface IInputController : IInitializable
    {
        event EventHandler MouseClickEvent;
    }

    public class InputControler : IInputController
    {
        public event EventHandler MouseClickEvent;

        public bool IsInited { get; private set; }

        public void Init()
        {
            if (IsInited)
            {
                Debug.LogError("InputController has already inited");
                return;
            }

            IsInited = true;

            var go = new GameObject("InputController");
            var comp = go.AddComponent<InputHandler>();
            UnityEngine.Object.DontDestroyOnLoad(go);

            comp.MouseClickedEvent += InputHandlerComponent_OnMouseClickedEventHandler;
        }

        private void InputHandlerComponent_OnMouseClickedEventHandler(object sender, EventArgs e)
        {
            MouseClickEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}