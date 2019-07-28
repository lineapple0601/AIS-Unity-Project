using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneBomb : MonoBehaviour
{
    public GameObject Bomb;
    public Transform BombLocation;
    public float FireDelay;             //ミサイル速度
    private bool FireState;             //ミサイル速度制御

    public int BombMaxPool;          //メモリープールに設定するミサイルの数
    private MemoryPool MPool;           //メモリープール
    private GameObject[] BombArray;  //ミサイルの配列

    public float timerForStop;   // 球打つことを止めるtime
    private float timer;         // timer

    private void OnApplicationQuit()
    {
        //終了の時、メモリープールをクリアする
        MPool.Dispose();
    }

    void Start()
    {
        timer = 0;        //初期化
        FireState = true;

        MPool = new MemoryPool();
        MPool.Create(Bomb, BombMaxPool);  //オブジェクトをMAXプールの数分生成する
        BombArray = new GameObject[BombMaxPool];
    }

    void Update()
    {
        //EnemyタグのObjectが存在する場合
        if (GameObject.FindWithTag("Enemy") != null)
        {
            //敵との距離を計算
            //敵のLayerがjoystickと同じパンネルに存在するので、position.zを0に設定しないと球を打たない
            float distance = Vector3.Distance(new Vector3(GameObject.FindWithTag("Enemy").transform.position.x, GameObject.FindWithTag("Enemy").transform.position.y, 0),
                                              new Vector3(transform.position.x, transform.position.y, 0));
            timer = timerForStop;        //

            //距離が5以下で、秒が生成されてからtimerForStop以内、両方満たす場合
            if ((distance <= 5) && (timer <= timerForStop))
                PlayerFire();
        }
        else { }
    }

    private void PlayerFire()
    {
        if (FireState)
        {
            timer += Time.deltaTime;
            StartCoroutine(FireCycleControl());

            for (int i = 0; i < BombMaxPool; i++)
            {
                if (BombArray[i] == null) //空配列の場合
                {
                    BombArray[i] = MPool.NewItem();  //プールでミサイルを持ってくる
                    BombArray[i].transform.position = BombLocation.transform.position;    //それの発射位置を設定する
                    break;
                }
            }
        }

        for (int i = 0; i < BombMaxPool; i++)
        {
            if (BombArray[i])    // 配列がTRUEの場合
            {
                if (BombArray[i].GetComponent<Collider2D>().enabled == false) // 配列のCollider2DがFALSEの場合
                {
                    BombArray[i].GetComponent<Collider2D>().enabled = true;  // またTRUEに設定
                    MPool.RemoveItem(BombArray[i]);  // ミサイルをメモリに返す
                    BombArray[i] = null; // 配列クリア
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
