using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // 定数

    // 公開変数
    bool                _moveFlg;       // 移動状態の有無
    public int          _hp;            // HP
    public float        _speed;         // 速度
    public float        _rotationSpeed; // 回転速度
    public float        _rotateAngle;   // 回転角(0.0f~359.0f)
    public Vector3      _movVector;     // 方向ベクトル
    public PlayerCtrl_joystick _movStick; // Joystickコントローラ
    // 内部変数
    private Rigidbody2D _rd;

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        _moveFlg        = false;
        _hp             = 100;
        _speed          = 1.5f;
        _rotationSpeed  = 0.7f;
        _rotateAngle    = -90f; // 初期向き補正：右
        _movVector      = Vector3.zero;
        _rd             = GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);
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
    }

    // 移動をコントロール
    private void MoveHandler()
    {
        _moveFlg = false;

        // Stickによる方向設定
        StickControl();
        if (_moveFlg) return; // stick操作を受け付けるとキーボード操作を受け付けないようにする

        // キーボードによる方向設定
        KeyboardControl();
    }

    // Stickボタンによる操作（向きを確定）
    private void StickControl()
    {
        float stickMovH   = _movStick.GetHorizontalValue();
        float stickMovV   = _movStick.GetVerticalValue();
        float nowAngle    = CorrectAngleValue(_rotateAngle);
        float targetAngle = nowAngle;

        if (!stickMovH.Equals(0f) || !stickMovV.Equals(0f))
        {
            _moveFlg = true;
            float theta = 0;
            theta = (float)Math.Atan(-stickMovH / stickMovV);
            targetAngle = theta * 180f / (float)Math.PI;
            if (stickMovV < 0)
            {
                targetAngle += 180f;
            }
            targetAngle = CorrectAngleValue(targetAngle);
        }
        // 回転角更新
        _rotateAngle = CalcRotationAngle(nowAngle, targetAngle);
    }

    // キーボードボタン（WASD）による操作（向きを確定）
    private void KeyboardControl()
    {
        float nowAngle    = CorrectAngleValue(_rotateAngle);
        float targetAngle = nowAngle;

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
        // 回転角更新
        _rotateAngle = CalcRotationAngle(nowAngle, targetAngle);
    }

    public void MoveAction()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);
        _movVector = GetDirectionVectorByAngle(_rotateAngle);
        // 前進
        if (_moveFlg)
        {
            Vector3 newPos = transform.position + _movVector * _speed * Time.deltaTime;
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
            if (angleDiff < _rotationSpeed)
            {
                fixAngle = angleDiff;
            }
            else
            {
                fixAngle = _rotationSpeed;
            }
        }
        else
        {
            angleDiff = 360f - angleDiff;
            if (angleDiff < _rotationSpeed)
            {
                fixAngle = -angleDiff;
            }
            else
            {
                fixAngle = -_rotationSpeed;
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

        return new Vector3(posX, posY, 0);
    }

    // DEBUG用関数（実行するとゲームがその時点で止まる
    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
}
