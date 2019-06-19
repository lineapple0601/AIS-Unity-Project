using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int destroyScore = 100;

    public float moveSpeed = 0.5f;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }
    
    void MoveControl()
    {
        float yMove = moveSpeed * Time.deltaTime;
        transform.Translate(0, -yMove, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            
            //Rocket Tag와 접촉되었을때 이팩트생성
            Instantiate(explosion, transform.position, Quaternion.identity);

            SoundManager.instance.PlaySound();

            GameManager.instance.AddScore(destroyScore);

            //Rocket제거
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false);
            //자기자신제거
            Destroy(gameObject);
        }
    }

}
