using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    private SEManager _seManager;

    // Start is called before the first frame update
    void Start()
    {
        _seManager = GameObject.Find("SoundManager").GetComponent<SEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //ぶつかるタグが"Player"`の場合
        {
            _seManager.Play_Item();
            collision.gameObject.GetComponent<PlayerController>()._changeType = true;   //プレイヤーの変更フラグ
            //GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);

        }
    }
}
