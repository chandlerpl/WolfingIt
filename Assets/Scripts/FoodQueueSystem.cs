using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodQueueSystem : MonoBehaviour
{
    [System.Serializable]
    public class FoodItem {
        public GameObject foodPrefab;
        public Sprite foodSprite;
        public Sprite cancelledSprite;
        public float spawnDelay;
        [HideInInspector]
        public bool cancelled;
    }

    public FoodItem[] foodItems;
    public Vector2 foodSpawnPosition;
    public Image[] queueIcons;
    public Slider countdownSlider;

    private List<FoodItem> queuedFood;

    private Coroutine spawnFoodCoroutine;
    void Start()
    {
        queuedFood = new List<FoodItem>(queueIcons.Length - 1);
    }

    public void QueuePlate(int foodId) {
        if(!Timer.TimerIsRunning || queuedFood.Count >= queueIcons.Length - 1)
            return;

        FoodItem defaultItem = foodItems[foodId];

        FoodItem item = new FoodItem();
        item.foodPrefab = defaultItem.foodPrefab;
        item.spawnDelay = defaultItem.spawnDelay;
        item.foodSprite = defaultItem.foodSprite;
        item.cancelledSprite = defaultItem.cancelledSprite;

        queuedFood.Add(item);
        int i = queuedFood.Count;
        queueIcons[i].enabled = true;
        queueIcons[i].sprite = queuedFood[i - 1].foodSprite;

        if(spawnFoodCoroutine == null)
            spawnFoodCoroutine = StartCoroutine(SpawnFood());
    }

    public void DequeuePlate(int plateId) {
        if(queuedFood.Count >= plateId) {
            FoodItem item = queuedFood[plateId - 1];
            queueIcons[plateId].sprite = item.cancelledSprite;
            item.cancelled = true;
            
            StartCoroutine(RemovePlate(item));
        }
    }

    private IEnumerator RemovePlate(FoodItem item) {
        yield return new WaitForSeconds(1);
        
        queuedFood.Remove(item);
        for(int i = 1; i < queueIcons.Length; ++i) {
            if(queuedFood.Count > i - 1) {
                queueIcons[i].enabled = true;
                FoodItem queued = queuedFood[i - 1];
                queueIcons[i].sprite = queued.cancelled ? queued.cancelledSprite : queued.foodSprite;
            } else {
                queueIcons[i].enabled = false;
            }
        }
    }

    public IEnumerator SpawnFood() {
        FoodItem item = queuedFood[0];

        if(item.cancelled) {
            yield return new WaitForSeconds(0.5f);
            spawnFoodCoroutine = queuedFood.Count > 0 ? StartCoroutine(SpawnFood()) : null;
            yield break;
        }
        queuedFood.Remove(item);

        countdownSlider.gameObject.SetActive(true);
        countdownSlider.value = 0;
        
        queueIcons[0].enabled = true;
        queueIcons[0].sprite = item.foodSprite;
        for(int i = 1; i < queueIcons.Length; ++i) {
            if(queuedFood.Count > i - 1) {
                queueIcons[i].enabled = true;
                FoodItem queued = queuedFood[i - 1];
                queueIcons[i].sprite = queued.cancelled ? queued.cancelledSprite : queued.foodSprite;
            } else {
                queueIcons[i].enabled = false;
            }
        }

        float originalDelay = item.spawnDelay;
        while(item.spawnDelay > 0 && Timer.TimerIsRunning) {
            yield return new WaitForSeconds(0.05f);
            item.spawnDelay -= 0.05f;
            countdownSlider.value = 1 - (item.spawnDelay / originalDelay);
        }
        if(!Timer.TimerIsRunning)
            yield break;
        Instantiate(item.foodPrefab, foodSpawnPosition, item.foodPrefab.transform.rotation);
        
        countdownSlider.gameObject.SetActive(false);
        queueIcons[0].enabled = false;
        spawnFoodCoroutine = queuedFood.Count > 0 ? StartCoroutine(SpawnFood()) : null;
    }
}
