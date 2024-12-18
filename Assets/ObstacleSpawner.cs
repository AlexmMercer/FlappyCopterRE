using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField, Tooltip("Time between obstacle spawns at the start.")]
    private float initialSpawnInterval = 5f;

    [SerializeField, Tooltip("Minimum time between obstacle spawns as difficulty increases.")]
    private float minSpawnInterval = 0.8f;

    [SerializeField, Tooltip("Height range for obstacle placement.")]
    private Vector2 spawnHeightRange = new Vector2(-3f, 3f);

    [SerializeField, Tooltip("Time to reach maximum difficulty.")]
    private float difficultyRampTime = 60f;

    private float currentSpawnInterval;
    private float spawnTimer;

    private void Start()
    {
        //currentSpawnInterval = initialSpawnInterval;
        spawnTimer = 0.0f;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnObstacle();
            spawnTimer = initialSpawnInterval;
        }

       // UpdateSpawnInterval();
    }

    /// <summary>
    /// Spawns an obstacle at a random height using the object pool.
    /// </summary>
    private void SpawnObstacle()
    {
        // Randomly select obstacle type
        string[] obstacleTags = { "ObstacleTopAndBottom" };
        string selectedTag = obstacleTags[Random.Range(0, obstacleTags.Length)];

        GameObject obstacle = ObjectPoolManager.Instance.GetObjectFromPool(selectedTag);
        if (obstacle != null)
        {
            // Set obstacle position
            float spawnHeight = Random.Range(spawnHeightRange.x, spawnHeightRange.y);
            obstacle.transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);

            // Reset rotation (in case pooled objects retain rotation)
            //obstacle.transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Dynamically decreases the spawn interval over time to increase difficulty.
    /// </summary>
    private void UpdateSpawnInterval()
    {
        if (currentSpawnInterval > minSpawnInterval)
        {
            float progress = Mathf.Clamp01(Time.time / difficultyRampTime);
            currentSpawnInterval = Mathf.Lerp(initialSpawnInterval, minSpawnInterval, progress);
        }
    }
}
