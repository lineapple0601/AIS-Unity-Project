using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ShipController
{
    // 定数

    // 公開変数
    public int           _playerType;
    public PlayerCtrl_joystick _movStick; // Joystickコントローラ

    public Button        _bsBtn;
    public Button        _spBtn;
    public Image         _spBtnImage;
    public GameObject    _atkObj;
    public int           _atkObjMaxPool; // メモリープールに設定するミサイルの数
    public float         _timerForEnd;   // 攻撃time
    public bool          _divingFlg;     // 潜水艦の潜行フラグ

    // 内部変数
    private SpriteRenderer _sr;          // スプライド情報
    private float        _atkObjSpeed;   // 攻撃OBJ速度
    private bool         _atkState;      // 攻撃OBJ速度制御
    private GameObject[] _atkObjArray;   // 攻撃OBJの配列
    private MemoryPool   _mPool;         // メモリープール
    private bool         _spAttack;
    private float        _timer;         // timer
    private AudioSource _basicAttackSE;
    private AudioSource _specialAttackSE;
    private SEManager _seManager;

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定
        // プレイヤーの船（0:駆逐艦、1:戦艦、2:潜水艦、3:空母）
        //_playerType = 2; // デバッグ用

        _aliveFlg = true;
        _movStick = GameObject.Find("joystickBG").GetComponent<PlayerCtrl_joystick>();
        InitPlayer();

        _sr = GetComponent<SpriteRenderer>();
        _divingFlg = false;
        _timer = 0;
        _bsBtn = GameObject.Find("BasicAttackBt").GetComponent<Button>();
        _spBtn = GameObject.Find("SpecialAttackBt").GetComponent<Button>();
        _spBtnImage = GameObject.Find("SpecialAttackBt").GetComponent<Image>();
        _atkState = true;
        _atkObjArray = new GameObject[_atkObjMaxPool];
        _mPool = new MemoryPool();
        _mPool.Create(_atkObj, _atkObjMaxPool);  //オブジェクトをMAXプールの数分生成する

        //攻撃SEの取得
        AudioSource[] audioSources = GetComponents<AudioSource>();
        _basicAttackSE = audioSources[0];
        _specialAttackSE = audioSources[1];

        _seManager = GameObject.Find("SoundManager").GetComponent<SEManager>();
    }

    private void OnApplicationQuit()
    {
        //終了の時、メモリープールをクリアする
        _mPool.Dispose();
    }

    // Update is called once per frame
    void Update() {}

    // 固定フレームレートによるUpdate
    public new void FixedUpdate()
    {
        CheckAlive();

        // 移動情報更新
        MoveHandler();

        // 速度更新
        UpdateSpeed();

        // 移動
        MoveAction();

        // 攻撃処理
        _attackedFlg = false;
        BasicAttack(false);
        SpecialAttack(false);
        UpdateAttackObject();
    }

    private void InitPlayer()
    {
        switch (_playerType)
        {
            case 0:
                // 駆逐艦の基本性能
                _maxHp = 50;
                _maxSpeed = 2.0f;
                _acc = 0.1f;
                _rotationSpeed = 1.5f;
                _atkObjMaxPool = 5;
                break;
            case 1:
                // 戦艦の基本性能
                _maxHp = 100;
                _maxSpeed = 1.7f;
                _acc = 0.08f;
                _rotationSpeed = 1.2f;
                _atkObjMaxPool = 10;
                break;
            case 2:
                // 潜水艦の基本性能
                _maxHp = 50;
                _maxSpeed = 1.6f;
                _acc = 0.08f;
                _rotationSpeed = 1.0f;
                _atkObjMaxPool = 4;
                break;
            case 3:
                // 空母の基本性能
                _maxHp = 80;
                _maxSpeed = 1.5f;
                _acc = 0.06f;
                _rotationSpeed = 0.7f;
                _atkObjMaxPool = 5;
                _timerForEnd = 1f;
                break;
        }

        _hp = _maxHp;
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
    public void BasicAttack(bool buttonCtr)
    {
        if (!_atkState) return;

        if (buttonCtr || Input.GetKeyDown(KeyCode.K))
        {
            _atkObjSpeed = 0.5f;
            StartCoroutine(FireCycleControl());
            _basicAttackSE.Play();

            switch (_playerType)
            {
                case 0:
                    for (int i = 0; i < _atkObjMaxPool; i++)
                    {
                        if (_atkObjArray[i] == null) //空配列の場合
                        {
                            _atkObjArray[i] = _mPool.NewItem();  //プールでミサイルを持ってくる
                            _atkObjArray[i].transform.position = transform.position;    //それの発射位置を設定する
                            _atkObjArray[i].transform.rotation = transform.rotation;  //それの発射方向を設定する
                            break;
                        }
                    }
                    break;
                case 1:
                case 2:
                    _spAttack = false;
                    for (int i = 0; i < _atkObjMaxPool; i++)
                    {
                        if (_atkObjArray[i] == null) //空配列の場合
                        {
                            _atkObjArray[i] = _mPool.NewItem();  //プールでミサイルを持ってくる
                            _atkObjArray[i].transform.position = transform.position;    //それの発射位置を設定する
                            _atkObjArray[i].transform.rotation = transform.rotation;  //それの発射方向を設定する
                            break;
                        }
                    }
                    break;
                case 3:
                    _spAttack = false;
                    for (int i = 0; i < _atkObjMaxPool; i++)
                    {
                        if (_atkObjArray[i] == null) //空配列の場合
                        {
                            _atkObjArray[i] = _mPool.NewItem();  //プールでミサイルを持ってくる
                            _atkObjArray[i].transform.position = transform.position;    //それの発射位置を設定する
                            break;
                        }
                    }
                    break;
            }

            _attackedFlg = true;
        }
    }

    // 特殊攻撃
    public void SpecialAttack(bool buttonCtr)
    {
        if (!_atkState) return;

        //TODO 下記処理とりあえず保留
        //if (_attackedFlg) return; // 通常攻撃があったフレームでは特殊攻撃できない
        if (buttonCtr || Input.GetKeyDown(KeyCode.J))
        {
            switch (_playerType)
            {
                case 0:
                    break;
                case 1:
                case 2:
                    _specialAttackSE.Play();
                    _spAttack = true;
                    _atkObjSpeed = 1.25f;
                    break;
                case 3:
                    _specialAttackSE.Play();
                    _spAttack = true;
                    _atkObjSpeed = 0.15f;
                    break;
            }
        }
        if (_spAttack == true)
        {
            switch (_playerType)
            {
                case 0:
                    break;
                case 1:
                    //必殺技の時間
                    if (_timer <= _timerForEnd)
                    {
                        _timer += Time.deltaTime;
                        StartCoroutine(CoolTime(4f));

                        for (int i = 0; i < _atkObjMaxPool; i++)
                        {
                            if (_atkObjArray[i] == null) //空配列の場合
                            {
                                Quaternion angle = transform.rotation; //それの発射方向を設定する
                                _atkObjArray[i] = _mPool.NewItem();  //プールでミサイルを持ってくる
                                _atkObjArray[i].transform.position = transform.position;    //それの発射位置を設定する
                                if (i % 5 == 1)
                                {
                                    _atkObjArray[i].transform.rotation = angle;     //球の方向がangleのまま
                                }
                                else if (i % 5 == 2)
                                {
                                    _atkObjArray[i].transform.rotation = angle * Quaternion.Euler(0, 0, 30);     //球の方向をangleより30度回転させる
                                }
                                else if (i % 5 == 3)
                                {
                                    _atkObjArray[i].transform.rotation = angle * Quaternion.Euler(0, 0, -30);     //球の方向をangleより-30度回転させる
                                }
                                else if (i % 5 == 4)
                                {
                                    _atkObjArray[i].transform.rotation = angle * Quaternion.Euler(0, 0, 60);     //球の方向をangleより30度回転させる
                                }
                                else if (i % 5 == 0)
                                {
                                    _atkObjArray[i].transform.rotation = angle * Quaternion.Euler(0, 0, -60);     //球の方向をangleより-30度回転させる
                                }
                            }
                        }
                    }
                    //必殺技の時間が終わったら基本攻撃に戻す
                    else
                    {
                        _spAttack = false;
                        _atkObjSpeed = 1;
                        _timer = 0;
                    }
                    break;
                case 2:
                    //必殺技の時間
                    if (_timer <= _timerForEnd)
                    {
                        _timer += Time.deltaTime;
                        StartCoroutine(CoolTime(4f));
                    }
                    //必殺技の時間が終わったら基本攻撃に戻す
                    else
                    {
                        _spAttack = false;
                        _atkObjSpeed = 1;
                        _timer = 0;
                    }
                    break;
                case 3:
                    //必殺技の時間
                    if (_timer <= _timerForEnd)
                    {
                        _timer += Time.deltaTime;
                        StartCoroutine(CoolTime(6f));

                        for (int i = 0; i < _atkObjMaxPool; i++)
                        {
                            if (_atkObjArray[i] == null) //空配列の場合
                            {
                                _atkObjArray[i] = _mPool.NewItem();  //プールでミサイルを持ってくる
                                _atkObjArray[i].transform.position = transform.position;    //それの発射位置を設定する
                                StartCoroutine(FireCycleControl());
                                break;
                            }
                        }
                    }
                    //必殺技の時間が終わったら基本攻撃に戻す
                    else
                    {
                        _spAttack = false;
                        _atkObjSpeed = 0.5f;
                        _timer = 0;
                    }
                    break;
            }
        }
    }

    // 攻撃オブジェクト更新処理
    public void UpdateAttackObject()
    {
        for (int i = 0; i < _atkObjMaxPool; i++)
        {
            if (_atkObjArray[i])    // 配列がTRUEの場合
            {
                if (_atkObjArray[i].GetComponent<Collider2D>().enabled == false) // 配列の Collider2DがFALSEの場合
                {
                    _atkObjArray[i].GetComponent<Collider2D>().enabled = true;  // またTRUEに設定
                    _mPool.RemoveItem(_atkObjArray[i]);  // ミサイルをメモリに返す
                    _atkObjArray[i] = null; // 配列クリア
                }
            }
        }
    }

    // 射撃間隔処理
    IEnumerator FireCycleControl()
    {
        _atkState = false;
        yield return new WaitForSeconds(_atkObjSpeed);
        _atkState = true;
    }

    //クールタイム処理
    IEnumerator CoolTime(float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            _spBtnImage.fillAmount = (1.0f / cool);   //buttonを埋める
            _spBtn.enabled = false;                   //buttonを非活性
            if (_playerType == 2)
            {
                _divingFlg = true;
                transform.localScale = new Vector3(0.5f, 0.75f, 1f);
                _sr.color = new Color(1, 1, 1, 0.2f);
                _bsBtn.enabled = false;
            }
            yield return new WaitForFixedUpdate();    //Update待ち
            _spBtn.enabled = true;                    //buttonを活性
            if (_playerType == 2)
            {
                _divingFlg = false;
                transform.localScale = new Vector3(1f, 1.5f, 1f);
                _sr.color = new Color(1, 1, 1, 0.5f);
                _bsBtn.enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        _seManager.Play_Damage(col.tag);
        if (col.tag == "Enemy")
        {
            _hp -= 80;
        }
        else if (col.tag == "Bomb")
        {
            col.gameObject.SetActive(false);
            //Destroy(col.gameObject);
            _hp -= 15;
        }

        if (_hp < 0) _hp = 0;
    }
}
