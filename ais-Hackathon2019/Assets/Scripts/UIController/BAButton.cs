using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAButton : MonoBehaviour
{
    private PlayerController _playerCtr;

    public void OnClick()
    {
        GameObject playerObj = GameObject.Find("GameManager").GetComponent<MainManager>().playerObj;
        _playerCtr = playerObj.GetComponent<PlayerController>();
        // プレイヤーの通常攻撃トリガー
        _playerCtr.BasicAtack();
    }
}