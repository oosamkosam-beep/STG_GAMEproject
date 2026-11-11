using UnityEngine;
using System.Collections;

public class CSuit : MonoBehaviour
{
    private RoadObject2D roadObject;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] GameObject hitodama_Prefab;
	[SerializeField] GameObject Destroy_Enemy_Prefab;
	int Life = 150;
	const float BULLET_Z = 0f;

	private float Speed = 0.5f; 

    private void Awake()
    {
        roadObject = GetComponent<RoadObject2D>();
    }

    private void Start()
    {
        // 弾発射
        StartCoroutine(ShotBullets());
    }

	private void Update()
	{
		// ------- 移動 -------
		roadObject.MoveDepth(RoadSpace2D.Autospeed + Speed);

		if (CUtility.IsOut(transform.position))
		{
			Destroy(gameObject);
		}
	}

	// ★★★ 弾幕の種類★★★
	IEnumerator ShotBullets()
	{
        yield return new WaitForSeconds(0.3f);

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
