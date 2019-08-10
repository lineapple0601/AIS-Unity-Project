using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool pauseOn = false;
    private GameObject pausePanel;
    private GameObject playBG;

    private void Awake()
    {
        pausePanel = GameObject.Find("Joystick").transform.Find("PauseUI").gameObject;
        playBG = GameObject.Find("Joystick").transform.Find("PauseingBt").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ActivePauseBt()
    {

        if (!pauseOn)
        {
            //一時的に停止する
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            playBG.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            playBG.SetActive(true);
        }
        pauseOn = !pauseOn;
    }

    public bool getPauseOn()
    {
        return this.pauseOn;
    }


}