using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ShipController
{
    // 定数

    // 公開変数
    public int _enemyType;

    // 内部変数
    private int AttackPattern = 0;      //敵の攻撃パータン
    private bool FireState;             //ミサイル制御
    private float timer;                // timer
    private bool isUpAngle = true;

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        _enemyType = 0; // 敵種類（0:初期敵, 1:）
        InitEnemy();
    }

    // Update is called once per frame
    void Update() { }

    // 固定フレームレートによるUpdate
    public new void FixedUpdate()
    {
        // 移動AI
        MoveAI();

        // 速度更新
        UpdateSpeed();

        // 移動
        MoveAction();

        // 攻撃AI
        AtackAI();

        // 攻撃行動
        AtackAction();
    }

    private void InitEnemy()
    {
        switch (_enemyType)
        {
            case 0:
                // EnemyA：初期敵（A）
                _hp = 30;
                _maxSpeed = 1.0f;
                _acc = 0.07f;
                _rotationSpeed = 1.0f;
                break;
            case 1:
                // EnemyB：初期敵（B）
                _hp = 30;
                _maxSpeed = 1.0f;
                _acc = 0.07f;
                _rotationSpeed = 1.0f;
                break;
            case 2:
                break;
            case 3:
                break;
        }

        _moveFlg = false;
        _speed = _maxSpeed / 2;
        _rotateAngle = 90f; // 初期向き補正：左
        _movVector = Vector3.zero;
        _rd = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);
    }

    // 移動アルゴリズム（AI）
    public void MoveAI()
    {
        GameObject playerObj = GameObject.Find("GameManager").GetComponent<MainManager>().playerObj;
        Transform playerTF = playerObj.transform;
        float dist = GetDistanceFromPlayer(playerTF);
        if (dist < 20f) _moveFlg = false; // 距離が近いと停止する

        // 敵の種類によって異なるAIを実装
        switch (_enemyType)
        {
            case 0:
                // EnemyA：初期敵（駆逐艦）
                // AI概要：プレイヤーへ接近し、一定距離に到達後待機、
                //        また一定距離離れると再度移動開始
                SetAngleToPlayer(playerTF);
                if (dist > 30f) _moveFlg = true;
                break;
            case 1:
                // AI概要：プレイヤーの動きに関わらず、常に上下に移動する
                SetAngleUpOrDown();
                break;
            case 2:
                // AI概要：プレイヤーに対して、時計回りに動く
                break;
        }

    }

    // 移動
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

    // 攻撃アルゴリズム（AI）
    public void AtackAI()
    {

    }

    // 攻撃行動
    public void AtackAction()
    {

    }

    // プレイヤーへ向くための角度を設定
    public void SetAngleToPlayer(Transform playerTF)
    {
        float nowAngle = CorrectAngleValue(_rotateAngle);
        float targetAngle = nowAngle;
        float distH = playerTF.position.x - transform.position.x;
        float distV = playerTF.position.y - transform.position.y;

        if (!distH.Equals(0f) || !distV.Equals(0f))
        {
            float theta = 0;
            theta = (float)Math.Atan(-distH / distV);
            targetAngle = theta * 180f / (float)Math.PI;
            if (distV < 0)
            {
                targetAngle += 180f;
            }
            targetAngle = CorrectAngleValue(targetAngle);
        }
        // 回転角更新
        _rotateAngle = CalcRotationAngle(nowAngle, targetAngle);
    }

    //向きを上下する
    public void SetAngleUpOrDown()
    {
        float nowAngle = CorrectAngleValue(_rotateAngle);
        float targetAngle = nowAngle;

        if (isUpAngle)
            targetAngle = 0f;
        else
            targetAngle = 180f;

        var position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.y <= 0.1f)
            isUpAngle = true;
        else if (position.y >= 0.9f)
            isUpAngle = false;

        //回転角更新
        _rotateAngle = CalcRotationAngle(nowAngle, targetAngle);
    }


    // プレイヤーとの距離（二乗）を求める
    public float GetDistanceFromPlayer(Transform playerTF)
    {
        return (float)(Math.Pow((playerTF.position.x - transform.position.x), 2) +
            Math.Pow((playerTF.position.y - transform.position.y), 2));
    }
}
