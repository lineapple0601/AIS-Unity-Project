using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    private static Slider HPSlider;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider = GetComponent<Slider>();
        HPSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateHPBar(float HP)
    {
        HpBarController.HPSlider.value = HP;
    }
}
