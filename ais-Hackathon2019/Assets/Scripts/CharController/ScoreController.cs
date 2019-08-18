using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    // 点数
    private static int Score = 0;
    public static TextMeshProUGUI ScoreText;

    void Start()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();
        updateScoreText();
    }

    void Update()
    {
        //
    }

    private static void updateScoreText()
    {
        ScoreText.text = "Score : " + Score;
    }

    // ScoreのSetter
    public static void setScore(int newScore)
    {
        Score = newScore;
    }

    // ScoreのGetter
    public static int getScore()
    {
        return Score;
    }

    // ScoreのSetter
    public static void addScore(int newScore)
    {
        Score += newScore;
        updateScoreText();
    }
}

