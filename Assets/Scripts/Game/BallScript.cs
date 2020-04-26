using FlappyDank.Controllers;
using System;
using UnityEngine;

namespace FlappyDank
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallScript : MonoBehaviour, IPhysicsListener
    {
        [SerializeField]
        private float _maxSpeed;

        [SerializeField]
        private float _forwardForce = 1.0f;
        [SerializeField]
        private float _upwardForce = 7.0f;

        private Rigidbody2D _rigidbody;
        private bool _active;
        private bool _isInit = false;

        private bool _upwardMovementReady = false;

        public void PhysicsUpdate()
        {
            if (!_active)
                return;

            _rigidbody.AddForce(Vector2.right * _forwardForce);

            if (_upwardMovementReady)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Vector2.up.y * _upwardForce);
                _upwardMovementReady = false;
            }
        }

        public void Begin()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.simulated = true;
            _active = true;
        }

        public void Finish()
        {
            _rigidbody.simulated = false;
            _active = false;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.simulated = false;
            _active = false;
            _isInit = false;

            SuperController.Instance.InputController.MouseClickEvent += InputController_OnMouseClickEventHandler;
        }

        private void InputController_OnMouseClickEventHandler(object sender, EventArgs e)
        {
            if (!_isInit)
            {
                _isInit = true;
                Begin();
                return;
            }

            _upwardMovementReady = true;
        }
    }
}