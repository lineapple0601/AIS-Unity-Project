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
        _movStick = GameObject.Find("joystickBG").GetComponent<PlayerCtrl_joystick>();
        InitPlayer();
    }

    // Update is called once per frame
    void Update() {}

    // 固定フレームレートによるUpdate
    public new void FixedUpdate()
    {
        // 移動情報更新
        MoveHandler();

        // 速度更新
        UpdateSpeed();

        // 移動
        MoveAction();
    }

    private void InitPlayer()
    {
        switch (_playerType)
        {
            case 0:
                // 駆逐艦の基本性能
                _hp = 50;
                _maxSpeed = 2.0f;
                _acc = 0.1f;
                _rotationSpeed = 1.5f;
                break;
            case 1:
                // 戦艦の基本性能
                _hp = 100;
                _maxSpeed = 1.7f;
                _acc = 0.08f;
                _rotationSpeed = 1.2f;
                break;
            case 2:
                // 潜水艦の基本性能
                _hp = 50;
                _maxSpeed = 1.6f;
                _acc = 0.08f;
                _rotationSpeed = 1.0f;
                break;
            case 3:
                // 空母の基本性能
                _hp = 80;
                _maxSpeed = 1.5f;
                _acc = 0.06f;
                _rotationSpeed = 0.7f;
                break;
        }

        _moveFlg = false;
        _speed = 0f;
        _rotateAngle = -90f; // 初期向き補正：右
        _movVector = Vector3.zero;
        _rd = GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);
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
        if (_speed > 0)
        {
            Vector3 newPos = transform.position + _movVector * _speed * Time.deltaTime;
            transform.position = newPos;
        }
    }

    // 通常攻撃
    public void BasicAtack()
    {
        Debug.Log("ba atack");
    }

    // 特殊攻撃
    public void SpecialAtack()
    {
        Debug.Log("sp atack");
    }

    
}
