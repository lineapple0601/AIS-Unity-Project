using UnityEngine;
using System.Collections;

public class SEManager : MonoBehaviour
{

    public AudioSource DamageSE;


    public void Start()
    {
        DamageSE = gameObject.GetComponent<AudioSource>();
    }

    public void Play_Damage(string tag)
    {
        if (tag == "Enemy")
            DamageSE.Play();
        else if (tag == "Bomb")
            DamageSE.Play();
    }
}