﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*作成者：MOON*/
public class CameraCtrl : MonoBehaviour
{
    // TODO カメラ追従のプレイヤーは一機に絞る
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void LateUpdate()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        // カメラのプレイヤー追従処理
        transform.position = new Vector3(playerObject.transform.position.x,
                                         playerObject.transform.position.y,
                                         transform.position.z
        );
    }

    /*
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
        else if (Player_Test.activeSelf == true)
        {
            AT = Player_Test.transform;
        }
    }
    */
}
