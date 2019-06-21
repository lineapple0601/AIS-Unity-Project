using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Playerの動き機能

    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            //objectが画面から出られないよう設定
        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);  //objectの位置を持つ変数
        if (pos.x < 0.05f) pos.x = 0.05f;   //min x
        if (pos.x > 0.95f) pos.x = 0.95f;   //max x
        if (pos.y < 0.05f) pos.y = 0.05f;   //min y
        if (pos.y > 0.95f) pos.y = 0.95f;   //max y
        this.transform.position = Camera.main.ViewportToWorldPoint(pos);

        Move();
    }

        //キーボード操作によるobjectを動く
    private void Move()
    {
        //Time.delataTime すべての機器にて同じ速度で動けるため使う
        if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  //ぶつかったTagが"Enemy"`の場合
        {
            Destroy(gameObject);
        }
    }
}
