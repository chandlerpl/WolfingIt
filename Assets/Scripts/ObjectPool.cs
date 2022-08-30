using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static Dictionary<string, ObjectPool> _availablePools = new Dictionary<string, ObjectPool>();

    private ObjectPool() {
        
    }

    public string poolTag = "";
    public int startingCapacity = 20;
    public bool expandWhenEmpty = true;
    public List<GameObject> poolPrefabs;

    private List<GameObject> _pooledObjects = new List<GameObject>();

    private void Awake() {
        if(_availablePools.ContainsKey(poolTag)) {
            _availablePools.Remove(poolTag);
        }

        _availablePools.Add(poolTag, this);
        _pooledObjects = new List<GameObject>(startingCapacity);
        for(int i = 0; i < startingCapacity; ++i) {
            _pooledObjects.Add(CreateObject());
        }
    }

    public void Push(GameObject obj) {
        obj.transform.parent = transform;
        
        _pooledObjects.Add(obj);
        obj.SetActive(false);
    }

    public GameObject Pop() {
        GameObject obj;
        if(_pooledObjects.Count > 0) {
            obj = _pooledObjects[Random.Range(0, _pooledObjects.Count - 1)];
            _pooledObjects.Remove(obj);
        } else {
            if(expandWhenEmpty) {
                obj = CreateObject();
            } else {
                return null;
            }
        }

        obj.SetActive(true);
        obj.transform.parent = null;

        return obj;
    }

    public GameObject CreateObject() {
        GameObject chosenObject = poolPrefabs[Random.Range(0, poolPrefabs.Count)];
        GameObject obj = Instantiate(chosenObject, transform);
        obj.SetActive(false);

        return obj;
    }

    public static ObjectPool CreatePool(string tag, GameObject pooledObj, int count) {
        if(_availablePools.ContainsKey(tag))
            return _availablePools[tag];

        GameObject obj = new GameObject();
        ObjectPool pool = obj.AddComponent<ObjectPool>();

        pool.poolPrefabs = new List<GameObject>() {
            pooledObj,
        };
        
        _availablePools.Add(tag, pool);

        return pool;
    }

    public static ObjectPool FindPool(string tag) {
        if(_availablePools.ContainsKey(tag)) {
            return _availablePools[tag];
        }

        return null;
    }

    public static bool HasPool(string tag) {
        return _availablePools.ContainsKey(tag);
    }
}
