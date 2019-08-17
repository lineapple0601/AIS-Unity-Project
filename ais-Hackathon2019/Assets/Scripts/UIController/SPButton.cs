using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPButton : MonoBehaviour
{
    private PlayerController _playerCtr;

    public void OnClick()
    {
        GameObject playerObj = GameObject.Find("GameManager").GetComponent<MainManager>().playerObj;
        _playerCtr = playerObj.GetComponent<PlayerController>();
        // プレイヤーの特殊攻撃トリガー
        _playerCtr.SpecialAtack();
    }
}
