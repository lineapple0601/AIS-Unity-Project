using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMisslieFire : MonoBehaviour
{
    public GameObject PlayerMissile;
    public Transform MissileLocation;
    public float FireDelay;     //ミサイル速度
    private bool FireState;     //ミサイル速度制御

    public int MissileMaxPool;  //メモリープールに設定するミサイルの数
    private MemoryPool MPool;   //メモリープール
    private GameObject[] MissileArray;  //ミサイルの配列

    private void OnApplicationQuit()
    {
        MPool.Dispose();  //終了の時、メモリープールをクリアする
    }

    // Start is called before the first frame update
    void Start()
    {
        FireState = true;

        MPool = new MemoryPool();
        MPool.Create(PlayerMissile, MissileMaxPool); //オブジェクトをMAXプールの数分生成する
        MissileArray = new GameObject[MissileMaxPool];
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFire();
    }

    private void PlayerFire()
    {
        if (FireState) {
            if (Input.GetKey(KeyCode.A)) {
                StartCoroutine(FireCycleControl());

                for (int i = 0; i < MissileMaxPool; i++) {
                    if (MissileArray[i] = null) {     //空配列の場合
                        MissileArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                        MissileArray[i].transform.position = MissileLocation.transform.position;   //それの発射位置を設定する
                        break;
                    }
                }
                //Instantiate(fireball, MissileLocation.position, MissileLocation.rotation);
            }
        }
        for (int i = 0; i < MissileMaxPool; i++)
        {
            // 配列がTRUEの場合
            if (MissileArray[i])
            {
                // 配列の Collider2DがFALSEの場合
                if (MissileArray[i].GetComponent<Collider2D>().enabled == false)
                {
                    // またTRUEに設定
                    MissileArray[i].GetComponent<Collider2D>().enabled = true;
                    // ミサイルをメモリに返す
                    MPool.RemoveItem(MissileArray[i]);
                    // 配列クリア
                    MissileArray[i] = null;
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