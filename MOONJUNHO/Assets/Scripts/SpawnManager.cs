using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    List<GameObject> enemies = new List<GameObject>();//적 캐릭터를 리스트에 담을 리스트 배열을 만든다

    Vector3[] positions = new Vector3[6];
    public GameObject enemy;
    public bool isSpawn = false;
    public float spawnDelay = 1.6f;
    float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        CreatePositions();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    void CreatePositions() // 적이 나오는 지점을 카메라의 월드좌표로 정의한다
    {
        float viewPosY = 1.2f;
        float gapX = 1f / 6f;
        float viewPosX = 0f;

        for (int i = 0; i < positions.Length; i++)
        {
            viewPosX = gapX + gapX * i;
            Vector3 viewPos = new Vector3(viewPosX,viewPosY,0);
            Vector3 WorldPos = Camera.main.ViewportToWorldPoint(viewPos);
            WorldPos.z = 0f;
            positions[i] = WorldPos;
        }
    }

    void SpawnEnemy() //isSpawn이 true일때 적을 랜덤하게 생성
    {
        if (isSpawn == true)
        {
            if (spawnTimer > spawnDelay)
            {
                int rand = Random.Range(0, positions.Length);

                GameObject enemyObj = Instantiate(enemy,positions[rand],Quaternion.identity) as GameObject;
                enemies.Add(enemyObj);

                spawnTimer = 0f;
            }
            spawnTimer += Time.deltaTime;
        }
    }

    private void Awake()
    {
        if (SpawnManager.instance == null)
        {
            SpawnManager.instance = this;
        }
    }

    public void ClearEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
        }
        enemies.Clear();
    }
}
