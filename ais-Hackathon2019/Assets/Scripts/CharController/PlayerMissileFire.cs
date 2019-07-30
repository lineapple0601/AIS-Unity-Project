using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileFire : MonoBehaviour
{
    /*作成者：MOON*/
    public bool basic_button = false;
    public bool final_button = false;
    public GameObject player_Ro;


    public GameObject PlayerMissile;
    public Transform MissileLocation;
    public float FireDelay = 1;             //ミサイル速度
    private bool FireState;             //ミサイル速度制御

    public int MissileMaxPool;          //メモリープールに設定するミサイルの数
    private MemoryPool MPool;           //メモリープール
    private GameObject[] MissileArray;  //ミサイルの配列

    //joystickによる攻撃を選択
    bool FinalAttack;
    public float timerForEnd;   // 攻撃time
    private float timer;        // timer

    private void OnApplicationQuit()
    {
        //終了の時、メモリープールをクリアする
        MPool.Dispose();
    }

    void Start()
    {
        FireState = true;

        MPool = new MemoryPool();
        MPool.Create(PlayerMissile, MissileMaxPool);  //オブジェクトをMAXプールの数分生成する
        MissileArray = new GameObject[MissileMaxPool];
    }

    void Update()
    {   
        playerFire();
    }

    private void playerFire()
    {
        if (FireState)
        {
            //基本攻撃ボタン
            if (Input.GetKey(KeyCode.A) || basic_button == true)
            {
                //FinalAttack = false;
                //FireDelay = 1;
                StartCoroutine(FireCycleControl());
                
                for (int i = 0; i < MissileMaxPool; i++)
                {
                    if (MissileArray[i] == null) //空配列の場合
                    {
                        MissileArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                        MissileArray[i].transform.position = MissileLocation.transform.position;    //それの発射位置を設定する
                        //MissileArray[i].transform.position = player_Ro.GetComponentInChildren<Transform>().GetChild(0).transform.position;
                       // MissileArray[i].transform.rotation = player_Ro.GetComponentInChildren<Transform>().GetChild(0).transform.rotation;
                        break;
                    }
                }
            }
            //必殺技ボタン
            if (Input.GetKey(KeyCode.S) || final_button == true)
            {
                FinalAttack = true;
                FireDelay = 0;
            }
            if (FinalAttack == true)
            {
                //必殺技の時間
                if (timer <= timerForEnd)
                {
                    timer += Time.deltaTime;
                    StartCoroutine(FireCycleControl());

                    for (int i = 0; i < MissileMaxPool; i++)
                    {
                        if (MissileArray[i] == null) //空配列の場合
                        {
                            MissileArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                            MissileArray[i].transform.position = MissileLocation.transform.position;    //それの発射位置を設定する
                            //MissileArray[i].transform.position = player_Ro.GetComponentInChildren<Transform>().GetChild(0).transform.position;
                            //MissileArray[i].transform.rotation = player_Ro.GetComponentInChildren<Transform>().GetChild(0).transform.rotation;
                            break;
                        }
                    }
                }
                //必殺技の時間が終わったら
                else
                {
                    FinalAttack = false;
                    FireDelay = 1;
                    timer = 0;
                }
            }
        }

        for (int i = 0; i < MissileMaxPool; i++)
        {
            if (MissileArray[i])    // 配列がTRUEの場合
            {
                if (MissileArray[i].GetComponent<Collider2D>().enabled == false) // 配列の Collider2DがFALSEの場合
                {
                    MissileArray[i].GetComponent<Collider2D>().enabled = true;  // またTRUEに設定
                    MPool.RemoveItem(MissileArray[i]);  // ミサイルをメモリに返す
                    MissileArray[i] = null; // 配列クリア
                }
            }
        }

        /*作成者：MOON*/
        basic_button = false;
        final_button = false;
    }


    IEnumerator FireCycleControl()
    {
        FireState = false;
        yield return new WaitForSeconds(FireDelay);
        FireState = true;
    }

    /*作成者：MOON*/
    public void setBasicButton()
    {
        basic_button = true;
    }

    /*作成者：MOON*/
    public void setFinalButton()
    {
        final_button = true;
    }
}
