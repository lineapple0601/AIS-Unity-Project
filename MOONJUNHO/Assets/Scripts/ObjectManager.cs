using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject rocketPrefab;

    List<GameObject> bullets = new List<GameObject>();//총알을 담아둘 리스트를 만듬

    // Start is called before the first frame update
    void Start()
    {
        CreateBullest(5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (ObjectManager.instance == null)
        {
            ObjectManager.instance = this;
        }
    }

    void CreateBullest(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            //Instantiate()로 생성한 게임 오브젝트를 변수에 담고자 하면, "as + 데어터타임"을 명령어 뒤에 붙여 주어야함
            GameObject bullet = Instantiate(rocketPrefab) as GameObject;
            bullet.transform.parent = transform;
            bullet.SetActive(false);

            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet(Vector3 pos)
    {
        GameObject reqBullet = null;
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].activeSelf == false)
            {
                reqBullet = bullets[i];//비활성화 되어있는 총알을 찾아 reqBullet에 담아둡니다
                break;
            }
        }

        if (reqBullet == null)//추가 총알 생성
        {
            GameObject newBullet = Instantiate(rocketPrefab) as GameObject;
            newBullet.transform.parent = transform;

            bullets.Add(newBullet);
            reqBullet = newBullet;
        }

        reqBullet.SetActive(true);//reqBullet활성
        reqBullet.transform.position = pos;
        return reqBullet;
    }

    public void ClearBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].SetActive(false);//총알을 모두 비활성 시킨다
        }
    }
}
