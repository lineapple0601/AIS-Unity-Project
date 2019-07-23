using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
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
        PlayerHPbar();

        if (enemyData.GetHP() <= 0)
        { //HPが0より少ない場合
            Destroy(gameObject); //objectを削除
        }
    }

    public void PlayerHPbar()
    {
        float HP = enemyData.GetHP(); // hpを読み込む
        bar.fillAmount = HP / 100f; // バーを埋める
        hptext.text = string.Format("HP {0}/100", HP); //HPによるテキスト

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
