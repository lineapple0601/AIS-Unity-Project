using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // 定数
    public float MAX_ROTATION_ANGLE = 1; // フレーム単位の最大回転角度
    // 公開変数
    public int          _hp;            // HP
    bool _moveFlg;
    public float        _speed;         // 速度
    public Vector3      _direction;     // 方向
    public Joystick     _joystick;      // Joystickコントローラ
    // 内部変数
    private float       _rotateAngle;   // 回転角(0.0f~359.0f)
    private Vector3     _moveVector;    // 船の移動Vector
    private Transform   _transform;
    private Rigidbody2D _rd;

    // Start is called before the first frame update
    void Start()
    {
        // 初期配置
        _speed = 1.5f;
        transform.position = Vector3.zero;
        _moveFlg = false;
        _rotateAngle = -90f; // 初期向き補正：右
        _direction = Vector3.zero;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);

        _rd          = GetComponent<Rigidbody2D>();
        _transform  = transform;
        _moveVector = Vector3.zero; // プレイヤーの移動Vector初期化
    }

    // Update is called once per frame
    void Update(){}

    // 固定フレームレートによるUpdate
    public void FixedUpdate()
    {
        // 移動情報更新
        MoveHandler();

        // 移動
        MoveAction();

        // カメラがプレイヤーを追う
        // Todo CameraControllerに移す
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        //if (pos.x < 0f) pos.x = 0f;
        //if (pos.x > 1f) pos.x = 1f;
        //if (pos.y < 0f) pos.y = 0f;
        //if (pos.y > 1f) pos.y = 1f;

        //transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    // 移動をコントロール
    private void MoveHandler()
    {
        _moveFlg = false;

        // JoyStickによる方向設定
        //GetMoveVectorFromJoystick();

        // キーボードによる方向設定
        GetMoveVectorFromKeyboard();
    }

    private void GetMoveVectorFromJoystick()
    {
        //float nowAngle = transform.rotation.z;
        //float targetAngle = nowAngle;
        //float h = _joystick.GetHorizontalValue();
        //float v = _joystick.GetVerticalValue();

        //targetAngle = (float)(Math.Atan(v / h) * 180 / Math.PI);
        //transform.Rotate(0, 0, UpdateRotateZ(nowAngle, targetAngle));

        //Vector3 moveDir = new Vector3(h, v, 0).normalized;
        //return moveDir;
    }

    private void GetMoveVectorFromKeyboard()
    {
        bool quitflg = false;
        float nowAngle      = CorrectAngleValue(_rotateAngle);
        float targetAngle   = nowAngle;

        if (Input.GetKey(KeyCode.W))
        {
            targetAngle = 0f;
            _moveFlg = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetAngle = 90f;
            _moveFlg = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetAngle = 180f;
            _moveFlg = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetAngle = 270f;
            _moveFlg = true;
        }
        // if (quitflg) { Quit(); }
        //_rotateAngle = targetAngle;
        _rotateAngle = CalcRotationAngle(nowAngle, targetAngle);
    }

    public void MoveAction()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);
        _direction = GetDirectionVectorByAngle(_rotateAngle);
        // 前進
        if (_moveFlg)
        {
            //_direction = new Vector3(0.5f, 0.7f, 0); //TEST
            Vector3 newPos = transform.position + _direction * _speed * Time.deltaTime;
            transform.position = newPos;
        }
    }

    // 操作による対象角度への回転を行う場合の回転角を計算（フレーム間最大回転角を考慮）
    private float CalcRotationAngle(float nowAngle, float targetAngle)
    {
        float fixAngle = 0f;
        float angleDiff = targetAngle - nowAngle;

        if (angleDiff < 0f)
        {
            angleDiff = 360f + angleDiff;
        }

        if (angleDiff < 180f)
        {
            if (angleDiff < MAX_ROTATION_ANGLE)
            {
                fixAngle = angleDiff;
            }
            else
            {
                fixAngle = MAX_ROTATION_ANGLE;
            }
        }
        else
        {
            angleDiff = 360f - angleDiff;
            if (angleDiff < MAX_ROTATION_ANGLE)
            {
                fixAngle = -angleDiff;
            }
            else
            {
                fixAngle = -MAX_ROTATION_ANGLE;
            }
        }
        return CorrectAngleValue(nowAngle + fixAngle);
    }

    private float CorrectAngleValue(float angle)
    {
        float fixAngle = angle;
        while (fixAngle < 0 || fixAngle >= 360)
        {
            if (fixAngle < 0)
            {
                fixAngle += 360;
            }
            else
            {
                fixAngle -= 360; 
            }
        }
        return fixAngle;
    }

    private Vector3 GetDirectionVectorByAngle(float angle)
    {
        float posX = - (float)Math.Sin(angle * Math.PI / 180f);
        float posY = (float)Math.Cos(angle * Math.PI / 180f);
        //Debug.Log("angle: " + angle + " X: " + X + " Y: " + Y);

        return new Vector3(posX, posY, 0);

        //return Vector3.zero;
    }

    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
}
