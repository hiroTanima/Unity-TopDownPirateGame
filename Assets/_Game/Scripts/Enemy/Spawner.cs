using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
///<summary>
/// Spawner description
///</summary>
public class Spawner : MonoBehaviour
{
    private List<GameObject> _pooledObjects = new List<GameObject>();
    [SerializeField] private int _amountToPool = 20;
    [SerializeField] private GameObject[] _prefab;
    private float spawnDelay = 2f;

    private void Start()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            for (int j = 0; j < _prefab.Length; j++)
            {
            GameObject obj = Instantiate(_prefab[j]);
            obj.transform.SetParent(this.transform);
            _pooledObjects.Add(obj);
            obj.SetActive(false);

            }
        }
    }

    private void Update()
    {
        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0f)
        {
            Spawn();
            spawnDelay = GameManager.Instance.enemySpawnTime;
        }
    }

    private GameObject GetPooledObject(EnemyType enemyType)
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy && _pooledObjects[i].GetComponent<Enemy_AI>()._enemyType == enemyType)
            {
                return _pooledObjects[i];
            }
        }

        return null;
    }

    private void Spawn()
    {
        float randEnemy = Random.Range(0f, 1f);
        GameObject _enemy = null;
        if (randEnemy > .5f)
        {
            _enemy = GetPooledObject(EnemyType.Shooter);
        }
        else
        {
            _enemy = GetPooledObject(EnemyType.Chaser);
        }

        _enemy.gameObject.GetComponent<Enemy_AI>().Initialize();
        _enemy.transform.position = new Vector2(Random.Range(GameManager.Instance.player.position.x -10f, GameManager.Instance.player.position.x + 10f), Random.Range(GameManager.Instance.player.position.y - 10f, GameManager.Instance.player.position.y + 10f));
        _enemy.SetActive(true);
    }
}
