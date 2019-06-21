using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP;
    private EnemyData enemyData;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = new EnemyData(HP);
        //Debug.Log(gameObject.name + "残っているHP：" + enemyData.getHP());
    }

    private void Update()
    {
        if (enemyData.getHP() <= 0) { //HPが0より少ない場合
            Destroy(gameObject); //objectを削除
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Missile"))  //ぶつかるタグが"Player Missile"`の場合
        {
            enemyData.decreaseHp(2);    //HP-2
            //Debug.Log(gameObject.name + "残っているHP：" + enemyData.getHP());
        }
    }
}
