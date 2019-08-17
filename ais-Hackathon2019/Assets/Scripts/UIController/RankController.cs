using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class RankController : MonoBehaviour
{
    public GameObject Row;
    public RectTransform Contents;

    const string DOMAIN = "13.59.236.35";
    const float RowHeight = 40f;
    private string jsonStr;
    private IEnumerator coroutine;
    private RankInfos RankInfo;
    private int RankNum = 1;
    public Text YourRankingText;
    private bool isLoaded = false;

    private int tick = 0;
    private string tickText = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getDataFromServer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLoaded)
        {
            tick++;
            if (tick % 30 == 0)
            {
                tickText = "";
            }
            else if (tick % 5 == 0)
            {
                tickText += ".";
            }
            YourRankingText.text = "Loading" + tickText;
        }
    }

    public IEnumerator getDataFromServer()
    {
        WWW wwwObj = new WWW(DOMAIN + "/rank");
        yield return wwwObj;
        jsonStr = wwwObj.text;
        RankInfo = JsonUtility.FromJson<RankInfos>("{\"ranks\":" + jsonStr + "}");

        // 各データ
        
        foreach (RankObject RankObj in RankInfo.ranks)
        {
            if (RankNum == 1)
            {
                // テキストの入力
                Component[] RowTextArr = Row.GetComponentsInChildren(typeof(Text));
                setRowText(RowTextArr, RankObj);
            }
            else
            {
                // 2行目からはクローンする
                GameObject CloneRow = GameObject.Instantiate(Row, Contents.transform);
                Component[] RowTextArr = CloneRow.GetComponentsInChildren(typeof(Text));
                setRowText(RowTextArr, RankObj);
                Contents.sizeDelta = new Vector2(Contents.sizeDelta.x, RankNum*RowHeight);
            }
            RankNum++;
        }

        string RankingTextTmp = "";
        switch (ScoreManager.Ranks)
        {
            case 1:
                RankingTextTmp = "Fantastic!! You are 1st!!";
                break;
            case 2:
                RankingTextTmp = "Amazing!! You are 2nd!!";
                break;
            case 3:
                RankingTextTmp = "Wow!! You are 3rd!!";
                break;
            default:
                RankingTextTmp = "Your rank : " + ScoreManager.Ranks.ToString();
                break;
        }
        isLoaded = true;
        YourRankingText.text = RankingTextTmp;
    }

    private void setRowText(Component[] RowTextArr, RankObject RankObj)
    {
        foreach (Text RowText in RowTextArr)
        {
            if (ScoreManager.Ranks == RankNum)
            {
                RowText.color = Color.red;
            }
            else
            {
                RowText.color = Color.black;
            }
            switch (RowText.name)
            {
                case "RankNum":
                    RowText.text = RankNum.ToString() + ".";
                    break;
                case "Name":
                    RowText.text = RankObj.name;
                    break;
                case "Score":
                    RowText.text = RankObj.score;
                    break;
            }
        }
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

[System.Serializable]
public class RankObject
{
    public int id;
    public string name;
    public string score;
}

[System.Serializable]
public class RankInfos
{
    public RankObject[] ranks;
}