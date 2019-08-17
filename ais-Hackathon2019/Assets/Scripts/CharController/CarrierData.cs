using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierData : MonoBehaviour
{
    public int HP; //HP設定値

    public CarrierData(int _HP)
    {
        HP = _HP;
    }

    public void DecreaseHp(int damage)
    {
        HP -= damage;
    }

    public int GetHP()
    {
        return HP;
    }
}
