﻿using FlappyDank.Controllers;
using UnityEngine;

namespace FlappyDank
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour, IFrameListener
    {
        [SerializeField]
        private GameObject _target;
        [SerializeField]
        private Vector3 _offset;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void UpdateFrame()
        {
            var updatedPosition = new Vector3(_target.transform.position.x, transform.position.y, _camera.transform.position.z);
            transform.position = updatedPosition + _offset;
        }
    }

}