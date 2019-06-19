using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public GameObject explosion;
    //public GameObject rocket;
    public bool canShoot = false;
    float shootDelay = 0.5f;
    float shootTimer = 0;

    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
        ShootControl();
    }

    void MoveControl()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(moveX, 0, 0);

        //현재 플레이어의 월드좌표(transform.position)을 뷰포트 기준 좌표로 변화시키는 명령
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        //Mathf.Clap01(값) - 입력된 값이 0 ~ 1사이를 벗어나지 못하게 강제로 조정 해 주는 함수
        viewPos.x = Mathf.Clamp01(viewPos.x);

        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Instantiate(프리팹, 생성위치, 생성시 방향)
            //Quaternion, identity:프리팹이 가지고 있는 방향값을 그대로 사용한다는 의미
            Instantiate(explosion, transform.position, Quaternion.identity);

            SoundManager.instance.PlaySound();

            GameManager.instance.KillPlayer();

            Destroy(col.gameObject);
            //Destroy(gameObject);
            InactivePlayer();
        }
    }

    void ShootControl()
    {
        if (canShoot == true)
        {

            //rocket 프리팹을 0.5초 간 생성 시킨다
            if (shootTimer > shootDelay)
            {
                //Instantiate(rocket, transform.position, rocket.transform.rotation);

                ObjectManager.instance.GetBullet(transform.position);
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
    }

    void InactivePlayer()//게임 재시작 하기위해서 Player를 삭제 안하고 비활성화 한다
    {
        gameObject.SetActive(false);
        canShoot = false;

        transform.position = playerPos;//Player가 초기화 지점에 올 수 있도록 한다
    }
}
