using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipType
{
    private float MaxHP;
    private int Attack;

    public void setMaxHP(float MaxHP)
    {
        this.MaxHP = MaxHP;
    }

    public float getMaxHP()
    {
        return this.MaxHP;
    }
}

public class BasicShip : BattleShipType
{
    public BasicShip()
    {
        setMaxHP(50);
    }
}