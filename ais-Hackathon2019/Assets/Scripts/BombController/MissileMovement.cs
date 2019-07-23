using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{

    public float MoveSpeed;
    public float DestroyXPos;
    public float DestroyYPos;
    Vector3 dir;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        dir = GameObject.FindWithTag("Enemy").transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

        if (transform.position.x >= DestroyXPos)
        {
            // missile非活性する。
            GetComponent<Collider2D>().enabled = false;
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
