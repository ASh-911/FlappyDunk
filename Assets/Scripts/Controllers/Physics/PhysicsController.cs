using System.Collections.Generic;
using UnityEngine;

namespace FlappyDank.Controllers
{
    public interface IPhysicsController : IInitializable
    {
        void AddListener(IPhysicsListener physicsListener);
    }

    public class PhysicsController : MonoBehaviour, IPhysicsController
    {
        private readonly List<IPhysicsListener> _listeners;

        public bool IsInited { get; private set; }

        public PhysicsController()
        {
            _listeners = new List<IPhysicsListener>();
        }

        public void Init()
        {
            if (IsInited)
            {
                Debug.LogError("PhysicsController has already inited");
                return;
            }

            IsInited = true;

            var go = new GameObject("PhysicsController");
            var comp = go.AddComponent<PhysicsHandler>();
            DontDestroyOnLoad(go);

            comp.FixedUpdateEvent += PhysicsHandlerComponent_OnFixedUpdateEventHandler;
        }

        public void AddListener(IPhysicsListener physicsListener)
        {
            _listeners.Add(physicsListener);
        }

        private void PhysicsHandlerComponent_OnFixedUpdateEventHandler(object sender, System.EventArgs e)
        {
            foreach (var listener in _listeners)
            {
                listener.PhysicsUpdate();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}