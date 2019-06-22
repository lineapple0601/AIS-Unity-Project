using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadein : MonoBehaviour
{
    //textをfadeinする機能

    public float speed = 0.02f;
    float alfa;
    float red, green, blue;
    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Text>().color.r;
        green = GetComponent<Text>().color.g;
        blue = GetComponent<Text>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().color = new Color(red, green, blue, alfa);
        Invoke("fade", 6);  //6秒後にfadeメソッド呼出
    }

    void fade() {
        alfa += speed;
    }
}
