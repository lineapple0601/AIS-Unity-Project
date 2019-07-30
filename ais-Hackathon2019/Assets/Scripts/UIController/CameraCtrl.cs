using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*作成者：MOON*/
public class CameraCtrl : MonoBehaviour
{
    public GameObject Player_1; //`プレイヤー
    public GameObject Player_2;
    //public GameObject Player_3;
    //public GameObject Player_4;
    Transform AT;


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
       // else if (Player_3.activeSelf == true)
        //{
        //    AT = Player_3.transform;
       // }
        //else if (Player_4.activeSelf == true)
       // {
       //     AT = Player_4.transform;
        //}
    }


}
