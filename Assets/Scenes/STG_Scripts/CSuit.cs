using UnityEngine;
using System.Collections;

public class CSuit : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject hitodama_Prefab;
    [SerializeField] GameObject Destroy_Enemy_Prefab;
    int Life = 15;

    const float BULLET_Z = 0f;
    int Cnt = 0;

    float radialSpeed = 3.3f; // 離れていく速さ（調整用）

    void Update()
    {
	    // ------- 移動 -------
	    Vector3 basePoint = new Vector3(0.06f, 12.7f, 5f);

       Vector3 dir = transform.position - basePoint;

       dir = dir.normalized;

       float speedMul = 1f;
       float cosTheta = Mathf.Abs(dir.y);
       if (cosTheta > 0.0001f)
       {
          speedMul = 1f / cosTheta;
       }
       transform.position += dir * radialSpeed * speedMul * Time.deltaTime;

       // -------- 弾発射--------
       if (Cnt == 20)
       {
          StartCoroutine(Shot5Bullets());
       }
       Cnt++;

       if (CUtility.IsOut(transform.position))
       {
          Destroy(gameObject);
       }
    }

    // ★★★ 弾幕の種類★★★
    IEnumerator Shot5Bullets()
    {
	    for (int burst = 0; burst < 2; burst++)
	    {
		    for (int i = 0; i < 5; i++)
		    {
			    Vector3 shotPos = transform.position;
			    shotPos.y += 1.0f;
			    shotPos.z = BULLET_Z;

			    GameObject bullet = Instantiate(bulletPrefab, shotPos, Quaternion.identity);

			    Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
			    float angle = Mathf.Atan2(
					    playerPos.y - transform.position.y,
					    playerPos.x - transform.position.x
					    );

			    bullet.GetComponent<CBullet>().SetParam(6.0f, angle);

			    for (int f = 0; f < 15; f++) yield return null;
		    }
		    for (int f = 0; f < 15; f++) yield return null;
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
              Instantiate(hitodama_Prefab, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
            GameData.score += 10;
            GameData.stage2_total_score += 10;
            GameData.stage2_hitpoint_score += 10;
        }
    }
}
