using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject badItem;
    public GameObject goodItem;
    public GameObject goodItem2;
    public GameObject rareItem;

    public float spawnRate = 1.5f;
    float currentSpawnRate;
    public float fallSpeedMultiplier = 1f;

    public float minX = -2.5f;
    public float maxX = 2.5f;

    void Start()
    {
        currentSpawnRate = spawnRate;
        InvokeRepeating("SpawnItem", 1f, currentSpawnRate);
    }

    void SpawnItem()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(randomX, transform.position.y, 0);

        float roll = Random.value;

        GameObject item;

        if (roll < 0.5f)
    	    item = Instantiate(badItem, pos, Quaternion.identity);
	else if (roll < 0.95f) {
            float goodRoll = Random.value;
            if (goodRoll < 0.5f)
            {
                item = Instantiate(goodItem, pos, Quaternion.identity);
            }
            else
            {
                item = Instantiate(goodItem2, pos, Quaternion.identity);
            } 
        }
	else
            item = Instantiate(rareItem, pos, Quaternion.identity);

	item.GetComponent<Rigidbody2D>().gravityScale *= fallSpeedMultiplier;
    }

    public void IncreaseDifficulty()
    {
        if (currentSpawnRate > 0.5f)
        {
            currentSpawnRate -= 0.1f;

            fallSpeedMultiplier += 0.15f;

            CancelInvoke("SpawnItem");
            InvokeRepeating("SpawnItem", currentSpawnRate, currentSpawnRate);
        }
    }
}