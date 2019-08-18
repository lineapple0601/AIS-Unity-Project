using UnityEngine;
using System.Collections;

public class SEManager : MonoBehaviour
{

    public AudioSource DamageSE;
    public AudioSource ItemSE;


    public void Start()
    {
    }

    public void Play_Damage(string tag)
    {
        if (tag == "Enemy")
            DamageSE.Play();
        else if (tag == "Bomb")
            DamageSE.Play();
    }

    public void Play_Item()
    {
        ItemSE.Play();
    }
}