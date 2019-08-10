using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*作成者：MOON*/
    public PlayerCtrl_joystick joystick; // PlayerController_Joystickのスクリプト
    public PlayerCtrl_Joystick_Ro joystickRo; // PlayerController_Joystick_Roのスクリプト
    public PauseManager paM;

    /*作成者：MOON*/
    public float MoveSpeed; // プレイヤーの移動速度

    /*作成者：MOON*/
    private Vector3 _moveVector; // プレイヤーの移動Vector
    private Transform _transform;

    /*作成者：MOON*/
    //プレイヤーのオブジェクト
    private bool facingRight = true;
    private Rigidbody2D rd;

    // 戦艦タイプ
    private BattleShipType Type;

    /*作成者：MOON*/
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        _transform = transform; //transform キャシング
        _moveVector = Vector3.zero; // プレイヤーの移動Vector初期化
        Time.timeScale = 1.0f;
        this.Type = new BasicShip(); // 基本戦艦
    }

    /*作成者：MOON*/
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paM.ActivePauseBt();
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    /*作成者：MOON*/
    private void HandleInput()
    {
        //タッチパッド入力受信
        _moveVector = PoolInput();
    }

    /*作成者：MOON*/
    public void FixedUpdate()
    {
        //プレイヤー移動
        Move();
    }

    /*作成者：MOON*/
    private Vector3 PoolInput()
    {   
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();

        Vector3 moveDir = new Vector3(h, v, 0).normalized;
        return moveDir;
    }

    /*作成者：MOON*/
    public void Move()
    {

        float rotate_speed = _transform.GetChild(0).rotation.z;


        float h_rotation = joystickRo.GetHorizontalValue();
        float v_rotation = joystickRo.GetVerticalValue();

        if (transform.rotation.z <= -360 || transform.rotation.z >= 360)
        {
            transform.Rotate(0, 0, 0);
        }

       if ((h_rotation > 0 && v_rotation > 0) || (h_rotation > 0 && v_rotation < 0))
        {
            _transform.GetChild(0).Rotate(0,0, rotate_speed * -h_rotation * v_rotation - 1);
        }
       else if ((h_rotation < 0 && v_rotation > 0) || (h_rotation < 0 && v_rotation < 0))
        {
            _transform.GetChild(0).Rotate(0, 0, rotate_speed * h_rotation * v_rotation + 1);
        }

        _transform.Translate(_moveVector * MoveSpeed * Time.deltaTime);
    }
}
