using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab; 
        [Min(1)] public int size = 1;
    }

    public static ObjectPoolManager Instance { get; private set; }

    [Header("Initial Pools")]
    [Tooltip("List of pools to initialize at the start of the game.")]
    [SerializeField]
    private List<Pool> pools = new List<Pool>();

    private readonly Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Multiple instances of ObjectPoolManager detected. Destroying the extra one.");
        }
    }

    private void Start()
    {
        InitializePools();
    }

    /// <summary>
    /// Initializes all pools defined in the inspector.
    /// </summary>
    private void InitializePools()
    {
        foreach (Pool pool in pools)
        {
            CreatePool(pool);
        }
    }

    /// <summary>
    /// Creates a pool for the given configuration.
    /// </summary>
    /// <param name="pool">The pool configuration to create.</param>
    private void CreatePool(Pool pool)
    {
        if (poolDictionary.ContainsKey(pool.tag))
        {
            Debug.LogError($"Pool with tag {pool.tag} already exists.");
            return;
        }

        Queue<GameObject> objectQueue = new Queue<GameObject>();

        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }

        poolDictionary.Add(pool.tag, objectQueue);
    }

    /// <summary>
    /// Retrieves an object from the pool with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the pool.</param>
    /// <returns>An active object from the pool, or null if the pool doesn't exist.</returns>
    public GameObject GetObjectFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} does not exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if (objectToSpawn == null)
        {
            Debug.LogError($"Object in pool {tag} is null. This may indicate an issue with pooling logic.");
            return null;
        }

        objectToSpawn.SetActive(true);
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    /// <summary>
    /// Adds a new pool dynamically at runtime.
    /// </summary>
    /// <param name="tag">The unique tag for the pool.</param>
    /// <param name="prefab">The prefab for the objects in the pool.</param>
    /// <param name="size">The initial size of the pool.</param>
    public void AddPool(string tag, GameObject prefab, int size)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Cannot add pool with tag {tag}, it already exists.");
            return;
        }

        Pool newPool = new Pool { tag = tag, prefab = prefab, size = size };
        CreatePool(newPool);
    }

    /// <summary>
    /// Checks if a pool with the given tag exists.
    /// </summary>
    /// <param name="tag">The tag to check.</param>
    /// <returns>True if the pool exists, false otherwise.</returns>
    public bool PoolExists(string tag)
    {
        return poolDictionary.ContainsKey(tag);
    }
}
