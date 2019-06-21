using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    /// <summary>
    /// backgroundを動かす機能
    /// </summary>

    private new Renderer renderer;
    public float ScrollSpeed = 0.5f;
    float Target_Offset;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Target_Offset += Time.deltaTime * ScrollSpeed;
        renderer.material.mainTextureOffset = new Vector2(Target_Offset, 0);
    }
}
