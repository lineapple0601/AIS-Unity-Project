using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class RankRegController : MonoBehaviour
{
    public InputField NicknameInput;
    private string Nickname;
    private int score = 99999;
    public TextMeshProUGUI ScoreText;
    public Button RegButton;

    const string DOMAIN = "13.59.236.35";
    private IEnumerator coroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        // TODO : Update Score
        ScoreText.text = score.ToString();
        RegButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckNicknameStatus()
    {
        Nickname = NicknameInput.text;
        if (Nickname == "")
        {
            RegButton.interactable = false;
        }
        else
        {
            RegButton.interactable = true;
        }
    }

    public void regRankDatas()
    {
        RegButton.interactable = false;
        StartCoroutine(saveDataToServer());
    }

    public IEnumerator saveDataToServer()
    {
        Nickname = NicknameInput.text;
        UnityWebRequest wwwObj = UnityWebRequest.Get(DOMAIN + "/rank/register?name=" + Nickname + "&score=" + score.ToString());
        yield return wwwObj.SendWebRequest();
        ScoreManager.Ranks = Convert.ToInt32(wwwObj.downloadHandler.text);
        SceneManager.LoadScene("ScoreScene");
    }
}