using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombA : MonoBehaviour
{
    public float MoveSpeed;
    Vector3 dir;
    float angle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);

        if (pos.x > 1f || pos.y > 1f || pos.x < 0f || pos.y < 0f)
        {
            // missile非活性する。
            Destroy(gameObject);
        }
    }

    //Collider処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) //タグがEnemyの場合
        {
            Destroy(gameObject);
        }
    }
}
