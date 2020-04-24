using System.Collections.Generic;
using UnityEngine;

namespace FlappyDank.Common
{
    public interface IObjectPool
    {
        GameObject Get();
        void Return(GameObject gameObject);
    }

    public class ObjectPool : IObjectPool
    {
        private readonly List<GameObject> _pooledObjects;
        private readonly GameObject _template;
        private readonly GameObject _parent;

        public ObjectPool(GameObject template, GameObject parent, int capacity = 0)
        {
            _pooledObjects = new List<GameObject>();
            _template      = template;
            _parent        = parent;

            for (int i = 0; i < capacity; i++)
            {
                CreateNewObject();
            }
        }

        public GameObject Get()
        {
            GameObject result = null;

            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    result = _pooledObjects[i];
                    break;
                }
            }

            if (result == null)
            {
                result = CreateNewObject();
            }

            result.SetActive(true);
            return result;
        }

        public void Return(GameObject gameObject)
        {
            if (!_pooledObjects.Contains(gameObject))
                return;

            gameObject.SetActive(false);
        }

        private GameObject CreateNewObject()
        {
            var go = GameObject.Instantiate(_template, _parent.transform);
            go.SetActive(false);
            _pooledObjects.Add(go);
            return go;
        }
    }
}