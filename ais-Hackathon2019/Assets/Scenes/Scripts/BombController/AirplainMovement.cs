using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplainMovement : MonoBehaviour
{
    public float MoveSpeed;     // 動きの速さ
    float ToPlayerDistance, ToEnemyDistance;   // objectとの距離

    public float timerForBack;   // 機体へ戻る時点のtime
    private float timer;        // timer
    private bool tmp = false;

    void Start()
    {

    }

    void Update()
    {
        //Playerのタグを持ってるObjectが存在する場合
        if (GameObject.FindWithTag("Player") != null)
        {
            //Objectとの距離を計算
            ToPlayerDistance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

            //Enemyのタグを持ってるObjectが存在する場合
            if (GameObject.FindWithTag("Enemy") != null)
            {
                //Objectとの距離を計算
                //敵のLayerがjoystickと同じパンネルに存在するので、position.zを0に設定しないと球を打たない
                ToEnemyDistance = Vector3.Distance(new Vector3(GameObject.FindWithTag("Enemy").transform.position.x, GameObject.FindWithTag("Enemy").transform.position.y, 0),
                                                    new Vector3(transform.position.x, transform.position.y, 0));

                //Objectとの距離が2未満で、秒が生成されてからtimerForBack以上、両方満たす場合
                if ((timer >= timerForBack) || ToEnemyDistance < 2)
                {
                    //Objectが向かう方向を計算
                    timer = timerForBack;
                    ObjectHoukouKeisan("Player");

                    //Playerが0.5以下の場合
                    if (ToPlayerDistance <= 0.5)
                    {
                        GetComponent<Collider2D>().enabled = false;     //非活性する
                        timer = 0;
                    }
                }
                //Object距離が2以上で、秒が生成されてからtimerForBack未満、両方満たす場合
                else
                {
                    //Objectが向かう方向を計算
                    timer += Time.deltaTime;
                    ObjectHoukouKeisan("Enemy");
                }
            }
            //Enemyのタグを持ってるObjectが存在しない場合
            else
            {
                float angle = 0;

                //飛行機の方向を1回だけ設定するため
                if (tmp == false)
                {
                    angle = Mathf.Atan2(GameObject.FindWithTag("Player").transform.position.y, GameObject.FindWithTag("Player").transform.position.x) * Mathf.Rad2Deg;
                    //angle = Mathf.Atan2(Random.Range(10f, -10f), Random.Range(10f, -10f)) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    tmp = true;
                }
                
                gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
                timer += Time.deltaTime;

                if (timer >= timerForBack)
                {
                    //Objectが向かう方向を計算
                    timer = timerForBack;
                    ObjectHoukouKeisan("Player");

                    if (ToPlayerDistance <= 0.5)
                    {
                        GetComponent<Collider2D>().enabled = false;
                        timer = 0;
                    }
                }
            }
        }
        else { }
    }

    //Objectが向かう方向を計算
    private void ObjectHoukouKeisan(string TagName)
    {
        Vector3 dir;
        float angle;

        dir = GameObject.FindWithTag(TagName).transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
    }

    //collider処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))  //Playerとcollisionが発生した場合
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}