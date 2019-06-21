using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileMove : MonoBehaviour
{
    //ミサイルのコントロール機能

    public float MoveSpeed;     //ミサイルの速度
    public float DestroyYPos;  //Y差表の限界値
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector2.up * MoveSpeed * Time.deltaTime);
        if (transform.position.y >= DestroyYPos) {
            Destroy(gameObject);
        }
    }
}
