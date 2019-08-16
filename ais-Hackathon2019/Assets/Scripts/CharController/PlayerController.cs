using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipController
{
    // 定数

    // 公開変数

    public int _playerType;
    public PlayerCtrl_joystick _movStick; // Joystickコントローラ
    // 内部変数

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        _playerType = 3; // プレイヤーの船（0:駆逐艦、1:戦艦、2:潜水艦、3:空母）
        //_moveStick = GameObject.Find("joystickBG");
        InitPlayer(_playerType);
    }

    // Update is called once per frame
    void Update() {}

    // 固定フレームレートによるUpdate
    public new void FixedUpdate()
    {
        // 移動情報更新
        MoveHandler();
        // 移動
        MoveAction();
    }

    private void InitPlayer(int playerType)
    {
        _moveFlg = false;
        _movVector = Vector3.zero;
        _rd = GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);

        switch (playerType)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                _hp = 100;
                _speed = 1.5f;
                _rotationSpeed = 0.7f;
                _rotateAngle = -90f; // 初期向き補正：右
                break;
        }
    }

    // 移動をコントロール
    public override void MoveHandler()
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
        float stickMovH = _movStick.GetHorizontalValue();
        float stickMovV = _movStick.GetVerticalValue();
        float nowAngle = CorrectAngleValue(_rotateAngle);
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
        float nowAngle = CorrectAngleValue(_rotateAngle);
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

    public override void MoveAction()
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
}
