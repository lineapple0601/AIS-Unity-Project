using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*作成者：MOON*/
public class BGController : MonoBehaviour
{
    Renderer myMaterial;

    public PlayerCtrl_joystick joystick;
    public GameObject player;

    float offset = 0.1f;

    public float Scroll_Speed = 0.5f;
    Vector2 offVec = Vector2.zero;

    float player_x;
    float player_y;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        player_x = joystick.GetHorizontalValue() * 0.5f;
        player_y = joystick.GetVerticalValue() * 0.5f;

        //offVec += new Vector2(offset * Scroll_Speed * Time.deltaTime, 0);
        if (joystick.drag_Check)
        {
            offVec += new Vector2(player_x * Scroll_Speed * Time.deltaTime, player_y * Scroll_Speed * Time.deltaTime);
        }
        else
        {
            offVec += new Vector2(offset * Scroll_Speed * Time.deltaTime, 0);
        }

        myMaterial.material.SetTextureOffset("_MainTex", offVec);
    }
}
