﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject enemyObj;
    public GameObject timerObj;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI CongratuText;
    public int enemyNum;
    public float gameTime = 300f;
    public float currentTime;
    private int tick = 0;
    private bool gameIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ScoreController.setScore(0);
        // ゲーム情報初期化
        timerObj = GameObject.Find("Timer");
        currentTime = gameTime;

        // プレイヤー初期化
        GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/Destroyer");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/BattleShip");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/Submarine");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/AirCraftCarrier");
        playerObj = Instantiate(createPlayerObj, new Vector3(0f, 0f, 0f), Quaternion.identity);
        playerObj.GetComponent<PlayerController>()._playerType = 0;
        playerObj.GetComponent<PlayerController>()._rotateAngle = -90f;

        // 敵初期化
        GameObject createEnemyObj = (GameObject)Resources.Load("Prefabs/EnemyA");
        enemyObj = Instantiate(createEnemyObj, new Vector3(20f, 0f, 0f), Quaternion.identity);
        enemyObj.GetComponent<EnemyController>()._enemyType = 0;
        enemyNum = 3;
    }

    // Update is called once per frame
    void Update()
    {
        SceneController();

        // ゲーム状況確認
        CheckGame();

        // プレイヤー確認＆更新処理
        CheckPlayer();

        // 敵確認＆更新処理
        CheckEnemy();

        // Invoke("Test", 4.0f); //DEBUG用
    }

    private void SceneController()
    {
        // スコア画面移行
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeToScoreScene();
        }
    }

    private void ChangeToScoreScene()
    {
        SceneManager.LoadScene("ScoreScene");
    }

    private void ChangeToScoreRegScene()
    {
        SceneManager.LoadScene("ScoreRegScene");
    }

    private void ChangeToPauseScene()
    {
        SceneManager.LoadScene("PauseScene");
    }

    private void CheckGame()
    {
        // ゲーム残存時間確認
        if (gameIsOver == false)
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime < 0f)
        {
            currentTime = 0f;
        }
        timerObj.GetComponent<TextMeshProUGUI>().SetText( ((int)currentTime).ToString() );

        // 時間経過でスコア上昇
        ScoreController.addScore(1);

        if(GameObject.FindWithTag("Item") == null && ScoreController.getScore() % 100 == 0)
        {

            Vector3 itemPos = new Vector3(playerObj.transform.position.x + Random.Range(-5f, 5f),
                                        playerObj.transform.position.y + Random.Range(-5f, 5f), 0);
            GameObject item = (GameObject)Resources.Load("Prefabs/Item");
            Instantiate(item, itemPos, Quaternion.identity);
        }

        tick++;
        if (tick % 30 == 0)
        {
            if (gameIsOver == false)
            {
                ScoreController.addScore(1);
            }

        }
    }

    private void CheckPlayer()
    {
        PlayerController player = playerObj.GetComponent<PlayerController>();
        if (!player._aliveFlg)
        {
            Destroy(playerObj);
            // ゲームオーバー(4秒後)
            if (gameIsOver == false)
            {
                StartCoroutine(GameOver());
                StartCoroutine(BlinkText("over"));
            }
        }

        if (player._changeType)
        {
            int type = UnityEngine.Random.Range(0, 4);
            GameObject createPlayerObj = new GameObject();

            switch (type)
            {
                case 0:
                    createPlayerObj = (GameObject)Resources.Load("Prefabs/Destroyer");
                    break;
                case 1:
                    createPlayerObj = (GameObject)Resources.Load("Prefabs/BattleShip");
                    break;
                case 2:
                    createPlayerObj = (GameObject)Resources.Load("Prefabs/Submarine");
                    break;
                case 3:
                    createPlayerObj = (GameObject)Resources.Load("Prefabs/AirCraftCarrier");
                    break;
                default:
                    break;
            }
            Vector3 beforePos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, 0);
            float beforeAng = playerObj.GetComponent<PlayerController>()._rotateAngle;
            Destroy(playerObj);
            playerObj = Instantiate(createPlayerObj, beforePos, Quaternion.identity);
            playerObj.GetComponent<PlayerController>()._rotateAngle = beforeAng;
            playerObj.GetComponent<PlayerController>()._playerType = type;

        }
    }

    private void CheckEnemy()
    {
        if (!enemyObj.GetComponent<EnemyController>()._aliveFlg)
        {
            GameObject createEnemyObj;
            float ranX = playerObj.transform.position.x + Random.Range(12.0f, 15.0f);
            float ranY = playerObj.transform.position.y + Random.Range(8.0f, -8.0f);

            Destroy(enemyObj);

            switch (enemyNum)
            {
                case 1:
                    ScoreController.addScore(1000);
                    createEnemyObj = (GameObject)Resources.Load("Prefabs/EnemyB");
                    enemyObj = Instantiate(createEnemyObj, new Vector3(ranX, ranY, 0f), Quaternion.identity);
                    enemyObj.GetComponent<EnemyController>()._enemyType = 1;
                    enemyNum = 2;
                    break;
                case 2:
                    ScoreController.addScore(3000);
                    createEnemyObj = (GameObject)Resources.Load("Prefabs/EnemyC");
                    enemyObj = Instantiate(createEnemyObj, new Vector3(ranX, ranY, 0f), Quaternion.identity);
                    enemyObj.GetComponent<EnemyController>()._enemyType = 2;
                    enemyNum = 3;
                    break;
                case 3:
                    ScoreController.addScore(5000);
                    createEnemyObj = (GameObject)Resources.Load("Prefabs/PowerfulBoss");
                    enemyObj = Instantiate(createEnemyObj, new Vector3(ranX, ranY, 0f), Quaternion.identity);
                    enemyObj.GetComponent<EnemyController>()._enemyType = 3;
                    enemyNum = 4;
                    break;
                case 4:
                    ScoreController.addScore(10000);
                    // ゲームクリア(4秒後)
                    if (gameIsOver == false)
                    {
                        StartCoroutine(GameClear());
                        StartCoroutine(BlinkText("end"));
                    }
                    break;
            }
        }
    }

    private IEnumerator GameClear()
    {
        gameIsOver = true;
        while ((int)currentTime > 0)
        {
            ScoreController.addScore(2);
            currentTime -= 1f;
            yield return new WaitForSecondsRealtime(0.003f);
        }
        yield return new WaitForSecondsRealtime(5f);
        // スコア画面移行
        ChangeToScoreRegScene();
    }

    private IEnumerator BlinkText(string type)
    {
        while(true)
        {
            if (type == "over")
            {
                if (GameOverText.text == "")
                {
                    GameOverText.text = "GAME OVER";
                }
                else
                {
                    GameOverText.text = "";
                }
               
            } else
            {
                if (CongratuText.text == "")
                {
                    CongratuText.text = "CONGRATULATIONS";
                }
                else
                {
                    CongratuText.text = "";
                }
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private IEnumerator GameOver()
    {
        Destroy(playerObj);
        gameIsOver = true;
        while ((int)currentTime > 0)
        {
            ScoreController.addScore(2);
            currentTime -= 1f;
            yield return new WaitForSecondsRealtime(0.003f);
        }
        yield return new WaitForSecondsRealtime(3f);
        // スコア画面移行
        ChangeToScoreRegScene();
    }

    private void Test()
    {
        playerObj.GetComponent<PlayerController>()._hp = 40;
    }
}
