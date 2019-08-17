using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Text StartText;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        tick++;
        if (tick % 60 == 0)
        {
            StartText.text = "";
            tick = 0;
        }
        else if (tick % 30 == 0)
        {
            StartText.text = "TOUCH To START";
        }
    }

    public void ChangeSceneMain()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }
}
