using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    #region fields
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private GameObject _mainPlatform;
    private bool _firstInitialize = false;
    #endregion

    #region mb callback 
    private void Start()
    {
        GenerateRunnerPlatform();
    }

    private void OnEnable()
    {
        EventSystem.GeneratePlatform += GenerateRunnerPlatform;
    }

    private void OnDisable()
    {
        EventSystem.GeneratePlatform -= GenerateRunnerPlatform;
    }
    #endregion

    #region private methods
    private void GenerateRunnerPlatform()
    {
        //_mainPlatform.transform.position = Vector3.zero;
        int poolSize = _objectPool.GetPoolSize();
        
        for (int i = 0; i < poolSize; i++)
        {
            var subPlatformObj = _objectPool.GetPooledObject(i);
            LocateSubPlatforms(subPlatformObj, i);
        }

        _firstInitialize = true;
        //LocateSubPlatforms(firstSubPlatformObject, secondSubPlatformObject, thirdSubPlatformObject);
    }

    private void LocateSubPlatforms(GameObject platformObject, int iteration)
    {
        if(!_firstInitialize)
        {
            platformObject.transform.position = Vector3.zero + new Vector3(0, 0, iteration * 50);
        }
        else
        {
            platformObject.transform.position = Vector3.zero + new Vector3(0, 0, 100) + new Vector3(0, 0, iteration * 50);
        }

            
    }
    #endregion

/*     private void LocateSubPlatforms(GameObject obj1, GameObject obj2, GameObject obj3)
    {
        obj1.transform.position = Vector3.zero;

        float obj1ZLength = obj1.transform.localScale.z;
        Vector3 obj2Position = obj1.transform.position + new Vector3(0, 0, obj1ZLength / 2); 
        obj2.transform.position = obj2Position;

        float obj2ZLength = obj2.transform.localScale.z; 
        Vector3 obj3Position = obj2.transform.position + new Vector3(0, 0, obj2ZLength / 2); 
        obj3.transform.position = obj3Position;
    } */
}
