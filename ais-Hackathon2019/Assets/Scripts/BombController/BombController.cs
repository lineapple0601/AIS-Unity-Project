using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    //public GameObject BombRotation;
    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x > 1f || pos.y > 1f || pos.x < 0f || pos.y < 0f)
        {
            gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false; // missile非活性する
        }
    }
}