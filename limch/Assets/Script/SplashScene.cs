using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    //Sceneをロードする機能

    //public float delayTime = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene("Play_Scene");
    }
    // Use this for initialization
}
