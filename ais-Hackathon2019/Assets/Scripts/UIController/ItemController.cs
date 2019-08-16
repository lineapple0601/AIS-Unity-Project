using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject aircraft;
    public GameObject destroyer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  //ぶつかるタグが"Player"`の場合
        {
            Vector3 beforePosition;
            GetComponent<Collider2D>().enabled = false;

            if (destroyer.activeSelf == true)
            {
                beforePosition = destroyer.transform.position;
                destroyer.SetActive(false);
                aircraft.SetActive(true);
                aircraft.transform.position = beforePosition;
            }
            if (aircraft.activeSelf == true)
            {
                beforePosition = aircraft.transform.position;
                aircraft.SetActive(false);
                destroyer.SetActive(true);
                destroyer.transform.position = beforePosition;
            }
        }
    }
}
