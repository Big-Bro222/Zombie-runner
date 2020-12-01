using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSpawn : MonoBehaviour
{
    private enum SpawnType { Zombie,PickUp};
    public Color GizmoColor;
    [SerializeField] private List<GameObject> Prefabs;
    [SerializeField] private SpawnType spawnType;
    [SerializeField] float waveSpawnTime;
    [SerializeField] Transform spawnFolder;
    [SerializeField] int Wave;
    [SerializeField] int SpawnAmount;
    [SerializeField] float InstantiateHeight;
    //public TextMeshProUGUI text;
    public bool isPlaying;
    private float timeCount;
    private int spawnPointCount;
    private int totalZombieCount;
    private void Start()
    {
        Wave = 0;
        isPlaying = true;
        timeCount = 0;
        spawnPointCount = transform.childCount;
        totalZombieCount = 0;
        //if (spawnType == SpawnType.Zombie)
        //{
        //    UpdateUI();
        //}
    }

    //private void UpdateUI()
    //{
    //    text.text = totalZombieCount.ToString();
    //}


    private void Update()
    {
        if (isPlaying)
        {
            timeCount += Time.deltaTime;
            if (timeCount > waveSpawnTime)
            {
                timeCount -= waveSpawnTime;
                if (spawnType == SpawnType.Zombie)
                {
                    SpawnEnemyWave();
                }
                if (spawnType == SpawnType.PickUp)
                {
                    SpawnPickUpWave();
                }
            }
        }
    }

    private void SpawnPickUpWave()
    {
        List<int> spawnPointIndex = GetRandomList(SpawnAmount);

        Wave++;
        foreach(int index in spawnPointIndex)
        {
            int prefabIndex = UnityEngine.Random.Range(0, Prefabs.Count - 1);
            GameObject item = Prefabs[prefabIndex];
            InstantiateHeightSetUp(item, transform.GetChild(index).position);
        }
    }

    private void InstantiateHeightSetUp(GameObject item, Vector3 position)
    {
        if(Physics.Raycast(position,Vector3.down,out RaycastHit hitInfo))
        {
            Vector3 InstantiatePosition = hitInfo.point + Vector3.up * InstantiateHeight;
            Instantiate(item, InstantiatePosition, Quaternion.identity, spawnFolder);

        }
    }


    private void SpawnEnemyWave()
    {
        if (SpawnAmount > transform.childCount)
        {
            Debug.LogError("you have selected too much spawnpoint in " + transform.name);
        }
        List<int> spawnPointIndex = GetRandomList(SpawnAmount);

        Wave++;
        foreach (int index in spawnPointIndex)
        {
            ObjectsPool.instance.SpawnFromPool("Zombie", transform.GetChild(index).position, Quaternion.identity);
        }

        //foreach (Transform spawnPoint in transform)
        //{
        //    //int prefabIndex = UnityEngine.Random.Range(0, Prefabs.Count - 1);
        //    //GameObject item = Prefabs[prefabIndex];
        //    ObjectsPool.instance.SpawnFromPool("Zombie", spawnPoint.position, Quaternion.identity);
        //    //Instantiate(item, spawnPoint.position, Quaternion.identity, spawnFolder);
        //}
        totalZombieCount += SpawnAmount;
        //UpdateUI();
    }

    private List<int> GetRandomList(int listCount)
    {
        List<int> spawnPointIndex = new List<int>();
        while (listCount > 0)
        {
            System.Random rand = new System.Random((int)DateTime.Now.Ticks);
            int random = rand.Next(0, spawnPointCount - 1);
            if (!spawnPointIndex.Contains(random))
            {
                spawnPointIndex.Add(random);
                listCount--;
            }
        }
        return spawnPointIndex;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        foreach(Transform spawnPoint in transform)
        {
            Gizmos.DrawSphere(spawnPoint.position, 0.5f);
        }
    }



}
