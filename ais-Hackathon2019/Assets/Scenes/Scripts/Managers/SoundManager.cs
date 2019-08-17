using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySE_touch()
    {
        audioSource.Play();
    }
}