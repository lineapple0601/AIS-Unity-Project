using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKey("right"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }
}
