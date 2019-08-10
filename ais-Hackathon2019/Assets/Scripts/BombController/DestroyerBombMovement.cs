using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBombMovement : MonoBehaviour
{
    //public GameObject BombRotation;
    public float MoveSpeed;
    Vector3 dir;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = GameObject.Find("Player2").GetComponentInChildren<Transform>().GetChild(0).transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = GameObject.Find("Player2").GetComponentInChildren<Transform>().GetChild(0).transform.rotation;
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

