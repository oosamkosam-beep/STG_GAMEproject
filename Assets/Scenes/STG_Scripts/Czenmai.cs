using UnityEngine;
using System.Collections;

public class Czenmai : MonoBehaviour
{
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] GameObject bulletPrefab_2;
  [SerializeField] GameObject pointitem_Prefab;
  [SerializeField] GameObject pointitem_homing_Prefab;
  [SerializeField] GameObject Destroy_Enemy_Prefab;
  int Life = 60;

  const float BULLET_Z = 0f;
  Animator animator;

  Vector3 basePoint = new Vector3(0.06f, 12.7f, 5f);

  float moveTime = 0.1f;
  float stopTime = 0.9f;
  float moveTimer = 0f;
  float stopTimer = 0f;

  bool isMoving = true;   // true = 移動中 / false = 停止中
  float speedMul = 5f;
  float radialSpeed = 2.0f; 
  float stopSpeed = 1.0f; 

  void Start()
  {
    animator = GetComponent<Animator>();
    StartCoroutine(ShotBullets());
    StartCoroutine(Shot2wayBullet());
  }

  void Update()
  {
    if (isMoving)
    {
	    // ------- 移動 -------
      Vector3 dir = transform.position - basePoint;
      dir = dir.normalized;
      float cosTheta = Mathf.Abs(dir.y);
      if (cosTheta > 0.0001f)
      {
        speedMul = 1f / cosTheta;
      }
      transform.position += dir * radialSpeed * speedMul * Time.deltaTime;

      moveTimer += Time.deltaTime;

      if (moveTimer >= moveTime)
      {
        moveTimer = 0f;
        isMoving = false; // 停止へ
      }
    }
    else
    {
      Vector3 dir = transform.position - basePoint;
      dir = dir.normalized;
      float cosTheta = Mathf.Abs(dir.y);
      if (cosTheta > 0.0001f)
      {
        speedMul = 1f / cosTheta;
      }
      transform.position += dir * stopSpeed * speedMul * Time.deltaTime;
      
      stopTimer += Time.deltaTime;

      if (stopTimer >= stopTime)
      {
        stopTimer = 0f;
        isMoving = true; // 再び移動
      }
    }

    if (CUtility.IsOut(transform.position))
    {
      Destroy(gameObject);
    }
  }

	void Shoot2Way()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) return;

		Vector3 playerPos = player.transform.position;

		float baseAngle = Mathf.Atan2(
				playerPos.y - transform.position.y,
				playerPos.x - transform.position.x
				);

		float[] angleOffsets =
		{
			20f * Mathf.Deg2Rad,
			-20f * Mathf.Deg2Rad,
		};
		foreach (float offset in angleOffsets)
		{
			Vector3 shotPos = transform.position;
        shotPos.y += 1.0f;
        shotPos.z = BULLET_Z;
			float angle = baseAngle + offset;

			GameObject bullet = Instantiate(
					bulletPrefab_2,
					shotPos,
					Quaternion.identity
					);

      bullet.GetComponent<CBullet>().SetParam(8.0f, angle);
		}
  }
	IEnumerator Shot2wayBullet()
	{
    yield return new WaitForSeconds(2.0f);
		for (int burst = 0; burst < 9; burst++)
		{
			for (int i = 0; i < 1; i++)
			{
				Shoot2Way();
				for (int f = 0; f < 30; f++) yield return null;
			}
			for (int f = 0; f < 60; f++) yield return null;
		}
	}
  IEnumerator ShotBullets()
  {
    yield return new WaitForSeconds(2.0f);
    for (int burst = 0; burst < 9; burst++)
    {
      for (int i = 0; i < 1; i++)
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

        bullet.GetComponent<CBullet>().SetParam(8.0f, angle);

        for (int f = 0; f < 30; f++) yield return null;
      }
      for (int f = 0; f < 60; f++) yield return null;
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
