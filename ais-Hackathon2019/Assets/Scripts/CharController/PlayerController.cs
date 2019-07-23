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

    //プレイヤーのオブジェクト
    private bool facingRight = true;
    private Rigidbody2D rd;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        _transform = transform; //transform キャシング
        _moveVector = Vector3.zero; // プレイヤーの移動Vector初期化
        Time.timeScale = 1.0f;
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
        float hor = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");

        // 左もしくは、右に移動中、0.1f以上の場合
        // JoyStick および、keybordでも動く
        if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f || joystick.GetHorizontalValue() > 0.1f || joystick.GetHorizontalValue() < -0.1f)
        {
            if ((Input.GetAxis("Horizontal") > 0.1f && !facingRight) || (joystick.GetHorizontalValue() > 0.1f && !facingRight))      //facingRightが falseで右移動キーを押した場合
            {
                Flip();
                facingRight = true;
            }
            else if ((Input.GetAxis("Horizontal") < -0.1f && facingRight) || (joystick.GetHorizontalValue() < -0.1f && facingRight))     // facingRightがtrueで左移動キーを押した場合
            {
                Flip();
                facingRight = false;
            }
        }


        _transform.Translate(_moveVector * MoveSpeed * Time.deltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.GetChild(0).localScale;
        theScale.x *= -1;             // 1 = 右方向, -1 = 左方向
        transform.GetChild(0).localScale = theScale;
    }


}
