using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject Option;
    private bool value;
    // Start is called before the first frame update
    void Start()
    {
        value = false;      //オプションの初期値はCLOSE
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))    //ESCボタンを押下
        {
            //print("input esc");
            if (value == false)    //画面を停止し、オプションUIを出力する
            {
                Option.SetActive(true);
                Time.timeScale = 0;
                //print("on");
                value = true;
            }

            else        //画面を再生し、オプションUIを閉じる
            {
                Option.SetActive(false);
                Time.timeScale = 1;
                //print("off");
                value = false;
            }
        }
    }
}
