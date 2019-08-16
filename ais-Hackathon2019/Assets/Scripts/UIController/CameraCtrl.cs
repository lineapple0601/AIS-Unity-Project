using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*作成者：MOON*/
public class CameraCtrl : MonoBehaviour
{
    public GameObject Player_1; //`プレイヤー
    public GameObject Player_2;
    public GameObject Player_3;
    public GameObject Player_4;
    public GameObject Player_Test;
    Transform AT;

    public GameObject SeaBackGround;

    // Start is called before the first frame update
    void Start()
    {
        userCheck();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        userCheck();

        transform.position = new Vector3(AT.position.x, AT.position.y, transform.position.z);
        /*
        RectTransform backGround = SeaBackGround.GetComponent<RectTransform>();
        float bg_max_x = backGround.position.x + backGround.sizeDelta.x / 2;
        float bg_min_x = backGround.position.x - backGround.sizeDelta.x / 2 + 10;
        float bg_max_y = backGround.position.y + backGround.sizeDelta.y / 2 - 25;
        float bg_min_y = backGround.position.y - backGround.sizeDelta.y / 2 + 19;

        Transform plyer = AT;
        float plyer_x = AT.position.x * 2f;
        float plyer_y = AT.position.y * 6f;
        */

        /*
        if ((plyer_x < bg_max_x && plyer_x > bg_min_x) && (plyer_y < bg_max_y && plyer_y > bg_min_y))
        {
            transform.position = new Vector3(AT.position.x, AT.position.y, transform.position.z);
        }
        if (plyer_x >= bg_max_x)
        {
            MoveControl();   
        }
        if (plyer_x <= bg_min_x)
        {
            MoveControl();
        }
        if (plyer_y >= bg_max_y)
        {
            MoveControl();
        }
        if(plyer_y <= bg_min_y)
        {
            MoveControl();
        }
        */

    }

    private void userCheck()
    {
        if (Player_1.activeSelf == true)
        {
            AT = Player_1.transform;
        }
        else if (Player_2.activeSelf == true)
        {
            AT = Player_2.transform;
        }
         else if (Player_3.activeSelf == true)
        {
            AT = Player_3.transform;
         }
        else if (Player_4.activeSelf == true)
         {
             AT = Player_4.transform;
        }
        else if (Player_Test.activeSelf == true)
        {
            AT = Player_Test.transform;
        }
    }

    /*
    void MoveControl()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;
    }*/

}
