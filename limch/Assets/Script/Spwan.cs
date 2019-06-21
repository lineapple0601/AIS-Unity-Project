using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    public GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Respawn", 3, 3.5f);    //3秒周期でobjectを作り出す
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Respawn() {
        GameObject Inst = Instantiate(spawnObject, transform.position, transform.rotation) as GameObject;
        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);  //objectの位置を持つ変数

        //objectがカメラから出るとobjectを削除
        if (pos.x < 0f)
            Destroy(Inst);
        if (pos.x > 1f)
            Destroy(Inst);
        if (pos.y < 0f)
            Destroy(Inst);
        if (pos.y > 1f)
            Destroy(Inst);
        //if (pos.x < 0f) Destroy(Inst);   //min x
        //if (pos.x > 1f) Destroy(Inst);    //max x
        //if (pos.y < 0f) Destroy(Inst);    //min y
        //if (pos.y > 1f) Destroy(Inst);    //max y
        //Destroy(Inst, 3.0f);
    }
}
