using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    private static Slider HPSlider;
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GetComponent<Slider>();
        HPSlider.value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playerObj = GameObject.Find("GameManager").GetComponent<MainManager>().playerObj;
        PlayerController playerCtrl = GameObject.Find("GameManager").GetComponent<MainManager>().playerObj.GetComponent<PlayerController>();
        UpdateHPBar((float)playerCtrl._hp / (float)playerCtrl._maxHp);
    }

    public static void UpdateHPBar(float HP)
    {
        HPSlider.value = HP;
    }
}
