using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed = 2.0f;

    public PlayerCtrl_joystick joystick; // PlayerController_Joystickのスクリプト
    public PauseManager paM;

    public float MoveSpeed; // プレイヤーの移動速度

    private Vector3 _moveVector; // プレイヤーの移動Vector
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform; //transform キャシング
        _moveVector = Vector3.zero; // プレイヤーの移動Vector初期化
    }

    // Update is called once per frame
    void Update()
    {
        //タッチパッド入力もらい
        HandleInput();

        /////Start
        ///キーボードのための操作
        if (Input.GetKey("up"))
        {
            transform.position += transform.up * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.up * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("right"))
        {
            transform.position += transform.right * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            paM.ActivePauseBt();
        }
        /////End
    }

    private void HandleInput()
    {
        //タッチパッド入力受信
        _moveVector = PoolInput();
    }

    public void FixedUpdate()
    {
        //プレイヤー移動
        Move();
    }

    private Vector3 PoolInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        return moveDir;
    }

    public void Move()
    {
        _transform.Translate(_moveVector * MoveSpeed * Time.deltaTime);
    }

}
