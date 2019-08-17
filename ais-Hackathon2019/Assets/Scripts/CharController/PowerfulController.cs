using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerfulController : ShipController
{
    // 定数
    public GameObject missile_1;
    public GameObject missile_2;
    public GameObject missile_3;
    public GameObject missile_4;
    public GameObject missile_5;
    public GameObject missile_6;
    public GameObject missile_7;
    public GameObject missile_8;

    Vector3[] missile_pos;

    Quaternion[] missile_ro;

    // 公開変数
    public int _enemyType;

    public GameObject EnemyBomb;    //敵の球
    
    // 内部変数
    private int AttackPattern = 0;      //敵の攻撃パータン
    private bool FireState;             //ミサイル制御
    private float timer;                // timer  

    // Start is called before the first frame update
    void Start()
    {
        missile_pos = new Vector3[8];
        missile_ro = new Quaternion[8];

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
                // EnemyA：初期敵（駆逐艦）
                _hp = 30;
                _maxSpeed = 1.0f;
                _acc = 0.07f;
                _rotationSpeed = 1.0f;
                break;
            case 1:

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
        transform.position = new Vector3(8, -3, 0);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _rotateAngle);
    }

    // 移動アルゴリズム（AI）
    public void MoveAI()
    {
        GameObject playerObj = GameObject.Find("GameManager").GetComponent<MainManager>().playerObj;
        Transform playerTF = playerObj.transform;
        float dist = GetDistanceFromPlayer(playerTF);
        if (dist < 70f) _moveFlg = false; // 距離が近いと停止する

        // 敵の種類によって異なるAIを実装
        switch (_enemyType)
        {
            case 0:
                // EnemyA：初期敵（駆逐艦）
                // AI概要：プレイヤーへ接近し、一定距離に到達後待機、
                //        また一定距離離れると再度移動開始
                SetAngleToPlayer(playerTF);
                if (dist > 60f) _moveFlg = true;
                break;
            case 1:
                break;
            case 2:
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
        //_enemyBombPos = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
        missile_pos[0] = missile_1.transform.position;
        missile_pos[1] = missile_2.transform.position;
        missile_pos[2] = missile_3.transform.position;
        missile_pos[3] = missile_4.transform.position;
        missile_pos[4] = missile_5.transform.position;
        missile_pos[5] = missile_6.transform.position;
        missile_pos[6] = missile_7.transform.position;
        missile_pos[7] = missile_8.transform.position;

        //_enemyBombPos = missile_1.transform.position;

        // 敵の種類によって異なるAIを実装
        switch (_enemyType)
        {
            case 0:
                // EnemyA_Bomb：初期敵（駆逐艦）
                // 概要：Position = enemyの位置と同一、Angle = enemyの角度と同一、Speed = 3f
                missile_ro[0] = missile_1.transform.rotation;
                missile_ro[1] = missile_2.transform.rotation;
                missile_ro[2] = missile_3.transform.rotation;
                missile_ro[3] = missile_4.transform.rotation;
                missile_ro[4] = missile_5.transform.rotation;
                missile_ro[5] = missile_6.transform.rotation;
                missile_ro[6] = missile_7.transform.rotation;
                missile_ro[7] = missile_8.transform.rotation;
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    // 攻撃行動
    public void AtackAction()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);
        FireState = true;
        timer += Time.deltaTime;
        
                        // 敵の種類によって異なるAIを実装
                // EnemyA_Bomb：初期敵（駆逐艦）
                // 概要：Position = enemyの位置と同一、Angle = enemyの角度と同一、Speed = 3f
                if (timer < 1f)
                {
                   for (int i = 0; i < 3; i++)
                    {
                        if (FireState)
                        {
                            //_EnemyBombAngle = _EnemyBombAngle * Quaternion.Euler(0, 0, 0);      
                            Instantiate(EnemyBomb, missile_pos[0], missile_ro[0] * Quaternion.Euler(0, 0, 0)); //生成
                            Instantiate(EnemyBomb, missile_pos[1], missile_ro[1] * Quaternion.Euler(0, 0, 90f)); //生成
                            Instantiate(EnemyBomb, missile_pos[2], missile_ro[2] * Quaternion.Euler(0, 0, 0)); //生成
                            Instantiate(EnemyBomb, missile_pos[3], missile_ro[3] * Quaternion.Euler(0, 0, -90f)); //生成
                            Instantiate(EnemyBomb, missile_pos[4], missile_ro[4] * Quaternion.Euler(0, 0, 90f)); //生成
                            Instantiate(EnemyBomb, missile_pos[5], missile_ro[5] * Quaternion.Euler(0, 0, 180f)); //生成
                            Instantiate(EnemyBomb, missile_pos[6], missile_ro[6] * Quaternion.Euler(0, 0, -90f)); //生成
                            Instantiate(EnemyBomb, missile_pos[7], missile_ro[7] * Quaternion.Euler(0, 0, -180f)); //生成
                            StartCoroutine(FireCycleControl());
                        }
                    }   
                }
                else if (timer > 5f)
                {
                    timer = 0f;
                }
    
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

    // プレイヤーとの距離（二乗）を求める
    public float GetDistanceFromPlayer(Transform playerTF)
    {
        return (float)(Math.Pow((playerTF.position.x - transform.position.x), 2) +
            Math.Pow((playerTF.position.y - transform.position.y), 2));
    }

    //射撃間隔処理
    IEnumerator FireCycleControl()
    {
        FireState = false;
        yield return new WaitForSeconds(0.3f);
        FireState = true;
    }

}
