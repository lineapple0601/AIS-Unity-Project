﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyerMissileFire : MonoBehaviour
{
    /*作成者：MOON*/
    public bool basic_button = false;
    public bool final_button = false;
    public GameObject player_Ro;

    public GameObject PlayerMissile;
    public Transform MissileLocation;   //生成位置
    public Image final_buttonImage;     //finalAttackButton_Image
    public Button final_btn;            //finalAttackButton
    private float FireDelay;         //ミサイル速度
    private bool FireState;             //ミサイル速度制御

    public int MissileMaxPool;          //メモリープールに設定するミサイルの数
    private MemoryPool MPool;           //メモリープール
    private GameObject[] MissileArray;  //ミサイルの配列

    //攻撃SE
    private AudioSource BasicAttackSE;
    private AudioSource SpecialAttackSE;

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

        //AudioSourceコンポーネントを取得し、変数に格納
        AudioSource[] audioSources = GetComponents<AudioSource>();
        BasicAttackSE = audioSources[0];
        SpecialAttackSE = audioSources[1];
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
            if (Input.GetKey(KeyCode.J) || basic_button == true)
            {
                FinalAttack = false;    //必殺技非活性
                FireDelay = 0.5f;       //射撃のdelay設定
                StartCoroutine(FireCycleControl());
                BasicAttackSE.Play();
                for (int i = 0; i < MissileMaxPool; i++)
                {
                    if (MissileArray[i] == null) //空配列の場合
                    {
                        MissileArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                        MissileArray[i].transform.position = MissileLocation.transform.position;    //それの発射位置を設定する
                        MissileArray[i].transform.rotation = 
                            GameObject.Find("Destroyer").transform.rotation;  //それの発射方向を設定する
                        break;
                    }
                }
            }
            //必殺技ボタン
            if (Input.GetKey(KeyCode.K) || final_button == true)
            {
                FinalAttack = true;     //必殺技活性
                FireDelay = 1.25f;      //射撃のdelay設定
                SpecialAttackSE.Play();
            }
            if (FinalAttack == true)
            {
                //必殺技の時間
                if (timer <= timerForEnd)
                {
                    timer += Time.deltaTime;
                    StartCoroutine(CoolTime(4f));

                    for (int i = 0; i < MissileMaxPool; i++)
                    {
                        if (MissileArray[i] == null) //空配列の場合
                        {
                            Quaternion angle = GameObject.Find("Destroyer").transform.rotation; //それの発射方向を設定する
                            MissileArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                            MissileArray[i].transform.position = MissileLocation.transform.position;    //それの発射位置を設定する
                            if (i % 3 == 1)
                            {
                                MissileArray[i].transform.rotation = angle;     //球の方向がangleのまま
                            }
                            else if (i % 3 == 2)
                            {
                                MissileArray[i].transform.rotation = angle * Quaternion.Euler(0, 0, 30);     //球の方向をangleより30度回転させる
                            }
                            else if (i % 3 == 0)
                            {
                                MissileArray[i].transform.rotation = angle * Quaternion.Euler(0, 0, -30);     //球の方向をangleより-30度回転させる
                            }
                            break;
                        }
                    }
                }
                //必殺技の時間が終わったら基本攻撃に戻す
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

    //射撃間隔処理
    IEnumerator FireCycleControl()
    {
        FireState = false;
        yield return new WaitForSeconds(FireDelay);
        FireState = true;
    }

    //クールタイム処理
    IEnumerator CoolTime(float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            final_buttonImage.fillAmount = (1.0f / cool);   //buttonを埋める
            final_btn.enabled = false;                      //buttonを非活性
            yield return new WaitForFixedUpdate();          //Update待ち
            final_btn.enabled = true;                       //buttonを活性
        }
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
