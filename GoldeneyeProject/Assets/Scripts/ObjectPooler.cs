using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int amountInPool;
    }

    #region Singleton
    public static ObjectPooler instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion


    public Dictionary<string, Queue<GameObject>> poolDict;
    public List<Pool> pools;


    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        InitialiseDictionary();
    }


    void InitialiseDictionary()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.amountInPool; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDict.Add(pool.tag, objPool);
        }
    }


    public void SpawnPooledObject(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDict.ContainsKey(tag))
        {
            return;
        }

        GameObject obj = poolDict[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = pos;
        obj.transform.rotation = rot;

        poolDict[tag].Enqueue(obj);
    }
}
