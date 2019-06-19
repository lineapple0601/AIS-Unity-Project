using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextControl : MonoBehaviour
{
    public static TextControl instance;

    public GameObject readyText;

    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        readyText.SetActive(false);

        gameOverText.SetActive(false);

        StartCoroutine(ShowReady());//`코루틴함수를 실행하여 텍스쳐를 깜박거리게 한다
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (TextControl.instance == null)
        {
            TextControl.instance = this;
        }
    }

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < 3)
        {
            readyText.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            readyText.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }

    public void ShowGameOver()
    {
        gameOverText.SetActive(this);
    }

    public void Restart()
    {
        gameOverText.SetActive(false);//게임오버 텍스쳐 비활성
        StartCoroutine(ShowReady());//코르틴 함수를 실행시켜 게임 재시작
    }
}
