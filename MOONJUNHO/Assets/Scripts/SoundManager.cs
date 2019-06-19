using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //사운드매니저 자신의 인스턴스를 담을 정적변수(static)을 만든다
    public static SoundManager instance;

    public AudioClip sndExplosion;
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }
    public void PlaySound()
    {
        myAudio.PlayOneShot(sndExplosion);
    }
}
