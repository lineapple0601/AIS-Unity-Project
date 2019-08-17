using System.Collections;
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
}
