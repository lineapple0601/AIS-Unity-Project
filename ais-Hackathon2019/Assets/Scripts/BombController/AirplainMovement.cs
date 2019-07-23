using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplainMovement : MonoBehaviour
{

    public float MoveSpeed;     // 動きの速さ
    float ToPlayerDistance, ToEnemyDistance;   // objectとの距離

    //public bool homing;
    //public Vector3 disVec;
    public float timerForBack;   // 機体に戻りが始まるtime
    private float timer;        // timer

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        /*
        if (timer > timerForDel)
        {
            //Destroy(GameObject.FindWithTag("Enemy"));
            disVec = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        }
        else
        {
            timer += Time.deltaTime;
            if (homing)
            {
                disVec = (GameObject.FindWithTag("Enemy").transform.position - transform.position).normalized;
            }


        }
        transform.position += disVec * Time.deltaTime * MoveSpeed;
        transform.forward = disVec;
        transform.Translate(disVec * Time.deltaTime * MoveSpeed);
        */

        /*
            Transform trEnemy = GameObject.FindWithTag("Enemy").transform;
            Vector3 v3Target = (trEnemy.position - transform.position).normalized;

            float fDot = Vector3.Dot(v3Target, gameObject.transform.forward);
            if (fDot > 0.1f)
            {
                gameObject.transform.Rotate(Vector3.up, -10.0f);
            }
            else if (fDot < 0.1f)
            {
                gameObject.transform.Rotate(Vector3.up, 10.0f);
            }
            else
            {
                // 거의 직선상이니 직진.. 
            }

            fOldTime = Time.time;
        }
        */

        Vector3 dir;
        float angle;

        if (GameObject.FindWithTag("Player") != null)
        {
            ToPlayerDistance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

            if (GameObject.FindWithTag("Enemy") != null)
            {
                ToEnemyDistance = Vector3.Distance(GameObject.FindWithTag("Enemy").transform.position, transform.position);

                if ((timer >= timerForBack) || ToEnemyDistance < 2)
                {
                    timer = timerForBack;
                    dir = GameObject.FindWithTag("Player").transform.position - transform.position;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

                    if (ToPlayerDistance <= 0.5)
                    {
                        GetComponent<Collider2D>().enabled = false;
                        timer = 0;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                    dir = GameObject.FindWithTag("Enemy").transform.position - transform.position;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
                }
            }
            else
            {
                timer += Time.deltaTime;
                angle = Mathf.Atan2(Random.Range(10f, -10f), Random.Range(10f, -10f)) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

                if (timer >= timerForBack)
                {
                    timer = timerForBack;
                    dir = GameObject.FindWithTag("Player").transform.position - transform.position;
                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

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

    //collider処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))  //Playerとcollisionが発生した場合
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}