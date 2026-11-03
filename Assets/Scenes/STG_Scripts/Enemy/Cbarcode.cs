using UnityEngine;
using System.Collections;

public class Cbarcode : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject pointitem_Prefab;
    [SerializeField] GameObject pointitem_homing_Prefab;
    [SerializeField] GameObject Destroy_Enemy_Prefab;
    int Life = 70;
    const float BULLET_Z = 0f;
    Animator animator;
    
    float moveTime = 0.5f;
    float stopTime = 0.5f;
    float moveTimer = 0f;
    float stopTimer = 0f;
    bool isMoving = true;   // true = 移動中 / false = 停止中
    bool prevIsMoving = true;

    void Start()
    {
       animator = GetComponent<Animator>();
    }
    void Update()
    {
       float x = transform.position.x;

       if (x >= 3f)
       {
          if (x >= 5f)
          {
             if (x >= 6f)
             {
                if (x >= 7f)
                {
                   animator.SetFloat("H",5f);
                }
                else
                {
                   animator.SetFloat("H",4f);
                }
             }
             else
             {
                animator.SetFloat("H",3f);
             }
          }
          else
          {
             animator.SetFloat("H",2f);
          }
       }
       else
       {
          animator.SetFloat("H",1f);
       }

	    // ------- 移動 -------
	    if (isMoving)
	    {
          moveTimer += Time.deltaTime;

		    if (moveTimer >= moveTime)
		    {
			    moveTimer = 0f;
			    isMoving = false; // 停止へ

			    // ------- 弾発射 -------
			    if (prevIsMoving == true)
			    {
				    StartCoroutine(Shot5Bullets());
			    }
		    }
	    }
	    else
	    {
		    stopTimer += Time.deltaTime;

		    if (stopTimer >= stopTime)
		    {
			    stopTimer = 0f;
			    isMoving = true; // 再び移動
		    }
	    }
	    prevIsMoving = isMoving;
    }


    // ★★★ 弾幕の種類★★★
    void Shoot8Way()
    {
         var player = GameObject.FindGameObjectWithTag("Player");
         if (player == null) return;

         Vector3 playerPos = player.transform.position;

         float baseAngle = Mathf.Atan2(
               playerPos.y - transform.position.y,
               playerPos.x - transform.position.x
               );

         CGameManager gm = FindFirstObjectByType<CGameManager>();

         // 8方向発射（45度刻み）
         for (int i = 0; i < 8; i++)
         {
            Vector3 shotPos = transform.position;
            shotPos.z = BULLET_Z;
            
            float angle = baseAngle + (Mathf.PI / 4f) * i;

            GameObject bullet = Instantiate(bulletPrefab, shotPos, Quaternion.identity);

            bullet.GetComponent<CBullet>().SetParam(5.0f, angle);
         }
    }

    // ★★★ 2連射 × 1セット ★★★
    IEnumerator Shot5Bullets()
    {
        for (int burst = 0; burst < 1; burst++)
        {
            for (int i = 0; i < 2; i++)
            {
                // 上記弾幕の種類を入力
                Shoot8Way();

                for (int f = 0; f < 30; f++) yield return null;//バースト間隔
            }

            for (int f = 0; f < 35; f++) yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            int power = 0;
            var normalShot = collision.GetComponent<CShot>();
            var homingShot = collision.GetComponent<CShot_homing>();
            var lazerShot = collision.GetComponent<CShot_lazer_trigger>();

            if (normalShot != null) power = normalShot.ShotPower;
            if (homingShot != null) power = homingShot.ShotPower;
            if (lazerShot != null) power = lazerShot.ShotPower;

            Life -= power;

            if (Life <= 0) 
            {
              Destroy(gameObject);
              GameData.score += 100;
              GameData.stage2_total_score += 100;
              GameData.stage2_destroy_score += 100;
              Instantiate(Destroy_Enemy_Prefab, transform.position, Quaternion.identity);

              float baseAngle = -90f * Mathf.Deg2Rad; // 真下

              float[] angleOffsets =
              {
                180f * Mathf.Deg2Rad
              };
              foreach (float offset in angleOffsets)
              {
                float angle = baseAngle + offset;
                GameObject pointitem = Instantiate(pointitem_Prefab, transform.position, Quaternion.identity);
                pointitem.GetComponent<Cpointitem>().SetParam(angle);
              }

              float[] angleOffsets_homing =
              {
                10f * Mathf.Deg2Rad,
                -120f * Mathf.Deg2Rad
              };
              foreach (float offset in angleOffsets_homing)
              {
                float angle = baseAngle + offset;
                GameObject pointitem_homing = Instantiate(pointitem_homing_Prefab, transform.position, Quaternion.identity);
                pointitem_homing.GetComponent<Cpointitem_homing>().SetParam(angle);
              }
            }
            Destroy(collision.gameObject);
            GameData.score += 10;
            GameData.stage2_total_score += 10;
            GameData.stage2_hitpoint_score += 10;
        }
    }
}
