using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public TextMeshProUGUI gameMsg;
    public TextMeshProUGUI LifeMsg;
    public TextMeshProUGUI ScoreMsg;
    private bool gameStopped = false;
    private int speed = 2;
    // Start is called before the first frame update
    private Vector3 targetLocation;
    private Vector3 startLocation;
    private Vector3 endLocation;
    private Vector3 startPosition;
    private List<GameObject> pooledItems;

    private void Awake()
    {
        this.transform.position = new Vector2(-3.5f, 1.0f);
        //countItem = 20;
        startPosition = this.transform.position;
        targetLocation.y = this.transform.position.y;
        startLocation = this.transform.position + (new Vector3(-4.0f, 0.0f, 0.0f));
        endLocation = this.transform.position + (new Vector3(+3.5f, 0.0f, 0.0f));
        pooledItems = new List<GameObject>();
        createEnemies();
    }
    
    private void Start()
    {
        StartCoroutine(FirstWait());
    }
    private IEnumerator FirstWait()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(StartBombing());
    }

    private IEnumerator StartBombing()
    {
        while (Pool.singleton.GetPlaneSpawn())
        {
            GameObject b = Pool.singleton.Get("Bomb");
            if (b != null)
            {
                float randomX = Random.Range(-3.0f, 3.0f);
                float randomY = Random.Range(0.0f, 1.5f);
                // transform.position 
                b.transform.position = new Vector2(randomX, randomY);
                b.SetActive(true);
            }
            yield return new WaitForSeconds(4);
        }
    }
    void GameOver()
    {
        LifeMsg.text = (Pool.singleton.GetTotalLife()).ToString();
        gameStopped = true;
        ClearEnemies();
        gameMsg.enabled = true;
        
    }

    void ClearEnemies()
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            Destroy(pooledItems[i]);
        }
        
    }
    void createEnemies()
    {
        for (int row = 0; row < 5; row++)
        {
            float width = 0.5f * 3;
            float height = 0.5f * 4;
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row), 0.0f);

            for (int col = 0; col < 6; col++)
            {
                if(row == 0 || row == 1)
                {
                    GameObject a = Instantiate(enemy1, this.transform);
                    Vector3 position = rowPosition;
                    position.x += col * 2.0f;
                    a.transform.localPosition = position;
                    a.SetActive(true);
                    pooledItems.Add(a);
                }
                if (row == 2 || row == 3)
                {
                    GameObject a = Instantiate(enemy2, this.transform);
                    Vector3 position = rowPosition;
                    position.x += col * 2.0f;
                    a.transform.localPosition = position;
                    a.SetActive(true);
                    pooledItems.Add(a);
                }
                
                if(row == 4)
                {
                    GameObject a = Instantiate(enemy3, this.transform);
                    Vector3 position = rowPosition;
                    position.x += col * 2.0f;
                    a.transform.localPosition = position;
                    a.SetActive(true);
                    pooledItems.Add(a);
                }


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStopped == false)
        {
            MoveEnemy();
            RespawnEnemy();
            LifeMsg.text = (Pool.singleton.GetTotalLife()).ToString();
            ScoreMsg.text = (Pool.singleton.GetTotalScore()).ToString();
        }
        if(Pool.singleton.GetPlaneSpawn()==false)
        {
            GameOver();
        }
    }

    private void MoveEnemy()
    {
        // Move the enemy towards the target location
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);

        // Check if the enemy has reached the target location
        if (transform.position == targetLocation)
        {
            // Switch the target location to the other endpoint
            if (targetLocation == startLocation)
                targetLocation = endLocation;
            else
                targetLocation = startLocation;
        }
    }

    void RespawnEnemy()
    {
        bool reSpawn = true;
        
        //if (Pool.singleton.GetTotalLife() > 0 && Pool.singleton.GetPlaneSpawn() == true)
        if (Pool.singleton.GetPlaneSpawn() == true)
        {
            StartCoroutine(SpawnPlane());
        }
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (pooledItems[i].activeInHierarchy)
            {
                reSpawn = false;
            }
        }
        if (reSpawn)
        {
            speed++;
            Pool.singleton.IncreaseLife();
            for (int i = 0; i < pooledItems.Count; i++)
            {
                pooledItems[i].SetActive(true);
            }
        }
        
    }

    private IEnumerator SpawnPlane()
    {
        yield return new WaitForSeconds(1);
        if (Pool.singleton.GetTotalLife() > 0)
        {
            GameObject p = Pool.singleton.Get("Plane");
            if (p != null)
            {
                p.transform.position = new Vector2(0.00f, -4.5f);
                p.SetActive(true);
            }
        }
    }
}
