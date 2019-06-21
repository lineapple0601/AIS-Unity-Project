using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public int HP;

    public EnemyData(int _HP) {
        HP = _HP;
    }

    public void decreaseHp(int damage) {
        HP -= damage;
    }

    public int getHP() {
        return HP;
    }
}
