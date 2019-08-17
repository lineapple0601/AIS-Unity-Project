using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤー初期化
        //playerObj = GameObject.FindWithTag("AirCraftCarrier");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/PullDownShip");
        //GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/PullDownShip");
        GameObject createPlayerObj = (GameObject)Resources.Load("Prefabs/Destroyer");
        Instantiate(createPlayerObj, new Vector3(0f, 0f, 0f), Quaternion.identity);
        playerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SceneController();
    }

    private void SceneController()
    {
        // ポーズ画面移行
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeToPauseScene();
        }

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
}
