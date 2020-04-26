using FlappyDank.Controllers;
using UnityEngine;

namespace FlappyDank
{
    public class SimpleCarousel : MonoBehaviour, IFrameListener
    {
        [SerializeField]
        private float _coefMoving;
        [SerializeField]
        private GameObject[] _children;

        [SerializeField]
        private bool _ignoreX;
        [SerializeField]
        private bool _ignoreY;
        [SerializeField]
        private bool _ignoreZ;

        [SerializeField]
        private float _width = 1668;

        private GameObject _target;

        private Vector3 _lastPosition;

        public void SetFollowTarget(GameObject target)
        {
            _target = target;
        }

        public void ResetCarousel()
        {
            var middle = _children.Length / 2;

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].transform.localPosition = new Vector3(_width * (i - middle),
                                                              _children[i].transform.position.y,
                                                              _children[i].transform.position.x);
            }
        }

        public void UpdateFrame()
        {
            var movement = _target.transform.position - _lastPosition;

            if (_ignoreX)
                movement.x = 0;
            if (_ignoreY)
                movement.y = 0;
            if (_ignoreZ)
                movement.z = 0;

            movement *= _coefMoving;

            foreach (var child in _children)
            {
                var prevPosition = child.transform.position;
                child.transform.position = prevPosition + movement;
            }

            _lastPosition = _target.transform.position;

            CheckCarousel();
        }

        private void CheckCarousel()
        {
            var minValue = _target.transform.position * _coefMoving;

            var width = _width;

            foreach (var child in _children)
            {
                if (child.transform.localPosition.x > -width)
                    continue;

                child.transform.localPosition = new Vector3(width * 2, child.transform.position.y, child.transform.position.z);
                break;
            }
        }
    }
}