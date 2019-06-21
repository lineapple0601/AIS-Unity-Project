using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileFire : MonoBehaviour
{

    public GameObject PlayerMissile;    
    public Transform MissileLocation;   
    public float FireDelay;             //ミサイル速度
    private bool FireState;             //ミサイル速度制御

    public int MissileMaxPool;          //メモリープールに設定するミサイルの数
    private MemoryPool MPool;           //メモリープール
    private GameObject[] MissileArray;  //ミサイルの配列

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
            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(FireCycleControl());

                for (int i = 0; i < MissileMaxPool; i++)
                {
                    if (MissileArray[i] == null) //空配列の場合
                    {
                        MissileArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                        MissileArray[i].transform.position = MissileLocation.transform.position;    //それの発射位置を設定する
                        break;
                    }
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
    }

    IEnumerator FireCycleControl()
    {
        FireState = false;
        yield return new WaitForSeconds(FireDelay);
        FireState = true;
    }
}
