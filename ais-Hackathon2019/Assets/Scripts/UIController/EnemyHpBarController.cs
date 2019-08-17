using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBarController : MonoBehaviour
{
    public GameObject ShipExplosion;
    public GameObject Item;
    GameObject obj;
    public Image bar;
    public Text hptext;
    EnemyData enemyData;
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = new EnemyData(HP);
    }

    // Update is called once per frame
    void Update()
    {
        float randomNum = Random.Range(0f, 100f);
        EnemyHPbar();

        if (enemyData.GetHP() <= 0)
        { //HPが0より少ない場合
            obj = Instantiate(ShipExplosion, transform.position, Quaternion.identity);   //爆発アニメーション生成
            Destroy(gameObject); //敵を削除
            if (randomNum <= 10f)
            {
                Instantiate(Item, transform.position, Quaternion.identity);   //アイテム生成
            }

            Destroy(obj,3.417f);    //爆発アニメーション削除
            ScoreController.addScore(50000); // TODO : TestCode
            HpBarController.UpdateHPBar(0.9f); // TODO : TestCode
        }
    }

    public void EnemyHPbar()
    {
        float HP = enemyData.GetHP(); // hpを読み込む
        bar.fillAmount = HP / 100f; // バーを埋める
        hptext.text = string.Format("{0}/100", HP); //HPによるテキスト

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))  //ぶつかるタグが"Player Missile"`の場合
        {
            enemyData.DecreaseHp(10);    //HP-2
            //Debug.Log(gameObject.name + "残っているHP：" + enemyData.getHP());
        }
    }
}
