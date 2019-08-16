using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDownShipBombMovement : MonoBehaviour
{
    public float MoveSpeed;
    public GameObject PlayerGyourai;    //必殺攻撃の魚雷
    public GameObject PlayerMissile;    //基本攻撃の球

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
            if (PlayerMissile.activeSelf == true)
            {
                // missile非活性する。
                gameObject.SetActive(false);
                GetComponent<Collider2D>().enabled = false; // missile非活性する。
            }
            if (PlayerGyourai.activeSelf == true)
            {
                Debug.Log(PlayerGyourai.activeSelf);
                Destroy(PlayerGyourai);
            }
        }

    }

    //Collider処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) //タグがEnemyの場合
        {
            if (PlayerMissile.activeSelf == true)
            {
                Debug.Log(PlayerMissile.activeSelf);
                GetComponent<Collider2D>().enabled = false; // missile非活性する。
            }
            if (PlayerGyourai.activeSelf == true)
            {
                Debug.Log(PlayerGyourai.activeSelf);
                Destroy(PlayerGyourai);
            }
        }
    }
}
