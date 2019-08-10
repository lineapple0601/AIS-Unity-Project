using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankController : MonoBehaviour
{
    public GameObject Row;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate(Row);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
