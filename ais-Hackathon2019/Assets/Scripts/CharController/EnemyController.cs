using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ShipController
{
    // 定数

    // 公開変数
    public int _enemyType;
    public GameObject EnemyBomb;    //敵の球

    // BOSS用
    public Vector3[] missile_pos;
    public Quaternion[] missile_ro;
    public GameObject missile_1;
    public GameObject missile_2;
    public GameObject missile_3;
    public GameObject missile_4;
    public GameObject missile_5;
    public GameObject missile_6;
    public GameObject missile_7;
    public GameObject missile_8;

    // 内部変数
    private int AttackPattern = 0;      //敵の攻撃パータン
    private bool FireState;             //ミサイル制御
    private float timer;                // timer
    private bool isUpAngle = true;

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        _aliveFlg = true;
        //_enemyType = 0; // 敵種類（0:初期敵, 1:）
        InitEnemy();
    }

    // Update is called once per frame
    void Update() { }

    // 固定フレームレートによるUpdate
    public new void FixedUpdate()
    {
        CheckAlive();

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
                // EnemyA
                _maxHp = 30;
                _maxSpeed = 1.2f;
                _acc = 0.07f;
                _rotationSpeed = 1.0f;
                break;
            case 1:
                // EnemyB
                _maxHp = 50;
                _maxSpeed = 1.5f;
                _acc = 0.09f;
                _rotationSpeed = 1.0f;
                break;
            case 2:
                // EnemyC
                _maxHp = 100;
                _maxSpeed = 1.4f;
                _acc = 0.05f;
                _rotationSpeed = 1.5f;
                break;
            case 3:
                // Boss
                _maxHp = 200;
                _maxSpeed = 1.0f;
                _acc = 0.07f;
                _rotationSpeed = 0.8f;
                missile_pos = new Vector3[8];
                missile_ro = new Quaternion[8];
                break;
        }

        _hp = _maxHp;
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

        // 敵の種類によって異なるAIを実装
        switch (_enemyType)
        {
            case 0:
                // EnemyA
                // AI概要：プレイヤーへ接近し、一定距離に到達後
                //        上下に移動する
                _moveFlg = true;
                if (dist < 40f)
                {
                    SetAngleUpOrDown();
                }
                else
                {
                    SetAngleToPlayer(playerTF);
                }
                break;
            case 1:
                // EnemyB
                // AI概要：プレイヤーへ接近し、一定距離に到達後待機、
                //        また一定距離離れると再度移動開始
                if (dist < 20f) _moveFlg = false; // 距離が近いと停止する
                SetAngleToPlayer(playerTF);
                if (dist > 30f) _moveFlg = true;
                break;
            case 2:
                // EnemyC
                // AI概要：プレイヤーに対して、時計回りに動く TODO
                // AI概要：プレイヤーへ接近し、一定距離に到達後待機、
                //        また一定距離離れると再度移動開始
                if (dist < 20f) _moveFlg = false; // 距離が近いと停止する
                SetAngleToPlayer(playerTF);
                if (dist > 30f) _moveFlg = true;
                break;
            case 3:
                // Boss
                // AI概要：プレイヤーへ接近し、一定距離に到達後待機、
                //        また一定距離離れると再度移動開始
                if (dist < 70f) _moveFlg = false; // 距離が近いと停止する
                SetAngleToPlayer(playerTF);
                if (dist > 60f) _moveFlg = true;
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
        _enemyBombPos = transform.position;
        // 敵の種類によって異なるAIを実装
        switch (_enemyType)
        {
            case 0:
                // EnemyA_Bomb
                // 概要：Position = enemyの位置と同一、Angle = enemyの角度と同一、Speed = 2f
                _EnemyBombAngle = transform.rotation;
                break;
            case 1:
                // EnemyB_Bomb
                // 概要：Position = enemyの位置と同一、Angle = enemy一周回り、Speed = 2f
                _EnemyBombAngle = transform.rotation;
                break;
            case 2:
                // EnemyC_Bomb
                // 概要：Position = enemyの位置と同一、Angle = enemy前後方、Speed = 2f
                _EnemyBombAngle = transform.rotation;
                break;
            case 3:
                // Boss_Bomb
                missile_pos[0] = missile_1.transform.position;
                missile_pos[1] = missile_2.transform.position;
                missile_pos[2] = missile_3.transform.position;
                missile_pos[3] = missile_4.transform.position;
                missile_pos[4] = missile_5.transform.position;
                missile_pos[5] = missile_6.transform.position;
                missile_pos[6] = missile_7.transform.position;
                missile_pos[7] = missile_8.transform.position;

                missile_ro[0] = missile_1.transform.rotation;
                missile_ro[1] = missile_2.transform.rotation;
                missile_ro[2] = missile_3.transform.rotation;
                missile_ro[3] = missile_4.transform.rotation;
                missile_ro[4] = missile_5.transform.rotation;
                missile_ro[5] = missile_6.transform.rotation;
                missile_ro[6] = missile_7.transform.rotation;
                missile_ro[7] = missile_8.transform.rotation;
                break;
        }
    }

    // 攻撃行動
    public void AtackAction()
    {
        if (_enemyType < 3)
        {
            timer += Time.deltaTime;

            // 敵の種類によって異なるAIを実装
            switch (_enemyType)
            {
                case 0:
                    // EnemyA_Bomb：初期敵（駆逐艦）
                    // 概要：Position = enemyの位置と同一、Angle = enemyの角度と同一、Speed = 2f
                    if (timer < 0.03f)
                    {
                        Instantiate(EnemyBomb, _enemyBombPos, _EnemyBombAngle); //生成
                    }
                    else if (timer > 4f)    //クールダウン
                    {
                        timer = 0f;
                    }
                    break;
                case 1:
                    // EnemyA_Bomb：敵2
                    // 概要：Position = enemyの位置と同一、Angle = enemy一周回り、Speed = 2f
                    if (timer < 0.03f)
                    {
                        for (int i = 0; i < 10; i++)    //球の数
                        {
                            _EnemyBombAngle = _EnemyBombAngle * Quaternion.Euler(0, 0, 36f);
                            Instantiate(EnemyBomb, _enemyBombPos, _EnemyBombAngle); //生成  
                        }
                    }
                    else if (timer > 5f)    //クールダウン
                    {
                        timer = 0f;
                    }
                    break;
                case 2:
                    // EnemyA_Bomb：敵3
                    // 概要：Position = enemyの位置と同一、Angle = enemy前後方、Speed = 2f
                    if (timer < 0.03f)
                    {
                        for (int i = 0; i < 10; i++)    //球の数
                        {
                            Instantiate(EnemyBomb, _enemyBombPos, _EnemyBombAngle); //生成  
                            Instantiate(EnemyBomb, _enemyBombPos, _EnemyBombAngle * Quaternion.Euler(0, 0, 180f)); //生成  
                        }
                    }
                    else if (timer > 1f)    //クールダウン
                    {
                        timer = 0f;
                    }
                    break;
            }
        }
        else
        {
            // Boss
            Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);
            FireState = true;
            timer += Time.deltaTime;

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

    //射撃間隔処理
    IEnumerator FireCycleControl()
    {
        FireState = false;
        yield return new WaitForSeconds(0.3f);
        FireState = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            _hp -= 80;
        }
        else if (col.tag == "Torpedo")
        {
            col.gameObject.SetActive(false);
            _hp -= 40;
        }
        else if (col.tag == "P_Bomb")
        {
            col.gameObject.SetActive(false);
            _hp -= 15;
        }

        if (_hp < 0) _hp = 0;
    }
}
