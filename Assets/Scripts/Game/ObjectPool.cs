using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private GameObject _parentObject; 
    [SerializeField] private Pool[] _pools = null;

    private void Awake()
    {
        for (int j = 0; j < _pools.Length; j++)
        {
            _pools[j].pooledObjects = new Queue<GameObject>();

            for (int i = 0; i < _pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(_pools[j].objectPrefab);
                obj.transform.parent = _parentObject.transform;
                obj.SetActive(false);

                _pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject(int objectType)
    {
        if(objectType >= _pools.Length)
        {
            Debug.Log("unidentified object type");
            return null;
        }

        GameObject obj = _pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);
        _pools[objectType].pooledObjects.Enqueue(obj);
        return obj;
    }
    public int GetPoolSize()
    {
        return _pools.Length;
    }
}
