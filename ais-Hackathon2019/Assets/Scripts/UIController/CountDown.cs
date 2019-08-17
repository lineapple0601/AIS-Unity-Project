using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class CountDown : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public Text startText; // used for showing countdown from 
    private GameObject countText;


    private void Awake()
    {
        countText = GameObject.Find("ControllerUI").transform.Find("CountText").gameObject;
    }
        
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.unscaledDeltaTime;
        startText.text = timeLeft.ToString("0");

        if (timeLeft < 0 && countText.active == true)
        {
            countText.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
