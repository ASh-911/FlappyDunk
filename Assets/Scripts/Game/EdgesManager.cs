
using System;
using UnityEngine;

namespace FlappyDank
{
    public class EdgesManager : MonoBehaviour
    {
        public event EventHandler EdgeHitEvent;

        [SerializeField]
        private Edge _topEdge;
        [SerializeField]
        private Edge _bottomEdge;

        private void Awake()
        {
            _topEdge.CollisionEnterEvent += Edge_OnCollisionEnterEventHandler;
            _bottomEdge.CollisionEnterEvent += Edge_OnCollisionEnterEventHandler;
        }

        public void SetVerticalEdges(float topEdge, float bottomEdge)
        {
            var topEdgePosition = _topEdge.gameObject.transform.position;
            var botEdgePosition = _bottomEdge.gameObject.transform.position;
            _topEdge.gameObject.transform.position = new Vector3(topEdgePosition.x, topEdge, topEdgePosition.z);
            _bottomEdge.gameObject.transform.position = new Vector3(botEdgePosition.x, bottomEdge, botEdgePosition.z);
        }

        public void RefreshByPoint(Vector3 position)
        {
            var topEdgePosition = _topEdge.gameObject.transform.position;
            var botEdgePosition = _bottomEdge.gameObject.transform.position;
            _topEdge.gameObject.transform.position = new Vector3(position.x, topEdgePosition.y, topEdgePosition.z);
            _bottomEdge.gameObject.transform.position = new Vector3(position.x, botEdgePosition.y, botEdgePosition.z);
        }

        private void Edge_OnCollisionEnterEventHandler(object sender, EventArgs e)
        {
            EdgeHitEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}