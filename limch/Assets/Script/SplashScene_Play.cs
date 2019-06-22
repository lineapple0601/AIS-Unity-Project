using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene_Play : MonoBehaviour
{
    //Sceneをロードする機能

    //public float delayTime = 3;
    public void OnClick() {
        SceneManager.LoadScene("Play_Scene");
    }
}
