using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject player;

    int score = 0;

    public bool isPlayerAlive = true;

    public Text scoreText;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("StartGame", 3f);//3초 후 StartGame실행
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }
    
    void StartGame()
    {
        player.GetComponent<Player>().canShoot = true;

        SpawnManager.instance.isSpawn = true;
    }

    public void AddScore(int enemyScore)//적을 격파할때마다 점수가 올라가는 함수
    {
        score += enemyScore;
        scoreText.text = "Score" + score;
    }

    public void KillPlayer()
    {
        isPlayerAlive = false;
        SpawnManager.instance.isSpawn = false;
        TextControl.instance.ShowGameOver();
    }

    public void ResetGame()
    {
        ObjectManager.instance.ClearBullets();//ObjectManager에서 총알을 모두 사라지게 하는 기능을 추가함
        SpawnManager.instance.ClearEnemies();//SpawnManager에서 적을 모두 사라지게 함

        score = 0;
        scoreText.text = string.Empty;

        TextControl.instance.Restart();//TextControl에서 게임 재시작
        Invoke("RetryGame", 3f);//3초 후 게임 재시작
    }

    void RetryGame()
    {
        StartGame();
        player.SetActive(true);
    }
}
