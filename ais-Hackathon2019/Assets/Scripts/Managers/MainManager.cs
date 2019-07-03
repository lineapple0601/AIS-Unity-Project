using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("get key down d");
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("PauseScene");
    }
}
