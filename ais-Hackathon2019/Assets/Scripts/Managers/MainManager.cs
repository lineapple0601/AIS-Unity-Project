using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject enemyObj;
    public GameObject timerObj;
    public int enemyNum;
    public float gameTime = 300f;
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム情報初期化
        ScoreController.setScore(0);
        timerObj = GameObject.Find("Timer");
        currentTime = gameTime;

        // プレイヤー初期化
        GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/Destroyer");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/BattleShip");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/Submarine");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/AirCraftCarrier");
        playerObj = Instantiate(createPlayerObj, new Vector3(0f, 0f, 0f), Quaternion.identity);
        playerObj.GetComponent<PlayerController>()._playerType = 0;

        // 敵初期化
        GameObject createEnemyObj = (GameObject)Resources.Load("Prefabs/EnemyA");
        enemyObj = Instantiate(createEnemyObj, new Vector3(20f, 0f, 0f), Quaternion.identity);
        enemyObj.GetComponent<EnemyController>()._enemyType = 0;
        enemyNum = 1;
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

    private void ChangeToPauseScene()
    {
        SceneManager.LoadScene("PauseScene");
    }

    private void CheckGame()
    {
        // ゲーム残存時間確認
        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 0f;
        }
        timerObj.GetComponent<TextMeshProUGUI>().SetText( ((int)currentTime).ToString() );

        // 時間経過でスコア上昇
        ScoreController.addScore(1);
    }

    private void CheckPlayer()
    {
        if (!playerObj.GetComponent<PlayerController>()._aliveFlg)
        {
            // ゲームオーバー(4秒後)
            Invoke("GameOver", 4.0f);
        }
    }

    private void CheckEnemy()
    {
        if (!enemyObj.GetComponent<EnemyController>()._aliveFlg)
        {
            GameObject createEnemyObj;
            float ranX = Random.Range(12.0f, 18.0f);
            float ranY = Random.Range(8.0f, -8.0f);

            Destroy(enemyObj);

            switch (enemyNum)
            {
                case 1:
                    ScoreController.addScore(10000);
                    createEnemyObj = (GameObject)Resources.Load("Prefabs/EnemyB");
                    enemyObj = Instantiate(createEnemyObj, new Vector3(ranX, ranY, 0f), Quaternion.identity);
                    enemyObj.GetComponent<EnemyController>()._enemyType = 1;
                    enemyNum = 2;
                    break;
                case 2:
                    ScoreController.addScore(30000);
                    createEnemyObj = (GameObject)Resources.Load("Prefabs/EnemyC");
                    enemyObj = Instantiate(createEnemyObj, new Vector3(ranX, ranY, 0f), Quaternion.identity);
                    enemyObj.GetComponent<EnemyController>()._enemyType = 2;
                    enemyNum = 3;
                    break;
                case 3:
                    ScoreController.addScore(50000);
                    createEnemyObj = (GameObject)Resources.Load("Prefabs/PowerfulBoss");
                    enemyObj = Instantiate(createEnemyObj, new Vector3(-20f, 0f, 0f), Quaternion.identity);
                    enemyObj.GetComponent<EnemyController>()._enemyType = 3;
                    enemyNum = 4;
                    break;
                case 4:
                    ScoreController.addScore(100000);
                    // ゲームクリア(4秒後)
                    Invoke("GameClear", 4.0f);
                    break;
            }
        }
    }

    private void GameClear()
    {
        // 残り時間によるスコア加算を行う
        ScoreController.addScore(100 * (int)currentTime);

        // スコア画面移行
        ChangeToScoreScene();
    }

    private void GameOver()
    {
        Destroy(playerObj);
        // スコア画面移行
        ChangeToScoreScene();
    }

    private void Test()
    {
        playerObj.GetComponent<PlayerController>()._hp = 40;
    }
}
