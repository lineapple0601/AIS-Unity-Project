using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float MoveSpeed;     // 動きの速さ
    Vector3 angle;
    float angle2,angle3;
    bool flag = false;

    public GameObject aircraft;
    public GameObject destroyer;

    // Start is called before the first frame update
    void Start()
    {
        angle.x = Random.Range(-1f, 1f);
        angle.y = Random.Range(-1f, 1f);
        //angle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        //angle = Mathf.Atan2(GameObject.FindWithTag("Player").transform.position.y, GameObject.FindWithTag("Player").transform.position.x) * Mathf.Rad2Deg;
        //angle = Mathf.Atan2(Random.Range(10f, -10f), Random.Range(10f, -10f)) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            gameObject.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
            flag = true;
        }
        else
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);
            flag = false;

            if (pos.x > 1f || pos.y > 1f || pos.x < 0f || pos.y < 0f)
            {
                angle.x = Random.Range(-1f, 1f);
                angle.y = Random.Range(-1f, 1f);
                flag = false;
            }
        }


    }
}
