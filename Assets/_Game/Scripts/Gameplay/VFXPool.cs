using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// VFXPool description
///</summary>
public class VFXPool : MonoBehaviour
{
    private List<GameObject> _pooledObjects = new List<GameObject>();
    [SerializeField] private int _amountToPool = 20;
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        return null;
    }

    public void AddToPoolList(GameObject prefab)
    {
        _pooledObjects.Add(prefab);
    }
}
