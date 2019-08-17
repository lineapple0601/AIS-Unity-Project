using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // 定数

    // 公開変数
    public bool        _moveFlg;       // 移動状態の有無
    public int         _hp;            // HP
    public float       _acc;           // 加速度
    public float       _speed;         // 速度
    public float       _maxSpeed;      // 速度（最大）
    public float       _rotationSpeed; // 回転速度
    public float       _rotateAngle;   // 回転角(0.0f~359.0f)
    public Vector3     _movVector;     // 方向ベクトル
    // 内部変数
    public Rigidbody2D _rd;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update(){}

    // 固定フレームレートによるUpdate
    public virtual void FixedUpdate()
    {
        // 移動情報更新
        MoveHandler();
        // 移動
        MoveAction();
    }

    // 移動をコントロール
    public virtual void MoveHandler() {}

    public virtual void MoveAction() {}

    // 操作による対象角度への回転を行う場合の回転角を計算（フレーム間最大回転角を考慮）
    protected float CalcRotationAngle(float nowAngle, float targetAngle)
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

    // 角度を0<x359以内に修正
    protected float CorrectAngleValue(float angle)
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

    // 角度から進行方向Vector3を取得
    protected Vector3 GetDirectionVectorByAngle(float angle)
    {
        float posX = - (float)Math.Sin(angle * Math.PI / 180f);
        float posY = (float)Math.Cos(angle * Math.PI / 180f);

        return new Vector3(posX, posY, 0);
    }

    // 加速度、最大速度や移動判定を元に、現在の速度を更新
    protected void UpdateSpeed()
    {
        if (_moveFlg)
        {
            _speed += _acc;
            if (_speed > _maxSpeed) _speed = _maxSpeed;
        }
        else
        {
            _speed -= 0.05f;
            if (_speed < 0) _speed = 0f;
        }
    }

    // DEBUG用関数（実行するとゲームがその時点で止まる
    protected void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
}
