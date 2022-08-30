using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static int Customers = 0;

    [Serializable]
    public class LockedSpawner {
        public GameObject spawner;
        public float unlockTime;
    }
    public GameObject[] startingSpawners;
    public LockedSpawner[] lockedSpawners;
    
    public float respawnTime = 5;

    private List<GameObject> currentSpawners;

    public int _customerLimit = 10;
    
    
    private void Start()
    {
        currentSpawners = new List<GameObject>();

        Customers = 0;

        foreach(GameObject obj in startingSpawners) {
            currentSpawners.Add(obj);
        }
        foreach(LockedSpawner spawner in lockedSpawners) {
            StartCoroutine(UnlockSpawner(spawner));
        }

        StartCoroutine(SpawnCustomer());
        StartCoroutine(CustomerTimerLimit());
    }

    IEnumerator SpawnCustomer() {
        while(Timer.TimerIsRunning) {
            yield return new WaitForSeconds(respawnTime);

            if(Customers < _customerLimit) {
                GameObject customerTest = ObjectPool.FindPool("Customers").Pop();
                customerTest.transform.position = currentSpawners[UnityEngine.Random.Range(0, currentSpawners.Count)].transform.position;
                Customers++;
            }
        }
    }

    IEnumerator CustomerTimerLimit() {
        yield return new WaitForSeconds(30f);
        //_customerLimit = 3;
        yield return new WaitForSeconds(20f);
        //_customerLimit = 6;
    }

    public IEnumerator UnlockSpawner(LockedSpawner spawner) {
        yield return new WaitForSeconds(spawner.unlockTime);

        currentSpawners.Add(spawner.spawner);
    }
}
