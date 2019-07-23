using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float MoveSpeed;
    bool tmp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tmp == true)
        {
            transform.Translate(Vector2.down * Time.deltaTime * MoveSpeed);

            if (transform.position.y <= (-4))
            {
                tmp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * MoveSpeed);

            if (transform.position.y >= 4)
            {
                tmp = true;
            }
        }
            

    }
}
