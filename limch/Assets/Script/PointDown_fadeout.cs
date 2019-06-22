﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDown_fadeout : MonoBehaviour
{
    //pointup時fadeinする機能

    public float speed = -0.01f;
    float alfa;
    float red, green, blue;
    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }

    void fade()
    {
        alfa += speed;
    }
}
