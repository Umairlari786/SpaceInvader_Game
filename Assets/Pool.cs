using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
[System.SerializableAttribute]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
};
public class Pool : MonoBehaviour
{
    public static Pool singleton;
    public AudioSource shipHit;
    public AudioSource bombHit;
    public List<PoolItem> items;
    public List<GameObject> pooledItems;
    private int planeCount = -1;
    private bool planeRespaw = true;
    private int TotalPlaneCount = 3;
    private int TotalScore = 0;
    private void Awake()
    {
        singleton = this;
    }

    public GameObject Get(string tag)
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
            {
                if(tag == "Plane" && planeCount < TotalPlaneCount && planeRespaw==true)
                {
                    planeCount++;
                }
                return pooledItems[i];
            }
        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledItems = new List<GameObject>();
        foreach(PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }
    public void SetPlaneSpawn()
    {
        TotalPlaneCount--;
        if(TotalPlaneCount==0)
        {
            planeRespaw = false;
        }
    }
    public bool GetPlaneSpawn()
    {
        return planeRespaw;
    }
    public int GetPlaneCount()
    {
        return planeCount;
    }
    
    public void IncreaseLife()
    {
        TotalPlaneCount++;
        if (TotalPlaneCount > 0)
        {
            planeRespaw = true;
        }
    }
    public int GetTotalLife()
    {
        return TotalPlaneCount;
    }
    public int GetTotalScore()
    {
        return TotalScore;
    }

    public void SetScore(int score)
    {
        TotalScore += score;
    }

    public void PlayShipHit()
    {
        shipHit.Play();
    }

    public void PlayEnemyHit()
    {
        bombHit.Play();
    }
}
