using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ObjectsPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform AssignedTarget;
    }
    [HideInInspector]
    public static ObjectsPool instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    [SerializeField] Transform poolFolder;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> poolqueue = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject prefab = Instantiate(pool.prefab, poolFolder);
                prefab.GetComponent<ZombieAI>().AssignedTarget = pool.AssignedTarget;
                poolqueue.Enqueue(prefab);
                prefab.SetActive(false);
            }
            poolDictionary.Add(pool.tag, poolqueue);
        }
    }

    public void SpawnFromPool(string tag, Vector3 position,Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool dictionary don't contain this key");
        }
        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.GetComponent<ZombieAI>().enabled = true;
        obj.GetComponent<Animator>().enabled = true;
        if (tag.Equals("Zombie"))
        {
            obj.GetComponent<NavMeshAgent>().Warp(position);
        }
        else
        {
            obj.transform.position = position;
        }
        obj.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(obj);
    }
}
