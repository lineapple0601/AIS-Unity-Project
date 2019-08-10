using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrierHpBarController : MonoBehaviour
{
    ///非使用クラス
    ///

    public Image bar;
    public Text hptext;
    CarrierData carrierData;
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        carrierData = new CarrierData(HP);
    }

    // Update is called once per frame
    void Update()
    {
        CarrierHPbar();

        if (carrierData.GetHP() <= 0)
        { //HPが0より少ない場合
            Destroy(gameObject); //objectを削除
        }
    }

    public void CarrierHPbar()
    {
        float HP = carrierData.GetHP(); // hpを読み込む
        bar.fillAmount = HP / 100f; // バーを埋める
        hptext.text = string.Format("HP {0}/100", HP); //HPによるテキスト

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  //ぶつかるタグが"Player Missile"`の場合
        {
            carrierData.DecreaseHp(10);    //HP-2
            //Debug.Log(gameObject.name + "残っているHP：" + enemyData.getHP());
        }
    }
}
