using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierBombMovement : MonoBehaviour
{
    //public GameObject BombRotation;
    public float MoveSpeed;
    Vector3 dir;
    float angle,finalAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Enemy"))
        {
            //航空母艦用
            dir = GameObject.FindWithTag("Enemy").transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            finalAngle = angle;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        else
        {
            transform.rotation = Quaternion.AngleAxis(finalAngle - 90, Vector3.forward);
        }


        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);

        if (pos.x > 1f || pos.y > 1f || pos.x < 0f || pos.y < 0f)
        {
            // missile非活性する。
            gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false; // missile非活性する。
        }

    }

    //Collider処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) //タグがEnemyの場合
        {
            GetComponent<Collider2D>().enabled = false; // missile非活性する。
        }
    }
}
