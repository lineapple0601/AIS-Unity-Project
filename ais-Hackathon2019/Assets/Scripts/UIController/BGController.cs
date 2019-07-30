using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    Renderer myMaterial;

    float offset = 0.1f;

    public float Scroll_Speed = 0.5f;
    Vector2 offVec = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offVec += new Vector2(offset * Scroll_Speed * Time.deltaTime, 0);

     
        myMaterial.material.SetTextureOffset("_MainTex", offVec);
    }
}
